using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    /// <summary>
    /// Платежная система
    /// </summary>
    public class PaymentSystem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Currency Currency { get; set; }
    }
}
