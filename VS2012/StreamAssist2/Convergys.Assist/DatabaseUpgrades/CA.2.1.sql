SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
----------------------------------------------------------------
-- Convergys ASSIST UPGRADE SCRIPT 2.1

----------------------------------------------------------------
----------------------------------------------------------------
--DROP SCRIPT
----------------------------------------------------------------
PRINT '---------------------------------------------------------';
PRINT 'DB 2.1 UPGRADE BEGINS';
PRINT '---------------------------------------------------------';


IF EXISTS ( SELECT * FROM SYS.TABLES T WHERE (T.[name] = 'tblOfflineLogs') )
BEGIN
	IF NOT EXISTS 
	(
		SELECT TOP 1 * FROM 
					SYS.TABLES T 
		INNER JOIN SYS.COLUMNS C ON C.OBJECT_ID = T.OBJECT_ID 
		WHERE C.NAME = 'Active' AND T.NAME = 'tblOfflineLogs'
	)
	BEGIN
		PRINT 'Alter tblOfflineLogs Table:  Add [Active]';
		ALTER TABLE [streamassist_dbo].[tblOfflineLogs]
		ADD  [Active] BIT NOT NULL DEFAULT(1)
	END;
END;
GO

IF EXISTS ( SELECT * FROM SYS.TABLES T WHERE (T.[name] = 'tblOfflineEvents') )
BEGIN
	IF NOT EXISTS 
	(
		SELECT TOP 1 * FROM 
					SYS.TABLES T 
		INNER JOIN SYS.COLUMNS C ON C.OBJECT_ID = T.OBJECT_ID 
		WHERE C.NAME = 'Active' AND T.NAME = 'tblOfflineEvents'
	)
	BEGIN
		PRINT 'Alter tblOfflineEvents Table:  Add [Active]';
		ALTER TABLE [streamassist_dbo].[tblOfflineEvents]
		ADD  [Active] BIT NOT NULL DEFAULT(1)
	END;
END;
GO


----------------------------------------------------------
--RECREATE VIEWS: vw_OfflineLogsLookup
----------------------------------------------------------
IF EXISTS ( SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'VIEW' AND TABLE_NAME = 'vw_OfflineLogsLookup')
BEGIN
	PRINT 'Drop View: vw_OfflineLogsLookup';
	DROP VIEW [streamassist_dbo].[vw_OfflineLogsLookup]
END;
GO

