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
    class AddMaintenanceViewModel : ViewModelBase
    {
        AddMaintenance addMaintenance;
        Service service = new Service();

        #region Costructors

        public AddMaintenanceViewModel(AddMaintenance addMaintenanceOpen)
        {
            addMaintenance = addMaintenanceOpen;
            account = new tblAccount();
            maintenance = new tblMaintenance();
            genderList = new List<string> { "M", "Z", "N", "X" };
        }

        #endregion

        #region Properties

        private tblMaintenance maintenance;

        public tblMaintenance Maintenance
        {
            get { return maintenance; }
            set
            {
                maintenance = value;
                OnPropertyChanged("Maintenance");
            }
        }

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

        private bool expand;

        public bool Expand
        {
            get { return expand; }
            set
            {
                expand = value;
                OnPropertyChanged("Expand");
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
                Maintenance.ExpandingClinicPermision = Expand;
                service.AddMaintenance(Account, Maintenance, BirthDate);
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
                && Expand != null)
            {
                if (Account.IdCardNumber.Length == 9 && Account.UserName.Length >= 6 && Account.Pass.Length >= 8
                    && int.TryParse(Account.IdCardNumber, out int i))
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
                addMaintenance.Close();
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
