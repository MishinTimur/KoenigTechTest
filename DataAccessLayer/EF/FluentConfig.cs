using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;

namespace DataAccessLayer.EF
{
    public partial class ApplicationDbContext 
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PaymentSystem>().HasRequired(a => a.Currency);
            modelBuilder.Entity<Exchanger>()
                .HasMany(a => a.LocalIndexes)
                .WithRequired(a => a.Exchanger);

            modelBuilder.Entity<LocalIndex>().HasRequired(a => a.PaymentSys);
        }
    }
}
