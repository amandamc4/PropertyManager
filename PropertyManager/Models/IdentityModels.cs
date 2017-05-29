
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Collections.Generic;

namespace PropertyManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
   
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<FacilityBooking> FacilityBookings { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<UnitPhoto> UnitPhotos { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Lease> Leases { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<Occupant> Occupants { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.ServiceRequests)
                .WithRequired(e => e.Service)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Unit>()
                .HasMany(e => e.Apartments)
                .WithRequired(e => e.Unit)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Unit>()
                .HasMany(e => e.UnitPhotos)
                .WithRequired(e => e.Unit)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<FacilityBooking>()
                .HasRequired(e => e.Facility);

            modelBuilder.Entity<FacilityBooking>()
                .HasRequired(e => e.Tenant);

            modelBuilder.Entity<WorkOrder>()
                .HasRequired(e => e.Tenant);

            modelBuilder.Entity<Occupant>()
                 .HasRequired(e => e.Tenant);

            modelBuilder.Entity<Lease>()
                .HasRequired(e => e.Tenant);

            modelBuilder.Entity<Lease>()
               .HasRequired(e => e.Apartment);

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        
    }
}