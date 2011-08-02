// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code. Version 3.4.0.38967
//    <NameSpace>De.AHoerstemeier.Tambon</NameSpace><Collection>List</Collection><codeType>CSharp</codeType><EnableDataBinding>False</EnableDataBinding><EnableLazyLoading>False</EnableLazyLoading><TrackingChangesEnable>False</TrackingChangesEnable><GenTrackingClasses>False</GenTrackingClasses><HidePrivateFieldInIDE>False</HidePrivateFieldInIDE><EnableSummaryComment>True</EnableSummaryComment><VirtualProp>False</VirtualProp><IncludeSerializeMethod>False</IncludeSerializeMethod><UseBaseClass>False</UseBaseClass><GenBaseClass>False</GenBaseClass><GenerateCloneMethod>False</GenerateCloneMethod><GenerateDataContracts>True</GenerateDataContracts><CodeBaseTag>Net40</CodeBaseTag><SerializeMethodName>Serialize</SerializeMethodName><DeserializeMethodName>Deserialize</DeserializeMethodName><SaveToFileMethodName>SaveToFile</SaveToFileMethodName><LoadFromFileMethodName>LoadFromFile</LoadFromFileMethodName><GenerateXMLAttributes>True</GenerateXMLAttributes><EnableEncoding>False</EnableEncoding><AutomaticProperties>False</AutomaticProperties><GenerateShouldSerialize>False</GenerateShouldSerialize><DisableDebug>False</DisableDebug><PropNameSpecified>Default</PropNameSpecified><Encoder>UTF8</Encoder><CustomUsings></CustomUsings><ExcludeIncludedTypes>True</ExcludeIncludedTypes><EnableInitializeFields>True</EnableInitializeFields>
//  </auto-generated>
// ------------------------------------------------------------------------------
namespace De.AHoerstemeier.Tambon
{
    using System;
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System.Collections;
    using System.Xml.Schema;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.Runtime.Serialization;


    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://hoerstemeier.com/tambon/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://hoerstemeier.com/tambon/", IsNullable = false)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "boardmeetings", Namespace = "http://hoerstemeier.com/tambon/")]
    public partial class boardmeetings
    {

        private List<MeetingEntryContainer> yearField;

        /// <summary>
        /// boardmeetings class constructor
        /// </summary>
        public boardmeetings()
        {
            this.yearField = new List<MeetingEntryContainer>();
        }

