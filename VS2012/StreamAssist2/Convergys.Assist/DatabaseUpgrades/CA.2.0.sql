SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
----------------------------------------------------------------
-- Convergys ASSIST UPGRADE SCRIPT 2.0
-- Create Table: tblCallbackLogs_Lines
-- Create Table: CA_Sys_Version
-- Create Table: CA_Sys_Logs 
----------------------------------------------------------------
----------------------------------------------------------------
--DROP SCRIPT
----------------------------------------------------------------
PRINT '---------------------------------------------------------';
PRINT 'DB 2.0 UPGRADE BEGINS';
PRINT '---------------------------------------------------------';

IF EXISTS ( SELECT TOP 1 * FROM SYS.TABLES T WHERE (T.[name] = 'CA_Sys_Version') )
BEGIN
	PRINT 'Drop CA_Sys_Version Table';
	DROP TABLE CA_Sys_Version
END;
GO

IF EXISTS ( SELECT TOP 1 * FROM SYS.TABLES T WHERE (T.[name] = 'CA_Sys_Logs') )
BEGIN
	PRINT 'Drop CA_Sys_Logs Table';
	DROP TABLE CA_Sys_Logs
END;
GO

IF EXISTS 
(
	SELECT TOP 1 * FROM SYS.FOREIGN_KEYS FK
	INNER JOIN SYS.TABLES T ON T.[Object_ID] = FK.Parent_object_id
	WHERE FK.[Name] = 'FK_tblEmp_Schedule_tblEmp_ScheduleLine' 
	AND T.[Name] = 'tblEmp_ScheduleLine'
)
BEGIN
	PRINT 'Dropping Foreign Key [FK_tblEmp_Schedule_tblEmp_ScheduleLine]';
	ALTER TABLE streamassist_dbo.tblEmp_ScheduleLine 
	DROP CONSTRAINT FK_tblEmp_Schedule_tblEmp_ScheduleLine;
END;
GO


IF EXISTS ( SELECT TOP 1 * FROM SYS.TABLES T WHERE (T.[name] = 'tblEmp_Schedule') )
BEGIN
	PRINT 'Drop tblEmp_Schedule Table';
	DROP TABLE [streamassist_dbo].[tblEmp_Schedule]
END;
GO

IF EXISTS ( SELECT TOP 1 * FROM SYS.TABLES T WHERE (T.[name] = 'tblEmp_ScheduleLine') )
BEGIN
	PRINT 'Drop tblEmp_ScheduleLine Table';
	DROP TABLE [streamassist_dbo].[tblEmp_ScheduleLine]
END;
GO


----------------------------------------------------------------
--CREATE SCRIPT
----------------------------------------------------------------

-- Create streamassist_dbo.tblEmp_Schedule
IF NOT EXISTS(
    SELECT * FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_NAME = 'tblEmp_Schedule' AND TABLE_TYPE = 'BASE TABLE'
    )
BEGIN
	PRINT 'Create Table: streamassist_dbo.tblEmp_Schedule';
	CREATE TABLE [streamassist_dbo].[tblEmp_Schedule] (
		[Id] [int] IDENTITY (1, 1) NOT NULL,
		[Emp_ID] [varchar] (30) NOT NULL,
		[ScheduleDate] [datetime] NOT NULL,
		[ScheduleDetail] [varchar] (2000) NULL,
		[Adj_Schedule_Date_Start] [smalldatetime] NULL,
		[Adj_Schedule_Date_End] [smalldatetime] NULL,
		[LoadedDate] [datetime] NOT NULL DEFAULT(GETDATE()),
		
		CONSTRAINT [PK_tblEmp_Schedule] PRIMARY KEY CLUSTERED([Id] ASC)
	)
END;
GO

-- Create streamassist_dbo.tblEmp_ScheduleLine
IF NOT EXISTS(
    SELECT * FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_NAME = 'tblEmp_ScheduleLine' AND TABLE_TYPE = 'BASE TABLE'
    )
BEGIN
	PRINT 'Create Table: streamassist_dbo.tblEmp_ScheduleLine';
	CREATE TABLE [streamassist_dbo].[tblEmp_ScheduleLine] (
		[Id] [int] IDENTITY (1, 1) NOT NULL,
		[ScheduleId] [int] NOT NULL,
		[Activity] [varchar] (250) NOT NULL,
		[ActivityStart] [varchar] (10) NOT NULL,
		[ActivityEnd] [varchar] (10)NOT NULL,
		[LoadedDate] [datetime] NOT NULL DEFAULT(GETDATE()),
		
		CONSTRAINT [PK_tblEmp_ScheduleLine] PRIMARY KEY CLUSTERED([Id] ASC),
		CONSTRAINT [FK_tblEmp_Schedule_tblEmp_ScheduleLine] FOREIGN KEY ([ScheduleId])
			REFERENCES [streamassist_dbo].[tblEmp_Schedule]([Id])
	)
END;
GO

-- Create Sys_Version Table
IF NOT EXISTS(
    SELECT * FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_NAME = 'CA_Employee_Stage' AND TABLE_TYPE = 'BASE TABLE'
    )
