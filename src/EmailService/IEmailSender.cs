using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService
{
    public interface IEmailSender
    {
        /// <summary>
        /// Send email for oneclicksocial media users.
        /// </summary>
        /// <param name="message">message to be sent to user. Would be the password url reset</param>
        void SendEmail(Message message);

        /// <summary>
        /// Send email asynchronously for oneclicksocial media users.
        /// </summary>
        /// <param name="message">message to be sent to user. Would be the password url reset</param>
        /// <returns></returns>
        Task SendEmailAsync(Message message);
    }

}
