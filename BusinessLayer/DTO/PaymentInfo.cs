using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTO
{
    public class PaymentInfo
    {
        public PaymentSystemInfo PaymentSystem { get; set; }

        public double Cost { get; set; }
    }
}
