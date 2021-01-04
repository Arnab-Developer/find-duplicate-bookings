namespace FindDuplicateBookings.ConsoleApp
{
    /// <summary>
    /// Interface to manage duplicate bookings finding and deleting.
    /// </summary>
    internal interface IDuplicateBookingManager
    {
        /// <summary>
        /// Find and delete duplicate bookings.
        /// </summary>
        void DeleteDuplicateBookingsAndSendMail();
    }
}