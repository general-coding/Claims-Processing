﻿using claimsprocessing.api.Models;
using AutoMapper;

namespace claimsprocessing.api.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClaimCreateDTO, tbl_claim>()
                .ForMember(dest => dest.claim_user, opt => opt.Ignore()) // prevent navigation assignment
                .ForMember(dest => dest.created_on, opt => opt.MapFrom(_ => DateTime.Now));

            CreateMap<ClaimUpdateDTO, tbl_claim>()
                .ForMember(dest => dest.modified_on, opt => opt.MapFrom(_ => DateTime.Now));

            CreateMap<UserCreateDTO, tbl_user>()
                .ForMember(dest => dest.created_on, opt => opt.MapFrom(_ => DateTime.Now));

            CreateMap<UserUpdateDTO, tbl_user>()
                .ForMember(dest => dest.modified_on, opt => opt.MapFrom(_ => DateTime.Now));
        }
    }
}
