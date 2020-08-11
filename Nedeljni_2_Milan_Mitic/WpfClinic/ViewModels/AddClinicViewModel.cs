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
    class AddClinicViewModel : ViewModelBase
    {
        AddClinic addClinic;
        Service service = new Service();

        #region Constructors

        public AddClinicViewModel(AddClinic addClinicOpen)
        {
            clinic = new tblClinic();
            addClinic = addClinicOpen;
        }

        public AddClinicViewModel(AddClinic addClinicOpen, tblAccount account)
        {
            clinic = new tblClinic();
            owner = new tblOwner();
            accountToEdit = account;
            addClinic = addClinicOpen;
        }

        #endregion

        #region Properties

        private tblAccount accountToEdit;

        public tblAccount AccountToEdit
        {
            get { return accountToEdit; }
            set
            {
                accountToEdit = value;
                OnPropertyChanged("AccountToEdit");
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

        private string date;

        public string Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        private bool yard;

        public bool Yard
        {
            get { return yard; }
            set { yard = value;
                OnPropertyChanged("Yard");
            }
        }

        private bool balcony;

        public bool Balcony
        {
            get { return balcony; }
            set
            {
                balcony = value;
                OnPropertyChanged("Balcony");
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
                service.AddClinic(Clinic, Owner, AccountToEdit, Date, Yard, Balcony);
                addClinic.Close();
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
                if (true)
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
                addClinic.Close();
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
