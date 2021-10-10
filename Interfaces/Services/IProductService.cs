using SupplyChain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplyChain.Interfaces.Services
{
    public interface IProductService
    {
        Task<bool> Insert(ProductRequest product);
        Task<bool> Update(int id, ProductRequest product);
        Task<IEnumerable<ProductResponse>> GetAll();
        Task<ProductResponse> GetById(int id);
        Task<bool> Delete(int id);
    }
}
