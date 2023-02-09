using System;
using System.Collections.Generic;

namespace LibreriaVirtualApi.Models
{
    public partial class SalesDetail
    {
        public int? SaleId { get; set; }
        public int SaleDetail { get; set; }
        public int? BookId { get; set; }
        public int BookQuantity { get; set; }
        public decimal Price { get; set; }

        public virtual Book? Book { get; set; }
        public virtual SalesHistory? Sale { get; set; }
    }
}
