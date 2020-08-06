using HospitalApp.Commands;
using HospitalApp.Service;
using HospitalApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HospitalApp.ViewModel
{
    public class MainWindowViewModel:ViewModelBase
    {
        MainWindow main;
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
                    login = new RelayCommand(param => LoginExecute(param), param => CanLoginExecute());
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
                if(doctor==null)
                {
                    Patient patient = service.LoginPatient(Username, password);

                    if (patient == null)
                    {
                        MessageBox.Show("Uneti korisnik ne postoji!");
                    }
                    else
                    {
                        //PactientViewModel viewModel = new PactientViewModel(doctor);
                        //PatientWindow window = new PatientWindow(viewModel);
                        //window.Show();
                    }
                }
                else
                {
                    DoctorViewModel viewModel = new DoctorViewModel(doctor);
                    DoctorWindow window = new DoctorWindow(viewModel);
                    window.Show();
                    main.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanLoginExecute()
        {
            return true;
        }
        #endregion
    }
}
