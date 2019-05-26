using System.Collections.Generic;

namespace Demo.Models
{
    public class Product : BaseModel<int>
    {
        public Product()
        {
            this.Orders = new HashSet<OrderProducts>();
        }

        public string  Name { get; set; }

        public decimal Price { get; set; }

        public string  ImageUrl { get; set; }

        public virtual ICollection<OrderProducts> Orders { get; set; }
    }
}
