using HospitalApp.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApp.Service
{
    public class ServiceCode
    {
        public List<vwPatient> GetAllPatients()
        {
            try
            {
                using(HospitalEntities5 context = new HospitalEntities5())
                {
                    List<vwPatient> list = new List<vwPatient>();
                    list = (from p in context.vwPatients select p).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exeption" + ex.Message.ToString());
                return null;
            }
        }
        
        public Patient AddPatient(Patient patient)
        {
            bool uniqueUser = CheckUserName(patient.Username);
            try
            {
                using(HospitalEntities5 context = new HospitalEntities5())
                {
                    if(patient.PatientId==0)
                    {
                        if (uniqueUser)
                        {
                            Patient newPatient = new Patient();
                            newPatient.Fullname = patient.Fullname;
                            newPatient.PatientJMBG = patient.PatientJMBG;
                            newPatient.NumInsurce = patient.NumInsurce;
                            newPatient.Username = patient.Username;
                            newPatient.PatientPassword = HashPasswordHelper.HashPassword(patient.PatientPassword);
                            context.Patients.Add(newPatient);
                            context.SaveChanges();
                            patient.PatientId = newPatient.PatientId;
                        }
                        return patient;
                    }
                    else
                    {
                        Patient editPatient = (from p in context.Patients where p.PatientId == patient.PatientId select p).First();
                        editPatient.Fullname = patient.Fullname;
                        editPatient.PatientJMBG = patient.PatientJMBG;
                        editPatient.NumInsurce = patient.NumInsurce;
                        editPatient.Username = patient.Username;                        
                        editPatient.DoctorId = patient.DoctorId;
                        editPatient.PatientId = patient.PatientId;
                        context.SaveChanges();
                        return patient;
                    }
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public List<vwDoctor> GetAllDoctors()
        {
            try
            {
                using (HospitalEntities5 context = new HospitalEntities5())
                {
                    List<vwDoctor> list = new List<vwDoctor>();
                    list = (from p in context.vwDoctors select p).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exeption" + ex.Message.ToString());
                return null;
            }
        }

        public bool CheckUserName(string username)
        {
            using (HospitalEntities5 context = new HospitalEntities5())
            {
                vwDoctor docUser = (from d in context.vwDoctors where d.Username == username select d).FirstOrDefault();
                vwPatient patientUser = (from d in context.vwPatients where d.Username == username select d).FirstOrDefault();
                if (docUser != null || patientUser!=null)
                {
                    return false;
                }
                return true;
            }
        }

        public Doctor AddDoctor(Doctor doctor)
        {
            bool uniqueUser = CheckUserName(doctor.Username);
            try
            {
                using (HospitalEntities5 context = new HospitalEntities5())
                {
                    if (doctor.DoctorId == 0)
                    {
                        if(uniqueUser)
                        {
                            Doctor newDoctor = new Doctor();
                            newDoctor.FullName = doctor.FullName;
                            newDoctor.DoctorJMBG = doctor.DoctorJMBG;
                            newDoctor.BankAccount = doctor.BankAccount;
                            newDoctor.Username = doctor.Username;
                            newDoctor.DoctorPassword = HashPasswordHelper.HashPassword(doctor.DoctorPassword);
                            context.Doctors.Add(newDoctor);
                            context.SaveChanges();
                            doctor.DoctorId = newDoctor.DoctorId;                           
                        }
                        return doctor;
                    }
                    else
                    {
                        Doctor editDoctor = (from p in context.Doctors where p.DoctorId == doctor.DoctorId select p).First();
                        editDoctor.FullName = doctor.FullName;
                        editDoctor.DoctorJMBG = doctor.DoctorJMBG;
                        editDoctor.BankAccount = doctor.BankAccount;
                        editDoctor.Username = doctor.Username;
                        editDoctor.DoctorPassword = doctor.DoctorPassword;
                        editDoctor.DoctorId = doctor.DoctorId;
                        context.SaveChanges();
                        return doctor;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public List<vwRequest> GetAllRequestByPatient(int patientId)
        {
            try
            {
                using (HospitalEntities5 context = new HospitalEntities5())
                {
                    List<vwRequest> list = new List<vwRequest>();
                    list = (from p in context.vwRequests where p.PatientId== patientId select p).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exeption" + ex.Message.ToString());
                return null;
            }
        }

        public vwRequest GetOpenRequestByPatient(int patientId)
        {
            using (HospitalEntities5 context = new HospitalEntities5())
            {
                if(context.vwRequests==null)
                {
                    return null;
                }
                else
                {
                    vwRequest patientRequest = (from r in context.vwRequests where r.PatientId == patientId where r.IsApproved == null select r).FirstOrDefault();
                    return patientRequest;
                }                
            }
        }

        public List<vwRequest> GetAllRequestByDoctor(int doctorId)
        {
            try
            {
                using (HospitalEntities5 context = new HospitalEntities5())
                {
                    List<vwRequest> list = new List<vwRequest>();
                    list = (from p in context.vwRequests where p.DoctorId == doctorId select p).OrderByDescending(x=>x.IsApproved==null).ThenByDescending(x=>x.IsUrgent).ThenByDescending(x=>x.Date).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exeption" + ex.Message.ToString());
                return null;
            }
        }

        public vwRequest AddRequest(vwRequest request)
        {
            try
            {
                using (HospitalEntities5 context = new HospitalEntities5())
                {
                    if (request.RequestId == 0)
                    {
                        Request newRequest = new Request();                        
                        newRequest.Date = request.Date;
                        newRequest.Reason = request.Reason;
                        newRequest.Company = request.Company;
                        newRequest.IsUrgent = request.IsUrgent;
                        newRequest.IsApproved = request.IsApproved;
                        newRequest.PatientId = request.PatientId;
                        newRequest.DoctorId = request.DoctorId;

                        context.Requests.Add(newRequest);
                        context.SaveChanges();

                        request.RequestId = newRequest.RequestId;
                        return request;
                    }
                    else
                    {
                        Request editRequest = (from p in context.Requests where p.RequestId == request.RequestId select p).First();
                        editRequest.Date = request.Date;
                        editRequest.Reason = request.Reason;
                        editRequest.Company = request.Company;
                        editRequest.IsUrgent = request.IsUrgent;
                        editRequest.IsApproved = request.IsApproved;
                        editRequest.PatientId = request.PatientId;
                        editRequest.DoctorId = request.DoctorId;
                        editRequest.RequestId = request.RequestId; 

                        context.SaveChanges();
                        return request;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public void DeleteRequest(int requestId)
        {
            try
            {
                using (HospitalEntities5 context = new HospitalEntities5())
                {
                    Request resultToDelete = (from r in context.Requests where r.RequestId == requestId select r).First();
                    if(resultToDelete.IsApproved!=null)
                    {
                        context.Requests.Remove(resultToDelete);
                        context.SaveChanges();
                    }                    
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        public Doctor LoginDoctor(string username, string password)
        {
            password = HashPasswordHelper.HashPassword(password);
            try
            {
                using(HospitalEntities5 context = new HospitalEntities5())
                {
                    Doctor doctor = (from d in context.Doctors 
                                     where d.Username.Equals(username) where d.DoctorPassword.Equals(password) select d).First();
                    return doctor;
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public Patient LoginPatient(string username, string password)
        {
            password = HashPasswordHelper.HashPassword(password);
            try
            {
                using (HospitalEntities5 context = new HospitalEntities5())
                {
                    Patient patient = (from p in context.Patients
                                     where p.Username.Equals(username)
                                     where p.PatientPassword.Equals(password)
                                     select p).First();
                    return patient;
                }              
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
    }
}
