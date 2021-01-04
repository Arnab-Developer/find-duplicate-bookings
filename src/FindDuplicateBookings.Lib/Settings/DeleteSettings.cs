namespace FindDuplicateBookings.Lib.Settings
{
    /// <summary>
    /// Booking data delete settings.
    /// </summary>
    public class DeleteSettings
    {
        /// <summary>
        /// Booking data delete enable or disable.
        /// </summary>
        public string IsDeleteEnable { get; set; }

        /// <summary>
        /// Creates a new object for DeleteSettings.
        /// </summary>
        public DeleteSettings()
        {
            IsDeleteEnable = string.Empty;
        }
    }
}
