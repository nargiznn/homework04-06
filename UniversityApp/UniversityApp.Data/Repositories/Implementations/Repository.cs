using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Models;
using UniversityApp.Data.Repositories.Interfaces;

namespace UniversityApp.Data.Repositories.Implementations
{
	public class Repository<TEntity>:IRepository<TEntity> where TEntity : class
    {
        private readonly UniversityDbContext _context;
		public Repository(UniversityDbContext context)
		{
            _context = context;
		}

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            foreach (var item in includes)
            {
                query = query.Include(item);
            }

            return query.Where(predicate).ToList();
        }

        public TEntity Get(Expression<Func<TEntity,bool>> predicate, params string[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            foreach (var item in includes)
            {
                query = query.Include(item);
            }

            return query.FirstOrDefault(predicate);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}

