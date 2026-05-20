using System;
using System.Collections.Generic;
using System.Text;

namespace _1C_Integration_UI.Models
{
    public class Counterparty
    {
        public int Id { get; set; }
        public string Inn { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
