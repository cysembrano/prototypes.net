/****** Object:  Table [dbo].[WCF_tblEmployee]    Script Date: 07/22/2015 09:27:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Part 6 Script --

CREATE TABLE [dbo].[WCF_tblEmployee_7](
	[Id] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[Gender] [nvarchar](50) NULL,
	[DateOfBirth] [datetime] NULL
) ON [PRIMARY]
GO

Create procedure [dbo].[spGetEmployee_7]
@Id int
as
Begin
	Select Id, Name, Gender, DateOfBirth from WCF_tblEmployee_7 where ID = @id
End
GO

Create procedure [dbo].[spSaveEmployee_7]
@id int, 
@name nvarchar(50),
@gender nvarchar(50),
@DateOfBirth DateTime
as
Begin
	Insert into Wcf_tblEmployee_7
	values (@id, @name, @gender, @DateOfBirth)
End
GO


-- Part 6 Script --




ALTER TABLE WCF_tblEmployee_7
ADD EmployeeType int, AnnualSalary int, HourlyPay int, HoursWorked int
GO
ALTER PROCEDURE spGetEmployee_7
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
		HOURSWORKED
	FROM
		WCF_TBLEMPLOYEE_7
	WHERE
		ID = @Id
END
Go

ALTER PROCEDURE spSaveEmployee_7
@Id int,
@Name nvarchar(50),
@Gender nvarchar(50),
@DateOfBirth DateTime,
@EmployeeType int,
@AnnualSalary int = null,
@HourlyPay int = null,
@HoursWorked int = null
AS
BEGIN
	Insert Into WCF_tblEmployee_7
	Values (@Id, @Name, @Gender, @DateOfBirth, @EmployeeType, @AnnualSalary, @HourlyPay, @HoursWorked)
END
GO


INSERT INTO WCF_tblEmployee_7 VALUES (1, 'Mark', 'Male', '1980-10-11', 1, 20000, NULL, NULL)
INSERT INTO WCF_tblEmployee_7 VALUES (2, 'Mary', 'Female', '1981-08-20',2, NULL, 175, 40)
INSERT INTO WCF_tblEmployee_7 VALUES (3, 'John', 'Male', '1983-06-04', 2, NULL, 180, 40)