using AutoMapper;
using eshop.core.DTO.Request;
using eshop.core.Entities;
using eshop.core.Helper;
using eshop.core.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace eshop.core.MapperProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ManagerInfoRequest, Manager>()
                .ForMember(m => m.Password_Hash, rq => rq.MapFrom(rq => AuthenticateHelper.HashPassword(rq.Password)))
                .ForMember(m => m.Role_Id, rq => rq.MapFrom(rq => rq.RoleId));

            CreateMap<CustomerInfoRequest, Customer>()
                .ForMember(m => m.Password_Hash, rq => rq.MapFrom(rq => AuthenticateHelper.HashPassword(rq.Password)));

            CreateMap<CategoryInfoRequest, Category>();

            CreateMap<ProductInfoRequest, Product>()
                .ForMember(p => p.Regular_Price, rq => rq.MapFrom(rq => rq.RegularPrice))
                .ForMember(p => p.Discount_Price, rq => rq.MapFrom(rq => rq.DiscountPrice));

            CreateMap<ProductCategoryViewModel, ProductViewModel>()
                .ForMember(p => p.Id, pc => pc.MapFrom(pc => pc.Product_Id))
                .ForMember(p => p.Name, pc => pc.MapFrom(pc => pc.Product_Name))
                .ForMember(p => p.Created_At, pc => pc.MapFrom(pc => pc.Created_At))
                .ForMember(p => p.Discount_Price, pc => pc.MapFrom(pc => pc.Discount_Price))
                .ForMember(p => p.Regular_Price, pc => pc.MapFrom(pc => pc.Regular_Price))
                .ForMember(p => p.Updated_At, pc => pc.MapFrom(pc => pc.Updated_At))
                .ForMember(p => p.Categories, pc => pc.Ignore());

            CreateMap<ProductCategoryViewModel, CategoryViewModel>()
                .ForMember(c => c.Id, pc => pc.MapFrom(pc => pc.Category_Id))
                .ForMember(c => c.Name, pc => pc.MapFrom(pc => pc.Category_Name))
                .ForMember(c => c.Created_At, pc => pc.MapFrom(pc => pc.Category_Created_At))
                .ForMember(c => c.Products, pc => pc.Ignore());

            _ = CreateMap<IGrouping<int, ProductCategoryViewModel>, CategoryViewModel>()
                .ForMember(c => c.Id, gr => gr.MapFrom(gr => gr.Key))
                .ForMember(c => c.Name, gr => gr.MapFrom(gr => gr.First().Category_Name))
                .ForMember(c => c.Created_At, gr => gr.MapFrom(gr => gr.First().Category_Created_At))
                //.ForMember(c => c.Products, gr => gr.MapFrom(gr => gr.ToList()));
                .ForMember(c => c.Products, gr => gr.MapFrom(gr => gr.First().Product_Name == null ? new List<ProductCategoryViewModel>() : gr.ToList()));

            CreateMap<IGrouping<int, ProductCategoryViewModel>, ProductViewModel>()
                .ForMember(p => p.Id, gr => gr.MapFrom(gr => gr.Key))
                .ForMember(p => p.Name, gr => gr.MapFrom(gr => gr.First().Product_Name))
                .ForMember(p => p.Regular_Price, gr => gr.MapFrom(gr => gr.First().Regular_Price))
                .ForMember(p => p.Discount_Price, gr => gr.MapFrom(gr => gr.First().Discount_Price))
                .ForMember(p => p.Created_At, gr => gr.MapFrom(gr => gr.First().Created_At))
                .ForMember(p => p.Updated_At, gr => gr.MapFrom(gr => gr.First().Updated_At))
                //.ForMember(p => p.Categories, gr => gr.MapFrom(gr => gr.ToList()));
                .ForMember(p => p.Categories, gr => gr.MapFrom(gr => gr.First().Category_Name == null ? new List<ProductCategoryViewModel>() : gr.ToList()));


            CreateMap<CategoryViewModel, Category>();
        }
    }
}
