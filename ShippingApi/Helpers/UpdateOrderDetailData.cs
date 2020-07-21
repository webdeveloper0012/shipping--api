namespace ShippingApi.Helpers
{
    public class UpdateOrderDetailData : BaseData
    {
        public const string UPDATEORDERDETAIL_TABLE = "UpdateOrderDetail";

        public const string WPPSONO_FIELD = "wppsono";
        public const string SONO_FIELD = "sono";
        public const string ITEM_FIELD = "item";
        public const string DESCRIP_FIELD = "descrip";
        public const string CONFIGURATION_FIELD = "configuration";
        public const string DISCOUNT_FIELD = "disc";
        public const string TAXRATE_FIELD = "taxrate";
        public const string PRICE_FIELD = "price";
        public const string QTYORD_FIELD = "qtyord";
        public const string EXTPRICE_FIELD = "extprice";
        public const string LINENO_FIELD = "source_is_lineno";
        public const string LENGTH_FIELD = "elength";
        public const string WIDTH_FIELD = "ewidth";
        public const string HEIGHT_FIELD = "eheight";
        public const string WEIGHT_FIELD = "weight";
        public const string TAXCODE_FIELD = "taxcode";
        public const string ETEMPLATE_FIELD = "etemplate";
        public const string ENOSHIPCHG_FIELD = "enoshipchg";
        public const string NOSHIPCALC_FIELD = "noshipcalc";
        public const string REQUIREDDATE_FIELD = "rqdate";
        public const string PRICESOURCE_FIELD = "PriceMethod";
        public const string INTMEMO_FIELD = "intmemo";
        public const string CUSTMEMO_FIELD = "custmemo";
        public const string COST_FIELD = "cost";
        public const string KITFLAG_FIELD = "kitflag";
        public const string ISKIT_FIELD = "iskit";
        public const string KITQTY_BUILT_FIELD = "kitqty_built";
        public const string KITQTY_NOTBUILT_FIELD = "kitqty_notbuilt";
        public const string SOLOPACK_FIELD = "solopack";
        public const string LOCTID_FIELD = "loctid";
        public const string QTYSHP_FIELD = "qtyshp";
        public const string OQTYREQ_FIELD = "oqtyreq";
        public const string QTYONHAND_FIELD = "qtyonhand";
        public const string AQTS_FIELD = "aqts";
        public const string CONDITION_FIELD = "condition";
        public const string FINALQTY_FIELD = "finalqty";
        public const string QTYPROMISE1_FIELD = "qtypromise1";
        public const string PROMDATE1_FIELD = "promdate1";
        public const string PROMDATE2_FIELD = "promdate2";
        public const string PONO_FIELD = "pono";
        public const string WEB_OPTION_FIELD = "web_option";
        public const string DATE_SAVED_FIELD = "date_saved";
        public const string TIME_SAVED_FIELD = "time_saved";
        public const string QTYPROMISE2_FIELD = "qtypromise2";
        public const string FIRSTSHIPMENT_FIELD = "firstshipment";
        public const string COUPONID_FIELD = "CouponID";
        public const string COUPONSAVINGS_FIELD = "CouponSavings";
        public const string RETAIL_FIELD = "retail";
        public const string RECURRING_FIELD = "recurring";
        public const string ITEMNOTE_FIELD = "itemnote";

        public const string ITEMCLASS_FIELD = "itemclass";
        public const string DISPLAYPRICE_FIELD = "Retail";
        public const string DISPLAYPRICEEXT_FIELD = "RetailExt";

        public const string SHIPCHG_FIELD = "Ship_Chg";

        //djoh added detail fields for 
        public const string UOM_FIELD = "sunmsid";
        public const string FABRIC_FIELD = "fabricid";
        public const string CLASS_FIELD = "classid";
        public const string COLORID_FIELD = "colorid";
        public const string COLORDESC_FIELD = "colordesc";
        public const string STYLE_FIELD = "styleid";
        public const string DISCOUNTABLE_FIELD = "discountable";

        public const string COLOR_FIELD = "color";
        public const string PATTERN_FIELD = "pattern";
        public const string COLOR2_FIELD = "color2";
        public const string COLOR3_FIELD = "color3";
        public const string COLOR4_FIELD = "color4";

        private string _Color;
        public string Color
        {
            get { return _Color; }
            set { _Color = value; }
        }

        private string _ItemId;
        private int _OrderQuantity;

        public string ItemId
        {
            get { return _ItemId; }
            set { _ItemId = value; }
        }

        public int OrderQuantity
        {
            get { return _OrderQuantity; }
            set { _OrderQuantity = value; }
        }

    }
}