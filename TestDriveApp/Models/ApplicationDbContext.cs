using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace TestDriveApp.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        public ApplicationDbContext()
            : base("TestDriveConnectionString", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}