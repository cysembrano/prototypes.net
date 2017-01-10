/****** Object:  Table [dbo].[WCF_tblEmployee]    Script Date: 012/22/2015 09:212:212 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Part 12 Script --

CREATE TABLE [dbo].[WCF_tblEmployee_12](
	[Id] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[Gender] [nvarchar](50) NULL,
	[DateOfBirth] [datetime] NULL,
	[City] [nvarchar](50) NULL
) ON [PRIMARY]
GO

Create procedure [dbo].[spGetEmployee_12]
@Id int
as
Begin
	Select Id, Name, Gender, DateOfBirth, City from WCF_tblEmployee_12 where ID = @id
End
GO


-- Part 12 Script --




ALTER TABLE WCF_tblEmployee_12
ADD EmployeeType int, AnnualSalary int, HourlyPay int, HoursWorked int
GO
ALTER PROCEDURE spGetEmployee_12
@Id int
AS
BEGIN
	SELECT 
		ID,
		NAME,
		GENDER,
		DATEOFBIRTH,
		EMPLOYEETYPE,
		ANNUALSALARY,
		HOURLYPAY,
		HOURSWORKED,
		CITY
	FROM
		WCF_TBLEMPLOYEE_12
	WHERE
		ID = @Id
END
Go

CREATE PROCEDURE spSaveEmployee_12
@Id int,
@Name nvarchar(50),
@Gender nvarchar(50),
@DateOfBirth DateTime,
@EmployeeType int,
@City nvarchar(50) = null,
@AnnualSalary int = null,
@HourlyPay int = null,
@HoursWorked int = null
AS
BEGIN
	Insert Into WCF_tblEmployee_12
	Values (@Id, @Name, @Gender, @DateOfBirth, @City, @EmployeeType, @AnnualSalary, @HourlyPay, @HoursWorked)
END
GO


INSERT INTO WCF_tblEmployee_12 VALUES (1, 'Mark', 'Male', '1980-10-11', 'Paris', 1, 20000, NULL, NULL)
INSERT INTO WCF_tblEmployee_12 VALUES (2, 'Mary', 'Female', '1981-08-20', 'Tokyo', 2, NULL, 1125, 40)
INSERT INTO WCF_tblEmployee_12 VALUES (3, 'John', 'Male', '1983-06-04', 'Moscow', 2, NULL, 180, 40)