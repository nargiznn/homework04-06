using System;
using Course.Service.Dtos;

namespace Course.Service.Services.Interfaces
{
	public interface IGroupService
	{
        int Create(GroupCreateDto createDto);
        List<GroupGetDto> GetAll();
    }
}

