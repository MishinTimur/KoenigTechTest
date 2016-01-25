using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    /// <summary>
    /// Валюта
    /// </summary>
    public class Currency
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double GlobalIndex { get; set; }
    }
}
