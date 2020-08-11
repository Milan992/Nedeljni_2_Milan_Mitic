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
    class AdminViewModel : ViewModelBase
    {
        Admin admin;
        Service service = new Service();

        #region Constructors

        public AdminViewModel(Admin adminOpen)
        {
            admin = adminOpen;
        }

        public AdminViewModel(Admin adminOpen, tblAdmin administrator)
        {
            admin = adminOpen;
            adminToView = administrator;
        }

        #endregion

        #region Properties

        private tblAdmin adminToView;

        public tblAdmin AdminToView
        {
            get { return adminToView; }
            set
            {
                adminToView = value;
                OnPropertyChanged("AdminToView");
            }
        }

        #endregion

        #region Commands

        private ICommand addClinic;

        public ICommand AddClinic
        {
            get
            {
                if (addClinic == null)
                {
                    addClinic = new RelayCommand(param => AddClinicExecute(), param => CanAddClinicExecute());
                }

                return addClinic;
            }
        }

        private void AddClinicExecute()
        {
            try
            {
                tblAccount account = new tblAccount();
                using (ClinicEntities context = new ClinicEntities())
                {
                    account = (from a in context.tblAccounts where a.AccountID == AdminToView.AccountID select a).First();
                }

                AddClinic addClinic = new AddClinic(account);
                addClinic.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanAddClinicExecute()
        {
            return true;
        }

        private ICommand addMaintenance;

        public ICommand AddMaintenance
        {
            get
            {
                if (addMaintenance == null)
                {
                    addMaintenance = new RelayCommand(param => AddMaintenanceExecute(), param => CanAddMaintenanceExecute());
                }

                return addMaintenance;
            }
        }

        private void AddMaintenanceExecute()
        {
            try
            {
                tblAccount account = new tblAccount();
                using (ClinicEntities context = new ClinicEntities())
                {
                    account = (from a in context.tblAccounts where a.AccountID == AdminToView.AccountID select a).First();
                }

                AddMaintenance addMaintenance = new AddMaintenance();
                addMaintenance.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanAddMaintenanceExecute()
        {
            return true;
        }

        #endregion
    }
}


