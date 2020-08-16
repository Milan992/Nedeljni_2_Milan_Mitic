using System;
using System.Windows;
using System.Windows.Input;
using WpfClinic.Model;
using WpfClinic.Views;

namespace WpfClinic.ViewModels
{
    class MasterViewModel : ViewModelBase
    {
        Master master;
        Service service = new Service();

        #region Constructors

        public MasterViewModel(Master masterOpen)
        {
            account = new tblAccount();
            master = masterOpen;
        }

        #endregion

        #region Properties

        private tblAccount account;

        public tblAccount Account
        {
            get
            {
                return account;
            }
            set
            {
                account = value;
                OnPropertyChanged("Account");
            }
        }

        #endregion

        #region Commands

        private ICommand addAdmin;

        public ICommand AddAdmin
        {
            get
            {
                if (addAdmin == null)
                {
                    addAdmin = new RelayCommand(param => AddAdminExecute(), param => CanAddAdminExecute());
                }

                return addAdmin;
            }
        }

        private void AddAdminExecute()
        {
            try
            {
                AddAdmin addAdmin = new AddAdmin();
                addAdmin.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanAddAdminExecute()
        {
            return true;
        }

        private ICommand addManager;

        public ICommand AddManager
        {
            get
            {
                if (addManager == null)
                {
                    addManager = new RelayCommand(param => AddManagerExecute(), param => CanAddManagerExecute());
                }

                return addManager;
            }
        }

        private void AddManagerExecute()
        {
            try
            {
                AddManager addManager = new AddManager();
                addManager.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanAddManagerExecute()
        {
            return true;
        }

        #endregion
    }
}
