using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfClinic.Model;
using WpfClinic.Views;

namespace WpfClinic.ViewModels
{
    class AddManagerViewModel : ViewModelBase
    {
        AddManager addManager;
        Service service = new Service();

        #region Constructors

        public AddManagerViewModel(AddManager addManagerOpen)
        {
            account = new tblAccount();
            manager = new tblManager();
            genderList = new List<string> { "M", "Z", "N", "X" };
            clinicList = service.GetAllClinics();
            addManager = addManagerOpen;
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

        private tblManager manager;

        public tblManager Manager
        {
            get { return manager; }
            set
            {
                manager = value;
                OnPropertyChanged("Manager");
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

        private List<tblClinic> clinicList;

        public List<tblClinic> ClinicList
        {
            get { return clinicList; }
            set
            {
                clinicList = value;
                OnPropertyChanged("ClinicList");
            }
        }

        private tblClinic clinic;

        public tblClinic Clinic
        {
            get { return clinic; }
            set
            {
                clinic = value;
                OnPropertyChanged("Clinic");
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
                service.AddManager(Account, BirthDate, Clinic, Manager);
                addManager.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute()
        {
            if (Account.FullName != null && Account.IdCardNumber != null && Account.Gender != null
                && BirthDate != null && Account.Citinzenship != null && Account.UserName != null && Account.Pass != null
                && Clinic != null && Manager.MaxNumberOfDoctors != null && Manager.MaxNumberOfRooms != null)
            {
                if (Account.IdCardNumber.Length == 9 && Account.UserName.Length >= 6 && Account.Pass.Length >= 8
                    && int.TryParse(Account.IdCardNumber, out int result))
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
                addManager.Close();
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
