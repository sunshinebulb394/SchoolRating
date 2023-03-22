using System;
using AutoMapper;
using SchoolRating.Dto;
using SchoolRating.Models;

namespace SchoolRating.Mapper
{
	public class MappingProfiles:Profile
	{
		public MappingProfiles()
		{
			CreateMap<School, SchoolDto>();
			CreateMap<Rating, RatingsDto>();
			CreateMap<RatingsDto, Rating>();
			CreateMap<SchoolDto, School>();
		}
	}
}

