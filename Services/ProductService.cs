using AutoMapper;
using SupplyChain.Entities;
using SupplyChain.Helpers;
using SupplyChain.Interfaces.Repository;
using SupplyChain.Interfaces.Services;
using SupplyChain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplyChain.Services
{
    public class ProductService : IProductService
    {
        private IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Insert(ProductRequest request)
        {
            try
            {
                var product = _mapper.Map<Product>(request);
                var res = await _uow.ProductRepository.Add(product);
                await _uow.CompleteAsync();
                return res;
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }

        public async Task<bool> Update(int id, ProductRequest request)
        {
            try
            {
                var product = await _uow.ProductRepository.GetById(id);
                product.Name = request.Name;
                product.Description = request.Description;
                product.Price = request.Price;
                product.UpdatedBy = request.UpdatedBy;
                product.UpdatedDateTime = DateTime.Now;
                await _uow.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }

        public async Task<IEnumerable<ProductResponse>> GetAll()
        {
            try
            {
                var products = await _uow.ProductRepository.GetAll();
                return _mapper.Map<IEnumerable<ProductResponse>>(products);
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }

        public async Task<ProductResponse> GetById(int id)
        {
            try
            {
                var products = await _uow.ProductRepository.GetById(id);
                return _mapper.Map<ProductResponse>(products);
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var res = await _uow.ProductRepository.Delete(id);
                await _uow.CompleteAsync();
                return res;
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }
    }
}
