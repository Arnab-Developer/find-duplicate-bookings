namespace FindDuplicateBookings.Lib.Models
{
    /// <summary>
    /// Model for booking.
    /// </summary>
    public class Booking
    {
        /// <summary>
        /// Assignment id.
        /// </summary>
        public int AssignmentId { get; set; }

        /// <summary>
        /// Test instance id.
        /// </summary>
        public int TestInsId { get; set; }

        /// <summary>
        /// Session id.
        /// </summary>
        public int SessionId { get; set; }

        /// <summary>
        /// Overrides the ToString() method for friendly output.
        /// </summary>
        /// <returns>Friendly string output.</returns>
        public override string ToString()
        {
            return $"AssignmentId {AssignmentId}, TestInsId {TestInsId}, SessionId {SessionId}.";
        }
    }
}
