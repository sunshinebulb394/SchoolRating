using System;
using SchoolRating.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolRating.Dto
{
	public class RatingsDto
	{
        public long RatingId { get; set; }

        public String? name { get; set; }

        public int? Score { get; set; }

        public String? Comments { get; set; }

        public long SchoolId { get; set; }

       
    }
}

