using ShippingApi.Helpers;

namespace ClientSite.controllers
{
    public class BaseCustomer : BaseData
    {
        public const string CUSTOMER_TABLE = "Customers";
        public const string CUSTOMERADDRESS_TABLE = "CustomerAddresses";
        public const string WEBCUSTOMER_TABLE = "UpdateCustomer";
        public const string WEBCUSTOMERADDRESS_TABLE = "UpdateCustomerAddress";
        public const string CUSTOMERTAXCERTIFICATE_TABLE = "CustomerTaxCertificates";

        public const string CUSTOMER_CUSTNO_FIELD = "Custno";
        public const string CUSTOMER_COMPANY_FIELD = "Company";
        public const string CUSTOMER_CONTACT_FIELD = "Contact";
        public const string CUSTOMER_ADDRESS1_FIELD = "Address1";
        public const string CUSTOMER_ADDRESS2_FIELD = "Address2";
        public const string CUSTOMER_CITY_FIELD = "City";
        public const string CUSTOMER_STATE_FIELD = "State";
        public const string CUSTOMER_ZIP_FIELD = "Zip";
        public const string CUSTOMER_COUNTRY_FIELD = "Country";
        public const string CUSTOMER_PHONE_FIELD = "Phone";
        public const string CUSTOMER_SALESMN_FIELD = "Salesmn";
        public const string CUSTOMER_PTERMS_FIELD = "Pterms";
        public const string CUSTOMER_INTENDPTERMS_FIELD = "intendedPterms";
        public const string CUSTOMER_DISC_FIELD = "Disc";
        public const string CUSTOMER_TAX_FIELD = "Tax";
        public const string CUSTOMER_TAXTBL_FIELD = "Taxtbl";
        public const string CUSTOMER_LIMIT_FIELD = "Limit";
        public const string CUSTOMER_WEBEMAIL_FIELD = "Webemail";
        public const string CUSTOMER_EMAIL_FIELD = "email";
        public const string CUSTOMER_PASSWORD_FIELD = "Passwd";
        public const string CUSTOMER_CCNUMBER_FIELD = "ccnumber";
        public const string CUSTOMER_CCNAME_FIELD = "ccname";
        public const string CUSTOMER_CCEXP_FIELD = "ccexp";
        public const string CUSTOMER_PRICECODE_FIELD = "Pricecode";
        public const string CUSTOMER_CUSTSTAT_FIELD = "custstat";
        public const string CUSTOMER_CREDSTAT_FIELD = "credstat";
        public const string CUSTOMER_FAXNO_FIELD = "Faxno";
        public const string CUSTOMER_WPPCSTNO_FIELD = "wppcstno";
        public const string CUSTOMER_BALANCE_FIELD = "balance";
        public const string CUSTOMER_ONORDER_FIELD = "onorder";
        public const string CUSTOMER_LCKDATE_FIELD = "lckdate";
        public const string CUSTOMER_LCKTIME_FIELD = "lcktime";
        public const string CUSTOMER_TERRITORY_FIELD = "terr";
        public const string CUSTOMER_GETPWANSWER_FIELD = "GetPwQuest";
        public const string CUSTOMER_GETPWQUESTION_FIELD = "GetPwAnswr";
        public const string CUSTOMER_BIRTHDATE_FIELD = "birthdate";
        public const string CUSTOMER_SOURCE_FIELD = "source";
        public const string CUSTOMER_TRANTYPE_FIELD = "custtrantype";
        public const string CUSTOMER_ADDUSER_FIELD = "adduser";
        public const string CUSTOMER_WPPCUSTTRANNO_FIELD = "wppcusttranno";
        public const string CUSTOMER_PICKEDUPBYACCOUNTING_FIELD = "pickedupbyaccounting";
        public const string CUSTOMER_PICKEDUPDATE_FIELD = "pickedupdate";
        public const string CUSTOMER_YTDSALES_FIELD = "ytdsls";
        public const string CUSTOMER_LDATE_FIELD = "ldate";
        public const string CUSTOMER_DASHBOARD_FIELD = "dashboard";
        public const string CUSTOMER_BUSLIC_FIELD = "bus_lic";
        public const string CUSTOMER_DEFFOB_FIELD = "deffob";
        public const string CUSTOMER_SHIPVIA_FIELD = "shipvia";
        public const string CUSTOMER_TYPE_FIELD = "type";
        public const string CUSTOMER_CODE_FIELD = "code";

        public const string PRICEDISPLAY_FIELD = "webdefdisp";
        public const string RETAILPRICEMARKUP_FIELD = "retmkup";
        public const string REGSHIPTO_FIELD = "regshipto";
        public const string NATIONALACCOUNTID_FIELD = "nlacno";
        public const string BRANCHDEF_FIELD = "brdef";
        public const string ORDREF_FIELD = "ordref";
        public const string MENUSACTIVE_FIELD = "menu_on";

