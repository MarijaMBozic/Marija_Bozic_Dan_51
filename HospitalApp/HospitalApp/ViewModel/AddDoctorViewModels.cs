using HospitalApp.Commands;
using HospitalApp.Service;
using HospitalApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace HospitalApp.ViewModel
{
    public class AddDoctorViewModels:ViewModelBase
    {
        AddDoctor addDoctor;

        ServiceCode service = new ServiceCode();
        
        #region Constructor

        public AddDoctorViewModels(AddDoctor addDoctorOpen)
        {
            doctor = new Doctor();
            addDoctor = addDoctorOpen;
        }

        public AddDoctorViewModels(AddDoctor addDoctorOpen, Doctor doctorEdit)
        {
            doctor = doctorEdit;
            addDoctor = addDoctorOpen;
        }

        #endregion

        #region Properties

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
        private bool isUpdateDoctor;
        public bool IsUpdateDoctor
        {
            get
            {
                return isUpdateDoctor;
            }
            set
            {
                isUpdateDoctor = value;
            }
        }*/
        #endregion

        #region Commands

        private ICommand save;

        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(param));
                }
                return save;
            }
        }

        private void SaveExecute(object parametar)
        {
            var passwordBox = parametar as PasswordBox;
            var password = passwordBox.Password;
            Doctor.DoctorPassword = password;
            try
            {
                if(service.AddDoctor(Doctor)!=null)
                {
                    //isUpdateDoctor = true;                    
                    DoctorWindow window = new DoctorWindow(Doctor);
                    window.Show();
                    addDoctor.Close();
                }
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.ToString());
            }
        }

        #endregion
    }
}
