using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Configuration
{
    public enum AuthorConstants
    {
        NAME_LENGHT = 50,
        SURNAME_LENGHT = 100,
        COUNTRY_LENGHT = 80
    }
    public class AuthorEntityConfiguration : IEntityTypeConfiguration<AuthorEntity>
    {
        public void Configure(EntityTypeBuilder<AuthorEntity> builder)
        {
            builder.HasKey(a => a.Id); 
            builder.Property(a => a.FirstName).IsRequired().HasMaxLength((int)AuthorConstants.NAME_LENGHT); 
            builder.Property(a => a.Surname).IsRequired().HasMaxLength((int)AuthorConstants.SURNAME_LENGHT);
            builder.Property(a => a.BirthDate).IsRequired().HasColumnType("date");
            builder.Property(a => a.Country).HasMaxLength((int)AuthorConstants.COUNTRY_LENGHT);

            builder.HasMany(a => a.Books)
                      .WithOne(b => b.Author)
                      .HasForeignKey(b => b.AuthorID)
                      .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
