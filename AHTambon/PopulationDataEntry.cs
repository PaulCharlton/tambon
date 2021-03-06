using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using De.AHoerstemeier.Geo;

namespace De.AHoerstemeier.Tambon
{
    public class PopulationDataEntry : ICloneable, IGeocode
    {
        public delegate void PopulationDataEntryEvent(PopulationDataEntry data, PopulationDataEntry parent);

        #region variables

        private List<Int32> mGeocodeParent = new List<Int32>(); // for former King Amphoe or Thesaban
        private List<Int32> mNewGeocode = new List<Int32>();
        private List<PopulationDataEntry> _subEntities = new List<PopulationDataEntry>();
        private List<String> mOldNames = new List<String>();
        private List<EntityOffice> mOffices = new List<EntityOffice>();
        private ConstituencyList mConstituencyList = new ConstituencyList();

        #endregion variables

        #region properties

        public String Name
        {
            get;
            set;
        }

        public String English
        {
            get;
            set;
        }

        public Int32 Male
        {
            get;
            set;
        }

        public Int32 Female
        {
            get;
            set;
        }

        public Int32 Total
        {
            get;
            set;
        }

        public Int32 Households
        {
            get;
            set;
        }

        public Int32 Geocode
        {
            get;
            set;
        }

        public Int32 GeocodeOfCorrespondingTambon
        {
            get;
            set;
        }

        public Boolean Obsolete
        {
            get;
            set;
        }

        public String Comment
        {
            get;
            set;
        }

        public List<EntityOffice> Offices
        {
            get
            {
                return mOffices;
            }
        }

        public List<PopulationDataEntry> SubEntities
        {
            get
            {
                return _subEntities;
            }
        }

        public List<String> OldNames
        {
            get
            {
                return mOldNames;
            }
        }

        private EntityType mType = EntityType.Unknown;

        public EntityType Type
        {
            get
            {
                return mType;
            }
            set
            {
                mType = value;
            }
        }

        public List<Int32> GeocodeParent
        {
            get
            {
                return mGeocodeParent;
            }
        }

        public List<Int32> NewGeocode
        {
            get
            {
                return mNewGeocode;
            }
        }

        public ConstituencyList ConstituencyList
        {
            get
            {
                return mConstituencyList;
            }
        }

        #endregion properties

        #region constructor

        public PopulationDataEntry()
        {
            Type = EntityType.Unknown;
            Total = 0;
            Male = 0;
            Female = 0;
            Households = 0;
            English = string.Empty;
            Geocode = 0;
            Obsolete = false;
        }

        public PopulationDataEntry(PopulationDataEntry value)
        {
            Type = value.Type;
            Total = value.Total;
            Male = value.Male;
            Female = value.Female;
            Households = value.Households;
            English = value.English;
            Geocode = value.Geocode;
            Obsolete = value.Obsolete;
            Name = value.Name;
            Comment = value.Comment;

            foreach ( Int32 lGeocode in value.mNewGeocode )
            {
                mNewGeocode.Add(lGeocode);
            }
            foreach ( Int32 lGeocode in value.mGeocodeParent )
            {
                mGeocodeParent.Add(lGeocode);
            }
            foreach ( PopulationDataEntry lSubEntity in value.SubEntities )
            {
                if ( lSubEntity != null )
                {
                    SubEntities.Add((PopulationDataEntry)lSubEntity.Clone());
                }
            }
            foreach ( EntityOffice lOffice in value.Offices )
            {
                Offices.Add((EntityOffice)lOffice.Clone());
            }
            foreach ( String lOldName in value.OldNames )
            {
                mOldNames.Add(lOldName);
            }
            foreach ( ConstituencyEntry lEntry in value.ConstituencyList )
            {
                ConstituencyList.Add((ConstituencyEntry)lEntry.Clone());
            }
        }

        public PopulationDataEntry(String iName, OfficeType iType, EntityLeaderList iLeaderList)
        {
            Name = iName;
            EntityOffice lOffice = new EntityOffice();
            lOffice.Type = iType;
            lOffice.OfficialsList = iLeaderList;
            Offices.Add(lOffice);
        }

        #endregion constructor

        #region methods

        public override string ToString()
        {
            return English;
        }

        internal void ParseName(string iValue)
        {
            if ( !String.IsNullOrEmpty(iValue) )
            {
                iValue = iValue.Replace("ทต.", EntityTypeHelper.EntityNames[EntityType.ThesabanTambon]);
                Type = EntityTypeHelper.ParseEntityType(iValue);
                if ( (Type == EntityType.Unknown) | (Type == EntityType.Bangkok) )
                {
                    Name = iValue;
                }
                else
                {
                    Name = iValue.Replace(EntityTypeHelper.EntityNames[Type], "");
                }
                if ( EntityTypeHelper.Sakha.Contains(Type) )
                {
                    // Some pages have the syntax "Name AmphoeName" with the word อำเภอ, others without
                    //Int32 pos = Name.IndexOf(Helper.EntityNames[EntityType.Amphoe]);
                    //if (pos > 0)
                    //{
                    //    mName = mName.Remove(pos - 1);
                    //}
                    Int32 pos = Name.IndexOf(" ");
                    if ( pos > 0 )
                    {
                        Name = Name.Remove(pos);
                    }
                }
                Obsolete = Name.Contains("*");
                Name = Name.Replace("*", "");
                if ( Name.StartsWith(".") )
                {
                    // Mistake in DOPA population statistic for Buriram 2005, a leading "."
                    Name = Name.Substring(1, Name.Length - 1);
                }
                Name = Name.Trim().Replace("  ", " ");  // "ปอภาร (ปอพาน)" may have a double blank in middle
            }
        }

