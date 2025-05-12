using AutoMapper;
using ReservationAPI.DTOs;
using DAL.Models;

namespace ReservationAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User,UserDto>().ReverseMap(); ;
            CreateMap<User, UserCreateUpdateDto>().ReverseMap();
            CreateMap<Payment, PaymentDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name));
            CreateMap<Promotion,PromotionDto>().ReverseMap();
            CreateMap<Reservation,ReservationDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.ResourceName, opt => opt.MapFrom(src => src.Resource.Name));
            CreateMap<Resource,ResourceDto>().ReverseMap();
            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.EscapeRoomName, opt => opt.MapFrom(src => src.EscapeRoom.Name));
            CreateMap<Role,RoleDto>().ReverseMap();
            CreateMap<TimeSlot,TimeSlotDto>().ReverseMap();
        }
    }
}
