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
using WpfClinic.ViewModels;

namespace WpfClinic.Views
{
    /// <summary>
    /// Interaction logic for AddClinic.xaml
    /// </summary>
    public partial class AddClinic : Window
    {
        public AddClinic()
        {
            InitializeComponent();
            this.DataContext = new AddClinicViewModel(this);
        }

        public AddClinic(tblAccount account)
        {
            InitializeComponent();
            this.DataContext = new AddClinicViewModel(this, account);
        }
    }
}
