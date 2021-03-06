﻿using AutoMapper;

using SportSquareDTOs;
using SportSquare.Models;

namespace SportSquare.MVP.App_Start.AutomapperProfiles
{
    public class VenueProfiles : Profile
    {
        public VenueProfiles()
            : base()
        {
            this.CreateMap<Venue, VenueDTO>()
                .ForMember(dest => dest.VenueTypes, opt => opt.MapFrom(v => v.VenueTypes))
                .ForMember(dest => dest.RatingAvarage, opt => opt.ResolveUsing<RatingResolverGeneric>());
            this.CreateMap<VenueType, VenueTypeDTO>()
              .ForMember(x => x.Name, opt => opt.MapFrom(v => v.Name));
        }
    }
}