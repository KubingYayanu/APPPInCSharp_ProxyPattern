using System.Collections.Generic;

namespace APPPInCSharp_ProxyPattern
{
    public class OrderImpl : Order
    {
        public OrderImpl(string cusid)
        {
            customerId = cusid;
        }

        private List<Item> items = new List<Item>();
        private string customerId;

        public string CustomerId => customerId;

        public int Total
        {
            get
            {
                int total = 0;
                foreach (var item in items)
                {
                    Product p = item.Product;
                    int qty = item.Quantity;
                    total += p.Price * qty;
                }

                return total;
            }
        }

        public void AddItem(Product p, int quantity)
        {
            Item item = new Item(p, quantity);
            items.Add(item);
        }
    }
}