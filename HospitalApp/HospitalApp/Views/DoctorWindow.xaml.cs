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
    /// Interaction logic for DoctorWindow.xaml
    /// </summary>
    public partial class DoctorWindow : Window
    {
        DoctorViewModel viewModel;
        public DoctorWindow(Doctor doctor)
        {
            InitializeComponent();
            DoctorViewModel viewModel = new DoctorViewModel(doctor, this);
            this.DataContext = viewModel;
            this.viewModel = viewModel;
        }

        private void btnAprRequest_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AproveRequestExecute();
        }

        private void btnUnAprRequest_Click(object sender, RoutedEventArgs e)
        {
            viewModel.UnAproveRequestExecute();
        }

        private void btnDeleteRequest_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DeleteRequestExecute();
        }
    }
}
