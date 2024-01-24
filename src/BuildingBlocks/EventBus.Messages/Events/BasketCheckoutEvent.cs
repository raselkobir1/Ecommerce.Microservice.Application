namespace EventBus.Messages.Events
{
    public class BasketCheckoutEvent: BaseEvent
    {
        public string UserName { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        //billing address
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;

        //payment 
        public string CardName { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public string CVV { get; set; } = string.Empty;
        public string Expiration { get; set; } = string.Empty;
        public int PaymentMethod { get; set; }
    }
}
