using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using WpfClinic.Model;
using WpfClinic.Views;

namespace WpfClinic
{
    class Service
    {  /// <summary>
       /// Checks if string is in JMBG format.
       /// </summary>
       /// <param name="userName"></param>
       /// <returns></returns>
        public bool IsJmbg(string jmbg)
        {
            bool isjmbg = false;
            if (jmbg.Length == 13)
            {
                try
                {
                    long i = Convert.ToInt64(jmbg);
                    string date = "1" + jmbg.Substring(4, 3) + "-" + jmbg.Substring(2, 2) + "-" + jmbg.Substring(0, 2);
                    DateTime dateOfBirth = DateTime.ParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    isjmbg = true;
                }
                catch
                {
                    isjmbg = false;
                }
            }
            else
            {
                isjmbg = false;
            }
            return isjmbg;
        }

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

        internal bool IsMaintenance(string userName, string password)
        {
            try
            {
                tblAccount account = new tblAccount();
                tblMaintenance maintenance = new tblMaintenance();
                using (ClinicEntities context = new ClinicEntities())
                {
                    account = GetAccount(userName, password);
                    maintenance = (from e in context.tblMaintenances where e.AccountID == account.AccountID select e).First();
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

                        Admin administrator = new Admin(admin);
                        administrator.ShowDialog();
                    }
                    else
                    {
                        Admin administrator = new Admin(admin);
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

        public tblManager GetManager(tblAccount account)
        {
            using (ClinicEntities context = new ClinicEntities())
            {
                tblManager manager = (from m in context.tblManagers where m.AccountID == account.AccountID select m).First();
                return manager;
            }
        }

        public tblMaintenance GetMaintenance(tblAccount account)
        {
            using (ClinicEntities context = new ClinicEntities())
            {
                tblMaintenance maintenance = (from m in context.tblMaintenances where m.AccountID == account.AccountID select m).First();
                return maintenance;
            }
        }

        public tblAdmin GetAdmin(tblAccount account)
        {
            using (ClinicEntities context = new ClinicEntities())
            {
                tblAdmin admin = (from a in context.tblAdmins where a.AccountID == account.AccountID select a).First();
                return admin;
            }
        }

        public tblPatient GetPatient(tblAccount account)
        {
            using (ClinicEntities context = new ClinicEntities())
            {
                tblPatient patient = (from a in context.tblPatients where a.AccountID == account.AccountID select a).First();
                return patient;
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

        /// <summary>
        /// Adds an account to tblAccount and an doctor with account's AccountID in tblDoctor.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="birthDate"></param>
        /// <param name="clinic"></param>
        internal void AddDoctor(tblAccount account, tblDoctor doctor, tblClinic clinic, tblShift shift, bool reception, tblManager manager, string birthDate)
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
                newAccount.ClinicID = clinic.ClinicID;
                newAccount.BirthDate = DateTime.ParseExact(birthDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                context.tblAccounts.Add(newAccount);
                context.SaveChanges();

                tblDoctor newDoctor = new tblDoctor();
                newDoctor.DoctorNumber = doctor.DoctorNumber;
                newDoctor.AccountID = newAccount.AccountID;
                newDoctor.BankAccount = doctor.BankAccount;
                newDoctor.Department = doctor.Department;
                newDoctor.ShiftID = shift.ShiftID;
                newDoctor.PatientReception = reception;
                newDoctor.ManagerID = manager.ManagerID;
                context.tblDoctors.Add(newDoctor);
                context.SaveChanges();

                MessageBox.Show("Doctor saved.");
            }
        }

        /// <summary>
        /// Adds an account to tblAccount and an maintenance with account's AccountID in tblMaintenance.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="birthDate"></param>
        /// <param name="clinic"></param>
        internal void AddMaintenance(tblAccount account, tblClinic clinic, tblMaintenance maintenance, string birthDate)
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
                newAccount.ClinicID = clinic.ClinicID;
                newAccount.BirthDate = DateTime.ParseExact(birthDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                context.tblAccounts.Add(newAccount);
                context.SaveChanges();

                tblMaintenance newMaintenance = new tblMaintenance();
                newMaintenance.AccountID = newAccount.AccountID;
                if (maintenance.ExpandingClinicPermision == true)
                {
                    newMaintenance.ExpandingClinicPermision = maintenance.ExpandingClinicPermision;
                    newMaintenance.InvalidEntranceResponsibility = false;
                }
                else
                {
                    newMaintenance.ExpandingClinicPermision = false;
                    newMaintenance.InvalidEntranceResponsibility = true;
                }
                context.tblMaintenances.Add(newMaintenance);
                context.SaveChanges();

                MessageBox.Show("Maintenance account saved.");
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
                newAccount.ClinicID = clinic.ClinicID;
                newAccount.BirthDate = DateTime.ParseExact(birthDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                context.tblAccounts.Add(newAccount);
                context.SaveChanges();

                tblManager newManager = new tblManager();
                newManager.AccountID = newAccount.AccountID;
                newManager.MaxNumberOfDoctors = manager.MaxNumberOfDoctors;
                newManager.MaxNumberOfRooms = manager.MaxNumberOfRooms;
                newManager.NumberOfFails = 0;
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
                newAccount.ClinicID = clinic.ClinicID;
                newAccount.BirthDate = DateTime.ParseExact(birthDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                context.tblAccounts.Add(newAccount);
                context.SaveChanges();

                tblPatient newPatient = new tblPatient();
                newPatient.AccountID = newAccount.AccountID;
                newPatient.InsuranceCardNumber = patient.InsuranceCardNumber;
                newPatient.DoctorNumber = (from d in context.tblDoctors where d.AccountID == doctor.AccountID select d.DoctorNumber).First();
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

        /// <summary>
        /// Gets all shifts from tblShift in database and adds them to a list.
        /// </summary>
        /// <returns></returns>
        public List<tblShift> GetAllShifts()
        {
            using (ClinicEntities context = new ClinicEntities())
            {
                List<tblShift> list = (from s in context.tblShifts select s).ToList();
                return list;
            }
        }

        public List<tblClinic> GetAllClinics()
        {
            try
            {
                using (ClinicEntities context = new ClinicEntities())
                {
                    List<tblClinic> list = (from c in context.tblClinics select c).ToList();
                    return list;
                }
            }
            catch
            {
                return null;
            }
        }

        internal string GetRiskPatientsAge()
        {
            string s;
            List<string> list = new List<string>();
            int age = 0;
            using (StreamReader sr = new StreamReader(@"..\..\AtRiskPatients.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    list.Add(line);
                }
                foreach (var item in list)
                {
                    string[] array = item.Split(',', ':');
                    age = age + Convert.ToInt32(array[2]);
                }
            }
            double average = age / list.Count;
            s = Convert.ToString(Convert.ToInt32(average));

            return "average age of patients with risk sympthoms is: " + s;
        }

        public string Statistics(tblAdmin admin)
        {
            using (ClinicEntities context = new ClinicEntities())
            {
                tblAccount adminAccount = (from a in context.tblAccounts where a.AccountID == admin.AccountID select a).First();
                List<tblAccount> accounts = (from a in context.tblAccounts where adminAccount.ClinicID == a.ClinicID select a).ToList();
                List<tblPatient> patients = new List<tblPatient>();
                foreach (var item in accounts)
                {
                    try
                    {
                        tblPatient patient = (from p in context.tblPatients where p.AccountID == item.AccountID select p).First();
                        patients.Add(patient);
                    }
                    catch
                    {
                        continue;
                    }
                }
                int employees = accounts.Count - patients.Count;
                int patientsNumber = patients.Count;
                return "Number of employees: " + employees.ToString() + ".\nNumber of patients: " + patientsNumber.ToString();
            }
        }

        public void AddMaintenanceReport(int hours, string description, tblMaintenance maintenance)
        {
            using (StreamWriter sw = new StreamWriter(@"..\..\MaintenanceReports" + maintenance.MaintenanceID + ".txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString() + " Duration: " + hours.ToString() + " hours, Description: " + description);
            }
        }

        public List<Service> GetMaintenanceReports(tblMaintenance maintenance)
        {
            List<Service> list = new List<Service>();
            try
            {
                using (StreamReader sr = new StreamReader(@"..\..\MaintenanceReports" + maintenance.MaintenanceID + ".txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Service a = new Service();
                        a.Report = line;
                        list.Add(a);
                    }
                }
            }
            catch
            {
                Service a = new Service();
                a.Report = "no reports";
                list.Add(a);
            }
            return list;
        }
        public string Report { get; set; }
    }
}


