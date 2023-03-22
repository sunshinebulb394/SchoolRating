using System;
using SchoolRating.Models;

namespace SchoolRating.Interface
{
	public interface ISchoolsRepository
	{
		ICollection<School> GetSchools();
		School GetSchool(long id);
		bool SchoolExist(long id);
		bool DeleteSchool(School school);
		bool CreateSchool(School school);
		bool UpdateSchool(School school);
		bool Save();

	}
}

