/****** Object:  Table [dbo].[WCF_tblEmployee]    Script Date: 07/22/2015 09:27:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE TABLE [dbo].[WCF_tblEmployee_6](
	[Id] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[Gender] [nvarchar](50) NULL,
	[DateOfBirth] [datetime] NULL
) ON [PRIMARY]

GO




Create procedure [dbo].[spGetEmployee_6]
@Id int
as
Begin
	Select Id, Name, Gender, DateOfBirth from WCF_tblEmployee_6 where ID = @id
End
GO



Create procedure [dbo].[spSaveEmployee_6]
@id int, 
@name nvarchar(50),
@gender nvarchar(50),
@DateOfBirth DateTime
as
Begin
	Insert into Wcf_tblEmployee_6
	values (@id, @name, @gender, @DateOfBirth)
End
GO


INSERT INTO WCF_tblEmployee_6 VALUES (1, 'Mark', 'Male', '1980-10-11')
INSERT INTO WCF_tblEmployee_6 VALUES (2, 'Mary', 'Female', '1981-08-20')
INSERT INTO WCF_tblEmployee_6 VALUES (3, 'John', 'Male', '1983-06-04')
