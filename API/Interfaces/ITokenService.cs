using API.Entities;

namespace API.Interfaces
{
    public interface ITokenService
    {
         string CreateToken(TbAddUser appUser);
    }
}