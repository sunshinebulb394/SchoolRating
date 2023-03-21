using System;
using System.Collections.Generic;
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
    public class SchoolsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SchoolsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Schools
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolDto>>> GetSchools()
        {
            
          if (_context.Schools == null)
          {
              return NotFound();
          }

            List<SchoolDto> schools = await (from s in _context.Schools
                                       select new SchoolDto()
                                       {
                                           SchoolId = s.SchoolId,
                                           HeadOfSchool = s.HeadOfSchool,
                                           Address = s.Address,
                                           City = s.City,
                                           fees = s.fees,
                                           Name = s.Name,
                                           OtherInfo = s.OtherInfo,
                                           Religion = s.Religion

                                       }).ToListAsync();

            return schools;

        }

        // GET: api/Schools/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolDto>> GetSchool(long id)
        {
            var school = await _context.Schools.Include(s => s.Ratings)
                .Select(s => new SchoolDto()
                {
                    SchoolId = s.SchoolId,
                    HeadOfSchool = s.HeadOfSchool,
                    Address = s.Address,
                    City = s.City,
                    fees = s.fees,
                    Name = s.Name,
                    OtherInfo = s.OtherInfo,
                    Religion = s.Religion,
                    Ratings = s.Ratings.Select(sc => new RatingsDto()
                    {
                        RatingId = sc.RatingId,
                        Comments = sc.Comments,
                        name = sc.name,
                        Score = sc.Score,
                        SchoolId = sc.SchoolId
                    }).ToList()

                }).SingleOrDefaultAsync(s => s.SchoolId == id);
               
            
            if (school == null)
            {
                return NotFound();
            }


            return school;
        }

      

        // PUT: api/Schools/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchool(long id, SchoolDto schoolDto)
        {
            if (id != schoolDto.SchoolId)
            {
                return BadRequest();
            }

            School? school = await _context.Schools.FindAsync(id);

            if (school == null)
            {
                return NotFound();
            }

            // Map the properties of the SchoolDto object to the corresponding properties of the School entity
            school.HeadOfSchool = schoolDto.HeadOfSchool;
            school.Address = schoolDto.Address;
            school.City = schoolDto.City;
            school.fees = schoolDto.fees;
            school.Name = schoolDto.Name;
            school.OtherInfo = schoolDto.OtherInfo;
            school.Religion = schoolDto.Religion;
   

            _context.Entry(school).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolExists(id))
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


        // POST: api/Schools
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SchoolDto>> PostSchool(SchoolDto schoolDto)
        {
            School school = new School()
            {
                HeadOfSchool = schoolDto.HeadOfSchool,
                Address = schoolDto.Address,
                City = schoolDto.City,
                fees = schoolDto.fees,
                Name = schoolDto.Name,
                OtherInfo = schoolDto.OtherInfo,
                Religion = schoolDto.Religion,
               
            };

            _context.Schools.Add(school);
            await _context.SaveChangesAsync();

            SchoolDto createdSchoolDto = new SchoolDto()
            {
                SchoolId = school.SchoolId,
                HeadOfSchool = school.HeadOfSchool,
                Address = school.Address,
                City = school.City,
                fees = school.fees,
                Name = school.Name,
                OtherInfo = school.OtherInfo,
                Religion = school.Religion,
                
            };

            return CreatedAtAction(nameof(GetSchool), new { id = createdSchoolDto.SchoolId }, createdSchoolDto);
        }


        // DELETE: api/Schools/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchool(long id)
        {
            if (_context.Schools == null)
            {
                return NotFound();
            }
            var school = await _context.Schools.FindAsync(id);
            if (school == null)
            {
                return NotFound();
            }

            _context.Schools.Remove(school);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SchoolExists(long id)
        {
            return (_context.Schools?.Any(e => e.SchoolId == id)).GetValueOrDefault();
        }
    }
}
