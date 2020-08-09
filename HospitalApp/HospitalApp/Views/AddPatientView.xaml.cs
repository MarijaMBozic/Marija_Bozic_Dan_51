using HospitalApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HospitalApp.Views
{
    /// <summary>
    /// Interaction logic for AddPatientView.xaml
    /// </summary>
    public partial class AddPatientView : Window
    {
        private bool isValidFullName;
        private bool isValidJMBG;
        private bool isValidNumInsurance;
        private bool isValidUsername;
        private bool isValidPassword;

        public AddPatientView()
        {
            InitializeComponent();
            this.DataContext = new AddPatientViewModel(this);
        }

        public AddPatientView(Patient patientEdit)
        {
            InitializeComponent();
            this.DataContext = new AddPatientViewModel(this, patientEdit);
        }

        private void IsRegistationEnabled()
        {
            if (isValidFullName && 
                isValidJMBG && 
                isValidNumInsurance && 
                isValidUsername &&
                isValidPassword)
            {
                btnSavePatient.IsEnabled = true;
            }
            else
            {
                btnSavePatient.IsEnabled = false;
            }
        }

        private void txtFullName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtFullName.Focus())
            {
                lblValidationFullName.Visibility = Visibility.Visible;
                lblValidationFullName.FontSize = 16;
                lblValidationFullName.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationFullName.Content = "The full name must contain at least 10 caracters!";
            }

            string patternFullName = @"^([a-zA-Z ]{10,100})$";
            Match match = Regex.Match(txtFullName.Text, patternFullName, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                txtFullName.BorderBrush = new SolidColorBrush(Colors.Red);
                txtFullName.Foreground = new SolidColorBrush(Colors.Red);
                isValidFullName = false;
            }
            else
            {
                lblValidationFullName.Visibility = Visibility.Hidden;
                txtFullName.BorderBrush = new SolidColorBrush(Colors.Black);
                txtFullName.Foreground = new SolidColorBrush(Colors.Black);
                isValidFullName = true;
            }
            IsRegistationEnabled();
        }

        private void txtPatientJMBG_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtPatientJMBG.Focus())
            {
                lblValidationJMBG.Visibility = Visibility.Visible;
                lblValidationJMBG.FontSize = 16;
                lblValidationJMBG.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationJMBG.Content = "The JMBG must contains exact 13 digits!";
            }

            string patternJMBG = @"^([0-9]{13})$";
            Match match = Regex.Match(txtPatientJMBG.Text, patternJMBG, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                txtPatientJMBG.BorderBrush = new SolidColorBrush(Colors.Red);
                txtPatientJMBG.Foreground = new SolidColorBrush(Colors.Red);
                isValidJMBG = false;
            }
            else
            {
                lblValidationJMBG.Visibility = Visibility.Hidden;
                txtPatientJMBG.BorderBrush = new SolidColorBrush(Colors.Black);
                txtPatientJMBG.Foreground = new SolidColorBrush(Colors.Black);
                isValidJMBG = true;
            }
            IsRegistationEnabled();
        }

        private void txtNumInsurace_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtNumInsurace.Focus())
            {
                lblValidationNumInsurance.Visibility = Visibility.Visible;
                lblValidationNumInsurance.FontSize = 16;
                lblValidationNumInsurance.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationNumInsurance.Content = "The Number Insurace must contains exact 7 digits!";
            }

            string patternNumInsurace = @"^([0-9]{7})$";
            Match match = Regex.Match(txtNumInsurace.Text, patternNumInsurace, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                txtNumInsurace.BorderBrush = new SolidColorBrush(Colors.Red);
                txtNumInsurace.Foreground = new SolidColorBrush(Colors.Red);
                isValidNumInsurance = false;
            }
            else
            {
                lblValidationNumInsurance.Visibility = Visibility.Hidden;
                txtNumInsurace.BorderBrush = new SolidColorBrush(Colors.Black);
                txtNumInsurace.Foreground = new SolidColorBrush(Colors.Black);
                isValidNumInsurance = true;
            }
            IsRegistationEnabled();
        }       

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtUsername.Focus())
            {
                lblValidationUserName.Visibility = Visibility.Visible;
                lblValidationUserName.FontSize = 16;
                lblValidationUserName.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationUserName.Content = "The username can't special characters.\nAt least 5 characters!";
            }

            string patternUserName = @"^([a-zA-Z0-9]{5,100})$";
            Match match = Regex.Match(txtUsername.Text, patternUserName, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                txtUsername.BorderBrush = new SolidColorBrush(Colors.Red);
                txtUsername.Foreground = new SolidColorBrush(Colors.Red);
                isValidUsername = false;
            }
            else
            {
                lblValidationUserName.Visibility = Visibility.Hidden;
                txtUsername.BorderBrush = new SolidColorBrush(Colors.Black);
                txtUsername.Foreground = new SolidColorBrush(Colors.Black);
                isValidUsername = true;
            }
            IsRegistationEnabled();
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Focus())
            {
                lblValidationPassword.Visibility = Visibility.Visible;
                lblValidationPassword.FontSize = 16;
                lblValidationPassword.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationPassword.Content = "The password can't contains special characters.\nMust contains least 2 numbers.\nMinimum length 6 characters!";
            }

            string patternPassword = @"^[A-Za-z]{4,}[0-9]{2,}$";
            Match match = Regex.Match(txtPassword.Password, patternPassword, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                txtPassword.BorderBrush = new SolidColorBrush(Colors.Red);
                txtPassword.Foreground = new SolidColorBrush(Colors.Red);
                isValidPassword = false;
            }
            else
            {
                lblValidationPassword.Visibility = Visibility.Hidden;
                txtPassword.BorderBrush = new SolidColorBrush(Colors.Black);
                txtPassword.Foreground = new SolidColorBrush(Colors.Black);
                isValidPassword = true;
            }
            IsRegistationEnabled();
        }
    }
}
