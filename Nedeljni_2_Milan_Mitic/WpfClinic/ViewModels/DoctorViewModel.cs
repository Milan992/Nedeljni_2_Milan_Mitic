using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClinic.Views;

namespace WpfClinic.ViewModels
{
    class DoctorViewModel
    {
        Doctor doctor;
        Service service = new Service();

        #region Constructors

        public DoctorViewModel(Doctor doctorOpen)
        {
            doctor = doctorOpen;
        }

        #endregion
    }
}
