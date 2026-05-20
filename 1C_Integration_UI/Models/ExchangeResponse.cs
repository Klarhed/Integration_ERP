using System;
using System.Collections.Generic;
using System.Text;

namespace _1C_Integration_UI.Models
{
    public class ExchangeResponse
    {
        public string base_code { get; set; }
        public Dictionary<string, decimal> rates { get; set; }
    }
}