        internal virtual void WriteToXMLNode(XmlElement iNode)
        {
            iNode.SetAttribute("name", Name);
            if ( !String.IsNullOrEmpty(English) )
            {
                iNode.SetAttribute("english", English);
            }
            if ( Geocode != 0 )
            {
                iNode.SetAttribute("geocode", Geocode.ToString());
            }
            String s = "";
            foreach ( Int32 i in NewGeocode )
            {
                s = s + i.ToString() + " ";
            }
            if ( !String.IsNullOrEmpty(s) )
            {
                s = s.Remove(s.Length - 1);
                iNode.SetAttribute("newgeocode", s);
            }

            if ( Type != EntityType.Unknown )
            {
                iNode.SetAttribute("type", Type.ToString());
            }
            if ( Total > 0 )
            {
                iNode.SetAttribute("total", Total.ToString());
            }
            if ( Female > 0 )
            {
                iNode.SetAttribute("female", Female.ToString());
            }
            if ( Male > 0 )
            {
                iNode.SetAttribute("male", Male.ToString());
            }
            if ( Households > 0 )
            {
                iNode.SetAttribute("households", Households.ToString());
            }
            s = "";
            foreach ( Int32 i in GeocodeParent )
            {
                s = s + i.ToString() + " ";
            }
            if ( !String.IsNullOrEmpty(s) )
            {
                s = s.Remove(s.Length - 1);
                iNode.SetAttribute("parent", s);
            }
            if ( !String.IsNullOrEmpty(Comment) )
            {
                iNode.SetAttribute("comment", Comment);
            }
            foreach ( EntityOffice lOffice in Offices )
            {
                lOffice.ExportToXML(iNode);
            }
            foreach ( PopulationDataEntry lEntity in SubEntities )
            {
                lEntity.ExportToXML(iNode);
            }
        }

        public void ExportToXML(XmlNode iNode)
        {
            XmlDocument lXmlDocument = TambonHelper.XmlDocumentFromNode(iNode);
            var lNewElement = (XmlElement)lXmlDocument.CreateNode("element", "entity", "");
            iNode.AppendChild(lNewElement);
            WriteToXMLNode(lNewElement);
        }

        internal static PopulationDataEntry Load(XmlNode iNode)
        {
            PopulationDataEntry RetVal = null;

            if ( iNode != null && iNode.Name.Equals("entity") )
            {
                RetVal = new PopulationDataEntry();
                RetVal.Name = TambonHelper.GetAttributeOptionalString(iNode, "name").Trim();
                RetVal.English = TambonHelper.GetAttributeOptionalString(iNode, "english").Trim();
                RetVal.Total = TambonHelper.GetAttributeOptionalInt(iNode, "total", 0);
                RetVal.Obsolete = TambonHelper.GetAttributeOptionalBool(iNode, "obsolete", false);
                RetVal.Male = TambonHelper.GetAttributeOptionalInt(iNode, "male", 0);
                RetVal.Female = TambonHelper.GetAttributeOptionalInt(iNode, "female", 0);
                RetVal.Households = TambonHelper.GetAttributeOptionalInt(iNode, "households", 0);
                RetVal.Geocode = TambonHelper.GetAttributeOptionalInt(iNode, "geocode", 0);
                RetVal.GeocodeOfCorrespondingTambon = TambonHelper.GetAttributeOptionalInt(iNode, "tambon", 0);
                RetVal.Comment = TambonHelper.GetAttributeOptionalString(iNode, "comment");
                string lNewGeocode = TambonHelper.GetAttributeOptionalString(iNode, "newgeocode");
                foreach ( string lSubString in lNewGeocode.Split(new Char[] { ' ' }) )
                {
                    if ( !string.IsNullOrEmpty(lSubString) )
                    {
                        RetVal.NewGeocode.Add(Convert.ToInt32(lSubString));
                    }
                }
                string s = TambonHelper.GetAttributeOptionalString(iNode, "type");
                if ( !String.IsNullOrEmpty(s) )
                {
                    RetVal.Type = (EntityType)Enum.Parse(typeof(EntityType), s);
                }
                string lGeocodeParent = TambonHelper.GetAttributeOptionalString(iNode, "parent");
                foreach ( string lSubString in lGeocodeParent.Split(new Char[] { ' ' }) )
                {
                    if ( !string.IsNullOrEmpty(lSubString) )
                    {
                        RetVal.GeocodeParent.Add(Convert.ToInt32(lSubString));
                    }
                }
                if ( iNode.HasChildNodes )
                {
                    foreach ( XmlNode lChildNode in iNode.ChildNodes )
                    {
                        if ( lChildNode.Name == "office" )
                        {
                            EntityOffice lOffice = EntityOffice.Load(lChildNode);
                            RetVal.Offices.Add(lOffice);
                        }
                        if ( lChildNode.Name == "history" )
                        {
                            RetVal.ParseHistory(lChildNode);
                        }
                        if ( lChildNode.Name == "entity" )
                        {
                            RetVal.SubEntities.Add(PopulationDataEntry.Load(lChildNode));
                        }
                        if ( lChildNode.Name == "constituencies" )
                        {
                            RetVal.ConstituencyList.ReadFromXml(lChildNode);
                        }
                    }
                }
            }
            return RetVal;
        }

