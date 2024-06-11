using System;
using UniversityApp.Service.Dtos.GroupDtos;

namespace UniversityApp.Service.Interfaces
{
	public interface IGroupService
	{
		int Create(GroupCreateDto createDto);
		void Update(int id, GroupUpdateDto updateDto);
		GroupGetDto GetById(int id);
		List<GroupGetDto> GetAll(string? search=null);
		void Delete(int Id);

	}
}

