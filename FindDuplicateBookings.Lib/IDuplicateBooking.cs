using FindDuplicateBookings.Lib.Models;
using System.Collections.Generic;

namespace FindDuplicateBookings.Lib
{
    public interface IDuplicateBooking
    {
        IEnumerable<Booking> GetDuplicateBookings(DuplicateBookingDateRange duplicateBookingDateRange);

        void DeleteDuplicateBookings(Booking booking);
    }
}