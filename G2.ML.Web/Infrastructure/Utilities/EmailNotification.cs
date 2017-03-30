using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace G2.ML.Web.Infrastructure.Utilities
{
    public class EmailNotification
    {
        public EmailNotification()
        {
            To = Web.Common.GetWebAppSettingParam("Email_To");
            CC = Web.Common.GetWebAppSettingParam("Email_CC");
            BCC = Web.Common.GetWebAppSettingParam("Email_Bcc");
            From = string.Empty;
        }

        public string To { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> Attachements { get; set; }

        public bool Send()
        {
            bool _success = false;

            if (string.IsNullOrEmpty(To))
            {
                throw new ArgumentNullException("To");
            }            
            if (string.IsNullOrEmpty(Subject))
            {
                throw new ArgumentNullException("Subject");
            }
            if (string.IsNullOrEmpty(Body))
            {
                throw new ArgumentNullException("Body");
            }

            try
            {
                using (MailMessage _email = new MailMessage())
                {
                    _email.Subject = Subject;
                    _email.Body = Body;

                    if (!string.IsNullOrEmpty(From))
                    {
                        _email.From = new MailAddress(From);
                    }

                    ExtractEmailID(To).All(e => { _email.To.Add(e); return true; });
                    ExtractEmailID(CC).All(e => { _email.CC.Add(e); return true; });
                    ExtractEmailID(BCC).All(e => { _email.Bcc.Add(e); return true; });

                    /*Add attachements*/
                    if (Attachements != null)
                    {
                        Attachements.All(a =>
                                {
                                    if (File.Exists(a))
                                    {
                                        _email.Attachments.Add(new Attachment(a));
                                    }
                                    return true;
                                });
                    }

                    _email.IsBodyHtml = true;

                    using (SmtpClient _smtpClient = new SmtpClient())
                    {
                        _smtpClient.Send(_email);
                    }
                    _success = true;
                }
            }
            catch (Exception ex)
            {
                _success = false;
                throw new Frameworks.Core.BaseException("E000100", "Unable to send email", ex);
            }

            return _success;
        }

        private IEnumerable<string> ExtractEmailID(string emailID)
        {
            return emailID.Split(',').Where(e => !string.IsNullOrEmpty(e));
        }
    }
}