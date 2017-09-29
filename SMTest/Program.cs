
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SMTest
{
    class Program
    {
           static void Main(string[] args)
        {

            Email newEmail = GetUserInput();

            bool answer = AskToSendEmail();

             if (answer == true)
             {
                 SendEmail(newEmail);

             }
             else
                 Environment.Exit(0);

            Console.ReadKey();
        }
       
        public static Email GetUserInput()
        {
            Email letter = new Email();
            string email = "";
            while ((string.IsNullOrEmpty(email)) || !(email.Contains("@")))
            {
                Console.WriteLine("Enter the e-mail: ");
                email = Console.ReadLine();
            }
            letter.Address = email;

            Console.WriteLine("Subject for your e-mail.");
            string subject = Console.ReadLine();
            if (string.IsNullOrEmpty(subject))
            {
                subject = "No Subject";
            }
            letter.Subject = subject;

            Console.WriteLine("Message: ");
            string message = Console.ReadLine();
            letter.Message = message;
            
            return letter;

        }
        public static bool AskToSendEmail()
        {
            bool success = false;
            string answer = "";
            while (!(answer.Equals("yes", StringComparison.InvariantCultureIgnoreCase)) && !(answer.Equals("y", StringComparison.InvariantCultureIgnoreCase))
               && !(answer.Equals("n", StringComparison.InvariantCultureIgnoreCase)) && !(answer.Equals("no", StringComparison.InvariantCultureIgnoreCase)))
            {
                Console.WriteLine("Do you want to send the email? [y/n]");
                answer = Console.ReadLine();
            }
            string confirm = answer;
            if (confirm.Equals("yes") || (confirm.Equals("y")))
            {
                success = true;
            }

            return success;
        }
        public static void SendEmail(Email letter)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["smtpServer"]);

                mail.From = new MailAddress(ConfigurationManager.AppSettings["emailSender"]);

                mail.To.Add(letter.Address);
                mail.Subject = letter.Subject;
                mail.Body = letter.Message;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["username"],
                                                                            ConfigurationManager.AppSettings["password"]);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

    }
}
