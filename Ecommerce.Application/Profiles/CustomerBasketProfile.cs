using AutoMapper;
using Ecommerce.Application.Dtos.BasketItemDto;
using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Profiles
{
    public class CustomerBasketProfile : Profile
    {
        public CustomerBasketProfile()
        {
            CreateMap<CustomerItem,CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        }
    }
}
