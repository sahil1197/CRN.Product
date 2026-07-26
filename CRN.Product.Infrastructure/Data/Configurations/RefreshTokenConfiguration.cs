using CRN.Product.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRN.Product.Infrastructure.Data.Configurations
{
    public class RefreshTokenConfiguration
    : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(
            EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Token)
                   .IsRequired();

            builder.HasOne(x => x.User)
                   .WithMany(x => x.RefreshTokens)
                   .HasForeignKey(x => x.UserId);
        }
    }
}
