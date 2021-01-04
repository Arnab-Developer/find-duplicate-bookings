using FindDuplicateBookings.Lib.Models;
using FindDuplicateBookings.Lib.Settings;
using System.Collections.Generic;

namespace FindDuplicateBookings.Lib
{
    /// <summary>
    /// Interface for duplicate bookings.
    /// </summary>
    public interface IDuplicateBooking
    {
        /// <summary>
        /// Get duplicate bookings in a date range.
        /// </summary>
        /// <param name="duplicateBookingDateRange">Date range to find duplicate bookings.</param>
        /// <returns>Duplicate bookings.</returns>
        IEnumerable<Booking> GetDuplicateBookings(DuplicateBookingDateRange duplicateBookingDateRange);

        /// <summary>
        /// Delete duplicate bookings.
        /// </summary>
        /// <param name="booking">Duplicate bookings to delete.</param>
        void DeleteDuplicateBookings(Booking booking);
    }
}