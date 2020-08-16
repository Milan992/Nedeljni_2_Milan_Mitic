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
    /// Interaction logic for Exam.xaml
    /// </summary>
    public partial class Exam : Window
    {
        public Exam()
        {
            InitializeComponent();
            this.DataContext = new ExamViewModel(this);
        }
        
        public Exam(tblPatient patient)
        {
            InitializeComponent();
            this.DataContext = new ExamViewModel(this, patient);
        }
    }
}
