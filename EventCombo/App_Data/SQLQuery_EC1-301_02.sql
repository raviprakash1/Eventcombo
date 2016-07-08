/*
   Thursday, July 7, 201610:53:28 AM
   User: sa
   Server: KT-PC-164
   Database: EC2
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_EventType
	(
	EventTypeID bigint NOT NULL IDENTITY (1, 1),
	EventType varchar(200) NOT NULL,
	IsOnFooter bit NOT NULL,
	EventHide char(1) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_EventType SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_EventType ADD CONSTRAINT
	DF_EventType_IsOnFooter DEFAULT 0 FOR IsOnFooter
GO
SET IDENTITY_INSERT dbo.Tmp_EventType ON
GO
IF EXISTS(SELECT * FROM dbo.EventType)
	 EXEC('INSERT INTO dbo.Tmp_EventType (EventTypeID, EventType, EventHide)
		SELECT EventTypeID, EventType, EventHide FROM dbo.EventType WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_EventType OFF
GO
DROP TABLE dbo.EventType
GO
EXECUTE sp_rename N'dbo.Tmp_EventType', N'EventType', 'OBJECT' 
GO
ALTER TABLE dbo.EventType ADD CONSTRAINT
	PK__EventTyp__A9216B1F1D8AD487 PRIMARY KEY CLUSTERED 
	(
	EventTypeID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
select Has_Perms_By_Name(N'dbo.EventType', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.EventType', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.EventType', 'Object', 'CONTROL') as Contr_Per 