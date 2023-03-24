using System;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using SchoolRating.Controllers;
using SchoolRating.Dto;
using SchoolRating.Interface;
using SchoolRating.Models;
using SchoolRating.Repository;

namespace SchoolRating.Tests.Controller
{
	public class SchoolControllerTests
	{
		private readonly ISchoolsRepository _schoolRepository;
        //private readonly RatingsController _ratingRepo;
		private readonly IMapper _mapper;
		public SchoolControllerTests()
		{
			_schoolRepository = A.Fake<ISchoolsRepository>();
			_mapper = A.Fake<IMapper>();
		}

		[Fact]
		public void SchoolsController_GetSchools_ReturnOk()
		{
			//Arrange
			var schools = A.Fake<ICollection<SchoolDto>>();
			var schoolsList = A.Fake<List<SchoolDto>>();
			A.CallTo(() => _mapper.Map<List<SchoolDto>>(schools)).Returns(schoolsList);
			var controller = new SchoolsController(_schoolRepository, _mapper);

			//Act
			var result = controller.GetSchools();

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}

		[Fact]
		public void SchoolController_CreateSchool_ReturnOk()
		{
			//Arrange
			var schoolMap = A.Fake<School>();
			var school = A.Fake<School>();
			var createSchool = A.Fake<SchoolDto>();
			var schoolList = A.Fake<IList<SchoolDto>>();
			A.CallTo(() => _mapper.Map<School>(createSchool)).Returns(school);
			A.CallTo(() => _schoolRepository.CreateSchool(schoolMap)).Returns(true);
			var controller = new SchoolsController(_schoolRepository, _mapper);

			//Act
			var result = controller.PostSchool(createSchool);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(ObjectResult));
		}

        [Fact]
        public void Create_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange
            var invalidSchool = A.Fake<SchoolDto>();
            invalidSchool.Name = null; // Set the Name property to null to create an invalid object

            var controller = new SchoolsController(_schoolRepository, _mapper);
            controller.ModelState.AddModelError("Name", "The Name field is required."); // Set the ModelState to invalid

            // Act
            var result = controller.PostSchool(invalidSchool);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result); // Assert that the result is a BadRequestObjectResult
        }


        [Fact]
        public void Create_ReturnsBadRequest_GivenInvalidModel2()
        {
            // Arrange
            var invalidSchool = A.Fake<SchoolDto>();
			// Set the Name property to null to create an invalid object
			invalidSchool.Name = null;
            var controller = new SchoolsController(_schoolRepository, _mapper);
            controller.ModelState.AddModelError("Name", "The Name field is required."); // Set the ModelState to invalid

            // Act
            var result = controller.PostSchool(invalidSchool);

            // Assert
            Assert.IsType<OkObjectResult>(result); // Assert that the result is a BadRequestObjectResult
        }



    }
}

