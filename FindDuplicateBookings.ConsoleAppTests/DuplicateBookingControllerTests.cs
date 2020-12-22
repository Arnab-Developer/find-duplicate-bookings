using FindDuplicateBookings.ConsoleApp;
using FindDuplicateBookings.Lib;
using FindDuplicateBookings.Lib.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FindDuplicateBookings.ConsoleAppTests
{
    public class DuplicateBookingControllerTests
    {
        private readonly Mock<IDuplicateBooking> _duplicateBookingMock;
        private readonly Mock<IOptionsMonitor<DuplicateBookingDateRange>> _duplicateBookingDateRangeOptionsMonitorMock;
        private readonly Mock<ILogger<DuplicateBookingController>> _duplicateBookingControllerLoggerMock;
        private DuplicateBookingController? _duplicateBookingController;

        public DuplicateBookingControllerTests()
        {
            _duplicateBookingMock = new Mock<IDuplicateBooking>();
            _duplicateBookingDateRangeOptionsMonitorMock = new Mock<IOptionsMonitor<DuplicateBookingDateRange>>();
            _duplicateBookingControllerLoggerMock = new Mock<ILogger<DuplicateBookingController>>();
        }

        [Fact]
        public void DeleteDuplicateBookingsAndSendMail_GivenData_ReturnData()
        {
            // Arrange.
            DuplicateBookingDateRange duplicateBookingDateRange = new()
            {
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now
            };

            IEnumerable<Booking> bookings = new List<Booking>
            {
                new Booking { AssignmentId = 10, TestInsId = 20, SessionId = 30 },
                new Booking { AssignmentId = 100, TestInsId = 200, SessionId = 300 }
            };

            _duplicateBookingDateRangeOptionsMonitorMock
                .Setup(d => d.CurrentValue)
                .Returns(duplicateBookingDateRange);

            _duplicateBookingMock
                .Setup(d => d.GetDuplicateBookings(duplicateBookingDateRange))
                .Returns(bookings)
                .Verifiable();

            _duplicateBookingMock
                .Setup(d => d.DeleteDuplicateBookings(bookings.First()))
                .Verifiable();

            _duplicateBookingController = new DuplicateBookingController(_duplicateBookingMock.Object,
                _duplicateBookingDateRangeOptionsMonitorMock.Object, _duplicateBookingControllerLoggerMock.Object);

            // Act.
            _duplicateBookingController.DeleteDuplicateBookingsAndSendMail();

            // Assert.
            _duplicateBookingMock.Verify();
        }

        [Fact]
        public void DeleteDuplicateBookingsAndSendMail_GivenEmpty_ReturnEmpty()
        {
            // Arrange.
            DuplicateBookingDateRange duplicateBookingDateRange = new()
            {
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now
            };

            IEnumerable<Booking> bookings = new List<Booking>();

            _duplicateBookingDateRangeOptionsMonitorMock
                .Setup(d => d.CurrentValue)
                .Returns(duplicateBookingDateRange);

            _duplicateBookingMock
                .Setup(d => d.GetDuplicateBookings(duplicateBookingDateRange))
                .Returns(bookings)
                .Verifiable();

            _duplicateBookingController = new DuplicateBookingController(_duplicateBookingMock.Object,
                _duplicateBookingDateRangeOptionsMonitorMock.Object, _duplicateBookingControllerLoggerMock.Object);

            // Act.
            _duplicateBookingController.DeleteDuplicateBookingsAndSendMail();

            // Assert.
            _duplicateBookingMock.Verify();
        }

        [Fact]
        public void DeleteDuplicateBookingsAndSendMail_GivenException_ReturnException()
        {
            // Arrange.
            DuplicateBookingDateRange duplicateBookingDateRange = new()
            {
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now
            };

            IEnumerable<Booking> bookings = new List<Booking>();

            _duplicateBookingDateRangeOptionsMonitorMock
                .Setup(d => d.CurrentValue)
                .Returns(duplicateBookingDateRange);

            _duplicateBookingMock
                .Setup(d => d.GetDuplicateBookings(duplicateBookingDateRange))
                .Throws<NullReferenceException>();

            _duplicateBookingController = new DuplicateBookingController(_duplicateBookingMock.Object,
                _duplicateBookingDateRangeOptionsMonitorMock.Object, _duplicateBookingControllerLoggerMock.Object);

            _duplicateBookingMock.Verify();

            // Act and assert.
            Assert.Throws<NullReferenceException>(() => _duplicateBookingController.DeleteDuplicateBookingsAndSendMail());
        }
    }
}
