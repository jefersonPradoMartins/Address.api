using Address.Domain.Entities;
using Address.Infrastructure.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Address.Infrastructure.Data.Context
{
    public class Context : DbContext
    {

        public Context() { }

        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AddressMap());
            modelBuilder.ApplyConfiguration(new CustomerMap());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            string conectionString = "Server=JEFERSON\\SQLDEV;Database=AddressApi;User Id=sa;Password=123456789;TrustServerCertificate=true;"; // _configuration["univida_connectionstring"];

            if (conectionString == "")
            {
                throw new ArgumentException("Connection string value cannot be empty");
            }

            optionsBuilder.UseSqlServer(conectionString);
        }
        public DbSet<Domain.Entities.Address> Addresses { get; set; }
        public DbSet<Customer> Customer { get; set; }
    }
}
