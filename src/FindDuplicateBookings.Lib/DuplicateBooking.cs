using Dapper;
using FindDuplicateBookings.Lib.Models;
using FindDuplicateBookings.Lib.Settings;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FindDuplicateBookings.Lib
{
    /// <summary>
    /// Class for duplicate bookings.
    /// </summary>
    public class DuplicateBooking : IDuplicateBooking
    {
        private readonly string _connectionString;

        /// <summary>
        /// Create a new object for DuplicateBooking.
        /// </summary>
        /// <param name="connectionString">Database connection string.</param>
        public DuplicateBooking(string connectionString)
        {
            _connectionString = connectionString;
        }

        IEnumerable<Booking> IDuplicateBooking.GetDuplicateBookings(DuplicateBookingDateRange duplicateBookingDateRange)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);
            string command = string.Format(Commands.GetDuplicateBookings,
                duplicateBookingDateRange.StartDate, duplicateBookingDateRange.EndDate);
            IEnumerable<Booking> bookings = connection.Query<Booking>(command);
            return bookings;
        }

        void IDuplicateBooking.DeleteDuplicateBookings(Booking booking)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);
            string command = string.Format(Commands.DeleteDuplicateBookings,
                booking.AssignmentId, booking.TestInsId, booking.SessionId);
            connection.Execute(command, commandTimeout: 120);
        }
    }
}
