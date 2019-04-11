using APPPInCSharp_ProxyPattern;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
