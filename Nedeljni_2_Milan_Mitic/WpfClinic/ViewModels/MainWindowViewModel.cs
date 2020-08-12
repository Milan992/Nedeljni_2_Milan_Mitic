using System;
using System.Windows;
using System.Windows.Input;
using WpfClinic;
using WpfClinic.Model;
using WpfClinic.ViewModels;
using WpfClinic.Views;

namespace WpfCompany.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        Service service = new Service();
        MainWindow main;

        #region Constructors

        public MainWindowViewModel(MainWindow mainOpen)
        {
            main = mainOpen; 

        }

        #endregion

        #region Properties

        private string userName;

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }

        private string password;

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        #endregion

        #region Commands

        private ICommand logIn;

        public ICommand LogIn
        {
            get
            {
                if (logIn == null)
                {
                    logIn = new RelayCommand(param => LogInExecute(), param => CanLogInExecute());
                }

                return logIn;
            }
        }

        private void LogInExecute()
        {
            try
            {
                if (service.IsMaster(UserName, Password))
                {
                    Master master = new Master();
                    master.ShowDialog();
                }
                else if (service.IsDoctor(UserName, Password))
                {
                    Doctor doctor = new Doctor();
                    doctor.ShowDialog();
                }
                else if (service.IsManager(UserName, Password))
                {
                    tblAccount account = service.GetAccount(userName, password);
                    using (ClinicEntities context = new ClinicEntities())
                    {
                        tblManager manager = service.GetManager(account);
                        Manager managerOpen = new Manager(manager);
                        managerOpen.ShowDialog();
                    }
                }
                else if (service.IsAdmin(UserName, Password))
                {
                    tblAccount account = service.GetAccount(userName, password);
                    tblAdmin admin = service.GetAdmin(account);
                    Admin adminOpen = new Admin(admin);
                    adminOpen.ShowDialog();
                }
                else if (service.IsPatient(UserName, Password))
                {
                    tblAccount account = service.GetAccount(userName, Password);
                    tblPatient patient = service.GetPatient(account);
                    Patient patientOpen = new Patient(patient);
                    patientOpen.ShowDialog();
                }
                else if (service.IsMaintenance(UserName, Password))
                {
                    tblAccount account = service.GetAccount(userName, Password);
                    tblMaintenance maintenance = service.GetMaintenance(account);
                    Maintenance maintenanceOpen = new Maintenance(maintenance);
                    maintenanceOpen.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Username or password incorrect");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanLogInExecute()
        {
            return true;
        }

        private ICommand newAccount;

        public ICommand NewAccount
        {
            get
            {
                if (newAccount == null)
                {
                    newAccount = new RelayCommand(param => NewAccountExecute(), param => CanNewAccountExecute());
                }

                return newAccount;
            }
        }

        private void NewAccountExecute()
        {
            try
            {
                Register register = new Register();
                register.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanNewAccountExecute()
        {
            return true;
        }

        #endregion
    }
}
