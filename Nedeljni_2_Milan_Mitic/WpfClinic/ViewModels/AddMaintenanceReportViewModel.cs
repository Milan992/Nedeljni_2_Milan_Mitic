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
    class AddMaintenanceReportViewModel : ViewModelBase
    {
        AddMaintenanceReport addMaintenanceReport;
        Service service = new Service();

        #region Constructors

        public AddMaintenanceReportViewModel(AddMaintenanceReport addMaintenanceReportOpen)
        {
            addMaintenanceReport = addMaintenanceReportOpen;
        }

        public AddMaintenanceReportViewModel(AddMaintenanceReport addMaintenanceReportOpen, tblMaintenance maintenanceToView)
        {
            addMaintenanceReport = addMaintenanceReportOpen;
            maintenance = maintenanceToView;
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

        private int hours;

        public int Hours
        {
            get { return hours; }
            set
            {
                hours = value;
                OnPropertyChanged("Hours");
            }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        #endregion

        #region Commands

        private ICommand addMaintenanceReportToFile;

        public ICommand AddMaintenanceReportToFile
        {
            get
            {
                if (addMaintenanceReportToFile == null)
                {
                    addMaintenanceReportToFile = new RelayCommand(param => AddMaintenanceReportToFileExecute(), param => CanAddMaintenanceReportToFileExecute());
                }

                return addMaintenanceReportToFile;
            }
        }

        private void AddMaintenanceReportToFileExecute()
        {
            try
            {
                service.AddMaintenanceReport(Hours, Description, Maintenance);
                MessageBox.Show("Report saved.");
                addMaintenanceReport.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanAddMaintenanceReportToFileExecute()
        {
            if (Hours != null && Description != null)
            {
                if (Hours < 20 && Description.Length < 100)
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

        #endregion
    }
}
