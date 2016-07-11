/****** Object:  Table [dbo].[City]    Script Date: 7/7/2016 11:03:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[City](
	[CityID] [bigint] IDENTITY(1,1) NOT NULL,
	[CityName] [nvarchar](150) NOT NULL,
	[Latitude] [varchar](50) NOT NULL CONSTRAINT [DF_City_Latitude]  DEFAULT (''),
	[Longitude] [varchar](50) NOT NULL CONSTRAINT [DF_City_Longitude]  DEFAULT (''),
	[IsOnFooter] [bit] NOT NULL CONSTRAINT [DF_City_IsOnFooter]  DEFAULT ((0)),
	[CreatedDate] [datetime] NOT NULL CONSTRAINT [DF_City_CreatedDate]  DEFAULT (getdate()),
	[UpdateDate] [datetime] NOT NULL CONSTRAINT [DF_City_UpdateDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[CityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[City] ON 

GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (1, N'Atlanta', N'1', N'1', 0, CAST(N'2016-07-07 10:58:05.253' AS DateTime), CAST(N'2016-07-07 10:58:05.253' AS DateTime))
GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (2, N'Dallas', N'1', N'1', 0, CAST(N'2016-07-07 10:58:22.907' AS DateTime), CAST(N'2016-07-07 10:58:22.907' AS DateTime))
GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (3, N'Boston', N'1', N'1', 0, CAST(N'2016-07-07 10:58:32.173' AS DateTime), CAST(N'2016-07-07 10:58:32.173' AS DateTime))
GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (4, N'Charlotte', N'1', N'1', 0, CAST(N'2016-07-07 10:58:44.960' AS DateTime), CAST(N'2016-07-07 10:58:44.960' AS DateTime))
GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (5, N'Chicago', N'1', N'1', 0, CAST(N'2016-07-07 10:58:51.220' AS DateTime), CAST(N'2016-07-07 10:58:51.220' AS DateTime))
GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (6, N'Houston', N'1', N'1', 0, CAST(N'2016-07-07 10:58:58.610' AS DateTime), CAST(N'2016-07-07 10:58:58.610' AS DateTime))
GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (7, N'Los Angeles', N'1', N'1', 0, CAST(N'2016-07-07 10:59:07.270' AS DateTime), CAST(N'2016-07-07 10:59:07.270' AS DateTime))
GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (8, N'Nashville', N'1', N'1.00', 0, CAST(N'2016-07-07 10:59:13.510' AS DateTime), CAST(N'2016-07-07 10:59:13.510' AS DateTime))
GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (9, N'New Orleans', N'1', N'1', 0, CAST(N'2016-07-07 10:59:20.180' AS DateTime), CAST(N'2016-07-07 10:59:20.180' AS DateTime))
GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (10, N'New York', N'1', N'1', 0, CAST(N'2016-07-07 10:59:26.997' AS DateTime), CAST(N'2016-07-07 10:59:26.997' AS DateTime))
GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (11, N'New Jersey', N'1', N'1', 0, CAST(N'2016-07-07 10:59:34.043' AS DateTime), CAST(N'2016-07-07 10:59:34.043' AS DateTime))
GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (12, N'Philadelphia', N'1', N'1', 0, CAST(N'2016-07-07 10:59:41.010' AS DateTime), CAST(N'2016-07-07 10:59:41.010' AS DateTime))
GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (13, N'San Francisco', N'1', N'1', 0, CAST(N'2016-07-07 10:59:46.717' AS DateTime), CAST(N'2016-07-07 10:59:46.717' AS DateTime))
GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (14, N'Seattle', N'1', N'1', 0, CAST(N'2016-07-07 10:59:53.290' AS DateTime), CAST(N'2016-07-07 10:59:53.290' AS DateTime))
GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (15, N'Washington DC', N'1', N'1', 0, CAST(N'2016-07-07 10:59:55.617' AS DateTime), CAST(N'2016-07-07 10:59:55.617' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[City] OFF
GO
