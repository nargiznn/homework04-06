﻿using System;
using System.Linq.Expressions;

namespace UniversityApp.Data.Repositories.Interfaces
{
	public interface IRepository<TEntity> where TEntity :class
	{
		void Add(TEntity entity);
		void Delete(TEntity entity);
		TEntity Get(Expression<Func<TEntity,bool>> predicate, params string[] includes);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, params string[] includes);
        bool Exists(Expression<Func<TEntity, bool>> predicate, params string[] includes);
		int Save();
	}
}

