using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Update(TbAddUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<TbAddUser>> GetUsersAsync()
        {
            return await _context.AddUsers.Include(i => i.Photos).ToListAsync();
        }

        public async Task<TbAddUser> GetUserByIdAsync(int id)
        {
            return await _context.AddUsers.FindAsync(id);
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await _context.AddUsers.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.AddUsers
                .Where(x => x.UserName == username)
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<TbAddUser> GetUserByUserNameAsync(string username)
        {
            return await _context.AddUsers.Include(i => i.Photos).FirstOrDefaultAsync(f => f.UserName == username);
        }
    }
}