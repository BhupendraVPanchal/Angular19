
using System.Net.Mail;


namespace Helper_CL.custom
{
    public class _MailManager
    {

        private static string smtp_server = _site_config.GetConfigValue("smtp_server");
        private static string smtp_port = _site_config.GetConfigValue("smtp_port");
        private static string smtp_ssl = _site_config.GetConfigValue("smtp_ssl");
        private static string smtp_from_email = _site_config.GetConfigValue("smtp_from_email");
        private static string smtp_from_email_name = _site_config.GetConfigValue("smtp_from_email");
        private static string smtp_from_email_password = _site_config.GetConfigValue("smtp_from_email_password");

        #region "Smtp Client/From Address"
        public static SmtpClient getSmtpClient()
        {
           
            SmtpClient smtp = new SmtpClient(smtp_server);

            if (smtp_from_email != "")
            {
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential(smtp_from_email, smtp_from_email_password);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
            }
            if (smtp_ssl == "1")
            {

                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtp.TargetName = "STARTTLS/smtp.office365.com";

                smtp.EnableSsl = true; //commented for testing
                //smtp.EnableSsl = false; //uncommented for testing
            }
            else
            {
                smtp.EnableSsl = false;
            }
            if (smtp_port != "")
            {
                smtp.Port = Convert.ToInt16(smtp_port);
            }

            return smtp;

        }
        #endregion


        #region getMail Message 3 overloads
        private static MailMessage getMailMessage(string strTO, string strSubject, bool IsBodyHtml)
        {
            MailMessage msg = new MailMessage(new MailAddress(smtp_from_email, smtp_from_email_name), new MailAddress(strTO));
            msg.IsBodyHtml = IsBodyHtml;
            msg.Subject = strSubject;
            return msg;
        }

        private static MailMessage getMailMessage(string strTO, string strSubject, string strBody, bool IsBodyHtml)
        {
            MailMessage msg = new MailMessage(new MailAddress(smtp_from_email, smtp_from_email_name), new MailAddress(strTO));
            msg.IsBodyHtml = IsBodyHtml;
            msg.Body = strBody;
            msg.Subject = strSubject;
            return msg;
        }

        private static MailMessage getMailMessage(string strTO, string CC, string strSubject, string strBody, bool IsBodyHtml, Dictionary<string, string> Attachments = null)
        {
            MailMessage msg;
            if (strTO.Contains(","))
            {
                string[] strTOs = strTO.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                msg = new MailMessage(new MailAddress(smtp_from_email, smtp_from_email_name), new MailAddress(strTOs[0]));

                for (int i = 1; i < strTOs.Length; i++)
                {
                    msg.To.Add(strTOs[i].Trim());
                }
            }
            else
            {
                msg = new MailMessage(new MailAddress(smtp_from_email, smtp_from_email_name), new MailAddress(strTO));
            }
            msg.IsBodyHtml = IsBodyHtml;
            msg.Subject = strSubject;
            msg.Body = strBody;
            if (CC.Trim() != "")
            {
                string[] CCID = CC.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string str in CCID)
                {
                    if (str == "") continue;
                    msg.CC.Add(str.Trim());
                }
            }


            return msg;
        }

        private static MailMessage getMailMessage(string strTO, string CC, string strSubject, string strBody, bool IsBodyHtml,
            string ActivationKey, string CustCode,
            Dictionary<string, string> Attachments = null)
        {
            MailMessage msg;
            if (strTO.Contains(","))
            {
                string[] strTOs = strTO.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                msg = new MailMessage(new MailAddress(smtp_from_email, smtp_from_email_name), new MailAddress(strTOs[0]));

                for (int i = 1; i < strTOs.Length; i++)
                {
                    msg.To.Add(strTOs[i].Trim());
                }
            }
            else
            {
                msg = new MailMessage(new MailAddress(smtp_from_email, smtp_from_email_name), new MailAddress(strTO));
            }
            msg.IsBodyHtml = IsBodyHtml;
            msg.Subject = strSubject;
            msg.Body = strBody;
            if (CC.Trim() != "")
            {
                string[] CCID = CC.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string str in CCID)
                {
                    if (str == "") continue;
                    msg.CC.Add(str.Trim());
                }
            }
            if (Attachments != null)
            {
                foreach (var item in Attachments)
                {
                    Attachment attachment = new Attachment(item.Value);
                    if (item.Key != null && item.Key != "")
                    {
                        attachment.Name = item.Key;
                    }
                    msg.Attachments.Add(attachment);
                }
            }

            return msg;
        }

        #endregion


        #region SendMail 3 overloads
        public static void SendMail(string strTO, string strSubject, bool IsBodyHtml)
        {
            try
            {
                SendMail(getSmtpClient(), getMailMessage(strTO, strSubject, IsBodyHtml));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void SendMail(string strTO, string strSubject, string strBody, bool IsBodyHtml = true)
        {
            try
            {
                SendMail(getSmtpClient(), getMailMessage(strTO, strSubject, strBody, IsBodyHtml));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void SendMail(string strTO, string CC, string strSubject, string strBody, bool IsBodyHtml, Dictionary<string, string> Attachments = null)
        {
            try
            {
                SendMail(getSmtpClient(), getMailMessage(strTO, CC, strSubject, strBody, IsBodyHtml, Attachments));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SendForMail(string strTO, string CC, string strSubject, string strBody, bool IsBodyHtml, string ActivationKey, string CustCode, Dictionary<string, string> Attachments = null)
        {
            try
            {
                SendMail(getSmtpClient(), getMailMessage(strTO, CC, strSubject, strBody, IsBodyHtml, ActivationKey, CustCode, Attachments));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void SendMail(SmtpClient smtp, MailMessage msg)
        {
            try
            {
                smtp.Send(msg);
            }
            catch (SmtpFailedRecipientException smex)
            {
                // StaticMethods.LogException("MailManager", "SendMail", msg.ToString(), smex.ToString());
                //smex.StatusCode =   SmtpStatusCode.
                //smex.
                // DAL.logException(smex, "SendMail");
                throw smex;
            }
            catch (SmtpException smex)
            {
                //   StaticMethods.LogException("MailManager", "SendMail", msg.ToString(), smex.ToString());
                //  DAL.logException(smex, "SendMail");
                throw smex;
            }
            catch (Exception ex)
            {
                //   DAL.logException(ex, "SendMail");
                // StaticMethods.LogException("MailManager", "SendMail", msg.ToString(), ex.ToString());
                throw ex;
            }
        }
        #endregion
    }
}
