namespace APPPInCSharp_ProxyPattern
{
    public class OrderData
    {
        public OrderData(int orderId, string customerId)
        {
            this.orderId = orderId;
            this.customerId = customerId;
        }

        public string customerId { get; set; }

        public int orderId { get; set; }
    }
}