using Postal;
using WebAPI.Models.DTO;
using WebAPI.Models.DTO._Doctor;

namespace WebAPI.Models
{
    public class EmailTemplate: Email
    {
        public string To { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }

        /*
         * --> propiedades necesarias para enviar
         * diferentes tipos de correos
         */
        public enum TypeEmail
        {
            forgotPwd,
            doctorRegister
        }

        public string Title { get; set; }
        public TypeEmail TypeEmailToSend { get; set; }
        public ForgotPasswordRes ForgotPwd { get; set; }
        public Register DoctorRegister { get; set; }
    }
}