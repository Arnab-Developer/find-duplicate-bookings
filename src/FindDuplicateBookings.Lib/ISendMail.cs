using FindDuplicateBookings.Lib.Models;
using System.Collections.Generic;

namespace FindDuplicateBookings.Lib
{
    /// <summary>
    /// Interface to send mail.
    /// </summary>
    public interface ISendMail
    {
        /// <summary>
        /// Send mail to customer with booking details.
        /// </summary>
        /// <param name="bookings">Booking details to be sent through mail.</param>
        void SendBookingDeletedMailToCustomer(IEnumerable<Booking> bookings);

        /// <summary>
        /// Send mail about no booking found to delete.
        /// </summary>
        void SendNoBookingDeletedMailToCustomer();
    }
}