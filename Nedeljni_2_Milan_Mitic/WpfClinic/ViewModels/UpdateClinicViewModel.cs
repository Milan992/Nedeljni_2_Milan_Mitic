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
    class UpdateClinicViewModel : ViewModelBase
    {
        UpdateClinic updateClinic;
        Service service = new Service();

        #region Constructors

        public UpdateClinicViewModel(UpdateClinic updateClinicOpen)
        {
            updateClinic = updateClinicOpen;
        }

        public UpdateClinicViewModel(UpdateClinic updateClinicOpen, tblClinic clinicToVew)
        {
            updateClinic = updateClinicOpen;
            clinic = clinicToVew;
            clinicCheck = clinicToVew;
            owner = service.GetOwner(clinicToVew); ;
        }

        #endregion

        #region Properties

        private tblClinic clinic;

        public tblClinic Clinic
        {
            get { return clinic; }
            set
            {
                clinic = value;
                OnPropertyChanged("Cinic");
            }
        }

        private tblOwner owner;

        public tblOwner Owner
        {
            get { return owner; }
            set
            {
                owner = value;
                OnPropertyChanged("Owner");
            }
        }

        private tblClinic clinicCheck;

        public tblClinic ClinicCheck
        {
            get
            {
                return clinicCheck;
            }
            set
            {
                clinicCheck = value;
                OnPropertyChanged("ClinicCheck");
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
                service.UpdateClinic(Clinic, Owner);
                updateClinic.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute()
        {
            if (true)
            {
                if (Clinic.ClinicName != null && Clinic.OpenDate != null && Clinic.Adress != null && Clinic.NumberOfFloors != null
                    && Clinic.RoomsByFloor != null && Clinic.OfficeNumber != null && Clinic.NumberOfAmbulanceCarParkings != null
                    && Clinic.NumberOfAmbulanceCarParkings != null && Owner.FullName != null && Owner.JMBG != null)
                {
                    if (service.IsJmbg(Owner.JMBG) && Clinic.NumberOfAmbulanceCarParkings >= ClinicCheck.NumberOfAmbulanceCarParkings
                        && Clinic.NumberOfInvalidEntrances >= ClinicCheck.NumberOfInvalidEntrances)
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
                updateClinic.Close();
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
