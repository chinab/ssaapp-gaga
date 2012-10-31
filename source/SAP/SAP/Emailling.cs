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
        private string GetTemplateEmail(string arg_ParentName, string arg_UserName, string arg_PassWord)
        {
            string l_PathTemplate = string.Empty;
            string l_Rs = string.Empty;

            l_PathTemplate = "/EmailTemplate.htm";

            if (l_PathTemplate.Trim().Equals(string.Empty))
            {
                l_Rs = string.Format("Xin chào phụ huynh: {0} \\n Tên đăng nhập: {1} \\n Mật khẩu: {2}", arg_ParentName, arg_UserName, arg_PassWord);
            }
            else
            {
                l_Rs = File.ReadAllText(l_PathTemplate);
                l_Rs = l_Rs.Replace("<@ParentName>", arg_ParentName);
                l_Rs = l_Rs.Replace("<@UserName>", arg_UserName);
                l_Rs = l_Rs.Replace("<@Password>", arg_PassWord);
            }
            return l_Rs;
        }

        public string SendMail(string arg_ParentName, string arg_ParentEmail, string arg_UserName, string arg_PassWord)
        {
            string l_Rs = "";
            string l_SenderEmail = "admin@condanglamgi.com";
            if (arg_ParentEmail.Trim().Equals(string.Empty))
            {
                arg_ParentEmail = l_SenderEmail;
            }
            else
            {
                arg_ParentEmail = arg_ParentEmail + "," + l_SenderEmail;
            }

            NetworkCredential loginInfo = new NetworkCredential(l_SenderEmail, "QZ2HrA<&");
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage(l_SenderEmail, arg_ParentEmail,
                    "SAP WEB, inform to " + arg_ParentName, 
                        GetTemplateEmail(arg_ParentName, arg_UserName, arg_PassWord));
            msg.IsBodyHtml = true;
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = loginInfo;
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