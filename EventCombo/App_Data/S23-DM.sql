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

INSERT INTO Email_Tag (Tag_Name)
VALUES ('ResetPwdCode')

GO
