using System;
using System.Data;
using System.Runtime.Serialization;

[System.ComponentModel.DesignerCategory("Code")]
[Serializable]
public class OrderData : DataSet
{
    public const string CUSTOMADDRESSID = "TOSW_X";

    /// <value>The constant used for Order Detail table. </value>
    public const string ORDER_DETAIL_TABLE = "UpdateOrderDetail";

    public const string WPPSONO_FIELD = "wppsono";
    public const string SONO_FIELD = "sono";
    public const string ITEM_FIELD = "item";
    public const string DESCRIP_FIELD = "descrip";
    public const string DISCOUNT_FIELD = "disc";
    public const string TAXRATE_FIELD = "taxrate";
    public const string COST_FIELD = "cost";
    public const string PRICE_FIELD = "price";
    public const string QTYORD_FIELD = "qtyord";
    public const string QTYSHP_FIELD = "qtyshp";
    public const string EXTPRICE_FIELD = "extprice";
    public const string LINENO_FIELD = "source_is_lineno";
    public const string WEIGHT_FIELD = "weight";
    public const string TAXCODE_FIELD = "taxcode";
    public const string ETEMPLATE_FIELD = "etemplate";
    public const string ENOSHIPCHG_FIELD = "enoshipchg";
    public const string PRICESOURCE_FIELD = "PriceMethod";
    public const string INTMEMO_FIELD = "intmemo";
    public const string ITEMCLASS_FIELD = "itemclass";
    public const string LOCATIONID_FIELD = "loctid";
    public const string CUSTMEMO_FIELD = "custmemo";

    /// <value>The constant used for Order Master table. </value>
    public const string ORDER_MASTER_TABLE = "UpdateOrderMaster";

    public const string SODATE_FIELD = "sodate";
    public const string ORDATE_FIELD = "ordate";
    public const string ADDDATE_FIELD = "adddatetime";
    public const string SHIPVIA_FIELD = "shipvia";
    public const string SHIPACCT_FIELD = "shipacct";
    public const string CSHIPNO_FIELD = "cshipno";
    public const string PTERMS_FIELD = "pterms";
    public const string PDISC_FIELD = "pdisc";
    public const string PDAYS_FIELD = "pdays";
    public const string PNET_FIELD = "pnet";
    public const string DISC_FIELD = "disc";
    public const string ORDAMT_FIELD = "ordamt";
    public const string SHPAMT_FIELD = "shpamt";
    public const string PONUM_FIELD = "ponum";
    public const string SALESMN_FIELD = "salesmn";
    public const string SOSTAT_FIELD = "sostat";
    public const string TAX_FIELD = "tax";
    public const string TOSW_FIELD = "tosw";
    public const string COMMENT_FIELD = "comment";
    public const string CCNUMBER_FIELD = "ccnumber";
    public const string CCNAME_FIELD = "ccname";
    public const string CCEXP_FIELD = "ccexp";
    public const string CCAUTHCODE_FIELD = "ccauthcode";
    public const string CCAUTHMEMO_FIELD = "ccauthmemo";
    public const string CCTRANID_FIELD = "cctranid";
    public const string CCAUTHAMT_FIELD = "authamt";
    public const string KEEPCARD_FIELD = "keepcard";
    public const string CUSTSOURCE_FIELD = "custsource";
    public const string WEBEMAIL_FIELD = "webemail";
    public const string ORDER_PROMASTER_TABLE = "SOMAST";
    public const string WEBSONO_FIELD = "websono";
    public const string SOTYPE_FIELD = "sotype";
    public const string STATUSTEXT_FIELD = "statustext";
    public const string CCSECUCODE_FIELD = "ccsecucode";
    public const string FINALIZED_FIELD = "finalized";
    public const string SHIP_COMPANY_FIELD = "company";
    public const string SHIP_ADDRESS1_FIELD = "address1";
    public const string SHIP_ADDRESS2_FIELD = "address2";
    public const string SHIP_CITY_FIELD = "city";
    public const string SHIP_STATE_FIELD = "state";
    public const string SHIP_ZIP_FIELD = "zip";
    public const string SHIP_COUNTRY_FIELD = "country";
    public const string TAXTBL_FIELD = "taxtbl";
    public const string EMAILOPTIN_FIELD = "optin";
    // Added for Sage Vault
    public const string CCGUID_FIELD = "ccguid";
    public const string DEFACARD_FIELD = "defacard";
    public const string VAULTERROR_FIELD = "vaulterror";
    public const string CCTYPE_FIELD = "cctype";
    public const string CCDISPLAY_FIELD = "ccdisplay";
    public const string CCADD1_FIELD = "ccadd1";
    public const string CCADD2_FIELD = "ccadd2";
    public const string CCCITY_FIELD = "cccity";
    public const string CCSTATE_FIELD = "ccstate";
    public const string CCZIP_FIELD = "cczip";
    public const string CCCOUNTRY_FIELD = "cccountry";

