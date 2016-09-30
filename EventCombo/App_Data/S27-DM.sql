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

DELETE FROM [dbo].[Email_Tag]
WHERE Tag_Id in (46, 47)
GO
SET IDENTITY_INSERT [dbo].[Email_Tag] ON 
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (46, N'CategoryName')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (47, N'SubCategoryName')
GO
SET IDENTITY_INSERT [dbo].[Email_Tag] OFF
GO

/****** Object:  View [dbo].[EventTicket_View]    Script Date: 29-Sep-16 11:17:38 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[EventTicket_View]
AS
SELECT 
	Ticket_Purchased_Detail.TPD_Id,
	Ticket_Purchased_Detail.TPD_Event_Id AS EventID,
	Ticket.T_name AS TicketName,
	Order_Detail_T.O_Id OId, 
	Order_Detail_T.O_Order_Id OrderId, 
	Order_Detail_T.O_First_Name FirstName, 
	Order_Detail_T.O_Last_Name LastName, 
	Order_Detail_T.O_Email Email,     
	Order_Detail_T.O_OrderAmount AS OrderAmount,
	Order_Detail_T.O_VariableAmount AS VariableAmount,  
	Order_Detail_T.O_VariableId AS VariableIds,
	Ticket_Quantity_Detail.TQD_Quantity AS TotalQuantity,
	Ticket_Purchased_Detail.TPD_Purchased_Qty AS PurchasedQuantity, 
	Ticket_Purchased_Detail.TPD_Amount AS PaidAmount, 
	CASE WHEN Ticket.TicketTypeID = 2 THEN ISNULL(Ticket.EC_Fee,0)-ISNULL(Ticket.T_EcAmount,0) ELSE 0 END AS ECFeePerTicket,
	CASE WHEN Ticket.TicketTypeID = 2 OR Ticket.TicketTypeID = 3 THEN Ticket.T_EcAmount ELSE 0 END AS MerchantFeePerTicket,
	Ticket.Customer_Fee AS Customer_Fee, 
	TicketType.TicketTypeID, 
	TicketType.TicketType AS TicketTypeName, 
	Ticket_Purchased_Detail.TPD_PromoCodeID AS PromoCodeID, 
	Promo_Code.PC_Code AS PromoCode,
	Ticket_Purchased_Detail.TPD_PromoCodeAmount AS PromoCodeAmount,
	Order_Detail_T.O_OrderDateTime,
	Order_Detail_T.OrderStateId,
	OrderState.OrderStateName,
	Order_Detail_T.PaymentTypeId

FROM Ticket_Purchased_Detail
INNER JOIN Order_Detail_T ON Order_Detail_T.O_Order_Id=Ticket_Purchased_Detail.TPD_Order_Id
INNER JOIN Ticket_Quantity_Detail ON Ticket_Quantity_Detail.TQD_Id=Ticket_Purchased_Detail.TPD_TQD_Id
INNER JOIN Ticket ON Ticket.T_Id=Ticket_Quantity_Detail.TQD_Ticket_Id
INNER JOIN TicketType ON TicketType.TicketTypeID=Ticket.TicketTypeID
INNER JOIN Profile ON Profile.UserID = Ticket_Purchased_Detail.TPD_User_Id
INNER JOIN OrderState ON ISNULL(Order_Detail_T.OrderStateId, 1) = OrderState.OrderStateId
LEFT OUTER JOIN Promo_Code ON Promo_Code.PC_id=Ticket_Purchased_Detail.TPD_PromoCodeID

GO

/****** Object:  View [dbo].[TicketBearer_View]    Script Date: 06-Sep-16 10:52:12 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[TicketBearer_View]
AS
SELECT 0 TicketbearerId, t.OrderId, t.UserId, null as Guid, ISNULL(p.FirstName + ' ' + p.LastName, p.Email) Name, p.Email
FROM
(
  SELECT tpd.TPD_Order_Id OrderId, tpd.TPD_User_Id UserId, SUM(tpd.TPD_Purchased_Qty) qty
  FROM Ticket_Purchased_Detail tpd
  GROUP BY tpd.TPD_Order_Id, tpd.TPD_User_Id
) t
INNER JOIN Profile p ON p.UserID = t.UserId
LEFT JOIN
(
  SELECT tb.OrderId, tb.UserId, COUNT(*) qty
  FROM TicketBearer tb
  GROUP BY tb.OrderId, tb.UserId
) att ON t.OrderId = att.OrderId AND t.UserId = att.UserId AND t.qty > att.qty
 
UNION ALL
 
SELECT tb.TicketbearerId,  tb.OrderId, tb.UserId, tb.Guid, tb.Name, tb.Email
FROM TicketBearer tb


GO

/****** Object:  Table [dbo].[AttendeeEmail]    Script Date: 09-Sep-16 10:01:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttendeeEmail](
	[AttendeeEmailId] [bigint] IDENTITY(1,1) NOT NULL,
	[ScheduledEmailId] [bigint] NOT NULL,
	[TicketbearerId] [bigint] NULL,
	[EventID] [bigint] NOT NULL,
 CONSTRAINT [PK_AttendeeEmail] PRIMARY KEY CLUSTERED 
(
	[AttendeeEmailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmailType]    Script Date: 09-Sep-16 10:01:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EmailType](
	[EmailTypeId] [tinyint] IDENTITY(1,1) NOT NULL,
	[EmailType] [varchar](50) NOT NULL,
	[EmailTypeDesc] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_EmailType] PRIMARY KEY CLUSTERED 
(
	[EmailTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScheduledEmail]    Script Date: 09-Sep-16 10:01:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScheduledEmail](
	[ScheduledEmailId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[EmailTypeId] [tinyint] NOT NULL,
	[SendFrom] [nvarchar](300) NOT NULL,
	[SendTo] [nvarchar](max) NOT NULL,
	[ReplyTo] [nvarchar](300) NOT NULL,
	[CC] [nvarchar](300) NOT NULL,
	[BCC] [nvarchar](300) NOT NULL,
	[Subject] [nvarchar](500) NOT NULL,
	[Body] [nvarchar](max) NOT NULL,
	[ScheduledDate] [datetime] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[SendDate] [datetime] NULL,
	[IsEmailSend] [bit] NOT NULL,
 CONSTRAINT [PK_ScheduledEmail] PRIMARY KEY CLUSTERED 
(
	[ScheduledEmailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[EmailType] ON 

GO
INSERT [dbo].[EmailType] ([EmailTypeId], [EmailType], [EmailTypeDesc]) VALUES (1, N'Attendee', N'Attendee')
GO
SET IDENTITY_INSERT [dbo].[EmailType] OFF
GO
ALTER TABLE [dbo].[ScheduledEmail] ADD  CONSTRAINT [DF_ScheduledEmail_CreateDate]  DEFAULT (getutcdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[ScheduledEmail] ADD  CONSTRAINT [DF_ScheduledEmail_IsEmailSend]  DEFAULT ((0)) FOR [IsEmailSend]
GO
ALTER TABLE [dbo].[AttendeeEmail]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeEmail_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([EventID])
GO
ALTER TABLE [dbo].[AttendeeEmail] CHECK CONSTRAINT [FK_AttendeeEmail_Event]
GO
ALTER TABLE [dbo].[AttendeeEmail]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeEmail_ScheduledEmail] FOREIGN KEY([ScheduledEmailId])
REFERENCES [dbo].[ScheduledEmail] ([ScheduledEmailId])
GO
ALTER TABLE [dbo].[AttendeeEmail] CHECK CONSTRAINT [FK_AttendeeEmail_ScheduledEmail]
GO
ALTER TABLE [dbo].[ScheduledEmail]  WITH CHECK ADD  CONSTRAINT [FK_ScheduledEmail_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[ScheduledEmail] CHECK CONSTRAINT [FK_ScheduledEmail_AspNetUsers]
GO
ALTER TABLE [dbo].[ScheduledEmail]  WITH CHECK ADD  CONSTRAINT [FK_ScheduledEmail_EmailType] FOREIGN KEY([EmailTypeId])
REFERENCES [dbo].[EmailType] ([EmailTypeId])
GO
ALTER TABLE [dbo].[ScheduledEmail] CHECK CONSTRAINT [FK_ScheduledEmail_EmailType]
GO

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
ALTER TABLE dbo.Ticket_Purchased_Detail ADD
	Customer_Fee numeric(18, 2) NOT NULL CONSTRAINT DF_Ticket_Purchased_Detail_Customer_Fee DEFAULT 0
GO
ALTER TABLE dbo.Ticket_Purchased_Detail SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

GO

UPDATE Ticket
SET Customer_Fee = 0
WHERE EC_Fee = Customer_Fee