using System;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Data.Entities;
using UniversityApi.Models;
using UniversityApp.Data.Repositories.Implementations;
using UniversityApp.Data.Repositories.Interfaces;

namespace UniversityApp.Data.Repositories
{
	public class GroupRepository:Repository<Group> ,IGroupRepository
	{
        private readonly UniversityDbContext _context;
        public GroupRepository(UniversityDbContext context) : base(context)
        {
            _context = context;
        }

    }
}