    public const string QUOTEID_FIELD = "quoteid";



    public const string UOM_FIELD = "sunmsid";
    public const string KIT_FIELD = "kit";
    public const string MASTERITEM_FIELD = "masteritem";
    public const string ADDUSER_FIELD = "adduser";
    public const string ITEMNOTE_FIELD = "itemnote";
    public const string CONFIGURATION_FIELD = "configuration";
    public const string ADDTOMYADDRESSLIST_FIELD = "addtomyaddresslist";
    public const string SHIP_CONTACT_FIELD = "contact";
    public const string SHIP_PHONE_FIELD = "phone";

    public const string RECURRING_FIELD = "recurring";
    public const string NEXTBILL_FIELD = "nextbill";
    public const string LASTBILL_FIELD = "lastbill";
    public const string RECURRINGFREQUENCY_FIELD = "freq";
    public const string RECURRINGPERIOD_FIELD = "period";
    public const string RECURRINGSHIPVIA_FIELD = "recshipvia";
    public const string CSHIPNO2_FIELD = "cshipno2";
    public const string CSHIPEFF1_FIELD = "cshipeff1";
    public const string CSHIPEFF2_FIELD = "cshipeff2";

    public const string CC_AUTHEXP = "preexp"; // preauthorization expiration
    public const string CC_PREAUTHID = "preid"; // preauthorization id

    public const string NOSHIPCALC_FIELD = "noshipcalc";
    public const string REQUIREDDATE_FIELD = "rqdate";
    public const string WPPCUSTNO_FIELD = "wppcstno";

    public const string LENGTH_FIELD = "length";
    public const string WIDTH_FIELD = "width";
    public const string HEIGHT_FIELD = "height";
    public const string SOLOPACK_FIELD = "solopack";

    public const string NOTES_FIELD = "notes";
    public const string COLLECTSHIPPING_FIELD = "CollectShipping";
    public const string SHIPPINGACCOUNT_FIELD = "ShipAcct";

    public const string INTERNATIONALPHONE_FIELD = "intlphone";


    public const string ORDER_PRODETAIL_TABLE = "SOTRAN";

    /// <value>The constant used for Customer table. </value>
    public const string CUSTOMER_TABLE = "UpdateCustomer";

    /// <value>The constant used for primary key field in the Customer table. </value>
    public const string CUSTNO_FIELD = "Custno";

    /// <value>The constant used for the Shipping Address table. </value>
    public const string SHIPPING_ADDRESS_TABLE = "WBADDR";

    public const string ADDRESS_TYPE_FIELD = "adtype";
    public const string COMPANY_FIELD = "company";
    public const string ADDRESS1_FIELD = "address1";
    public const string ADDRESS2_FIELD = "address2";
    public const string ADDRESS3_FIELD = "address3";
    public const string CITY_FIELD = "city";
    public const string STATE_FIELD = "state";
    public const string COUNTRY_FIELD = "country";
    public const string ZIP_FIELD = "zip";


