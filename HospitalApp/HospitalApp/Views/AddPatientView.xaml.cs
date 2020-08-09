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
    /// Interaction logic for AddPatientView.xaml
    /// </summary>
    public partial class AddPatientView : Window
    {
        private bool isValidFullName;

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

        private void txtFullName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtFullName.Focus())
            {
                lblValidationFullName.Visibility = Visibility.Visible;
                lblValidationFullName.FontSize = 16;
                lblValidationFullName.Foreground = new SolidColorBrush(Colors.Red);
                lblValidationFullName.Content = "The Account NUmber must contain 10 digits";
            }
 
            if (txtFullName.Text.Length < 10)
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

        private void IsRegistationEnabled()
        {
            if(isValidFullName)
            {
                btnSavePatient.IsEnabled = true;
            }
            else
            {
                btnSavePatient.IsEnabled = false;
            }
        }
    }
}
