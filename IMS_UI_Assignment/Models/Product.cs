using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS_UI_Assignment.Models
{
    public class Product
    {
        public int productId { get; set; }
        public string? productName { get; set; }
        public string? productCategory { get; set; }
        public decimal productPrice { get; set; }
        public int quantity { get; set; }
    }
}
