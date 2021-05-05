using System.Collections.Generic;
using System.Linq;

namespace CheckoutPromotionClassLibrary
{
    public class DriverEngine
    {
        private IEnumerable<SKU_Price> PriceList;
        private IEnumerable<Promotion> Promotions;

        public DriverEngine(IEnumerable<SKU_Price> priceList, IEnumerable<Promotion> promotions)
        {
            this.PriceList = priceList;
            this.Promotions = promotions;
        }

        public void CheckOut(Order order)
        {
            var foundItems = new List<Item>();
            if (Promotions != null && Promotions.Count() > 0)
                foreach (var promotion in Promotions)
                {
                    var validatedItems = promotion.ValidateOrder(order, foundItems);
                    UpdateValidatedItems(foundItems, validatedItems);
                }

            ApplyRegularPrice(order, foundItems);
        }

        private void ApplyRegularPrice(Order order, List<Item> foundItems)
        {
            foreach (var item in order.Items)
            {
                var validateItem = foundItems.FirstOrDefault(x => x.SKU_Id == item.SKU_Id) ?? item;
                var quantity = validateItem.Quantity;
                if (quantity > 0)
                    order.TotalAmount += quantity * PriceList.First(x => x.SKU_Id == item.SKU_Id).UnitPrice;
            }
        }

        private static void UpdateValidatedItems(List<Item> foundItems, IEnumerable<Item> validatedItems)
        {
            if (validatedItems == null || validatedItems.Count() < 1)
                return;

            foreach (var item in validatedItems)
                if (!foundItems.Any(x => x.SKU_Id == item.SKU_Id))
                    foundItems.Add(item);
        }
    }
}
