using AutoMapper;
using CRN.Product.Application.DTOs.Product;
using CRN.Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRN.Product.Application.Mapping
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductDetail, ProductResponseDto>();

            CreateMap<CreateProductDto, ProductDetail>();

            CreateMap<UpdateProductDto, ProductDetail>();
        }
    }
}
