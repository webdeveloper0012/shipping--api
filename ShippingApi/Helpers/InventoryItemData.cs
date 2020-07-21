using System;
using System.Collections;
using System.Data;
using System.Xml.Serialization;

namespace ShippingApi.Helpers
{
    public class InventoryItemData : BaseData
    {
        public const string ITEM_TABLE = "InventoryItems";
        public const string ITEMCROSSREF_TABLE = "InventoryItem_CrossReference";

        public const string UPCCODE_FIELD = "upccode";

        public const string ORDERQTY_FIELD = "orderqty";
        public const string ITEM_FIELD = "Item";
        public const string ITMDESC_FIELD = "Itmdesc";
        public const string PLINID_FIELD = "Plinid";
        public const string TAXCODE_FIELD = "Taxcode";
        public const string PRICE_FIELD = "Price";
        public const string MSRP_FIELD = "Msrp";
        public const string ITMMEMO_FIELD = "Itmmemo";
        public const string WEIGHT_FIELD = "Weight";

        //DJOH MOD FOR SHIPRATE PROGRAM
        public const string VOLUME_FIELD = "Volume";

        public const string ITMDESC2_FIELD = "ItmDesc2";
        public const string ITMDESC3_FIELD = "ItmDesc3";
        public const string ITMDESC4_FIELD = "ItmDesc4";
        public const string ITMDESC5_FIELD = "ItmDesc5";
        public const string ITMDESC6_FIELD = "ItmDesc6";
        public const string ITMDESC7_FIELD = "ItmDesc7";
        public const string MFGR_FIELD = "Mfgr";
        public const string MFGNO_FIELD = "Mfgno";
        public const string EIMAGESM_FIELD = "EImageSM";
        public const string EIMAGEMD_FIELD = "EImageMD";
        public const string EIMAGELG_FIELD = "EImageLG";
        public const string EIMAGEXL_FIELD = "EImageXL";
        public const string ESPECIAL_FIELD = "ESpecial";
        public const string EFEATURED_FIELD = "EFeatured";
        public const string ESELLABLE_FIELD = "ESellable";
        public const string WEBPRICE_FIELD = "WebPrice";
        public const string WARRANTY_FIELD = "Warranty";
        public const string CONDITION_FIELD = "Condition";
        public const string ETEMPLATE_FIELD = "ETemplate";
        public const string ELINK_FIELD = "ELink";
        public const string ENOSHIPCHG_FIELD = "ENoShipChg";
        public const string PAGINGCONTROL_FIELD = "PagingControl";
        public const string ESTYLE_FIELD = "estyle";
        public const string INACTIVE_FIELD = "inactive";
        public const string PHANTOM_FIELD = "phantom";
        public const string MANUFACTURER_FIELD = "mfg";
        public const string MANUFACTURERNO_FIELD = "mfgno";
        public const string WEBTYPE_FIELD = "webtype";
        public const string ALTITM1_FIELD = "altitm1";
        public const string ALTITM2_FIELD = "altitm2";
        public const string ALTITM1TYPE_FIELD = "altitm1typ";
        public const string ALTITM2TYPE_FIELD = "altitm2typ";
        public const string CROSSREFMFG_FIELD = "compmfg";
        public const string CROSSREFMFG_PN_FIELD = "compmfgpn";
        public const string COMPMFG_FLAG_FIELD = "c_m";
        public const string CROSSREFACTIVE_FIELD = "active";
        public const string CROSSREFACTIVE_ISACTIVEFLAG = "A";

        public const string DISCOUNT_FIELD = "discount";

        public const string ISKIT_FIELD = "iskit";

        public const string SUNSMID_FIELD = "sunmsid";
        public const string STKUMID_FIELD = "stkumid";
        public const string ITEMCATEGORYMAPPING_TABLE = "InventoryItems_ProductLines";
        public const string RECURRING_FIELD = "erecurring";
        public const string ESTYLE1_FIELD = "estyle1";
        public const string ESTYLE2_FIELD = "estyle2";

        public const string ELENGTH_FIELD = "elength";
        public const string EWIDTH_FIELD = "ewidth";
        public const string EHEIGHT_FIELD = "eheight";

