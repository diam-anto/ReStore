namespace API.Entities
{
    public class BasketItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        // navigation Properties
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}