using Application.Services;
using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Tests
{
    [TestClass]
    public class DynamicMapperTests
    {
        private DynamicMapper _dynamicMapper;
        private Dictionary<string, string> _mappingRules;

        [TestInitialize]
        public void Setup()
        {
            _dynamicMapper = new DynamicMapper();
            _mappingRules = new Dictionary<string, string>
            {
                { "Id", "ReservationId" },
                { "GuestName", "CustomerName" },
                { "CheckIn", "ArrivalDate" },
                { "CheckOut", "DepartureDate" }
            };
        }

        [TestMethod]
        public void Map_ValidReservation_ReturnsGoogleReservationModel()
        {
            // Arrange
            var reservation = new Reservation
            {
                Id = 1,
                GuestName = "John Doe",
                CheckIn = new DateTime(2024, 10, 1, 14, 0, 0),
                CheckOut = new DateTime(2024, 10, 5, 11, 0, 0)
            };

            // Act
            var result = _dynamicMapper.Map<Reservation, GoogleReservationModel>(reservation, _mappingRules);

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(1, result.ReservationId);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("John Doe", result.CustomerName);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(new DateTime(2024, 10, 1, 14, 0, 0), result.ArrivalDate);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(new DateTime(2024, 10, 5, 11, 0, 0), result.DepartureDate);
        }

        [TestMethod]
        public void MapBack_ValidGoogleReservationModel_ReturnsReservation()
        {
            // Arrange
            var googleModel = new GoogleReservationModel
            {
                ReservationId = 1,
                CustomerName = "John Doe",
                ArrivalDate = new DateTime(2024, 10, 1, 14, 0, 0),
                DepartureDate = new DateTime(2024, 10, 5, 11, 0, 0)
            };

            // Act
            var result = _dynamicMapper.MapBack<Reservation, GoogleReservationModel>(googleModel, _mappingRules);

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(1, result.Id);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("John Doe", result.GuestName);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(new DateTime(2024, 10, 1, 14, 0, 0), result.CheckIn);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(new DateTime(2024, 10, 5, 11, 0, 0), result.CheckOut);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Map_NullSource_ThrowsArgumentNullException()
        {
            // Act
            _dynamicMapper.Map<Reservation, GoogleReservationModel>(null, _mappingRules);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MapBack_NullTarget_ThrowsArgumentNullException()
        {
            // Act
            _dynamicMapper.MapBack<Reservation, GoogleReservationModel>(null, _mappingRules);
        }
    }
}
