using Microsoft.EntityFrameworkCore;
using Midwest.Models.Domain;

namespace Midwest.Data
{
    public class MidWestDBContext : DbContext
    {
        public MidWestDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<tblClients> tblClients { get; set; }
        public DbSet<tblLicenses> tblLicenses { get; set; }

        public DbSet<ActivatedDevice> ActivatedDevice { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationship between tblClients and tblLicenses
            modelBuilder.Entity<tblLicenses>()
                .HasOne(l => l.Client)
                .WithMany(c => c.Licenses)
                .HasForeignKey(l => l.ClientID);

            // Other configurations...

            base.OnModelCreating(modelBuilder);
        }

    }
}
