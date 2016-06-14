USE [FlowFBAccounting]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

----------------------------------------------------
-- Create Table FFBA_Projects
----------------------------------------------------
IF NOT EXISTS(
    SELECT * FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_NAME = 'FFBA_Projects' AND TABLE_TYPE = 'BASE TABLE'
    )
BEGIN
	PRINT 'Create Table FFBA_Projects'
	CREATE TABLE [dbo].[FFBA_Projects](
		[ProjectID] [int] NOT NULL,
		[ProjectName] [varchar](50) NULL,
		[PageNumbers] [bit] NOT NULL,
		[Boxes] [bit] NULL,
		[LabelManager] [int] NULL,
		[LabelCheckInterval] [int] NULL,
		[LabelAlertMin] [int] NULL,
		[FullText] [bit] NULL,
		[ShowChangeDate] [bit] NULL,
		[DividerSecurity] [int] NULL,
		[Imaging] [bit] NULL,
		[CAR] [bit] NULL,
		[ForceBox] [bit] NULL,
		[ImportNewToLabelManager] [bit] NULL,
		[SecurityField] [int] NULL,
		[MultiKey] [bit] NULL,
		[TaskManager] [bit] NULL,
		[LabelManagerFolder] [varchar](250) NULL,
		[MultiLevel] [bit] NULL,
		[PageSize] [int] NULL,
		[NumRevisions] [int] NULL,
		[Revisions] [int] NULL,
		[NewDocRoute] [int] NULL,
		[AllowFileCreateOnBatch] [int] NULL,
		[FileRoomEmails] [varchar](250) NULL,
		[Locking] [int] NULL,
		[KeepBox] [int] NULL,
		[NewDocRouteAction] [int] NULL,
		[IndexDividers] [int] NULL,
		[IndexSeparators] [int] NULL,
		[ScanEmails] [varchar](250) NULL,
		[Reminders] [int] NULL,
		[FieldSecurity] [int] NULL,
		[FileTracking] [bit] NULL,
		[ArchiveEmail] [bit] NULL,
		[NoPaper] [bit] NULL,
		[ServerID] [int] NULL CONSTRAINT [DF__Projects__Server__45F365D3]  DEFAULT ((0)),
		[ReportView] [int] NULL,
		[LockSep] [int] NULL,
		[LockDiv] [int] NULL,
		[SepLabel] [varchar](50) NULL,
		[DivLabel] [varchar](50) NULL,
		[SeparatorSecurity] [int] NULL,
		[SepSortDescending] [int] NULL,
		[DivSortDescending] [int] NULL,
		[QueueSelect] [int] NULL,
		[SaveStyle] [int] NULL,
		[Hidden] [int] NULL CONSTRAINT [DF__Projects__Hidden__799DF262]  DEFAULT ((0)),
		[AllowUnsign] [int] NOT NULL CONSTRAINT [DF__Projects__AllowU__6379A719]  DEFAULT ((1)),
		[RemoteID] [int] NULL,
		[InteractiveRevisions] [int] NOT NULL CONSTRAINT [DF__Projects__Intera__114071C9]  DEFAULT ((0)),
		[RenditionPerformed] [int] NOT NULL DEFAULT ((4)),
		[RenditionAction] [int] NOT NULL DEFAULT ((0)),
		[RenditionMaximumDimension] [int] NOT NULL DEFAULT ((800)),
		[ServerOCRType] [int] NOT NULL DEFAULT ((3)),
		[ServerOCRSkipExtensions] [varchar](2000) NULL,
		[FBDrive] [bit] NULL,
		[DocuSignEnabled] [bit] NOT NULL DEFAULT ((0)),
		[AllowManualDeclaration] [int] NOT NULL DEFAULT ((1)),
		[NativePDF] [bit] NULL,
		[SearchPortalSecurityMode] [int] NOT NULL DEFAULT ((0))
	) ON [PRIMARY]
	SET ANSI_PADDING OFF
	ALTER TABLE [dbo].[FFBA_Projects] ADD [ProjectType] [varchar](50) NULL DEFAULT ('')
	ALTER TABLE [dbo].[FFBA_Projects] ADD [Status] [int] NOT NULL DEFAULT ((1))
	ALTER TABLE [dbo].[FFBA_Projects] ADD [LastUpdated] [datetime] NOT NULL DEFAULT ('1/1/1990')
	ALTER TABLE [dbo].[FFBA_Projects] ADD [LastUpdatedBy] [varchar](40) NULL
	ALTER TABLE [dbo].[FFBA_Projects] ADD [SearchPortal] [bit] NOT NULL DEFAULT ((0))
	ALTER TABLE [dbo].[FFBA_Projects] ADD [IncludeLineItemSearch] [bit] NOT NULL DEFAULT ((0))
	 CONSTRAINT [PK_FFBA_Projects] PRIMARY KEY CLUSTERED 
	(
		[ProjectID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
END
GO

----------------------------------------------------
-- Create Table FFBA_Purchase
----------------------------------------------------
IF NOT EXISTS(
    SELECT * FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_NAME = 'FFBA_Purchase' AND TABLE_TYPE = 'BASE TABLE'
    )
BEGIN
PRINT 'Create FFBA_Purchase Table';
	CREATE TABLE [dbo].[FFBA_Purchase](
		[PurchaseID] [int] NOT NULL,
		[ProjectID] [int] NULL,
		[Status] [int] NULL,
		[Notes] [ntext] NULL,
		[DateChanged] [smalldatetime] NULL,
		[Destruction] [smalldatetime] NULL,
		[PurchaseInvoiceNumber] [varchar](255) NULL,
		[PurchaseInvoiceDescription] [varchar](255) NULL,
		[PurchaseInvoiceTotal] [money] NULL,
		[ContactName][varchar](255) NULL,
		[ContactAddress][varchar](255) NULL,
		[PurchaseStatus][varchar](255) NULL,
		[DateCreated] [smalldatetime] NULL,
		[DateStarted] [smalldatetime] NULL,
	 CONSTRAINT [PK_FFBA_Purchase] PRIMARY KEY CLUSTERED 
	(
		[PurchaseID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	SET ANSI_PADDING OFF
	ALTER TABLE [dbo].[FFBA_Purchase]  WITH CHECK ADD  CONSTRAINT [FK_FFBA_Purchase_FFBA_Projects] FOREIGN KEY([ProjectID])
	REFERENCES [dbo].[FFBA_Projects] ([ProjectID])
	ALTER TABLE [dbo].[FFBA_Purchase] CHECK CONSTRAINT [FK_FFBA_Purchase_FFBA_Projects]
END
GO

----------------------------------------------------
-- Create Table FFBA_Purchase_Proof
----------------------------------------------------
IF NOT EXISTS(
    SELECT * FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_NAME = 'FFBA_Purchase_Proof' AND TABLE_TYPE = 'BASE TABLE'
    )
BEGIN
PRINT 'Create FFBA_Purchase_Proof Table';
	CREATE TABLE [dbo].[FFBA_Purchase_Proof](
		[PurchaseProofID] [int] NOT NULL,
		[PurchaseID] [int] NULL,
		[DividerName] [varchar](200) NULL,
		[DateFiled] [smalldatetime] NULL,
		[UserFiled] [int] NULL,
		[Description] [varchar](50) NULL,
		[Extension] [varchar](50) NULL,
		[Status] [smallint] NOT NULL,
		[Pages] [int] NULL,
		[SortOrder] [int] NULL,
		[Contents] [text] NULL,
		[Archive] [varchar](50) NULL,
		[FileSize] [int] NULL,
		[LastView] [smalldatetime] NULL,
	 CONSTRAINT [PK_FFBA_Purchase_Proof] PRIMARY KEY CLUSTERED 
	(
		[PurchaseProofID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	ALTER TABLE [dbo].[FFBA_Purchase_Proof]  WITH CHECK ADD  CONSTRAINT [FK_FFBA_Purchase_Proof_FFBA_Purchase] FOREIGN KEY([PurchaseID])
	REFERENCES [dbo].[FFBA_Purchase] ([PurchaseID])
	ALTER TABLE [dbo].[FFBA_Purchase_Proof] CHECK CONSTRAINT [FK_FFBA_Purchase_Proof_FFBA_Purchase]
END
GO



----------------------------------------------------
-- Create Table FFBA_Purchase_History
----------------------------------------------------
IF NOT EXISTS(
    SELECT * FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_NAME = 'FFBA_Purchase_History' AND TABLE_TYPE = 'BASE TABLE'
    )
BEGIN
PRINT 'Create FFBA_Purchase_History Table';
	CREATE TABLE [dbo].[FFBA_Purchase_History](
		[PurchaseHistoryID] [int] NOT NULL,
		[PurchaseProofID] [int] NULL,
		[UserID] [int] NULL,
		[ByUserID] [int] NULL,
		[Comment] [text] NULL,
		[Status] [int] NULL,
		[Completed] [smalldatetime] NULL,
	 CONSTRAINT [PK_FFBA_Purchase_History] PRIMARY KEY CLUSTERED 
	(
		[PurchaseHistoryID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	ALTER TABLE [dbo].[FFBA_Purchase_History]  WITH CHECK ADD  CONSTRAINT [FK_FFBA_Purchase_History_FFBA_Purchase_Proof] FOREIGN KEY([PurchaseProofID])
	REFERENCES [dbo].[FFBA_Purchase_Proof] ([PurchaseProofID])
	ALTER TABLE [dbo].[FFBA_Purchase_History] CHECK CONSTRAINT [FK_FFBA_Purchase_History_FFBA_Purchase_Proof]
END
GO






----------------------------------------------------
-- Create Table FFBA_Logs
----------------------------------------------------
IF NOT EXISTS(
    SELECT * FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_NAME = 'FFBA_Logs' AND TABLE_TYPE = 'BASE TABLE'
    )
BEGIN
	PRINT 'Create Table: FFBA_Logs';
	CREATE TABLE [dbo].[FFBA_Logs] (
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



IF EXISTS ( SELECT * FROM SYS.TABLES T WHERE (T.[name] = 'FFBA_Purchase') )
BEGIN
	IF NOT EXISTS 
	(
		SELECT TOP 1 * FROM 
					SYS.TABLES T 
		INNER JOIN SYS.COLUMNS C ON C.OBJECT_ID = T.OBJECT_ID 
		WHERE C.NAME = 'Comment' AND T.NAME = 'FFBA_Purchase'
	)
	BEGIN
		PRINT 'Alter FFBA_Purchase Table:  Add [Comment]';
		ALTER TABLE [dbo].[FFBA_Purchase]
		ADD  [Comment] NVARCHAR(1000) NULL
	END;
END;
GO


SET ANSI_PADDING OFF
GO


