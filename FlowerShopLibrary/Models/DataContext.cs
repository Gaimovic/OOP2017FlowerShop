using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopLibrary.Models
{
    public class DataContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>, IDataContext
    {
        public virtual IDbSet<Product> Products { get; set; }
        public virtual IDbSet<Category> Category { get; set; }      
        public virtual IDbSet<OrderDetails> OrderDetails { get; set; }
        public virtual IDbSet<Order> Orders { get; set; }
        public virtual IDbSet<CategoryImages> CategoryImages { get; set; }
        public virtual IDbSet<ProductImages> ProductImages { get; set; }
        public virtual IDbSet<News> News { get; set; }
        public IDbSet<UserRole> UserRoles { get; set; }
        public IDbSet<UserLogin> UserLogins { get; set; }
        public IDbSet<UserClaim> UserClaims { get; set; }

        public DataContext() : base ("FlowerShopIdentity") {}

        public static DataContext Create()
        {
            return new DataContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Product>().ToTable("Products");

            modelBuilder.Entity<Category>().HasKey(c => c.CategoryId);
            modelBuilder.Entity<Category>().ToTable("Categories");

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Category>().HasRequired(c => c.CreatedBy);
            modelBuilder.Entity<Category>().HasOptional(c => c.ModifiedBy);

            modelBuilder.Entity<OrderDetails>().HasKey(od => od.OrderDetailsId);
            modelBuilder.Entity<OrderDetails>().ToTable("OrderDetails");

            modelBuilder.Entity<Order>().HasKey(o => o.OrderId);
            modelBuilder.Entity<Order>().ToTable("Orders");

            modelBuilder.Entity<Product>().HasRequired(p => p.CreatedBy);
            modelBuilder.Entity<Product>().HasOptional(c => c.ModifiedBy);

            modelBuilder.Entity<Category>().HasRequired(c => c.CreatedBy);
            modelBuilder.Entity<Category>().HasOptional(c => c.ModifiedBy);

            modelBuilder.Entity<OrderDetails>().HasRequired(o => o.Product);
            modelBuilder.Entity<OrderDetails>().HasRequired(o => o.Order);

            modelBuilder.Entity<ProductImages>().HasOptional(i => i.Product);

            modelBuilder.Entity<News>().HasKey(n => n.Id);
            modelBuilder.Entity<News>().ToTable("News");
            modelBuilder.Entity<News>().HasRequired(n => n.CreatedBy);
            modelBuilder.Entity<News>().HasOptional(n => n.ModifiedBy);

            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<UserRole>().ToTable("UserRole");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaim");

        }
    }

    public interface IDataContext
    {
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<Product> Products { get; set; }
        IDbSet<Category> Category { get; set; }
        IDbSet<User> Users { get; set; }
        IDbSet<OrderDetails> OrderDetails { get; set; }
        IDbSet<Order> Orders { get; set; }
        IDbSet<CategoryImages> CategoryImages { get; set; }
        IDbSet<ProductImages> ProductImages { get; set; }
        IDbSet<News> News { get; set; }

        IDbSet<UserRole> UserRoles { get; set; }
        IDbSet<UserLogin> UserLogins { get; set; }
        IDbSet<UserClaim> UserClaims { get; set; }

        int SaveChanges();
    }
}
