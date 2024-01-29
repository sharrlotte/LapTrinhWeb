using Bai2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Bai2.Repository
{
	public class ApplicationDbContext : IdentityDbContext

	{
		// Public 
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
		{

		}
		public DbSet<Product> Products { get; set; }
		
		public DbSet<Category> Categories { get; set; }
		
		public DbSet<ProductImage> ProductImages { get; set; }
        
		public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
