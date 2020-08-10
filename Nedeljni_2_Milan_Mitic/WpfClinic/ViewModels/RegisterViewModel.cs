using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfClinic.Model;
using WpfClinic.Views;

namespace WpfClinic.ViewModels
{
    class RegisterViewModel : ViewModelBase
    {
        Register register;
        Service service = new Service();

        #region Constructors

        public RegisterViewModel(Register registerOpen)
        {
            account = new tblAccount();
            doctor = new tblAccount();
            doctorList = service.GetAllDoctors();
            register = registerOpen;
            genderList = new List<string> { "M", "Z", "N", "X" };
        }

        #endregion

        #region Properties

        private tblAccount doctor;

        public tblAccount Doctor
        {
            get { return doctor; }
            set
            {
                doctor = value;
                OnPropertyChanged("Doctor");
            }
        }

        private List<tblAccount> doctorList;

        public List<tblAccount> DoctorList
        {
            get { return doctorList; }
            set
            {
                doctorList = value;
                OnPropertyChanged("DoctorList");
            }
        }

        private tblAccount account;

        public tblAccount Account
        {
            get { return account; }
            set
            {
                account = value;
                OnPropertyChanged("Account");
            }
        }

        private List<string> genderList;

        public List<string> GenderList
        {
            get { return genderList; }
            set
            {
                genderList = value;
                OnPropertyChanged("GenderList");
            }
        }

        private string birthDate;

        public string BirthDate
        {
            get { return birthDate; }
            set
            {
                birthDate = value;
                OnPropertyChanged("BirthDate");
            }
        }

        private string expiry;

        public string Expiry
        {
            get { return expiry; }
            set
            {
                expiry = value;
                OnPropertyChanged("Expiry");
            }
        }

        private List<tblClinic> clinicList;

        public List<tblClinic> ClinicList
        {
            get { return clinicList; }
            set
            {
                clinicList = value;
                OnPropertyChanged("ClinicList");
            }
        }

        private tblClinic clinic;

        public tblClinic Clinic
        {
            get { return clinic; }
            set
            {
                clinic = value;
                OnPropertyChanged("Clinic");
            }
        }

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

        #endregion

        #region Commands

        private ICommand save;

        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }

                return save;
            }
        }

        private void SaveExecute()
        {
            try
            {
                service.AddPatient(Account, BirthDate, Clinic, Doctor, Patient, Expiry);
                register.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute()
        {
            if (Account.FullName != null && Account.IdCardNumber != null && Account.Gender != null
                && BirthDate != null && Account.Citinzenship != null && Account.UserName != null && Account.Pass != null
                && Patient.InsuranceCardExpiry != null && Patient.InsuranceCardNumber != null
                && Clinic != null && Doctor != null)
            {
                if (Account.IdCardNumber.Length == 9 && Account.UserName.Length >= 6 && Account.Pass.Length >= 8
                    && int.TryParse(Account.IdCardNumber, out int i) && int.TryParse(Patient.InsuranceCardNumber, out int j))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private ICommand close;

        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }

                return close;
            }
        }

        private void CloseExecute()
        {
            try
            {
                register.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanCloseExecute()
        {
            return true;
        }

        #endregion
    }
}
