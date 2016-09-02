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
ALTER TABLE dbo.Event ADD
	ECBackgroundId bigint NULL,
	BackgroundColor varchar(10) NULL
GO
CREATE NONCLUSTERED INDEX FK_ECBackgroundId ON dbo.Event
	(
	ECBackgroundId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.Event SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
GO

BEGIN TRANSACTION
GO
ALTER TABLE dbo.Organizer_Master ADD
	ECImageId bigint NULL
GO
CREATE NONCLUSTERED INDEX FK_ECImageId ON dbo.Organizer_Master
	(
	ECImageId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.Organizer_Master SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
GO

SET IDENTITY_INSERT TicketType ON
INSERT INTO TicketType (TicketTypeID, TicketType)
VALUES (1, 'Free')
INSERT INTO TicketType (TicketTypeID, TicketType)
VALUES (2, 'Paid')
INSERT INTO TicketType (TicketTypeID, TicketType)
VALUES (3, 'Donation')
SET IDENTITY_INSERT TicketType OFF

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BusinessPage]') AND type in (N'U'))
DROP TABLE [dbo].[BusinessPage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BusinessPage]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BusinessPage](
	[BusinessPageID] [bigint] IDENTITY(1,1) NOT NULL,
	[PageNameUrl] [nvarchar](200) NOT NULL,
	[PageName] [nvarchar](250) NOT NULL,
	[PageContent] [nvarchar](max) NOT NULL,
	[PageOrder] [int] NOT NULL CONSTRAINT [DF_BusinessPage_PageOrder]  DEFAULT ((0)),
	[IsOnFooter] [bit] NOT NULL CONSTRAINT [DF_BusinessPage_IsOnFooter]  DEFAULT ((0)),
	[CreatedDate] [datetime] NOT NULL CONSTRAINT [DF_BusinessPage_CreatedDate]  DEFAULT (getdate()),
	[UpdateDate] [datetime] NOT NULL CONSTRAINT [DF_BusinessPage_UpdateDate]  DEFAULT (getdate()),
	[ResponseCode] [int] NULL,
 CONSTRAINT [PK_BusinessPage] PRIMARY KEY CLUSTERED 
(
	[BusinessPageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_BusinessPage] UNIQUE NONCLUSTERED 
(
	[PageNameUrl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO


SET IDENTITY_INSERT [dbo].[BusinessPage] ON 

GO
INSERT [dbo].[BusinessPage] ([BusinessPageID], [PageNameUrl], [PageName], [PageContent]) VALUES (1, N'about-us', N'about us', N'<p>Vel augue laoreet rutrum faucibus dolor auctor. Nullam quis risus eget urna mollis ornare vel eu leo. Cras justo odio, dapibus ac f<s><i><b>acilisis in, egestas eget quam. Vivamus sagittis lacus vel augue laoreet rutrum faucibus dolor auctor. Donec sed odio dui. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi leo risus, porta ac consectetur ac, vestibulum at eros. Curabitur blandit tempus porttitor. Praesent commodo cursus magna, vel scelerisque nisl consectetur et. Donec sed odio dui. Praesent commodo cursus magna, vel scelerisque nisl consectetur et. A</b></i></s>enean lacinia bibendum nulla sed consectetur. Curabitur blandit tempus porttitor. Vestibulum id ligula porta felis euismod semper. Donec ullamcorper nulla non metus auctor fringilla. Integer posuere erat a ante venenatis dapibus posuere velit aliquet. Integer posuere erat a ante venenatis dapibus posuere velit aliquet. Duis mollis, est non commodo luctus, nisi erat porttitor ligula, eget lacinia odio sem nec elit. Vestibulum id ligula porta felis euismod semper.</p><p>Malesuada magna mollis euismod. Integer posuere erat a ante venenatis dapibus posuere velit aliquet. Morbi leo risus, porta ac consectetur ac, vestibulum at eros. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Duis mollis, est non commodo luctus, nisi erat porttitor ligula, eget lacinia odio sem nec elit. Morbi leo risus, porta ac consectetur ac, vestibulum at eros.</p><h3>Etiam porta sem malesuada magna</h3><p>Mollis euismod. Integer posuere erat a ante venenatis dapibus posuere velit aliquet. Morbi leo risus, porta ac consectetur ac, vestibulum at eros. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus.</p><p>Ticket Type</p><p>Whatever</p><p>Etiam porta sem malesuada magna</p><p>Duis mollis, est non commodo luctus, nisi erat porttitor ligula, eget lacinia odio sem nec elit. Morbi leo risus, porta ac consectetur ac, vestibulum at eros.</p><p><br></p>')
GO
INSERT [dbo].[BusinessPage] ([BusinessPageID], [PageNameUrl], [PageName], [PageContent]) VALUES (2, N'testimonials', N'Testimonials', N'<p>Testimonials</p>')
GO
INSERT [dbo].[BusinessPage] ([BusinessPageID], [PageNameUrl], [PageName], [PageContent]) VALUES (4, N'how-does-it-work', N'How does it work? ', N'<p>How does it work?</p>')
GO
INSERT [dbo].[BusinessPage] ([BusinessPageID], [PageNameUrl], [PageName], [PageContent]) VALUES (7, N'terms-and-conditions', N'Terms and Conditions', N'<ul><li>Terms and Conditions (terms-and-conditions)</li></ul>')
GO
INSERT [dbo].[BusinessPage] ([BusinessPageID], [PageNameUrl], [PageName], [PageContent]) VALUES (8, N'contact-us', N'Contact Us', N'<p>Contact Us<br></p><p> </p>')
GO
INSERT [dbo].[BusinessPage] ([BusinessPageID], [PageNameUrl], [PageName], [PageContent]) VALUES (9, N'ticket-purchase-faq', N'Ticket Purchase FAQ', N'<p>Ticket Purchase FAQ<br></p>')
GO
INSERT [dbo].[BusinessPage] ([BusinessPageID], [PageNameUrl], [PageName], [PageContent]) VALUES (10, N'ticket-fulfillment-policy', N'Ticket Fulfillment Policy', N'<p>Ticket Fulfillment Policy (ticket-fullfilment-policy)</p>')
GO
INSERT [dbo].[BusinessPage] ([BusinessPageID], [PageNameUrl], [PageName], [PageContent]) VALUES (11, N'privacy-policy', N'Privacy Policy', N'<p>Privacy Policy (privacy-policy)</p>')
GO
INSERT [dbo].[BusinessPage] ([BusinessPageID], [PageNameUrl], [PageName], [PageContent], [ResponseCode]) VALUES (12, N'server-error', N'500 Error code', N'<p>500 Error code</p>', 404)
GO
INSERT [dbo].[BusinessPage] ([BusinessPageID], [PageNameUrl], [PageName], [PageContent], [ResponseCode]) VALUES (13, N'page-not-found', N'404 Error code', N'<p>404 Error code</p>', 500)
GO
INSERT [dbo].[BusinessPage] ([BusinessPageID], [PageNameUrl], [PageName], [PageContent], [ResponseCode]) VALUES (22, N'unknown-error', N'Unknown Error', N'<p>Unknown Error<br></p>', 403)
GO
SET IDENTITY_INSERT [dbo].[BusinessPage] OFF
GO
SET ANSI_PADDING ON

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
CREATE TABLE dbo.Tmp_EventType
	(
	EventTypeID bigint NOT NULL IDENTITY (1, 1),
	EventType varchar(200) NOT NULL,
	IsOnFooter bit NOT NULL,
	EventHide char(1) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_EventType SET (LOCK_ESCALATION = TABLE)
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_EventType_IsOnFooter]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventType] DROP CONSTRAINT [DF_EventType_IsOnFooter]
END

ALTER TABLE dbo.Tmp_EventType ADD CONSTRAINT
	DF_EventType_IsOnFooter DEFAULT 0 FOR IsOnFooter
GO
SET IDENTITY_INSERT dbo.Tmp_EventType ON
GO
IF EXISTS(SELECT * FROM dbo.EventType)
	 EXEC('INSERT INTO dbo.Tmp_EventType (EventTypeID, EventType, EventHide)
		SELECT EventTypeID, EventType, EventHide FROM dbo.EventType WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_EventType OFF
GO
DROP TABLE dbo.EventType
GO
EXECUTE sp_rename N'dbo.Tmp_EventType', N'EventType', 'OBJECT' 
GO
ALTER TABLE dbo.EventType ADD CONSTRAINT
	PK__EventTyp__A9216B1F1D8AD487 PRIMARY KEY CLUSTERED 
	(
	EventTypeID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
select Has_Perms_By_Name(N'dbo.EventType', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.EventType', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.EventType', 'Object', 'CONTROL') as Contr_Per 

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[City]') AND type in (N'U'))
DROP TABLE [dbo].[City]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[City]') AND type in (N'U'))
BEGIN
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
END
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

UPDATE BusinessPage 
SET PageContent='<div class="servererrorcode">500</div>
  <div class="servererrormessage">Oops! Looks like we rocked a too hard and broke something...<br>
    <p>Please try the <a href="/">homepage</a></p>
  </div>
<style>
.servererrorcode {
    text-align: center;
    font-size: 50vw;
    color: #eeeeee;
    line-height: 70vw;
    font-weight: 700;
}
.servererrormessage {
    color: #FF0086;
    font-size: 5vw;
    text-align: center;
    position: absolute;
    margin-top: -50%;
    font-weight: 700;
}
</style>'
WHERE PageNameUrl='server-error'

UPDATE BusinessPage 
SET PageContent='<div class="servererrorcode">404</div>
  <div class="servererrormessage">Hmm, this combo is not on our servers...<br>
    Spring cleaning, amirite?! ;)
    <p>Please try the <a href="/">homepage</a> or <a href="/ec/about-us">let us know</a>.</p>
  </div>
<style>
.servererrorcode {
    text-align: center;
    font-size: 50vw;
    color: #eeeeee;
    line-height: 70vw;
    font-weight: 700;
}
.servererrormessage {
    color: #FF0086;
    font-size: 5vw;
    text-align: center;
    position: absolute;
    margin-top: -50%;
    font-weight: 700;
}
</style>'
WHERE PageNameUrl='page-not-found'

GO

alter table Event_OrganizerMessages  Add PhoneNo varchar(20)

GO


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
WHERE Tag_Name in ('ResetPwdCode', 'UserPhone', 'MessageBody', 'FriendsEmail')

SET IDENTITY_INSERT Email_Tag ON
INSERT INTO Email_Tag (Tag_Id, Tag_Name)
VALUES (43, 'ResetPwdCode')
INSERT INTO Email_Tag (Tag_Id, Tag_Name)
VALUES (44, 'UserPhone')
INSERT INTO Email_Tag (Tag_Id, Tag_Name)
VALUES (45, 'MessageBody')
INSERT INTO Email_Tag (Tag_Id, Tag_Name)
VALUES (46, 'FriendsEmail')
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

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[v_OrderList]'))
DROP VIEW [dbo].[v_OrderList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[v_OrderList]'))
EXEC dbo.sp_executesql @statement = N'
CREATE VIEW [dbo].[v_OrderList]
AS

SELECT ISNULL(od.O_Id, 0) OId, od.O_Order_Id OrderId, od.O_First_Name FirstName, od.O_Last_Name LastName, od.O_Email Email,
  pr.City, pr.State, od.O_OrderDateTime OrderDateTime, e.EventTitle Name, od.O_TotalAmount TotalPaid, tpdsum.Quantity, 
  CAST(ped.PE_Scheduled_Date AS DateTime) EventStartDate,
  CAST(CASE WHEN ISNULL(ped.PE_MultipleVenue_id, 0) > 0 THEN ped.PE_Scheduled_Date ELSE ev.EventEndDate END AS DateTime) EventEndDate,
  CAST(CASE WHEN UPPER(e.EventCancel) = ''Y'' THEN 1 ELSE 0 END as bit) EventCancelled,
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


' 
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[EventTicket_View]'))
DROP VIEW [dbo].[EventTicket_View]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[EventTicket_View]'))
EXEC dbo.sp_executesql @statement = N'

CREATE VIEW [dbo].[EventTicket_View]
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
	Ticket_Quantity_Detail.TQD_Quantity AS TotalQuantity,
	Ticket_Purchased_Detail.TPD_Purchased_Qty AS PurchasedQuantity, 
	Ticket_Purchased_Detail.TPD_Amount AS PaidAmount, 
	CASE WHEN Ticket.TicketTypeID = 2 THEN ISNULL(Ticket.EC_Fee, 0) - ISNULL(Ticket.T_EcAmount, 0) ELSE 0 END AS ECFeePerTicket,
	CASE WHEN Ticket.TicketTypeID = 2 OR Ticket.TicketTypeID = 3 THEN Ticket.T_EcAmount ELSE 0 END AS MerchantFeePerTicket,
	Ticket.Customer_Fee AS Customer_Fee, 
	TicketType.TicketTypeID, 
	TicketType.TicketType AS TicketTypeName, 
	Ticket_Purchased_Detail.TPD_PromoCodeID AS PromoCodeID, 
	Promo_Code.PC_Code AS PromoCode,
	Ticket_Purchased_Detail.TPD_PromoCodeAmount AS PromoCodeAmount,
	Order_Detail_T.O_OrderDateTime,
	Order_Detail_T.OrderStateId,
	OrderState.OrderStateName

FROM Ticket_Purchased_Detail
INNER JOIN Order_Detail_T ON Order_Detail_T.O_Order_Id=Ticket_Purchased_Detail.TPD_Order_Id
INNER JOIN Ticket_Quantity_Detail ON Ticket_Quantity_Detail.TQD_Id=Ticket_Purchased_Detail.TPD_TQD_Id
INNER JOIN Ticket ON Ticket.T_Id=Ticket_Quantity_Detail.TQD_Ticket_Id
INNER JOIN TicketType ON TicketType.TicketTypeID=Ticket.TicketTypeID
INNER JOIN Profile ON Profile.UserID = Ticket_Purchased_Detail.TPD_User_Id
INNER JOIN OrderState ON ISNULL(Order_Detail_T.OrderStateId, 1) = OrderState.OrderStateId
LEFT OUTER JOIN Promo_Code ON Promo_Code.PC_id=Ticket_Purchased_Detail.TPD_PromoCodeID

' 
GO


