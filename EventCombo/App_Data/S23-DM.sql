IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_AspNetUserCode_TryCount]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AspNetUserCode] DROP CONSTRAINT [DF_AspNetUserCode_TryCount]
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserCode]') AND type in (N'U'))
DROP TABLE [dbo].[AspNetUserCode]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserCode]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserCode](
	[AspNetUserCodeId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[Code] [varchar](30) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[SendDate] [datetime] NULL,
	[UseDate] [datetime] NULL,
 CONSTRAINT [PK_AspNetUserCode] PRIMARY KEY CLUSTERED 
(
	[AspNetUserCodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

SET ANSI_PADDING OFF
GO

DELETE FROM Email_Tag
WHERE Tag_Name = 'ResetPwdCode'

SET IDENTITY_INSERT Email_Tag ON
INSERT INTO Email_Tag (Tag_Id, Tag_Name)
VALUES (43, 'ResetPwdCode')
SET IDENTITY_INSERT Email_Tag OFF

GO

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
IF NOT EXISTS(
  SELECT *
  FROM sys.columns 
  WHERE Name      = N'ECImageId'
    AND Object_ID = Object_ID(N'Event'))
BEGIN
  ALTER TABLE dbo.Event ADD
	  ECImageId bigint NULL
  CREATE NONCLUSTERED INDEX FK_ECImageId ON dbo.Event
	  (
	  ECImageId
	  ) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
  ALTER TABLE dbo.Event SET (LOCK_ESCALATION = TABLE)
END
GO
COMMIT

GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[EventECImage]') AND name = N'FK_EventId')
DROP INDEX [FK_EventId] ON [dbo].[EventECImage]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[EventECImage]') AND name = N'FK_ECImageId')
DROP INDEX [FK_ECImageId] ON [dbo].[EventECImage]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventECImage]') AND type in (N'U'))
DROP TABLE [dbo].[EventECImage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventECImage]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EventECImage](
	[EventECImageId] [bigint] IDENTITY(1,1) NOT NULL,
	[EventId] [bigint] NOT NULL,
	[ECImageId] [bigint] NOT NULL,
 CONSTRAINT [PK_EventECImage] PRIMARY KEY CLUSTERED 
(
	[EventECImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[EventECImage]') AND name = N'FK_ECImageId')
CREATE NONCLUSTERED INDEX [FK_ECImageId] ON [dbo].[EventECImage]
(
	[ECImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[EventECImage]') AND name = N'FK_EventId')
CREATE NONCLUSTERED INDEX [FK_EventId] ON [dbo].[EventECImage]
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
