using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolRating.Interface;
using SchoolRating.Models;

namespace SchoolRating.Repository
{
	public class SchoolRepository : ISchoolsRepository
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;

		public SchoolRepository(ApplicationDbContext context ,IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

        public bool CreateSchool(School school)
        {
           _context.Add(school);
            return Save();
        }

        public bool DeleteSchool(School school)
        {
            _context.Remove(school);
            return Save();
        }

        public School GetSchool(long id)
        {
            return _context.Schools.Include(s => s.Ratings).SingleOrDefault(s => s.SchoolId == id);
        }

        public ICollection<School> GetSchools()
        {
           return _context.Schools.Include(s => s.Ratings).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }



        public bool SchoolExist(long id)
        {
            return (_context.Schools?.Any(e => e.SchoolId == id)).GetValueOrDefault();
        }

        public bool UpdateSchool(School school)
        {
            _context.Update(school);
            return Save();
        }
    }
}

