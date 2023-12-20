using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SneackersStore.Infrastructure
{
    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context")
        {
        }

        public virtual DbSet<CategoryEntity> category { get; set; }
        public virtual DbSet<DiscountEntity> discount { get; set; }
        public virtual DbSet<ProductEntity> product { get; set; }
        public virtual DbSet<RoleEntity> role { get; set; }
        public virtual DbSet<StatusEntity> status { get; set; }
        public virtual DbSet<UserEntity> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryEntity>()
                .HasMany(e => e.product)
                .WithRequired(e => e.category)
                .HasForeignKey(e => e.category_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DiscountEntity>()
                .HasMany(e => e.product)
                .WithOptional(e => e.discount)
                .HasForeignKey(e => e.discount_id);

            modelBuilder.Entity<RoleEntity>()
                .HasMany(e => e.users)
                .WithRequired(e => e.role1)
                .HasForeignKey(e => e.role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StatusEntity>()
                .HasMany(e => e.users)
                .WithRequired(e => e.status1)
                .HasForeignKey(e => e.status)
                .WillCascadeOnDelete(false);
        }
    }
}
