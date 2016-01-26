using System;
using BusinessLayer.DTO;

namespace BusinessLayer.Exchange
{
    public class ExchangerInfo
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public PaymentInfo Give { get; set; }

        public PaymentInfo Get { get; set; }
    }
}