using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Models
{
    public class Order :BaseModel<int>
    {
        public Order()
        {
            this.Products = new HashSet<OrderProducts>();
        }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime DateOfCreation { get; set; } = DateTime.UtcNow;

        public virtual ICollection<OrderProducts> Products { get; set; }
    }
}
