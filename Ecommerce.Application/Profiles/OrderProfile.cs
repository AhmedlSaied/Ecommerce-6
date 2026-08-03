using AutoMapper;
using Ecommerce.Application.Dtos.AutenticationDtos;
using Ecommerce.Application.Dtos.OrderDtos;
using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Profiles
{
    public class OrderProfile : Profile
    {

        public OrderProfile()
        {
            CreateMap<AddressDto , ShipingAddress>().ReverseMap();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(x => x.DeliveryMethod, o => o.MapFrom(x => x.DeliveryMethod.ShortName))
                .ForMember(x => x.DeliveryMethodCost, o => o.MapFrom(x => x.DeliveryMethod.Price));


            CreateMap<ItemOfOrder, OrderItemDto>()
                .ForMember(o => o.ProductId, o => o.MapFrom(x => x.ProductOfOrderItem.ProductId))
                .ForMember(o => o.ProductName, o => o.MapFrom(x => x.ProductOfOrderItem.ProductName))
                .ForMember(o => o.PictureUrl, o => o.MapFrom<PictureOrderUrlResolver>());




        }
    }
}
