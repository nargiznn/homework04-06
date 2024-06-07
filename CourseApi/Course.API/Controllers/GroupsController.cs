using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Course.Core.Entities;
using Course.Service.Dtos;
using Course.Service.Dtos.Group;
using Course.Service.Exceptions;
using Course.Service.Services;
using Course.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Course.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly GroupService _groupService;

        public GroupsController(GroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPost("")]
        public ActionResult Create(GroupCreateDto createDto)
        {
            try
            {
                return StatusCode(201, new { id = _groupService.Create(createDto) });
            }
            catch (DublicateEntityException e)
            {
                return Conflict();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error");
            }
        }
        public ActionResult Update(int id, GroupUpdateDto updateDto)
        {
            try
            {
                var result = _groupService.Update(id, updateDto);
                if (result)
                {
                    return NoContent(); 
                }
                return NotFound(); 
            }
            catch (Exception)
            {
                return StatusCode(500, "Error"); 
            }
        }


        //[HttpPut("{id}")]
        //public ActionResult Update(int id, GroupUpdateDto updateDto)
        //{
        //    var entity = _context.Groups.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        //    if (entity == null) return NotFound();

        //    if (entity.No != updateDto.No && _context.Groups.Any(x => x.No == updateDto.No && !x.IsDeleted))
        //        return Conflict();

        //    entity.No = updateDto.No;
        //    entity.Limit = updateDto.Limit;

        //    _context.SaveChanges();
        //    return NoContent();
        //}
    }
}

