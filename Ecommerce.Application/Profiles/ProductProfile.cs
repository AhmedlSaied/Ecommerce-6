using AutoMapper;
using Ecommerce.Application.Dtos.ProductDtos;
using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Profiles
{
    public class ProductProfile :Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDtos>()
                .ForMember(dest => dest.BrandName, option => option.MapFrom(src => src.Brand.Name))
                .ForMember(dest=>dest.TypeName,option=>option.MapFrom(src=>src.Type.Name))
                .ForMember(dect => dect.PictureUrl, option => option.MapFrom<PictureUrlResolver>());
            CreateMap<Brand, BrandDtos>();
            CreateMap<ProductType, TypeDtos>();
        }
    }
}
