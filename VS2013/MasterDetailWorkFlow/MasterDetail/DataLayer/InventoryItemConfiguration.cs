using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using MasterDetail.Models;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDetail.DataLayer
{
    public class InventoryItemConfiguration : EntityTypeConfiguration<InventoryItem>
    {
        public InventoryItemConfiguration()
        {
            Property(ii => ii.InventoryItemCode)
                .HasMaxLength(15)
                .IsRequired()
                .HasColumnAnnotation("Index",
                new IndexAnnotation(new IndexAttribute("AK_InventoryItem_InventoryItemCode") { IsUnique = true }));

            Property(ii => ii.InventoryItemName)
                .HasMaxLength(80)
                .IsRequired()
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute("AK_InventoryItem_InventoryItemName") { IsUnique = true }));


            Property(ii => ii.UnitPrice).HasPrecision(18, 2);
        }
    }
}