IF NOT EXISTS ( SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'VIEW' AND TABLE_NAME = 'vw_OfflineLogsLookup')
BEGIN
PRINT 'Create View: vw_OfflineLogsLookup';
EXECUTE(
'CREATE VIEW streamassist_dbo.vw_OfflineLogsLookup
AS
SELECT     streamassist_dbo.tblOfflineLogs.OfflineLogId, streamassist_dbo.tblOfflineLogs.CreationDate, streamassist_dbo.tblTeam.Team, 
                      streamassist_dbo.tblOfflineLogs.EmpId, streamassist_dbo.tblOfflineLogs.Status, streamassist_dbo.tblOfflineContactType.OfflineContactType, 
                      streamassist_dbo.tblOfflineActivityType.OfflineActivityType, streamassist_dbo.tblTeam.TeamId, streamassist_dbo.tblOfflineLogs.CaseIdentity, 
                      streamassist_dbo.tblOfflineLogs.Comments, streamassist_dbo.tblOfflineLogs.OfflineActivityTypeId, 
                      streamassist_dbo.tblOfflineLogs.OfflineContactTypeId, streamassist_dbo.tblEmp.FirstName, streamassist_dbo.tblOfflineLogs.TotalElapsedTime
FROM         streamassist_dbo.tblOfflineLogs INNER JOIN
                      streamassist_dbo.tblTeam ON streamassist_dbo.tblOfflineLogs.TeamId = streamassist_dbo.tblTeam.TeamId INNER JOIN
                      streamassist_dbo.tblEmp ON streamassist_dbo.tblOfflineLogs.EmpId = streamassist_dbo.tblEmp.EmpId LEFT OUTER JOIN
                      streamassist_dbo.tblOfflineActivityType ON 
                      streamassist_dbo.tblOfflineLogs.OfflineActivityTypeId = streamassist_dbo.tblOfflineActivityType.OfflineActivityTypeId LEFT OUTER JOIN
                      streamassist_dbo.tblOfflineContactType ON 
                      streamassist_dbo.tblOfflineLogs.OfflineContactTypeId = streamassist_dbo.tblOfflineContactType.OfflineContactTypeId
WHERE
	streamassist_dbo.tblOfflineLogs.Active = 1
')
END;
GO

IF NOT EXISTS ( 
SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
WHERE CONSTRAINT_NAME = 'PK_tblPreferences'
)
BEGIN
PRINT'Put a Primary Key on tblPreference';
ALTER TABLE streamassist_dbo.tblPreferences ADD CONSTRAINT
	PK_tblPreferences PRIMARY KEY CLUSTERED 
	(
	PreferenceId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
ALTER TABLE streamassist_dbo.tblPreferences SET (LOCK_ESCALATION = TABLE)
END;
GO

----------------------------------------------------------
--RECREATE VIEWS: vw_EmpLogonData
----------------------------------------------------------
IF EXISTS ( SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'VIEW' AND TABLE_NAME = 'vw_EmpLogonData')
BEGIN
	PRINT 'Drop View: vw_EmpLogonData';
	DROP VIEW [streamassist_dbo].[vw_EmpLogonData]
END;
GO

IF NOT EXISTS ( SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'VIEW' AND TABLE_NAME = 'vw_EmpLogonData')
BEGIN
PRINT 'Create View: vw_EmpLogonData';
EXECUTE(
'CREATE VIEW streamassist_dbo.vw_EmpLogonData
AS
SELECT 
streamassist_dbo.tblEmp.EmpId, 
streamassist_dbo.tblEmpEmail.EmailAddress, 
streamassist_dbo.tblPwd.NewPwd, 
streamassist_dbo.tblPermission.RoleId, 
streamassist_dbo.tblEmp.FirstName, 
streamassist_dbo.tblTeam.Team, 
streamassist_dbo.tblSiteInfo.SiteName, tblEmp_1.FirstName AS ManagerName, 
streamassist_dbo.tblEmp.Active, 
streamassist_dbo.tblPermission.AppEntitlement, 
streamassist_dbo.tblJobTitle.JobTitle, 
streamassist_dbo.tblEmp.TeamId, 
streamassist_dbo.tblTimezone.Timezone, 
streamassist_dbo.tblTimezone.HrDifference, 
streamassist_dbo.tblPreferences.TimeZoneDST, 
streamassist_dbo.tblPreferences.TimeZoneId, 
streamassist_dbo.tblEmp.ManagerId, 
streamassist_dbo.tblEmp.EmpNumber, 
streamassist_dbo.tblSiteInfo.SiteAbbreviation, 
streamassist_dbo.tblEmp.SiteId,
streamassist_dbo.tblPermission.PermissionId,
streamassist_dbo.tblPreferences.PreferenceId
FROM  streamassist_dbo.tblEmp 
INNER JOIN streamassist_dbo.tblEmpEmail ON streamassist_dbo.tblEmp.EmpId = streamassist_dbo.tblEmpEmail.EmpId 
LEFT JOIN streamassist_dbo.tblPwd ON streamassist_dbo.tblEmp.EmpId = streamassist_dbo.tblPwd.EmpId 
LEFT JOIN streamassist_dbo.tblPermission ON streamassist_dbo.tblEmp.EmpId = streamassist_dbo.tblPermission.EmpId 
INNER JOIN streamassist_dbo.tblTeam ON streamassist_dbo.tblEmp.TeamId = streamassist_dbo.tblTeam.TeamId 
INNER JOIN streamassist_dbo.tblSiteInfo ON streamassist_dbo.tblEmp.SiteId = streamassist_dbo.tblSiteInfo.SiteId 
INNER JOIN streamassist_dbo.tblEmp tblEmp_1 ON streamassist_dbo.tblEmp.ManagerId = tblEmp_1.EmpId 
INNER JOIN streamassist_dbo.tblEmpDetail ON streamassist_dbo.tblEmp.EmpId = streamassist_dbo.tblEmpDetail.EmpDetailId 
LEFT JOIN streamassist_dbo.tblJobTitle ON streamassist_dbo.tblEmpDetail.JobTitleId = streamassist_dbo.tblJobTitle.JobTitleId 
LEFT JOIN streamassist_dbo.tblPreferences ON streamassist_dbo.tblPreferences.EmpId = streamassist_dbo.tblEmp.EmpId
LEFT JOIN streamassist_dbo.tblTimezone ON streamassist_dbo.tblTimezone.TimezoneId = streamassist_dbo.tblPreferences.TimeZoneId
')
END;
GO





-- Upgrade Sys_Version
DECLARE @MAJOR_VERSION INT = 2
DECLARE @MINOR_VERSION INT = 1

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



