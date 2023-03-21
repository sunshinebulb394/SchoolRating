using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolRating.Models
{
   public class Rating
    {
        [Key]
        public long RatingId { get; set; }

        public String? name { get; set; }

        [Range(1, 5,ErrorMessage = "Score must be between 1 and 5")]
        public int? Score { get; set; }

        public String? Comments { get; set; }

        [ForeignKey("SchoolId")]
        public long SchoolId { get; set; }

        public School? School { get; set; }

    }

}