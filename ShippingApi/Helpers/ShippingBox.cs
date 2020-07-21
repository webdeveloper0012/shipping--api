using System.Collections.Generic;

using WPPDataModel.ShippingSystem.DataAccess;
using WPPDataModel.ShippingSystem.DataStructure;

namespace WPPBusinessRules.ShippingSystem
{
    public class ShippingBox : ShippingBoxData
    {
        public int BoxId;
        public decimal BoxValue;
        public decimal BoxInsValue;
        public decimal BoxVolUsed;
        public decimal BoxWeightUsed;

        public ShippingBox(ShippingBoxData pShippingBoxData)
        {

            this.BoxId = 0;
            this.BoxName = pShippingBoxData.BoxName;
            this.BoxDesc = pShippingBoxData.BoxDesc;
            this.BoxType = pShippingBoxData.BoxType;
            this.BoxLength = pShippingBoxData.BoxLength;
            this.BoxWidth = pShippingBoxData.BoxWidth;
            this.BoxHeight = pShippingBoxData.BoxHeight;
            this.BoxWeight = pShippingBoxData.BoxWeight;
            this.BoxCost = pShippingBoxData.BoxCost;
            this.BoxEnabled = pShippingBoxData.BoxEnabled;
            this.BoxMaxWeight = pShippingBoxData.BoxMaxWeight;
            this.BoxMaxVolume = pShippingBoxData.BoxMaxVolume;
            this.BoxValue = 0;
            this.BoxInsValue = 0;
            this.BoxVolUsed = 0;
            this.BoxWeightUsed = 0;
        }

        public static ShippingBox[] GetAllShippingBoxes(string pType = null)
        {
            List<ShippingBox> shippingBoxList = new List<ShippingBox>();
            ShippingBoxData[] shippingBoxes = ShippingBoxAccess.GetAllShippingBoxes();
            foreach (ShippingBoxData shippingBox in shippingBoxes)
            {
                if (string.IsNullOrEmpty(pType) || shippingBox.BoxType.Contains(pType))
                {
                    shippingBoxList.Add(new ShippingBox(shippingBox));
                }
            }

            return shippingBoxList.ToArray();
        }

    }
}
