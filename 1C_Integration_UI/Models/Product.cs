using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1C_Integration_UI.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Article { get; set; }
        public string Name { get; set; }

        public decimal BasePrice { get; set; }

        public virtual ICollection<InvoiceItem> InvoiceEntries { get; set; } = new List<InvoiceItem>();

    }
}

