using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;

namespace BusinessLayer.DTO
{
    public class PaymentSystemInfo
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Currency { get; set; }

        public static explicit operator PaymentSystemInfo(PaymentSystem value)
        {
            return new PaymentSystemInfo()
            {
                Id = value.Id,
                Name = value.Name,
                Currency = value.Currency.Name
            };
        }
    }
}
