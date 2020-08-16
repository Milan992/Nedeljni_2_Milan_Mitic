using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfClinic.ViewModels;

namespace WpfClinic.Views
{
    /// <summary>
    /// Interaction logic for AddMaintenance.xaml
    /// </summary>
    public partial class AddMaintenance : Window
    {
        public AddMaintenance()
        {
            InitializeComponent();
            this.DataContext = new AddMaintenanceViewModel(this);
        }
    }
}
