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
    class MaintenanceViewModel : ViewModelBase
    {
        Maintenance maintenance;
        Service service = new Service();

        #region Constructors 

        public MaintenanceViewModel(Maintenance maintenanceOpen)
        {
            maintenance = maintenanceOpen;
        }

        public MaintenanceViewModel(Maintenance maintenanceOpen, tblMaintenance maintenanceToView)
        {
            maintenance = maintenanceOpen;
            maintenanceView = maintenanceToView;
        }

        #endregion

        #region Properties

        private tblMaintenance maintenanceView;

        public tblMaintenance MaintenanceView
        {
            get { return maintenanceView; }
            set
            {
                maintenanceView = value;
                OnPropertyChanged("MaintenanceView");
            }
        }

        private List<Service> reports;

        public List<Service> Reports
        {
            get { return reports; }
            set
            {
                reports = value;
                OnPropertyChanged("Reports");
            }
        }

        #endregion

        #region Commands

        private ICommand maintenanceReport;

        public ICommand MaintenanceReport
        {
            get
            {
                if (maintenanceReport == null)
                {
                    maintenanceReport = new RelayCommand(param => MaintenanceReportExecute(), param => CanMaintenanceReportExecute());
                }

                return maintenanceReport;
            }
        }

        private void MaintenanceReportExecute()
        {
            try
            {
                AddMaintenanceReport addMaintenanceReport = new AddMaintenanceReport(MaintenanceView);
                addMaintenanceReport.ShowDialog();
                Reports = service.GetMaintenanceReports(MaintenanceView);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanMaintenanceReportExecute()
        {
            return true;
        }

        private ICommand showReports;

        public ICommand ShowReports
        {
            get
            {
                if (showReports == null)
                {
                    showReports = new RelayCommand(param => ShowReportsExecute(), param => CanShowReportsExecute());
                }

                return showReports;
            }
        }

        private void ShowReportsExecute()
        {
            try
            {
                Reports = service.GetMaintenanceReports(MaintenanceView);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanShowReportsExecute()
        {
            return true;
        }
        #endregion

    }
}
