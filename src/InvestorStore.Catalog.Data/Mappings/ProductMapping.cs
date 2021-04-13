using InvestorStore.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestorStore.Catalog.Data.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("VARCHAR(250)");

            builder.Property(p => p.Description)
                .IsRequired()
                .HasColumnType("VARCHAR(500)");

            builder.Property(p => p.Image)
                .IsRequired()
                .HasColumnType("VARCHAR(250)");

            builder.OwnsOne(p => p.Dimensions, b =>
            {
                b.Property(d => d.Height)
                    .HasColumnName("Height")
                    .HasColumnType("int");

                b.Property(d => d.Width)
                    .HasColumnName("Width")
                    .HasColumnType("int");

                b.Property(d => d.Depth)
                    .HasColumnName("Depth")
                    .HasColumnType("int");
            });

            builder.ToTable("Products");
        }
    }
}