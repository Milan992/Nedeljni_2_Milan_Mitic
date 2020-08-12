using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WpfClinic.Model;
using WpfClinic.Views;

namespace WpfClinic.ViewModels
{
    class SystematicExamViewModel : ViewModelBase
    {
        SystematicExam systematicExam;
        Service service = new Service();
        static int counter;

        #region Costructors

        public SystematicExamViewModel(SystematicExam systematicExamOpen)
        {
            systematicExam = systematicExamOpen;
        }

        public SystematicExamViewModel(SystematicExam systematicExamOpen, tblPatient patientToView)
        {
            systematicExam = systematicExamOpen;
            patient = patientToView;
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync();
            doctor = new tblDoctor();
            counter = 0;
        }

        #endregion

        #region Properties

        private tblPatient patient;

        public tblPatient Patient
        {
            get { return patient; }
            set
            {
                patient = value;
                OnPropertyChanged("Patient");
            }
        }

        private int percent;

        public int Percent
        {
            get { return percent; }
            set
            {
                percent = value;
                OnPropertyChanged("Percent");
            }
        }

        private tblDoctor doctor;

        public tblDoctor Doctor
        {
            get { return doctor; }
            set
            {
                doctor = value;
                OnPropertyChanged("Doctor");
            }
        }


        public BackgroundWorker worker = new BackgroundWorker();

        #endregion

        #region Background worker 

        public void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Systematic exam completed");
        }

        public void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 7; i++)
            {
                Thread.Sleep(1000);

                Percent = Percent + 14;
            }

            #endregion
        }
        }
    }
