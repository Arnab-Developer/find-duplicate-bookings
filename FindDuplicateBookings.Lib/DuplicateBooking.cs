using Dapper;
using FindDuplicateBookings.Lib.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FindDuplicateBookings.Lib
{
    public class DuplicateBooking : IDuplicateBooking
    {
        private readonly string _connectionString;

        public DuplicateBooking(string connectionString)
        {
            _connectionString = connectionString;
        }

        IEnumerable<Booking> IDuplicateBooking.GetDuplicateBookings(DuplicateBookingDateRange duplicateBookingDateRange)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);
            string command = string.Format(Commands.GET_DUPLICATE_BOOKING,
                duplicateBookingDateRange.StartDate, duplicateBookingDateRange.EndDate);
            IEnumerable<Booking> bookings = connection.Query<Booking>(command);
            return bookings;
        }

        void IDuplicateBooking.DeleteDuplicateBookings(Booking booking)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);
            string command = string.Format(Commands.DELETE_DUPLICATE_BOOKING,
                booking.AssignmentId, booking.TestInsId, booking.SessionId);
            connection.Execute(command);
        }
    }
}
