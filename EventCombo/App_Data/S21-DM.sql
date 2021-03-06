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
