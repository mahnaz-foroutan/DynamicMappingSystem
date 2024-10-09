using Application.Mappers;
using Domain.Exceptions;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Mappers
{
    [TestFixture]
    public class ReservationMapperTests
    {
        private ReservationMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = new ReservationMapper();
        }

        [Test]
        public void Map_ShouldReturnGoogleReservationModel_WhenGivenReservation()
        {
            var reservation = new Reservation { Id = 1, GuestName = "John Doe", CheckIn = DateTime.Now, CheckOut = DateTime.Now.AddDays(2) };

            var result = _mapper.Map(reservation);

            Assert.IsNotNull(result);
            Assert.AreEqual(reservation.Id, result.ReservationId);
            Assert.AreEqual(reservation.GuestName, result.CustomerName);
        }

        [Test]
        public void MapBack_ShouldReturnReservation_WhenGivenGoogleReservationModel()
        {
            var googleModel = new GoogleReservationModel { ReservationId = 1, CustomerName = "John Doe", ArrivalDate = DateTime.Now, DepartureDate = DateTime.Now.AddDays(2) };

            var result = _mapper.MapBack(googleModel);

            Assert.IsNotNull(result);
            Assert.AreEqual(googleModel.ReservationId, result.Id);
            Assert.AreEqual(googleModel.CustomerName, result.GuestName);
        }

        [Test]
        public void Map_ShouldThrowMappingException_WhenSourceIsNull()
        {
            Assert.Throws<MappingException>(() => _mapper.Map(null));
        }

        [Test]
        public void MapBack_ShouldThrowMappingException_WhenTargetIsNull()
        {
            Assert.Throws<MappingException>(() => _mapper.MapBack(null));
        }
    }
}