        public const string NOSHIPCALC_FIELD = "noshipcalc";
        public const string INVENTORYATLOCATION_TABLE = "InventoryAtLocation";
        public const string SUPLEAD_FIELD = "suplead";
        public const string LONHAND_FIELD = "lonhand";
        public const string SOALLOCATED_FIELD = "lsoaloc";
        public const string WOALLOCATED_FIELD = "lwoaloc";

        public const string SOLOPACK_FIELD = "solopack";
        public const string AVGCOST_FIELD = "avgcost";

        public const string ITEMCLASS_FIELD = "itmclss";
        public const string LISTORDER_FIELD = "listorder";

        //The SearchRank column is NOT in the DB. It is used to hold a calculated value after an item is found in a search operation
        public const string SEARCHRANK_FIELD = "SearchRank";

        public const string RECOMMENDEDITEM_TABLE = "RecommendedItem";
        public const string RECOMMENDEDID_FIELD = "RecommendedId";
        public const string RECOMMENDEDSEQNO_FIELD = "Seqno";
        public const string RECOMMENDEDPARENTITEM_TEMP_FIELD = "ParentItemId";

        public const string ORDERINGINSTRUCTION_ID = "OrderingInstructionId";

        public const string ITEMATLOCATION_TABLE = "InventoryAtLocation";


        public InventoryItemData() : base()
        {
        }

