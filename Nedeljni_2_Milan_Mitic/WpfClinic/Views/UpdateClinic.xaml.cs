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
    /// Interaction logic for UpdateClinic.xaml
    /// </summary>
    public partial class UpdateClinic : Window
    {
        public UpdateClinic()
        {
            InitializeComponent();
            this.DataContext = new UpdateClinicViewModel(this);
        }

        public UpdateClinic(tblClinic clinic)
        {
            InitializeComponent();
            this.DataContext = new UpdateClinicViewModel(this, clinic);
        }
    }
}
