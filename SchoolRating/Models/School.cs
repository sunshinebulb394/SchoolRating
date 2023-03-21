using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolRating.Models
{
	public class School
	{
		public School()
		{
			Ratings = new List<Rating>();
		}

		[Key]
		public long SchoolId{ get; set; }

		
		public string? Name { get; set; }

		
		public string? City { get; set; }

		
		public string? Address { get; set; }

		
		public string? HeadOfSchool { get; set; }

		
		public double? fees { get; set; }

		
		public string? Religion { get; set; }

		
		public string? OtherInfo { get; set; }

		public IList<Rating> Ratings { get; set; } = default!;

	}
}

