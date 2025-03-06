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
    internal class UserEntityConfiguration :IEntityTypeConfiguration<UserEntity>
    {
        public enum UserConstants
        {
            EMAIL_LENGHT = 255,
            NAME_LENGHT = 100
        }
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email).IsRequired().HasMaxLength((int)UserConstants.EMAIL_LENGHT);
            builder.Property(u => u.HashPassword).IsRequired();
            builder.Property(u => u.DisplayName).IsRequired().HasMaxLength((int)UserConstants.NAME_LENGHT);

        }

    }
}
