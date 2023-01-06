using AutoMapper;
using OrderMicroservice.Data.Entities;
using OrderMicroservice.Protos;

namespace OrderMicroservice.Dtos.AutoMapperProfiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderDto, Order>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

        CreateMap<OrderItemDto, OrderItem>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.PromotionCode, opt => opt.MapFrom(src => src.PromotionCode))
            .ForMember(dest => dest.ExtendedGurantee, opt => opt.MapFrom(src => src.ExtendedGurantee));
        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.PromotionCode, opt => opt.MapFrom(src => src.PromotionCode))
            .ForMember(dest => dest.ExtendedGurantee, opt => opt.MapFrom(src => src.ExtendedGurantee));
    }
}