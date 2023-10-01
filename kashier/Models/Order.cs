namespace kashier.Models
{
    public class Order
    {
        public string Amount { get; set; }
        public string Currency { get; set; }
        public string MerchantOrderId { get; set; }
        public string Mid { get; set; }
        public string Secret { get; set; }
        public string BaseUrl { get; set; }
        public string Mode { get; set; }
        public string AllowedMethods { get; set; }
        public string Hash { get; set; }
    }
}
