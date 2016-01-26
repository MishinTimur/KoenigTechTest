using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    /// <summary>
    /// Локальный индекс (в пределах одного обменника) между разными платежными системами
    /// </summary>
    public class LocalIndex
    {
        public Guid Id { get; set; }

        public PaymentSystem PaymentSys { get; set; }

        public Exchanger Exchanger { get; set; }

        /// <summary>
        /// Индекс на продажу
        /// </summary>
        public double GiveIndex { get; set; }


        /// <summary>
        /// Индекс на покупку
        /// </summary>
        public double GetIndex { get; set; }
    }
}
