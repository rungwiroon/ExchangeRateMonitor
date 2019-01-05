using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRate.Services.Models
{
    public class Rate
    {
        public string cCode { get; set; }
        public decimal cBuying { get; set; }
        public decimal cSelling { get; set; }
        public DateTime dateTime { get; set; }
        public string cName { get; set; }
        public string curcode { get; set; }
        public string pff { get; set; }
        public string denom { get; set; }
    }
}
