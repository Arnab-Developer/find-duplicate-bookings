namespace FindDuplicateBookings.Lib.Settings
{
    /// <summary>
    /// Settings for mail sending.
    /// </summary>
    public class MailSettings
    {
        /// <summary>
        /// From address.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// To address.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Email subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Email host.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Email port.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Smtp user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Smtp password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Enable ssl for smtp or not.
        /// </summary>
        public string EnableSsl { get; set; }

        /// <summary>
        /// Create a new object of MailSettings.
        /// </summary>
        public MailSettings()
        {
            From = string.Empty;
            To = string.Empty;
            Subject = string.Empty;
            Host = string.Empty;
            UserName = string.Empty;
            Password = string.Empty;
            EnableSsl = string.Empty;
        }
    }
}
