using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Lakeshore.Models.DbEntities
{
    public partial class EpicorStagingDataContex : DbContext
    {
        public EpicorStagingDataContex()
        {
        }

        public EpicorStagingDataContex(DbContextOptions<EpicorStagingDataContex> options)
            : base(options)
        {
        }

        public virtual DbSet<Opo> Opos { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
