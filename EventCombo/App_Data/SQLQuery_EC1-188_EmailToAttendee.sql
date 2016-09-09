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
