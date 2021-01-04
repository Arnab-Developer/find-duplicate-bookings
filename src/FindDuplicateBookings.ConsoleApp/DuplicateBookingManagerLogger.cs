using FindDuplicateBookings.Lib.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FindDuplicateBookings.ConsoleApp
{
    /// <summary>
    /// Class for duplicate booking manager logger.
    /// </summary>
    internal static class DuplicateBookingManagerLogger
    {
        private static readonly Action<ILogger, string, Exception> _deletingDuplicateBooking = LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId(1, nameof(DeletingDuplicateBookings)),
            "Deleting duplicate bookings: {bookingLogString}");

        private static readonly Action<ILogger, Exception> _duplicateBookingsError = LoggerMessage.Define(
            LogLevel.Error,
            new EventId(1, nameof(DuplicateBookingsError)),
            "Duplicate bookings error:");

        /// <summary>
        /// Log duplicate bookings which are found and deleted.
        /// </summary>
        /// <param name="logger">Logger to log duplicate bookings.</param>
        /// <param name="bookings">Duplicate bookings to log.</param>
        public static void DeletingDuplicateBookings(this ILogger logger, IEnumerable<Booking> bookings)
        {
            var bookingLogString = $"{bookings.Count()}\n\n";
            foreach (Booking booking in bookings)
            {
                bookingLogString += $"{booking} \n";
            }
            _deletingDuplicateBooking(logger, bookingLogString, null);
        }

        /// <summary>
        /// Log about duplicate booking deleting error.
        /// </summary>
        /// <param name="logger">Logger to log error.</param>
        /// <param name="ex">Exception to log.</param>
        public static void DuplicateBookingsError(this ILogger logger, Exception ex)
        {
            _duplicateBookingsError(logger, ex);
        }
    }
}
