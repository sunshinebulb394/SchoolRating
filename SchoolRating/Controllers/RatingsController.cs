using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolRating.Dto;
using SchoolRating.Models;

namespace SchoolRating.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RatingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Ratings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RatingsDto>>> GetRatings()
        {
          if (_context.Ratings == null)
          {
              return NotFound();
          }
            List<RatingsDto> ratings = await (from r in _context.Ratings
                                              select new RatingsDto()
                                              {
                                                  RatingId = r.RatingId,
                                                  Comments = r.Comments,
                                                  Score = r.Score,
                                                  SchoolId = r.SchoolId,
                                                  name = r.name
                                              }).ToListAsync();

            return ratings;
        }

        // GET: api/Ratings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RatingsDto>> GetRating(long id)
        {
          if (_context.Ratings == null)
          {
              return NotFound();
          }
            var rating = await _context.Ratings.FindAsync(id);

            if (rating == null)
            {
                return NotFound();
            }

            RatingsDto ratings = new RatingsDto()
            {
                SchoolId = rating.SchoolId,
                Comments = rating.Comments,
                name = rating.name,
                Score = rating.Score
            };


            return ratings;
        }

        // PUT: api/Ratings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRating(long id, RatingsDto ratingDto)
        {
            if (id != ratingDto.RatingId)
            {
                return BadRequest();
            }

            Rating? rating = await _context.Ratings.FindAsync(id);

            if (rating == null)
            {
                return NotFound();
            }

            // Map the properties of the RatingsDto object to the corresponding properties of the Rating entity
            rating.Comments = ratingDto.Comments;
            rating.SchoolId = ratingDto.SchoolId;
            rating.Score = ratingDto.Score;
            rating.name = ratingDto.name;

            _context.Entry(rating).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Ratings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rating>> PostRating(RatingsDto ratingDto)
        {
            School? school = await _context.Schools.FirstOrDefaultAsync(s => s.SchoolId == ratingDto.SchoolId);

            if (school == null)
            {
                return NotFound();
            }

            Rating rating = new Rating()
            {
                Comments = ratingDto.Comments,
                Score = ratingDto.Score,
                name = ratingDto.name,
                SchoolId = ratingDto.SchoolId
            };

            school.Ratings.Add(rating);

            await _context.SaveChangesAsync();

            RatingsDto createdRatingsDto = new RatingsDto()
            {
                RatingId = rating.RatingId,
                Comments = rating.Comments,
                name = rating.name,
                SchoolId = rating.SchoolId,
                Score = rating.Score
            };
            



            return CreatedAtAction(nameof(GetRating), new { id = createdRatingsDto.RatingId}, createdRatingsDto);
        }

        // DELETE: api/Ratings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating(long id)
        {
            if (_context.Ratings == null)
            {
                return NotFound();
            }
            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RatingExists(long id)
        {
            return (_context.Ratings?.Any(e => e.RatingId == id)).GetValueOrDefault();
        }
    }
}
