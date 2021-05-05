namespace CheckoutPromotionClassLibrary
{
    public class Item
    {
        public Item() { }
        public Item(Item item)
        {
            SKU_Id = item.SKU_Id;
            Quantity = item.Quantity;
        }

        public char SKU_Id { get; set; }
        public int Quantity { get; set; }

    }
}
