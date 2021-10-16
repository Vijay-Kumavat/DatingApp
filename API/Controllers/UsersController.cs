using System.Collections.Generic;
using System.Linq;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TbAddUser>>> GetUsers()
        {
            return await _context.AddUsers.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TbAddUser>> GetUsers(int id)
        {
            return await _context.AddUsers.FindAsync(id);
        }
    }
}