using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<HomeMainSlider> HomeMainSlider { get; set; }
        public DbSet<OurVision> OurVision { get; set; }
        public DbSet<MedicalDepartment> MedicalDepartment { get; set; }
        public DbSet<WhyChoose> WhyChoose { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<AboutPhoto> AboutPhotos { get; set; }
        public DbSet<LastestNews> LastestNews { get; set; }
        public DbSet<HomeVideo> HomeVideo { get; set; }
        public DbSet<PricingCard> PricingCard { get; set; }
        public DbSet<FAQCategory> FAQCategory { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketProduct> BasketProducts { get; set; }
        public DbSet<Statistics> Statistics { get; set; }
        public DbSet<Doctor> Doctor { get; set; }

    }
}
