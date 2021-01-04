using FindDuplicateBookings.Lib.Models;
using FindDuplicateBookings.Lib.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace FindDuplicateBookings.Lib
{
    /// <summary>
    /// Class to send mail.
    /// </summary>
    public class SendMail : ISendMail
    {
        private readonly MailSettings _mailSettings;

        /// <summary>
        /// Create a new object of SendMail.
        /// </summary>
        /// <param name="mailSettings">Settings for mail sending.</param>
        public SendMail(MailSettings mailSettings)
        {
            _mailSettings = mailSettings;
        }

        void ISendMail.SendBookingDeletedMailToCustomer(IEnumerable<Booking> bookings)
        {
            var bookingEmailBody = $"We have found and deleted below duplicate bookings.\n\nDuplicate bookings: {bookings.Count()}\n\n";
            foreach (Booking booking in bookings)
            {
                bookingEmailBody += $"{booking} \n";
            }
            SmtpClient smtpClient = new(_mailSettings.Host, _mailSettings.Port)
            {
                EnableSsl = _mailSettings.EnableSsl == "1",
                Credentials = new NetworkCredential(_mailSettings.UserName, _mailSettings.Password)
            };
            foreach (string to in _mailSettings.To.Split(';'))
            {
                MailMessage mailMessage = new(_mailSettings.From, to, _mailSettings.Subject,
                    bookingEmailBody);
                smtpClient.Send(mailMessage);
            }
        }

        void ISendMail.SendNoBookingDeletedMailToCustomer()
        {
            SmtpClient smtpClient = new(_mailSettings.Host, _mailSettings.Port)
            {
                EnableSsl = _mailSettings.EnableSsl == "1",
                Credentials = new NetworkCredential(_mailSettings.UserName, _mailSettings.Password)
            };
            foreach (string to in _mailSettings.To.Split(';'))
            {
                MailMessage mailMessage = new(_mailSettings.From, to, _mailSettings.Subject,
                    "We have checked duplicate booking and found that there is no duplicate bookings present as of now.");
                smtpClient.Send(mailMessage);
            }
        }
    }
}
