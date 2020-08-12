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
    class AddDoctorViewModel : ViewModelBase
    {
        AddDoctor addDoctor;
        Service service = new Service();

        #region Constructors

        public AddDoctorViewModel(AddDoctor addDoctorOpen)
        {
            doctor = new tblDoctor();
            managerToView = new tblManager();
            account = new tblAccount();
            genderList = new List<string> { "M", "Z", "N", "X" };
            clinicList = service.GetAllClinics();
            shiftList = service.GetAllShifts();
            addDoctor = addDoctorOpen;
        }

        public AddDoctorViewModel(AddDoctor addDoctorOpen, tblManager manager)
        {
            doctor = new tblDoctor();
            managerToView = manager;
            account = new tblAccount();
            genderList = new List<string> { "M", "Z", "N", "X" };
            clinicList = service.GetAllClinics();
            shiftList = service.GetAllShifts();
            addDoctor = addDoctorOpen;
        }

        #endregion

        #region Properties

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

        private tblManager managerToView;

        public tblManager ManagerToView
        {
            get { return managerToView; }
            set
            {
                managerToView = value;
                OnPropertyChanged("ManagerToView");
            }
        }

        private bool reception;

        public bool Reception
        {
            get { return reception; }
            set
            {
                reception = value;
                OnPropertyChanged("Reception");
            }
        }

        private List<tblShift> shiftList;

        public List<tblShift> ShiftList
        {
            get { return shiftList; }
            set
            {
                shiftList = value;
                OnPropertyChanged("ShiftList");
            }
        }

        private tblShift shift;

        public tblShift Shift
        {
            get { return shift; }
            set
            {
                shift = value;
                OnPropertyChanged("Shift");
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
                service.AddDoctor(Account, Doctor, Clinic, Shift, Reception, ManagerToView, BirthDate);
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
                && Doctor.DoctorNumber != null && Doctor.BankAccount != null && Doctor.DoctorNumber != null && Doctor.Department != null
                && Shift != null && Reception != null && Clinic != null)
            {
                if (Account.IdCardNumber.Length == 9 && Account.UserName.Length >= 6 && Account.Pass.Length >= 8
                    && Doctor.DoctorNumber.Length == 5 && Doctor.BankAccount.Length == 10
                    && int.TryParse(Account.IdCardNumber, out int i) && int.TryParse(Doctor.DoctorNumber, out int j)
                    && int.TryParse(Doctor.BankAccount, out int k))
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
                addDoctor.Close();
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
