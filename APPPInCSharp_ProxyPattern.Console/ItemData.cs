namespace APPPInCSharp_ProxyPattern
{
    public class ItemData
    {
        public ItemData()
        {
        }

        public ItemData(int orderId, int qty, string sku)
        {
            this.orderId = orderId;
            this.qty = qty;
            this.sku = sku;
        }

        public int orderId { get; set; }

        public int qty { get; set; }

        public string sku { get; set; } = "junk";

        public override bool Equals(object obj)
        {
            if (obj is ItemData)
            {
                ItemData id = obj as ItemData;
                return orderId == id.orderId && qty == id.qty && sku.Equals(id.sku);
            }

            return false;
        }
    }
}