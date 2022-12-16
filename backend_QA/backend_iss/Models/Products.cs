namespace backend_iss.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string productName { get; set; }
        public string expirationDate { get; set; }
        public int StockId { get; set; }
        public virtual Stock Stock { get; set; }
    }
}