    public const string ORIGINALQTYREQUESTED_FIELD = "oqtyreq";
    public const string QTYONHAND_FIELD = "qtyonhand";
    public const string AQTS_FIELD = "aqts";
    public const string FINALQTY_FIELD = "finalqty";
    public const string PONO_FIELD = "pono";
    public const string QTYPROMISE1_FIELD = "qtypromise1";
    public const string QTYPROMISE2_FIELD = "qtypromise2";
    public const string PROMDATE1_FIELD = "promdate1";
    public const string PROMDATE2_FIELD = "promdate2";
    public const string CONDITION_FIELD = "condition";
    public const string WEBOPTION_FIELD = "web_option";
    public const string DATESAVED_FIELD = "date_saved";
    public const string DISPLAYPRICE_FIELD = "Retail";
    public const string DISPLAYPRICEEXT_FIELD = "RetailExt";

    public const string FIRSTSHIPMENT_FIELD = "firstshipment";
    public const string SHIPCOMPLETE_FIELD = "shipcmplt";


    public const string COUPONSAVINGS_FIELD = "CouponSavings";

    public const string DISPLAYSUBTOTAL_FIELD = "RetailSubtotal";

    public const string FOB_FIELD = "fob";

    public const string INTLPHONE_FIELD = "intlphone";

    public const string SHIPTOTYPE_FIELD = "shiptotype";


    /// <value>The constant used for row error when there is an 'Invalid Field' in one of the tables. </value>
    public const string INVALID_FIELD = "Invalid Field";

