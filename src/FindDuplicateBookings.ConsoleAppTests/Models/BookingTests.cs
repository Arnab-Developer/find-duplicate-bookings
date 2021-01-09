using FindDuplicateBookings.Lib.Models;
using Xunit;

namespace FindDuplicateBookings.ConsoleAppTests.Models
{
    public class BookingTests
    {
        [Fact]
        public void BookingModelTest()
        {
            // Arrange.
            Booking booking = new()
            {
                AssignmentId = 15,
                TestInsId = 102,
                SessionId = 452
            };

            // Assert.
            Assert.NotNull(booking);
            Assert.Equal(15, booking.AssignmentId);
            Assert.Equal(102, booking.TestInsId);
            Assert.Equal(452, booking.SessionId);
        }

        [Fact]
        public void BookingModelToStringTest()
        {
            // Arrange.
            Booking booking = new()
            {
                AssignmentId = 15,
                TestInsId = 102,
                SessionId = 452
            };

            // Act.
            string bookingString = booking.ToString();

            // Assert.
            Assert.Equal("AssignmentId 15, TestInsId 102, SessionId 452.", bookingString);
        }
    }
}
