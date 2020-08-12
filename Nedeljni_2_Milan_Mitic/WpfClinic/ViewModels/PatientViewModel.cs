using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClinic.Model;
using WpfClinic.Views;

namespace WpfClinic.ViewModels
{
    class PatientViewModel : ViewModelBase
    {
        Patient patient;
        Service service = new Service();

        #region Constructors 

        public PatientViewModel(Patient patientOpen)
        {
            patient = patientOpen;
        }

        public PatientViewModel(Patient patientOpen, tblPatient patientToView)
        {
            patient = patientOpen;
            patientView = patientToView;
        }

        #endregion

        #region Properties

        private tblPatient patientView;

        public tblPatient PatientView
        {
            get { return patientView; }
            set
            {
                patientView = value;
                OnPropertyChanged("PatientView");
            }
        }

        #endregion

    }
}
