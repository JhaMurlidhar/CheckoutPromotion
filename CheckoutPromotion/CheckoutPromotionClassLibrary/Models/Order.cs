using System.Collections.Generic;

namespace CheckoutPromotionClassLibrary
{
    public class Order
    {
        public List<Item> Items { get; set; }
        public double TotalAmount { get; set; }
    }
}
