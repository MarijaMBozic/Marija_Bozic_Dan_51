﻿using HospitalApp.Commands;
using HospitalApp.Service;
using HospitalApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HospitalApp.ViewModel
{
    public class AddPatientViewModel:ViewModelBase
    {
        AddPatientView addPatientWindow;

        ServiceCode service = new ServiceCode();

        #region Constructor

        public AddPatientViewModel(AddPatientView addPatientOpen)
        {
            patient = new Patient();
            addPatientWindow = addPatientOpen;
        }

        public AddPatientViewModel(AddPatientView addPatientOpen, Patient patientEdit)
        {
            patient = patientEdit;
            addPatientWindow = addPatientOpen;
        }

        #endregion

        #region Properties

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
        #endregion

        #region Commands

        private ICommand savePatient;

        public ICommand SavePatient
        {
            get
            {
                if (savePatient == null)
                {
                    savePatient = new RelayCommand(param => SaveExecute(param));
                }

                return savePatient;
            }
        }

        private void SaveExecute(object parametar)
        {
            var passwordBox = parametar as PasswordBox;
            var password = passwordBox.Password;
            Patient.PatientPassword = password;
            try
            {
                if (service.AddPatient(Patient) != null)
                {
                    isUpdatePatient = true;
                    
                    PatientWindow window = new PatientWindow(Patient);
                    window.Show();
                    addPatientWindow.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //private bool CanSaveExecute()
        //{
        //    return false;
        //}

        private ICommand close;

        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }

                return close;
            }
        }

        private void CloseExecute()
        {
            try
            {
                addPatientWindow.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanCloseExecute()
        {
            return true;
        }

        #endregion
    }
}
