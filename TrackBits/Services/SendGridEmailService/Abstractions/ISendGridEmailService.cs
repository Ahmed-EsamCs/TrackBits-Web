using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackBits.Services.SendGridEmailService.Abstractions
{
        public interface ISendGridEmailService
    {
        Task<(int StatusCode, string Body)> SendEmailAsyncRaw(string to, string subject, string body);
        Task SendTemplateEmailAsync(string to, string templateId, object templateData);

    }
}