using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Text;
using System.IO;

namespace Cote_Cloud.User
{
    public class email
    {
        public string SMTPServer { get; set; }
        private string SMTPUser { get; set; }
        public string SMTPPass { get; set; }
        public string SMTPPort { get; set; }
        public string MailFrom { get; set; }
        public string MailFromName { get; set; }
        public string MailTo { get; set; }
        public string MailSubject { get; set; }
        public List<string> MailRecipients { get; set; }
        public List<string> MailCCRecipients { get; set; }
        public List<string> MailBccRecipients { get; set; }
        public List<string> MailAttachments { get; set; }
        public bool bodyHtml { get; set; }
        public bool informativo { get; set; }
        public string MailMessage { get; set; }
        public string mensaje { get; set; }
        public string contra { get; set; }
        public string usuario { get; set; }
        public email()
        {
            this.SMTPServer = System.Configuration.ConfigurationManager.AppSettings.Get("SMTPServer").ToString();
            this.MailFromName = System.Configuration.ConfigurationManager.AppSettings.Get("SMTPUser").ToString();
            this.SMTPPass = System.Configuration.ConfigurationManager.AppSettings.Get("SMTPPass").ToString();
            this.SMTPPort = System.Configuration.ConfigurationManager.AppSettings.Get("SMTPPort").ToString();
            this.MailFrom = System.Configuration.ConfigurationManager.AppSettings.Get("FromMail").ToString();

            this.MailSubject = String.Empty;
            this.MailRecipients = new List<string>();
            this.MailCCRecipients = new List<string>();
            this.MailBccRecipients = new List<string>();
            this.MailAttachments = new List<string>();
            this.MailMessage = String.Empty;
            this.bodyHtml = true;
            this.informativo = true;
        }
        public void AddRecipient(string sMailRecipient)
        {
            this.MailRecipients.Add(sMailRecipient);
        }
        public void AddCCRecipient(string sMailRecipient)
        {
            this.MailCCRecipients.Add(sMailRecipient);
        }
        public void AddBccRecipient(string sMailRecipient)
        {
            this.MailBccRecipients.Add(sMailRecipient);
        }
        public void AddAttachment(string sAttachLocation)
        {
            this.MailAttachments.Add(sAttachLocation);
        }
        public void ClearRecipients() {
            this.MailRecipients.Clear();
            this.MailCCRecipients.Clear();
            this.MailBccRecipients.Clear();
        }
        public void ClearAttachments() {
            this.MailAttachments.Clear();
        }
        public bool SendMail() {
            try
            {
                SmtpClient smtpcli = new SmtpClient
                {
                    Host = this.SMTPServer,
                    Port = Convert.ToInt32(this.SMTPPort),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(this.MailFrom, this.SMTPPass)
                };
                MailMessage mjs = new MailMessage();
                if (this.MailFromName != string.Empty)
                    mjs.From = new MailAddress(this.MailFrom, this.MailFromName);
                else
                    mjs.From = new MailAddress(this.MailFrom);
                foreach (string sMailCCRecipient in this.MailCCRecipients)
                {
                    mjs.CC.Add(sMailCCRecipient);
                }
                foreach (string sMailBccRecipient in this.MailBccRecipients)
                {
                    mjs.Bcc.Add(sMailBccRecipient);
                }
                foreach (string sMailRecipient in this.MailRecipients)
                {
                    MailAddress email = new MailAddress(sMailRecipient);
                    mjs.ReplyToList.Add(email);
                }
                if(this.informativo)
                {
                    StringBuilder replacements = new StringBuilder(File.ReadAllText(HttpContext.Current.Server.MapPath("~/informativo.txt")));
                    replacements.Replace("{mensaje}", this.mensaje);
                    this.MailMessage = replacements.ToString();
                }
                else
                {
                    StringBuilder replacements = new StringBuilder(File.ReadAllText(HttpContext.Current.Server.MapPath("~/emaildesign.txt")));
                    replacements.Replace("{mensaje}", this.mensaje);
                    replacements.Replace("{contra}", this.usuario);
                    replacements.Replace("{user}", this.contra);
                    this.MailMessage = replacements.ToString();
                }
                mjs.To.Add(new MailAddress(this.MailTo));
                mjs.IsBodyHtml = this.bodyHtml;
                mjs.Body = this.MailMessage;
                mjs.Subject = this.MailSubject;
                smtpcli.Send(mjs);
                smtpcli.Dispose();
                mjs.Dispose();
                return true;
            }
            catch (Exception a)
            {
                HttpContext.Current.Session["errorCorreo"] = a.Message.ToString();
                return false;
            }
        }
    }
}