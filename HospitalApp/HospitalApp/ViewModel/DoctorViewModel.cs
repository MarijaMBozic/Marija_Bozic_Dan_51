using HospitalApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Effects;

namespace HospitalApp.ViewModel
{
    public class DoctorViewModel:ViewModelBase
    {
      
        #region Constructor
        public DoctorViewModel(Doctor doctor)
        {
            this.doctor = doctor;
        }
        #endregion
        #region Property
        private Doctor doctor;
        public Doctor Doctor
        {
            get
            {
                return doctor;
            }
            set
            {
                doctor = value;
                OnPropertyChanged("Doctor");
            }
        }
        #endregion
    }
}
