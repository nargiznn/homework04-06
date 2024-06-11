using System;
using UniversityApi.Data.Entities;

namespace UniversityApp.Data.Repositories.Interfaces
{
	public interface IGroupRepository
	{
		void Add(Group entity);
		void Delete(Group entity);
		Group GetById(int id, bool? isDeleted=null, params string[] includes);
		List<Group> GetAll(bool? isDeleted = null, params string[] includes);
		int Save();
	}
}