    /// <summary>
    ///     Constructor to support serialization.
    ///     <remarks>Constructor that supports serialization.</remarks> 
    ///     <param name="info">The SerializationInfo object to read from.</param>
    ///     <param name="context">Information on who is calling this method.</param>
    /// </summary>
    public OrderData(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
    /// <summary>
    ///     Constructor for OrderData.  
    ///     <remarks>Initialize a OrderData instance by building the table schema.</remarks> 
    /// </summary>
    public OrderData()
    {
        // Create the tables in the dataset
        BuildDataTables();
    }

    protected virtual void BuildDataTables()
    {
        DataTable table;
        DataColumn column;
        DataColumnCollection columns;

        // Create the order detail table
        table = new DataTable(ORDER_DETAIL_TABLE);
        columns = table.Columns;

        columns.Add(WPPSONO_FIELD, typeof(System.Int32));
        columns.Add(SONO_FIELD, typeof(System.String));
        columns.Add(ITEM_FIELD, typeof(System.String));
        columns.Add(DESCRIP_FIELD, typeof(System.String));
        columns.Add(DISCOUNT_FIELD, typeof(System.Decimal));
        columns.Add(TAXRATE_FIELD, typeof(System.Decimal));
        columns.Add(COST_FIELD, typeof(System.Decimal));
        columns.Add(PRICE_FIELD, typeof(System.Decimal));
        columns.Add(QTYORD_FIELD, typeof(System.Int32));
        columns.Add(QTYSHP_FIELD, typeof(System.Int32));
        columns.Add(EXTPRICE_FIELD, typeof(System.Decimal));
        columns.Add(LINENO_FIELD, typeof(System.Int32));
        columns.Add(WEIGHT_FIELD, typeof(System.Decimal));
        columns.Add(TAXCODE_FIELD, typeof(System.String));
        columns.Add(ETEMPLATE_FIELD, typeof(System.String));
        columns.Add(ENOSHIPCHG_FIELD, typeof(System.Int32));
        columns.Add(PRICESOURCE_FIELD, typeof(System.String));
        columns.Add(ITEMCLASS_FIELD, typeof(System.String));

        columns.Add(UOM_FIELD, typeof(System.String));
        columns.Add(ADDUSER_FIELD, typeof(System.String));
        columns.Add(ITEMNOTE_FIELD, typeof(System.String));
        columns.Add(CONFIGURATION_FIELD, typeof(System.String));
        columns.Add(KIT_FIELD, typeof(System.String));
        columns.Add(MASTERITEM_FIELD, typeof(System.String));
        columns.Add(RECURRING_FIELD, typeof(System.Boolean));
        columns.Add(NOSHIPCALC_FIELD, typeof(System.Boolean));
        columns.Add(REQUIREDDATE_FIELD, typeof(System.DateTime));
        columns.Add(SOLOPACK_FIELD, typeof(System.Boolean));
        columns.Add(LENGTH_FIELD, typeof(System.Int32));
        columns.Add(HEIGHT_FIELD, typeof(System.Int32));
        columns.Add(WIDTH_FIELD, typeof(System.Int32));

        columns.Add(INTMEMO_FIELD, typeof(System.String));

        columns.Add(ORIGINALQTYREQUESTED_FIELD, typeof(System.Decimal));
        columns.Add(QTYONHAND_FIELD, typeof(System.Decimal));
        columns.Add(AQTS_FIELD, typeof(System.Decimal));
        columns.Add(FINALQTY_FIELD, typeof(System.Decimal));
        columns.Add(PONO_FIELD, typeof(System.String));
        columns.Add(QTYPROMISE1_FIELD, typeof(System.Decimal));
        columns.Add(QTYPROMISE2_FIELD, typeof(System.Decimal));
        columns.Add(PROMDATE1_FIELD, typeof(System.String));
        columns.Add(PROMDATE2_FIELD, typeof(System.String));
        columns.Add(CONDITION_FIELD, typeof(System.Int32));
        columns.Add(WEBOPTION_FIELD, typeof(System.String));
        columns.Add(DATESAVED_FIELD, typeof(System.DateTime));
        columns.Add(FIRSTSHIPMENT_FIELD, typeof(System.Boolean));

        columns.Add(DISPLAYPRICE_FIELD, typeof(System.Decimal));
        columns.Add(DISPLAYPRICEEXT_FIELD, typeof(System.Decimal));
        columns.Add(CUSTMEMO_FIELD, typeof(System.String));


        this.Tables.Add(table);

        // Create the order master table
        table = new DataTable(ORDER_MASTER_TABLE);
        columns = table.Columns;

        columns.Add(SONO_FIELD, typeof(System.String));
        columns.Add(CUSTNO_FIELD, typeof(System.String));
        columns.Add(SODATE_FIELD, typeof(System.String));
        columns.Add(ORDATE_FIELD, typeof(System.DateTime));
        columns.Add(SHIPVIA_FIELD, typeof(System.String));
        columns.Add(SHIPACCT_FIELD, typeof(System.String));
        columns.Add(CSHIPNO_FIELD, typeof(System.String));
        columns.Add(PTERMS_FIELD, typeof(System.String));
        columns.Add(PDISC_FIELD, typeof(System.Decimal));
        columns.Add(PDAYS_FIELD, typeof(System.Int32));
        columns.Add(PNET_FIELD, typeof(System.Int32));
        columns.Add(DISC_FIELD, typeof(System.Decimal));
        columns.Add(TAXRATE_FIELD, typeof(System.Decimal));
        columns.Add(ORDAMT_FIELD, typeof(System.Decimal));
        columns.Add(SHPAMT_FIELD, typeof(System.Decimal));
        columns.Add(PONUM_FIELD, typeof(System.String));
        columns.Add(SALESMN_FIELD, typeof(System.String));
        columns.Add(SOSTAT_FIELD, typeof(System.String));
        columns.Add(TAX_FIELD, typeof(System.Decimal));
        columns.Add(TOSW_FIELD, typeof(System.String));
        columns.Add(COMMENT_FIELD, typeof(System.String));
        columns.Add(CCNUMBER_FIELD, typeof(System.String));
        columns.Add(CCNAME_FIELD, typeof(System.String));
        columns.Add(CCEXP_FIELD, typeof(System.String));
        columns.Add(CCAUTHCODE_FIELD, typeof(System.String));
        columns.Add(CCAUTHMEMO_FIELD, typeof(System.String));
        columns.Add(CCTRANID_FIELD, typeof(System.String));
        columns.Add(CCAUTHAMT_FIELD, typeof(System.Decimal));
        columns.Add(CCGUID_FIELD, typeof(System.String));
        columns.Add(DEFACARD_FIELD, typeof(System.Boolean));
        columns.Add(VAULTERROR_FIELD, typeof(System.Boolean));
        columns.Add(CCTYPE_FIELD, typeof(System.String));
        columns.Add(CCDISPLAY_FIELD, typeof(System.String));
        columns.Add(CCADD1_FIELD, typeof(System.String));
        columns.Add(CCADD2_FIELD, typeof(System.String));
        columns.Add(CCCITY_FIELD, typeof(System.String));
        columns.Add(CCSTATE_FIELD, typeof(System.String));
        columns.Add(CCZIP_FIELD, typeof(System.String));
        columns.Add(CCCOUNTRY_FIELD, typeof(System.String));

        columns.Add(KEEPCARD_FIELD, typeof(System.Boolean));
        columns.Add(CUSTSOURCE_FIELD, typeof(System.Int32));
        columns.Add(WEBEMAIL_FIELD, typeof(System.String));
        columns.Add(CCSECUCODE_FIELD, typeof(System.String));
        columns.Add(FINALIZED_FIELD, typeof(System.Boolean));
        columns.Add(SHIP_COMPANY_FIELD, typeof(System.String));
        columns.Add(SHIP_ADDRESS1_FIELD, typeof(System.String));
        columns.Add(SHIP_ADDRESS2_FIELD, typeof(System.String));
        columns.Add(SHIP_CITY_FIELD, typeof(System.String));
        columns.Add(SHIP_STATE_FIELD, typeof(System.String));
        columns.Add(SHIP_ZIP_FIELD, typeof(System.String));
        columns.Add(SHIP_COUNTRY_FIELD, typeof(System.String));
        columns.Add(TAXTBL_FIELD, typeof(System.String));
        columns.Add(EMAILOPTIN_FIELD, typeof(System.Boolean));
        column = columns.Add(WPPSONO_FIELD, typeof(System.Int32));
        column.AllowDBNull = false;
        column.AutoIncrement = true;

        columns.Add(ADDUSER_FIELD, typeof(System.String));
        columns.Add(ADDTOMYADDRESSLIST_FIELD, typeof(System.Boolean));
        columns.Add(SHIP_CONTACT_FIELD, typeof(System.String));
        columns.Add(SHIP_PHONE_FIELD, typeof(System.String));
        columns.Add(RECURRING_FIELD, typeof(System.Boolean));
        columns.Add(NEXTBILL_FIELD, typeof(System.DateTime));
        columns.Add(LASTBILL_FIELD, typeof(System.DateTime));
        columns.Add(RECURRINGFREQUENCY_FIELD, typeof(System.Int32));
        columns.Add(RECURRINGPERIOD_FIELD, typeof(System.String));
        columns.Add(RECURRINGSHIPVIA_FIELD, typeof(System.String));
        columns.Add(CSHIPNO2_FIELD, typeof(System.String));
        columns.Add(CSHIPEFF1_FIELD, typeof(System.DateTime));
        columns.Add(CSHIPEFF2_FIELD, typeof(System.DateTime));
        columns.Add(CC_AUTHEXP, typeof(System.DateTime));
        columns.Add(CC_PREAUTHID, typeof(System.String));
        columns.Add(WPPCUSTNO_FIELD, typeof(System.Int32));
        columns.Add(NOTES_FIELD, typeof(System.String));
        columns.Add(COLLECTSHIPPING_FIELD, typeof(System.Boolean));
        columns.Add(SHIPCOMPLETE_FIELD, typeof(System.String));

        columns.Add(INTERNATIONALPHONE_FIELD, typeof(System.String));

        columns.Add(FOB_FIELD, typeof(System.String));

        columns.Add(SHIPTOTYPE_FIELD, typeof(System.Int32));

        columns.Add(QUOTEID_FIELD, typeof(System.Int32));

        this.Tables.Add(table);

        // Create the customer table
        table = new DataTable(CUSTOMER_TABLE);

        table.Columns.Add(CUSTNO_FIELD, typeof(System.String));
        table.Columns.Add(TAXRATE_FIELD, typeof(System.Decimal));
        table.Columns.Add(WEBEMAIL_FIELD, typeof(System.String));

        this.Tables.Add(table);

        // Create the shipping address table
        table = new DataTable(SHIPPING_ADDRESS_TABLE);
        columns = table.Columns;

        columns.Add(WPPSONO_FIELD, typeof(System.Int32));
        columns.Add(SONO_FIELD, typeof(System.String));
        columns.Add(CUSTNO_FIELD, typeof(System.String));
        columns.Add(CSHIPNO_FIELD, typeof(System.String));
        columns.Add(ADDRESS_TYPE_FIELD, typeof(System.String));
        columns.Add(COMPANY_FIELD, typeof(System.String));
        columns.Add(ADDRESS1_FIELD, typeof(System.String));
        columns.Add(ADDRESS2_FIELD, typeof(System.String));
        columns.Add(ADDRESS3_FIELD, typeof(System.String));
        columns.Add(CITY_FIELD, typeof(System.String));
        columns.Add(STATE_FIELD, typeof(System.String));
        columns.Add(COUNTRY_FIELD, typeof(System.String));
        columns.Add(ZIP_FIELD, typeof(System.String));

        this.Tables.Add(table);

        // Create the Pro order detail table, for viewing past orders
        table = new DataTable(ORDER_PRODETAIL_TABLE);
        columns = table.Columns;

        columns.Add(WPPSONO_FIELD, typeof(System.Int32));
        columns.Add(SONO_FIELD, typeof(System.String));
        columns.Add(CUSTNO_FIELD, typeof(System.String));
        columns.Add(ITEM_FIELD, typeof(System.String));
        columns.Add(DESCRIP_FIELD, typeof(System.String));
        columns.Add(DISCOUNT_FIELD, typeof(System.Decimal));
        columns.Add(TAXRATE_FIELD, typeof(System.Decimal));
        columns.Add(COST_FIELD, typeof(System.Decimal));
        columns.Add(PRICE_FIELD, typeof(System.Decimal));
        columns.Add(QTYORD_FIELD, typeof(System.Int32));
        columns.Add(QTYSHP_FIELD, typeof(System.Int32));
        columns.Add(EXTPRICE_FIELD, typeof(System.Decimal));
        columns.Add(LINENO_FIELD, typeof(System.Int32));
        columns.Add(WEIGHT_FIELD, typeof(System.Decimal));
        columns.Add(TAXCODE_FIELD, typeof(System.String));
        columns.Add(PRICESOURCE_FIELD, typeof(System.String));

        this.Tables.Add(table);

        // Create the Pro order master table, for viewing past orders
        table = new DataTable(ORDER_PROMASTER_TABLE);
        columns = table.Columns;

        columns.Add(SONO_FIELD, typeof(System.String));
        columns.Add(CUSTNO_FIELD, typeof(System.String));
        columns.Add(SODATE_FIELD, typeof(System.String));
        columns.Add(ORDATE_FIELD, typeof(System.DateTime));
        columns.Add(SHIPVIA_FIELD, typeof(System.String));
        columns.Add(CSHIPNO_FIELD, typeof(System.String));
        columns.Add(PTERMS_FIELD, typeof(System.String));
        columns.Add(PDISC_FIELD, typeof(System.Decimal));
        columns.Add(PDAYS_FIELD, typeof(System.Int32));
        columns.Add(PNET_FIELD, typeof(System.Int32));
        columns.Add(DISC_FIELD, typeof(System.Decimal));
        columns.Add(TAXRATE_FIELD, typeof(System.Decimal));
        columns.Add(ORDAMT_FIELD, typeof(System.Decimal));
        columns.Add(SHPAMT_FIELD, typeof(System.Decimal));
        columns.Add(PONUM_FIELD, typeof(System.String));
        columns.Add(SALESMN_FIELD, typeof(System.String));
        columns.Add(SOSTAT_FIELD, typeof(System.String));
        columns.Add(SOTYPE_FIELD, typeof(System.String));
        columns.Add(WEBSONO_FIELD, typeof(System.String));
        columns.Add(STATUSTEXT_FIELD, typeof(System.String));

        this.Tables.Add(table);

    }

}