        private void ParseHistory(XmlNode iNode)
        {
            if ( iNode != null && iNode.Name.Equals("history") )
            {
                if ( iNode.HasChildNodes )
                {
                    foreach ( XmlNode lChildNode in iNode.ChildNodes )
                    {
                        if ( lChildNode.Name == "misspelling" )
                        {
                            OldNames.Add(TambonHelper.GetAttribute(lChildNode, "name").Trim());
                        }
                        if ( lChildNode.Name == "rename" )
                        {
                            OldNames.Add(TambonHelper.GetAttribute(lChildNode, "oldname").Trim());
                        }
                    }
                }
            }
        }

        public void CopyStaticDataFrom(PopulationDataEntry iValue)
        {
            Geocode = iValue.Geocode;
            GeocodeOfCorrespondingTambon = iValue.GeocodeOfCorrespondingTambon;
            if ( !String.IsNullOrEmpty(iValue.English) )
            {
                English = iValue.English;
            }
            foreach ( Int32 i in iValue.GeocodeParent )
            {
                GeocodeParent.Add(i);
            }
            if ( EntityTypeHelper.IsCompatibleEntityType(Type, EntityType.Tambon) )
            {
                var lVillageList = new List<EntityType>() { EntityType.Muban };
                if ( (iValue.NrOfEntities(lVillageList) > 0) &
                    (this.NrOfEntities(lVillageList) == 0) )
                {
                    foreach ( PopulationDataEntry lVillage in iValue.SubEntities )
                    {
                        if ( EntityTypeHelper.IsCompatibleEntityType(lVillage.Type, EntityType.Muban) )
                        {
                            this.SubEntities.Add((PopulationDataEntry)lVillage.Clone());
                        }
                    }
                }
            }
        }

        public void CopyPopulationDataFrom(PopulationDataEntry iValue)
        {
            Total = iValue.Total;
            Households = iValue.Households;
            Male = iValue.Male;
            Female = iValue.Female;
        }

