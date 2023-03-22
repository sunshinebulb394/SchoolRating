using System;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using SchoolRating.Controllers;
using SchoolRating.Dto;
using SchoolRating.Models;

namespace SchoolRating.Tests.Controller
{
	public class SchoolControllerTests
	{
        private readonly ApplicationDbContext _context;
        //private readonly RatingsController _ratingRepo;
		private readonly IMapper _mapper;
		public SchoolControllerTests()
		{
			_context = A.Fake<ApplicationDbContext>();
			_mapper = A.Fake<IMapper>();
		}

		[Fact]
		public void SchoolsController_GetSchools_ReturnOk()
		{
			//Arrange
			var schools = A.Fake<ICollection<SchoolDto>>();
			var schoolsList = A.Fake<List<SchoolDto>>();
			A.CallTo(() => _mapper.Map<List<SchoolDto>>(schools)).Returns(schoolsList);
			var controller = new SchoolsController(_context, _mapper);

			//Act
			var result = controller.GetSchools();

			//Assert
			result.Should().NotBeNull();
		}
	}
}

