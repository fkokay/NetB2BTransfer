using Microsoft.EntityFrameworkCore;
using NetB2BTransfer.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetB2BTransfer.Data
{
    public class NetB2BTransferContext : DbContext
    {
        public NetB2BTransferContext(string connectionString) : base(GetOptions(connectionString))
        {
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries().Where(m => m.State == EntityState.Added || m.State == EntityState.Modified).Select(m => m.Entity);

            foreach (var entity in entities)
            {
                ValidationContext validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext);
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<ErpSetting> ErpSetting { get; set; }
        public DbSet<LogoTransferSetting> LogoTransferSetting { get; set; }
        public DbSet<B2BSetting> B2BSetting { get; set; }

    }
}
