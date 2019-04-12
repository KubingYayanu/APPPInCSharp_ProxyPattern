using NUnit.Framework;

namespace APPPInCSharp_ProxyPattern.UnitTests
{
    [TestFixture]
    public class DBTests
    {
        [SetUp]
        public void SetUp()
        {
            DB.Init();
        }

        [TearDown]
        public void TearDown()
        {
            DB.Clear();
            DB.Close();
        }

        [Test]
        public void StoreProduct()
        {
            ProductData storedProduct = new ProductData();
            storedProduct.name = "MyProduct";
            storedProduct.price = 1234;
            storedProduct.sku = "999";

            DB.Store(storedProduct);
            ProductData retrievedProduct = DB.GetProductData("999");
            DB.DeleteProductData("999");
            Assert.AreEqual(storedProduct, retrievedProduct);
        }

        [Test]
        public void OrderKeyGeneration()
        {
            OrderData o1 = DB.NewOrder("Bob");
            OrderData o2 = DB.NewOrder("Bill");
            int firstOrderId = o1.orderId;
            int secondOrderId = o2.orderId;
            Assert.AreEqual(firstOrderId + 1, secondOrderId);
        }

        [Test]
        public void StoreItem()
        {
            ItemData storedItem = new ItemData(1, 3, "sku");
            DB.Store(storedItem);
            ItemData[] retrievedItems = DB.GetItemsForOrder(1);
            Assert.AreEqual(1, retrievedItems.Length);
            Assert.AreEqual(storedItem, retrievedItems[0]);
        }

        [Test]
        public void NoItems()
        {
            ItemData[] id = DB.GetItemsForOrder(42);
            Assert.AreEqual(0, id.Length);
        }
    }
}