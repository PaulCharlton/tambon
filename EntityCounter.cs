﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace De.AHoerstemeier.Tambon
{
    class EntityCounter
    {
        private Dictionary<String, Int32> mNamesCount = new Dictionary<String, Int32>();
        private List<EntityType> mEntityTypes;
        public Int32 BaseGeocode { get; set; }

        public EntityCounter(List<EntityType> iEntityTypes)
        {
            mEntityTypes = iEntityTypes;
        }
        public String CommonNames(Int32 iCutOff)
        {
            StringBuilder lBuilder = new StringBuilder();

            List<KeyValuePair<String, Int32>> lSorted = new List<KeyValuePair<String, Int32>>();
            foreach (KeyValuePair<String, Int32> lKeyValuePair in mNamesCount)
            {
                String lName = lKeyValuePair.Key;
                lSorted.Add(lKeyValuePair);
            }
            lSorted.Sort(delegate(KeyValuePair<String, Int32> x, KeyValuePair<String, Int32> y) { return y.Value.CompareTo(x.Value); });
            Int32 lCount = 0;
            foreach (KeyValuePair<String, Int32> lKeyValuePair in lSorted)
            {
                lBuilder.AppendLine(lKeyValuePair.Key + " (" + lKeyValuePair.Value.ToString() + ") ");
                lCount++;
                if (lCount > iCutOff)
                {
                    break;
                }

            }

            String RetVal = lBuilder.ToString();
            return RetVal;
        }

        private List<PopulationDataEntry> LoadGeocodeLists()
        {
            var lList = new List<PopulationDataEntry>();
            foreach (PopulationDataEntry lEntry in Helper.Geocodes)
            {
                String lFilename = Helper.GeocodeSourceFile(lEntry.Geocode);
                if (File.Exists(lFilename))
                {
                    PopulationData lEntities = PopulationData.Load(lFilename);
                    lList.AddRange(lEntities.Data.FlatList(mEntityTypes));
                }
            }
            return lList;
        }
        private static List<PopulationDataEntry> NormalizeNames(List<PopulationDataEntry> iList)
        {
            foreach (PopulationDataEntry lEntry in iList)
            {
                if (lEntry.Type == EntityType.Muban)
                {
                    lEntry.Name = Helper.StripBan(lEntry.Name);
                }
            }
            iList.Sort(delegate(PopulationDataEntry p1, PopulationDataEntry p2) { return p1.Name.CompareTo(p2.Name); });
            return iList;
        }
        private static Dictionary<String, Int32> DoCalculate(List<PopulationDataEntry> iList)
        {
            Dictionary<String, Int32> lResult = new Dictionary<String, Int32>();
            String lLastname = String.Empty;
            Int32 lCount = 0;
            foreach (PopulationDataEntry lEntry in iList)
            {
                if (lEntry.IsObsolete())
                { }
                else if (lEntry.Name == lLastname)
                {
                    lCount++;
                }
                else
                {
                    if (!String.IsNullOrEmpty(lLastname))
                    {
                        lResult.Add(lLastname, lCount + 1);
                    }
                    lCount = 0;
                    lLastname = lEntry.Name;
                }
            }
            if (!String.IsNullOrEmpty(lLastname))
            {
                lResult.Add(lLastname, lCount + 1);
            }
            return lResult;
        }
        public void Calculate()
        {
            var lList = LoadGeocodeLists();
            lList = NormalizeNames(lList);
            mNamesCount = DoCalculate(lList);
        }
    }
}
