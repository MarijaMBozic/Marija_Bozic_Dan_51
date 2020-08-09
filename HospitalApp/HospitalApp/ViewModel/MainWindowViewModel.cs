using HospitalApp.Commands;
using HospitalApp.Service;
using HospitalApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HospitalApp.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        MainWindow main;
        AddDoctor addDoctorWindow;
        #region Constructor

        public MainWindowViewModel(MainWindow mainOpen)
        {
            this.main = mainOpen;
        }

        #endregion

        #region Properties
        private string username;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        #endregion

        #region Command
        private ICommand login;
        public ICommand Login
        {
            get
            {
                if (login == null)
                {
                    login = new RelayCommand(param => LoginExecute(param));
                }
                return login;
            }
        }

        private void LoginExecute(object parametar)
        {
            //Doctor.DoctorPassword = password;
            try
            {
                var passwordBox = parametar as PasswordBox;
                var password = passwordBox.Password;
                ServiceCode service = new ServiceCode();

                Doctor doctor = service.LoginDoctor(Username, password);
                if (doctor == null)
                {
                    Patient patient = service.LoginPatient(Username, password);

                    if (patient == null)
                    {
                        MessageBox.Show("Uneti korisnik ne postoji!");
                    }
                    else
                    {

                        PatientWindow patientWindow = new PatientWindow(patient);
                        patientWindow.Show();
                        main.Close();
                    }
                }
                else
                {
                    // DoctorViewModel viewModel = new DoctorViewModel(doctor);
                    DoctorWindow window = new DoctorWindow(doctor);
                    window.Show();
                    main.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private ICommand registrationDoctor;
        public ICommand RegistrationDoctor
        {
            get
            {
                if (registrationDoctor == null)
                {
                    registrationDoctor = new RelayCommand(param => RegistrationDoctorExecute(), param => CanRegistrationDoctorExecute());
                }
                return registrationDoctor;
            }
        }

        private void RegistrationDoctorExecute()
        {
            try
            {
                addDoctorWindow = new AddDoctor();
                addDoctorWindow.Show();
                main.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanRegistrationDoctorExecute()
        {
            return true;
        }

        private ICommand registrationPatient;
        public ICommand RegistrationPatient
        {
            get
            {
                if (registrationPatient == null)
                {
                    registrationPatient = new RelayCommand(param => RegistrationPatientExecute(), param => CanRegistrationPatientExecute());
                }
                return registrationPatient;
            }
        }

        private void RegistrationPatientExecute()
        {
            try
            {
                AddPatientView addPatientWindow = new AddPatientView();
                addPatientWindow.Show();
                main.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanRegistrationPatientExecute()
        {
            return true;
        }
        #endregion
    }
}
