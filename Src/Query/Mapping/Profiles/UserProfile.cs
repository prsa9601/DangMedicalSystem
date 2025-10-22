using Application.User.Create;
using AutoMapper;
using Domain.UserAgg;
using Query.User.DTOs;

namespace Query.Mapping.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //CreateMap<UserDto, Domain.UserAgg.User>()
            //     .ForMember(destination => destination.FullName,
            //           opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<Domain.UserAgg.User, UserDto>();

        }
    }
}
