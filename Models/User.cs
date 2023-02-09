using System;
using System.Collections.Generic;

namespace LibreriaVirtualApi.Models
{
    public partial class User
    {
        public User()
        {
            Books = new HashSet<Book>();
            SalesHistories = new HashSet<SalesHistory>();
            ShoppingCars = new HashSet<ShoppingCar>();
        }

        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<SalesHistory> SalesHistories { get; set; }
        public virtual ICollection<ShoppingCar> ShoppingCars { get; set; }
    }
}
