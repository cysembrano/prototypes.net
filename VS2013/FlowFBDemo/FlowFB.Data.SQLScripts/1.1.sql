USE [FlowFBAccounting]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

----------------------------------------------------
-- Create Table FFBA_GLCodes
----------------------------------------------------
IF NOT EXISTS(
    SELECT * FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_NAME = 'FFBA_GLCodes' AND TABLE_TYPE = 'BASE TABLE'
    )
BEGIN
	PRINT 'Create Table FFBA_GLCodes'
	CREATE TABLE [dbo].FFBA_GLCodes(
		[GLCodeID] [int] NOT NULL,
		[GLCode] [int] NOT NULL,
		[Description] [nvarchar] (255) NULL,
		[Status] [int] NOT NULL DEFAULT(1),	 
		CONSTRAINT [PK_FFBA_GLCodes] PRIMARY KEY CLUSTERED ([GLCodeID] ASC ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
	) ON [PRIMARY]
END
GO

----------------------------------------------------
-- Create Table FFBA_CostCenter
----------------------------------------------------
IF NOT EXISTS(
    SELECT * FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_NAME = 'FFBA_CostCenter' AND TABLE_TYPE = 'BASE TABLE'
    )
BEGIN
	PRINT 'Create Table FFBA_CostCenter'
	CREATE TABLE [dbo].FFBA_CostCenter(
		[CostCenterID] [int] NOT NULL,
		[CostCenterCode] [int] NOT NULL,
		[Description] [nvarchar] (255) NULL,
		[Status] [int] NOT NULL DEFAULT(1),	 
		CONSTRAINT [PK_FFBA_CostCenter] PRIMARY KEY CLUSTERED ([CostCenterID] ASC ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
	) ON [PRIMARY]
END
GO

----------------------------------------------------
-- Create Table FFBA_TaxCodes
----------------------------------------------------
IF NOT EXISTS(
    SELECT * FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_NAME = 'FFBA_TaxCodes' AND TABLE_TYPE = 'BASE TABLE'
    )
BEGIN
	PRINT 'Create Table FFBA_TaxCodes'
	CREATE TABLE [dbo].FFBA_TaxCodes(
		[TaxCodeID] [int] NOT NULL,
		[TaxCode] [int] NOT NULL,
		[Description] [nvarchar] (255) NULL,
		[Status] [int] NOT NULL DEFAULT(1),	 
		CONSTRAINT [PK_FFBA_TaxCodes] PRIMARY KEY CLUSTERED ([TaxCodeID] ASC ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
	) ON [PRIMARY]
END
GO

SET ANSI_PADDING OFF
GO


