using System.Net;
using System.Net.Mail;

namespace Application.Message;

public class SendService
{
    SmtpClient _smtpClient;

    public SendService()
    {
        _smtpClient = new SmtpClient("smtp.gmail.com", 587)
        {
            Port = 587,
            Credentials = new NetworkCredential("leadersacademy187@gmail.com", "btij wwta hxkj agto"),
            EnableSsl = true,
            UseDefaultCredentials = false
        };
    }

    public async Task SendEmail(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Email address or password is empty or null. Skipping email sending.");
            return;
        }

        using (MailMessage mail = new MailMessage())
        {
            mail.From = new MailAddress("leadersacademy187@gmail.com", "Leaders Academy"); 
            mail.To.Add(email);
            mail.Subject = "Your Account Password";
            mail.Body = $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Your Account Password</title>
                </head>
                <body>
                    <h1>Hi, Dear Teacher</h1>
                    <p>Your password: {password}</p>
                    <p>Please keep it secure and do not share it with anyone.</p>
                </body>
                </html>";
            mail.IsBodyHtml = true;

            try
            {
                _smtpClient.Send(mail); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
    }
}