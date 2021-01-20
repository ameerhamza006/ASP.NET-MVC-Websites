create database [Symphont LTD]
use [Symphont LTD]
create  table [Registration] 
(
[ID] int primary key identity,
[First Name] varchar(30),
[Last Name] varchar(30),
[Email] nvarchar(50),
[Password] nvarchar(20),
[Gender] varchar(10),
[Date of birth] date,
[phone] nvarchar(20),
[pichure] nvarchar(max),
[C_id] int FOREIGN KEY REFERENCES [Course](C_id),
)
create table [Course]
(
[C_id] int primary key identity,
[Course Name] nvarchar(50),
[Course Descripation] nvarchar(500),
[Course Image] nvarchar(max),
[Course price] money,
[Course start Date] date,
[Course end Date] date,
)
create table [Books]
(
[C_id] int FOREIGN KEY REFERENCES [Course](C_Id),
[B_id] int primary key identity,
[Book Name] varchar(30) not null,
[Book PDF] varchar(max),
[Upload date] date,
)
create table [Fee]
(
[ID] int FOREIGN KEY REFERENCES [Registration](ID),
[F_id] int primary key identity,
[Amount] money,
[status] char(10),
[Pay Date] date,
)
create table [Exam]
(
[ID] int FOREIGN KEY REFERENCES [Registration](ID),
[E_id] int primary key identity,
[Exam Name] char(20),
[Exam Time] time,
[Exam Date] date,
)
create table [FAQS]
(
[FAQS ID] int primary key identity,
[Questions] nvarchar(500),
[Answer] nvarchar(1000),
)

alter table [Registration] add [Country] varchar(100)
alter table [Registration] add [Role] varchar(50)
alter table [Course] add [Course Price] money
truncate table [Registration]
DELETE FROM [Course]
alter table [Registration] drop column [city], [Adderss], [Short bio]

select * from [Registration] 
select * from [Course]
select * from [Books]
select * from [Fee]
select * from [Exam]
select * from [FAQS]