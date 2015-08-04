/****** Object:  Table [dbo].[WCF_tblEmployee2]    Script Date: 07/22/2015 09:27:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


IF NOT EXISTS (
	SELECT
		*
	FROM
		SYS.TABLES T
	WHERE
		(T.[name] = 'WCF_tblEmployee2')
	)
BEGIN
	PRINT 'Creating Table [WCF_tblEmployee2]';
	
	CREATE TABLE [WCF_tblEmployee2]
	(
		 	 [Id] 				[int] NULL
			,[Name] 			[nvarchar](50) NULL
			,[Gender] 			[nvarchar](50) NULL
			,[DateOfBirth] 		[datetime] NULL
			,[EmployeeType]		[int] NULL
			,[AnnualSalary]		[int] NULL
			,[HourlyPay]		[int] NULL
			,[HoursWorked]		[int] NULL
	);
END
GO

INSERT INTO WCF_tblEmployee2 VALUES (1, 'Mark', 'Male', '1980-10-11', 1, 60000,NULL,NULL)
INSERT INTO WCF_tblEmployee2 VALUES (2, 'Mary', 'Female', '1981-08-20', 2, NULL, 250, 40)
INSERT INTO WCF_tblEmployee2 VALUES (3, 'John', 'Male', '1983-06-04',3, NULL, 300, 40)


IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE ([object_id] = OBJECT_ID('spGetEmployee2')) AND ([type] in (N'P', N'PC')))
BEGIN
	PRINT 'Dropping Procedure [dbo].[spGetEmployee2]';
	DROP PROCEDURE [dbo].[spGetEmployee2];
END;
GO

PRINT 'Creating Procedure [dbo].[spGetEmployee2]'
Create procedure [dbo].[spGetEmployee2]
@Id int
as
Begin
	Select 
		Id, 
		Name, 
		Gender, 
		DateOfBirth,
		EmployeeType,
		AnnualSalary,
		HourlyPay,
		HoursWorked
	from WCF_tblEmployee2 
	where ID = @id
End
GO


IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE ([object_id] = OBJECT_ID('spSaveEmployee2')) AND ([type] in (N'P', N'PC')))
BEGIN
	PRINT 'Dropping Procedure [dbo].[spSaveEmployee2]';
	DROP PROCEDURE [dbo].[spSaveEmployee2];
END;
GO

PRINT 'Create Procedure [dbo].[spSaveEmployee2]'
Create procedure [dbo].[spSaveEmployee2]
@id int, 
@name nvarchar(50),
@gender nvarchar(50),
@DateOfBirth DateTime,
@Employeetype int,
@AnnualSalary int, 
@HourlyPay int,
@HoursWorked int
as
Begin
	Insert into WCF_tblEmployee2
	values (@id, @name, @gender, @DateOfBirth, @Employeetype, @AnnualSalary, @HourlyPay, @HoursWorked)
End
GO

