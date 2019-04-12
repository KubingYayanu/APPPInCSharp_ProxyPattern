namespace APPPInCSharp_ProxyPattern
{
    public class OrderProxy : Order
    {
        public OrderProxy(int orderId)
        {
            this.orderId = orderId;
        }

        private int orderId;

        public string CustomerId
        {
            get
            {
                OrderData od = DB.GetOrderData(orderId);
                return od.customerId;
            }
        }

        public int Total
        {
            get
            {
                OrderImpl imp = new OrderImpl(CustomerId);
                ItemData[] itemDataArray = DB.GetItemsForOrder(orderId);
                foreach (var item in itemDataArray)
                {
                    imp.AddItem(new ProductProxy(item.sku), item.qty);
                }
                return imp.Total;
            }
        }

        public void AddItem(Product p, int quantity)
        {
            ItemData id = new ItemData(orderId, quantity, p.Sku);
            DB.Store(id);
        }
    }
}