using AutoMapper;
using Ecommerce.Application.Dtos.OrderDtos;
using Ecommerce.Application.Dtos.ProductDtos;
using Ecommerce.Domain.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Profiles
{
    public class PictureOrderUrlResolver(IOptions<UrlSetting> option) : IValueResolver<ItemOfOrder, OrderItemDto, string?>
    {
        public string? Resolve(ItemOfOrder source, OrderItemDto destination, string? destMember, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source.ProductOfOrderItem.PictureUrl)) return null;
            var BaseUrl = option.Value.BaseUrl.TrimEnd('/');
            var path = source.ProductOfOrderItem.PictureUrl.TrimStart('/');
            return $"{BaseUrl}/Files/{path}";
        }
    }
}