BEGIN
  PRINT 'Create Table: CA_Employee_Stage'
  CREATE TABLE dbo.CA_Employee_Stage (
          [Emp_Id] 					[nvarchar](30) 		NOT NULL,
		  [First_Name]				[nvarchar](20)	  		NULL,
		  [Middle_Name]				[nvarchar](60)	  		NULL,
		  [Last_Name]				[nvarchar](40)	  		NULL,
		  [Job_Name]				[nvarchar](365)	  		NULL,
		  [Location]				[nvarchar](20) 		NOT NULL,
		  [Supervisor_id]			[nvarchar](30)	  		NULL,
		  [agent_nonagent]			[nvarchar](5)	  		NULL,
		  [Date_Of_Hire]			[smalldatetime]		NOT	NULL,
		  [Actual_Termination_Date]	[smalldatetime]			NULL,
		  [Adjusted_Hire_Date]		[nvarchar](50)			NULL,
		  [Person_Type]				[nvarchar](80)			NULL,
		  [Supervisor]				[nvarchar](240)			NULL,
		  [FLSA]					[nvarchar](50)			NULL,
		  [Department]				[nvarchar](50)			NULL,
		  [name_lfm]				[nvarchar](250)			NULL,
		  [name_fml]				[nvarchar](250)			NULL,
		  [oracle_id]				[nvarchar](50)			NULL
          )
END;
GO


-- Create Sys_Logs
IF NOT EXISTS(
    SELECT * FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_NAME = 'CA_Sys_Logs' AND TABLE_TYPE = 'BASE TABLE'
    )
BEGIN
	PRINT 'Create Table: CA_Sys_Logs';
	CREATE TABLE [dbo].[CA_Sys_Logs] (
		[Id] [int] IDENTITY (1, 1) NOT NULL,
		[Date] [datetime] NOT NULL,
		[Thread] [nvarchar] (255) NOT NULL,
		[Level] [nvarchar] (50) NOT NULL,
		[Logger] [nvarchar] (255) NOT NULL,
		[Message] [nvarchar] (4000) NOT NULL,
		[Exception] [nvarchar] (MAX) NULL		
	)
END;
GO


-- Create Sys_Version Table
IF NOT EXISTS(
    SELECT * FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_NAME = 'CA_Sys_Version' AND TABLE_TYPE = 'BASE TABLE'
    )
 BEGIN
  PRINT 'Create Table: CA_Sys_Version'
  CREATE TABLE dbo.CA_Sys_Version (
          [Major] int NOT NULL,
          [Minor] int NOT NULL
          )
 END;
GO

----------------------------------------------------------------
--ALTER SCRIPT
----------------------------------------------------------------


