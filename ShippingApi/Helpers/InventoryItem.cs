using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Text;

namespace ShippingApi.Helpers
{
    public class InventoryItem : InventoryItemData
    {
        public const string WEBTYPE_NOLONGERAVAILABLE = "NLA";

        public InventoryItem() : base()
        {
        }

        public static InventoryItem Create(InventoryItemData pItemData)
        {
            if (pItemData == null)
            {
                return null;
            }

            InventoryItem item = new InventoryItem();
            item.AddDate = pItemData.AddDate;
            item.AltItem1 = pItemData.AltItem1;
            item.AltItem1Type = pItemData.AltItem1Type;
            item.AltItem2 = pItemData.AltItem2;
            item.AltItem2Type = pItemData.AltItem2Type;
            item.AvgCost = pItemData.AvgCost;
            item.Children = pItemData.Children;
            item.Description = pItemData.Description;
            item.Description2 = pItemData.Description2;
            item.Description3 = pItemData.Description3;
            item.Description4 = pItemData.Description4;
            item.Description5 = pItemData.Description5;
            item.Description6 = pItemData.Description6;
            item.Description7 = pItemData.Description7;
            item.Discontinued = pItemData.Discontinued;
            item.DropShip = pItemData.DropShip;
            item.EFeatured = pItemData.EFeatured;
            item.EHeight = pItemData.EHeight;
            item.ELength = pItemData.ELength;
            item.ELink = pItemData.ELink;
            item.ENoShipCharge = pItemData.ENoShipCharge;
            item.ESellable = pItemData.ESellable;
            item.ESpecial = pItemData.ESpecial;
            item.EStyle = pItemData.EStyle;
            item.EStyle1 = pItemData.EStyle1;
            item.EStyle2 = pItemData.EStyle2;
            item.ETemplate = pItemData.ETemplate;
            item.EWidth = pItemData.EWidth;
            item.ImageLg = pItemData.ImageLg;
            item.ImageMd = pItemData.ImageMd;
            item.ImageSm = pItemData.ImageSm;
            item.ImageXL = pItemData.ImageXL;
            item.IsKit = pItemData.IsKit;
            item.ItemClass = pItemData.ItemClass;
            item.ItemId = pItemData.ItemId;
            item.LeadTime = pItemData.LeadTime;
            item.ListOrder = pItemData.ListOrder;
            item.Manufacturer = pItemData.Manufacturer;
            item.ManufacturerNo = pItemData.ManufacturerNo;
            item.Memo = pItemData.Memo;
            item.Msrp = pItemData.Msrp;
            item.NoShipCalc = pItemData.NoShipCalc;
            item.OnHand = pItemData.OnHand;
            item.OrderingInstructionId = pItemData.OrderingInstructionId;
            item.Phantom = pItemData.Phantom;
            item.Price = pItemData.Price;
            item.pumfact = pItemData.pumfact;
            item.punmsid = pItemData.punmsid;
            item.RecommendedItemTable = pItemData.RecommendedItemTable;
            item.Recurring = pItemData.Recurring;
            item.SearchRank = pItemData.SearchRank;
            item.SearchSource = pItemData.SearchSource;
            item.SOAllocated = pItemData.SOAllocated;
            item.SoloPack = pItemData.SoloPack;
            item.stkumid = pItemData.stkumid;
            item.sumfact = pItemData.sumfact;
            item.sunmsid = pItemData.sunmsid;
            item.TaxCode = pItemData.TaxCode;
            item.ViewOrder = pItemData.ViewOrder;
            item.WebPrice = pItemData.WebPrice;
            item.WebType = pItemData.WebType;

            item.Weight = pItemData.Weight;
            item.Volume = pItemData.Volume;

            item.WOAllocated = pItemData.WOAllocated;
            item.YSalesQty = pItemData.YSalesQty;
            item.UpcCode = pItemData.UpcCode;
            item.SumRating = pItemData.SumRating;
            item.CountRating = pItemData.CountRating;
            item.Discount = pItemData.Discount;

            return item;
        }

        public static ItemSelectParams ItemSelectParameters()
        {
            return new ItemSelectParams();
        }

        public static InventoryItem GetById(string pId)
        {
            return InventoryItem.Create(InventoryItemAccess.GetItemById(pId, InventoryItem.ItemSelectParameters()));
        }

    }

