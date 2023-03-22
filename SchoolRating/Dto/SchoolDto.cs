using System;
using SchoolRating.Models;

namespace SchoolRating.Dto
{
	public class SchoolDto
	{
        public long SchoolId { get; set; }


        public string? Name { get; set; }


        public string? City { get; set; }


        public string? Address { get; set; }


        public string? HeadOfSchool { get; set; }


        public double? fees { get; set; }


        public string? Religion { get; set; }


        public string? OtherInfo { get; set; }

        public IList<RatingsDto>? Ratings { get; set; } 
    }
}

