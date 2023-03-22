using System;
using SchoolRating.Models;

namespace SchoolRating.Interface
{
	public interface IRatingsRepository
	{
        Task<ICollection<Rating>> GetRatingsAsync();
        Task<ICollection<Rating>> GetRatingBySchoolIdAsync(long id);
        Task<Rating> GetRatingAsync(long id);
        bool RatingExist(long id);
        Task<bool> DeleteRatingAsync(Rating rating);
        Task<bool> CreateRatingAsync(Rating rating);
        Task<bool> UpdateRatingAsync(Rating Rating);
        Task<bool> SaveAsync();

    }
}

