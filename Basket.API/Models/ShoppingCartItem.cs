namespace Basket.API.Models
{
    public class ShoppingCartItem
    {
        public string ProductId { get; set; }
        public int Quentity { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; }
    }
}
