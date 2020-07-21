using ShippingApi.DataStructure;
using ShippingApi.Helpers;

namespace ClientSite.controllers
{
    public class RateShipmentRequest
    {
        public RateShipmentRequest()
        {
            Cart = new BCart();
            ShipToAddress = new ShipToAddress();

        }
        public BCart Cart { get; set; }

        public ShipRateConfigData ShipRateConfigData { get; set; }

        public ShipToAddress ShipToAddress { get; set; }

        public string ShippingLineItemId { get; set; }
    }
}