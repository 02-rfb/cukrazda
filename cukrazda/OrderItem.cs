using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cukrazda
{
    public class OrderItem
    {
        public string Name { get; set; } = "";
        public int Price { get; set; }
        public bool Order { get; set; }
        public int Amount { get; set; }

    }
}
