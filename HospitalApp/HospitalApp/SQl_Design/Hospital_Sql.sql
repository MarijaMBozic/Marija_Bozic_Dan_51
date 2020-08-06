Use Hospital

drop table if exists Request
drop table if exists Patient
drop table if exists Doctor 

create table Doctor (
   DoctorId       int            identity (1,1) primary key,   
   Name           nvarchar(100)          not null,
   Lastname       nvarchar(100)          not null,
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
   DoctorId        int        ,
   FOREIGN KEY (DoctorId) REFERENCES Doctor(DoctorId),
)

create table Request (
	RequestId        int            identity (1,1) primary key,
	Date             date                   not null,
	Reason           nvarchar(100)          not null,
    Company          nvarchar(100)          not null,
	IsUrgent         bit                    ,
	IsApproved       bit                    null,                     
    PatientId        int                    not null,
    FOREIGN KEY (PatientId) REFERENCES Patient(PatientId),
    DoctorId         int                    not null,
    FOREIGN KEY (DoctorId) REFERENCES Doctor(DoctorId),
)

ALTER TABLE Patient
	ADD CONSTRAINT PatientPassword 
	CHECK(LEN([PatientPassword]) >=6 )

ALTER TABLE Doctor
	ADD CONSTRAINT DoctorPassword 
	CHECK(LEN([DoctorPassword]) >=6 )

ALTER TABLE Patient
	ADD CONSTRAINT PatientJMBG 
	CHECK(LEN([PatientJMBG]) >=13 AND ([PatientJMBG] not like '%[^0-9]%'))

ALTER TABLE Doctor
	ADD CONSTRAINT DoctorJMBG 
	CHECK(LEN([DoctorJMBG]) >=13 AND ([DoctorJMBG] not like '%[^0-9]%'))
