using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolRating.Dto;
using SchoolRating.Interface;
using SchoolRating.Models;

namespace SchoolRating.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly ISchoolsRepository _schoolsReposiory;
        private readonly IMapper _mapper;

        public SchoolsController(ISchoolsRepository schoolsReposiory,IMapper mapper)
        {
            _schoolsReposiory = schoolsReposiory;
            _mapper = mapper;
        }

        // GET: api/Schools
        [HttpGet]
        [ProducesResponseType(200,Type = typeof(IEnumerable<School>))]
        public IActionResult GetSchools()
        {
            var schools = _mapper.Map<List<SchoolDto>>(_schoolsReposiory.GetSchools());


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(schools);

        }




        // GET: api/Schools/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(School))]
        [ProducesResponseType(400)]
        public IActionResult GetSchool(long id)
        {
            if (!_schoolsReposiory.SchoolExist(id))
            {
                return NotFound();
            }
            var school = _mapper.Map<SchoolDto>(_schoolsReposiory.GetSchool(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            return Ok(school);
        }


         
            

      

        // PUT: api/Schools/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult PutSchool([FromRoute]long id,[FromBody] SchoolDto schoolDto)
        {
            if (!_schoolsReposiory.SchoolExist(id)) return NotFound();

            if (schoolDto == null) return BadRequest(ModelState);

            if(id != schoolDto.SchoolId) return BadRequest(ModelState);

            if (!ModelState.IsValid) return BadRequest();

            var school = _mapper.Map<School>(schoolDto);

            if (!_schoolsReposiory.UpdateSchool(school))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // POST: api/Schools
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult PostSchool([FromBody] SchoolDto schoolDto)
        {
            if (schoolDto == null)
                return BadRequest(ModelState);

            var school = _schoolsReposiory.GetSchools()
                .Where(s => s.Name == schoolDto.Name)
                .Where(s => s.Address == schoolDto.Address)
                .Where(s => s.City == schoolDto.City)
                .FirstOrDefault();

            if (school != null)
            {
                ModelState.AddModelError("", "School already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var schoolMap = _mapper.Map<School>(schoolDto);

            if (!_schoolsReposiory.CreateSchool(schoolMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
        




        // DELETE: api/Schools/5
        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteSchool(long id)
        {

            if (!_schoolsReposiory.SchoolExist(id))
            {
                return NotFound();
            }

            var schoolToDelete = _schoolsReposiory.GetSchool(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_schoolsReposiory.DeleteSchool(schoolToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }

       
    }
}
