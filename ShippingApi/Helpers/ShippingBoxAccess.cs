using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using WPPDataModel.ShippingSystem.DataStructure;

namespace WPPDataModel.ShippingSystem.DataAccess
{
    public class ShippingBoxAccess : ShippingBoxData
    {
        //=====================================================================
        protected static ShippingBoxData[] Get(string pSelectSql)
        {
            List<ShippingBoxData> shippingBoxDataList = new List<ShippingBoxData>();
            DataTable shippingBoxTable = FillTable(pSelectSql);
            foreach (DataRow row in shippingBoxTable.Rows)
            {
                shippingBoxDataList.Add(new ShippingBoxData(row));
            }
            return shippingBoxDataList.ToArray();
        }

        private static DataTable FillTable(string pSelectSql)
        {
            SqlConnection connection = new SqlConnection();
            SqlCommand command = new SqlCommand(pSelectSql);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            var dataSet = new DataTable();
            adapter.Fill(dataSet);
            return dataSet;
        }

        const string data = @"[
  {
    'boxname': 'ENVSMALL',
    'boxdesc': 'Small Envelope',
    'boxtype': 'SALE',
    'boxlength': 10,
    'boxwidth': 12.25,
    'boxheight': 0.5,
    'boxweight': 0.25,
    'boxcost': 0,
    'boxenabled': 1,
    'boxmaxweight': 2,
    'boxmaxvolume': 60
  },
  {
    'boxname': 'ENVLARGE',
    'boxdesc': 'Large Envelope',
    'boxtype': 'SALE',
    'boxlength': 13,
    'boxwidth': 18,
    'boxheight': 0.5,
    'boxweight': 0.4,
    'boxcost': 0,
    'boxenabled': 1,
    'boxmaxweight': 4,
    'boxmaxvolume': 117
  },
  {
    'boxname': '36F',
    'boxdesc': '36F',
    'boxtype': 'SALE',
    'boxlength': 16,
    'boxwidth': 12,
    'boxheight': 8,
    'boxweight': 1.75,
    'boxcost': 0,
    'boxenabled': 1,
    'boxmaxweight': 19,
    'boxmaxvolume': 1536
  },
  {
    'boxname': '44T',
    'boxdesc': '44T',
    'boxtype': 'SALE',
    'boxlength': 18,
    'boxwidth': 14,
    'boxheight': 12,
    'boxweight': 2.45,
    'boxcost': 0,
    'boxenabled': 1,
    'boxmaxweight': 35,
    'boxmaxvolume': 3024
  },
  {
    'boxname': '50T',
    'boxdesc': '50T',
    'boxtype': 'SALE',
    'boxlength': 20,
    'boxwidth': 16,
    'boxheight': 14,
    'boxweight': 3.6,
    'boxcost': 0,
    'boxenabled': 1,
    'boxmaxweight': 50,
    'boxmaxvolume': 4480
  },
  {
    'boxname': '56T',
    'boxdesc': '56T',
    'boxtype': 'SALE, POP',
    'boxlength': 22,
    'boxwidth': 18,
    'boxheight': 16,
    'boxweight': 3.95,
    'boxcost': 0,
    'boxenabled': 1,
    'boxmaxweight': 69,
    'boxmaxvolume': 6336
  },
  {
    'boxname': 'CATBOX',
    'boxdesc': 'CATBOX',
    'boxtype': 'SALE',
    'boxlength': 12,
    'boxwidth': 11,
    'boxheight': 2.5,
    'boxweight': 0.75,
    'boxcost': 0,
    'boxenabled': 1,
    'boxmaxweight': 5,
    'boxmaxvolume': 330
  },
  {
    'boxname': '28F',
    'boxdesc': '28F',
    'boxtype': 'SALE',
    'boxlength': 12,
    'boxwidth': 11,
    'boxheight': 7,
    'boxweight': 1.5,
    'boxcost': 0,
    'boxenabled': 1,
    'boxmaxweight': 12,
    'boxmaxvolume': 924
  },
  {
    'boxname': '58T',
    'boxdesc': '58T',
    'boxtype': 'RENTAL,RISOP',
    'boxlength': 24,
    'boxwidth': 18,
    'boxheight': 16,
    'boxweight': 4.3,
    'boxcost': 0,
    'boxenabled': 1,
    'boxmaxweight': 69,
    'boxmaxvolume': 6912
  },
  {
    'boxname': 'M12',
    'boxdesc': 'M12',
    'boxtype': 'RENTAL,RISOP',
    'boxlength': 24,
    'boxwidth': 18,
    'boxheight': 12,
    'boxweight': 4.1,
    'boxcost': 0,
    'boxenabled': 1,
    'boxmaxweight': 52,
    'boxmaxvolume': 5184
  },
  {
    'boxname': 'M6',
    'boxdesc': 'M6',
    'boxtype': 'RENTAL,RISOP',
    'boxlength': 24,
    'boxwidth': 18,
    'boxheight': 6,
    'boxweight': 2.95,
    'boxcost': 0,
    'boxenabled': 1,
    'boxmaxweight': 32,
    'boxmaxvolume': 2592
  },
  {
    'boxname': 'M4',
    'boxdesc': 'M4',
    'boxtype': 'RENTAL,RISOP',
    'boxlength': 24,
    'boxwidth': 18,
    'boxheight': 4,
    'boxweight': 2.25,
    'boxcost': 0,
    'boxenabled': 1,
    'boxmaxweight': 20,
    'boxmaxvolume': 1728
  }
]";
        static ShippingBoxData[] _shippingData;
        //=====================================================================
        public static ShippingBoxData[] GetAllShippingBoxes()
        {
            if (_shippingData ==null)
                _shippingData = JsonConvert.DeserializeObject< ShippingBoxData[]>(data);

            return _shippingData;
            //string selectSQL = "select * from shippingboxes";
            //return Get(selectSQL);
        }
    }
}
