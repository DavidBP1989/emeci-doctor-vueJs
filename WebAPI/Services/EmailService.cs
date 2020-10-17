using System;
using System.Threading.Tasks;
using WebAPI.App_Code;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Models.DTO._Doctor;

namespace WebAPI.Services
{
    public class EmailService
    {
        public string To { get; set; }
        public EmailService(string to)
        {
            To = to;
        }

        public async Task SendForgotPwd(ForgotPasswordRes req)
        {
            var email = new EmailTemplate
            {
                To = To,
                //To = "david@univisit.com",
                Bcc = "bustamante24.1989@gmail.com",
                Subject = "EMECI - Recordatorio de contraseña",
                Title = "Protege tu salud y la de tu familia a través de EMECI",
                TypeEmailToSend = EmailTemplate.TypeEmail.forgotPwd,
                ForgotPwd = req
            };

            try
            {
                await email.SendAsync();
            }
            catch (Exception ex)
            {
                Log.Write($"EmailService, SendForgotPwd=> {ex.Message}");
            }
        }

        public async Task SendDoctorRegister(Register req)
        {
            var email = new EmailTemplate
            {
                To = To,
                Bcc = "bustamante24.1989@gmail.com",
                Subject = "EMECI - Registro médico",
                Title = "Registro médico",
                TypeEmailToSend = EmailTemplate.TypeEmail.doctorRegister,
                DoctorRegister = req
            };

            try
            {
                await email.SendAsync();
            }
            catch (Exception ex)
            {
                Log.Write($"EmailService, SendDoctorRegister=> {ex.Message}");
            }
        }
    }
}