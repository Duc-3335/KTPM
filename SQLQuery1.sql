use master
go
create database Zola
go
use Zola
go
create table LienHe(
	SoDT varchar(50) primary key
	,Ten nvarchar(100)
)

create table TinNhan(
	ID int primary key identity(1,1)
	,NoiDung nvarchar(1000)
	,NguoiGuiID varchar(50) foreign key references LienHe(SoDT)
	,NguoiNhanID varchar(50) foreign key references LienHe(SoDT)

) 
go
