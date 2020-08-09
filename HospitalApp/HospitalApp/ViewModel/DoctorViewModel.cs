using HospitalApp.Commands;
using HospitalApp.Service;
using HospitalApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Effects;

namespace HospitalApp.ViewModel
{
    public class DoctorViewModel:ViewModelBase
    {
        DoctorWindow docWindow;
        ServiceCode service = new ServiceCode();
        #region Constructor

        public DoctorViewModel(Doctor doctor, DoctorWindow docWindow)
        {
            this.docWindow = docWindow;
            this.doctor = doctor;
            RequestList = new ObservableCollection<vwRequest>(service.GetAllRequestByDoctor(doctor.DoctorId));
        }
        #endregion
        #region Property
        private Doctor doctor;
        public Doctor Doctor
        {
            get
            {
                return doctor;
            }
            set
            {
                doctor = value;
                OnPropertyChanged("Doctor");
            }
        }
        /*
        private vwRequest request = new vwRequest();
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
        */
        private vwRequest selectedRequest = new vwRequest();
        public vwRequest SelectedRequest
        {
            get
            {
                return selectedRequest;
            }
            set
            {
                selectedRequest = value;
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

        #endregion
        #region Command
        /*
        private ICommand aproveRequest;

        public ICommand AproveRequest
        {
            get
            {
                if (aproveRequest == null)
                {
                    aproveRequest = new RelayCommand(param => AproveRequestExecute(), param => CanAproveRequestExecute());
                }
                return aproveRequest;
            }
        }
        */
        public void AproveRequestExecute()
        {
            selectedRequest.IsApproved = true;
            try
            {
                if (service.AddRequest(selectedRequest) != null)
                {
                    RequestList = new ObservableCollection<vwRequest>(service.GetAllRequestByDoctor(doctor.DoctorId));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /*
        private bool CanAproveRequestExecute()
        {
            return true;            
        }

        private ICommand unAprovedRequest;

        public ICommand UnAprovedRequest
        {
            get
            {
                if (unAprovedRequest == null)
                {
                    unAprovedRequest = new RelayCommand(param => UnAproveRequestExecute(), param => CanUnAproveRequestExecute());
                }
                return unAprovedRequest;
            }
        }
        */
        public void UnAproveRequestExecute()
        {
            selectedRequest.IsApproved = false;
            try
            {
                if (service.AddRequest(selectedRequest) != null)
                {
                    RequestList = new ObservableCollection<vwRequest>(service.GetAllRequestByDoctor(doctor.DoctorId));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /*
        private bool CanUnAproveRequestExecute()
        {
            return true;
        }
        /*
        private ICommand deleteRequest;

        public ICommand DeleteRequest
        {
            get
            {
                if (deleteRequest == null)
                {
                    deleteRequest = new RelayCommand(param => DeleteRequestExecute(), param => CanDeleteRequestExecute());
                }
                return deleteRequest;
            }
        }
        */
        public void DeleteRequestExecute()
        {
            try
            {
                service.DeleteRequest(selectedRequest.RequestId);
                RequestList = new ObservableCollection<vwRequest>(service.GetAllRequestByDoctor(doctor.DoctorId));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /*
        private bool CanDeleteRequestExecute()
        {
            if (selectedRequest.IsApproved != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        */
        private ICommand logOutDoctor;

        public ICommand LogOutDoctor
        {
            get
            {
                if (logOutDoctor == null)
                {
                    logOutDoctor = new RelayCommand(param => LogOutDoctorExecute(), param => CanLogOutDoctorExecute());
                }
                return logOutDoctor;
            }
        }

        public void LogOutDoctorExecute()
        {
            try
            {                
                MainWindow main = new MainWindow();
                main.Show();
                docWindow.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanLogOutDoctorExecute()
        {
           return true;
        }

        #endregion
    }
}
