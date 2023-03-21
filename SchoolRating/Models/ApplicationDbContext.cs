using System;
using Microsoft.EntityFrameworkCore;

namespace SchoolRating.Models
{
	public class ApplicationDbContext : DbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<School> Schools { get; set; }
		public DbSet<Rating> Ratings { get; set; }

      
    }
}

