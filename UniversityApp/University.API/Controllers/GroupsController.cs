using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityApi.Data;
using UniversityApp.Service.Dtos.GroupDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Models;
using UniversityApi.Data;
using UniversityApp.Service.Dtos.GroupDtos;
using UniversityApi.Data.Entities;
using UniversityApp.Service.Interfaces;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniversityApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;
        public GroupsController(IGroupService groupService,IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        }

        [HttpGet("")]
        public ActionResult<List<GroupGetDto>> GetAll()
        {

            return StatusCode(200, _groupService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<GroupGetDto> GetById(int id)
        {

            return StatusCode(200, _groupService.GetById(id));
        }


        [HttpPost("")]
        public ActionResult Create(GroupCreateDto createDto)
        {
           
            return StatusCode(201, new { Id = _groupService.Create(createDto) });
        }

        //[HttpPut("{id}")]
        //public ActionResult Update([FromBody]int id,[FromBody] GroupUpdateDto updateDto)
        //{
        //    _groupService.Update(id, updateDto);
        //    return NoContent();

        //}

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {

            _groupService.Delete(id);
            return NoContent();

        }

        
    }
}


