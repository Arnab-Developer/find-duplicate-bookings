using FindDuplicateBookings.Lib;
using FindDuplicateBookings.Lib.Models;
using FindDuplicateBookings.Lib.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace FindDuplicateBookings.ConsoleApp
{
    /// <summary>
    /// Class to manage duplicate bookings finding and deleting.
    /// </summary>
    internal class DuplicateBookingManager : IDuplicateBookingManager
    {
        private readonly IDuplicateBooking _duplicateBooking;
        private readonly ISendMail _sendMail;
        private readonly ILogger<DuplicateBookingManager> _logger;
        private readonly DuplicateBookingDateRange _duplicateBookingDateRange;
        private readonly DeleteSettings _deleteSettings;

        /// <summary>
        /// Create a new object of DuplicateBookingManager.
        /// </summary>
        /// <param name="duplicateBooking">Duplicate booking finding and deleting functionality.</param>
        /// <param name="sendMail">Send mail functionality.</param>
        /// <param name="duplicateBookingDateRangeOptionsMonitor">Duplicate booking date range settings.</param>
        /// <param name="deleteSettingsOptionsMonitor">Delete settings.</param>
        /// <param name="logger">Logger for logging.</param>
        public DuplicateBookingManager(
            IDuplicateBooking duplicateBooking,
            ISendMail sendMail,
            IOptionsMonitor<DuplicateBookingDateRange> duplicateBookingDateRangeOptionsMonitor,
            IOptionsMonitor<DeleteSettings> deleteSettingsOptionsMonitor,
            ILogger<DuplicateBookingManager> logger)
        {
            _duplicateBooking = duplicateBooking;
            _sendMail = sendMail;
            _logger = logger;
            _duplicateBookingDateRange = duplicateBookingDateRangeOptionsMonitor.CurrentValue;
            _deleteSettings = deleteSettingsOptionsMonitor.CurrentValue;
        }

        void IDuplicateBookingManager.DeleteDuplicateBookingsAndSendMail()
        {
            IEnumerable<Booking> bookings = _duplicateBooking.GetDuplicateBookings(_duplicateBookingDateRange);
            _logger.DeletingDuplicateBookings(bookings);
            if (bookings is null || !bookings.Any())
            {
                _sendMail.SendNoBookingDeletedMailToCustomer();
                return;
            }
            if (_deleteSettings.IsDeleteEnable == "true")
            {
                foreach (Booking booking in bookings)
                {
                    _duplicateBooking.DeleteDuplicateBookings(booking);
                }
            }
            _sendMail.SendBookingDeletedMailToCustomer(bookings);
        }
    }
}