        public const string CUSTOMERTAXCERT_CUSTNO_FIELD = "custno";
        public const string CUSTOMERTAXCERT_STATE_FIELD = "txcrtst";
        public const string CUSTOMERTAXCERT_NUMBER_FIELD = "txcrtno";
        public const string CUSTOMERTAXCERT_NAME_FIELD = "txcrtnm";
        public const string CUSTOMERTAXCERT_EFFDATE_FIELD = "effdate";
        public const string CUSTOMERTAXCERT_EXPDATE_FIELD = "expdate";


        public const string CUSTOMERADDRESS_CUSTNO_FIELD = "custno";
        public const string CUSTOMERADDRESS_CSHIPNO_FIELD = "cshipno";
        public const string CUSTOMERADDRESS_DEFASHIP_FIELD = "defaship";
        public const string CUSTOMERADDRESS_COMPANY_FIELD = "company";
        public const string CUSTOMERADDRESS_CONTACT_FIELD = "contact";
        public const string CUSTOMERADDRESS_TITLE_FIELD = "title";
        public const string CUSTOMERADDRESS_ADDRESS1_FIELD = "address1";
        public const string CUSTOMERADDRESS_ADDRESS2_FIELD = "address2";
        public const string CUSTOMERADDRESS_CITY_FIELD = "city";
        public const string CUSTOMERADDRESS_STATE_FIELD = "state";
        public const string CUSTOMERADDRESS_ZIP_FIELD = "zip";
        public const string CUSTOMERADDRESS_COUNTRY_FIELD = "country";
        public const string CUSTOMERADDRESS_PHONE_FIELD = "phone";
        public const string CUSTOMERADDRESS_SALESMN_FIELD = "salesmn";
        public const string CUSTOMERADDRESS_TAX_FIELD = "tax";
        public const string CUSTOMERADDRESS_SHIPVIA_FIELD = "shipvia";
        public const string CUSTOMERADDRESS_WPPCSHIPNO_FIELD = "wppcshipno";
        public const string CUSTOMERADDRESS_WPPCSTNO_FIELD = "wppcstno";
        public const string CUSTOMERADDRESS_WBCADRID_FIELD = "wbcadrId";
        public const string CUSTOMERADDRESS_LCKDATE_FIELD = "lckdate";
        public const string CUSTOMERADDRESS_LCKTIME_FIELD = "lcktime";
        public const string CUSTOMERADDRESS_DROPSHIP_FIELD = "dropship";

        public const string WEBCUSTOMERADDRESS_AUTOID_FIELD = "autoid";
        public const string WEBCUSTOMERADDRESS_CUSTNO_FIELD = "custno";
        public const string WEBCUSTOMERADDRESS_CSHIPNO_FIELD = "cshipno";
        public const string WEBCUSTOMERADDRESS_DEFASHIP_FIELD = "defaultaddress";
        public const string WEBCUSTOMERADDRESS_COMPANY_FIELD = "company";
        public const string WEBCUSTOMERADDRESS_CONTACT_FIELD = "contact";
        public const string WEBCUSTOMERADDRESS_ADDRESS1_FIELD = "address1";
        public const string WEBCUSTOMERADDRESS_ADDRESS2_FIELD = "address2";
        public const string WEBCUSTOMERADDRESS_CITY_FIELD = "city";
        public const string WEBCUSTOMERADDRESS_STATE_FIELD = "state";
        public const string WEBCUSTOMERADDRESS_ZIP_FIELD = "zip";
        public const string WEBCUSTOMERADDRESS_COUNTRY_FIELD = "country";
        public const string WEBCUSTOMERADDRESS_PHONE_FIELD = "phone";
        public const string WEBCUSTOMERADDRESS_TAX_FIELD = "tax";
        public const string WEBCUSTOMERADDRESS_SHIPVIA_FIELD = "shipvia";
        public const string WEBCUSTOMERADDRESS_WPPCSHIPNO_FIELD = "wppcshipno";
        public const string WEBCUSTOMERADDRESS_WPPCSTNO_FIELD = "wppcstno";
        public const string WEBCUSTOMERADDRESS_WBCADRID_FIELD = "autoid";
        public const string WEBCUSTOMERADDRESS_TRANTYPE_FIELD = "trantype";
        public const string WEBCUSTOMERADDRESS_ADDDATETIME_FIELD = "adddatetime";
        public const string WEBCUSTOMERADDRESS_DROPSHIP_FIELD = "dropship";

        public const string WEBCUSTOMER_TAXCERT_STATE_FIELD = "txcrtst";
        public const string WEBCUSTOMER_TAXCERT_NUMBER_FIELD = "txcrtno";
        public const string WEBCUSTOMER_TAXCERT_NAME_FIELD = "txcrtnm";
        public const string WEBCUSTOMER_TAXCERT_EFFDATE_FIELD = "effdate";
        public const string WEBCUSTOMER_TAXCERT_EXPDATE_FIELD = "expdate";

        public const string COLLECTSHIPPING_FIELD = "CollectShipping";
        public const string CUSTOMER_DISABLEWS_FIELD = "DisableWS";



    }
}