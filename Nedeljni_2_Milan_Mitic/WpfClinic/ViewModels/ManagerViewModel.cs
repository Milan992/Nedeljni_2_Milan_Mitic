using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfClinic;
using WpfClinic.Model;
using WpfClinic.ViewModels;
using WpfClinic.Views;

namespace WpfDoctor.ViewModels
{
    class ManagerViewModel : ViewModelBase
    {
        Manager manager;
        Service service = new Service();

        #region Constructors

        public ManagerViewModel(Manager managerOpen)
        {
            manager = managerOpen;
        }

        public ManagerViewModel(Manager managerOpen, tblManager managerView)
        {
            manager = managerOpen;
            managerToView = managerView;
            doctors = service.GetAllDoctors();
        }

        #endregion

        #region Properties

        private tblManager managerToView;

        public tblManager ManagerToView
        {
            get { return managerToView; }
            set
            {
                managerToView = value;
                OnPropertyChanged("ManagerToView");
            }
        }

        private tblAccount doctor;

        public tblAccount Doctor
        {
            get { return doctor; }
            set
            {
                doctor = value;
                OnPropertyChanged("Doctor");
            }
        }

        private List<tblAccount> doctors;

        public List<tblAccount> Doctors
        {
            get { return doctors; }
            set
            {
                doctors = value;
                OnPropertyChanged("Doctor");
            }
        }

        #endregion

        #region Commands

        private ICommand addDoctor;

        public ICommand AddDoctor
        {
            get
            {
                if (addDoctor == null)
                {
                    addDoctor = new RelayCommand(param => AddDoctorExecute(), param => CanAddDoctorExecute());
                }

                return addDoctor;
            }
        }

        private void AddDoctorExecute()
        {
            try
            {
                AddDoctor addDoctor = new AddDoctor(ManagerToView);
                addDoctor.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanAddDoctorExecute()
        {
            return true;
        }
        private ICommand updateDoctor;

        public ICommand UpdateDoctor
        {
            get
            {
                if (updateDoctor == null)
                {
                    updateDoctor = new RelayCommand(param => UpdateDoctorExecute(), param => CanUpdateDoctorExecute());
                }

                return updateDoctor;
            }
        }

        private void UpdateDoctorExecute()
        {
            try
            {
                AddDoctor addDoctor = new AddDoctor(ManagerToView, Doctor);
                addDoctor.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanUpdateDoctorExecute()
        {
            return true;
        }


        #endregion
    }
}
