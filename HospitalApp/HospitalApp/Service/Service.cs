using HospitalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApp.Service
{
    public class Service
    {
        List<vwPatient> GetAllPatients()
        {
            try
            {
                using(HospitalEntities context = new HospitalEntities())
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
        
        vwPatient AddPatient(vwPatient patient)
        {
            try
            {
                using(HospitalEntities context = new HospitalEntities())
                {
                    if(patient.PatientId==0)
                    {
                        Patient newPatient = new Patient();
                        newPatient.Fullname = patient.Fullname;
                        newPatient.PatientJMBG = patient.PatientJMBG;
                        newPatient.NumInsurce = patient.NumInsurce;
                        newPatient.Username = patient.Username;
                        newPatient.PatientPassword = patient.Username;
                        context.Patients.Add(newPatient);
                        context.SaveChanges();
                        patient.PatientId = newPatient.PatientId;
                        return patient;
                    }
                    else
                    {
                        Patient editPatient = (from p in context.Patients where p.PatientId == patient.PatientId select p).First();
                        editPatient.Fullname = patient.Fullname;
                        editPatient.PatientJMBG = patient.PatientJMBG;
                        editPatient.NumInsurce = patient.NumInsurce;
                        editPatient.Username = patient.Username;
                        editPatient.PatientPassword = patient.Username;
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

        List<vwDoctor> GetAllDoctors()
        {
            try
            {
                using (HospitalEntities context = new HospitalEntities())
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

        vwDoctor AddDoctor(vwDoctor doctor)
        {
            try
            {
                using (HospitalEntities context = new HospitalEntities())
                {
                    if (doctor.DoctorId == 0)
                    {
                        Doctor newDoctor = new Doctor();
                        newDoctor.Name = doctor.Name;
                        newDoctor.Lastname = doctor.Lastname;
                        newDoctor.DoctorJMBG = doctor.DoctorJMBG;
                        newDoctor.BankAccount = doctor.BankAccount;
                        newDoctor.Username = doctor.Username;
                        newDoctor.DoctorPassword = doctor.DoctorPassword;
                        context.Doctors.Add(newDoctor);
                        context.SaveChanges();
                        doctor.DoctorId = newDoctor.DoctorId;
                        return doctor;
                    }
                    else
                    {
                        Doctor editDoctor = (from p in context.Doctors where p.DoctorId == doctor.DoctorId select p).First();
                        editDoctor.Name = doctor.Name;
                        editDoctor.Lastname = editDoctor.Lastname;
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

        List<vwRequest> GetAllRequestByPatient(int patientId)
        {
            try
            {
                using (HospitalEntities context = new HospitalEntities())
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

        List<vwRequest> GetAllRequestByDoctor(int doctorId)
        {
            try
            {
                using (HospitalEntities context = new HospitalEntities())
                {
                    List<vwRequest> list = new List<vwRequest>();
                    list = (from p in context.vwRequests where p.DoctorId == doctorId select p).OrderByDescending(x=>x.IsApproved).ThenByDescending(x=>x.Date).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exeption" + ex.Message.ToString());
                return null;
            }
        }

        vwRequest AddRequest(vwRequest request)
        {
            try
            {
                using (HospitalEntities context = new HospitalEntities())
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

        void DeleteRequest(int requestId)
        {
            try
            {
                using (HospitalEntities context = new HospitalEntities())
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
    }
}
