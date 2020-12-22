using FindDuplicateBookings.Lib;
using FindDuplicateBookings.Lib.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace FindDuplicateBookings.ConsoleApp
{
    internal class DuplicateBookingController
    {
        private readonly IDuplicateBooking _duplicateBooking;
        private readonly ILogger _logger;
        private readonly DuplicateBookingDateRange _duplicateBookingDateRange;

        public DuplicateBookingController(IDuplicateBooking duplicateBooking,
            IOptionsMonitor<DuplicateBookingDateRange> optionsMonitor, ILogger<DuplicateBookingController> logger)
        {
            _duplicateBooking = duplicateBooking;
            _logger = logger;
            _duplicateBookingDateRange = optionsMonitor.CurrentValue;
        }

        public void DeleteDuplicateBookingsAndSendMail()
        {
            IEnumerable<Booking> bookings = _duplicateBooking.GetDuplicateBookings(_duplicateBookingDateRange);
            _logger.DeletingDuplicateBookings(bookings);
            if (bookings is null || !bookings.Any())
            {
                return;
            }
            foreach (Booking booking in bookings)
            {
                _duplicateBooking.DeleteDuplicateBookings(booking);
            }
        }
    }
}
