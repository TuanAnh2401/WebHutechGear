using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Web_Hutech_Gear.Models.EF;

namespace Web_Hutech_Gear.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Avatar { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Messages> Messages { get; set; }




        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<NewsCategory> NewsCategory { get; set; }
        public DbSet<Adv> Advs { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}