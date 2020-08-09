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
        PatientViewModel viewModel;
        public PatientWindow(Patient patient)
        {
            InitializeComponent();
            PatientViewModel viewModel = new PatientViewModel(patient, this);
            this.DataContext = viewModel;
            this.viewModel = viewModel;
        }    
    }
}
