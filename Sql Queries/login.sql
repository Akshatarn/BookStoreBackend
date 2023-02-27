CREATE or Alter procedure SPLogin
@EmailId varchar(100),
 @Password varchar(100)
AS
BEGIN
select *from UserTable where EmailId=@EmailId and Password=@Password
END