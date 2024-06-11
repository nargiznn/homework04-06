using System;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Data.Entities;
using UniversityApi.Models;
using UniversityApp.Data.Repositories.Interfaces;

namespace UniversityApp.Data.Repositories
{
	public class GroupRepository:IGroupRepository
	{
		private readonly UniversityDbContext _context;
		public GroupRepository(UniversityDbContext context)
		{
			_context = context;
		}
        //public Group? GetById(int id, bool isDeleted = false)
        //{
     
        //    return _context.Groups.FirstOrDefault(x => x.Id == id && x.IsDeleted == isDeleted);
        //}
        public Group? GetById(int id,bool? isDeleted=null,params string[] includes)
		{
			var query = _context.Groups.AsQueryable();
				foreach(var item in includes)
				{
					query = query.Include(item);
				}
			if (isDeleted != null) query = query.Where(x => x.IsDeleted == isDeleted);
			return query.FirstOrDefault(x=>x.Id==id);
		}
		public void Add(Group group)
		{
			_context.Groups.Add(group);
		}
		public void Delete(Group group)
		{
			_context.Groups.Remove(group);
		}

        //public int Create(Group entity)
        //{
        //    throw new NotImplementedException();
        //}

        public List<Group> GetAll(bool? isDeleted = null, params string[] includes)
        {
            var query = _context.Groups.AsQueryable();

            if (isDeleted != null) query = query.Where(x => x.IsDeleted == isDeleted);
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            return query.ToList();
        }

        public int Save()
        {
			return _context.SaveChanges();
        }

        //int IGroupRepository.Add(Group entity)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

