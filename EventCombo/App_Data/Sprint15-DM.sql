
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderState]') AND type in (N'U'))
DROP TABLE [dbo].[OrderState]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Order_Detail_T]') AND type in (N'U'))
DROP TABLE [dbo].[Order_Detail_T]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Order_Detail_T]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Order_Detail_T](
	[O_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[O_Order_Id] [varchar](100) NULL,
	[O_User_Id] [nvarchar](256) NULL,
	[O_TotalAmount] [numeric](18, 2) NULL,
	[O_OrderAmount] [numeric](18, 2) NULL,
	[O_VariableId] [varchar](500) NULL,
	[O_VariableAmount] [numeric](18, 2) NULL,
	[O_PromoCodeId] [bigint] NULL,
	[O_OrderDateTime] [datetime] NULL CONSTRAINT [DF_Order_Detail_T_O_OrderDateTime]  DEFAULT (getdate()),
	[O_PayPal_TokenId] [varchar](100) NULL,
	[O_PayPal_PayerId] [varchar](100) NULL,
	[O_PayPal_TrancId] [varchar](100) NULL,
	[OrderStateId] [tinyint] NULL,
 CONSTRAINT [PK_Order_Detail_T] PRIMARY KEY CLUSTERED 
(
	[O_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

SET ANSI_PADDING OFF
GO


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderState]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OrderState](
	[OrderStateId] [tinyint] IDENTITY(1,1) NOT NULL,
	[OrderStateName] [varchar](50) NULL,
 CONSTRAINT [PK_OrderState] PRIMARY KEY CLUSTERED 
(
	[OrderStateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

SET ANSI_PADDING OFF
GO

SET IDENTITY_INSERT [dbo].[Order_Detail_T] ON 
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (1, N'T000000001', N'0101b4eb-0ade-4142-9400-4cfeadf6f506', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'', CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-02-23 11:10:38.597' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (2, N'T000000002', N'0101b4eb-0ade-4142-9400-4cfeadf6f506', CAST(57.45 AS Numeric(18, 2)), CAST(57.45 AS Numeric(18, 2)), N'', CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-02-25 11:24:08.857' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (3, N'T000000003', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(22.98 AS Numeric(18, 2)), CAST(22.98 AS Numeric(18, 2)), NULL, CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-02-25 11:32:45.633' AS DateTime), N'EC-1SM849505J292692V', N'3V4P29FJC94V6', N'6RS2860738470303G')
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (4, N'T000000004', N'0101b4eb-0ade-4142-9400-4cfeadf6f506', CAST(57.45 AS Numeric(18, 2)), CAST(57.45 AS Numeric(18, 2)), N'', CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-02-25 11:46:34.433' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (5, N'T000000005', N'0101b4eb-0ade-4142-9400-4cfeadf6f506', CAST(57.45 AS Numeric(18, 2)), CAST(57.45 AS Numeric(18, 2)), NULL, CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-02-25 11:51:55.407' AS DateTime), N'EC-52W57918MC092421U', N'3V4P29FJC94V6', N'2J780572R5454482W')
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (6, N'T000000006', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(22.98 AS Numeric(18, 2)), CAST(22.98 AS Numeric(18, 2)), N'', CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-02-25 11:55:48.383' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (7, N'T000000007', N'0101b4eb-0ade-4142-9400-4cfeadf6f506', CAST(12.49 AS Numeric(18, 2)), CAST(12.49 AS Numeric(18, 2)), N'', CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-02-25 17:35:02.210' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (8, N'T000000008', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'', CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-02-26 09:11:13.197' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (9, N'T000000009', N'0101b4eb-0ade-4142-9400-4cfeadf6f506', CAST(11.49 AS Numeric(18, 2)), CAST(11.49 AS Numeric(18, 2)), N'', CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-02-28 18:48:27.417' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (10, N'T000000010', N'0101b4eb-0ade-4142-9400-4cfeadf6f506', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'', CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-02-28 18:51:40.397' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (11, N'T000000011', N'0101b4eb-0ade-4142-9400-4cfeadf6f506', CAST(12.49 AS Numeric(18, 2)), CAST(12.49 AS Numeric(18, 2)), NULL, CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-02-28 18:55:53.277' AS DateTime), N'EC-58C98607SF7411905', N'3V4P29FJC94V6', N'0LH570316N232033U')
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (12, N'T000000012', N'acc0dbf8-fe97-4b94-9723-7cbad3baa2ba', CAST(22.98 AS Numeric(18, 2)), CAST(22.98 AS Numeric(18, 2)), NULL, CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-02-29 04:47:36.967' AS DateTime), N'EC-31R40039D8968503W', N'3V4P29FJC94V6', N'9Y0606810H238504N')
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (13, N'T000000013', N'acc0dbf8-fe97-4b94-9723-7cbad3baa2ba', CAST(12.00 AS Numeric(18, 2)), CAST(12.00 AS Numeric(18, 2)), NULL, CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-02-29 04:59:00.583' AS DateTime), N'EC-67308264RP829723P', N'3V4P29FJC94V6', N'7LG14243Y42099444')
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (14, N'T000000014', N'11648b7f-d810-4d64-a887-d7cd6939c466', CAST(3.04 AS Numeric(18, 2)), CAST(3.04 AS Numeric(18, 2)), N'', CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-03-06 01:06:41.823' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (15, N'T000000015', N'761426e4-21e7-4e2e-8657-9aa31897e56f', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'', CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-03-07 15:12:09.237' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (16, N'T000000016', N'761426e4-21e7-4e2e-8657-9aa31897e56f', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'', CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-03-07 15:57:10.880' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (17, N'T000000017', N'761426e4-21e7-4e2e-8657-9aa31897e56f', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'', CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-03-07 16:08:37.250' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (18, N'T000000018', N'11648b7f-d810-4d64-a887-d7cd6939c466', CAST(11.49 AS Numeric(18, 2)), CAST(11.49 AS Numeric(18, 2)), N'', CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-03-13 22:41:11.353' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (19, N'T000000019', N'acc0dbf8-fe97-4b94-9723-7cbad3baa2ba', CAST(21.99 AS Numeric(18, 2)), CAST(21.99 AS Numeric(18, 2)), NULL, CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-03-14 16:27:35.107' AS DateTime), N'EC-2LN22042WM9151237', N'3V4P29FJC94V6', N'9HD94815A4451943L')
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (20, N'T000000020', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'', CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-03-15 06:22:17.567' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (21, N'T000000021', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(21.99 AS Numeric(18, 2)), CAST(21.99 AS Numeric(18, 2)), N'', CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-03-15 09:30:38.213' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (22, N'T000000022', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(21.99 AS Numeric(18, 2)), CAST(21.99 AS Numeric(18, 2)), NULL, CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-03-15 09:33:48.550' AS DateTime), N'EC-9C111920LF114294S', N'3V4P29FJC94V6', N'18698072908402932')
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (23, N'T000000023', N'acc0dbf8-fe97-4b94-9723-7cbad3baa2ba', CAST(14.48 AS Numeric(18, 2)), CAST(12.48 AS Numeric(18, 2)), N'3', CAST(2.00 AS Numeric(18, 2)), 0, CAST(N'2016-03-18 04:37:54.917' AS DateTime), N'EC-03K301138E336294N', N'3V4P29FJC94V6', N'86M66310J82980717')
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (24, N'', N'0101b4eb-0ade-4142-9400-4cfeadf6f506', CAST(1.00 AS Numeric(18, 2)), CAST(1.00 AS Numeric(18, 2)), N'', CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-03-23 10:30:38.120' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (25, N'', N'acc0dbf8-fe97-4b94-9723-7cbad3baa2ba', CAST(59.79 AS Numeric(18, 2)), CAST(59.79 AS Numeric(18, 2)), NULL, CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-03-30 09:31:52.207' AS DateTime), N'EC-1UX647575U1000535', N'3V4P29FJC94V6', N'7FA39661KB139784Y')
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (26, N'', N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', CAST(8.00 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), N'', CAST(3.00 AS Numeric(18, 2)), 0, CAST(N'2016-03-30 12:09:48.147' AS DateTime), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Order_Detail_T] OFF

UPDATE Order_Detail_T SET OrderStateId = 1

SET IDENTITY_INSERT [dbo].[OrderState] ON
INSERT INTO [dbo].[OrderState] (OrderStateId, OrderStateName)
VALUES (1, 'Active')
INSERT INTO [dbo].[OrderState] (OrderStateId, OrderStateName)
VALUES (2, 'Cancelled')
INSERT INTO [dbo].[OrderState] (OrderStateId, OrderStateName)
VALUES (3, 'Refunded')
SET IDENTITY_INSERT [dbo].[OrderState] OFF
