using System;
using UniversityApi.Data.Entities;

namespace UniversityApp.Service.Dtos.GroupDtos
{
	public class GroupMapper
	{
        public static Group MapFromCreateDtoToEntity(GroupCreateDto dto)
        {
            return new Group
            {
                Limit = dto.Limit,
                No = dto.No,
            };
        }

        public static GroupGetDto MapFromEntityToGetDto(Group entity)
        {
            return new GroupGetDto
            {
                Id = entity.Id,
                No = entity.No,
                Limit = entity.Limit
            };
        }
    }
}

