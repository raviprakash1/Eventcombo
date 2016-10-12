/*
   Wednesday, October 12, 20162:25:42 PM
   User: sa
   Server: KT-PC-195
   Database: EventCombo
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
ALTER TABLE dbo.Order_Detail_T ADD
	IsManualOrder bit NOT NULL CONSTRAINT DF_Order_Detail_T_IsManualOrder DEFAULT 0
GO
ALTER TABLE dbo.Order_Detail_T SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
