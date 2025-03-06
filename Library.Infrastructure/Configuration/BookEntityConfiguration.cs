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
    public enum BookConstants
    {
        ISBN_LENGHT = 13,
        TITLE_LENGHT = 255,
        DESCRIPTION_LENGHT = 1000,
        IMGPATH_LENGHT = 500,
        GENRE_LENGHT = 20
    }
    internal class BookEntityConfiguration : IEntityTypeConfiguration<BookEntity>
    {
        public void Configure(EntityTypeBuilder<BookEntity> builder)
        {
            builder.HasKey(b => b.Id); 

            builder.Property(b => b.ISBN).IsRequired().HasMaxLength((int)BookConstants.ISBN_LENGHT);
            builder.Property(b => b.Title).IsRequired().HasMaxLength((int)BookConstants.TITLE_LENGHT);
            builder.Property(b => b.Genre).HasMaxLength((int)BookConstants.GENRE_LENGHT);
            builder.Property(b => b.Description).HasMaxLength((int)BookConstants.DESCRIPTION_LENGHT);
            builder .Property(b => b.ImgPath).HasMaxLength((int)BookConstants.IMGPATH_LENGHT);

            builder.Property(b => b.PickDate).IsRequired(false).HasColumnType("date"); 
            builder.Property(b => b.ReturnDate).IsRequired(false).HasColumnType("date"); 
        }
    }
}
