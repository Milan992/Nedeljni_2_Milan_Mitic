IF DB_ID('Clinic') IS NULL
CREATE DATABASE Clinic
GO
USE Clinic;


if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblMaintenance')
drop table tblMaintenance;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblPatient')
drop table tblPatient;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblDoctor')
drop table tblDoctor;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblShift')
drop table tblShift;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblManager')
drop table tblManager;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblAdmin')
drop table tblAdmin;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblAccount')
drop table tblAccount;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblClinic')
drop table tblClinic;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblOwner')
drop table tblOwner;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'vwNumberOfAccounts')
drop view vwNumberOfAccounts;


create table tblOwner(
OwnerID int identity(1, 1) primary key ,
FullName varchar(30) not null,
JMBG varchar(30) check(len(JMBG) = 13)  not null unique,
constraint checkJMBG check(JMBG not like '%[^0-9]%')
)

create table tblClinic(
ClinicID int identity(1,1) primary key,
ClinicName varchar(30) not null,
OpenDate date not null,
OwnerID int foreign key (OwnerID) references tblOwner(OwnerID) not null,
Adress varchar(30),
NumberOfFloors int,
RoomsByFloor int,
OfficeNumber int,
Balcony bit,
Yard bit,
NumberOfAmbulanceCarParkings int,
NumberOfInvalidEntrances int
)

create table tblAccount(
AccountID int identity(1,1) primary key,
FullName varchar(30) not null,
IdCardNumber varchar(30) check(len(IdCardNumber) = 9)  not null unique,
Gender varchar(2) check(Gender in ('M', 'Z', 'N', 'X')) not null,
BirthDate date not null,
Citinzenship varchar(30),
UserName varchar(30) check(len(UserName) > 5) not null unique,
Pass varchar(30) check(len(Pass) > 7) not null,
ClinicID int foreign key (ClinicID) references tblClinic(ClinicID),
constraint CheckIdCard check(IdCardNumber not like '%[^0-9]%')
)

create table tblAdmin(
AdminID int identity(1,1) primary key,
AccountID int foreign key (AccountID) references tblAccount(AccountID) not null,
LoggedIn bit
)

create table tblManager(
ManagerID int identity(1,1) primary key,
AccountID int foreign key (AccountID) references tblAccount(AccountID) not null,
MaxNumberOfDoctors int,
MaxNumberOfRooms int,
NumberOfFails int
)

Create table tblShift(
ShiftID int identity (1,1) primary key,
ShiftName varchar(10)
)

Create table tblDoctor(
DoctorNumber varchar(30) primary key check(len(DoctorNumber) = 5),
AccountID int foreign key (AccountID) references tblAccount(AccountID),
BankAccount varchar(30) check(len(BankAccount) = 10)  not null unique,
Department varchar(30),
ShiftID int foreign key (ShiftID) references tblShift(ShiftID),
PatientReception bit,
ManagerID int foreign key (ManagerID) references tblManager(ManagerID),
constraint CheckBankAccount check(BankAccount not like '%[^0-9]%'),
constraint CheckDoctorNumber check(DoctorNumber not like '%[^0-9]%')
)

Create table tblPatient(
PatientID int identity (1,1) primary key,
AccountID int foreign key (AccountID) references tblAccount(AccountID),
InsuranceCardNumber varchar(30) not null unique,
InsuranceCardExpiry date not null,
DoctorNumber varchar(30) foreign key (DoctorNumber) references tblDoctor(DoctorNumber),
constraint CheckInsuranceCardNumber check(InsuranceCardNumber not like '%[^0-9]%')
)

Create table tblMaintenance(
MaintenanceID int identity (1,1) primary key,
AccountID int foreign key (AccountID) references tblAccount(AccountID),
ExpandingClinicPermision bit,
InvalidEntranceResponsibility bit
)


insert into tblShift (ShiftName)
values ('morning');

insert into tblShift (ShiftName)
values ('day');

insert into tblShift (ShiftName)
values ('night');

insert into tblShift (ShiftName)
values ('24 hour');



