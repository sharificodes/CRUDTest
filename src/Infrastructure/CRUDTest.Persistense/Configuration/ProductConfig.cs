using CRUDTest.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;

namespace CRUDTest.Persistense.Configuration
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(u => u.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(u => u.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.ManufacturePhone)
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(u => u.ManufactureEmail)
            .HasMaxLength(50)
            .IsRequired();

            builder.HasKey(u => u.Id);
            builder.HasIndex(u => new { u.ProduceDate, u.ManufactureEmail });
            builder.ToTable("Products");

        }
    }
}