    public class InventoryItemAccess : BaseData
    {
        protected const string FORCENOMATCH_CONDITION = " 1 = 0 ";
        public const string LEFTOUTERJOIN = " LEFT OUTER JOIN ";
        public const string RIGHTOUTERJOIN = " RIGHT OUTER JOIN ";
        public const string INNERJOIN = " INNER JOIN ";

        protected static InventoryItemData[] Get(string pSelectString)
        {
            List<InventoryItemData> inventoryItemList = new List<InventoryItemData>();
            DataTable inventoryItemTable = FillTable(pSelectString);
            foreach (DataRow row in inventoryItemTable.Rows)
            {
                InventoryItemData inventoryItem = new InventoryItemData(row);
                inventoryItemList.Add(inventoryItem);
            }

            return inventoryItemList.ToArray();
        }

        protected static InventoryItemData GetSingle(string pSelectString)
        {
            InventoryItemData[] inventoryItems = Get(pSelectString);
            if (inventoryItems != null)
            {
                if (inventoryItems.Length > 0)
                {
                    if (inventoryItems[0] != null)
                    {
                        return inventoryItems[0];
                    }
                }
            }

            return null;
        }

        protected static string BaseItemColumnList(string pItemTableAlias)
        {
            return string.Join(",", BuildBaseItemColumnList(pItemTableAlias));
        }

        public static string ItemSelectColumns(ItemSelectParams pParams)
        {
            StringBuilder commandText = new StringBuilder(string.Empty);
            commandText.Append("SELECT ");
            string columnList = BaseItemColumnList(InventoryItemData.ITEM_TABLE);
            // The base column list uses InventoryItems for all columns, 
            // but we want Plinid to come from InventoryItems_ProductLines
            columnList = ModifyItemColumnList(columnList, FormatItemColumnString(InventoryItemData.ITEM_TABLE, "Plinid"), FormatItemColumnString(InventoryItemData.ITEMCATEGORYMAPPING_TABLE, "plinid"));
            columnList = ModifyItemColumnList(columnList, null, FormatItemColumnString(InventoryItemData.INVENTORYATLOCATION_TABLE, InventoryItemData.SOALLOCATED_FIELD));
            columnList = ModifyItemColumnList(columnList, null, FormatItemColumnString(InventoryItemData.INVENTORYATLOCATION_TABLE, InventoryItemData.LONHAND_FIELD));
            columnList = ModifyItemColumnList(columnList, null, FormatItemColumnString(InventoryItemData.INVENTORYATLOCATION_TABLE, InventoryItemData.SUPLEAD_FIELD));
            columnList = ModifyItemColumnList(columnList, null, "cpc.SumRating");
            columnList = ModifyItemColumnList(columnList, null, "cpc.CountRating");

            commandText.Append(columnList);
            commandText.Append(string.Format(" FROM {0} {1}", InventoryItemData.ITEM_TABLE, InventoryItemData.ITEM_TABLE));
            commandText.Append(LEFTOUTERJOIN);
            commandText.Append(ProductLineMappingJoin());
            commandText.Append(LEFTOUTERJOIN);
            commandText.Append(InventoryAtLocationJoin(pParams));
            commandText.Append(LEFTOUTERJOIN);
            commandText.Append(CustomerProductRatingJoin());
            return commandText.ToString();
        }

