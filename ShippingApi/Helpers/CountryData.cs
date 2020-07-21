using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ShippingApi.Helpers
{
    public class Country : CountryData
    {
        public const string CONNECTSHIP_CODE_UNITED_STATES = "UNITED_STATES";
        public const string CONNECTSHIP_CODE_US_VIRGIN_ISLANDS = "US_VIRGIN_ISLANDS";
        public const string CONNECTSHIP_CODE_PUERTO_RICO = "PUERTO_RICO";

        public Country(CountryData pCountryData)
        {
            this.Active = pCountryData.Active;
            this.AddrType = pCountryData.AddrType;
            this.Code2Char = pCountryData.Code2Char;
            this.Code3Char = pCountryData.Code3Char;
            this.Code3Digit = pCountryData.Code3Digit;
            this.CountryId = pCountryData.CountryId;
            this.CountryName = pCountryData.CountryName;
            this.DisplayOrder = pCountryData.DisplayOrder;
            this.ShipMethodCat = pCountryData.ShipMethodCat;
        }

        public static Country GetByCode2Char(string pCountryCode2Char)
        {
            CountryData country = CountryAccess.GetByCode2Char(pCountryCode2Char);
            return country != null ? new Country(country) : null;
        }

    }

    public class CountryData : BaseData
    {
        public const string UNITED_STATES = "United States";
        public const string CANADA = "Canada";
        public const string USA_CODE3CHAR = "USA";
        public const string CANADA_CODE3CHAR = "CAN";


        public const string COUNTRY_TABLE = "Country";
        public const string COUNTRY_COUNTRYID_DBCOL = "CountryId";
        public const string COUNTRY_COUNTRYNAME_DBCOL = "CountryName";
        public const string COUNTRY_CODE2CHAR_DBCOL = "Code2Char";
        public const string COUNTRY_CODE3CHAR_DBCOL = "Code3Char";
        public const string COUNTRY_CODE3DIGIT_DBCOL = "Code3Digit";
        public const string COUNTRY_ADDRESSTYPE_DBCOL = "AddrType";
        public const string COUNTRY_SHIPMETHODCATEGORY_DBCOL = "ShipMethodCat";
        public const string COUNTRY_DISPLAYORDER_DBCOL = "DisplayOrder";
        public const string COUNTRY_ACTIVE_DBCOL = "Active";
        public const string CONNECTSHIPCODE_FIELD = "ConnectShipCode";

        public CountryData()
        {
        }

        public CountryData(DataRow pRow)
        {
            _CountryId = DataConverter.ColumnToInt(pRow, COUNTRY_COUNTRYID_DBCOL);
            _CountryName = DataConverter.ColumnToString(pRow, COUNTRY_COUNTRYNAME_DBCOL, true);
            _Code2Char = DataConverter.ColumnToString(pRow, COUNTRY_CODE2CHAR_DBCOL, true);
            _Code3Char = DataConverter.ColumnToString(pRow, COUNTRY_CODE3CHAR_DBCOL, true);
            _Code3Digit = DataConverter.ColumnToString(pRow, COUNTRY_CODE3DIGIT_DBCOL, true);
            _AddrType = DataConverter.ColumnToString(pRow, COUNTRY_ADDRESSTYPE_DBCOL, true);
            _ShipMethodCat = DataConverter.ColumnToString(pRow, COUNTRY_SHIPMETHODCATEGORY_DBCOL, true);
            _DisplayOrder = DataConverter.ColumnToInt(pRow, COUNTRY_DISPLAYORDER_DBCOL);
            _Active = DataConverter.ColumnToBoolean(pRow, COUNTRY_ACTIVE_DBCOL);
            ConnectShipCode = DataConverter.ColumnToString(pRow, CONNECTSHIPCODE_FIELD, true);
        }

        public static CountryData[] CountriesFromTable(DataTable pTable)
        {
            List<CountryData> countryList = new List<CountryData>();
            foreach (DataRow row in pTable.Rows)
            {
                countryList.Add(new CountryData(row));
            }
            return countryList.ToArray();
        }


        private int _CountryId;
        public int CountryId
        {
            get { return _CountryId; }
            set { _CountryId = value; }
        }

        private string _CountryName;
        public string CountryName
        {
            get { return _CountryName; }
            set { _CountryName = value; }
        }

        private string _Code2Char;
        public string Code2Char
        {
            get { return _Code2Char; }
            set { _Code2Char = value; }
        }

        private string _Code3Char;
        public string Code3Char
        {
            get { return _Code3Char; }
            set { _Code3Char = value; }
        }

        private string _Code3Digit;
        public string Code3Digit
        {
            get { return _Code3Digit; }
            set { _Code3Digit = value; }
        }

        private string _AddrType;
        public string AddrType
        {
            get { return _AddrType; }
            set { _AddrType = value; }
        }

        private string _ShipMethodCat;
        public string ShipMethodCat
        {
            get { return _ShipMethodCat; }
            set { _ShipMethodCat = value; }
        }

        private int _DisplayOrder;
        public int DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }

        private bool _Active;
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }

        private string _ConnectShipCode;
        public string ConnectShipCode
        {
            get { return _ConnectShipCode; }
            set { _ConnectShipCode = value; }
        }

    }

    public class CountryAccess : BaseData
    {
        protected static CountryData GetSingle(DataTable tableCountry)
        {
            CountryData[] countries = CountryData.CountriesFromTable(tableCountry);

            if (countries != null)
            {
                if (countries.Length > 0)
                {
                    if (countries[0] != null)
                    {
                        return countries[0];
                    }
                }
            }

            return null;
        }

        public static CountryData GetByCode2Char(string pCountryCode2Char)
        {
            StringBuilder select = new StringBuilder();

            select.Append(SelectPart());
            select.Append(" FROM ")
                .Append(CountryData.COUNTRY_TABLE);
            select.Append(string.Format(" WHERE {0}='{1}'", CountryData.COUNTRY_CODE2CHAR_DBCOL, pCountryCode2Char));

            DataTable tableCountry = FillTable(select.ToString());

            return GetSingle(tableCountry);
        }

        public static string SelectPart()
        {
            StringBuilder select = new StringBuilder();
            select.Append("SELECT ")
                .Append(CountryData.COUNTRY_COUNTRYID_DBCOL).Append(",")
                .Append(CountryData.COUNTRY_COUNTRYNAME_DBCOL).Append(",")
                .Append(CountryData.COUNTRY_CODE2CHAR_DBCOL).Append(",")
                .Append(CountryData.COUNTRY_CODE3CHAR_DBCOL).Append(",")
                .Append(CountryData.COUNTRY_CODE3DIGIT_DBCOL).Append(",")
                .Append(CountryData.COUNTRY_ADDRESSTYPE_DBCOL).Append(",")
                .Append(CountryData.COUNTRY_SHIPMETHODCATEGORY_DBCOL).Append(",")
                .Append(CountryData.COUNTRY_DISPLAYORDER_DBCOL).Append(",")
                .Append(CountryData.COUNTRY_ACTIVE_DBCOL);

            return select.ToString();
        }
    }
}