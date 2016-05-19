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




SET ANSI_PADDING OFF
GO


