using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;

namespace DataAccessLayer.EF
{
    public class Initializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            context.Currencies.Add(new Currency() {Id = Guid.NewGuid(), Name = "EU", GlobalIndex = 1});
            base.Seed(context);
        }
    }
}
