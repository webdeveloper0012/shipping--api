using System;
using System.Data;
using System.Data.SqlClient;

namespace ShippingApi.DataStructure
{
    public class BaseData
    {
        protected static DataTable FillTable(string pSelectString)
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
                Console.WriteLine(sqlOops.Message);
            }
            catch (Exception oops)
            {
                Console.WriteLine(oops.Message);
            }
            finally
            {
                da.SelectCommand.Connection.Close();
                da.SelectCommand.Connection.Dispose();
            }

            return table;
        }

        protected static string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationSettings.AppSettings["WebProPackConfiguration.ConnectionString"];
            }
        }

    }
}
