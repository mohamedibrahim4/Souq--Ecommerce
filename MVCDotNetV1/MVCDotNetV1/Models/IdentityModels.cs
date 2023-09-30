using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MVCDotNetV1.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public GenderType? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
       public CustomerAddressType Address { get; set; }
        public string CustomerVISA { get; set; }


        // Navigation Properties
        public virtual List<CustomerPhone> phones { get; set; }
        public virtual List<CustomerWishList> customerWishList { get; set; }
        public virtual List<Order> orders { get; set; }
        public virtual Cart cart { get; set; }


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

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerPhone> CustomerPhones { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<CustomerWishList> CustomerWishLists { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<offer> Offers { get; set; }
        public virtual DbSet<maincat> Maincats { get; set; }
        public virtual DbSet<subcat> Subcats { get; set; }
        public virtual DbSet<CartAdmin> CartAdmins { get; set; }



    }
}