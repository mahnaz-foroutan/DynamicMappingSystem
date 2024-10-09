using Application.Interfaces;
using Application.Services;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsController
{
    
    public class ReservationControllerTests
    {
        private ReservationController _controller;
        private Mock<IDynamicMapper> _mockDynamicMapper;
        private Dictionary<string, string> _mappingRules;

        
        public  ReservationControllerTests()
        {
            _mockDynamicMapper = new Mock<IDynamicMapper>();
            _controller = new ReservationController(_mockDynamicMapper.Object);

            _mappingRules = new Dictionary<string, string>
            {
                { "Id", "ReservationId" },
                { "GuestName", "CustomerName" },
                { "CheckIn", "ArrivalDate" },
                { "CheckOut", "DepartureDate" }
            };
        }

        [Fact]
        public void MapToGoogle_ValidReservation_ReturnsGoogleReservationModel()
        {
            // Arrange
            var reservation = new Reservation
            {
                Id = 1,
                GuestName = "John Doe",
                CheckIn = new DateTime(2024, 10, 1),
                CheckOut = new DateTime(2024, 10, 5)
            };

            var googleModel = new GoogleReservationModel
            {
                ReservationId = 1,
                CustomerName = "John Doe",
                ArrivalDate = new DateTime(2024, 10, 1),
                DepartureDate = new DateTime(2024, 10, 5)
            };

            _mockDynamicMapper.Setup(m => m.Map<Reservation, GoogleReservationModel>(reservation, _mappingRules)).Returns(googleModel);

            // Act
            var result = _controller.MapToGoogle(reservation);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(googleModel, okResult.Value);

        }

        [Fact]
        public void MapFromGoogle_ValidGoogleReservationModel_ReturnsReservation()
        {
            // Arrange
            var googleModel = new GoogleReservationModel
            {
                ReservationId = 1,
                CustomerName = "John Doe",
                ArrivalDate = new DateTime(2024, 10, 1),
                DepartureDate = new DateTime(2024, 10, 5)
            };

            var reservation = new Reservation
            {
                Id = 1,
                GuestName = "John Doe",
                CheckIn = new DateTime(2024, 10, 1),
                CheckOut = new DateTime(2024, 10, 5)
            };

            _mockDynamicMapper.Setup(m => m.MapBack<Reservation, GoogleReservationModel>(googleModel, _mappingRules)).Returns(reservation);

            // Act
            var result = _controller.MapFromGoogle(googleModel);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(reservation, okResult.Value);

         
        }

        [Fact]
        public void MapToGoogle_NullReservation_ReturnsBadRequest()
        {
            // Act
            var result = _controller.MapToGoogle(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);

        }

        [Fact]
        public void MapFromGoogle_NullGoogleModel_ReturnsBadRequest()
        {
            // Act
            var result = _controller.MapFromGoogle(null) ;

            // Assert
           
            Assert.IsType<BadRequestObjectResult>(result.Result);
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

      
    }
}