        protected static string[] BuildBaseItemColumnList(string pItemTableAlias)
        {
            List<string> columnList = new List<string>();
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.ITEM_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.ITMDESC_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.PLINID_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.PRICE_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.ITMMEMO_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.TAXCODE_FIELD));

            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.WEIGHT_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.VOLUME_FIELD));

            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.ITMDESC2_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.ITMDESC3_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.ITMDESC4_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.ITMDESC5_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.ITMDESC6_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.EIMAGESM_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.EIMAGEMD_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.EIMAGELG_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.EIMAGEXL_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.ESPECIAL_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.EFEATURED_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.ESELLABLE_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.WEBPRICE_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.ETEMPLATE_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.ELINK_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.ENOSHIPCHG_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.MSRP_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.ESTYLE_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.ESTYLE1_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.ESTYLE2_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.STKUMID_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.SUNSMID_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.RECURRING_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.ELENGTH_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.EWIDTH_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.EHEIGHT_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.NOSHIPCALC_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.SOLOPACK_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.AVGCOST_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.ORDERINGINSTRUCTION_ID));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.PHANTOM_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.MANUFACTURER_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.MANUFACTURERNO_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.WEBTYPE_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.ALTITM1_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.ALTITM1TYPE_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.ALTITM2_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.ALTITM2TYPE_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.UPCCODE_FIELD));
            columnList.Add(FormatItemColumnString(pItemTableAlias, InventoryItemData.DISCOUNT_FIELD));
            columnList.Add("0 AS listorder");
            columnList.Add("0 AS vieworder");

            return columnList.ToArray();
        }

        protected static string ModifyItemColumnList(string pColumnList, string pColumnToRemove, string pColumnToAdd)
        {
            string[] columnList = pColumnList.Split(',');
            if (!string.IsNullOrEmpty(pColumnToRemove))
            {
                columnList = RemoveItemColumn(columnList, pColumnToRemove);
            }
            if (!string.IsNullOrEmpty(pColumnToAdd))
            {
                columnList = AddItemColumn(columnList, pColumnToAdd);
            }
            return string.Join(",", columnList);
        }

        protected static string FormatItemColumnString(string pItemTableAlias, string pColumnName)
        {
            return string.Format("{0}.{1}", pItemTableAlias, pColumnName);
        }

        protected static string[] AddItemColumn(string[] pColumnList, string pColumnToAdd)
        {
            if (pColumnList == null)
            {
                return null;
            }

            List<string> columnList = new List<string>();
            columnList.AddRange(pColumnList);
            columnList.Add(pColumnToAdd);

            return columnList.ToArray();
        }

        protected static string[] RemoveItemColumn(string[] pColumnList, string pColumnToRemove)
        {
            if (pColumnList == null)
            {
                return null;
            }

            List<string> columnList = new List<string>();
            foreach (string column in pColumnList)
            {
                if (column.ToUpper() != pColumnToRemove.ToUpper())
                {
                    columnList.Add(column);
                }
            }

            return columnList.ToArray();
        }

        protected static string ProductLineMappingJoin()
        {
            return ProductLineMappingJoin(null, null);
        }

        protected static string ProductLineMappingJoin(string pMappingTableAlias, string pItemTableAlias)
        {
            StringBuilder plinJoin = new StringBuilder();
            plinJoin.Append(" (SELECT ");
            plinJoin.Append(" item ");
            plinJoin.Append(", MAX(plinid) AS plinid  ");
            plinJoin.Append(", MAX(listorder) AS listorder  ");
            plinJoin.Append(" FROM InventoryItems_ProductLines  ");
            plinJoin.Append(" GROUP BY item ");
            plinJoin.Append(") ");
            plinJoin.Append(" AS InventoryItems_ProductLines  ");
            plinJoin.Append(" ON InventoryItems.Item = InventoryItems_ProductLines.Item");
            return plinJoin.ToString();
        }


        protected static string InventoryAtLocationJoin(ItemSelectParams pParams)
        {
            string[] excludeLocations = pParams.NotShipLocations;

            string excludeWhere = string.Empty;
            if (!ArrayFuncs.IsNullOrEmpty(excludeLocations))
            {
                List<string> excludeLocationList = new List<string>();
                foreach (string locationId in excludeLocations)
                {
                    excludeLocationList.Add(string.Format("inventoryatlocation.loctid <> '{0}' ", locationId));
                }


                string excludes = string.Join(" AND ", excludeLocationList.ToArray());
                excludeWhere = string.Format(" WHERE {0} ", excludes);
            }

            StringBuilder ilocJoin = new StringBuilder();
            ilocJoin.Append("(SELECT ");
            ilocJoin.Append("InventoryAtLocation.item ");
            ilocJoin.Append(", SUM(InventoryAtLocation.lsoaloc) as lsoaloc ");
            ilocJoin.Append(", SUM(InventoryAtLocation.lonhand) AS lonhand ");
            ilocJoin.Append(", SUM(InventoryAtLocation.lwoaloc) AS lwoaloc ");
            ilocJoin.Append(", MAX(InventoryAtLocation.suplead) AS suplead ");
            ilocJoin.Append(string.Format("FROM {0} {0} ", InventoryItemData.INVENTORYATLOCATION_TABLE));
            ilocJoin.Append(excludeWhere);
            ilocJoin.Append(string.Format(" GROUP BY {0}.{1}", InventoryItemData.INVENTORYATLOCATION_TABLE, InventoryItemData.ITEM_FIELD));
            ilocJoin.Append(string.Format(" ) AS {0}", InventoryItemData.INVENTORYATLOCATION_TABLE));
            ilocJoin.Append(string.Format(" ON {0}.{1} = {2}.{1} ", InventoryItemData.ITEM_TABLE, InventoryItemData.ITEM_FIELD, InventoryItemData.INVENTORYATLOCATION_TABLE));
            return ilocJoin.ToString();
        }
        protected static string CustomerProductRatingJoin()
        {
            //select itemid, sum(rating) AS SumRating , count(rating) as countrating from CustomerProductComment group by itemid )  as cpc on inventoryitems.item = cpc.itemId 

            StringBuilder cpcJoin = new StringBuilder();
            cpcJoin.Append("(SELECT ");
            cpcJoin.Append(" itemId, SUM(rating) AS SumRating, COUNT(rating) AS CountRating ");
            cpcJoin.Append(" FROM CustomerProductComment ");
            cpcJoin.Append(" WHERE approved = 1 ");
            cpcJoin.Append(" GROUP BY itemId ");
            cpcJoin.Append(")");
            cpcJoin.Append(" AS cpc ");
            cpcJoin.Append(" ON InventoryItems.Item = cpc.ItemId ");
            return cpcJoin.ToString();
        }
        public static string LimitByPricecodeSql(string pInventoryItemsAlias, LimitByPricecodeParams pParams)
        {
            string sql = string.Empty;
            string custno = pParams.Custno;
            string pricecode = pParams.Pricecode;

            string limitByPricecodeFormatString = " AND {0}.item IN (SELECT item FROM inventorypricing ip WHERE ip.psched = '{1}' OR ip.custno = '{2}') ";

            if (pParams.LimitItemsToCustomerPriceCode)
            {
                switch (pParams.UserType)
                {
                    case 0://UserType.Regular:
                        sql = string.Format(limitByPricecodeFormatString, pInventoryItemsAlias, pricecode, custno);
                        break;
                    case 1://UserType.Administrator:
                        sql = string.Empty;
                        break;
                    case 2://UserType.SalesRep:
                        sql = string.Format(limitByPricecodeFormatString, pInventoryItemsAlias, pricecode, custno);
                        break;
                    default://UserType.Anonymous:
                        sql = string.Format(limitByPricecodeFormatString, pInventoryItemsAlias, pParams.PriceCodeForAnonymousUsers, "NOTUSED");
                        break;
                }
            }
            return sql;
        }

        public static InventoryItemData GetItemById(string pItemId, ItemSelectParams pParams)
        {
            return GetByKey(pItemId, pParams, InventoryItemData.ITEM_FIELD);
        }

        public static InventoryItemData GetByKey(string pId, ItemSelectParams pParams, string pKeyField)
        {
            StringBuilder commandText = new StringBuilder(string.Empty);

            commandText.Append(ItemSelectColumns(pParams));
            commandText.Append(string.Format(" WHERE {0}.{1} = '{2}'", InventoryItemData.ITEM_TABLE, pKeyField, pId));

            commandText.Append(LimitByPricecodeSql(InventoryItemData.ITEM_TABLE, pParams.PriceCodeParams));


            return GetSingle(commandText.ToString());
        }

        public static DataTable GetRecommendedItems(string pItemId)
        {
            string command = string.Format("select * from recommendeditem where item='{0}' ORDER BY seqno", pItemId);
            return BaseData.FillTable(command);
        }


    }
    public class LimitByPricecodeParams
    {
        public string Custno = "";
        public string Pricecode = "";
        public int UserType = 0;
        public string PriceCodeForAnonymousUsers = "";
        public bool LimitItemsToCustomerPriceCode = false;
    }
    public class ItemSelectParams
    {
        public ItemSelectParams()
        {

        }

        public string CategoryId = string.Empty;
        public bool EsellableOnly = true;
        public int BeginningRowNumber = 0;
        public int EndingRowNumber = 0;
        public string[] FilterSelect = new string[0];
        public string[] SortColumns = new string[0];
        public bool GroupParentOnly = false;
        public string[] NotShipLocations = WebProPackConfiguration.NotShipLocations;
        public LimitByPricecodeParams PriceCodeParams = new LimitByPricecodeParams();
        public string CategoryCustomQuery = string.Empty;
    }

    public class WebProPackConfiguration
    {   
        public static string[] NotShipLocations
        {
            get
            {
                NameValueCollection clientsiteconfiguration = (NameValueCollection)ConfigurationManager.GetSection("ClientSiteConfiguration");
                string notShipLocationsSetting = clientsiteconfiguration["NotShipLocations"];
                if (StringFuncs.IsNullOrEmpty(notShipLocationsSetting, true))
                {
                    return null;
                }
                string[] notShipLocations = notShipLocationsSetting.Split('~');
                return notShipLocations;
            }
        }
    }
}