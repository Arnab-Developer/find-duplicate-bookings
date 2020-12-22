namespace FindDuplicateBookings.Lib.Models
{
    public class Booking
    {
        public int AssignmentId { get; set; }
        public int TestInsId { get; set; }
        public int SessionId { get; set; }

        public override string ToString()
        {
            return $"AssignmentId {AssignmentId}, TestInsId {TestInsId}, SessionId {SessionId}.";
        }
    }
}
