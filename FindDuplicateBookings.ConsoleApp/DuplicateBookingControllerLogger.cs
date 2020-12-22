using FindDuplicateBookings.Lib.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FindDuplicateBookings.ConsoleApp
{
    internal static class DuplicateBookingControllerLogger
    {
        private static readonly Action<ILogger, string, Exception> _deletingDuplicateBooking = LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId(1, nameof(DeletingDuplicateBookings)),
            "Deleting duplicate bookings: {bookingLogString}");

        private static readonly Action<ILogger, Exception> _duplicateBookingsError = LoggerMessage.Define(
            LogLevel.Error,
            new EventId(1, nameof(DuplicateBookingsError)),
            "Duplicate bookings error:");

        public static void DeletingDuplicateBookings(this ILogger logger, IEnumerable<Booking> bookings)
        {
            var bookingLogString = $"{bookings.Count()}\n\n";
            foreach (Booking booking in bookings)
            {
                bookingLogString += $"{booking} \n";
            }
            _deletingDuplicateBooking(logger, bookingLogString, null);
        }

        public static void DuplicateBookingsError(this ILogger logger, Exception ex)
        {
            _duplicateBookingsError(logger, ex);
        }
    }
}
