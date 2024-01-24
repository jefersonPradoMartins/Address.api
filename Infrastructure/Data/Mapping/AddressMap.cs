using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Address.Infrastructure.Data.Mapping
{
    public class AddressMap : IEntityTypeConfiguration<Domain.Entities.Address>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Address> builder)
        {
            builder.Property(x => x.AddressId).ValueGeneratedOnAdd();
            builder.HasKey(x => x.AddressId);
            builder.Property(x => x.Street).HasMaxLength(100);
            builder.Property(x => x.City).HasMaxLength(100);
            builder.Property(x => x.State).HasMaxLength(2);
            builder.Property(x => x.ZipCode).HasMaxLength(8);
            builder.Property(x => x.Complements).HasMaxLength(100);
            builder.Property(x => x.Neighborhood).HasMaxLength(100);
            builder.Property(x => x.Ibge).HasMaxLength(7);
        }
    }
}