        [System.Xml.Serialization.XmlElementAttribute("year", Order = 0)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public List<MeetingEntryContainer> year
        {
            get
            {
                return this.yearField;
            }
            set
            {
                this.yearField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://hoerstemeier.com/tambon/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://hoerstemeier.com/tambon/", IsNullable = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "MeetingEntryContainer", Namespace = "http://hoerstemeier.com/tambon/")]
    public partial class MeetingEntryContainer
    {

        private List<MeetingEntry> boardmeetingField;

        private List<MeetingEntryContainer> yearField;

        private string valueField;

        /// <summary>
        /// MeetingEntryContainer class constructor
        /// </summary>
        public MeetingEntryContainer()
        {
            this.yearField = new List<MeetingEntryContainer>();
            this.boardmeetingField = new List<MeetingEntry>();
        }

        [System.Xml.Serialization.XmlElementAttribute("boardmeeting", Order = 0)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public List<MeetingEntry> boardmeeting
        {
            get
            {
                return this.boardmeetingField;
            }
            set
            {
                this.boardmeetingField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("year", Order = 1)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public List<MeetingEntryContainer> year
        {
            get
            {
                return this.yearField;
            }
            set
            {
                this.yearField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://hoerstemeier.com/tambon/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://hoerstemeier.com/tambon/", IsNullable = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "MeetingEntry", Namespace = "http://hoerstemeier.com/tambon/")]
    public partial class MeetingEntry
    {

        private List<object> itemsField;

        private byte numberField;

        private BoardNumber boardField;

        private System.DateTime dateField;

        private string urlField;

        /// <summary>
        /// MeetingEntry class constructor
        /// </summary>
        public MeetingEntry()
        {
            this.itemsField = new List<object>();
        }

        [System.Xml.Serialization.XmlElementAttribute("abolish", typeof(AbolishOperation), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("areachange", typeof(AreaOperation), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("related", typeof(GazetteReference), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("rename", typeof(RenameOperation), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("renamewat", typeof(RenameWatOperation), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("status", typeof(StatusOperation), Order = 0)]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public List<object> Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte number
        {
            get
            {
                return this.numberField;
            }
            set
            {
                this.numberField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public BoardNumber board
        {
            get
            {
                return this.boardField;
            }
            set
            {
                this.boardField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime date
        {
            get
            {
                return this.dateField;
            }
            set
            {
                this.dateField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string url
        {
            get
            {
                return this.urlField;
            }
            set
            {
                this.urlField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://hoerstemeier.com/tambon/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://hoerstemeier.com/tambon/", IsNullable = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "AbolishOperation", Namespace = "http://hoerstemeier.com/tambon/")]
    public partial class AbolishOperation : BasicOperation
    {

        private EntityType typeField;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public EntityType type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    [System.Xml.Serialization.XmlIncludeAttribute(typeof(StatusOperation))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(AbolishOperation))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(AreaOperation))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RenameWatOperation))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RenameOperation))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://hoerstemeier.com/tambon/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://hoerstemeier.com/tambon/", IsNullable = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "BasicOperation", Namespace = "http://hoerstemeier.com/tambon/")]
    public partial class BasicOperation
    {

        private List<object> itemsField;

        private List<ItemsChoiceType4> itemsElementNameField;

        private uint geocodeField;

        private bool geocodeFieldSpecified;

        private string nameField;

        private string englishField;

        private uint tambonField;

        private bool tambonFieldSpecified;

        private System.DateTime effectiveField;

        private bool effectiveFieldSpecified;

        private string commentField;

        private string indexField;

        /// <summary>
        /// BasicOperation class constructor
        /// </summary>
        public BasicOperation()
        {
            this.itemsElementNameField = new List<ItemsChoiceType4>();
            this.itemsField = new List<object>();
        }

        [System.Xml.Serialization.XmlElementAttribute("abolish", typeof(GazetteAbolish), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("abolishpark", typeof(GazettePark), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("areachange", typeof(GazetteAreaChange), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("areachangepark", typeof(GazettePark), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("capital", typeof(GazetteCapital), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("constituency", typeof(GazetteOperation), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("correction", typeof(GazetteReference), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("create", typeof(GazetteCreate), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("createpark", typeof(GazettePark), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("interpellation", typeof(GazetteInterpellation), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("mention", typeof(GazetteOperation), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("reassign", typeof(GazetteReassign), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("related", typeof(GazetteReference), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("rename", typeof(GazetteRename), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("renumber", typeof(GazetteRenumber), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("status", typeof(GazetteStatusChange), Order = 0)]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public List<object> Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order = 1)]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public List<ItemsChoiceType4> ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }
            set
            {
                this.itemsElementNameField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public uint geocode
        {
            get
            {
                return this.geocodeField;
            }
            set
            {
                this.geocodeField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool geocodeSpecified
        {
            get
            {
                return this.geocodeFieldSpecified;
            }
            set
            {
                this.geocodeFieldSpecified = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string english
        {
            get
            {
                return this.englishField;
            }
            set
            {
                this.englishField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public uint tambon
        {
            get
            {
                return this.tambonField;
            }
            set
            {
                this.tambonField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool tambonSpecified
        {
            get
            {
                return this.tambonFieldSpecified;
            }
            set
            {
                this.tambonFieldSpecified = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime effective
        {
            get
            {
                return this.effectiveField;
            }
            set
            {
                this.effectiveField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool effectiveSpecified
        {
            get
            {
                return this.effectiveFieldSpecified;
            }
            set
            {
                this.effectiveFieldSpecified = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string comment
        {
            get
            {
                return this.commentField;
            }
            set
            {
                this.commentField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "nonNegativeInteger")]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string index
        {
            get
            {
                return this.indexField;
            }
            set
            {
                this.indexField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://hoerstemeier.com/tambon/", IncludeInSchema = false)]
    public enum ItemsChoiceType4
    {

        /// <remarks/>
        abolish,

        /// <remarks/>
        abolishpark,

        /// <remarks/>
        areachange,

        /// <remarks/>
        areachangepark,

        /// <remarks/>
        capital,

        /// <remarks/>
        constituency,

        /// <remarks/>
        correction,

        /// <remarks/>
        create,

        /// <remarks/>
        createpark,

        /// <remarks/>
        interpellation,

        /// <remarks/>
        mention,

        /// <remarks/>
        reassign,

        /// <remarks/>
        related,

        /// <remarks/>
        rename,

        /// <remarks/>
        renumber,

        /// <remarks/>
        status,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://hoerstemeier.com/tambon/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://hoerstemeier.com/tambon/", IsNullable = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "StatusOperation", Namespace = "http://hoerstemeier.com/tambon/")]
    public partial class StatusOperation : BasicOperation
    {

        private EntityType oldField;

        private EntityType newField;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public EntityType old
        {
            get
            {
                return this.oldField;
            }
            set
            {
                this.oldField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public EntityType @new
        {
            get
            {
                return this.newField;
            }
            set
            {
                this.newField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://hoerstemeier.com/tambon/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://hoerstemeier.com/tambon/", IsNullable = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "AreaOperation", Namespace = "http://hoerstemeier.com/tambon/")]
    public partial class AreaOperation : BasicOperation
    {

        private EntityType typeField;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public EntityType type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://hoerstemeier.com/tambon/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://hoerstemeier.com/tambon/", IsNullable = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "RenameWatOperation", Namespace = "http://hoerstemeier.com/tambon/")]
    public partial class RenameWatOperation : BasicOperation
    {

        private string oldnameField;

        private string oldenglishField;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string oldname
        {
            get
            {
                return this.oldnameField;
            }
            set
            {
                this.oldnameField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string oldenglish
        {
            get
            {
                return this.oldenglishField;
            }
            set
            {
                this.oldenglishField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://hoerstemeier.com/tambon/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://hoerstemeier.com/tambon/", IsNullable = true)]
    [System.Runtime.Serialization.DataContractAttribute(Name = "RenameOperation", Namespace = "http://hoerstemeier.com/tambon/")]
    public partial class RenameOperation : BasicOperation
    {

        private EntityType typeField;

        private string oldnameField;

        private string oldenglishField;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public EntityType type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string oldname
        {
            get
            {
                return this.oldnameField;
            }
            set
            {
                this.oldnameField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string oldenglish
        {
            get
            {
                return this.oldenglishField;
            }
            set
            {
                this.oldenglishField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://hoerstemeier.com/tambon/")]
    public enum BoardNumber
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1,2")]
        Item12,

        /// <remarks/>
        rename,
    }
}