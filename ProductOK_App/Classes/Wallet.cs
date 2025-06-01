using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductOK_App.Classes
{
    public class Wallet
    {
        public decimal _balance { get; set; }

        public Wallet()
        {
            var random = new Random();
            _balance = random.Next(1500, 5001);
        }
    }
}
