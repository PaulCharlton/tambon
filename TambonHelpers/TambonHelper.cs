using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace De.AHoerstemeier.Tambon
{
    /// <summary>
    /// Static helper class.
    /// </summary>
    public static class TambonHelper
    {
        #region constants

        public const Int32 PopulationStatisticMaxYear = 2011;
        public const Int32 PopulationStatisticMinYear = 1993;
        public static XNamespace TambonNameSpace = "http://hoerstemeier.com/tambon/";

        #endregion constants

        static public Encoding ThaiEncoding = Encoding.GetEncoding(874);
        static public CultureInfo CultureInfoUS = new CultureInfo("en-us");

        public static Dictionary<EntityLeaderType, String> EntityLeaderName = new Dictionary<EntityLeaderType, String>()
        {
            {EntityLeaderType.Governor, "ผู้ว่าราชการจังหวัด"},
            {EntityLeaderType.ViceGovernor, "รองผู้ว่าราชการจังหวัด"},
            {EntityLeaderType.DistrictOfficer,"นายอำเภอ"},
            {EntityLeaderType.MinorDistrictOfficer,"หัวหน้ากิ่งอำเภอ"},
            {EntityLeaderType.Kamnan,"กำนัน"},
            {EntityLeaderType.PhuYaiBan,"ผู้ใหญ่บ้าน"},
            {EntityLeaderType.Mayor,"นายกเทศมนตรี"},
            {EntityLeaderType.TAOMayor,"นายกองค์การบริหารส่วนตำบล"},
            {EntityLeaderType.TAOChairman,"ประธานกรรมการบริหารองค์การบริหารส่วนตำบล"},
            {EntityLeaderType.PAOChairman,"นายกองค์การบริหารส่วนจังหวัด"},
            {EntityLeaderType.SanitaryDistrictChairman,"ประธานกรรมการสุขาภิบาล"},
            {EntityLeaderType.ChumchonChairman,"ประธานชุมชน"}
        };

        public static Dictionary<GazetteSignPosition, String> GazetteSignPositionThai = new Dictionary<GazetteSignPosition, string>()
        {
            {GazetteSignPosition.PrimeMinister, "นายกรัฐมนตรี" },
            {GazetteSignPosition.DeputyPrimeMinister, "รองนายกรัฐมนตรี" },
            {GazetteSignPosition.MinisterOfInterior, "รัฐมนตรีว่าการกระทรวงมหาดไทย" },
            {GazetteSignPosition.DeputyMinisterOfInterior, "รัฐมนตรีช่วยว่าการกระทรวงมหาดไทย" },
            {GazetteSignPosition.MinistryOfInteriorPermanentSecretary, "ปลัดกระทรวงมหาดไทย" },
            {GazetteSignPosition.ProvinceGovernor, "ผู้ว่าราชการจังหวัด" },
            {GazetteSignPosition.ViceProvinceGovernor, "รองผู้ว่าราชการจังหวัด" },
            // {GazetteSignPosition.BangkokGovernor, ""},
            {GazetteSignPosition.BangkokPermanentSecretary, "ปลัดกรุงเทพ" },
            {GazetteSignPosition.DeputyBangkokPermanentSecretary, "รองปลัดกรุงเทพ" },
            {GazetteSignPosition.MinisterOfInformationAndCommunicationTechnology, "รัฐมนตรีว่าการกระทรวงเทคโนโลยีสารสนเทศและการสื่อสาร" },
            {GazetteSignPosition.ElectionCommissionPresident, "ประธานกรรมการการเลือกตั้ง" },
            {GazetteSignPosition.RoyalInstitutePresident, "นายกราชบัณฑิตยสถาน" } ,
            {GazetteSignPosition.RoyalInstituteActingPresident, "รักษาการตำแหน่งนายกราชบัณฑิตยสถาน" },
            {GazetteSignPosition.DepartmentOfTransportDirectorGeneral,"อธิบดีกรมการขนส่งทางบก" },
            {GazetteSignPosition.DistrictOfficerBangkok,"ผู้อำนวยการเขต"},
            {GazetteSignPosition.DistrictOfficer,"นายอำเภอ"},
            {GazetteSignPosition.SpeakerOfParliament,"ประธานสภาผู้แทนราษฎร"},
            {GazetteSignPosition.Mayor,"นายกเทศมนตรี"},
            {GazetteSignPosition.TAOMayor,"นายกองค์การบริหารส่วนตำบล"},
            {GazetteSignPosition.PAOChairman,"นายกองค์การบริหารส่วนจังหวัด"},
            {GazetteSignPosition.MunicipalPermanentSecretary,"ปลัดเทศบาล"},
            {GazetteSignPosition.MinistryOfInteriorDeputyPermanentSecretary,"รองปลัดกระทรวงมหาดไทย"},
            {GazetteSignPosition.FineArtsDepartmentDirectorGeneral,"อธิบดีกรมศิลปากร"}
        };

        public static Dictionary<String, String> NameSuffixRomanizations = new Dictionary<String, String>()
        {
            {"เหนือ", "Nuea"},  //North
            {"ใต้", "Tai"}, // South
            {"พัฒนา", "Phatthana"}, // Development
            {"ใหม่", "Mai"},  // New
            {"ทอง", "Thong"}, // Gold
            {"น้อย","Noi"}, // Small
            {"ใน", "Nai"},  // within
            // less common ones
            { "สามัคคี", "Samakkhi" },  // Harmonious
            { "ใหม่พัฒนา", "Mai Phatthana"}, // New Development
            {"ตะวันออก", "Tawan Ok"}, // East
            {"ตะวันตก", "Tawan Tok"}, // West
            {"สอง", "Song"},  // second
            {"กลาง", "Klang"}, // Middle
            {"คำ", "Kham"},  // Word
            {"ใหญ่", "Yai"}, // Large
            {"เล็ก","Lek"},  // small
            {"เก่า", "Kao"}, // Old
            {"สันติสุข", "Santi Suk"},  // peace
            {"เจริญ", "Charoen"},  // growth
            {"ศรีเจริญ", "Si Charoen"},
            {"เจริญสุข","Charoen Suk"},
            {"บูรพา", "Burapha"},  // East
            {"สวรรค์", "Sawan"}, // Heaven
            {"หลวง", "Luang"},  // Big
            {"งาม", "Ngam"},     // Beautiful
            {"สมบูรณ์", "Sombun"}, // Complete
            {"สะอาด", "Sa-at"},  // clean
            {"นอก","Nok"},  // outside
            {"แดง","Daeng"},  // red
            {"ดง","Dong"},
            {"ไร","Rai"},  // Gain
            {"ราษฏร์","Rat"}, // people
            {"อรุณ","Arun"},  // dawn
            {"เรือ", "Ruea"},  // boat
            {"เฒ่า", "Thao"},  // old
            {"ยืน", "Yuen"},  // durable
            {"ยาง","Yang"},  // Rubber
            {"บน", "Bon"},  // upon
            {"อุดม", "Udom"},  // rich
            {"เดิม","Doem"},  // old
            {"บำรุง","Bamrung"}, // administrate
            {"เตียน","Tian"},
            {"เหลี่ยม","Liam"},
            {"คีรี","Khiri"},
            {"เด่น","Den"},  // notable
            {"สำนัก","Samnak"},  // office
            {"มงคล","Mongkhon"},  // dragon
            {"ศิริ","Siri"},
            {"ถาวร","Thawon"},  // permanent
            {"นคร", "Nakhon"},  // city
            {"สูง","Sung"},  // high
            {"ต่ำ","Tam"},  // low
            {"หัก","Hak"}, // less
            {"หนึ่ง","Nueng"},  // first, one
            {"ยาว","Yao"},  // Long
            {"รุ่งเรือง","Rung Rueang"},  // prosperous
            {"สำโรง","Samrong"},  // Sterculia foetida L.
            {"นิคม","Nikhom"}  // plantation
        };

        public static Dictionary<String, String> NamePrefixRomanizations = new Dictionary<String, String>()
        {
            {"ปากคลอง", "Pak Khlong"},  // Mouth of Canal
            {"คลอง", "Khlong"},  // Canal
            {"น้ำ","Nam"},  // Water (river)
            {"ปากน้ำ","Pak Nam"},  // River mouth
            {"แม่","Mae"},  // Mother
            {"วัง","Wang"},  // Palace
            {"หนอง","Nong"},  // Swamp
            {"หัว","Hua"},  // Head
            {"ตลาด","Talat"},  // Market
            {"ห้วย", "Huai"},  // Creek
            {"ดอน","Don"},  // Hill
            {"แหลม","Laem"},  // Cape
            {"ท่า","Tha"},  // position
            {"โคก","Khok"},  // mound
            {"บาง","Bang"},  // village
            {"นา","Na"},  // field
            {"ลาด","Lat"},  // slope
            {"ไผ่","Phai"},  // Bamboo
            {"วัด","Wat"},  // Temple
            {"พระ","Phra"}, // holy
            {"ศรี","Si"},
            {"โนน","Non"},
            {"โพธิ์","Pho"},
            {"หอม","Hom"},  // good smell
            {"บึง","Bueng"},  // swamp
            {"หลัก","Lak"},  // pillar
            {"ปาก","Pak"},  // mouth
            {"เกาะ","Ko"},  // Island
            {"ป่า","Pa"},  // forest
            {"มาบ","Map"},  // marshland
            {"อ่าง","Ang"},  // Basin
            {"หาด","Hat"},  // Beach
            {"สวน","Suan"},  // Garden
            {"อ่าว","Ao"},  // Bay
            {"ถ้ำ","Tham"},  // cave
            {"ดอย","Doi"},  // mountain
            {"ซับ","Sap"},  // absorb
            {"สัน","San"},  // crest
            {"โป่ง","Pong"},   // large
            {"ต้น","Ton"},   // Beginning
            {"เชียง","Chiang"}, //
            {"เหล่า","Lao"},
            {"ชัย","Chai"},
            {"โพรง","Phrong"},
            {"บ้าน","Ban"},  // house
            {"หมู่บ้าน","Mu Ban"},  // village
            {"คุย","Khui"},  // talk
            {"ตรอก","Trok"},  // lane
            {"หมื่น","Muen"},   // ten thousand
            {"บุ่ง","Bung"},  // Marsh
            {"กุด","Kut"},
            {"บัว","Bua"},  // Lotus
            {"ควน","Khuan"},
            {"หลัง","Lang"}, // Behind
            {"ถนน","Thanon"}, // Street
            {"ดวง","Duang"}  // disc
        };

        public static Dictionary<String, String> ChangwatMisspellings = new Dictionary<String, String>()
        {
            {"สุราษฏร์ธานี","สุราษฎร์ธานี"}
        };

        public static Dictionary<String, PersonTitle> PersonTitleStrings = new Dictionary<String, PersonTitle>()
        {
            {"นาย",PersonTitle.Mister},
            {"นาง",PersonTitle.Mistress},
            {"น.ส.",PersonTitle.Miss},
            {"พล.อ.",PersonTitle.General},
            {"พล.ท.",PersonTitle.LieutenantGeneral},
            {"พล.ต.",PersonTitle.MajorGeneral},
            {"พลตรี",PersonTitle.MajorGeneral},
            {"พ.อ.",PersonTitle.Colonel},
            {"พ.ท.",PersonTitle.LieutenantColonel},
            {"พ.ต.",PersonTitle.Major},
            {"ร.อ.",PersonTitle.Captain},
            {"ร้อยเอก",PersonTitle.Captain},
            {"ร.ท.",PersonTitle.FirstLieutenant},
            {"ร.ต.",PersonTitle.SecondLieutenant},
            {"ร้อยตรี",PersonTitle.SecondLieutenant},
            {"ว่าที่ ร.ต.",PersonTitle.ActingSecondLieutenant},
            {"ว่าที่ร.ต.",PersonTitle.ActingSecondLieutenant},
            {"ว่าที่ร้อยตรี",PersonTitle.ActingSecondLieutenant},
            {"เรือตรี",PersonTitle.SubLieutenant}

            // พล.ต.อ. Police General
            // พล.ต.ท. Police Lieutenant General
            // พล.ต.ต. = พลตำรวจตรี Police Major General
            // พ.ต.อ. Police Colonel
            // พ.ต.ท. Police Lieutenant Colonel
            // พ.ต.ต. Police Major
            // ร.ต.อ. Police Captain
            // ร.ต.ท. Police Lieutenant
            // ร.ต.ต. Police Sub-Lieutenant
            // นายกองเอก ?
            // อำมาตย์โท
            // อำมาตย์เอก
            // เรืออากาศตรี
        };

        /// <summary>
        /// Copies to content of one stream into another.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public static void StreamCopy(Stream input, Stream output)
        {
            byte[] buffer = new byte[2048];
            int read = 0;

            do
            {
                read = iInput.Read(buffer, 0, buffer.Length);
                uutput.Write(buffer, 0, read);
            } while ( read > 0 );
        }

        /// <summary>
        /// Reads a stream into a string.
        /// </summary>
        /// <param name="stream">Input stream.</param>
        /// <returns>Content of stream as string.</returns>
        public static String StreamToString(Stream stream)
        {
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Converts a datetime into the javascript datetime value, which is the milliseconds since January 1 1970.
        /// </summary>
        /// <param name="value">Date and time to be converted.</param>
        /// <returns>Javascript datetime value.</returns>
        internal static Int64 GetDateJavaScript(DateTime value)
        {
            TimeSpan lDifference = value.ToUniversalTime() - new DateTime(1970, 1, 1);
            Int64 retval = Convert.ToInt64(lDifference.TotalMilliseconds);
            return retval;
        }

        /// <summary>
        /// Removes all non-numerical characters from the string.
        /// </summary>
        /// <param name="value">String to be filtered.</param>
        /// <returns>Filtered string.</returns>
        internal static String OnlyNumbers(String value)
        {
            String retval = String.Empty;
            foreach ( Char c in value )
            {
                if ( (c >= '0') && (c <= '9') )
                {
                    retval = retval + c;
                }
            }
            return retval;
        }

        /// <summary>
        /// Gets the geocode from the province name.
        /// </summary>
        /// <param name="changwatName">Name of the province in Thai.</param>
        /// <returns>The geocode of the province; zero if no province was found.</returns>
        internal static Int32 GetGeocode(String changwatName)
        {
            XElement geocodeXml = XElement.Load(BasicGeocodeFileName());
            Int32 provinceId = 0;
            var query = from c in geocodeXml.Descendants(TambonHelper.TambonNameSpace + "entity")
                        where (String)c.Attribute("name") == changwatName
                        select (Int32)c.Attribute("geocode");

            foreach ( Int32 entry in query )
            {
                provinceId = entry;
            }
            return provinceId;
        }

        /// <summary>
        /// Gets the geocode of a entity by its Thai name, administrative type and the province.
        /// </summary>
        /// <param name="changwatName">Name of the province in Thai.</param>
        /// <param name="subdivisionName">Thai name of administrative entity to find.</param>
        /// <param name="subdivisionType">Type of administrative entity to find.</param>
        /// <returns>Code of the entity found; zero if none was found.</returns>
        internal static Int32 GetGeocode(String changwatName, String subdivisionName, EntityType subdivisionType)
        {
            Int32 provinceId = GetGeocode(changwatName);
            Int32 geocode = 0;
            if ( provinceId != 0 )
            {
                String searchName = subdivisionName;
                if ( searchName.Contains(" ") )
                {
                    searchName = searchName.Substring(0, searchName.IndexOf(" "));
                }
                XElement changwatXml = XElement.Load(GeocodeSourceFile(provinceId));
                var subdivisionQuery = from c in changwatXml.Descendants(TambonHelper.TambonNameSpace + "entity")
                                       where (
                                         ((String)c.Attribute("name") == searchName) &&
                                         EntityTypeHelper.IsCompatibleEntityType(
                                           (EntityType)Enum.Parse(typeof(EntityType), (String)c.Attribute("type"))
                                           , subdivisionType))
                                       select (Int32)c.Attribute("geocode");

                geocode = subdivisionQuery.FirstOrDefault();
            }
            return geocode;
        }

        internal static String _BaseXMLDirectory = Path.GetDirectoryName(Application.ExecutablePath);

        public static String BaseXMLDirectory
        {
            get
            {
                return _BaseXMLDirectory;
            }
            set
            {
                _BaseXMLDirectory = value;
            }
        }

        internal static String GeocodeXmlSourceDir()
        {
            String retval = BaseXMLDirectory + "\\geocode\\";
            return retval;
        }

        static private String GeocodeSourceFile(Int32 provinceGeocode)
        {
            String filename = GeocodeXmlSourceDir() + "geocode" + provinceGeocode.ToString("D2") + ".XML";
            return filename;
        }

        static private Dictionary<Int32, PopulationDataEntry> _GeocodeCache = new Dictionary<Int32, PopulationDataEntry>();

        /// <summary>
        /// Returns the tree of administrative subdivisions for a given province.
        /// </summary>
        /// <param name="provinceCode">TIS1099 code of the province.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <cref>provinceCode</cref> does not refer to a valid province.</exception>
        /// <returns>Tree of subdivisions.</returns>
        /// <remarks>Internally caches a clone of the returned value, to load the file from disc only once.</remarks>
        static public PopulationData GetGeocodeList(Int32 provinceCode)
        {
            PopulationData result = null;
            if ( !ProvinceGeocodes.Any(entry => entry.Geocode == provinceCode) )
            {
                throw new ArgumentOutOfRangeException("provinceCode");
            }
            if ( _GeocodeCache.Keys.Contains(provinceCode) )
            {
                result = new PopulationData((PopulationDataEntry)(_GeocodeCache[provinceCode].Clone()));
            }
            else
            {
                String fileName = TambonHelper.GeocodeSourceFile(provinceCode);
                if ( File.Exists(fileName) )
                {
                    result = PopulationData.Load(fileName);
                    _GeocodeCache.Add(provinceCode, (PopulationDataEntry)(result.Data.Clone()));
                }
            }
            return result;
        }

        static public String BasicGeocodeFileName()
        {
            String lFilename = BaseXMLDirectory + "\\geocode.xml";
            return lFilename;
        }

        static public String BasicRegionFileName()
        {
            String lFilename = BaseXMLDirectory + "\\regions.xml";
            return lFilename;
        }

        static public void LoadGeocodeList()
        {
            if ( !ProvinceGeocodes.Any() )
            {
                XElement lGeocodeXML = XElement.Load(BasicGeocodeFileName());

                var lQuery = from c in lGeocodeXML.Descendants(TambonHelper.TambonNameSpace + "entity")
                             orderby (string)c.Attribute("english")
                             select new PopulationDataEntry
                             {
                                 Name = (string)c.Attribute("name"),
                                 English = (string)c.Attribute("english"),
                                 Type = (EntityType)Enum.Parse(typeof(EntityType), (string)c.Attribute("type")),
                                 Geocode = (Int32)c.Attribute("geocode")
                             };

                ProvinceGeocodes.Clear();
                ProvinceGeocodes.AddRange(lQuery);
            }
        }

        static public List<String> RegionSchemes()
        {
            XElement lRegionsXML = XElement.Load(BasicRegionFileName());

            var lQuery = from c in lRegionsXML.Descendants(TambonHelper.TambonNameSpace + "regions")
                         orderby (string)c.Attribute("english")
                         select (String)c.Attribute("english");
            List<String> lResult = new List<String>();
            foreach ( var lEntry in lQuery )
            {
                lResult.Add(lEntry);
            }
            return lResult;
        }

        static public List<PopulationDataEntry> GetRegionBySchemeName(String iSchemeName)
        {
            XElement lRegionsXML = XElement.Load(BasicRegionFileName());
            List<PopulationDataEntry> lResult = new List<PopulationDataEntry>();

            var lQuery = from c in lRegionsXML.Descendants(TambonHelper.TambonNameSpace + "regions")
                         where (string)c.Attribute("english") == iSchemeName
                         select c.Elements();
            foreach ( var lEntry in lQuery )
            {
                foreach ( XElement lNode in lEntry )
                {
                    PopulationDataEntry lData = PopulationDataEntry.Load(XNodeToXmlNode(lNode));
                    lResult.Add(lData);
                }
            }
            return lResult;
        }

        public static Dictionary<Int32, PopulationDataEntry> PopulationDataByYear = new Dictionary<Int32, PopulationDataEntry>();

        public static PopulationDataEntry GetCountryPopulationData(Int32 year)
        {
            PopulationDataEntry country = null;
            if ( TambonHelper.PopulationDataByYear.Keys.Contains(year) )
            {
                country = TambonHelper.PopulationDataByYear[year];
            }
            else
            {
                PopulationData downloader = new PopulationData(year, 0);
                downloader.Process();
                downloader.ReOrderThesaban();
                country = downloader.Data;
                TambonHelper.PopulationDataByYear[year] = country;
            }
            return country;
        }
    }
}