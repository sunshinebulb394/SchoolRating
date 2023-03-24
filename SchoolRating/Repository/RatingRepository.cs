using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolRating.Interface;
using SchoolRating.Models;

namespace SchoolRating.Repository
{
	public class RatingRepository : IRatingsRepository
	{
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RatingRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<bool> CreateRatingAsync(Rating rating)
        {
         var school = _context.Schools.SingleOrDefault(r => r.SchoolId == rating.SchoolId);
            school.Ratings.Add(rating);
            return SaveAsync();
        }

        public  Task<bool> DeleteRatingAsync(Rating rating)
        {
            _context.Remove(rating);
            return SaveAsync();
       
        }

        public async Task<Rating> GetRatingAsync(long id)
        {
            return await _context.Ratings.SingleOrDefaultAsync(r => r.RatingId == id);
        }

        public async Task<ICollection<Rating>> GetRatingBySchoolIdAsync(long id)
        {
            return await _context.Ratings.Where(r => r.SchoolId == id).ToListAsync();
        }

        public async Task<ICollection<Rating>> GetRatingsAsync()
        {
            return await _context.Ratings.ToListAsync();
        }

      
        public bool RatingExist(long id)
        {
            return (_context.Ratings?.Any(e => e.RatingId == id)).GetValueOrDefault();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public Task<bool> UpdateRatingAsync(Rating rating)
        {
            _context.Update(rating);
            return SaveAsync();
        }
    }
}

