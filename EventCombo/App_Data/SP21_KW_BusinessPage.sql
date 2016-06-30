/****** Object:  Table [dbo].[BusinessPage]    Script Date: 6/30/2016 4:53:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessPage](
	[BusinessPageID] [bigint] IDENTITY(1,1) NOT NULL,
	[PageNameUrl] [nvarchar](200) NOT NULL,
	[PageName] [nvarchar](250) NOT NULL,
	[PageContent] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL CONSTRAINT [DF_BusinessPage_CreatedDate]  DEFAULT (getdate()),
	[UpdateDate] [datetime] NOT NULL CONSTRAINT [DF_BusinessPage_UpdateDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_BusinessPage] PRIMARY KEY CLUSTERED 
(
	[BusinessPageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[BusinessPage] ON 

GO
INSERT [dbo].[BusinessPage] ([BusinessPageID], [PageNameUrl], [PageName], [PageContent], [CreatedDate], [UpdateDate]) VALUES (1, N'about-us', N'about us', N'<p>Vel augue laoreet rutrum faucibus dolor auctor. Nullam quis risus eget urna mollis ornare vel eu leo. Cras justo odio, dapibus ac f<s><i><b>acilisis in, egestas eget quam. Vivamus sagittis lacus vel augue laoreet rutrum faucibus dolor auctor. Donec sed odio dui. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi leo risus, porta ac consectetur ac, vestibulum at eros. Curabitur blandit tempus porttitor. Praesent commodo cursus magna, vel scelerisque nisl consectetur et. Donec sed odio dui. Praesent commodo cursus magna, vel scelerisque nisl consectetur et. A</b></i></s>enean lacinia bibendum nulla sed consectetur. Curabitur blandit tempus porttitor. Vestibulum id ligula porta felis euismod semper. Donec ullamcorper nulla non metus auctor fringilla. Integer posuere erat a ante venenatis dapibus posuere velit aliquet. Integer posuere erat a ante venenatis dapibus posuere velit aliquet. Duis mollis, est non commodo luctus, nisi erat porttitor ligula, eget lacinia odio sem nec elit. Vestibulum id ligula porta felis euismod semper.</p><p>Malesuada magna mollis euismod. Integer posuere erat a ante venenatis dapibus posuere velit aliquet. Morbi leo risus, porta ac consectetur ac, vestibulum at eros. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Duis mollis, est non commodo luctus, nisi erat porttitor ligula, eget lacinia odio sem nec elit. Morbi leo risus, porta ac consectetur ac, vestibulum at eros.</p><h3>Etiam porta sem malesuada magna</h3><p>Mollis euismod. Integer posuere erat a ante venenatis dapibus posuere velit aliquet. Morbi leo risus, porta ac consectetur ac, vestibulum at eros. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus.</p><p>Ticket Type</p><p>Whatever</p><p>Etiam porta sem malesuada magna</p><p>Duis mollis, est non commodo luctus, nisi erat porttitor ligula, eget lacinia odio sem nec elit. Morbi leo risus, porta ac consectetur ac, vestibulum at eros.</p><p><br></p>', CAST(N'2016-06-29 12:55:42.000' AS DateTime), CAST(N'2016-06-30 12:01:11.490' AS DateTime))
GO
INSERT [dbo].[BusinessPage] ([BusinessPageID], [PageNameUrl], [PageName], [PageContent], [CreatedDate], [UpdateDate]) VALUES (2, N'testimonials', N'Testimonials', N'<p>Testimonials</p>', CAST(N'2016-06-27 17:37:28.627' AS DateTime), CAST(N'2016-06-27 17:37:28.627' AS DateTime))
GO
INSERT [dbo].[BusinessPage] ([BusinessPageID], [PageNameUrl], [PageName], [PageContent], [CreatedDate], [UpdateDate]) VALUES (4, N'how-does-it-work', N'How does it work? ', N'<p>How does it work?</p>', CAST(N'2016-06-29 11:21:56.000' AS DateTime), CAST(N'2016-06-29 13:07:48.217' AS DateTime))
GO
INSERT [dbo].[BusinessPage] ([BusinessPageID], [PageNameUrl], [PageName], [PageContent], [CreatedDate], [UpdateDate]) VALUES (7, N'terms-and-conditions', N'Terms and Conditions', N'<ul><li>Terms and Conditions (terms-and-conditions)</li></ul>', CAST(N'2016-06-28 15:06:04.000' AS DateTime), CAST(N'2016-06-29 13:04:03.793' AS DateTime))
GO
INSERT [dbo].[BusinessPage] ([BusinessPageID], [PageNameUrl], [PageName], [PageContent], [CreatedDate], [UpdateDate]) VALUES (8, N'contact-us', N'Contact Us', N'<p>Contact Us<br></p><p> </p>', CAST(N'2016-06-28 16:10:17.000' AS DateTime), CAST(N'2016-06-29 13:05:14.377' AS DateTime))
GO
INSERT [dbo].[BusinessPage] ([BusinessPageID], [PageNameUrl], [PageName], [PageContent], [CreatedDate], [UpdateDate]) VALUES (9, N'ticket-purchase-faq', N'Ticket Purchase FAQ', N'<p>Ticket Purchase FAQ<br></p>', CAST(N'2016-06-29 13:08:46.900' AS DateTime), CAST(N'2016-06-29 13:08:46.900' AS DateTime))
GO
INSERT [dbo].[BusinessPage] ([BusinessPageID], [PageNameUrl], [PageName], [PageContent], [CreatedDate], [UpdateDate]) VALUES (10, N'ticket-fulfillment-policy', N'Ticket Fulfillment Policy', N'<p>Ticket Fulfillment Policy (ticket-fullfilment-policy)</p>', CAST(N'2016-06-29 13:09:28.000' AS DateTime), CAST(N'2016-06-30 13:15:23.863' AS DateTime))
GO
INSERT [dbo].[BusinessPage] ([BusinessPageID], [PageNameUrl], [PageName], [PageContent], [CreatedDate], [UpdateDate]) VALUES (11, N'privacy-policy', N'Privacy Policy', N'<p>Privacy Policy (privacy-policy)</p>', CAST(N'2016-06-29 13:10:13.113' AS DateTime), CAST(N'2016-06-29 13:10:13.113' AS DateTime))
GO
INSERT [dbo].[BusinessPage] ([BusinessPageID], [PageNameUrl], [PageName], [PageContent], [CreatedDate], [UpdateDate]) VALUES (12, N'server-error', N'500 Error code', N'<p>500 Error code</p>', CAST(N'2016-06-29 13:10:55.000' AS DateTime), CAST(N'2016-06-29 16:44:36.180' AS DateTime))
GO
INSERT [dbo].[BusinessPage] ([BusinessPageID], [PageNameUrl], [PageName], [PageContent], [CreatedDate], [UpdateDate]) VALUES (13, N'page-not-found', N'404 Error code', N'<p>404 Error code</p>', CAST(N'2016-06-29 13:11:32.000' AS DateTime), CAST(N'2016-06-29 16:44:56.590' AS DateTime))
GO
INSERT [dbo].[BusinessPage] ([BusinessPageID], [PageNameUrl], [PageName], [PageContent], [CreatedDate], [UpdateDate]) VALUES (22, N'unknown-error', N'Unknown Error', N'<p>Unknown Error<br></p>', CAST(N'2016-06-29 16:45:33.870' AS DateTime), CAST(N'2016-06-29 16:45:33.870' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[BusinessPage] OFF
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_BusinessPage]    Script Date: 6/30/2016 4:53:31 PM ******/
ALTER TABLE [dbo].[BusinessPage] ADD  CONSTRAINT [IX_BusinessPage] UNIQUE NONCLUSTERED 
(
	[PageNameUrl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
