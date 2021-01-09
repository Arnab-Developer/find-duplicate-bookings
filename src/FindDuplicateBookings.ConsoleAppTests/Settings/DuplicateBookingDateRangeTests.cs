using FindDuplicateBookings.Lib.Settings;
using System;
using Xunit;

namespace FindDuplicateBookings.ConsoleAppTests.Settings
{
    public class DuplicateBookingDateRangeTests
    {
        [Fact]
        public void DuplicateBookingDateRangeModelTest()
        {
            // Arrange.
            DuplicateBookingDateRange duplicateBookingDateRange = new()
            {
                StartDate = DateTime.UtcNow.AddDays(-10),
                EndDate = DateTime.UtcNow.AddDays(10)
            };

            // Assert.
            Assert.Equal(DateTime.UtcNow.AddDays(-10).Date, duplicateBookingDateRange.StartDate.Date);
            Assert.Equal(DateTime.UtcNow.AddDays(10).Date, duplicateBookingDateRange.EndDate.Date);
        }
    }
}
