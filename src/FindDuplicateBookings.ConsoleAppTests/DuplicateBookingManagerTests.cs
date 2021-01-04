using FindDuplicateBookings.ConsoleApp;
using FindDuplicateBookings.Lib;
using FindDuplicateBookings.Lib.Models;
using FindDuplicateBookings.Lib.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FindDuplicateBookings.ConsoleAppTests
{
    public class DuplicateBookingManagerTests
    {
        private readonly Mock<IDuplicateBooking> _duplicateBookingMock;
        private readonly Mock<ISendMail> _sendMailMock;
        private readonly Mock<IOptionsMonitor<DuplicateBookingDateRange>> _duplicateBookingDateRangeOptionsMonitorMock;
        private readonly Mock<IOptionsMonitor<DeleteSettings>> _deleteSettingsOptionsMonitorMock;
        private readonly Mock<IOptionsMonitor<ExecutionSettings>> _executionSettingsOptionsMonitorMock;
        private readonly Mock<ILogger<DuplicateBookingManager>> _duplicateBookingControllerLoggerMock;
        private IDuplicateBookingManager? _duplicateBookingManager;

        public DuplicateBookingManagerTests()
        {
            _duplicateBookingMock = new Mock<IDuplicateBooking>();
            _sendMailMock = new Mock<ISendMail>();
            _duplicateBookingDateRangeOptionsMonitorMock = new Mock<IOptionsMonitor<DuplicateBookingDateRange>>();
            _deleteSettingsOptionsMonitorMock = new Mock<IOptionsMonitor<DeleteSettings>>();
            _executionSettingsOptionsMonitorMock = new Mock<IOptionsMonitor<ExecutionSettings>>();
            _duplicateBookingControllerLoggerMock = new Mock<ILogger<DuplicateBookingManager>>();
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

            _sendMailMock
                .Setup(m => m.SendBookingDeletedMailToCustomer(bookings))
                .Verifiable();

            _sendMailMock
                .Setup(m => m.SendNoBookingDeletedMailToCustomer());

            _deleteSettingsOptionsMonitorMock
                .Setup(m => m.CurrentValue)
                .Returns(new DeleteSettings { IsDeleteEnable = "true" });

            _executionSettingsOptionsMonitorMock
                .Setup(m => m.CurrentValue)
                .Returns(new ExecutionSettings { ExecutionFrequency = 10 });

            _duplicateBookingManager = new DuplicateBookingManager(_duplicateBookingMock.Object, _sendMailMock.Object,
                _duplicateBookingDateRangeOptionsMonitorMock.Object, _deleteSettingsOptionsMonitorMock.Object,
                _duplicateBookingControllerLoggerMock.Object);

            // Act.
            _duplicateBookingManager.DeleteDuplicateBookingsAndSendMail();

            // Assert.
            _duplicateBookingMock.Verify();
            _sendMailMock.Verify();
            _sendMailMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void DeleteDuplicateBookingsAndSendMail_GivenIsDeleteFalse_CallNoDelete()
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
                .Setup(d => d.DeleteDuplicateBookings(bookings.First()));

            _sendMailMock
                .Setup(m => m.SendBookingDeletedMailToCustomer(bookings))
                .Verifiable();

            _sendMailMock
                .Setup(m => m.SendNoBookingDeletedMailToCustomer());

            _deleteSettingsOptionsMonitorMock
                .Setup(m => m.CurrentValue)
                .Returns(new DeleteSettings { IsDeleteEnable = "false" });

            _executionSettingsOptionsMonitorMock
                .Setup(m => m.CurrentValue)
                .Returns(new ExecutionSettings { ExecutionFrequency = 10 });

            _duplicateBookingManager = new DuplicateBookingManager(_duplicateBookingMock.Object, _sendMailMock.Object,
                _duplicateBookingDateRangeOptionsMonitorMock.Object, _deleteSettingsOptionsMonitorMock.Object,
                _duplicateBookingControllerLoggerMock.Object);

            // Act.
            _duplicateBookingManager.DeleteDuplicateBookingsAndSendMail();

            // Assert.            
            _duplicateBookingMock.Verify();
            _duplicateBookingMock.VerifyNoOtherCalls();
            _sendMailMock.Verify();
            _sendMailMock.VerifyNoOtherCalls();
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

            _duplicateBookingMock
                .Setup(d => d.DeleteDuplicateBookings(new Booking { AssignmentId = 10, TestInsId = 20, SessionId = 30 }));

            _sendMailMock
                .Setup(m => m.SendNoBookingDeletedMailToCustomer())
                .Verifiable();

            _sendMailMock
                .Setup(m => m.SendBookingDeletedMailToCustomer(bookings));

            _deleteSettingsOptionsMonitorMock
                .Setup(m => m.CurrentValue)
                .Returns(new DeleteSettings { IsDeleteEnable = "true" });

            _executionSettingsOptionsMonitorMock
                .Setup(m => m.CurrentValue)
                .Returns(new ExecutionSettings { ExecutionFrequency = 10 });

            _duplicateBookingManager = new DuplicateBookingManager(_duplicateBookingMock.Object, _sendMailMock.Object,
                _duplicateBookingDateRangeOptionsMonitorMock.Object, _deleteSettingsOptionsMonitorMock.Object,
                _duplicateBookingControllerLoggerMock.Object);

            // Act.
            _duplicateBookingManager.DeleteDuplicateBookingsAndSendMail();

            // Assert.
            _duplicateBookingMock.Verify();
            _duplicateBookingMock.VerifyNoOtherCalls();
            _sendMailMock.Verify();
            _sendMailMock.VerifyNoOtherCalls();
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

            _duplicateBookingManager = new DuplicateBookingManager(_duplicateBookingMock.Object, _sendMailMock.Object,
                _duplicateBookingDateRangeOptionsMonitorMock.Object, _deleteSettingsOptionsMonitorMock.Object,
                _duplicateBookingControllerLoggerMock.Object);

            _duplicateBookingMock.Verify();

            // Act and assert.
            Assert.Throws<NullReferenceException>(() => _duplicateBookingManager.DeleteDuplicateBookingsAndSendMail());
        }
    }
}
