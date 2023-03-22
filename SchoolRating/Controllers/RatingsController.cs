using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolRating.Dto;
using SchoolRating.Models;
using AutoMapper;
using SchoolRating.Interface;

namespace SchoolRating.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingsRepository _ratingsRepository;
        private readonly IMapper _mapper;


        public RatingsController(IRatingsRepository ratingsRepository, IMapper mapper)
        {
            _ratingsRepository = ratingsRepository;
            _mapper = mapper;
        }

        // GET: api/Ratings
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Rating>))]
        public async Task<IActionResult> GetRatings()
        {
            var ratings = _mapper.Map<List<RatingsDto>>(await _ratingsRepository.GetRatingsAsync());


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(ratings);

        }

        // GET: api/Ratings/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Rating))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRatingBySchool(long id)
        {
            if (!_ratingsRepository.RatingExist(id))
            {
                return NotFound();
            }
            var rating = _mapper.Map<List<RatingsDto>>(await _ratingsRepository.GetRatingBySchoolIdAsync(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            return Ok(rating);
        }

        // PUT: api/Ratings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutRating([FromRoute] long id, [FromBody] RatingsDto ratingsDto)
        {
            if (!_ratingsRepository.RatingExist(id))  return NotFound();

            if (ratingsDto == null) return BadRequest(ModelState);

            if (id != ratingsDto.SchoolId) return BadRequest(ModelState);

            if (!ModelState.IsValid) return BadRequest();

            var rating = _mapper.Map<Rating>(ratingsDto);

            if (!await _ratingsRepository.UpdateRatingAsync(rating))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // POST: api/Ratings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostRating([FromBody] RatingsDto ratingsDto)
        {
            if (ratingsDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ratingMap = _mapper.Map<Rating>(ratingsDto);

            if (!await _ratingsRepository.CreateRatingAsync(ratingMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        // DELETE: api/Ratings/5
        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteRating(long id)
        {

            if (!_ratingsRepository.RatingExist(id))
            {
                return NotFound();
            }

            var ratingToDelete = await _ratingsRepository.GetRatingAsync(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _ratingsRepository.DeleteRatingAsync(ratingToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }
    }
}
