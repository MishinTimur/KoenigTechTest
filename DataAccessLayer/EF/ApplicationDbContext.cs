using System.Data.Entity;
using DataAccessLayer.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer.EF
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        /// <summary>
        /// Список валют
        /// </summary>
        public DbSet<Currency> Currencies { get; set; }

        /// <summary>
        /// Список обменников
        /// </summary>
        public DbSet<Exchanger> Exchangers { get; set; }

        /// <summary>
        /// Список платежных систем
        /// </summary>
        public DbSet<PaymentSystem> PaymentSystems { get; set; } 
    }
}