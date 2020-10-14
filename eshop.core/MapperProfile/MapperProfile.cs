using AutoMapper;
using eshop.core.ViewModels;
using System.Linq;

namespace eshop.core.MapperProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
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


            CreateMap<IGrouping<int, ProductCategoryViewModel>, ProductViewModel>().
                ForMember(a => a.Id, b => b.Ignore());

            CreateMap<IGrouping<int, ProductCategoryViewModel>, CategoryViewModel>()
                .ForMember(a => a.Id, b => b.MapFrom(src => src.Key))
                .ForMember(a => a.Name, b => b.Ignore());
        }
    }
}
