using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfClinic.Model;
using WpfClinic.Views;

namespace WpfClinic
{
    class Service
    {
        /// <summary>
        /// Checks if userName and password match first two rows of the ClinicAccess.txt file.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        internal bool IsMaster(string userName, string password)
        {
            try
            {
                string[] userPass = new string[2];
                int couter = 0;

                using (StreamReader sr = new StreamReader("../../ClinicAccess.txt"))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (couter > 1)
                        {
                            break;
                        }
                        userPass[couter] = line;
                        couter++;
                    }
                }
                if (userPass[0] == userName && userPass[1] == password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }

        /// <summary>
        /// Checks if there is an account in tblAccount with the userName and password.
        /// Checks if there is a Doctor with the account's AccountID.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        internal bool IsDoctor(string userName, string password)
        {
            try
            {
                tblAccount account = new tblAccount();
                tblDoctor doctor = new tblDoctor();
                using (ClinicEntities context = new ClinicEntities())
                {
                    account = GetAccount(userName, password);
                    doctor = (from e in context.tblDoctors where e.AccountID == account.AccountID select e).First();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if there is an account in tblAccount with the userName and password.
        /// Checks if there is a Manager with the account's AccountID.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        internal bool IsManager(string userName, string password)
        {
            try
            {
                tblAccount account = new tblAccount();
                tblManager manager = new tblManager();
                using (ClinicEntities context = new ClinicEntities())
                {
                    account = GetAccount(userName, password);
                    manager = (from e in context.tblManagers where e.AccountID == account.AccountID select e).First();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if there is an account in tblAccount with the userName and password.
        /// Checks if there is an Admin with the account's AccountID.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        internal bool IsAdmin(string userName, string password)
        {
            try
            {
                tblAccount account = new tblAccount();
                tblAdmin admin = new tblAdmin();
                using (ClinicEntities context = new ClinicEntities())
                {
                    account = GetAccount(userName, password);
                    admin = (from e in context.tblAdmins where e.AccountID == account.AccountID select e).First();
                    if (admin.LoggedIn == false)
                    {
                        MessageBox.Show("As this is your first LogIn. Please crete a Clinic you want to administrate.");
                        AddClinic addClinic = new AddClinic(account);
                        addClinic.ShowDialog();

                        Admin administrator = new Admin(account);
                        administrator.ShowDialog();
                    }
                    else
                    {
                        Admin administrator = new Admin(account);
                        administrator.ShowDialog();
                    }
                    admin.LoggedIn = true;
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if there is an account in tblAccount with the userName and password.
        /// Checks if there is a Patient with the account's AccountID.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        internal bool IsPatient(string userName, string password)
        {
            try
            {
                tblAccount account = new tblAccount();
                tblPatient patient = new tblPatient();
                using (ClinicEntities context = new ClinicEntities())
                {
                    account = GetAccount(userName, password);
                    patient = (from e in context.tblPatients where e.AccountID == account.AccountID select e).First();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Returns a tblAccount object with the userName and password.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public tblAccount GetAccount(string userName, string password)
        {
            using (ClinicEntities context = new ClinicEntities())
            {
                tblAccount account = (from a in context.tblAccounts where a.UserName == userName && a.Pass == password select a).First();
                return account;
            }
        }

        /// <summary>
        /// Adds an account to tblAccount and an admin with account's AccountID in tblAdmin.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="birthDate"></param>
        /// <param name="clinic"></param>
        internal void AddAdmin(tblAccount account, string birthDate, tblClinic clinic)
        {
            using (ClinicEntities context = new ClinicEntities())
            {
                tblAccount newAccount = new tblAccount();
                newAccount.FullName = account.FullName;
                newAccount.IdCardNumber = account.IdCardNumber;
                newAccount.Gender = account.Gender;
                newAccount.Citinzenship = account.Citinzenship;
                newAccount.UserName = account.UserName;
                newAccount.Pass = account.Pass;
                newAccount.BirthDate = DateTime.ParseExact(birthDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                newAccount.ClinicID = clinic.ClinicID;
                context.tblAccounts.Add(newAccount);
                context.SaveChanges();

                tblAdmin newAdmin = new tblAdmin();
                newAdmin.AccountID = newAccount.AccountID;
                context.tblAdmins.Add(newAdmin);
                context.SaveChanges();

                MessageBox.Show("Administrator saved.");
            }
        }

        internal void AddClinic(tblClinic clinic, tblOwner owner, tblAccount accountToEdit, string date, bool yard, bool balcony)
        {
            using (ClinicEntities context = new ClinicEntities())
            {
                tblOwner newOwner = new tblOwner();
                newOwner.FullName = owner.FullName;
                newOwner.JMBG = owner.JMBG;
                context.tblOwners.Add(newOwner);
                context.SaveChanges();

                tblClinic newClinic = new tblClinic();
                newClinic.ClinicName = clinic.ClinicName;
                newClinic.Adress = clinic.Adress;
                newClinic.NumberOfFloors = clinic.NumberOfFloors;
                newClinic.RoomsByFloor = clinic.RoomsByFloor;
                newClinic.OfficeNumber = clinic.OfficeNumber;
                newClinic.Balcony = yard;
                newClinic.Yard = balcony;
                newClinic.NumberOfInvalidEntrances = clinic.NumberOfInvalidEntrances;
                newClinic.NumberOfAmbulanceCarParkings = clinic.NumberOfAmbulanceCarParkings;
                newClinic.OpenDate = DateTime.ParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                newClinic.OwnerID = newOwner.OwnerID;
                context.tblClinics.Add(newClinic);
                context.SaveChanges();

                tblAccount account = (from a in context.tblAccounts where a.AccountID == accountToEdit.AccountID select a).First();
                account.ClinicID = newClinic.ClinicID;
                context.SaveChanges();

                MessageBox.Show("Clinic saved.");
            }
        }

        /// <summary>
        /// Adds an account to tblAccount and an admin with account's AccountID in tblAdmin.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="birthDate"></param>
        /// <param name="clinic"></param>
        internal void AddAdmin(tblAccount account, string birthDate)
        {
            using (ClinicEntities context = new ClinicEntities())
            {
                tblAccount newAccount = new tblAccount();
                newAccount.FullName = account.FullName;
                newAccount.IdCardNumber = account.IdCardNumber;
                newAccount.Gender = account.Gender;
                newAccount.Citinzenship = account.Citinzenship;
                newAccount.UserName = account.UserName;
                newAccount.Pass = account.Pass;
                newAccount.BirthDate = DateTime.ParseExact(birthDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                context.tblAccounts.Add(newAccount);
                context.SaveChanges();

                tblAdmin newAdmin = new tblAdmin();
                newAdmin.AccountID = newAccount.AccountID;
                newAdmin.LoggedIn = false;
                context.tblAdmins.Add(newAdmin);
                context.SaveChanges();
                MessageBox.Show("Administrator saved.");
            }
        }

        /// <summary>
        /// Adds an account to tblAccount and an manager with account's AccountID in tblManager.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="birthDate"></param>
        /// <param name="clinic"></param>
        internal void AddManager(tblAccount account, string birthDate, tblClinic clinic, tblManager manager)
        {
            using (ClinicEntities context = new ClinicEntities())
            {
                tblAccount newAccount = new tblAccount();
                newAccount.FullName = account.FullName;
                newAccount.IdCardNumber = account.IdCardNumber;
                newAccount.Gender = account.Gender;
                newAccount.Citinzenship = account.Citinzenship;
                newAccount.UserName = account.UserName;
                newAccount.Pass = account.Pass;
                newAccount.BirthDate = DateTime.ParseExact(birthDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                context.tblAccounts.Add(newAccount);
                context.SaveChanges();

                tblManager newManager = new tblManager();
                newManager.MaxNumberOfDoctors = manager.MaxNumberOfDoctors;
                newManager.MaxNumberOfRooms = manager.MaxNumberOfRooms;
                context.tblManagers.Add(newManager);
                context.SaveChanges();
                MessageBox.Show("Manager saved.");
            }
        }

        /// <summary>
        /// Adds an account to tblAccount and an patient with account's AccountID in tblPatient.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="birthDate"></param>
        /// <param name="clinic"></param>
        internal void AddPatient(tblAccount account, string birthDate, tblClinic clinic, tblAccount doctor, tblPatient patient, string expiry)
        {
            using (ClinicEntities context = new ClinicEntities())
            {
                tblAccount newAccount = new tblAccount();
                newAccount.FullName = account.FullName;
                newAccount.IdCardNumber = account.IdCardNumber;
                newAccount.Gender = account.Gender;
                newAccount.Citinzenship = account.Citinzenship;
                newAccount.UserName = account.UserName;
                newAccount.Pass = account.Pass;
                newAccount.BirthDate = DateTime.ParseExact(birthDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                context.tblAccounts.Add(newAccount);
                context.SaveChanges();

                tblPatient newPatient = new tblPatient();
                newPatient.InsuranceCardNumber = patient.InsuranceCardNumber;
                newPatient.InsuranceCardExpiry = DateTime.ParseExact(expiry, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                context.tblPatients.Add(newPatient);
                context.SaveChanges();
                MessageBox.Show("Patient saved.");
            }
        }

        /// <summary>
        /// Gets all doctors from tblDoctor and adds them to a list.
        /// Returns a list of accouts from tblAccount who are also doctors.
        /// </summary>
        /// <returns></returns>
        internal List<tblAccount> GetAllDoctors()
        {
            try
            {
                using (ClinicEntities context = new ClinicEntities())
                {
                    List<tblDoctor> doctors = (from d in context.tblDoctors select d).ToList();
                    List<tblAccount> accounts = new List<tblAccount>();
                    foreach (var doctor in doctors)
                    {
                        try
                        {
                            tblAccount account = (from a in context.tblAccounts where doctor.AccountID == a.AccountID select a).First();
                            accounts.Add(account);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    return accounts;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}

