/*
   Thursday, July 7, 201610:53:57 AM
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
ALTER TABLE dbo.BusinessPage
	DROP CONSTRAINT DF_BusinessPage_CreatedDate
GO
ALTER TABLE dbo.BusinessPage
	DROP CONSTRAINT DF_BusinessPage_UpdateDate
GO
CREATE TABLE dbo.Tmp_BusinessPage
	(
	BusinessPageID bigint NOT NULL IDENTITY (1, 1),
	PageNameUrl nvarchar(200) NOT NULL,
	PageName nvarchar(250) NOT NULL,
	PageContent nvarchar(MAX) NOT NULL,
	PageOrder int NOT NULL,
	IsOnFooter bit NOT NULL,
	CreatedDate datetime NOT NULL,
	UpdateDate datetime NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_BusinessPage SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_BusinessPage ADD CONSTRAINT
	DF_BusinessPage_PageOrder DEFAULT 0 FOR PageOrder
GO
ALTER TABLE dbo.Tmp_BusinessPage ADD CONSTRAINT
	DF_BusinessPage_IsOnFooter DEFAULT 0 FOR IsOnFooter
GO
ALTER TABLE dbo.Tmp_BusinessPage ADD CONSTRAINT
	DF_BusinessPage_CreatedDate DEFAULT (getdate()) FOR CreatedDate
GO
ALTER TABLE dbo.Tmp_BusinessPage ADD CONSTRAINT
	DF_BusinessPage_UpdateDate DEFAULT (getdate()) FOR UpdateDate
GO
SET IDENTITY_INSERT dbo.Tmp_BusinessPage ON
GO
IF EXISTS(SELECT * FROM dbo.BusinessPage)
	 EXEC('INSERT INTO dbo.Tmp_BusinessPage (BusinessPageID, PageNameUrl, PageName, PageContent, CreatedDate, UpdateDate)
		SELECT BusinessPageID, PageNameUrl, PageName, PageContent, CreatedDate, UpdateDate FROM dbo.BusinessPage WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_BusinessPage OFF
GO
DROP TABLE dbo.BusinessPage
GO
EXECUTE sp_rename N'dbo.Tmp_BusinessPage', N'BusinessPage', 'OBJECT' 
GO
ALTER TABLE dbo.BusinessPage ADD CONSTRAINT
	PK_BusinessPage PRIMARY KEY CLUSTERED 
	(
	BusinessPageID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.BusinessPage ADD CONSTRAINT
	IX_BusinessPage UNIQUE NONCLUSTERED 
	(
	PageNameUrl
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
select Has_Perms_By_Name(N'dbo.BusinessPage', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.BusinessPage', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.BusinessPage', 'Object', 'CONTROL') as Contr_Per 