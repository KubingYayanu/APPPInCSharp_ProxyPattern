namespace APPPInCSharp_ProxyPattern
{
    public class ProductProxy : Product
    {
        private string sku;

        public ProductProxy(string sku)
        {
            this.sku = sku;
        }

        public string Name
        {
            get
            {
                ProductData pd = DB.GetProductData(sku);
                return pd.name;
            }
        }

        public int Price
        {
            get
            {
                ProductData pd = DB.GetProductData(sku);
                return pd.price;
            }
        }

        public string Sku => sku;
    }
}