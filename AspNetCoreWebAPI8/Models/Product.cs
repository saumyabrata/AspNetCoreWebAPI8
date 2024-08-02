namespace AspNetCoreWebAPI8.Models
{
    public class Product
    {
        public int Product_Id { get; set; }
        public string? Title { get; set; }
        public decimal? Rate { get; set; }
        public char? TaxCode { get; set; }
    }
}