        internal void GetCodes(PopulationDataEntry geocodeSource)
        {
            List<PopulationDataEntry> missedEntities = new List<PopulationDataEntry>();
            if ( geocodeSource != null )
            {
                // this == geocodeSource => copy directly from source
                if ( ((Name == geocodeSource.Name) | (geocodeSource.OldNames.Contains(Name))) & (EntityTypeHelper.IsCompatibleEntityType(Type, geocodeSource.Type)) )
                {
                    CopyStaticDataFrom(geocodeSource);
                }

                foreach ( PopulationDataEntry subEntity in SubEntities )
                {
                    // find number of sub entities with same name and type
                    Int32 position = 0;
                    if ( subEntity.Type != EntityType.Muban )
                    {
                        foreach ( PopulationDataEntry find in SubEntities )
                        {
                            if ( find == subEntity )
                            {
                                break;
                            }
                            if ( find.SameNameAndType(subEntity.Name, subEntity.Type) )
                            {
                                position++;
                            }
                        }
                    }

                    PopulationDataEntry sourceEntity = null;
                    if ( subEntity.Type == EntityType.Muban )
                    {
                        sourceEntity = geocodeSource.FindByCode(subEntity.Geocode);
                    }
                    else
                    {
                        sourceEntity = geocodeSource.FindByNameAndType(subEntity.Name, subEntity.Type, false, position);
                        if ( sourceEntity == null )
                        {
                            sourceEntity = geocodeSource.FindByNameAndType(subEntity.Name, subEntity.Type, true, position);
                        }
                    }
                    if ( sourceEntity != null )
                    {
                        subEntity.GetCodes(sourceEntity);
                    }
                    else
                    {
                        // Problem!
                    }

                    if ( EntityTypeHelper.Thesaban.Contains(subEntity.Type) | (EntityTypeHelper.Sakha.Contains(subEntity.Type)) )
                    {
                        foreach ( Int32 parentCode in subEntity.GeocodeParent )
                        {
                            PopulationDataEntry parentEntity = geocodeSource.FindByCode(parentCode);
                            if ( parentEntity != null )
                            {
                                subEntity.GetCodes(parentEntity);
                                PopulationDataEntry sourceValue = this.FindByCode(parentCode);
                                if ( sourceValue == null )
                                {
                                    PopulationDataEntry newEntry = (PopulationDataEntry)parentEntity.Clone();
                                    newEntry.SubEntities.Clear();
                                    Boolean found = false;
                                    foreach ( PopulationDataEntry compare in missedEntities )
                                    {
                                        found = found | (compare.Geocode == newEntry.Geocode);
                                    }
                                    if ( !found )
                                    {
                                        missedEntities.Add(newEntry);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            foreach ( PopulationDataEntry newEntry in missedEntities )
            {
                PopulationDataEntry parent = this.FindByCode(newEntry.Geocode / 100);
                if ( parent != null )
                {
                    parent.SubEntities.Add(newEntry);
                }
                parent.SortSubEntitiesByGeocode();
            }
        }

        private PopulationDataEntry FindByNameAndType(String iValue, EntityType iEntityType, Boolean iAllowOldNames, Int32 iPosition)
        {
            PopulationDataEntry retval = null;
            retval = FindByNameAndType(iValue, iEntityType, iAllowOldNames, false, iPosition);
            if ( retval == null )
            {
                retval = FindByNameAndType(iValue, iEntityType, iAllowOldNames, true, iPosition);
            }
            return retval;
        }

        private bool SameNameAndType(String iName, EntityType iFindType)
        {
            return (Name == iName) & (EntityTypeHelper.IsCompatibleEntityType(Type, iFindType));
        }

        private Boolean NameAndTypeUnique(String iValue, EntityType iEntityType, Boolean iAllowOldNames, Boolean iAllowObsolete)
        {
            Int32 lCount = 0;
            foreach ( PopulationDataEntry lEntity in SubEntities )
            {
                if ( lEntity.SameNameAndType(iValue, iEntityType) )
                {
                    if ( (!lEntity.IsObsolete()) | iAllowObsolete )
                    {
                        lCount++;
                    }
                }
                if ( iAllowOldNames & (lEntity.OldNames.Contains(iValue)) & (EntityTypeHelper.IsCompatibleEntityType(lEntity.Type, iEntityType)) )
                {
                    if ( (!lEntity.IsObsolete()) | iAllowObsolete )
                    {
                        lCount++;
                    }
                }
            }
            return (lCount == 1);
        }

        private PopulationDataEntry FindByNameAndType(String iValue, EntityType iEntityType, Boolean iAllowOldNames, Boolean iAllowObsolete, Int32 iPosition)
        {
            PopulationDataEntry retval = null;
            SubEntities.Sort(
                delegate(PopulationDataEntry a, PopulationDataEntry b)
                {
                    return a.Geocode.CompareTo(b.Geocode);
                }
                );

            foreach ( PopulationDataEntry lEntity in SubEntities )
            {
                if ( lEntity.SameNameAndType(iValue, iEntityType) )
                {
                    if ( (!lEntity.IsObsolete()) | iAllowObsolete )
                    {
                        iPosition--;
                        if ( iPosition < 0 )
                        {
                            retval = lEntity;
                            break;
                        }
                    }
                }
                if ( iAllowOldNames & (lEntity.OldNames.Contains(iValue)) & (EntityTypeHelper.IsCompatibleEntityType(lEntity.Type, iEntityType)) )
                {
                    if ( (!lEntity.IsObsolete()) | iAllowObsolete )
                    {
                        iPosition--;
                        if ( iPosition < 0 )
                        {
                            retval = lEntity;
                            break;
                        }
                    }
                }
            }
            return retval;
        }

        private PopulationDataEntry FindByNameAndType(String iValue, EntityType iEntityType, Boolean iAllowOldNames)
        {
            PopulationDataEntry retval = null;
            retval = FindByNameAndType(iValue, iEntityType, iAllowOldNames, false, 0);
            if ( retval == null )
            {
                retval = FindByNameAndType(iValue, iEntityType, iAllowOldNames, true, 0);
            }
            return retval;
        }

        private PopulationDataEntry FindByName(string iValue, bool iAllowObsolete)
        {
            PopulationDataEntry retval = null;
            foreach ( PopulationDataEntry lEntity in SubEntities )
            {
                if ( lEntity.Name == iValue )
                {
                    if ( (!lEntity.IsObsolete()) | iAllowObsolete )
                    {
                        retval = lEntity;
                    }
                }
            }
            return retval;
        }

        private PopulationDataEntry FindByName(string iValue)
        {
            PopulationDataEntry retval = null;
            retval = FindByName(iValue, false);
            if ( retval == null )
            {
                retval = FindByName(iValue, true);
            }
            return retval;
        }

        public PopulationDataEntry FindByCode(Int32 code)
        {
            PopulationDataEntry retval = null;
            if ( this.Geocode == code )
            {
                retval = this;
            }
            else
            {
                retval = SubEntities.FirstOrDefault(entry => (entry != null) && (entry.Geocode == code));
                if ( retval == null )
                {
                    SubEntities.ForEach(delegate(PopulationDataEntry entity)
                    {
                        if ( (retval == null) && (entity != null) )
                        {
                            if ( TambonHelper.IsBaseGeocode(entity.Geocode, code) )
                            {
                                retval = entity.FindByCode(code);
                            }
                        }
                    });
                }
            }
            return retval;
        }

        private PopulationDataEntry FindByCodeAndType(Int32 iCode, EntityType iEntityType)
        {
            PopulationDataEntry retval = null;
            if ( (this.Geocode == iCode) & (this.Type == iEntityType) )
            {
                retval = this;
            }
            else
            {
                foreach ( PopulationDataEntry entry in this.SubEntities )
                {
                    retval = entry.FindByCodeAndType(iCode, iEntityType);
                    if ( retval != null )
                        break;
                }
            }
            return retval;
        }

        internal void AddTambonInThesabanToAmphoe(PopulationDataEntry iTambon, PopulationDataEntry iThesaban)
        {
            PopulationDataEntry lMainTambon = FindByCodeAndType(iTambon.Geocode, EntityType.Tambon);
            PopulationDataEntry lMainAmphoe = FindByCode(iTambon.Geocode / 100);
            if ( lMainTambon == null )
            {
                if ( lMainAmphoe != null )
                {
                    lMainTambon = (PopulationDataEntry)iTambon.Clone();
                    lMainAmphoe.SubEntities.Add(lMainTambon);
                }
            }
            else
            {
                lMainTambon.Total = lMainTambon.Total + iTambon.Total;
                lMainTambon.Male = lMainTambon.Male + iTambon.Male;
                lMainTambon.Female = lMainTambon.Female + iTambon.Female;
                lMainTambon.Households = lMainTambon.Households + iTambon.Households;
            }
            if ( lMainAmphoe != null )
            {
                lMainAmphoe.Total = lMainAmphoe.Total + iTambon.Total;
                lMainAmphoe.Male = lMainAmphoe.Male + iTambon.Male;
                lMainAmphoe.Female = lMainAmphoe.Female + iTambon.Female;
                lMainAmphoe.Households = lMainAmphoe.Households + iTambon.Households;
            }
            if ( lMainTambon != null )
            {
                PopulationDataEntry lNewEntity = new PopulationDataEntry()
                {
                    Geocode = iThesaban.Geocode,
                    Name = iThesaban.Name,
                    English = iThesaban.English,
                    Type = iThesaban.Type,
                    Total = iTambon.Total,
                    Male = iTambon.Male,
                    Female = iTambon.Female,
                    Households = iTambon.Households
                };
                lMainTambon.SubEntities.Add(lNewEntity);
            }
        }

        internal Boolean IsObsolete()
        {
            Boolean retval = (Obsolete) | (NewGeocode.Any());
            return retval;
        }

        public void CalculateNumbersFromSubEntities()
        {
            this.Female = 0;
            this.Male = 0;
            this.Households = 0;
            this.Total = 0;
            foreach ( PopulationDataEntry lEntry in SubEntities )
            {
                if ( lEntry != null )
                {
                    this.Female += lEntry.Female;
                    this.Male += lEntry.Male;
                    this.Households += lEntry.Households;
                    this.Total += lEntry.Total;
                }
            }
        }

        public Int32 NrOfEntities(List<EntityType> iRequestedType)
        {
            Int32 retval = 0;
            foreach ( PopulationDataEntry lEntity in SubEntities )
            {
                if ( (iRequestedType.Contains(lEntity.Type)) & (!lEntity.IsObsolete()) )
                {
                    retval++;
                }
                retval = retval + lEntity.NrOfEntities(iRequestedType);
            }
            return retval;
        }

        public Boolean CanWriteForWikipedia()
        {
            Boolean lResult = EntityTypeHelper.IsCompatibleEntityType(Type, EntityType.Amphoe);
            if ( Type == EntityType.Tambon )
            {
                lResult = SubEntities.Find(
                    delegate(PopulationDataEntry lEntry)
                    {
                        return lEntry.Type == EntityType.Muban;
                    }) != null;
            }
            return lResult;
        }

        public String WriteForWikipedia(String iPopulationReference)
        {
            String lResult = String.Empty;
            if ( Type == EntityType.Tambon )
            {
                lResult = WriteMubanListForWikipedia();
            }
            else
            {
                lResult = WritePopulationForWikipedia(iPopulationReference);
            }
            return lResult;
        }

        private String WriteMubanListForWikipedia()
        {
            StringWriter lWriter = new StringWriter();
            lWriter.WriteLine("{|");

            lWriter.WriteLine("! No.");
            lWriter.WriteLine("! Name");
            lWriter.WriteLine("! Thai");
            Int32 lMaxGeocode = 0;
            foreach ( PopulationDataEntry lEntity in this.SubEntities )
            {
                lMaxGeocode = Math.Max(lMaxGeocode, lEntity.Geocode % 100);
            }
            foreach ( PopulationDataEntry lEntity in this.SubEntities )
            {
                lWriter.WriteLine("|-");
                Int32 lGeocode = lEntity.Geocode % 100;
                lWriter.Write("||");
                String lGeocodeString = lGeocode.ToString() + ".";
                if ( (lGeocode < 10) & (lMaxGeocode >= 10) )
                {
                    lGeocodeString = "{{0}}" + lGeocodeString;
                }
                lWriter.Write(lGeocodeString);
                lWriter.Write("||");
                lWriter.Write(lEntity.English);
                lWriter.Write("||");
                lWriter.Write(lEntity.Name);
                lWriter.WriteLine();
            }
            lWriter.WriteLine("|}");

            return lWriter.ToString();
        }

        private String WritePopulationForWikipedia(String iPopulationReference)
        {
            Int32 lVillageNumberTotal = NrOfEntities(new List<EntityType>() { EntityType.Muban });
            Boolean lShowVillages = (lVillageNumberTotal > 0) & (EntityTypeHelper.IsCompatibleEntityType(this.Type, EntityType.Amphoe));
            StringWriter lWriter = new StringWriter();
            lWriter.WriteLine("{|");

            lWriter.WriteLine("! No.");
            lWriter.WriteLine("! Name");
            lWriter.WriteLine("! Thai");
            if ( lShowVillages )
            {
                lWriter.WriteLine("! Villages");
            }
            lWriter.WriteLine("! [[Population|Inh.]]" + iPopulationReference);
            Int32 lMaxGeocode = 0;
            Int32 lMaxVillageCount = 0;
            Int32 lMaxPopulation = 0;
            foreach ( PopulationDataEntry lEntity in this.SubEntities )
            {
                lMaxGeocode = Math.Max(lMaxGeocode, lEntity.Geocode % 100);
                lMaxPopulation = Math.Max(lMaxPopulation, lEntity.Total);
                lMaxVillageCount = Math.Max(lMaxVillageCount, lEntity.NrOfEntities(new List<EntityType>() { EntityType.Muban }));
            }
            foreach ( PopulationDataEntry lEntity in this.SubEntities )
            {
                lWriter.WriteLine("|-");
                Int32 lGeocode = lEntity.Geocode % 100;
                lWriter.Write("||");
                String lGeocodeString = lGeocode.ToString() + ".";
                if ( (lGeocode < 10) & (lMaxGeocode >= 10) )
                {
                    lGeocodeString = "{{0}}" + lGeocodeString;
                }
                lWriter.Write(lGeocodeString);
                lWriter.Write("||");
                lWriter.Write(lEntity.English);
                lWriter.Write("||");
                lWriter.Write(lEntity.Name);
                lWriter.Write("||");
                if ( lShowVillages )
                {
                    Int32 lVillageCount = lEntity.NrOfEntities(new List<EntityType>() { EntityType.Muban });
                    if ( lVillageCount == 0 )
                    {
                        lWriter.Write("-");
                    }
                    else
                    {
                        String lVillages = lVillageCount.ToString();
                        if ( lVillageCount < 10 )
                        {
                            lVillages = "{{0}}" + lVillages;
                        }
                        lWriter.Write(lVillages);
                    }
                    lWriter.Write("||");
                }
                String lPopulation = lEntity.Total.ToString("###,##0", TambonHelper.CultureInfoUS);
                for ( int i = lPopulation.Length ; i < lMaxPopulation.ToString("###,##0").Length ; i++ )
                {
                    lPopulation = "{{0}}" + lPopulation;
                }
                lWriter.Write(lPopulation);
                lWriter.WriteLine();
            }
            lWriter.WriteLine("|}");
            String lResult = lWriter.ToString();

            return lResult;
        }

        public String SubNames(List<EntityType> iRequestedType)
        {
            var lSubNamesList = SubNamesList(iRequestedType);
            string retval = "";
            foreach ( string s in lSubNamesList )
            {
                retval = retval + s + ", ";
            }
            if ( !string.IsNullOrEmpty(retval) )
            {
                retval = retval.Remove(retval.Length - 2);
            }
            return retval;
        }

        /// <summary>
        /// Sort the sub entity list by the geocode recursively.
        /// </summary>
        public void SortSubEntitiesByGeocode()
        {
            _subEntities = SubEntities.OrderBy(p => p.Geocode).ToList();
            foreach ( var entry in SubEntities )
            {
                entry.SortSubEntitiesByGeocode();
            }
        }

        /// <summary>
        /// Sort the sub entity list by the english name recursively.
        /// </summary>
        public void SortSubEntitiesByEnglishName()
        {
            _subEntities = SubEntities.OrderBy(p => p.English).ToList();
            foreach ( var entry in SubEntities )
            {
                entry.SortSubEntitiesByEnglishName();
            }
        }

        internal void IterateEntitiesWithoutGeocode(PopulationDataEntryEvent iCallback, PopulationDataEntry parent)
        {
            if ( this.Geocode == 0 )
            {
                iCallback(this, parent);
            }
            if ( SubEntities != null )
            {
                foreach ( PopulationDataEntry lSubEntity in SubEntities )
                {
                    if ( lSubEntity != null )
                    {
                        lSubEntity.IterateEntitiesWithoutGeocode(iCallback, this);
                    }
                }
            }
        }

        protected List<string> SubNamesList(List<EntityType> iRequestedType)
        {
            List<string> RetVal = new List<string>();
            foreach ( PopulationDataEntry subelement in this.SubEntities )
            {
                if ( iRequestedType.Contains(subelement.Type) )
                {
                    if ( !RetVal.Contains(subelement.English) )
                    {
                        RetVal.Add(subelement.English);
                    }
                }
                var lSubNamesList = subelement.SubNamesList(iRequestedType);
                foreach ( string s in lSubNamesList )
                {
                    if ( !RetVal.Contains(s) )
                    {
                        RetVal.Add(s);
                    }
                }
            }
            RetVal.Sort();
            return RetVal;
        }

        public List<PopulationDataEntry> FlatList(List<EntityType> entityTypeFilter)
        {
            var RetVal = new List<PopulationDataEntry>();

            foreach ( PopulationDataEntry entry in SubEntities )
            {
                if ( entry != null )
                {
                    if ( entityTypeFilter.Contains(entry.Type) )
                    {
                        RetVal.Add(entry);
                    }
                    RetVal.AddRange(entry.FlatList(entityTypeFilter));
                }
            }
            return RetVal;
        }

        public Dictionary<EntityType, Int32> EntityTypeNumbers()
        {
            Dictionary<EntityType, Int32> Result = new Dictionary<EntityType, Int32>();

            foreach ( var entry in FlatList(EntityTypeHelper.CentralAdministration) )
            {
                if ( !Result.Keys.Contains(entry.Type) )
                {
                    Result[entry.Type] = 0;
                }
                Result[entry.Type]++;
            }

            foreach ( var entry in ThesabanList() )
            {
                if ( !Result.Keys.Contains(entry.Type) )
                {
                    Result[entry.Type] = 0;
                }
                Result[entry.Type]++;
            }

            return Result;
        }

        public Boolean LeaderAlreadyInList(EntityLeader iLeader)
        {
            Boolean RetVal = false;
            foreach ( EntityOffice lOffice in Offices )
            {
                if ( lOffice.OfficialsList != null )
                {
                    foreach ( EntityLeader lLeader in lOffice.OfficialsList )
                    {
                        RetVal = RetVal | ((lLeader.Name == iLeader.Name) && (lLeader.Position == iLeader.Position));
                    }
                }
            }
            return RetVal;
        }

        internal List<PopulationDataEntry> InvalidGeocodeEntries()
        {
            List<PopulationDataEntry> lResult = new List<PopulationDataEntry>();

            foreach ( PopulationDataEntry lEntry in SubEntities )
            {
                if ( !TambonHelper.IsBaseGeocode(this.Geocode, lEntry.Geocode) )
                {
                    lResult.Add(lEntry);
                }

                Int32 lCount = 0;
                foreach ( PopulationDataEntry lCountEntry in SubEntities )
                {
                    if ( lCountEntry.Geocode == lEntry.Geocode )
                    {
                        lCount++;
                    }
                }
                if ( lCount > 1 )
                {
                    lResult.Add(lEntry);
                }

                lResult.AddRange(lEntry.InvalidGeocodeEntries());
            }
            return lResult;
        }

        public void ExportToKml(String iFilename)
        {
            KmlHelper lKmlWriter = new KmlHelper();
            EntityOffice.AddKmlStyles(lKmlWriter);
            WriteToKml(lKmlWriter, lKmlWriter.DocumentNode);
            lKmlWriter.SaveToFile(iFilename);
        }

        private void WriteToKml(KmlHelper lKmlWriter, XmlNode iNode)
        {
            XmlNode lNode = iNode;
            String lDescription = "Geocode: " + Geocode.ToString();
            if ( (Type == EntityType.Changwat) | (Type == EntityType.Bangkok) )
            {
                lNode = lKmlWriter.AddFolder(lNode, English, false);
            }
            String lName = English;
            if ( Type == EntityType.Muban )
            {
                lName = "Mu " + (Geocode % 100).ToString();
                if ( !String.IsNullOrEmpty(English) )
                {
                    lName = lName + ' ' + English;
                }
            }
            if ( !String.IsNullOrEmpty(this.Comment) )
            {
                lDescription = lDescription + Environment.NewLine + this.Comment;
            }
            foreach ( EntityOffice lOffice in Offices )
            {
                lOffice.AddToKml(lKmlWriter, lNode, lName, lDescription);
            }
            foreach ( PopulationDataEntry lEntity in SubEntities )
            {
                lEntity.WriteToKml(lKmlWriter, lNode);
            }
        }

        #endregion methods

        #region ICloneable Members

        public object Clone()
        {
            return new PopulationDataEntry(this);
        }

        #endregion ICloneable Members

        #region IGeocode Members

        public bool IsAboutGeocode(int iGeocode, bool iIncludeSubEntities)
        {
            Boolean retval = TambonHelper.IsSameGeocode(iGeocode, Geocode, iIncludeSubEntities);
            return retval;
        }

        #endregion IGeocode Members

        public List<PopulationDataEntry> ThesabanList()
        {
            List<PopulationDataEntry> lResult = new List<PopulationDataEntry>();
            List<PopulationDataEntry> lThesaban = this.FlatList(EntityTypeHelper.Thesaban);

            lThesaban.Sort(delegate(PopulationDataEntry p1, PopulationDataEntry p2)
            {
                return (p2.Geocode.CompareTo(p1.Geocode));
            });
            foreach ( PopulationDataEntry lEntry in lThesaban )
            {
                PopulationDataEntry lResultEntry = lResult.Find(delegate(PopulationDataEntry p)
                {
                    return (p.Geocode == lEntry.Geocode);
                });
                if ( lResultEntry == null )
                {
                    lResult.Add(new PopulationDataEntry(lEntry));
                }
                else
                {
                    lResultEntry.AddNumbers(lEntry);
                }
            }
            return lResult;
        }

        private void AddNumbers(PopulationDataEntry iEntry)
        {
            this.Households += iEntry.Households;
            this.Female += iEntry.Female;
            this.Male += iEntry.Male;
            this.Total += iEntry.Total;
        }

        public void CopyPopulationToConstituencies(PopulationDataEntry iPopulationSource)
        {
            PopulationDataEntry lSourcePopulationdataEntry = iPopulationSource.FindByCode(Geocode);
            if ( lSourcePopulationdataEntry != null )
            {
                lSourcePopulationdataEntry.CalculateNumbersFromSubEntities();
                CopyPopulationDataFrom(lSourcePopulationdataEntry);
            }
            Debug.Assert(lSourcePopulationdataEntry != null, "No source data entry with geocode " + Geocode.ToString());
            if ( lSourcePopulationdataEntry != null )
            {
                foreach ( ConstituencyEntry lConstituency in ConstituencyList )
                {
                    GetPopulationData(lSourcePopulationdataEntry, lConstituency.AdministrativeEntities);
                    foreach ( var lKeyValuePair in lConstituency.ExcludedAdministrativeEntities )
                    {
                        PopulationDataEntry lSourceSubEntity = iPopulationSource.FindByCode(lKeyValuePair.Key.Geocode);
                        GetPopulationData(lSourceSubEntity, lKeyValuePair.Value);
                    }
                    foreach ( var lKeyValuePair in lConstituency.SubIncludedAdministrativeEntities )
                    {
                        PopulationDataEntry lSourceSubEntity = iPopulationSource.FindByCode(lKeyValuePair.Key.Geocode);
                        GetPopulationData(lSourceSubEntity, lKeyValuePair.Value);
                    }
                }
            }
            Int32 lPopulation = ConstituencyList.Population();
            Int32 lTotal = Total;
            if ( (lPopulation > 0) && (lTotal > 0) )
            {
                Int32 lPopulationDiff = lPopulation - lTotal;
                Debug.Assert(lPopulationDiff == 0, "Population for " + English + " does not sum up, off by " + lPopulationDiff.ToString());
            }
        }

        private static void GetPopulationData(PopulationDataEntry iPopulation, List<PopulationDataEntry> iData)
        {
            if ( iPopulation == null )
            {
                throw new ArgumentNullException("iPopulation", "No source for population data");
            }
            List<PopulationDataEntry> lNewEntityList = new List<PopulationDataEntry>();
            List<PopulationDataEntry> lThesabanList = iPopulation.ThesabanList();
            foreach ( PopulationDataEntry lConstituencyEntry in iData )
            {
                PopulationDataEntry lPopulationdataEntry = null;
                foreach ( PopulationDataEntry lThesaban in lThesabanList )
                {
                    if ( lThesaban.Geocode == lConstituencyEntry.Geocode )
                    {
                        lPopulationdataEntry = lThesaban;
                        break;
                    }
                }
                if ( lPopulationdataEntry == null )
                {
                    lPopulationdataEntry = iPopulation.FindByCode(lConstituencyEntry.Geocode);
                }
                Debug.Assert(lPopulationdataEntry != null, "Entity with code " + lConstituencyEntry.Geocode.ToString() + " not found");
                if ( lPopulationdataEntry != null )
                {
                    lNewEntityList.Add(lPopulationdataEntry);
                }
            }
            iData.Clear();
            iData.AddRange(lNewEntityList);
        }
    }
}