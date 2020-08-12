using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfClinic.Model;
using WpfClinic.Views;

namespace WpfClinic.ViewModels
{
    class ExamViewModel : ViewModelBase
    {
        Exam exam;
        Service service = new Service();
        static int counter;

        #region Costructors

        public ExamViewModel(Exam examOpen)
        {
            exam = examOpen;
        }

        public ExamViewModel(Exam examOpen, tblPatient patientToView)
        {
            exam = examOpen;
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
            try
            {
                using (ClinicEntities context = new ClinicEntities())
                {
                    //searchig for first available doctor that recepts patients.
                    Doctor = (from d in context.tblDoctors where d.PatientReception == true select d).First();
                    Random random = new Random();
                    int i = 1;// random.Next(0, 2);
                    // 1 means that patient has sympthoms.
                    if (i == 1)
                    {
                        counter++;
                        if (counter < 2)
                        {
                            //ask patient if he wants to check again
                            string sMessageBoxText = "WARNING! You have active flue sympthoms found.\nDo you want to proceed to another check?\n" +
                                "If the sympthoms are found again you will take an systematic exam.\n" +
                                "If the symphtoms are not found again, you can proceed to completing your exam appointment.\n" +
                                "Do you want to be checked again?";
                            string sCaption = "";

                            MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
                            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

                            switch (rsltMessageBox)
                            {
                                case MessageBoxResult.Yes:
                                    Percent = 0;
                                    worker.RunWorkerAsync();
                                    break;

                                case MessageBoxResult.No:
                                    exam.Close();
                                    break;

                                case MessageBoxResult.Cancel:
                                    exam.Close();
                                    break;
                            }
                        }
                        // patient had sympthoms twice.
                        else
                        {
                            MessageBox.Show("You had active flue sypmthoms twice. Please take a systematic exam.");
                            SystematicExam systematicExam = new SystematicExam(Patient);
                            systematicExam.ShowDialog();
                            using (StreamWriter sw = new StreamWriter(@"..\..\AtRiskPatients.txt", true))
                            {
                                tblAccount account = (from a in context.tblAccounts where a.AccountID == Patient.AccountID select a).First();
                                string diff = (DateTime.Now -account.BirthDate).TotalDays.ToString();
                                double years = Convert.ToDouble(diff) / 365;
                                sw.WriteLine(account.FullName + ", age: {0}, has active flue sympthoms", years);
                            }
                        }
                    }
                    // patient has no sympthoms.
                    else
                    {
                        using (StreamWriter sw = new StreamWriter(@"..\..\Requests.txt", true))
                        {
                            sw.WriteLine("Patient with an accountID {0} sent an exam request", Patient.AccountID);
                        }
                        MessageBox.Show("Request for examination sent.");
                        exam.Close();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Sorry, there are no doctors to examinate you.");
                using (ClinicEntities context = new ClinicEntities())
                {
                    // increase number of fails to every manager,
                    List<tblManager> list = (from m in context.tblManagers select m).ToList();
                    foreach (var manager in list)
                    {
                        manager.NumberOfFails++;
                        context.SaveChanges();
                    }
                }
            }
        }

        public void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1000);

                Percent = Percent + 20;
            }

            #endregion
        }
    }
}

