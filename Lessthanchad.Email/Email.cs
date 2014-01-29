using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Lessthanchad.Email
{
  public interface IEmail
  {
    MailAddress From { get; set; }
    List<string> Recipients { get; set; }
    string Subject { get; set; }
    string Body { get; set; }

    void Send();
  }
  public class Email : IEmail
  {
   
    public MailAddress From { get; set; }
    public List<string> Recipients { get; set; }
    public List<string> CC { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public List<Attachment> Attachments { get; set; }
    public EmailSettings Settings { get; set; }
   

    public Email(EmailSettings emailSettings)
    {
      Settings = emailSettings;

      From = new MailAddress(emailSettings.FromEmailAddress, emailSettings.FromDisplayName);
      Attachments = new List<Attachment>();
      Body = String.Empty;
      CC = new List<string>();
      
    }
    public void Send()
    {
      var mail = new MailMessage
      {
        From = From,
        Subject = Subject,
        Body = Body
      };

      foreach (var recipient in Recipients)
      {
        mail.To.Add(new MailAddress(recipient));
      }

      foreach (var cc in CC)
      {
        mail.CC.Add(new MailAddress(cc));
      }

      foreach (var attachment in Attachments)
      {
        mail.Attachments.Add(attachment);
      }


      var alternameView = AlternateView.CreateAlternateViewFromString(Body, new ContentType("text/html"));
      mail.AlternateViews.Add(alternameView);

      var smtpClient = new SmtpClient(Settings.SmtpServer);
      smtpClient.Credentials = new NetworkCredential(Settings.UserName, Settings.Password);

      smtpClient.Send(mail);
    }
  }
}
