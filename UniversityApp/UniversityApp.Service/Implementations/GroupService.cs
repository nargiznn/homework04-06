﻿using System;
using Microsoft.AspNetCore.Http;
using UniversityApi.Data.Entities;
using UniversityApp.Data.Repositories.Interfaces;
using UniversityApp.Service.Dtos.GroupDtos;
using UniversityApp.Service.Exceptions;
using UniversityApp.Service.Interfaces; 

namespace UniversityApp.Service.Implementations
{
	public class GroupService:IGroupService
	{
        private readonly IGroupRepository _groupRepository;
		public GroupService(IGroupRepository groupRepository)
		{
            _groupRepository = groupRepository;
		}

        public int Create(GroupCreateDto createDto)
        {
            if(_groupRepository.Exists(x=>x.No==createDto.No && !x.IsDeleted) )
            {
                throw new RestException(StatusCodes.Status400BadRequest, "No", "No already taken");
            }
            Group entity = new Group
            {
                No = createDto.No,
                Limit = createDto.Limit
            };
            _groupRepository.Add(entity);
            _groupRepository.Save();
            return entity.Id;

        }

        public void Delete(int Id)
        {
            Group entity = _groupRepository.Get(x => x.Id == Id && !x.IsDeleted);
            if (entity == null) throw new RestException(StatusCodes.Status404NotFound, "Group not found");
            //_groupRepository.Delete(entity);
            entity.IsDeleted = true;
            entity.ModifiedAt = DateTime.Now;
            _groupRepository.Save();
        }

        public List<GroupGetDto> GetAll(string search=null)
        {
            return _groupRepository.GetAll(x => x.No.Contains(search)).Select(x => new GroupGetDto
            {
                Id = x.Id,
                No = x.No,
                Limit = x.Limit
            }).ToList();
         
        }

        public GroupGetDto GetById(int id)
        {
            Group entity = _groupRepository.Get(x => x.Id == id && !x.IsDeleted);
            if (entity == null) throw new RestException(StatusCodes.Status404NotFound, "Group not found");
            return GroupMapper.MapFromEntityToGetDto(entity);
        }

        public void Update(int id, GroupUpdateDto updateDto)
        {
            Group entity = _groupRepository.Get(x => x.Id == id && !x.IsDeleted,"Students");
            if(entity.No!=updateDto.No && _groupRepository.Exists(x=>x.No==updateDto.No && !x.IsDeleted))
            {
                throw new RestException(StatusCodes.Status400BadRequest, "No", "No already group taken");
            }
            if (entity.Students.Count > updateDto.Limit)
            {
                throw new RestException(StatusCodes.Status400BadRequest, "Limit", "Limit problem");
            }
            if (entity == null) throw new RestException(StatusCodes.Status404NotFound, "Group not found");
            entity.No = updateDto.No;
            entity.Limit = updateDto.Limit;
            entity.ModifiedAt = DateTime.Now;
            _groupRepository.Save(); 
        }
    }
}

