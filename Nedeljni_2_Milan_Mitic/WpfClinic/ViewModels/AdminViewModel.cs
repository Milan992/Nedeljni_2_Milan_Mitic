using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
            clinics = service.GetAllClinics();
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

        private List<tblClinic> clinics;

        public List<tblClinic> Clinics
        {
            get { return clinics; }
            set
            {
                clinics = value;
                OnPropertyChanged("Clinics");
            }
        }


        #endregion

        #region Commands

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

        private ICommand riskStats;

        public ICommand RiskStats
        {
            get
            {
                if (riskStats == null)
                {
                    riskStats = new RelayCommand(param => RiskStatsExecute(), param => CanRiskStatsExecute());
                }

                return riskStats;
            }
        }

        private void RiskStatsExecute()
        {
            try
            {
                MessageBox.Show(service.GetRiskPatientsAge());
            }
            catch
            {
                MessageBox.Show("There are no patients wiht risky sympthoms.");
            }
        }

        private bool CanRiskStatsExecute()
        {
            return true;
        }

        private ICommand statistics;

        public ICommand Statistics
        {
            get
            {
                if (statistics == null)
                {
                    statistics = new RelayCommand(param => StatisticsExecute(), param => CanStatisticsExecute());
                }

                return statistics;
            }
        }

        private void StatisticsExecute()
        {
            try
            {
                MessageBox.Show(service.Statistics(AdminToView));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanStatisticsExecute()
        {
            return true;
        }

        private ICommand updateClinic;

        public ICommand UpdateClinic
        {
            get
            {
                if (updateClinic == null)
                {
                    updateClinic = new RelayCommand(param => UpdateClinicExecute(), param => CanUpdateClinicExecute());
                }

                return updateClinic;
            }
        }

        private void UpdateClinicExecute()
        {
            try
            {
                UpdateClinic updateClinic = new UpdateClinic(Clinic);
                updateClinic.ShowDialog();
                clinics = service.GetAllClinics();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanUpdateClinicExecute()
        {
            if (Clinic != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}


