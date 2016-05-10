IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ECImageType]') AND type in (N'U'))
DROP TABLE [dbo].[ECImageType]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ECImage]') AND name = N'FK_ImageType')
DROP INDEX [FK_ImageType] ON [dbo].[ECImage]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ECImage]') AND type in (N'U'))
DROP TABLE [dbo].[ECImage]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ArticleAuthor]') AND name = N'FK_ImageId')
DROP INDEX [FK_ImageId] ON [dbo].[ArticleAuthor]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ArticleAuthor]') AND type in (N'U'))
DROP TABLE [dbo].[ArticleAuthor]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_ArticleImage_Sort]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ArticleImage] DROP CONSTRAINT [DF_ArticleImage_Sort]
END

GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ArticleImage]') AND name = N'FK_ImageId')
DROP INDEX [FK_ImageId] ON [dbo].[ArticleImage]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ArticleImage]') AND name = N'FK_ArticleId')
DROP INDEX [FK_ArticleId] ON [dbo].[ArticleImage]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ArticleImage]') AND type in (N'U'))
DROP TABLE [dbo].[ArticleImage]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Article]') AND name = N'FK_ImageId')
DROP INDEX [FK_ImageId] ON [dbo].[Article]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Article]') AND name = N'FK_ArticleAuthorId')
DROP INDEX [FK_ArticleAuthorId] ON [dbo].[Article]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Article]') AND type in (N'U'))
DROP TABLE [dbo].[Article]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ECImageType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ECImageType](
	[ECImageTypeId] [tinyint] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ImageType] PRIMARY KEY CLUSTERED 
(
	[ECImageTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ECImage]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ECImage](
	[ECImageId] [bigint] IDENTITY(1,1) NOT NULL,
	[ImagePath] [nvarchar](300) NOT NULL,
	[ECImageTypeId] [tinyint] NOT NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[ECImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ECImage]') AND name = N'FK_ImageType')
CREATE NONCLUSTERED INDEX [FK_ImageType] ON [dbo].[ECImage]
(
	[ECImageTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ArticleAuthor]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ArticleAuthor](
	[ArticleAuthorId] [bigint] IDENTITY(1,1) NOT NULL,
	[ECImageId] [bigint] NULL,
	[Name] [nvarchar](500) NULL,
	[TwitterLink] [nvarchar](200) NULL,
 CONSTRAINT [PK_ArticleAuthor] PRIMARY KEY CLUSTERED 
(
	[ArticleAuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ArticleAuthor]') AND name = N'FK_ImageId')
CREATE NONCLUSTERED INDEX [FK_ImageId] ON [dbo].[ArticleAuthor]
(
	[ECImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ArticleImage]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ArticleImage](
	[ArticleImageId] [bigint] IDENTITY(1,1) NOT NULL,
	[ArticleId] [bigint] NOT NULL,
	[ECImageId] [bigint] NOT NULL,
	[Sort] [bigint] NOT NULL,
 CONSTRAINT [PK_ArticleImage] PRIMARY KEY CLUSTERED 
(
	[ArticleImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ArticleImage]') AND name = N'FK_ArticleId')
CREATE NONCLUSTERED INDEX [FK_ArticleId] ON [dbo].[ArticleImage]
(
	[ArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ArticleImage]') AND name = N'FK_ImageId')
CREATE NONCLUSTERED INDEX [FK_ImageId] ON [dbo].[ArticleImage]
(
	[ECImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_ArticleImage_Sort]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ArticleImage] ADD  CONSTRAINT [DF_ArticleImage_Sort]  DEFAULT ((0)) FOR [Sort]
END

GO


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Article]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Article](
	[ArticleId] [bigint] IDENTITY(1,1) NOT NULL,
	[ArticleAuthorId] [bigint] NOT NULL,
	[Title] [nvarchar](500) NOT NULL,
	[Body] [nvarchar](max) NOT NULL,
	[ECImageId] [bigint] NULL,
	[SubHeading] [nvarchar](500) NULL,
	[EnableFBComments] [bit] NOT NULL CONSTRAINT [DF_Article_EnableFBComments]  DEFAULT ((1)),
	[HomepageFlag] [bit] NOT NULL CONSTRAINT [DF_Article_HomepageFlag]  DEFAULT ((1)),
	[PremiumFlag] [bit] NOT NULL CONSTRAINT [DF_Article_PremiumFlag]  DEFAULT ((0)),
	[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_Article_CreateDate]  DEFAULT (getdate()),
	[EditDate] [datetime] NOT NULL CONSTRAINT [DF_Article_EditDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED 
(
	[ArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Article]') AND name = N'FK_ArticleAuthorId')
CREATE NONCLUSTERED INDEX [FK_ArticleAuthorId] ON [dbo].[Article]
(
	[ArticleAuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Article]') AND name = N'FK_ImageId')
CREATE NONCLUSTERED INDEX [FK_ImageId] ON [dbo].[Article]
(
	[ECImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO








SET IDENTITY_INSERT ECImageType ON

INSERT INTO ECImageType (ECImageTypeId, TypeName)
VALUES (1, 'image/jpeg')

INSERT INTO ECImageType (ECImageTypeId, TypeName)
VALUES (2, 'image/png')

SET IDENTITY_INSERT ECImageType OFF
