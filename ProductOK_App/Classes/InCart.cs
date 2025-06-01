using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductOK_App
{
    public class InCart
    {
        public string _product { get; set; }
        public decimal _price { get; set; }
        public int _count { get; set; }
        public decimal TotalPrice => _price * _count;
        public InCart(string product, decimal price, int count)
        {
            _product = product;
            _price = price;
            _count = count;
        }
    }
}
