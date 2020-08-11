using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfClinic.Model;
using WpfClinic.Views;

namespace WpfClinic.ViewModels
{
    class AddDoctorViewModel : ViewModelBase
    {
        AddDoctor addDoctor;
        Service service = new Service();

        #region Constructors

        public AddDoctorViewModel(AddDoctor addDoctorOpen)
        {
            doctor = new tblDoctor();
            managerToView = new tblManager();
            addDoctor = addDoctorOpen;
        }

        public AddDoctorViewModel(AddDoctor addDoctorOpen, tblManager manager)
        {
            doctor = new tblDoctor();
            managerToView = manager;
            account = new tblAccount();
            genderList = new List<string> { "M", "Z", "N", "X" };
            addDoctor = addDoctorOpen;
        }

        #endregion

        #region Properties

        private tblAccount account;

        public tblAccount Account
        {
            get { return account; }
            set
            {
                account = value;
                OnPropertyChanged("Account");
            }
        }

        private List<string> genderList;

        public List<string> GenderList
        {
            get { return genderList; }
            set
            {
                genderList = value;
                OnPropertyChanged("GenderList");
            }
        }

        private tblDoctor doctor;

        public tblDoctor Doctor
        {
            get { return doctor; }
            set
            {
                doctor = value;
                OnPropertyChanged("Doctor");
            }
        }

        private string birthDate;

        public string BirthDate
        {
            get { return birthDate; }
            set
            {
                birthDate = value;
                OnPropertyChanged("BirthDate");
            }
        }

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

        #endregion

        #region Commands

        private ICommand save;

        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }

                return save;
            }
        }

        private void SaveExecute()
        {
            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute()
        {
            if (true)
            {
                if (true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private ICommand close;

        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }

                return close;
            }
        }

        private void CloseExecute()
        {
            try
            {
                addDoctor.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanCloseExecute()
        {
            return true;
        }

        #endregion
    }
}
