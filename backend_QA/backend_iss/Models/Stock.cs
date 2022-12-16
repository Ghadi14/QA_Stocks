namespace backend_iss.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public string StockName { get; set; }
        public string Location { get; set; }
        public int OwnerId { get; set; }
        public virtual User Owner { get; set; }
        public virtual ICollection<User> Members { get; set; }
        public virtual ICollection<Products> ProductsInStock { get; set; }

    }
}
