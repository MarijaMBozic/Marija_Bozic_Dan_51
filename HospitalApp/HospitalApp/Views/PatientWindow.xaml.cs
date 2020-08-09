using HospitalApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for PatientWindow.xaml
    /// </summary>
    public partial class PatientWindow : Window
    {
        private bool isValidDate;
        private bool isValidReason;
        private bool isValidCompany;

        PatientViewModel viewModel;
        public PatientWindow(Patient patient)
        {
            InitializeComponent();
            PatientViewModel viewModel = new PatientViewModel(patient, this);
            this.DataContext = viewModel;
            this.viewModel = viewModel;
        }

        private void IsRegistationEnabled()
        {
            if (isValidDate &&
                isValidReason &&
                isValidCompany)
            {
                btnSaveRequest.IsEnabled = true;
            }
            else
            {
                btnSaveRequest.IsEnabled = false;
            }
        }

        private void txtDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (txtDate.Focus())
            {
                lblValidationDate.Visibility = Visibility.Visible;
                lblValidationDate.FontSize = 16;
                lblValidationDate.Foreground = new SolidColorBrush(Colors.Red);
                //lblValidationDate.Content = "You must select a date!";
            }

            if (!txtDate.SelectedDate.HasValue)
            {
                txtDate.BorderBrush = new SolidColorBrush(Colors.Red);
                txtDate.Foreground = new SolidColorBrush(Colors.Red);
                isValidDate = false;
            }
            else
            {
                lblValidationDate.Visibility = Visibility.Hidden;
                txtDate.BorderBrush = new SolidColorBrush(Colors.Black);
                txtDate.Foreground = new SolidColorBrush(Colors.Black);
                isValidDate = true;
            }
            IsRegistationEnabled();
        }

        private void txtReason_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtReason.Focus() && txtReason.Text.Length>0)
            {
                lblValidationReason.Visibility = Visibility.Visible;
                lblValidationReason.FontSize = 16;
                lblValidationReason.Foreground = new SolidColorBrush(Colors.Red);
               // lblValidationReason.Content = "You must enter a reason!";
            }

            if (txtReason.Text.Length<5 && txtReason.Text.Length > 0)
            {
                txtReason.BorderBrush = new SolidColorBrush(Colors.Red);
                txtReason.Foreground = new SolidColorBrush(Colors.Red);
                isValidReason = false;
            }
            else
            {
                lblValidationReason.Visibility = Visibility.Hidden;
                txtReason.BorderBrush = new SolidColorBrush(Colors.Black);
                txtReason.Foreground = new SolidColorBrush(Colors.Black);
                isValidReason = true;
            }
            IsRegistationEnabled();
        }

        private void txtCompany_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtCompany.Focus() && txtCompany.Text.Length > 0)
            {
                lblValidationCompany.Visibility = Visibility.Visible;
                lblValidationCompany.FontSize = 16;
                lblValidationCompany.Foreground = new SolidColorBrush(Colors.Red);
                //lblValidationCompany.Content = "You must enter a company!";
            }

            if (txtCompany.Text.Length < 5 && txtCompany.Text.Length > 0)
            {
                txtCompany.BorderBrush = new SolidColorBrush(Colors.Red);
                txtCompany.Foreground = new SolidColorBrush(Colors.Red);
                isValidCompany = false;
            }
            else
            {
                lblValidationCompany.Visibility = Visibility.Hidden;
                txtCompany.BorderBrush = new SolidColorBrush(Colors.Black);
                txtCompany.Foreground = new SolidColorBrush(Colors.Black);
                isValidCompany = true;
            }
            IsRegistationEnabled();   
        }
    }
}

