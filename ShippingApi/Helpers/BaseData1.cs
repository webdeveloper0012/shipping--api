using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using WPPDataModel.ShippingSystem.DataStructure;

namespace ShippingApi.Helpers
{
    public class BaseData
    {
        private const int EXCEPTION_NUMBER_TIMEOUTEXPIRED = -2;
        private const int EXCEPTION_NUMBER_LOGINFAILED = 4060;
        private const int EXCEPTION_NUMBER_SERVERNOTFOUND = 53;

        protected const string ROWCOUNT_FIELD = "rowcounter";

        protected static string ConnectionString
        {
            get
            {
                return ConfigurationManager.AppSettings["WebProPackConfiguration.ConnectionString"];
            }
        }

        protected static DataTable FillTable(string pSelectString, bool pReturnError)
        {
            DataTable table = new DataTable();
            SqlCommand command = new SqlCommand(pSelectString, new SqlConnection(ConnectionString));
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.SelectCommand.Connection.Open();

            try
            {
                da.Fill(table);
            }
            catch (SqlException sqlOops)
            {
                HandleSqlError(sqlOops, pSelectString);
                if (pReturnError)
                {
                    throw sqlOops;
                }
            }
            catch (Exception oops)
            {
                if (pReturnError)
                {
                    throw oops;
                }
            }
            finally
            {
                da.SelectCommand.Connection.Close();
                da.SelectCommand.Connection.Dispose();
            }

            return table;
        }

        protected static DataTable FillTable(string pSelectString)
        {
            DataTable table = new DataTable();
            try
            {
                table = FillTable(pSelectString, true);
            }
            catch (SqlException sqlOops)
            {
                HandleSqlError(sqlOops, pSelectString);
            }
            catch (Exception oops)
            {
                Console.WriteLine(oops.Message);
            }
            finally
            {
            }

            return table;
        }

        private static void HandleSqlError(SqlException pException, string pSql)
        {
            if (pException == null)
            {
                return;
            }

            StringBuilder message = new StringBuilder();
            message.Append(string.Format("SQL Error Number: {0}", pException.Number));
            message.Append(Environment.NewLine);
            if (pException.Number == EXCEPTION_NUMBER_TIMEOUTEXPIRED)
            {
                message.Append("Timeout Expired");
            }

            if (!string.IsNullOrEmpty(pSql))
            {
                message.Append(Environment.NewLine);
                message.Append(Environment.NewLine);
                message.Append("SQL:");
                message.Append(Environment.NewLine);
                message.Append(pSql);
            }

            message.Append(Environment.NewLine);
            message.Append(Environment.NewLine);
            Exception oops = pException;
            while (oops != null)
            {
                message.Append("ERROR: ");
                message.Append(oops.Message);
                message.Append(Environment.NewLine);
                message.Append("STACK TRACE:");
                message.Append(Environment.NewLine);
                message.Append(oops.StackTrace);
                message.Append(Environment.NewLine);
                message.Append(Environment.NewLine);

                oops = oops.InnerException;
            }

            switch (pException.Number)
            {
                case EXCEPTION_NUMBER_SERVERNOTFOUND:
                case EXCEPTION_NUMBER_LOGINFAILED:
                    WPPErrorHandler.EmergencyEmail("EMERGENCY SQL ERROR", message.ToString());
                    break;
                default:
                    if (pSql.Contains("FTRank"))
                    {
                        WPPErrorHandler.PerformanceEmail("SQL Error", message.ToString());
                    }
                    else
                    {
                        WPPErrorHandler.EmergencyEmail("EMERGENCY SQL ERROR", message.ToString());
                    }
                    break;
            }
        }
    }
}