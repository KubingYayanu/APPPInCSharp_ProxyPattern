namespace APPPInCSharp_ProxyPattern
{
    public class ProductImpl : Product
    {
        private int price;
        private string name;
        private string sku;

        public ProductImpl(string sku, string name, int price)
        {
            this.price = price;
            this.name = name;
            this.sku = sku;
        }

        public string Name => name;

        public int Price => price;

        public string Sku => sku;
    }
}