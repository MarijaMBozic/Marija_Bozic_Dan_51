using HospitalApp.Commands;
using HospitalApp.Service;
using HospitalApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace HospitalApp.ViewModel
{
    public class PatientViewModel:ViewModelBase
    {
        ServiceCode service = new ServiceCode();
        List<vwDoctor> listDoc;
        PatientWindow patientWindow;
        #region Constructor

        public PatientViewModel(Patient patient, PatientWindow patientWindow)
        {
            this.patient = patient;
            this.patientWindow = patientWindow;
            listDoc= service.GetAllDoctors();
            RequestList = new ObservableCollection<vwRequest>(service.GetAllRequestByPatient(patient.PatientId));
            DoctorList = new ObservableCollection<vwDoctor>(listDoc);
            CheckOpenRequest(RequestList);
        }

        #endregion

        #region Property

        public Visibility ViewRequest
        {
            get
            {
                if (patient.DoctorId == null)
                {
                    return Visibility.Hidden;
                }
                return Visibility.Visible;
            }
        }
        public Visibility ViewComBox
        {
            get
            {
                if(patient.DoctorId==null)
                {
                    if(listDoc.Count!=0)
                    {
                        return Visibility.Visible;
                    }                   
                }
                return Visibility.Hidden;
            }            
        }
        public Visibility ViewLblDrNotAvailable
        {
            get
            {
                if (patient.DoctorId == null)
                {
                    if (listDoc.Count == 0)
                    {
                        return Visibility.Visible;
                    }
                }
                return Visibility.Hidden;
            }
        }

        private Patient patient;
        public Patient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
                OnPropertyChanged("Patient");
            }
        }

        private vwRequest request= new vwRequest();
        public vwRequest Request
        {
            get
            {
                return request;
            }
            set
            {
                request = value;
                OnPropertyChanged("Request");
            }
        }

        private ObservableCollection<vwRequest> requestList;
        public ObservableCollection<vwRequest> RequestList
        {
            get
            {
                return requestList;
            }
            set
            {
                requestList = value;
                OnPropertyChanged("RequestList");
            }
        }

        private bool isUpdateRequest;
        public bool IsUpdateRequest
        {
            get
            {
                return isUpdateRequest;
            }
            set
            {
                isUpdateRequest = value;
            }
        }

        private vwDoctor selectedDoctor;
        public vwDoctor SelectedDoctor
        {
            get
            {
                return selectedDoctor;
            }
            set
            {
                selectedDoctor = value;
                OnPropertyChanged("SelectedDoctor");
            }
        }

        private bool openRequest;
        public bool OpenRequest
        {
            get
            {
                return openRequest;
            }
            set
            {
                openRequest = value;
                OnPropertyChanged("OpenRequest");
            }
        }

        private bool isUpdatePatient;
        public bool IsUpdatePatient
        {
            get
            {
                return isUpdatePatient;
            }
            set
            {
                isUpdatePatient = value;
            }
        }

        private ObservableCollection<vwDoctor> doctorList;
        public ObservableCollection<vwDoctor> DoctorList
        {
            get
            {
                return doctorList;
            }
            set
            {
                doctorList = value;
                OnPropertyChanged("DoctorList");
            }
        }
        #endregion

        #region Commands

        private ICommand updatePatient;

        public ICommand UpdatePatient
        {
            get
            {
                if (updatePatient == null)
                {
                    updatePatient = new RelayCommand(param => UpdatePatientExecute(), param => CanUpdatePatientExecute());
                }

                return updatePatient;
            }
        }

        private void UpdatePatientExecute()
        {
            Patient.DoctorId = selectedDoctor.DoctorId;
            try
            {
                if (service.AddPatient(Patient) != null)
                {
                    isUpdatePatient = true;
                    OnPropertyChanged("ViewComBox");
                    OnPropertyChanged("ViewRequest");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanUpdatePatientExecute()
        {
            return true;
        }

        private ICommand saveRequest;

        public ICommand SaveRequest
        {
            get
            {
                if (saveRequest == null)
                {
                    saveRequest = new RelayCommand(param => RequestExecute());
                }
                return saveRequest;
            }
        }

        private void RequestExecute()
        {
            request.PatientId = patient.PatientId;
            request.DoctorId = patient.DoctorId;
            try
            {
                if (service.AddRequest(request) != null)
                {
                    RequestList = new ObservableCollection<vwRequest>(service.GetAllRequestByPatient(patient.PatientId));
                    CheckOpenRequest(RequestList);
                    ResetRequestFilds();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
                
        private ICommand logOutPatient;

        public ICommand LogOutPatient
        {
            get
            {
                if (logOutPatient == null)
                {
                    logOutPatient = new RelayCommand(param => LogOutPatientExecute(), param => CanLogOutPatientExecute());
                }
                return logOutPatient;
            }
        }

        public void LogOutPatientExecute()
        {
            try
            {
                MainWindow main = new MainWindow();
                main.Show();
                patientWindow.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanLogOutPatientExecute()
        {
            return true;
        }

        private void CheckOpenRequest(ObservableCollection<vwRequest> listRequest)
        {
            foreach (var r in listRequest)
            {
                if (r.IsApproved == null)
                {
                    OpenRequest = true;
                    break;
                }
            }
        }

        private void ResetRequestFilds()
        {
            request.Date = DateTime.Now;
            request.Reason = "";
            request.Company = "";
            request.IsUrgent = false;
            OnPropertyChanged("Request");
        }
        #endregion
    }
}
