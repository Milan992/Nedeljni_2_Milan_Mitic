using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        #endregion

    }
}
