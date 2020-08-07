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
        public DoctorViewModel(vwDoctor doctor)
        {
            this.doctor = doctor;
        }
        #endregion
        #region Property
        private vwDoctor doctor;
        public vwDoctor Doctor
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
