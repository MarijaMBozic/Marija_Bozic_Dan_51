CREATE DATABASE Hospital;
GO

Use Hospital

drop table if exists Request
drop table if exists Patient
drop table if exists Doctor 

create table Doctor (
   DoctorId       int            identity (1,1) primary key,   
   FullName       nvarchar(100)          not null,
   DoctorJMBG     nvarchar(13)   unique  not null,
   BankAccount    nvarchar(100)  unique  not null,
   Username       nvarchar(100)  unique  not null,
   DoctorPassword nvarchar(max)          not null,
)

create table Patient (
   PatientId       int            identity (1,1) primary key,   
   Fullname        nvarchar(100)          not null,
   PatientJMBG	   nvarchar(13)   unique  not null,
   NumInsurce      nvarchar(20)   unique  not null,
   Username        nvarchar(100)  unique  not null,
   PatientPassword nvarchar(max)          not null,
   DoctorId        int                        null,
   FOREIGN KEY (DoctorId) REFERENCES Doctor(DoctorId),
)

create table Request (
	RequestId        int            identity (1,1) primary key,
	Date             date                   default getDate(),
	Reason           nvarchar(100)          not null,
    Company          nvarchar(100)          not null,
	IsUrgent         bit                    not null default 0,
	IsApproved       bit                    null,                     
    PatientId        int                    not null,
    FOREIGN KEY (PatientId) REFERENCES Patient(PatientId),
    DoctorId         int                    null,
    FOREIGN KEY (DoctorId) REFERENCES Doctor(DoctorId)
)

ALTER TABLE Patient
	ADD CONSTRAINT PatientPassword 
	CHECK(LEN([PatientPassword]) >=6 )

ALTER TABLE Doctor
	ADD CONSTRAINT DoctorPassword 
	CHECK(LEN([DoctorPassword]) >=6 )

ALTER TABLE Patient
	ADD CONSTRAINT PatientJMBG 
	CHECK(LEN([PatientJMBG])=13 AND ([PatientJMBG] not like '%[^0-9]%'))

ALTER TABLE Doctor
	ADD CONSTRAINT DoctorJMBG 
	CHECK(LEN([DoctorJMBG])=13 AND ([DoctorJMBG] not like '%[^0-9]%'))

go

CREATE VIEW vwDoctor AS
	SELECT        DoctorId, FullName, DoctorJMBG, BankAccount, Username, DoctorPassword
    FROM            dbo.Doctor

go

CREATE VIEW vwPatient AS
	SELECT        PatientId, Fullname, PatientJMBG, NumInsurce, Username, PatientPassword, DoctorId
	FROM            dbo.Patient
	
go

CREATE VIEW vwRequest AS
	SELECT        dbo.Request.RequestId, dbo.Request.Date, dbo.Request.Reason, dbo.Request.Company, dbo.Request.IsUrgent, dbo.Request.IsApproved, dbo.Request.PatientId, dbo.Request.DoctorId, dbo.Patient.Fullname AS Expr1, 
                         dbo.Patient.PatientId AS Expr2, dbo.Doctor.DoctorId AS Expr3, dbo.Doctor.FullName AS DoctorFullName
	FROM            dbo.Request INNER JOIN
                         dbo.Doctor ON dbo.Request.DoctorId = dbo.Doctor.DoctorId INNER JOIN
                         dbo.Patient ON dbo.Request.PatientId = dbo.Patient.PatientId AND dbo.Doctor.DoctorId = dbo.Patient.DoctorId

