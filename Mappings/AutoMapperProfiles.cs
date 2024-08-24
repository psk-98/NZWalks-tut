using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NZWalks.Dtos.Difficulty;
using NZWalks.Dtos.Region;
using NZWalks.Dtos.Walk;
using NZWalks.Models.Domain;

namespace NZWalks.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Region, RegionDto>().ReverseMap();
        CreateMap<CreateRegionFromRequestDto, Region>().ReverseMap();
        CreateMap<AddWalkFromRequestDto, Walk>().ReverseMap();
        CreateMap<Walk, WalkDto>().ReverseMap();
        CreateMap<Difficulty, DifficultyDto>().ReverseMap();
        CreateMap<UpdateWalkFromRequestDto, Walk>().ReverseMap();
    }

}
