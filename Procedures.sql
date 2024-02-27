CREATE TABLE tblEmployeeForm(EmployeeId int Primary Key,FirstName varchar(50),MiddleName varchar(50),LastName varchar(50),CourseID int,GenderId int,PhoneNumber varchar(12),Email varchar(50),Password nvarchar(50))

CREATE TABLE tblEmpCourses (
    CourseID INT PRIMARY KEY,
    CourseName VARCHAR(50),
    --EmployeeId INT,
    --FOREIGN KEY (EmployeeId) REFERENCES tblEmployeeForm(EmployeeId)
);
CREATE TABLE tblEmpGender(
GenderId int primary key,
GenderName varchar(50)
)


--Author : Prashant Khode
--Description : To insert update Employee Details
--Created By : Prashant Khode

CREATE PROC tblEmployeeForm_Insert_Update
(
@P_EmployeeId INT=0,
@P_FirstName varchar(50)=NULL,
@P_MiddleName varchar(50)=NULL,
@P_LastName varchar(50)=NULL,
@P_CourseID int,
@P_GenderId int,
@P_PhoneNumber varchar(12),
@P_Email varchar(50),
@P_Password nvarchar(50)
)
AS
BEGIN
if(@P_EmployeeId=0)
BEGIN

END
ELSE
BEGIN

END

END