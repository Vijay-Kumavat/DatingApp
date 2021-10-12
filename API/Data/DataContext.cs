using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
    public DbSet<TbAddUser> AddUsers {get; set;}
    
    }
    // dotnet ef database  update -c DataContext 
}