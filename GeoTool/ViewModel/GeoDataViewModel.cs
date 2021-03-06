﻿using System;
using System.Collections.Generic;
using System.Linq;
using De.AHoerstemeier.Geo;
using De.AHoerstemeier.Tambon;
using De.AHoerstemeier.GeoTool.Model;
using GalaSoft.MvvmLight;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl.WPF.Core;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace De.AHoerstemeier.GeoTool.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    [System.Runtime.InteropServices.GuidAttribute("B10CB298-EC04-44BD-84A2-A1087FF95AF4")]
    public class GeoDataViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the GeoDataViewModel class.
        /// </summary>
        public GeoDataViewModel(GeoDataModel model)
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real": Connect to service, etc...
            ////}

            Model = model;
            Model.PropertyChanged += (s, e) =>
            {
                if ( e.PropertyName == GeoDataModel.DatumPropertyName )
                {
                    RaisePropertyChanged(CurrentGeoDatumPropertyName);
                }
                if ( e.PropertyName == GeoDataModel.LocationPropertyName )
                {
                    RaisePropertyChanged(GeoHashPropertyName);
                    RaisePropertyChanged(GeoLocationPropertyName);
                    RaisePropertyChanged(UtmLocationPropertyName);
                    RaisePropertyChanged(MgrsLocationPropertyName);
                    RaisePropertyChanged(L7018FramePropertyName);
                }
                GeoPoint point = new GeoPoint(Model.Location);
                point.Datum = Model.Datum;
                RaisePropertyChanged(GeoPointPropertyName, _oldLocation, point, true);
                _oldLocation = point;
            };

            GeoDatums = new ObservableCollection<GeoDatum>();
            GeoDatums.Add(GeoDatum.DatumWGS84());
            GeoDatums.Add(GeoDatum.DatumIndian1975());
            GeoDatums.Add(GeoDatum.DatumIndian1954());

            L7018Index = new ObservableCollection<RtsdMapFrame>();
            RtsdMapIndex.CalcIndexList();
            foreach (var entry in RtsdMapIndex.MapIndexL7018.OrderBy(x => x.Name))
            {
                L7018Index.Add(entry);
            }

            _oldLocation = new GeoPoint(Model.Location);
            _oldLocation.Datum = Model.Datum;
        }

        private GeoPoint _oldLocation = null;
        public static String GeoPointPropertyName = "GeoPoint";
        public GeoDataModel Model
        {
            get;
            private set;
        }

        /// <summary>
        /// The <see cref="GeoDatums" /> property's name.
        /// </summary>
        public const string GeoDatumsPropertyName = "GeoDatums";

        private ObservableCollection<GeoDatum> _GeoDatums = null;

        /// <summary>
        /// Gets the GeoDatums property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<GeoDatum> GeoDatums
        {
            get
            {
                return _GeoDatums;
            }

            set
            {
                if ( _GeoDatums == value )
                {
                    return;
                }

                var oldValue = _GeoDatums;
                _GeoDatums = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(GeoDatumsPropertyName);

            }
        }

        public const String CurrentGeoDatumPropertyName = "CurrentGeoDatum";
        public GeoDatum CurrentGeoDatum
        {
            get { return Model.Datum; }
            set
            {
                if ( Model.Datum == value )
                {
                    return;
                }
                Model.Datum = value;
            }
        }

        public const String GeoHashPropertyName = "GeoHash";
        public String GeoHash
        {
            get { return Model.Location.GeoHash; }
            set { Model.SetGeoHash(value); }
        }

        public const String GeoLocationPropertyName = "GeoLocation";
        public String GeoLocation
        {
            get { return Model.Location.ToString(); }
            set { Model.SetGeoLocation(value); }
        }

        public const String UtmLocationPropertyName = "UtmLocation";
        public String UtmLocation
        {
            get { return Model.UtmLocation.ToString(); }
            set { Model.SetUtmLocation(value); }
        }

        public const String MgrsLocationPropertyName = "MgrsLocation";
        public String MgrsLocation
        {
            get { return Model.UtmLocation.ToMgrsString(6); }
            set { Model.SetMgrsLocation(value); }
        }

        public const String L7018IndexPropertyName = "L7018Index";
        private ObservableCollection<RtsdMapFrame> _L7018Index = null;
        public ObservableCollection<RtsdMapFrame> L7018Index
        {
            get
            {
                return _L7018Index;
            }
            set
            {
                if (_L7018Index == value)
                {
                    return;
                }

                var oldL7018Index = _L7018Index;
                _L7018Index = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(L7018IndexPropertyName);

            }

        }

        public const String L7018FramePropertyName = "L7018Frame";
        public RtsdMapFrame L7018Frame
        {
            get { return Model.L7018Frame; }
            set
            {
                if (Model.L7018Frame == value)
                {
                    return;
                }
                Model.L7018Frame = value;
            }
        }
    }

}