using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Video.Core.Entities;

namespace Video.Infrastructrue.Database.EntityConfigurations
{
   public class AccountConfiguration :IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            //builder.HasKey(p =>p.Id );
            //builder.Property(p=>p.Id).HasValueGenerator(DatabaseGeneratedOption.Identity);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(20).HasColumnType("nvarchar(20)").HasDefaultValue("");
            builder.Property(p => p.Phone).IsRequired().HasMaxLength(11).HasColumnType("nvarchar(11)");
            builder.Property(p => p.Password).IsRequired().HasMaxLength(20).HasColumnType("nvarchar(20)");
            builder.Property(p => p.HeadImage).HasColumnType("nvarchar(200)");
            builder.Property(p => p.VipType).IsRequired();
            builder.Property(p => p.VipExpirationDate).HasDefaultValue(DateTime.MinValue);
            builder.Property(p => p.IsGeneralAgent).IsRequired().HasDefaultValue(false);
            builder.Property(p => p.IsShared).IsRequired().HasDefaultValue(false);
            builder.Property(p => p.FreeVideoCount).IsRequired().HasDefaultValue(0);
            builder.Property(p => p.Score).IsRequired().HasDefaultValue(0);
            builder.Property(p => p.Balance).IsRequired().HasDefaultValue(0).HasColumnType("decimal(10,2)"); 
            builder.Property(p => p.Remark).HasMaxLength(200).HasColumnType("nvarchar(200)"); 
            builder.Property(p => p.CreatedDateTime).IsRequired().HasDefaultValueSql("datetime('now')");
            builder.Property(b => b.Timestamp).IsRowVersion();
            builder.Property(p => p.PromoCode).IsRequired().HasColumnType("nvarchar(20)");
            builder.HasOne<Account>(s => s.GeneralAgent)
                .WithMany(g => g.Subordinates)
                .HasForeignKey(s => s.GeneralAgentId);
            builder.HasOne<Account>(s => s.Leader1)
                .WithMany(g => g.Subordinates1)
                .HasForeignKey(s => s.Leader1Id);
            builder.HasOne<Account>(s => s.Leader2)
                .WithMany(g => g.Subordinates2)
                .HasForeignKey(s => s.Leader2Id);
            builder.HasOne<Account>(s => s.Leader3)
                .WithMany(g => g.Subordinates3)
                .HasForeignKey(s => s.Leader3Id);

             
        }
    }
}
