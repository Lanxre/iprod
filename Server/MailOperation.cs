using System;
using System.Net;
using System.Net.Mail;
using Server.Entity;

namespace Server
{
    public static class MailOperation
    {
        public static void SendMail(Users user)
        {
            try
            {
                using var mail = new MailMessage {From = new MailAddress("tat4vit7@gmail.com")};
                mail.To.Add(user.UserMail);
                mail.Subject = "Служба социальной защиты.";
                mail.Body = "<td class= 'm_-4310583515329030051mpt-40 m_-4310583515329030051mpx-20' style = 'padding-top:80'px;padding-left:85px; padding-right: 85px'> <table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                            "<tbody> <tr><td style='font-size: 0pt; line-height:0pt; text-align:left; padding-bottom:'25px'> </td> </tr> <tr> " +
                            $"<td style='font-size:36px;line-height:42px;font-family:'Motiva Sans',Helvetica,Arial,sans-serif;text-align:left;padding-bottom:30px;color:#0d0d0d;font-weight:bold'> Здравствуйте,{user.UserLogin}!</td> </tr> <tr>" +
                            "<td class='m_-4310583515329030051mfz-28 m_-4310583515329030051mpr-0' style='font-size:36px;font-family:Motiva Sans,Helvetica,Arial,sans-serif;text-align:left;font-weight:normal;min-width:auto!important;line-height:130%;color:#eeeeee;padding-bottom:10px;padding-right:100px'>" +
                            $"<strong style = 'color:#0d0d0d' > УСПЕШНАЯ РЕГЕСТРАЦИЯ </strong> </td> </tr> <tr> " +
                            $"<td class='m_-4310583515329030051mfz-16' style='font-size:18px;font-family:Motiva Sans,Helvetica,Arial,sans-serif;text-align:left;min-width:auto!important;line-height:130%;color:#0d0d0d;padding-bottom:35px'>Вы успешно зарегестрировались, сохраните ваш пароль: {user.UserPassword}</td> </tr> </tbody></table></td>";
                mail.IsBodyHtml = true;

                using var smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("tat4vit7@gmail.com", "!vitalij%2002&FagroN-"),
                    EnableSsl = true
                };
                smtp.Send(mail);
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка отправки сообщения на почту" + user.UserMail);
                throw;
            }
        }
    }
}