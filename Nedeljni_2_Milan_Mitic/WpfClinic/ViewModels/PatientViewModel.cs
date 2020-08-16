using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
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

        #region Commands

        private ICommand appointment;

        public ICommand Appointment
        {
            get
            {
                if (appointment == null)
                {
                    appointment = new RelayCommand(param => AppointmentExecute(), param => CanAppointmentExecute());
                }

                return appointment;
            }
        }

        private void AppointmentExecute()
        {
            try
            {
                MessageBox.Show("Before sending an exam request you have to be checked\n if you have any active virus's sympthoms.");
                Exam exam = new Exam(PatientView);
                exam.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanAppointmentExecute()
        {
                    return true;
        }

        #endregion

    }
}
