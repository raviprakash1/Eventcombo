
/****** Object:  Table [dbo].[ContactEventCombo]    Script Date: 7/28/2016 8:01:23 PM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactEventCombo]') AND type in (N'U'))
	DROP TABLE [dbo].[ContactEventCombo]
GO
/****** Object:  Table [dbo].[ContactEventCombo]    Script Date: 7/28/2016 8:01:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactEventCombo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ContactEventCombo](
	[ContactEventComboId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](256) NULL,
	[Email] [varchar](256) NULL,
	[PhoneNo] [varchar](20) NULL,
	[Category] [int] NULL,
	[SubCategory] [int] NULL,
	[Question] [nvarchar](max) NULL,
 CONSTRAINT [PK_ContactEventCombo] PRIMARY KEY CLUSTERED 
(
	[ContactEventComboId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
