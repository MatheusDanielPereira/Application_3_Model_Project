using System;
using System.Runtime.CompilerServices;
using System.IO;
using System.Net.Mail;
using System.Data.Entity.Validation;

namespace YourProjectName_Domain.Helpers
{
    public static class ErrorHelper
    {
        /// <summary>
        /// Returns the error message from the given Exception, including all inner exception's messages. Messages will be separated with BR tag when hmtl = true. It will also send the error message to Metso software development team.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="html"></param>
        /// <param name="CallerFilePath"></param>
        /// <param name="CallerMethodName"></param>
        /// <returns></returns>
        public static string GetAllErrorMessages(Exception ex, bool html, [CallerFilePath]string CallerFilePath = null, [CallerMemberName] string CallerMethodName = null, DbEntityValidationException exval = null)
        {
            var CallerFileName = Path.GetFileNameWithoutExtension(CallerFilePath);

            string ErrorMessage = "[" + CallerFileName + "." + CallerMethodName + "] " + ex.Message;

            Exception Inner = ex.InnerException;

            while (Inner != null)
            {
                ErrorMessage += (html ? "<br />" : " - ") + Inner.Message;
                Inner = Inner.InnerException;
            }

            if (exval != null)
            {
                foreach (DbEntityValidationResult item in exval.EntityValidationErrors)
                {
                    foreach (DbValidationError error in item.ValidationErrors)
                    {
                        ErrorMessage += (html ? "<br />" : " - ") + error.ErrorMessage;
                    }
                }
            }

            if (ex is DbEntityValidationException)
            {
                DbEntityValidationException exval2 = ((DbEntityValidationException)ex);

                if (exval2 != null)
                {
                    foreach (DbEntityValidationResult item in exval2.EntityValidationErrors)
                    {
                        foreach (DbValidationError error in item.ValidationErrors)
                        {
                            ErrorMessage += (html ? "<br />" : " - ") + error.ErrorMessage;
                        }
                    }
                }
            }

            //You can send it by email if needed
            //try
            //{
            //    SendErrorByMail(ErrorMessage);
            //}
            //catch
            //{
            //    ErrorMessage += " - This error message could not be sent to Metso software development team.";
            //}

            return ErrorMessage;
        }

        private static void SendErrorByMail(string ErrorMessage)
        {
            SmtpClient smtpClient = new SmtpClient();
            MailMessage mailMessage = new MailMessage();

            mailMessage.To.Add(new MailAddress("luiz.mariano@metsopartners.com"));
            mailMessage.Subject = "Foundry Monitor - Error information";
            mailMessage.IsBodyHtml = true;

            string MsgBody = "<html><body>";
            MsgBody += "<h2>Error message: </h2>";
            MsgBody += "<br /><span style=\"color:red\">";
            MsgBody += ErrorMessage;
            MsgBody += "</span><br /><br />";
            MsgBody += "</body></html>";

            mailMessage.Body = MsgBody;
            smtpClient.Send(mailMessage);
        }
    }
}
