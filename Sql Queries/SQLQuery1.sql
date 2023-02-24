Create database BookStrore

Create table UserTable(
UserId int not null primary key identity(1,1),
FullName varchar(100),
EmailId varchar(100),
Password varchar(100),
PhoneNum bigint
);

Select *from UserTable;



Create or Alter procedure SPRegistration
 @FullName varchar(100),
 @EmailId varchar(100),
 @Password varchar(100),
 @PhoneNum bigint
AS 
insert into UserTable values(@FullName,@EmailId,@Password,@PhoneNum)
GO
