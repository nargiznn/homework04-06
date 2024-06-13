using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Data.Entities;
using UniversityApp.Core.Entities;

namespace UniversityApi.Models
{
	public class UniversityDbContext:DbContext
	{
		public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options)
		{

		}
		public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration<Group>(new GroupConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}

 