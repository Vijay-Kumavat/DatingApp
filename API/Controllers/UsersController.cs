using System.Collections.Generic;
using System.Linq;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<IEnumerable<TbAddUser>> GetUsers()
        {
            return _context.AddUsers.ToList();
        }


        [HttpGet("{id}")]
        public ActionResult<TbAddUser> GetUsers(int id)
        {
            return _context.AddUsers.Find(id);
        }
    }
}