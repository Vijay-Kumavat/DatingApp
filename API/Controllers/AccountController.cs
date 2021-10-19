using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;

        public AccountController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<TbAddUser>> Register(string userName, string passWord)
        {
            using var hmac = new HMACSHA512();

            var user = new TbAddUser
            {   
                UserName = userName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(passWord)),
                PasswordSalt = hmac.Key
            };
            
            _context.AddUsers.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}