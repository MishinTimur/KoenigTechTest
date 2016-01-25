using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    /// <summary>
    /// Обменник
    /// </summary>
    public class Exchanger
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<LocalIndex> LocalIndexes { get; set; } 
    }
}
