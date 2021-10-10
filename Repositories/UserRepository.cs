using Microsoft.EntityFrameworkCore;
using SupplyChain.Data;
using SupplyChain.Entities;
using SupplyChain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplyChain.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            try
            {
                return await _context.Users.Where(s => s.Username == username && s.Password == password).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