IF NOT EXISTS
(
	SELECT TOP 1 * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
	WHERE CONSTRAINT_NAME = 'PK_tblPreferences'
)
BEGIN
ALTER TABLE streamassist_dbo.tblPreferences ADD CONSTRAINT
	PK_tblPreferences PRIMARY KEY CLUSTERED 
	(
	PreferenceId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
END;
GO



IF EXISTS ( SELECT * FROM SYS.TABLES T WHERE (T.[name] = 'tblCallbackLogs') )
BEGIN
	IF NOT EXISTS 
	(
		SELECT TOP 1 * FROM 
					SYS.TABLES T 
		INNER JOIN SYS.COLUMNS C ON C.OBJECT_ID = T.OBJECT_ID 
		WHERE C.NAME = 'CallbackReasonTypeIdRef' AND T.NAME = 'tblCallbackLogs'
	)
	BEGIN
		PRINT 'Alter tblCallbackLogs Table:  Add [CallbackReasonTypeIdRef]';
		ALTER TABLE [streamassist_dbo].[tblCallbackLogs]
		ADD  [CallbackReasonTypeIdRef] Int NULL
	END;
END;
GO


IF EXISTS ( SELECT * FROM SYS.TABLES T WHERE (T.[name] = 'tblCallbackLogs') )
BEGIN
	IF NOT EXISTS 
	(
		SELECT TOP 1 * FROM 
					SYS.TABLES T 
		INNER JOIN SYS.COLUMNS C ON C.OBJECT_ID = T.OBJECT_ID 
		WHERE C.NAME = 'CustomerCallbackTimeStart' AND T.NAME = 'tblCallbackLogs'
	)
	BEGIN
		PRINT 'Alter tblCallbackLogs Table:  Add [CustomerCallbackTimeStart]';
		ALTER TABLE [streamassist_dbo].[tblCallbackLogs]
		ADD  [CustomerCallbackTimeStart]	DATETIMEOFFSET 	 NULL
	END;
END;
GO


IF EXISTS ( SELECT * FROM SYS.TABLES T WHERE (T.[name] = 'tblCallbackLogs') )
BEGIN
	IF NOT EXISTS 
	(
		SELECT TOP 1 * FROM 
					SYS.TABLES T 
		INNER JOIN SYS.COLUMNS C ON C.OBJECT_ID = T.OBJECT_ID 
		WHERE C.NAME = 'CustomerCallbackTimeEnd' AND T.NAME = 'tblCallbackLogs'
	)
	BEGIN
		PRINT 'Alter tblCallbackLogs Table:  Add [CustomerCallbackTimeEnd]';
		ALTER TABLE [streamassist_dbo].[tblCallbackLogs]
		ADD  [CustomerCallbackTimeEnd]	DATETIMEOFFSET 	 NULL
	END;
END;
GO



IF EXISTS ( SELECT * FROM SYS.TABLES T WHERE (T.[name] = 'tblCallbackLogs') )
BEGIN
	IF NOT EXISTS 
	(
		SELECT TOP 1 * FROM 
					SYS.TABLES T 
		INNER JOIN SYS.COLUMNS C ON C.OBJECT_ID = T.OBJECT_ID 
		WHERE C.NAME = 'AgentCallbackTimeStart' AND T.NAME = 'tblCallbackLogs'
	)
	BEGIN
		PRINT 'Alter tblCallbackLogs Table:  Add [AgentCallbackTimeStart]';
		ALTER TABLE [streamassist_dbo].[tblCallbackLogs]
		ADD  [AgentCallbackTimeStart]		DATETIMEOFFSET 	 NULL
	END;
END;
GO


IF EXISTS ( SELECT * FROM SYS.TABLES T WHERE (T.[name] = 'tblCallbackLogs') )
BEGIN
	IF NOT EXISTS 
	(
		SELECT TOP 1 * FROM 
					SYS.TABLES T 
		INNER JOIN SYS.COLUMNS C ON C.OBJECT_ID = T.OBJECT_ID 
		WHERE C.NAME = 'AgentCallbackTimeEnd' AND T.NAME = 'tblCallbackLogs'
	)
	BEGIN
		PRINT 'Alter tblCallbackLogs Table:  Add [AgentCallbackTimeEnd]';
		ALTER TABLE [streamassist_dbo].[tblCallbackLogs]
		ADD  [AgentCallbackTimeEnd]		DATETIMEOFFSET 	 NULL
	END
END;
GO


IF EXISTS ( SELECT * FROM SYS.TABLES T WHERE (T.[name] = 'tblCallbackLogs') )
BEGIN
	IF NOT EXISTS 
	(
		SELECT TOP 1 * FROM 
					SYS.TABLES T 
		INNER JOIN SYS.COLUMNS C ON C.OBJECT_ID = T.OBJECT_ID 
		WHERE C.NAME = 'Active' AND T.NAME = 'tblCallbackLogs'
	)
	BEGIN
		PRINT 'Alter tblCallbackLogs Table:  Add [Active]';
		ALTER TABLE [streamassist_dbo].[tblCallbackLogs]
		ADD  [Active] BIT NOT NULL DEFAULT (1)
	END;
END;
GO


IF EXISTS ( SELECT * FROM SYS.TABLES T WHERE (T.[name] = 'tblCallbackLogs') )
BEGIN
	IF NOT EXISTS 
	(
		SELECT TOP 1 * FROM 
					SYS.TABLES T 
		INNER JOIN SYS.COLUMNS C ON C.OBJECT_ID = T.OBJECT_ID 
		WHERE C.NAME = 'CallbackStatusId' AND T.NAME = 'tblCallbackLogs'
	)
	BEGIN
		PRINT 'Alter tblCallbackLogs Table:  Add [CallbackStatusId]';
		ALTER TABLE [streamassist_dbo].[tblCallbackLogs]
		ADD  CallbackStatusId INT NULL
	END;
END;
GO


IF EXISTS ( SELECT * FROM SYS.TABLES T WHERE (T.[name] = 'tblCallbackLogs') )
BEGIN
	IF NOT EXISTS 
	(
		SELECT TOP 1 * FROM 
					SYS.TABLES T 
		INNER JOIN SYS.COLUMNS C ON C.OBJECT_ID = T.OBJECT_ID 
		WHERE C.NAME = 'EmpName' AND T.NAME = 'tblCallbackLogs'
	)
	BEGIN
		PRINT 'Alter tblCallbackLogs Table:  Add [EmpName]';
		ALTER TABLE [streamassist_dbo].[tblCallbackLogs]
		ADD  EmpName NVARCHAR(200) NULL
	END;
END;
GO

IF EXISTS ( SELECT * FROM SYS.TABLES T WHERE (T.[name] = 'tblCallbackLogs') )
BEGIN
	IF NOT EXISTS 
	(
		SELECT TOP 1 * FROM 
					SYS.TABLES T 
		INNER JOIN SYS.COLUMNS C ON C.OBJECT_ID = T.OBJECT_ID 
		WHERE C.NAME = 'TeamName' AND T.NAME = 'tblCallbackLogs'
	)
	BEGIN
		PRINT 'Alter tblCallbackLogs Table:  Add [TeamName]';
		ALTER TABLE [streamassist_dbo].[tblCallbackLogs]
		ADD  TeamName NVARCHAR(50) NULL
	END;
END;
GO

IF EXISTS ( SELECT * FROM SYS.TABLES T WHERE (T.[name] = 'tblCallbackLogs') )
BEGIN
	IF NOT EXISTS 
	(
		SELECT TOP 1 * FROM 
					SYS.TABLES T 
		INNER JOIN SYS.COLUMNS C ON C.OBJECT_ID = T.OBJECT_ID 
		WHERE C.NAME = 'CreatedByName' AND T.NAME = 'tblCallbackLogs'
	)
	BEGIN
		PRINT 'Alter tblCallbackLogs Table:  Add [CreatedByName]';
		ALTER TABLE [streamassist_dbo].[tblCallbackLogs]
		ADD  CreatedByName NVARCHAR(200) NULL
	END;
END;
GO

IF EXISTS ( SELECT * FROM SYS.TABLES T WHERE (T.[name] = 'tblCallbackLogs') )
BEGIN
	IF NOT EXISTS 
	(
		SELECT TOP 1 * FROM 
					SYS.TABLES T 
		INNER JOIN SYS.COLUMNS C ON C.OBJECT_ID = T.OBJECT_ID 
		WHERE C.NAME = 'CallbackReasonTypeText' AND T.NAME = 'tblCallbackLogs'
	)
	BEGIN
		PRINT 'Alter tblCallbackLogs Table:  Add [CallbackReasonTypeText]';
		ALTER TABLE [streamassist_dbo].[tblCallbackLogs]
		ADD  CallbackReasonTypeText NVARCHAR(50) NULL
	END;
END;
GO


-- Create Sys_Logs
IF NOT EXISTS
(
    SELECT * FROM SYS.TABLES T
    WHERE T.NAME = 'tblCallbackLogs_Status'
)
BEGIN
PRINT 'Create Table: [tblCallbackLogs_Status]';
	CREATE TABLE [streamassist_dbo].[tblCallbackLogs_Status]
	(
		 [Id]			INT			  NOT NULL
		,[Key]			NVARCHAR(100) NOT NULL
		,[Description]	NVARCHAR(100) NOT NULL
		,[Active]		BIT			  NOT NULL DEFAULT (1)		
		,CONSTRAINT [PK_tblCallbackLogs_Status] PRIMARY KEY CLUSTERED([Id] ASC)
	)
END;
GO


IF NOT EXISTS ( SELECT Id FROM [streamassist_dbo].[tblCallbackLogs_Status] WHERE Id = 0 )
BEGIN
	PRINT 'INSERT 0 - CLOSED to tblCallbackLogs_Status';
	INSERT INTO [streamassist_dbo].[tblCallbackLogs_Status] 
	VALUES (0, 'CLOSED', 'CLOSED CALLBACKS', 1)
END;
GO

IF NOT EXISTS ( SELECT Id FROM [streamassist_dbo].[tblCallbackLogs_Status] WHERE Id = 1 )
BEGIN
	PRINT 'INSERT 1 - OPEN to tblCallbackLogs_Status';
	INSERT INTO [streamassist_dbo].[tblCallbackLogs_Status] 
	VALUES (1, 'OPEN', 'OPEN CALLBACKS', 1)
END;
GO


----------------------------------------------------------
--DATA UPGRADE: tblTimezone
--Update Records and Insert New Ones
----------------------------------------------------------

IF EXISTS (SELECT TOP 1 TimezoneId FROM [streamassist_dbo].tblTimezone WHERE [HrDifference] = '5.50')
	BEGIN
		PRINT 'Update Timezone: Kathmandu - UTC +5.45 Hrs';
		UPDATE [streamassist_dbo].[tblTimezone]
		SET Timezone = 'Kathmandu - UTC +5.45 Hrs', HrDifference = 5.45
		WHERE [HrDifference] = 5.50
	END
ELSE
BEGIN
	IF NOT EXISTS (SELECT TOP 1 TimezoneId FROM [streamassist_dbo].tblTimezone WHERE [HrDifference] = '5.45')
	BEGIN
		PRINT 'Insert Timezone: Kathmandu (UTC+05:45)';
		INSERT INTO [streamassist_dbo].[tblTimezone]
		VALUES ('Kathmandu - UTC +5.45 Hrs', 5.45)
	END
END;
GO

IF EXISTS (SELECT TOP 1 TimezoneId FROM [streamassist_dbo].tblTimezone WHERE [HrDifference] = 9.50)
	BEGIN
		PRINT 'Update Timezone: Austrailia Central - UTC +9.30 Hrs';
		UPDATE [streamassist_dbo].[tblTimezone]
		SET Timezone = 'Australia Central - UTC +9.30 Hrs', HrDifference = 9.30
		WHERE [HrDifference] = 9.50
	END
ELSE
BEGIN
	IF NOT EXISTS (SELECT TOP 1 TimezoneId FROM [streamassist_dbo].tblTimezone WHERE [HrDifference] = 9.30)
	BEGIN
		PRINT 'Insert Timezone: Australia Central - UTC +9.30 Hrs';
		INSERT INTO [streamassist_dbo].[tblTimezone]
		VALUES ('Australia Central - UTC +9.30 Hrs', 9.30)
	END
END;
GO

IF NOT EXISTS (SELECT TOP 1 TimezoneId FROM [streamassist_dbo].[tblTimezone] WHERE [HrDifference] = -12.00)
	BEGIN
		PRINT 'Insert Timezone: Intl Date Line West - UTC -12.00 Hrs';
		INSERT INTO [streamassist_dbo].[tblTimezone] VALUES ('Intl Date Line West - UTC -12.00 Hrs', -12.00)
	END;
GO
IF NOT EXISTS (SELECT TOP 1 TimezoneId FROM [streamassist_dbo].[tblTimezone] WHERE [HrDifference] = -11.00)
	BEGIN
		PRINT 'Insert Timezone: Coordinated Universal Time11 - UTC -11.00 Hrs';
		INSERT INTO [streamassist_dbo].[tblTimezone] VALUES ('Coordinated Universal Time11 - UTC -11.00 Hrs', -11.00)
	END;
GO
IF NOT EXISTS (SELECT TOP 1 TimezoneId FROM [streamassist_dbo].[tblTimezone] WHERE [HrDifference] = -10.00)
	BEGIN
		PRINT 'Insert Timezone: Hawaii - UTC -10.00 Hrs';
		INSERT INTO [streamassist_dbo].[tblTimezone] VALUES ('Hawaii - UTC -10.00 Hrs', -10.00)
	END;
GO
IF NOT EXISTS (SELECT TOP 1 TimezoneId FROM [streamassist_dbo].[tblTimezone] WHERE [HrDifference] = -9.00)
	BEGIN
		PRINT 'Insert Timezone: Alaska - UTC -9.00 Hrs';
		INSERT INTO [streamassist_dbo].[tblTimezone] VALUES ('Alaska - UTC -9.00 Hrs', -9.00)
	END;
GO
IF NOT EXISTS (SELECT TOP 1 TimezoneId FROM [streamassist_dbo].[tblTimezone] WHERE [HrDifference] = -4.30)
	BEGIN
		PRINT 'Insert Timezone: Caracas - UTC -4.30 Hrs';
		INSERT INTO [streamassist_dbo].[tblTimezone] VALUES ('Caracas - UTC -4.30 Hrs', -4.30)
	END;
GO
IF NOT EXISTS (SELECT TOP 1 TimezoneId FROM [streamassist_dbo].[tblTimezone] WHERE [HrDifference] = -3.30)
	BEGIN
		PRINT 'Insert Timezone: Newfoundland - UTC -3.30 Hrs';
		INSERT INTO [streamassist_dbo].[tblTimezone] VALUES ('Newfoundland - UTC -3.30 Hrs', -3.30)
	END;
GO
IF NOT EXISTS (SELECT TOP 1 TimezoneId FROM [streamassist_dbo].[tblTimezone] WHERE [HrDifference] = -3.00)
	BEGIN
		PRINT 'Insert Timezone: Brasilia - UTC -3.00 Hrs';
		INSERT INTO [streamassist_dbo].[tblTimezone] VALUES ('Brasilia - UTC -3.00 Hrs', -3.00)
	END;
GO
IF NOT EXISTS (SELECT TOP 1 TimezoneId FROM [streamassist_dbo].[tblTimezone] WHERE [HrDifference] = 3.30)
	BEGIN
		PRINT 'Insert Timezone: Tehran - UTC +3.30 Hrs';
		INSERT INTO [streamassist_dbo].[tblTimezone] VALUES ('Tehran - UTC +3.30 Hrs', 3.30)
	END;
GO
IF NOT EXISTS (SELECT TOP 1 TimezoneId FROM [streamassist_dbo].[tblTimezone] WHERE [HrDifference] = 4.00)
	BEGIN
		PRINT 'Insert Timezone: Abu Dhabi - UTC +4.00 Hrs';
		INSERT INTO [streamassist_dbo].[tblTimezone] VALUES ('Tehran - UTC +4.00 Hrs', 4.00)
	END;
GO
IF NOT EXISTS (SELECT TOP 1 TimezoneId FROM [streamassist_dbo].[tblTimezone] WHERE [HrDifference] = 4.30)
	BEGIN
		PRINT 'Insert Timezone: Kabul - UTC +4.30 Hrs';
		INSERT INTO [streamassist_dbo].[tblTimezone] VALUES ('Kabul - UTC +4.30 Hrs', 4.30)
	END;
GO
IF NOT EXISTS (SELECT TOP 1 TimezoneId FROM [streamassist_dbo].[tblTimezone] WHERE [HrDifference] = 5.00)
	BEGIN
		PRINT 'Insert Timezone: Islamabad - UTC +5.00 Hrs';
		INSERT INTO [streamassist_dbo].[tblTimezone] VALUES ('Islamabad - UTC +5.00 Hrs', 5.00)
	END;
GO
IF NOT EXISTS (SELECT TOP 1 TimezoneId FROM [streamassist_dbo].[tblTimezone] WHERE [HrDifference] = 5.30)
	BEGIN
		PRINT 'Insert Timezone: Mumbai - UTC +5.30 Hrs';
		INSERT INTO [streamassist_dbo].[tblTimezone] VALUES ('Mumbai - UTC +5.30 Hrs', 5.30)
	END;
GO
IF NOT EXISTS (SELECT TOP 1 TimezoneId FROM [streamassist_dbo].[tblTimezone] WHERE [HrDifference] = 6.00)
	BEGIN
		PRINT 'Insert Timezone: Dhaka - UTC +6.00 Hrs';
		INSERT INTO [streamassist_dbo].[tblTimezone] VALUES ('Dhaka - UTC +6.00 Hrs', 6.00)
	END;
GO
IF NOT EXISTS (SELECT TOP 1 TimezoneId FROM [streamassist_dbo].[tblTimezone] WHERE [HrDifference] = 6.30)
	BEGIN
		PRINT 'Insert Timezone: Yangon - UTC +6.30 Hrs';
		INSERT INTO [streamassist_dbo].[tblTimezone] VALUES ('Yangon - UTC +6.30 Hrs', 6.30)
	END;
GO
IF NOT EXISTS (SELECT TOP 1 TimezoneId FROM [streamassist_dbo].[tblTimezone] WHERE [HrDifference] = 7.00)
	BEGIN
		PRINT 'Insert Timezone: Bangkok - UTC +7.00 Hrs';
		INSERT INTO [streamassist_dbo].[tblTimezone] VALUES ('Bangkok - UTC +7.00 Hrs', 7.00)
	END;
GO
IF NOT EXISTS (SELECT TOP 1 TimezoneId FROM [streamassist_dbo].[tblTimezone] WHERE [HrDifference] = 11.00)
	BEGIN
		PRINT 'Insert Timezone: Vladivostok - UTC +11.00 Hrs';
		INSERT INTO [streamassist_dbo].[tblTimezone] VALUES ('Vladivostok - UTC +11.00 Hrs', 11.00)
	END;
GO
IF NOT EXISTS (SELECT TOP 1 TimezoneId FROM [streamassist_dbo].[tblTimezone] WHERE [HrDifference] = 13.00)
	BEGIN
		PRINT 'Insert Timezone: Samoa - UTC +13.00 Hrs';
		INSERT INTO [streamassist_dbo].[tblTimezone] VALUES ('Samoa - UTC +13.00 Hrs', 13.00)
	END;
GO
----------------------------------------------------------
--DATA UPGRADE: tblCallbackLogs
--Copy from the CallbackTimeEnd to [AgentCallbackTimeEnd]
----------------------------------------------------------
PRINT 'Data UPGRADE For AgentCallbackTimeEnd';
DECLARE @CurrentCallBackId Int
DECLARE CallbackLogs CURSOR FOR  
	SELECT CallbackLogId FROM streamassist_dbo.tblCallbackLogs
OPEN CallbackLogs   
FETCH NEXT FROM CallbackLogs INTO @CurrentCallBackId   
WHILE @@FETCH_STATUS = 0   
BEGIN   
	Declare @CallbackTimeEnd DATETIMEOFFSET
	SET @CallbackTimeEnd = (
								SELECT CallbackTimeEnd 
								FROM streamassist_dbo.tblCallbackLogs
								WHERE CallbackLogId = @CurrentCallBackId
							 )


	UPDATE streamassist_dbo.tblCallbackLogs
	SET [AgentCallbackTimeEnd] = @CallbackTimeEnd
	WHERE CallbackLogId = @CurrentCallBackId
    AND AgentCallbackTimeEnd IS NULL	
	
	  
FETCH NEXT FROM CallbackLogs INTO @CurrentCallBackId   
END
CLOSE CallbackLogs   
DEALLOCATE CallbackLogs
GO

----------------------------------------------------------
--DATA UPGRADE: tblCallbackLogs
--Copy from the CallbackTimeStart to [AgentCallbackTimeStart]
----------------------------------------------------------
PRINT 'Data UPGRADE For AgentCallbackTimeStart';
DECLARE @CurrentCallBackId Int
DECLARE CallbackLogs CURSOR FOR  
	SELECT CallbackLogId FROM streamassist_dbo.tblCallbackLogs
OPEN CallbackLogs   
FETCH NEXT FROM CallbackLogs INTO @CurrentCallBackId   
WHILE @@FETCH_STATUS = 0   
BEGIN   
	Declare @CallbackTimeStart DATETIMEOFFSET
	SET @CallbackTimeStart = (
								SELECT CallbackTimeStart 
								FROM streamassist_dbo.tblCallbackLogs
								WHERE CallbackLogId = @CurrentCallBackId
							 )


	UPDATE streamassist_dbo.tblCallbackLogs
	SET [AgentCallbackTimeStart] = @CallbackTimeStart
	WHERE CallbackLogId = @CurrentCallBackId   
	AND AgentCallbackTimeStart IS NULL
	  
FETCH NEXT FROM CallbackLogs INTO @CurrentCallBackId   
END
CLOSE CallbackLogs   
DEALLOCATE CallbackLogs

GO

----------------------------------------------------------
--DATA UPGRADE: tblCallbackLogs
--Copy from the numeric column to int column
----------------------------------------------------------
PRINT 'Data UPGRADE For CallbackReasonTypeIdRef';
DECLARE @CurrentCallBackId Int
DECLARE CallbackLogs CURSOR FOR  
	SELECT CallbackLogId FROM streamassist_dbo.tblCallbackLogs
OPEN CallbackLogs   
FETCH NEXT FROM CallbackLogs INTO @CurrentCallBackId   
WHILE @@FETCH_STATUS = 0   
BEGIN   
	Declare @CallbackReasonTypeIdNum Numeric(18,0)
	SET @CallbackReasonTypeIdNum = (
									SELECT CallbackReasonTypeId 
									FROM streamassist_dbo.tblCallbackLogs
									WHERE CallbackLogId = @CurrentCallBackId
									)


	UPDATE streamassist_dbo.tblCallbackLogs
	SET CallbackReasonTypeIdRef = @CallbackReasonTypeIdNum
	WHERE CallbackLogId = @CurrentCallBackId   
	AND CallbackReasonTypeIdRef IS NULL
	  
FETCH NEXT FROM CallbackLogs INTO @CurrentCallBackId   
END
CLOSE CallbackLogs   
DEALLOCATE CallbackLogs
GO


----------------------------------------------------------
--DATA UPGRADE: tblCallbackLogs
--Copy from the numeric column to int column
----------------------------------------------------------
PRINT 'Data UPGRADE For CallbackStatusId';
DECLARE @CurrentCallBackId Int
DECLARE CallbackLogs CURSOR FOR  
	SELECT CallbackLogId FROM streamassist_dbo.tblCallbackLogs
OPEN CallbackLogs   
FETCH NEXT FROM CallbackLogs INTO @CurrentCallBackId   
WHILE @@FETCH_STATUS = 0   
BEGIN   
	Declare @Status Numeric(18,0)
	SET @Status = (
					SELECT Status 
					FROM streamassist_dbo.tblCallbackLogs
					WHERE CallbackLogId = @CurrentCallBackId
				  )
	UPDATE streamassist_dbo.tblCallbackLogs
	SET CallbackStatusId = @Status
	WHERE CallbackLogId = @CurrentCallBackId   
	AND CallbackStatusId IS NULL
	  
FETCH NEXT FROM CallbackLogs INTO @CurrentCallBackId   
END
CLOSE CallbackLogs   
DEALLOCATE CallbackLogs
GO


----------------------------------------------------------
--DATA UPGRADE: tblCallbackLogs
--Copy from tblEmp the FirstName
----------------------------------------------------------
PRINT 'Data UPGRADE For EmpName';
DECLARE @CurrentCallBackEmpId Int
DECLARE CallbackLogs CURSOR FOR  
	SELECT DISTINCT(EmpId) FROM streamassist_dbo.tblCallbackLogs
OPEN CallbackLogs   
FETCH NEXT FROM CallbackLogs INTO @CurrentCallBackEmpId   
WHILE @@FETCH_STATUS = 0   
BEGIN   
	Declare @EmpName NVARCHAR(80)
	SET @EmpName = (
					SELECT FirstName 
					FROM streamassist_dbo.tblEmp
					WHERE EmpId = @CurrentCallBackEmpId
				  )
	UPDATE streamassist_dbo.tblCallbackLogs
	SET EmpName = @EmpName
	WHERE EmpId = @CurrentCallBackEmpId   
	  
FETCH NEXT FROM CallbackLogs INTO @CurrentCallBackEmpId   
END
CLOSE CallbackLogs   
DEALLOCATE CallbackLogs
GO


----------------------------------------------------------
--DATA UPGRADE: tblCallbackLogs
--Copy from tblFirstName the FirstName to CreatedByName
----------------------------------------------------------
PRINT 'Data UPGRADE For CreatedByName';
DECLARE @CurrentCallBackEmpId Int
DECLARE CallbackLogs CURSOR FOR  
	SELECT DISTINCT(CreatedBy) FROM streamassist_dbo.tblCallbackLogs
OPEN CallbackLogs   
FETCH NEXT FROM CallbackLogs INTO @CurrentCallBackEmpId   
WHILE @@FETCH_STATUS = 0   
BEGIN   
	Declare @EmpName NVARCHAR(80)
	SET @EmpName = (
					SELECT FirstName 
					FROM streamassist_dbo.tblEmp
					WHERE EmpId = @CurrentCallBackEmpId
				  )
	UPDATE streamassist_dbo.tblCallbackLogs
	SET CreatedByName = @EmpName
	WHERE CreatedBy = @CurrentCallBackEmpId   
	  
FETCH NEXT FROM CallbackLogs INTO @CurrentCallBackEmpId   
END
CLOSE CallbackLogs   
DEALLOCATE CallbackLogs
GO


----------------------------------------------------------
--DATA UPGRADE: tblCallbackLogs
--Copy from CallbackReasonType to CallbackReasonTypeText
----------------------------------------------------------
PRINT 'Data UPGRADE For CallbackReasonTypeText';
DECLARE @CallbackReasonTypeId Int
DECLARE CallbackLogs CURSOR FOR  
	SELECT DISTINCT(CallbackReasonTypeIdRef) FROM streamassist_dbo.tblCallbackLogs
OPEN CallbackLogs   
FETCH NEXT FROM CallbackLogs INTO @CallbackReasonTypeId   
WHILE @@FETCH_STATUS = 0   
BEGIN   
	Declare @Text NVARCHAR(50)
	SET @Text = (
					SELECT CallbackReasonType 
					FROM streamassist_dbo.tblCallbackReasonType
					WHERE CallbackReasonTypeId = @CallbackReasonTypeId
				  )
	UPDATE streamassist_dbo.tblCallbackLogs
	SET CallbackReasonTypeText = @Text
	WHERE CallbackReasonTypeIdRef = @CallbackReasonTypeId   
	  
FETCH NEXT FROM CallbackLogs INTO @CallbackReasonTypeId   
END
CLOSE CallbackLogs   
DEALLOCATE CallbackLogs
GO


----------------------------------------------------------
--DATA UPGRADE: tblCallbackLogs
--Copy from tblTeam the Team
----------------------------------------------------------
PRINT 'Data UPGRADE For TeamName';
DECLARE @CurrentCallBackTeamId Int
DECLARE CallbackLogs CURSOR FOR  
	SELECT DISTINCT(TeamId) FROM streamassist_dbo.tblCallbackLogs
OPEN CallbackLogs   
FETCH NEXT FROM CallbackLogs INTO @CurrentCallBackTeamId   
WHILE @@FETCH_STATUS = 0   
BEGIN   
	Declare @Team NVARCHAR(50)
	SET @Team = (
					SELECT Team 
					FROM streamassist_dbo.tblTeam
					WHERE TeamId = @CurrentCallBackTeamId
				  )
	UPDATE streamassist_dbo.tblCallbackLogs
	SET TeamName = @Team
	WHERE TeamId = @CurrentCallBackTeamId   
	  
FETCH NEXT FROM CallbackLogs INTO @CurrentCallBackTeamId   
END
CLOSE CallbackLogs   
DEALLOCATE CallbackLogs
GO

----------------------------------------------------------
--RECREATE VIEWS: vw_CallbackLogsSearchView
----------------------------------------------------------
IF EXISTS ( SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'VIEW' AND TABLE_NAME = 'vw_CallbackLogsSearchView')
BEGIN
	PRINT 'Drop View: vw_CallbackLogsSearchView';
	DROP VIEW [streamassist_dbo].[vw_CallbackLogsSearchView]
END;
GO

IF NOT EXISTS ( SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'VIEW' AND TABLE_NAME = 'vw_CallbackLogsSearchView')
BEGIN
PRINT 'Create View: vw_CallbackLogsSearchView';
EXECUTE(
'CREATE VIEW streamassist_dbo.vw_CallbackLogsSearchView
AS
SELECT 
	streamassist_dbo.tblCallbackLogs.CallbackLogId,
	streamassist_dbo.tblTeam.TeamId,
	streamassist_dbo.tblTeam.Team,
	streamassist_dbo.tblTeam.Active as TeamActive,
	Assigned.EmpId as AssignedToEmpId,
	Assigned.FirstName as AssignedTo,
	Assigned.Active as AssignedToActive,
	Created.EmpId as LogCreatorEmpId,
	Created.FirstName as LogCreator,
	Created.Active as LogCreatorActive, 
    streamassist_dbo.tblCallbackLogs.CreationDate, 
    streamassist_dbo.tblCallbackLogs.CustomerName, 
    streamassist_dbo.tblCallbackLogs.Contact1Phone,
    streamassist_dbo.tblCallbackLogs.CallReferenceNumber, 
    streamassist_dbo.tblCallbackLogs.CallbackStatusId, 
    streamassist_dbo.tblCallbackLogs.AgentCallbackTimeStart, 
    streamassist_dbo.tblCallbackLogs.AgentCallbackTimeEnd, 
	streamassist_dbo.tblCallbackLogs.CreatedBy,
	streamassist_dbo.tblCallbackReasonType.CallbackReasonType,
	streamassist_dbo.tblCallbackReasonType.Active as ReasonTypeActive
FROM         
	streamassist_dbo.tblCallbackLogs 
LEFT JOIN
	streamassist_dbo.tblEmp Assigned ON streamassist_dbo.tblCallbackLogs.EmpId = Assigned.EmpId 
LEFT JOIN
	streamassist_dbo.tblEmp Created ON streamassist_dbo.tblCallbackLogs.CreatedBy = Created.EmpId 
LEFT JOIN
	streamassist_dbo.tblTeam ON streamassist_dbo.tblCallbackLogs.TeamId = streamassist_dbo.tblTeam.TeamId 
LEFT JOIN
	streamassist_dbo.tblCallbackReasonType ON streamassist_dbo.tblCallbackLogs.CallbackReasonTypeIdRef = streamassist_dbo.tblCallbackReasonType.CallbackReasonTypeId
WHERE
	streamassist_dbo.tblCallbackLogs.Active = 1
')
END;
GO


----------------------------------------------------------
--RECREATE VIEWS: vw_ListEmpTeams
----------------------------------------------------------
IF EXISTS ( SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'VIEW' AND TABLE_NAME = 'vw_ListEmpTeams')
BEGIN
	PRINT 'Drop View: vw_ListEmpTeams';
	DROP VIEW [streamassist_dbo].[vw_ListEmpTeams]
END;
GO

IF NOT EXISTS ( SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'VIEW' AND TABLE_NAME = 'vw_ListEmpTeams')
BEGIN
PRINT 'Create View: vw_ListEmpTeams';
EXECUTE(
'CREATE VIEW [streamassist_dbo].[vw_ListEmpTeams]
AS
SELECT     streamassist_dbo.tblEmpTeams.EmpID, 
		   streamassist_dbo.tblEmp.EmpNumber, 
		   streamassist_dbo.tblEmpTeams.TeamID, 
           streamassist_dbo.tblTeam.Team, 
		   streamassist_dbo.tblEmp.ManagerId, 
		   streamassist_dbo.tblEmp.FirstName, 
		   streamassist_dbo.tblEmp.Active,
		   streamassist_dbo.tblPermission.RoleId
FROM       streamassist_dbo.tblEmpTeams 
INNER JOIN streamassist_dbo.tblTeam ON streamassist_dbo.tblEmpTeams.TeamID = streamassist_dbo.tblTeam.TeamId 
INNER JOIN streamassist_dbo.tblEmp ON streamassist_dbo.tblEmpTeams.EmpID = streamassist_dbo.tblEmp.EmpId
LEFT JOIN streamassist_dbo.tblPermission ON  streamassist_dbo.tblPermission.EmpID = streamassist_dbo.tblEmp.EmpId
')
END;
GO



-- Upgrade Sys_Version
DECLARE @MAJOR_VERSION INT = 2
DECLARE @MINOR_VERSION INT = 0

IF EXISTS(
    SELECT * FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_NAME = 'CA_Sys_Version' AND TABLE_TYPE = 'BASE TABLE'
      )
 BEGIN
  PRINT 'UPGRADE SYS_VERSION TO MAJOR:(' + 
     CAST(@MAJOR_VERSION AS NVARCHAR(3)) + 
     ') MINOR:(' + 
     CAST(@MINOR_VERSION AS NVARCHAR(3)) + ')'
  IF ( SELECT COUNT(*) FROM CA_Sys_Version ) < 1
   BEGIN
    INSERT INTO CA_Sys_Version 
    Values(@MAJOR_VERSION, @MINOR_VERSION)
   END
  ELSE
   BEGIN
    UPDATE CA_Sys_Version
    SET Major = @MAJOR_VERSION,
     Minor = @MINOR_VERSION
   END
 END;
GO



