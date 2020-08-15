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
    /// Interaction logic for AddDoctor.xaml
    /// </summary>
    public partial class AddDoctor : Window
    {
        public AddDoctor()
        {
            InitializeComponent();
            this.DataContext = new AddDoctorViewModel(this);
        }

        public AddDoctor(tblManager manager)
        {
            InitializeComponent();
            this.DataContext = new AddDoctorViewModel(this, manager);
        }

        public AddDoctor(tblManager manager, tblAccount doctor)
        {
            InitializeComponent();
            this.DataContext = new AddDoctorViewModel(this, manager, doctor);
        }
    }
}
