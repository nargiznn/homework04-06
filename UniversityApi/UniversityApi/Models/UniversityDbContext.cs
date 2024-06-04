using System;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Data.Entities;

namespace UniversityApi.Models
{
	public class UniversityDbContext:DbContext
	{
		public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options)
		{

		}
		public DbSet<Group> Groups { get; set; }
	}
}

 