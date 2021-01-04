using System;

namespace FindDuplicateBookings.Lib.Settings
{
    /// <summary>
    /// Date range to find duplicate bookings.
    /// </summary>
    public class DuplicateBookingDateRange
    {
        /// <summary>
        /// Start date.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// End date.
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
