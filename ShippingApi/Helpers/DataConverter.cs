using System;
using System.Data;

namespace ShippingApi
{
    class DataConverter
    {
        internal static string ColumnToString(DataRow pRow, string columnName, bool notKnown = false)
        {
            return Convert.ToString(pRow[columnName]);
        }
        internal static string ColumnToString(DataRow pRow, string columnName,string defaultVal, bool notKnown = false)
        {
            if (pRow[columnName] != null)
                return Convert.ToString(pRow[columnName]);
            return defaultVal;
        }
        internal static decimal ColumnToDecimal(DataRow pRow, string columnName)
        {
            return Convert.ToDecimal(pRow[columnName]);
        }

        internal static bool ColumnToBoolean(DataRow pRow, string columnName)
        {
            return Convert.ToBoolean(pRow[columnName]);
        }

        internal static int ColumnToInt(DataRow pRow, string columnName)
        {
            return Convert.ToInt32(pRow[columnName]);
        }
        internal static int ColumnToInt(DataRow pRow, string columnName,int defaultValue)
        {
            return pRow[columnName] !=null ? Convert.ToInt32(pRow[columnName]) :defaultValue;
        }

        internal static DateTime ColumnToDateTime(DataRow pRow, string columnName, DateTime defaultValue)
        {
            try
            {
                return Convert.ToDateTime(pRow[columnName]);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        internal static int ColumnToInt(DataRowView rv, string columnName)
        {
            return Convert.ToInt32(rv[columnName]);
        }
    }
}