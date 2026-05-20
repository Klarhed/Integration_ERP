using System;
using System.Collections.Generic;
using System.Text;

namespace _1C_Integration_UI.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public int? WarehouseId { get; set; }
        public int? CounterpartyId { get; set; }

        public virtual Warehouse? Warehouse { get; set; }
        public virtual Counterparty? Counterparty { get; set; }

        public virtual ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
    }
}
