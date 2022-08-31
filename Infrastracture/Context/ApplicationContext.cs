using Infrastracture.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Context
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductGallery> ProductGalleries { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<EmailRegister> emailRegisters { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Slider> sliders { get; set; }


    }
}
