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

SET ANSI_PADDING OFF
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[v_OrderList]
AS

SELECT ISNULL(od.O_Id, 0) OId, od.O_Order_Id OrderId, od.O_First_Name FirstName, od.O_Last_Name LastName, od.O_Email Email,
  pr.City, pr.State, od.O_OrderDateTime OrderDateTime, e.EventTitle Name, tpdsum.Amount TotalPaid, tpdsum.Quantity, 
  CAST(ped.PE_Scheduled_Date AS DateTime) EventStartDate,
  CAST(CASE WHEN ISNULL(ped.PE_MultipleVenue_id, 0) > 0 THEN ped.PE_Scheduled_Date ELSE ev.EventEndDate END AS DateTime) EventEndDate,
  CAST(CASE WHEN UPPER(e.EventCancel) = 'Y' THEN 1 ELSE 0 END as bit) EventCancelled,
  ISNULL(od.OrderStateId, 1) OrderStateId, os.OrderStateName, e.EventID, od.O_User_Id UserId,
  ISNULL(CAST(CASE WHEN ef.FavId is null THEN 0 ELSE 1 END as bit), 0) Favorite
FROM Order_Detail_T od
LEFT JOIN 
  (
    SELECT t.TPD_Order_Id, SUM(t.TPD_Amount) Amount, SUM(t.TPD_Purchased_Qty) Quantity, MIN(t.TPD_Id) TPD_Id
    FROM Ticket_Purchased_Detail t
    GROUP BY TPD_Order_Id) tpdsum ON tpdsum.TPD_Order_Id = od.O_Order_Id
LEFT JOIN  Ticket_Purchased_Detail tpd ON tpd.TPD_Id = tpdsum.TPD_Id
LEFT JOIN [Event] e ON e.EventID = tpd.TPD_Event_Id
LEFT JOIN Ticket_Quantity_Detail tqd ON tqd.TQD_Id = tpd.TPD_TQD_Id
LEFT JOIN Publish_Event_Detail ped ON ped.PE_Id = tqd.TQD_PE_Id
LEFT JOIN [Profile] pr ON pr.UserID = od.O_User_Id
LEFT JOIN EventVenue  ev ON ev.EventVenueID = ped.PE_SingleVenue_Id
LEFT JOIN OrderState os ON ISNULL(od.OrderStateId, 1) = os.OrderStateId
LEFT JOIN EventFavourite ef ON ef.eventId = e.EventID AND ef.UserID = od.O_User_Id

GO