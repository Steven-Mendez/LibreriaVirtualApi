using System;
using System.Collections.Generic;

namespace LibreriaVirtualApi.Models
{
    public partial class ShoppingCar
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }

        public virtual Book Book { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
