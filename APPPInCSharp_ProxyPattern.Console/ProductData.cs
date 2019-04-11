namespace APPPInCSharp_ProxyPattern
{
    public class ProductData
    {
        public ProductData()
        {
        }

        public ProductData(string name, int price, string sku)
        {
            this.name = name;
            this.price = price;
            this.sku = sku;
        }

        public string name { get; set; }

        public int price { get; set; }

        public string sku { get; set; }

        public override bool Equals(object obj)
        {
            ProductData pd = (ProductData)obj;
            return name.Equals(pd.name) & price.Equals(pd.price) & sku.Equals(pd.sku);
        }

        public override int GetHashCode()
        {
            return name.GetHashCode() ^ price.GetHashCode() ^ sku.GetHashCode();
        }
    }
}