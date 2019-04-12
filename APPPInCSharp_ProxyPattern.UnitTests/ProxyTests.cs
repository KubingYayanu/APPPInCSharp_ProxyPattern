using NUnit.Framework;

namespace APPPInCSharp_ProxyPattern.UnitTests
{
    [TestFixture]
    public class ProxyTests
    {
        [SetUp]
        public void SetUp()
        {
            DB.Init();
            ProductData pd = new ProductData();
            pd.sku = "ProxyTest1";
            pd.name = "ProxyTestName1";
            pd.price = 456;
            DB.Store(pd);
        }

        [TearDown]
        public void TearDown()
        {
            DB.DeleteProductData("ProxyTest1");
            DB.Clear();
            DB.Close();
        }

        [Test]
        public void ProductProxy()
        {
            Product p = new ProductProxy("ProxyTest1");
            Assert.AreEqual(456, p.Price);
            Assert.AreEqual("ProxyTestName1", p.Name);
            Assert.AreEqual("ProxyTest1", p.Sku);
        }

        [Test]
        public void OrderProxyTotal()
        {
            DB.Store(new ProductData("Wheaties", 349, "wheaties"));
            DB.Store(new ProductData("Crest", 258, "crest"));
            ProductProxy wheaties = new ProductProxy("wheaties");
            ProductProxy crest = new ProductProxy("crest");
            OrderData od = DB.NewOrder("testOrderProxy");
            OrderProxy order = new OrderProxy(od.orderId);
            order.AddItem(crest, 1);
            order.AddItem(wheaties, 2);
            Assert.AreEqual(956, order.Total);
        }
    }
}