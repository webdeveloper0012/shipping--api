using ShippingApi;
using System.Data;

namespace ShippingApi.DataStructure
{
    public class ShipRateConfigData : BaseData
    {

        public const string SHIPRATECONFIGDATA_RATECONFIGID_FIELD = "rateConfigId";
        public const string SHIPRATECONFIGDATA_RATECONFIGDESC_FIELD = "rateConfigDesc";
        public const string SHIPRATECONFIGDATA_RATEFORUSA_FIELD = "rateForUSA";
        public const string SHIPRATECONFIGDATA_RATEFORCANADA_FIELD = "rateForCanada";
        public const string SHIPRATECONFIGDATA_RATEFORINTNL_FIELD = "rateForInternational";

        public const string SHIPRATECONFIGDATA_UPSERROR_FIELD = "upsErrorXML";
        public const string SHIPRATECONFIGDATA_UPSACCESSXML_FIELD = "upsAccessXml";
        public const string SHIPRATECONFIGDATA_UPSRATINGXML_FIELD = "upsRatingXml";
        public const string SHIPRATECONFIGDATA_UPSURL_FIELD = "upsRatingUrl";
        public const string SHIPRATECONFIGDATA_UPSAVURL_FIELD = "upsAVUrl";

        public const string SHIPRATECONFIGDATA_RISGNDPCTMKUP_FIELD = "risopGroundPctMarkup";
        public const string SHIPRATECONFIGDATA_RISAIRPCTMKUP_FIELD = "risopAirPctMarkup";
        public const string SHIPRATECONFIGDATA_RENGNDPCTMKUP_FIELD = "rentalGroundPctMarkup";
        public const string SHIPRATECONFIGDATA_RENAIRPCTMKUP_FIELD = "rentalAirPctMarkup";
        public const string SHIPRATECONFIGDATA_SALEGNDPCTMKUP_FIELD = "saleGroundPctMarkup";
        public const string SHIPRATECONFIGDATA_SALEAIRPCTMKUP_FIELD = "saleAirPctMarkup";

        public const string SHIPRATECONFIGDATA_RISDISCOUNTPCT_FIELD = "risopDiscountPct";
        public const string SHIPRATECONFIGDATA_RENDISCOUNTPCT_FIELD = "rentalDiscountPct";
        public const string SHIPRATECONFIGDATA_SALEDISCOUNTPCT_FIELD = "saleDiscountPct";

        public ShipRateConfigData()
        {
        }

        private int _RateConfigId;
        public int RateConfigId
        {
            get { return _RateConfigId; }
            set { _RateConfigId = value; }
        }

        private string _RateConfigDesc;
        public string RateConfigDesc
        {
            get { return _RateConfigDesc; }
            set { _RateConfigDesc = value; }
        }

        private bool _RateForUSA;
        public bool RateForUSA
        {
            get { return _RateForUSA; }
            set { _RateForUSA = value; }
        }

        private bool _RateForCanada;
        public bool RateForCanada
        {
            get { return _RateForCanada; }
            set { _RateForCanada = value; }
        }

        private bool _RateForIntnl;
        public bool RateForIntnl
        {
            get { return _RateForIntnl; }
            set { _RateForIntnl = value; }
        }

        private string _UpsUrl;
        public string UpsUrl
        {
            get { return _UpsUrl; }
            set { _UpsUrl = value; }
        }

        private string _UpsAVUrl;
        public string UpsAVUrl
        {
            get { return _UpsAVUrl; }
            set { _UpsAVUrl = value; }
        }

        private decimal _RisopGroundPctMarkup;
        public decimal RisopGroundPctMarkup
        {
            get { return _RisopGroundPctMarkup; }
            set { _RisopGroundPctMarkup = value; }
        }

        private decimal _RisopAirPctMarkup;
        public decimal RisopAirPctMarkup
        {
            get { return _RisopAirPctMarkup; }
            set { _RisopAirPctMarkup = value; }
        }

        private decimal _RentalGroundPctMarkup;
        public decimal RentalGroundPctMarkup
        {
            get { return _RentalGroundPctMarkup; }
            set { _RentalGroundPctMarkup = value; }
        }

        private decimal _RentalAirPctMarkup;
        public decimal RentalAirPctMarkup
        {
            get { return _RentalAirPctMarkup; }
            set { _RentalAirPctMarkup = value; }
        }

        private decimal _SaleGroundPctMarkup;
        public decimal SaleGroundPctMarkup
        {
            get { return _SaleGroundPctMarkup; }
            set { _SaleGroundPctMarkup = value; }
        }

        private decimal _SaleAirPctMarkup;
        public decimal SaleAirPctMarkup
        {
            get { return _SaleAirPctMarkup; }
            set { _SaleAirPctMarkup = value; }
        }

        private decimal _SaleDiscountPct;
        public decimal SaleDiscountPct
        {
            get { return _SaleDiscountPct; }
            set { _SaleDiscountPct = value; }
        }

        private decimal _RisopDiscountPct;
        public decimal RisopDiscountPct
        {
            get { return _RisopDiscountPct; }
            set { _RisopDiscountPct = value; }
        }

        private decimal _RentalDiscountPct;
        public decimal RentalDiscountPct
        {
            get { return _RentalDiscountPct; }
            set { _RentalDiscountPct = value; }
        }
    }
}
