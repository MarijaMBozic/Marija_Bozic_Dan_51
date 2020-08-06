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
    /// Interaction logic for LoginDoctor.xaml
    /// </summary>
    public partial class LoginDoctor : Window
    {
        public LoginDoctor()
        {
            InitializeComponent();
            this.DataContext = new LoginViewModel();
        }
    }
}
