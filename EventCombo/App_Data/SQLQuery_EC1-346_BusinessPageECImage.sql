/****** Object:  Table [dbo].[BusinessPageECImage]    Script Date: 24-Aug-16 5:50:39 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BusinessPageECImage](
	[BusinessPageECImageId] [bigint] IDENTITY(1,1) NOT NULL,
	[BusinessPageId] [bigint] NOT NULL,
	[ECImageId] [bigint] NOT NULL,
 CONSTRAINT [PK_BusinessPageECImage] PRIMARY KEY CLUSTERED 
(
	[BusinessPageECImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[BusinessPageECImage]  WITH CHECK ADD  CONSTRAINT [FK_BusinessPageECImage_BusinessPage] FOREIGN KEY([BusinessPageId])
REFERENCES [dbo].[BusinessPage] ([BusinessPageID])
GO

ALTER TABLE [dbo].[BusinessPageECImage] CHECK CONSTRAINT [FK_BusinessPageECImage_BusinessPage]
GO

ALTER TABLE [dbo].[BusinessPageECImage]  WITH CHECK ADD  CONSTRAINT [FK_BusinessPageECImage_ECImage] FOREIGN KEY([ECImageId])
REFERENCES [dbo].[ECImage] ([ECImageId])
GO

ALTER TABLE [dbo].[BusinessPageECImage] CHECK CONSTRAINT [FK_BusinessPageECImage_ECImage]
GO


