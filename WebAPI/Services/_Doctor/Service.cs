using System;
using System.Linq;
using System.Transactions;
using WebAPI.App_Code;
using WebAPI.Models;
using WebAPI.Models.DTO._Doctor;

namespace WebAPI.Services._Doctor
{
    public class Service: IDisposable
    {
        readonly EmeciEntities Context = new EmeciEntities();

        public void Dispose()
        {
            Context.Dispose();
        }


        /// <summary>
        /// get basic information from the doctor
        /// </summary>
        public Doctor Get(int doctorId)
        {
            return Context.Registro
                .Join(Context.Medico,
                    reg => reg.idRegistro,
                    med => med.IdRegistro,
                    (reg, med) => new { r = reg, m = med })
                .Where(x => x.m.Idmedico == doctorId)
                .Select(x => new Doctor
                {
                    Name = x.r.Nombre + " " + x.r.Apellido,
                    EMECI = x.r.Emeci
                }).FirstOrDefault();
        }

        /// <summary>
        /// change doctor'password
        /// </summary>
        public ChangePwdRes ChangePassword(int doctorId, ChangePwdReq changePwdReq)
        {
            var result = new ChangePwdRes();
            var doctor = Context.Registro
                .Join(Context.Medico,
                    reg => reg.idRegistro,
                    med => med.IdRegistro,
                    (reg, med) => new { r = reg, m = med })
                .Where(x => x.m.Idmedico == doctorId)
                .FirstOrDefault()?.r;

            if (doctor != null)
            {
                if (doctor.clave == changePwdReq.CurrentPassword)
                {
                    try
                    {
                        doctor.clave = changePwdReq.NewPassword;
                        Context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        result.Error = ex.Message;
                        Log.Write($"WebAPI.Services._Doctor, ChangePassword => {ex.Message}");
                    }
                }
                else result.Error = "Contraseña incorrecta";
            } else result.Error = "Error al obtener la información del doctor";
            return result;
        }


        /// <summary>
        /// doctor register
        /// </summary>
        public bool Register(Register req)
        {
            using (var transaction = new TransactionScope())
            {
                try
                {
                    var register = new Registro
                    {
                        Nombre = req.Name,
                        Apellido = req.LastName,
                        Colonia = req.Colony,
                        Domicilio = req.Address,
                        Emails = req.Email,
                        Telefono = req.Phone,
                        TelefonoCel = req.CellPhone,
                        CodigoPostal = req.PostalCode,
                        idEstado = req.State,
                        idCiudad = int.TryParse(req.City, out int intValue) ? intValue : (int?)null,
                        IdPais = "MX",
                        FechaRegistro = DateTime.Now,
                        Tipo = "M",
                        Status = "V",
                        CURP = req.CURP,
                    };

                    Context.Registro.Add(register);
                    Context.SaveChanges();

                    var doctor = new Medico
                    {
                        IdRegistro = register.idRegistro,
                        RFC = req.RFC,
                        TelefonoConsultorio = req.OfficePhone,
                        DomicilioConsultorio = req.OfficeAddress,
                        CertCMCP = req.NoCertification_CMCP,
                        CedulaEspecialidad = req.SpecialtyCertificate,
                        NoRegSSA = req.NoSSA,
                        AgrupacionLocal = req.NameStateSchool,
                        AgrupacionNacional = req.NameStateGrouping,
                        UniversidadEspecialidad = req.UniversitySpecialty,
                        CedulaProfesional = req.NoSEP_ProfessionalCertificate,
                        HospitalResidenciaPediatra = req.ProfessionalResidenceHospital
                    };

                    Context.Medico.Add(doctor);
                    Context.SaveChanges();

                    transaction.Complete();
                }
                catch (Exception ex)
                {
                    Log.Write($"WebAPI.Services._Doctor - Register => ${ex.Message}");
                    return false;
                }
            }

            return true;
        }


        public RegisterInfo GetRegisterInfo(int doctorId)
        {
            var response = new RegisterInfo();
            var doctor = Context.Medico.FirstOrDefault(x => x.Idmedico == doctorId);
            if (doctor != null)
            {
                response.RFC = doctor.RFC;
                response.OfficePhone = doctor.TelefonoConsultorio;
                response.OfficeAddress = doctor.DomicilioConsultorio;
                response.NoCertification_CMCP = doctor.CertCMCP;
                response.SpecialtyCertificate = doctor.CedulaEspecialidad;
                response.NoSSA = doctor.NoRegSSA;
                response.NameStateSchool = doctor.AgrupacionLocal;
                response.NameStateGrouping = doctor.AgrupacionLocal;
                response.UniversitySpecialty = doctor.UniversidadEspecialidad;
                response.NoSEP_ProfessionalCertificate = doctor.CedulaProfesional;
                response.ProfessionalResidenceHospital = doctor.HospitalResidenciaPediatra;

                var register = Context.Registro.FirstOrDefault(x => x.idRegistro == doctor.IdRegistro);
                if (register != null)
                {
                    response.Name = register.Nombre;
                    response.LastName = register.Apellido;
                    response.Colony = register.Colonia;
                    response.Address = register.Domicilio;
                    response.Email = register.Emails;
                    response.Phone = register.Telefono;
                    response.CellPhone = register.TelefonoCel;
                    response.PostalCode = register.CodigoPostal;
                    response.State = register.idEstado;
                    response.City = register.idCiudad.ToString();
                    response.CURP = register.CURP;
                }
            }
            
            return response;
        }


        public bool UpdateRegisterInfo(int doctorId, RegisterInfo req)
        {
            using (var transaction = new TransactionScope())
            {
                try
                {
                    var doctor = Context.Medico.FirstOrDefault(x => x.Idmedico == doctorId);
                    if (doctor != null)
                    {
                        doctor.RFC = req.RFC;
                        doctor.TelefonoConsultorio = req.OfficePhone;
                        doctor.DomicilioConsultorio = req.OfficeAddress;
                        doctor.CertCMCP = req.NoCertification_CMCP;
                        doctor.CedulaEspecialidad = req.SpecialtyCertificate;
                        doctor.NoRegSSA = req.NoSSA;
                        doctor.AgrupacionLocal = req.NameStateSchool;
                        doctor.AgrupacionNacional = req.NameStateGrouping;
                        doctor.UniversidadEspecialidad = req.UniversitySpecialty;
                        doctor.CedulaProfesional = req.NoSEP_ProfessionalCertificate;
                        doctor.HospitalResidenciaPediatra = req.ProfessionalResidenceHospital;

                        Context.SaveChanges();

                        var register = Context.Registro.FirstOrDefault(x => x.idRegistro == doctor.IdRegistro);
                        if (register != null)
                        {
                            register.Nombre = req.Name;
                            register.Apellido = req.LastName;
                            register.Colonia = req.Colony;
                            register.Domicilio = req.Address;
                            register.Emails = req.Email;
                            register.Telefono = req.Phone;
                            register.TelefonoCel = req.CellPhone;
                            register.CodigoPostal = req.PostalCode;
                            register.idEstado = req.State;
                            register.idCiudad = int.TryParse(req.City, out int intValue) ? intValue : (int?)null;
                            register.CURP = req.CURP;

                            Context.SaveChanges();

                            transaction.Complete();
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Write($"WebAPI.Services._Doctor - UpdateRegisterInfo => ${ex.Message}");
                    return false;
                }
            }

            return false;
        }
    }
}