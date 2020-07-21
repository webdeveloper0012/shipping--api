namespace ShippingApi.Helpers
{
    public class BCart : UpdateOrderMasterData
    {

        public const string P_AND_A_ITEMID = "99-999-9999";
        private const string BID_CONVERSION_ADD_USER_DEFAULT = "ECOM";  // Set adduser to the value expected by the bid conversion;
        private const string configurationFormatString = "{0:000}"; // Store the configuration in a specific format so they sort correctly, so we can find the last one.
        private const short MINIMUM_SHIPPING_CHARGE = 5;  // The minimum shipping charge.
        private const short STANDARD_ITEM_COUNT = 5;      // The number of items that can be shipped for the minimum shipping charge.

        public BCart()
        {

        }
        /// <summary>
        /// Flag to keep the cart from being written to the database in Update(), to improve performance in contexts where changes to the cart don't need to be recorded.
        /// </summary>
        public bool BlockUpdate = false;

        private BCartItem[] _CartItems = new BCartItem[] { };

        /// <summary>
        /// 
        /// </summary>
        public BCartItem[] CartItems
        {
            get
            {
                if (_CartItems == null)
                {
                    _CartItems = new BCartItem[] { };
                }
                return _CartItems;
            }
            set
            {
                if (value == null)
                {
                    _CartItems = new BCartItem[] { };
                }
                else
                {
                    _CartItems = value;
                }
            }
        }

    }
}