        public InventoryItemData(DataRow pRow)
        {
            if (pRow == null)
            {
                return;
            }

            ItemId = DataConverter.ColumnToString(pRow, ITEM_FIELD, true);
            Description = DataConverter.ColumnToString(pRow, ITMDESC_FIELD, true);
            Description2 = DataConverter.ColumnToString(pRow, ITMDESC2_FIELD, true);
            Description3 = DataConverter.ColumnToString(pRow, ITMDESC3_FIELD, true);
            Description4 = DataConverter.ColumnToString(pRow, ITMDESC4_FIELD, true);
            Description5 = DataConverter.ColumnToString(pRow, ITMDESC5_FIELD, true);
            Description6 = DataConverter.ColumnToString(pRow, ITMDESC6_FIELD, true);
            Description7 = DataConverter.ColumnToString(pRow, ITMDESC7_FIELD, true);

            WebPrice = DataConverter.ColumnToDecimal(pRow, WEBPRICE_FIELD);
            ESellable = DataConverter.ColumnToBoolean(pRow, ESELLABLE_FIELD);
            ESpecial = DataConverter.ColumnToBoolean(pRow, ESPECIAL_FIELD);
            EFeatured = DataConverter.ColumnToBoolean(pRow, EFEATURED_FIELD);
            ENoShipCharge = DataConverter.ColumnToBoolean(pRow, ENOSHIPCHG_FIELD);
            ETemplate = DataConverter.ColumnToString(pRow, ETEMPLATE_FIELD, true);
            ELink = DataConverter.ColumnToString(pRow, ELINK_FIELD, true);
            ImageSm = DataConverter.ColumnToString(pRow, EIMAGESM_FIELD, true);
            ImageMd = DataConverter.ColumnToString(pRow, EIMAGEMD_FIELD, true);
            ImageLg = DataConverter.ColumnToString(pRow, EIMAGELG_FIELD, true);
            ImageXL = DataConverter.ColumnToString(pRow, EIMAGEXL_FIELD, true);
            Price = DataConverter.ColumnToDecimal(pRow, PRICE_FIELD);
            AvgCost = DataConverter.ColumnToDecimal(pRow, AVGCOST_FIELD);
            TaxCode = DataConverter.ColumnToString(pRow, TAXCODE_FIELD, true);

            //DJOH
            Weight = DataConverter.ColumnToDecimal(pRow, WEIGHT_FIELD);
            Volume = DataConverter.ColumnToDecimal(pRow, VOLUME_FIELD);

            Memo = DataConverter.ColumnToString(pRow, ITMMEMO_FIELD, true);
            Msrp = DataConverter.ColumnToDecimal(pRow, MSRP_FIELD);
            EStyle = DataConverter.ColumnToString(pRow, ESTYLE_FIELD, true);
            EStyle1 = DataConverter.ColumnToString(pRow, ESTYLE1_FIELD, true);
            EStyle2 = DataConverter.ColumnToString(pRow, ESTYLE2_FIELD, true);
            ELength = DataConverter.ColumnToDecimal(pRow, ELENGTH_FIELD);
            EWidth = DataConverter.ColumnToDecimal(pRow, EWIDTH_FIELD);
            EHeight = DataConverter.ColumnToDecimal(pRow, EHEIGHT_FIELD);
            Recurring = DataConverter.ColumnToBoolean(pRow, RECURRING_FIELD);
            NoShipCalc = DataConverter.ColumnToBoolean(pRow, NOSHIPCALC_FIELD);
            OnHand = DataConverter.ColumnToDecimal(pRow, LONHAND_FIELD);
            LeadTime = DataConverter.ColumnToDecimal(pRow, SUPLEAD_FIELD);
            SoloPack = DataConverter.ColumnToBoolean(pRow, SOLOPACK_FIELD);
            OrderingInstructionId = DataConverter.ColumnToInt(pRow, ORDERINGINSTRUCTION_ID);
            ItemClass = DataConverter.ColumnToString(pRow, ITEMCLASS_FIELD, true);
            Phantom = DataConverter.ColumnToBoolean(pRow, PHANTOM_FIELD);
            ListOrder = DataConverter.ColumnToInt(pRow, LISTORDER_FIELD);
            stkumid = DataConverter.ColumnToString(pRow, STKUMID_FIELD, true);
            sunmsid = DataConverter.ColumnToString(pRow, SUNSMID_FIELD, true);
            OnHand = DataConverter.ColumnToDecimal(pRow, LONHAND_FIELD);
            SOAllocated = DataConverter.ColumnToDecimal(pRow, SOALLOCATED_FIELD);
            WOAllocated = DataConverter.ColumnToDecimal(pRow, WOALLOCATED_FIELD);
            Manufacturer = DataConverter.ColumnToString(pRow, MANUFACTURER_FIELD, string.Empty, true);
            ManufacturerNo = DataConverter.ColumnToString(pRow, MANUFACTURERNO_FIELD, string.Empty, true);
            WebType = DataConverter.ColumnToString(pRow, WEBTYPE_FIELD, string.Empty, true);
            AltItem1 = DataConverter.ColumnToString(pRow, ALTITM1_FIELD, string.Empty, true);
            AltItem2 = DataConverter.ColumnToString(pRow, ALTITM2_FIELD, string.Empty, true);
            AltItem1Type = DataConverter.ColumnToString(pRow, ALTITM1TYPE_FIELD, string.Empty, true).ToUpper();
            AltItem2Type = DataConverter.ColumnToString(pRow, ALTITM2TYPE_FIELD, string.Empty, true).ToUpper();
            UpcCode = DataConverter.ColumnToString(pRow, UPCCODE_FIELD, true);

            SearchRank = DataConverter.ColumnToInt(pRow, SEARCHRANK_FIELD, 0);
            SearchSource = DataConverter.ColumnToString(pRow, "src", true);

            SumRating = DataConverter.ColumnToInt(pRow, "SumRating");
            CountRating = DataConverter.ColumnToInt(pRow, "CountRating");

            Discount = DataConverter.ColumnToBoolean(pRow, DISCOUNT_FIELD);
        }

        public DataSet SourceDataSet = null;

        private DataTable _RecommendedItemTable = null;
        [XmlIgnore]
        public DataTable RecommendedItemTable
        {
            get
            {
                if (_RecommendedItemTable == null)
                {
                    _RecommendedItemTable = InventoryItemAccess.GetRecommendedItems(this.ItemId);
                }
                return _RecommendedItemTable;
            }
            set
            {
                _RecommendedItemTable = value;
            }
        }


        private Hashtable _children = new Hashtable();


        private bool _Discount;
        public bool Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }

