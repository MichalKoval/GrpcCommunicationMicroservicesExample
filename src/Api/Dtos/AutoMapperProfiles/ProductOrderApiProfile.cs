using Api.Protos;
using AutoMapper;

namespace Api.Dtos.AutoMapperProfiles
{
    public class ProductOrderApiProfile : Profile
    {
        public ProductOrderApiProfile()
        {
            CreateMap<OrderDto, OrderResult>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));
            CreateMap<OrderItemDto, OrderItemResult>()
                .ForMember(dest => dest.ProductInfo, opt => opt.MapFrom(src => new ProductResult { Id = src.ProductId }))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.PromotionCode, opt => opt.MapFrom(src => src.PromotionCode))
                .ForMember(dest => dest.ExtendedGurantee, opt => opt.MapFrom(src => src.ExtendedGurantee));

            CreateMap<ProductDto, ProductResult>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size))
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews));

            CreateMap<ProductReviewDto, ProductReviewResult>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.StartRating, opt => opt.MapFrom(src => src.StarRating));
        }
    }
}
