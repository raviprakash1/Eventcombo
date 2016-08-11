TRUNCATE TABLE City
SET IDENTITY_INSERT [dbo].[City] ON 
GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (1, N'Atlanta', N'33.748995', N'-84.387982', 1, CAST(N'2016-07-07 10:58:05.253' AS DateTime), CAST(N'2016-07-07 10:58:05.253' AS DateTime))
GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (2, N'Dallas', N'32.776664', N'-96.796988', 1, CAST(N'2016-07-07 10:58:22.907' AS DateTime), CAST(N'2016-07-07 10:58:22.907' AS DateTime))
GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (3, N'Boston', N'42.360083', N'-71.058880', 1, CAST(N'2016-07-07 10:58:32.173' AS DateTime), CAST(N'2016-07-07 10:58:32.173' AS DateTime))
GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (4, N'Charlotte', N'35.227087', N'-80.843127', 1, CAST(N'2016-07-07 10:58:44.960' AS DateTime), CAST(N'2016-07-07 10:58:44.960' AS DateTime))
GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (5, N'Chicago', N'41.878114', N'-87.629798', 1, CAST(N'2016-07-07 10:58:51.220' AS DateTime), CAST(N'2016-07-07 10:58:51.220' AS DateTime))
GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (6, N'Houston', N'29.760427', N'-95.369803', 1, CAST(N'2016-07-07 10:58:58.610' AS DateTime), CAST(N'2016-07-07 10:58:58.610' AS DateTime))
GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (7, N'Los Angeles', N'34.052234', N'-118.243685', 1, CAST(N'2016-07-07 10:59:07.270' AS DateTime), CAST(N'2016-07-07 10:59:07.270' AS DateTime))
GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (8, N'Nashville', N'36.162664', N'-86.781602', 1, CAST(N'2016-07-07 10:59:13.510' AS DateTime), CAST(N'2016-07-07 10:59:13.510' AS DateTime))
GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (9, N'New Orleans', N'29.951066', N'-90.071532', 1, CAST(N'2016-07-07 10:59:20.180' AS DateTime), CAST(N'2016-07-07 10:59:20.180' AS DateTime))
GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (10, N'New York', N'40.712784', N'-74.0059413', 1, CAST(N'2016-07-07 10:59:26.997' AS DateTime), CAST(N'2016-07-07 10:59:26.997' AS DateTime))
GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (11, N'New Jersey', N'40.0583', N'-74.077642', 1, CAST(N'2016-07-07 10:59:34.043' AS DateTime), CAST(N'2016-07-07 10:59:34.043' AS DateTime))
GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (12, N'Philadelphia', N'39.952584', N'-75.165222', 1, CAST(N'2016-07-07 10:59:41.010' AS DateTime), CAST(N'2016-07-07 10:59:41.010' AS DateTime))
GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (13, N'San Francisco', N'37.774930', N'-122.419416', 1, CAST(N'2016-07-07 10:59:46.717' AS DateTime), CAST(N'2016-07-07 10:59:46.717' AS DateTime))
GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (14, N'Seattle', N'47.606210', N'-122.332071', 1, CAST(N'2016-07-07 10:59:53.290' AS DateTime), CAST(N'2016-07-07 10:59:53.290' AS DateTime))
GO
INSERT [dbo].[City] ([CityID], [CityName], [Latitude], [Longitude], [IsOnFooter], [CreatedDate], [UpdateDate]) VALUES (15, N'Washington DC', N'38.907192', N'-77.036871', 1, CAST(N'2016-07-07 10:59:55.617' AS DateTime), CAST(N'2016-07-07 10:59:55.617' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[City] OFF
GO

TRUNCATE TABLE Permission_Detail
GO
SET IDENTITY_INSERT [dbo].[Permission_Detail] ON 
GO
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (1, N'Event', N'APP')
GO
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (2, N'Deal', N'APP')
GO
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (3, N'Users', N'CMS')
GO
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (4, N'Events', N'CMS')
GO
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (5, N'Tickets', N'CMS')
GO
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (6, N'EMails', N'CMS')
GO
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (7, N'Messages', N'CMS')
GO
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (8, N'EventType', N'CMS')
GO
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (9, N'EventCategory', N'CMS')
GO
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (10, N'Users', N'CMS')
GO
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (11, N'Fee Settings', N'CMS')
GO
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (12, N'Article Management', N'CMS')
GO
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (13, N'Ticket Orders', N'CMS')
GO
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (14, N'Business Pages', N'CMS')
GO
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (15, N'Footer', N'CMS')
GO
SET IDENTITY_INSERT [dbo].[Permission_Detail] OFF
GO
