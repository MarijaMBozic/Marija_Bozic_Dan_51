using HospitalApp.Commands;
using HospitalApp.Service;
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
        #region Constructor

        public PatientViewModel(vwPatient patient)
        {
            this.patient = patient;
            List<vwDoctor> listDoc= service.GetAllDoctors();
            List<vwRequest> listPatientRequests = service.GetAllRequestByPatient(patient.PatientId);
            OpenRequest = service.GetOpenRequestByPatient(patient.PatientId);
            DoctorList = new ObservableCollection<vwDoctor>(listDoc);
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
                    return Visibility.Visible;
                }
                return Visibility.Hidden;
            }
            
        }

        private vwPatient patient;
        public vwPatient Patient
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

        private vwRequest request;
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

        private vwRequest openRequest;
        public vwRequest OpenRequest
        {
            get
            {
                return openRequest;
            }
            set
            {
                openRequest = value;
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
                    saveRequest = new RelayCommand(param => RequestExecute(), param => CanRequestExecute());
                }
                return saveRequest;
            }
        }

        private void RequestExecute()
        {
            Request.PatientId = patient.PatientId;
            Request.DoctorId = patient.DoctorId;
            try
            {
                if (service.AddRequest(Request) != null)
                {
                    isUpdateRequest = true;                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanRequestExecute()
        {
            if(OpenRequest!=null)
            {
                return false;
            }
            else
            {                
                return true;
            }
        }
        #endregion
    }
}
