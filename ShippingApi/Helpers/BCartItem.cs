namespace ShippingApi.Helpers
{
    public class BCartItem : UpdateOrderDetailData
    {
        public int Weight { get; set; }
        public int Volume { get; set; }
        public decimal Price { get; set; }
    }
}