using System;
using System.Collections.Generic;

namespace LibreriaVirtualApi.Models
{
    public partial class SalesHistory
    {
        public SalesHistory()
        {
            SalesDetails = new HashSet<SalesDetail>();
        }

        public int SaleId { get; set; }
        public int? CustomerId { get; set; }
        public decimal Total { get; set; }

        public virtual User? Customer { get; set; }
        public virtual ICollection<SalesDetail> SalesDetails { get; set; }
    }
}
