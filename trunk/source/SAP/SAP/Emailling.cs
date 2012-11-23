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
using System.Globalization;
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


        private string GetTemplateforDS(DataSet Ds,string TemplatePath,string title)
        {
            string l_Rs = "";
            try
            { 
                string l_PathTemplate = string.Empty;

                if (TemplatePath.Trim().Equals(string.Empty))
                {
                    l_Rs = string.Format("Template is empty");
                }
                else
                {
                    if (Ds.Tables.Count < 2 || Ds.Tables[0].Rows.Count < 1 || Ds.Tables[1].Rows.Count < 1)
                        l_Rs = string.Format("No Data");
                    else
                    {
                        l_Rs = File.ReadAllText(TemplatePath);

                        //-----------header-----------------
                        DataRow dr = Ds.Tables[0].Rows[0];
                        l_Rs = l_Rs.Replace("<@TITLE>", title + "-" + dr["DocEntry"].ToString());
                        l_Rs = l_Rs.Replace("<@User>", dr["CardName"].ToString());
                        l_Rs = l_Rs.Replace("<@Code>", dr["CardCode"].ToString());
                        l_Rs = l_Rs.Replace("<@Name>", dr["CardName"].ToString());
                        l_Rs = l_Rs.Replace("<@RefNo>", dr["NumAtCard"].ToString());
                        CultureInfo ivC = new System.Globalization.CultureInfo("es-US");

                        l_Rs = l_Rs.Replace("<@PostingDate>", String.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(dr["DocDate"], ivC)));
                        l_Rs = l_Rs.Replace("<@DocDate>", String.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(dr["DocDueDate"], ivC)));
                        l_Rs = l_Rs.Replace("<@DueDate>", String.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(dr["TaxDate"], ivC)));

                        //-----------line-----------------
                        string str = "";
                        for (int i = 0; i < Ds.Tables[1].Rows.Count; i++)
                        {
                            str = str + "<tr>";
                            str = str + "<td style=\"border: thin solid #008080;\"><@ItemCode"+i.ToString()+"></td>";
                            str = str + "<td style=\"border: thin solid #008080;\"><@ItemName" + i.ToString() + "></td>";
                            str = str + "<td align=\"right\" style=\"border: thin solid #008080;\"><@Quantity" + i.ToString() + "></td>";
                            str = str + "<td align=\"right\" style=\"border: thin solid #008080;\"><@Price" + i.ToString() + "></td>";
                            str = str + "</tr>";
                        }
                        l_Rs = l_Rs.Replace("<@ITEMLINEHERE>", str);
                        int j = 0;
                        foreach (DataRow dr1 in Ds.Tables[1].Rows)
                        {
                            l_Rs = l_Rs.Replace("<@ItemCode"+j.ToString()+">", dr1["ItemCode"].ToString());
                            l_Rs = l_Rs.Replace("<@ItemName" + j.ToString() + ">", dr1["Dscription"].ToString());
                            l_Rs = l_Rs.Replace("<@Quantity" + j.ToString() + ">", dr1["Quantity"].ToString());
                            l_Rs = l_Rs.Replace("<@Price" + j.ToString() + ">", dr1["Price"].ToString());
                            j++;
                        }
                        //-----------footer-----------------
                        double t =double.Parse( dr["DocTotal"].ToString()) - double.Parse( dr["VatSum"].ToString());
                        GeneralFunctions gf = new GeneralFunctions();
                        gf.FormatNumeric(t.ToString(), "SumDec");
                        l_Rs = l_Rs.Replace("<@SubTotal>",gf.FormatNumeric(t.ToString(), "SumDec"));
                        l_Rs = l_Rs.Replace("<@AmountDue>", gf.FormatNumeric(dr["DocTotal"].ToString(), "SumDec")); 
                        l_Rs = l_Rs.Replace("<@Tax>",gf.FormatNumeric( dr["VatSum"].ToString(), "SumDec"));
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
            }
            return l_Rs;
        }
        public string SendMailByDS(DataSet ds, string ToEmailList,string Subject,string TemplatePath)
        {
            string l_Rs = "Email Sent!";
            string l_SenderEmail = "truongthaithuy@gmail.com";
            if (ToEmailList.Trim().Equals(string.Empty))
            {
                ToEmailList = l_SenderEmail;
            }
           
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.From = new MailAddress(l_SenderEmail, "AUTO MAILER");
            msg.To.Add(ToEmailList);
            msg.Subject = Subject;
            msg.Body = GetTemplateforDS(ds, TemplatePath,"SALES ORDER");
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
                l_Rs = "<span style='color:#CC0033'>Send error:<br /><br />" + ex.Message + "</span>";
                return l_Rs;
            }
            return l_Rs;
        }
    }
}