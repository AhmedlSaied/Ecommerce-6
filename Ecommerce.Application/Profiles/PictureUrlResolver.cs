using AutoMapper;
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
    public class PictureUrlResolver(IOptions<UrlSetting> option) : IValueResolver<Product, ProductDtos, string?>
    {
        public string? Resolve(Product source, ProductDtos destination, string? destMember, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source.PictureUrl)) return null;
            var BaseUrl= option.Value.BaseUrl.TrimEnd('/');
            var path = source.PictureUrl.TrimStart('/');
            return $"{BaseUrl}/Files/{path}";
        }
    }



    public class UrlSetting
    {
        public string BaseUrl { get; set; }
    }
}
