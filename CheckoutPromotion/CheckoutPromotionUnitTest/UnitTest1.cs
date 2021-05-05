using CheckoutPromotionClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CheckoutPromotionUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        static readonly IEnumerable<SKU_Price> PriceList = new List<SKU_Price>
        {
            new SKU_Price { SKU_Id = 'A', UnitPrice = 50 },
            new SKU_Price { SKU_Id = 'B', UnitPrice = 30 },
            new SKU_Price { SKU_Id = 'C', UnitPrice = 20 },
            new SKU_Price { SKU_Id = 'D', UnitPrice = 15 }
        };

        static readonly IEnumerable<Promotion> Promotions =
            new List<Promotion> {
                new Promotion {
                    Items = new List<Item> {
                        new Item { SKU_Id = 'A', Quantity = 3 }},
                    TotalAmount = 130 }, // 3A's for 130
                new Promotion {
                    Items = new List<Item> {
                        new Item { SKU_Id = 'B', Quantity = 2 }},
                    TotalAmount = 45 }, // 2B's for 45
                new Promotion {
                    Items = new List<Item> {
                        new Item { SKU_Id = 'C', Quantity = 1 },
                        new Item { SKU_Id = 'D', Quantity = 1 }},
                    TotalAmount = 30 } // C + D for 30
            };
        static readonly DriverEngine objEngine = new DriverEngine(PriceList, Promotions);

        [TestMethod]
        public void Test_Case_A()
        {
            var order =
                new Order
                {
                    Items = new List<Item>
                    {
                        new Item { SKU_Id = 'A', Quantity = 1 },
                        new Item { SKU_Id = 'B', Quantity = 1 },
                        new Item { SKU_Id = 'C', Quantity = 1 }
                    }          
                };
            objEngine.CheckOut(order);
            Assert.IsTrue(order.TotalAmount == 100);
        }

        [TestMethod]
        public void Test_Case_B()
        {
            var order =
                new Order
                {
                    Items = new List<Item>
                    {
                        new Item { SKU_Id = 'A', Quantity = 5 },
                        new Item { SKU_Id = 'B', Quantity = 5 },
                        new Item { SKU_Id = 'C', Quantity = 1 }
                    }
                };
            objEngine.CheckOut(order);
            Assert.IsTrue(order.TotalAmount == 370);
        }

        [TestMethod]
        public void Test_Case_C()
        {
            var order =
               new Order
               {
                   Items = new List<Item>
                   {
                        new Item { SKU_Id = 'A', Quantity = 3 },
                        new Item { SKU_Id = 'B', Quantity = 5 },
                        new Item { SKU_Id = 'C', Quantity = 1 },
                        new Item { SKU_Id = 'D', Quantity = 1 }
                   }
               };
            objEngine.CheckOut(order);
            Assert.IsTrue(order.TotalAmount == 280);
        }
    }
}
