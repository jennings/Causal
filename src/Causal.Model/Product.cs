using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Causal.Model
{
    public class Product
    {
        public string ProductId { get; set; }
        public Schedule Schedule { get; set; }

        public override string ToString()
        {
            return "Product = " + (ProductId != null ? ProductId : "(null)");
        }
    }
}
