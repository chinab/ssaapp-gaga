using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Mail;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace SAP
{
    public class Emailling
    {
        private string GetTemplateEmail(string UserName, string CardName, string Information, string url, string urlBP)
        {
            string l_PathTemplate = string.Empty;
            string l_Rs = string.Empty;
            l_PathTemplate =  "C:\\EmailTemplate.htm";

            if (l_PathTemplate.Trim().Equals(string.Empty))
            {
                l_Rs = string.Format("Dear: {0} \\n Business Parter Name: {1} \\n Information: {2}", UserName, CardName, Information);
            }
            else
            {
                l_Rs = File.ReadAllText(l_PathTemplate);
                l_Rs = l_Rs.Replace("<@User>", UserName);
                l_Rs = l_Rs.Replace("<@CardName>", CardName);
                l_Rs = l_Rs.Replace("<@Information>", Information);
                l_Rs = l_Rs.Replace("<@url>", url);
                l_Rs = l_Rs.Replace("<@url1>", urlBP);
            }
            return l_Rs;
        }

        public string SendMail(string UserName, string CardName, string Information, string ToEmail, string url, string urlBP)
        {
            string l_Rs = "";
            string l_SenderEmail = "truongthaithuy@gmail.com";
            if (ToEmail.Trim().Equals(string.Empty))
            {
                ToEmail = l_SenderEmail;
            }
            else
            {
                ToEmail = ToEmail + "," + l_SenderEmail;
            }

            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.From = new MailAddress(l_SenderEmail,"AUTO MAILER");
            msg.To.Add(ToEmail);
            msg.Subject = "SBO WEB Information";
            msg.Body = GetTemplateEmail(UserName, CardName, Information, url, urlBP);
            msg.IsBodyHtml = true;
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 0;
                client.Credentials = new NetworkCredential(l_SenderEmail, "KHONGbiet");
                client.Send(msg);
            }
            catch (SmtpException ex)
            {
                l_Rs = "<span style='color:#CC0033'>Xảy ra lỗi khi Gởi mail!!<br /><br />" + ex.Message + "</span>";
                return l_Rs;
            }
            return l_Rs;
        }
    }
}