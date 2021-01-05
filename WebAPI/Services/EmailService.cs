using System;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;
using WebAPI.App_Code;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Models.DTO._Doctor;
using WebAPI.Models.DTO._Patient;

namespace WebAPI.Services
{
    public class EmailService
    {
        private const string Bcc = "bustamante24.1989@gmail.com";
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
                Bcc = Bcc,
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
                Bcc = Bcc,
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

        public async Task SendPatientRegister(NewPatientReq req, MemoryStream positions)
        {
            var email = new EmailTemplate
            {
                To = To,
                Bcc = Bcc,
                Subject = "EMECI - Registro de paciente",
                Title = "Registro de paciente",
                TypeEmailToSend = EmailTemplate.TypeEmail.patientRegister,
                PatientReq = req
            };

            email.Attach(new Attachment(positions, "PosicionesDeAcceso.pdf"));

            try
            {
                await email.SendAsync();
            }
            catch (Exception ex)
            {
                Log.Write($"EmailService, SendPatientRegister=> {ex.Message}");
            }
        }
    }
}