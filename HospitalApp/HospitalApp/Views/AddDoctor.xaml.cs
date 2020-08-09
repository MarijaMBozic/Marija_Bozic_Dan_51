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
    /// Interaction logic for AddDoctor.xaml
    /// </summary>
    public partial class AddDoctor : Window
    {
        private bool isValidFullName;
        private bool isValidJMBG;
        private bool isValidBankAccount;
        private bool isValidUsername;
        private bool isValidPassword;
        public AddDoctor()
        {
            InitializeComponent();
            this.DataContext = new AddDoctorViewModels(this);
        }

        public AddDoctor(Doctor doctorEdit)
        {
            InitializeComponent();
            this.DataContext = new AddDoctorViewModels(this, doctorEdit);
        }
        private void IsRegistationEnabled()
        {
            if (isValidFullName &&
                isValidJMBG &&
                isValidBankAccount &&
                isValidUsername &&
                isValidPassword)
            {
                btnSaveDoctor.IsEnabled = true;
            }
            else
            {
                btnSaveDoctor.IsEnabled = false;
            }
        }

        private void txtFullname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtFullname.Focus())
            {
                lblValidationFullname.Visibility = Visibility.Visible;
                lblValidationFullname.FontSize = 16;
                lblValidationFullname.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationFullname.Content = "The full name must contain at least 10 caracters!";
            }

            string patternFullName = @"^([a-zA-Z ]{10,100})$";
            Match match = Regex.Match(txtFullname.Text, patternFullName, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                txtFullname.BorderBrush = new SolidColorBrush(Colors.Red);
                txtFullname.Foreground = new SolidColorBrush(Colors.Red);
                isValidFullName = false;
            }
            else
            {
                lblValidationFullname.Visibility = Visibility.Hidden;
                txtFullname.BorderBrush = new SolidColorBrush(Colors.Black);
                txtFullname.Foreground = new SolidColorBrush(Colors.Black);
                isValidFullName = true;
            }
            IsRegistationEnabled();
        }

        private void txtDoctorJMBG_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtDoctorJMBG.Focus())
            {
                lblValidationJMBG.Visibility = Visibility.Visible;
                lblValidationJMBG.FontSize = 16;
                lblValidationJMBG.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationJMBG.Content = "The JMBG must contains exact 13 digits!";
            }

            string patternJMBG = @"^([0-9]{13})$";
            Match match = Regex.Match(txtDoctorJMBG.Text, patternJMBG, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                txtDoctorJMBG.BorderBrush = new SolidColorBrush(Colors.Red);
                txtDoctorJMBG.Foreground = new SolidColorBrush(Colors.Red);
                isValidJMBG = false;
            }
            else
            {
                lblValidationJMBG.Visibility = Visibility.Hidden;
                txtDoctorJMBG.BorderBrush = new SolidColorBrush(Colors.Black);
                txtDoctorJMBG.Foreground = new SolidColorBrush(Colors.Black);
                isValidJMBG = true;
            }
            IsRegistationEnabled();
        }

        private void txtBankAccount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtBankAccount.Focus())
            {
                lblValidationBankAccount.Visibility = Visibility.Visible;
                lblValidationBankAccount.FontSize = 16;
                lblValidationBankAccount.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationBankAccount.Content = "The Number Insurace must contains exact 10 digits!";
            }

            string patternBancAcount = @"^([0-9]{10})$";
            Match match = Regex.Match(txtBankAccount.Text, patternBancAcount, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                txtBankAccount.BorderBrush = new SolidColorBrush(Colors.Red);
                txtBankAccount.Foreground = new SolidColorBrush(Colors.Red);
                isValidBankAccount = false;
            }
            else
            {
                lblValidationBankAccount.Visibility = Visibility.Hidden;
                txtBankAccount.BorderBrush = new SolidColorBrush(Colors.Black);
                txtBankAccount.Foreground = new SolidColorBrush(Colors.Black);
                isValidBankAccount = true;
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
