using ShippingApi;
using ShippingApi.DataStructure;
using System.Data;

namespace WPPDataModel.ShippingSystem.DataStructure
{
    public class ShippingBoxData : BaseData
    {
        public const string SHIPPINGBOXDATA_BOXNAME_FIELD = "boxname";
        public const string SHIPPINGBOXDATA_BOXDESC_FIELD = "boxdesc";
        public const string SHIPPINGBOXDATA_BOXTYPE_FIELD = "boxtype";
        public const string SHIPPINGBOXDATA_BOXLENGTH_FIELD = "boxlength";
        public const string SHIPPINGBOXDATA_BOXWIDTH_FIELD = "boxwidth";
        public const string SHIPPINGBOXDATA_BOXHEIGHT_FIELD = "boxheight";
        public const string SHIPPINGBOXDATA_BOXWEIGHT_FIELD = "boxweight";
        public const string SHIPPINGBOXDATA_BOXCOST_FIELD = "boxcost";
        public const string SHIPPINGBOXDATA_BOXENABLED_FIELD = "boxenabled";
        public const string SHIPPINGBOXDATA_BOXMAXWEIGHT_FIELD = "boxmaxweight";
        public const string SHIPPINGBOXDATA_BOXMAXVOLUME_FIELD = "boxmaxvolume";
        public ShippingBoxData()
        { }

        public ShippingBoxData(DataRow pRow)
        {
            if (pRow == null)
            {
                return;
            }

            BoxName = DataConverter.ColumnToString(pRow, SHIPPINGBOXDATA_BOXNAME_FIELD);
            BoxDesc = DataConverter.ColumnToString(pRow, SHIPPINGBOXDATA_BOXDESC_FIELD);
            BoxType = DataConverter.ColumnToString(pRow, SHIPPINGBOXDATA_BOXTYPE_FIELD);
            BoxLength = DataConverter.ColumnToDecimal(pRow, SHIPPINGBOXDATA_BOXLENGTH_FIELD);
            BoxWidth = DataConverter.ColumnToDecimal(pRow, SHIPPINGBOXDATA_BOXWIDTH_FIELD);
            BoxHeight = DataConverter.ColumnToDecimal(pRow, SHIPPINGBOXDATA_BOXHEIGHT_FIELD);
            BoxWeight = DataConverter.ColumnToDecimal(pRow, SHIPPINGBOXDATA_BOXWEIGHT_FIELD);
            BoxCost = DataConverter.ColumnToDecimal(pRow, SHIPPINGBOXDATA_BOXCOST_FIELD);
            BoxEnabled = DataConverter.ColumnToBoolean(pRow, SHIPPINGBOXDATA_BOXENABLED_FIELD);
            BoxMaxWeight = DataConverter.ColumnToDecimal(pRow, SHIPPINGBOXDATA_BOXMAXWEIGHT_FIELD);
            BoxMaxVolume = DataConverter.ColumnToDecimal(pRow, SHIPPINGBOXDATA_BOXMAXVOLUME_FIELD);

        }

        private string _BoxName;
        public string BoxName
        {
            get { return _BoxName; }
            set { _BoxName = value; }
        }


        private string _BoxDesc;
        public string BoxDesc
        {
            get { return _BoxDesc; }
            set { _BoxDesc = value; }
        }

        private string _BoxType;
        public string BoxType
        {
            get { return _BoxType; }
            set { _BoxType = value; }
        }

        private decimal _BoxLength;
        public decimal BoxLength
        {
            get { return _BoxLength; }
            set { _BoxLength = value; }
        }

        private decimal _BoxWidth;
        public decimal BoxWidth
        {
            get { return _BoxWidth; }
            set { _BoxWidth = value; }
        }

        private decimal _BoxHeight;
        public decimal BoxHeight
        {
            get { return _BoxHeight; }
            set { _BoxHeight = value; }
        }

        private decimal _BoxWeight;
        public decimal BoxWeight
        {
            get { return _BoxWeight; }
            set { _BoxWeight = value; }
        }

        private decimal _BoxCost;
        public decimal BoxCost
        {
            get { return _BoxCost; }
            set { _BoxCost = value; }
        }

        private bool _BoxEnabled;
        public bool BoxEnabled
        {
            get { return _BoxEnabled; }
            set { _BoxEnabled = value; }
        }

        private decimal _BoxMaxWeight;
        public decimal BoxMaxWeight
        {
            get { return _BoxMaxWeight; }
            set { _BoxMaxWeight = value; }
        }

        private decimal _BoxMaxVolume;
        public decimal BoxMaxVolume
        {
            get { return _BoxMaxVolume; }
            set { _BoxMaxVolume = value; }
        }
    }
}
