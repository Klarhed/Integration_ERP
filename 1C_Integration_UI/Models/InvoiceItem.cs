using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace _1C_Integration_UI.Models
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal VatRate { get; set; }
    }
}
