using AutoMapper;
using DAL.Models;
using ReservationAPI.DTOs.Auth;
using ReservationAPI.DTOs.Read;
using ReservationAPI.DTOs.Write;

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
            CreateMap<PaymentCreateUpdateDto, Payment>();

            CreateMap<Promotion,PromotionDto>().ReverseMap();
            CreateMap<PromotionCreateUpdateDto, Promotion>();

            CreateMap<Reservation, ReservationDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.ResourceName, opt => opt.MapFrom(src => src.Resource.Name));
            CreateMap<ReservationCreateUpdateDto, Reservation>()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Resource, opt => opt.Ignore());



            CreateMap<Resource,ResourceDto>().ReverseMap();
            CreateMap<ResourceCreateUpdateDto, Resource>();

            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.EscapeRoomName, opt => opt.MapFrom(src => src.EscapeRoom.Name));
            CreateMap<ReviewCreateUpdateDto, Review>();


            CreateMap<Role,RoleDto>().ReverseMap();
            CreateMap<RoleCreateUpdateDto, Role>();

            CreateMap<TimeSlot,TimeSlotDto>().ReverseMap();
            CreateMap<TimeSlotCreateUpdateDto, TimeSlot>();

            //CreateMap<AuthUser,LoginDto>().ReverseMap();
            CreateMap<LoginDto, AuthUser>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // hash manually
                .ForMember(dest => dest.Role, opt => opt.Ignore());        // set manually after mapping


            CreateMap<AuthRole, AuthRoleDto>().ReverseMap();
        }
    }
}
