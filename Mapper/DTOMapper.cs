using AutoMapper;
using SupplyChain.Entities;
using SupplyChain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplyChain.Mapper
{
    public class DTOMapper : Profile
    {
        public DTOMapper()
        {
            CreateMap<Product, ProductResponse>().ReverseMap();
            CreateMap<ProductRequest, Product>().ReverseMap();
            CreateMap<RegisterRequest, User>().ReverseMap();
        }
    }
}
