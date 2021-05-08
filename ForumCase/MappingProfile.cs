using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumCase
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Review, ReviewDto>();
            CreateMap<UserForCreationDto, User>();
            CreateMap<ReviewForCreationDto, Review>();
            CreateMap<ReviewForUpdateDto, Review>();
        }
    }
}
