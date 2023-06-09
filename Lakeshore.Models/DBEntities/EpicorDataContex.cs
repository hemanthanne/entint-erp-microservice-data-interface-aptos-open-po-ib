using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Lakeshore.Models.DbEntities
{
    public partial class EpicorDataContex : DbContext
    {
        public EpicorDataContex()
        {
        }

        public EpicorDataContex(DbContextOptions<EpicorDataContex> options)
            : base(options)
        {
        }

        public virtual DbSet<OrderPayment> OrderPayments { get; set; } = null!;
        public virtual DbSet<VSapOrderPayment> VSapOrderPayments { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderPayment>(entity =>
            {
                entity.HasKey(e => new { e.StoreTransactionNo, e.StoreNo, e.EntryDatetime, e.SequenceNo });

                //entity.Property(e => e.CustLiabilityStatus).IsFixedLength();

                //entity.Property(e => e.OfflineFlag).IsFixedLength();
            });

            if (Database.IsSqlServer())
            {
                modelBuilder.Entity<VSapOrderPayment>(entity =>
                {
                    entity.HasNoKey();
                    entity.ToView("v_sap_order_payment");
                });
            }
            else
            {
                modelBuilder.Entity<VSapOrderPayment>(entity =>
                {
                    entity.HasKey(e => e.StoreTransactionNo);
                    
                });
            }

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
