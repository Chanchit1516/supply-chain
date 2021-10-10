using SupplyChain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplyChain.Interfaces.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
}
