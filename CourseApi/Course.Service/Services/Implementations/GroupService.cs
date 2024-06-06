using System;
using Course.Core.Entities;
using Course.Data;
using Course.Service.Dtos;

namespace Course.Service.Services
{
	public class GroupService
	{
		private readonly AppDbContext _context;
		public GroupService(AppDbContext context)
		{
			_context = context;
		}
        public int Create(GroupCreateDto dto)
        {

            Group entity = new Group
            {
                No = dto.No,
                Limit=dto.Limit  
            };

            _context.Groups.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }
        public List<GroupGetDto> GetAll()
        {
            return _context.Groups.Select(x => new GroupGetDto
            {
                Id = x.Id,
                No = x.No,
                Limit=x.Limit
            }).ToList();
        }
    }
}

