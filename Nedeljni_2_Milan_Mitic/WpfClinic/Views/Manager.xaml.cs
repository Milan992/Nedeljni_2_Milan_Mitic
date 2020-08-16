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
using WpfClinic.Model;
using WpfDoctor.ViewModels;

namespace WpfClinic.Views
{
    /// <summary>
    /// Interaction logic for Manager.xaml
    /// </summary>
    public partial class Manager : Window
    {
        public Manager()
        {
            InitializeComponent();
            this.DataContext = new ManagerViewModel(this);
        }

        public Manager(tblManager manager)
        {
            InitializeComponent();
            this.DataContext = new ManagerViewModel(this, manager);
        }
    }
}
