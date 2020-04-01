using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPayments
{
    public class Deposit
    {
        public Deposit()
        {
            Id = 0;
            Account = "";
            Sum = 0;
            Count = 0;
            Address = "";
        }

        public int Id { get; set; }
        public string Account { get; set; }
        public string Address { get; set; }
        public double Sum { get; set; }
        public int Count { get; set; }
    }
}
