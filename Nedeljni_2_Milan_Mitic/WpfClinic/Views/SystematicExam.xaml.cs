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
    /// Interaction logic for SystematicExam.xaml
    /// </summary>
    public partial class SystematicExam : Window
    {
        public SystematicExam()
        {
            InitializeComponent();
            this.DataContext = new SystematicExamViewModel(this);
        }

        public SystematicExam(tblPatient patient)
        {
            InitializeComponent();
            this.DataContext = new SystematicExamViewModel(this, patient);
        }
    }
}
