using System;
using Course.Core.Entities;
using Course.Data;
using Course.Service.Dtos;
using Course.Service.Dtos.Group;
using Course.Service.Exceptions;

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
            if (_context.Groups.Any(x => x.No == dto.No))
            {
                throw new DublicateEntityException();
            }

            Group entity = new Group
            {
                No = dto.No,
                Limit=dto.Limit
            };

            _context.Groups.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }
        public bool Update(int id, GroupUpdateDto dto)
        {
            try
            {
                var entity = _context.Groups.FirstOrDefault(x => x.Id == id);
                if (entity == null)
                {
                    return false; 
                }
                entity.No = dto.No;
                entity.Limit = dto.Limit;

                _context.Groups.Update(entity);
                _context.SaveChanges();
                return true; 
            }
            catch (Exception)
            {
               
                throw; 
            }
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

