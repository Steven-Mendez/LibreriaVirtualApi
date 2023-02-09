using System;
using System.Collections.Generic;

namespace LibreriaVirtualApi.Models
{
    public partial class Book
    {
        public Book()
        {
            SalesDetails = new HashSet<SalesDetail>();
            ShoppingCars = new HashSet<ShoppingCar>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string Edition { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<SalesDetail> SalesDetails { get; set; }
        public virtual ICollection<ShoppingCar> ShoppingCars { get; set; }
    }
}