        private string _ItemId;
        public string ItemId
        {
            get { return _ItemId; }
            set { _ItemId = value; }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private string _itmdesc2;
        public string Description2
        {
            get { return _itmdesc2; }
            set { _itmdesc2 = value; }
        }

        private string _itmdesc3;
        public string Description3
        {
            get { return _itmdesc3; }
            set { _itmdesc3 = value; }
        }

        private string _itmdesc4;
        public string Description4
        {
            get { return _itmdesc4; }
            set { _itmdesc4 = value; }
        }

        private string _itmdesc5;
        public string Description5
        {
            get { return _itmdesc5; }
            set { _itmdesc5 = value; }
        }

        private string _itmdesc6;
        public string Description6
        {
            get { return _itmdesc6; }
            set { _itmdesc6 = value; }
        }

        private string _itmdesc7;
        public string Description7
        {
            get { return _itmdesc7; }
            set { _itmdesc7 = value; }
        }

        private decimal _webprice;
        public Decimal WebPrice
        {
            get { return _webprice; }
            set { _webprice = value; }
        }

        private bool _esellable;
        public bool ESellable
        {
            get { return _esellable; }
            set { _esellable = value; }
        }

        private bool _especial;
        public bool ESpecial
        {
            get { return _especial; }
            set { _especial = value; }
        }

        private bool _efeatured;
        public bool EFeatured
        {
            get { return _efeatured; }
            set { _efeatured = value; }
        }

        private bool _enoshipchg;
        public bool ENoShipCharge
        {
            get { return _enoshipchg; }
            set { _enoshipchg = value; }
        }

        private string _etemplate;
        public string ETemplate
        {
            get { return _etemplate; }
            set { _etemplate = value; }
        }

        private string _elink;
        public string ELink
        {
            get { return _elink; }
            set { _elink = value; }
        }

        private string _stkumid;
        public string stkumid
        {
            get { return _stkumid; }
            set { _stkumid = value; }
        }

        private string _sunmsid;
        public string sunmsid
        {
            get { return _sunmsid; }
            set { _sunmsid = value; }
        }

        private string _eimagesm;
        public string ImageSm
        {
            get { return _eimagesm; }
            set { _eimagesm = value; }
        }

        private string _eimagemd;
        public string ImageMd
        {
            get { return _eimagemd; }
            set { _eimagemd = value; }
        }

        private string _eimagelg;
        public string ImageLg
        {
            get { return _eimagelg; }
            set { _eimagelg = value; }
        }

        private string _eimagexl;
        public string ImageXL
        {
            get { return _eimagexl; }
            set { _eimagexl = value; }
        }

        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        private string _taxcode;
        public string TaxCode
        {
            get { return _taxcode; }
            set { _taxcode = value; }
        }

        private decimal _weight;
        public decimal Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        private decimal _volume;
        public decimal Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        private string _itmmemo;
        public string Memo
        {
            get { return _itmmemo; }
            set { _itmmemo = value; }
        }

        private bool _drpshp;
        public bool DropShip
        {
            get { return _drpshp; }
            set { _drpshp = value; }
        }

        private decimal _msrp;
        public decimal Msrp
        {
            get { return _msrp; }
            set { _msrp = value; }
        }

        private DateTime _adddate;
        public DateTime AddDate
        {
            get { return _adddate; }
            set { _adddate = value; }
        }

        private decimal _iyslqt;
        public decimal YSalesQty
        {
            get { return _iyslqt; }
            set { _iyslqt = value; }
        }

        private string _estyle;
        public string EStyle
        {
            get { return _estyle; }
            set { _estyle = value; }
        }

        private string _estyle1;
        public string EStyle1
        {
            get { return _estyle1; }
            set { _estyle1 = value; }
        }

        private string _estyle2;
        public string EStyle2
        {
            get { return _estyle2; }
            set { _estyle2 = value; }
        }

        private decimal _sumfact;
        public decimal sumfact
        {
            get { return _sumfact; }
            set { _sumfact = value; }
        }

        private decimal _pumfact;
        public decimal pumfact
        {
            get { return _pumfact; }
            set { _pumfact = value; }
        }

        private string _punmsid;
        public string punmsid
        {
            get { return _punmsid; }
            set { _punmsid = value; }
        }

        private decimal _avgcost;
        public decimal AvgCost
        {
            get { return _avgcost; }
            set { _avgcost = value; }
        }

        private int _listorder;
        public int ListOrder
        {
            get { return _listorder; }
            set { _listorder = value; }
        }

        private int _vieworder;
        public int ViewOrder
        {
            get { return _vieworder; }
            set { _vieworder = value; }
        }

        private bool _recurring;
        public bool Recurring
        {
            get { return _recurring; }
            set { _recurring = value; }
        }

        private decimal _elength;
        public decimal ELength
        {
            get { return _elength; }
            set { _elength = value; }
        }

        private decimal _ewidth;
        public decimal EWidth
        {
            get { return _ewidth; }
            set { _ewidth = value; }
        }

        private decimal _eheight;
        public decimal EHeight
        {
            get { return _eheight; }
            set { _eheight = value; }
        }

        private bool _noshipcalc;
        public bool NoShipCalc
        {
            get { return _noshipcalc; }
            set { _noshipcalc = value; }
        }

        private decimal _lonhand;
        public decimal OnHand
        {
            get { return _lonhand; }
            set { _lonhand = value; }
        }

        private decimal _SOAllocated;
        public decimal SOAllocated
        {
            get { return _SOAllocated; }
            set { _SOAllocated = value; }
        }

        private decimal _suplead;
        public decimal LeadTime
        {
            get { return _suplead; }
            set { _suplead = value; }
        }
        private decimal _WOAllocated;
        public decimal WOAllocated
        {
            get { return _WOAllocated; }
            set { _WOAllocated = value; }
        }

        private bool _solopack;
        public bool SoloPack
        {
            get { return _solopack; }
            set { _solopack = value; }
        }

        private int _orderinginstructionId;
        public int OrderingInstructionId
        {
            get { return _orderinginstructionId; }
            set { _orderinginstructionId = value; }
        }

        private string _itemclass;
        public string ItemClass
        {
            get { return _itemclass; }
            set { _itemclass = value; }
        }

        private bool _phantom;
        public bool Phantom
        {
            get { return _phantom; }
            set { _phantom = value; }
        }

        private bool _discontinu;
        public bool Discontinued
        {
            get { return _discontinu; }
            set { _discontinu = value; }
        }

        private string _webtype;
        public string WebType
        {
            get { return _webtype; }
            set { _webtype = value; }
        }

        private bool _iskit;
        public bool IsKit
        {
            get { return _iskit; }
            set { _iskit = value; }
        }

        private string _mfg;
        public string Manufacturer
        {
            get { return _mfg; }
            set { _mfg = value; }
        }

        private string _mfgno;
        public string ManufacturerNo
        {
            get { return _mfgno; }
            set { _mfgno = value; }
        }

        private string _altitm1;
        public string AltItem1
        {
            get { return _altitm1; }
            set { _altitm1 = value; }
        }

        private string _altitm2;
        public string AltItem2
        {
            get { return _altitm2; }
            set { _altitm2 = value; }
        }

        private string _altitm1type;
        public string AltItem1Type
        {
            get { return _altitm1type; }
            set { _altitm1type = value; }
        }

        private string _altitm2type;
        public string AltItem2Type
        {
            get { return _altitm2type; }
            set { _altitm2type = value; }
        }


        private int _searchRank;
        public int SearchRank
        {
            get { return _searchRank; }
            set { _searchRank = value; }
        }

        private string _searchSource;
        public string SearchSource
        {
            get { return _searchSource; }
            set { _searchSource = value; }
        }

        private string _UpcCode;
        public string UpcCode
        {
            get { return _UpcCode; }
            set { _UpcCode = value; }
        }


        private int _SumRating;
        public int SumRating
        {
            get { return _SumRating; }
            set { _SumRating = value; }
        }

        private int _CountRating;
        public int CountRating
        {
            get { return _CountRating; }
            set { _CountRating = value; }
        }

        [XmlIgnore]
        public Hashtable Children
        {
            get { return _children; }
            set { _children = value; }
        }

    }
}