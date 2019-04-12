namespace APPPInCSharp_ProxyPattern
{
    public interface Order
    {
        string CustomerId { get; }

        int Total { get; }

        void AddItem(Product p, int quantity);
    }
}