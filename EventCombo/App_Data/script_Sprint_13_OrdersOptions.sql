IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'OrderTemplate'))
BEGIN

	ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_EnableWaitlist]
	ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_LanguageId]
	ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_OrderTemplateEventTypeID]
	ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_IncludePrintableTickets]
	ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_CustomIncludeSettings]
	ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_AcceptRefund]
	ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_AllowEdit]
	ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_CallPickup]
	ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_TimeLimit]
	ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_OrderTemplateTypeId]

  DROP INDEX [FK_OrderTemplateTypeId] ON [dbo].[OrderTemplate]
  DROP INDEX [FK_OrderTemplateEventTypeID] ON [dbo].[OrderTemplate]
  DROP INDEX [FK_EventID] ON [dbo].[OrderTemplate]

	DROP TABLE [dbo].[OrderTemplate]

END
GO

IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'OrderTemplateQuestion'))
BEGIN
	ALTER TABLE [dbo].[OrderTemplateQuestion] DROP CONSTRAINT [DF_OrderTemplateQuestion_Require]
	ALTER TABLE [dbo].[OrderTemplateQuestion] DROP CONSTRAINT [DF_OrderTemplateQuestion_Include]
  DROP INDEX [FK_QuestionTypeId] ON [dbo].[OrderTemplateQuestion]
  DROP INDEX [FK_OrderTemplateId] ON [dbo].[OrderTemplateQuestion]
	DROP TABLE [dbo].[OrderTemplateQuestion]
END
GO

IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'OrderTemplateTicket'))
BEGIN
	ALTER TABLE [dbo].[OrderTemplateTicket] DROP CONSTRAINT [DF_OrderTemplateTicket_CollectInformation]
  DROP INDEX [FK_TicketId] ON [dbo].[OrderTemplateTicket]
  DROP INDEX [FK_OrderTemplateId] ON [dbo].[OrderTemplateTicket]
	DROP TABLE [dbo].[OrderTemplateTicket]
END
GO

IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'OrderTemplateType'))
	DROP TABLE [dbo].[OrderTemplateType]
GO

IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'OrderTemplateWaitlist'))
BEGIN
	ALTER TABLE [dbo].[OrderTemplateWaitlist] DROP CONSTRAINT [DF_OrderTemplateWaitlist_RespondTime]
	ALTER TABLE [dbo].[OrderTemplateWaitlist] DROP CONSTRAINT [DF_OrderTemplateWaitlist_PhoneRequired]
	ALTER TABLE [dbo].[OrderTemplateWaitlist] DROP CONSTRAINT [DF_OrderTemplateWaitlist_EmailRequired]
	ALTER TABLE [dbo].[OrderTemplateWaitlist] DROP CONSTRAINT [DF_OrderTemplateWaitlist_NameRequired]
	ALTER TABLE [dbo].[OrderTemplateWaitlist] DROP CONSTRAINT [DF_OrderTemplateWaitlist_MaxSize]
	ALTER TABLE [dbo].[OrderTemplateWaitlist] DROP CONSTRAINT [DF_OrderTemplateWaitlist_TicketId]
	DROP INDEX [FK_OrderTemplateId] ON [dbo].[OrderTemplateWaitlist]
  DROP TABLE [dbo].[OrderTemplateWaitlist]
END
GO

IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'QuestionType'))
BEGIN
	DROP INDEX [FK_QuestionTypeGroupId] ON [dbo].[QuestionType]
  DROP TABLE [dbo].[QuestionType]
END
GO

IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'QuestionTypeGroup'))
	DROP TABLE [dbo].[QuestionTypeGroup]
GO

IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'OrderTemplateEventType'))
	DROP TABLE [dbo].[OrderTemplateEventType]
GO

IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'OrderTemplateReceiveByType'))
BEGIN
	ALTER TABLE [dbo].[OrderTemplateReceiveByType] DROP CONSTRAINT [DF_OrderTemplateReceiveByType_Receive]
	ALTER TABLE [dbo].[OrderTemplateReceiveByType] DROP CONSTRAINT [DF_OrderTemplateReceiveByType_TicketId]
	ALTER TABLE [dbo].[OrderTemplateReceiveByType] DROP  CONSTRAINT [DF_OrderTemplateReceiveByType_OrderTemplateId] 
	DROP INDEX [FK_TicketId] ON [dbo].[OrderTemplateReceiveByType]
	DROP INDEX [FK_OrderTemplateId] ON [dbo].[OrderTemplateReceiveByType]
	DROP TABLE [dbo].[OrderTemplateReceiveByType]
END
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OrderTemplate](
	[OrderTemplateId] [bigint] IDENTITY(1,1) NOT NULL,
	[EventID] [bigint] NOT NULL,
	[Title] [nvarchar](600) NOT NULL,
	[OrderTemplateTypeId] [bigint] NOT NULL,
	[Instruction] [nvarchar](max) NULL,
	[TimeLimit] [int] NOT NULL,
	[AfterMessage] [nvarchar](max) NULL,
	[AllowCallPickup] [bit] NOT NULL,
	[AllowEdit] [bit] NOT NULL,
	[AcceptRefund] [bit] NOT NULL,
	[ConfirmationMessage] [nvarchar](max) NULL,
	[ReplyEmail] [nvarchar](256) NULL,
	[TicketMessage] [nvarchar](max) NULL,
	[CustomIncludeSettings] [bit] NOT NULL,
	[IncludePrintableTickets] [bit] NOT NULL,
	[OrderTemplateEventTypeID] [bigint] NOT NULL,
	[LanguageId] [bigint] NOT NULL,
	[EnableWaitlist] [bit] NOT NULL,
 CONSTRAINT [PK_OrderTemplate] PRIMARY KEY CLUSTERED 
(
	[OrderTemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE NONCLUSTERED INDEX [FK_EventID] ON [dbo].[OrderTemplate]
(
	[EventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [FK_OrderTemplateEventTypeID] ON [dbo].[OrderTemplate]
(
	[OrderTemplateEventTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [FK_OrderTemplateTypeId] ON [dbo].[OrderTemplate]
(
	[OrderTemplateTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_OrderTemplateTypeId]  DEFAULT ((0)) FOR [OrderTemplateTypeId]
GO

ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_TimeLimit]  DEFAULT ((0)) FOR [TimeLimit]
GO

ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_CallPickup]  DEFAULT ((0)) FOR [AllowCallPickup]
GO

ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_AllowEdit]  DEFAULT ((1)) FOR [AllowEdit]
GO

ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_AcceptRefund]  DEFAULT ((1)) FOR [AcceptRefund]
GO

ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_CustomIncludeSettings]  DEFAULT ((0)) FOR [CustomIncludeSettings]
GO

ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_IncludePrintableTickets]  DEFAULT ((0)) FOR [IncludePrintableTickets]
GO

ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_OrderTemplateEventTypeID]  DEFAULT ((0)) FOR [OrderTemplateEventTypeID]
GO

ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_LanguageId]  DEFAULT ((0)) FOR [LanguageId]
GO

ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_EnableWaitlist]  DEFAULT ((0)) FOR [EnableWaitlist]
GO

CREATE TABLE [dbo].[OrderTemplateType](
	[OrderTemplateTypeId] [bigint] NOT NULL,
	[OrderTemplateTypeName] [nvarchar](50) NULL,
 CONSTRAINT [PK_OrderTemplateType] PRIMARY KEY CLUSTERED 
(
	[OrderTemplateTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[OrderTemplateTicket](
	[OrderTemplateTicketId] [bigint] IDENTITY(1,1) NOT NULL,
	[TicketId] [bigint] NOT NULL,
	[OrderTemplateId] [bigint] NOT NULL,
	[CollectInformation] [bit] NOT NULL,
 CONSTRAINT [PK_OrderTemplateTicket] PRIMARY KEY CLUSTERED 
(
	[OrderTemplateTicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE NONCLUSTERED INDEX [FK_OrderTemplateId] ON [dbo].[OrderTemplateTicket]
(
	[OrderTemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [FK_TicketId] ON [dbo].[OrderTemplateTicket]
(
	[TicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrderTemplateTicket] ADD  CONSTRAINT [DF_OrderTemplateTicket_CollectInformation]  DEFAULT ((0)) FOR [CollectInformation]
GO

CREATE TABLE [dbo].[OrderTemplateQuestion](
	[OrderTemplateQuestionId] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderTemplateId] [bigint] NOT NULL,
	[QuestionTypeId] [bigint] NOT NULL,
	[Include] [bit] NOT NULL,
	[Require] [bit] NOT NULL,
 CONSTRAINT [PK_OrderTemplateQuestion] PRIMARY KEY CLUSTERED 
(
	[OrderTemplateQuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE NONCLUSTERED INDEX [FK_OrderTemplateId] ON [dbo].[OrderTemplateQuestion]
(
	[OrderTemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [FK_QuestionTypeId] ON [dbo].[OrderTemplateQuestion]
(
	[QuestionTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrderTemplateQuestion] ADD  CONSTRAINT [DF_OrderTemplateQuestion_Include]  DEFAULT ((0)) FOR [Include]
GO

ALTER TABLE [dbo].[OrderTemplateQuestion] ADD  CONSTRAINT [DF_OrderTemplateQuestion_Require]  DEFAULT ((0)) FOR [Require]
GO

CREATE TABLE [dbo].[QuestionType](
	[QuestionTypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[QuestionTypeGroupId] [bigint] NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Visible] [bit] NOT NULL,
	[Include] [bit] NOT NULL,
	[Require] [bit] NOT NULL,
	[MustInclude] [bit] NOT NULL,
	[MustRequire] [bit] NOT NULL,
 	[SortNum] [bigint] NOT NULL DEFAULT 0,
CONSTRAINT [PK_QuestionType] PRIMARY KEY CLUSTERED 
(
	[QuestionTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE NONCLUSTERED INDEX [FK_QuestionTypeGroupId] ON [dbo].[QuestionType]
(
	[QuestionTypeGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[QuestionType] ADD  CONSTRAINT [DF_QuestionType_Visible]  DEFAULT ((0)) FOR [Visible]
GO

ALTER TABLE [dbo].[QuestionType] ADD  CONSTRAINT [DF_QuestionType_Include]  DEFAULT ((0)) FOR [Include]
GO

ALTER TABLE [dbo].[QuestionType] ADD  CONSTRAINT [DF_QuestionType_Require]  DEFAULT ((0)) FOR [Require]
GO

ALTER TABLE [dbo].[QuestionType] ADD  CONSTRAINT [DF_QuestionType_MustInclude]  DEFAULT ((0)) FOR [MustInclude]
GO

ALTER TABLE [dbo].[QuestionType] ADD  CONSTRAINT [DF_QuestionType_MustRequire]  DEFAULT ((0)) FOR [MustRequire]
GO

CREATE TABLE [dbo].[QuestionTypeGroup](
	[QuestionTypeGroupId] [bigint] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](600) NOT NULL,
	[SortNum] [bigint] NOT NULL DEFAULT 0,
 CONSTRAINT [PK_QuestionTypeGroup] PRIMARY KEY CLUSTERED 
(
	[QuestionTypeGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET IDENTITY_INSERT QuestionTypeGroup ON 
INSERT INTO QuestionTypeGroup (QuestionTypeGroupId, GroupName, SortNum)
VALUES (1, 'Contact Information', 0)
INSERT INTO QuestionTypeGroup (QuestionTypeGroupId, GroupName, SortNum)
VALUES (2, 'Home Address', 100)
INSERT INTO QuestionTypeGroup (QuestionTypeGroupId, GroupName, SortNum)
VALUES (3, 'Shipping Address', 200)
INSERT INTO QuestionTypeGroup (QuestionTypeGroupId, GroupName, SortNum)
VALUES (4, 'Work Information', 300)
INSERT INTO QuestionTypeGroup (QuestionTypeGroupId, GroupName, SortNum)
VALUES (5, 'Other Information', 400)
SET IDENTITY_INSERT QuestionTypeGroup OFF

SET IDENTITY_INSERT QuestionType ON 
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(1, 1, 'Prefix (Mr., Mrs., etc.)', 1, 0, 0, 0, 0, 0)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(2, 1, 'First Name', 1, 1, 1, 1, 1, 10)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(3, 1, 'Last Name', 1, 1, 1, 1, 1,  20)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(4, 1, 'Suffix', 1, 0, 0, 0, 0, 30)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(5, 1, 'Email Address', 1, 1, 1, 1, 0, 40)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(6, 2, 'Home Phone', 1, 0, 0, 0, 0, 60)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(7, 2, 'Cell Phone', 1, 0, 0, 0, 0, 70)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(8, 4, 'Tax & Business Info', 1, 0, 0, 0, 0, 80)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(9, 5, 'Billing Address', 1, 1, 1, 1, 1, 90)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(10, 5, 'Card Info', 1, 1, 1, 1, 1, 100)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(11, 2, 'Home Address', 1, 0, 0, 0, 0, 50)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(12, 3, 'Shipping Address', 1, 0, 0, 0, 0, 110)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(13, 4, 'Job Title', 1, 0, 0, 0, 0, 120)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(14, 4, 'Company / Organization', 1, 0, 0, 0, 0, 130)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(15, 4, 'Work Address', 1, 0, 0, 0, 0, 140)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(16, 4, 'Work Phone', 1, 0, 0, 0, 0, 150)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(17, 4, 'Website', 1, 0, 0, 0, 0, 160)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(18, 4, 'Blog', 1, 0, 0, 0, 0, 170)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(19, 5, 'Gender', 1, 0, 0, 0, 0, 180)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(20, 5, 'Birth Date', 1, 0, 0, 0, 0, 190)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(21, 5, 'Age', 1, 0, 0, 0, 0, 200)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(22, 5, 'User question', 0, 0, 0, 0, 0, 210)
SET IDENTITY_INSERT QuestionType OFF


INSERT INTO [dbo].[OrderTemplateType] (OrderTemplateTypeId, OrderTemplateTypeName)
VALUES (1, 'Basic Information')
INSERT INTO [dbo].[OrderTemplateType] (OrderTemplateTypeId, OrderTemplateTypeName)
VALUES (2, 'Buyer Only')
INSERT INTO [dbo].[OrderTemplateType] (OrderTemplateTypeId, OrderTemplateTypeName)
VALUES (3, 'Each Attendee')
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OrderTemplateWaitlist](
	[OrderTemplateWaitlistId] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderTemplateId] [bigint] NOT NULL,
	[TicketId] [bigint] NOT NULL,
	[MaxSize] [bigint] NOT NULL,
	[NameRequired] [bit] NOT NULL,
	[EmailRequired] [bit] NOT NULL,
	[PhoneRequired] [bit] NOT NULL,
	[RespondTime] [datetime] NOT NULL,
	[ResponseMessage] [nvarchar](max) NULL,
	[ReleaseMessage] [nvarchar](max) NULL,
 CONSTRAINT [PK_OrderTemplateWaitlist] PRIMARY KEY CLUSTERED 
(
	[OrderTemplateWaitlistId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE NONCLUSTERED INDEX [FK_OrderTemplateId] ON [dbo].[OrderTemplateWaitlist]
(
	[OrderTemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [FK_TicketId] ON [dbo].[OrderTemplateWaitlist]
(
	[TicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrderTemplateWaitlist] ADD  CONSTRAINT [DF_OrderTemplateWaitlist_TicketId]  DEFAULT ((0)) FOR [TicketId]
GO

ALTER TABLE [dbo].[OrderTemplateWaitlist] ADD  CONSTRAINT [DF_OrderTemplateWaitlist_MaxSize]  DEFAULT ((0)) FOR [MaxSize]
GO

ALTER TABLE [dbo].[OrderTemplateWaitlist] ADD  CONSTRAINT [DF_OrderTemplateWaitlist_NameRequired]  DEFAULT ((1)) FOR [NameRequired]
GO

ALTER TABLE [dbo].[OrderTemplateWaitlist] ADD  CONSTRAINT [DF_OrderTemplateWaitlist_EmailRequired]  DEFAULT ((1)) FOR [EmailRequired]
GO

ALTER TABLE [dbo].[OrderTemplateWaitlist] ADD  CONSTRAINT [DF_OrderTemplateWaitlist_PhoneRequired]  DEFAULT ((0)) FOR [PhoneRequired]
GO

ALTER TABLE [dbo].[OrderTemplateWaitlist] ADD  CONSTRAINT [DF_OrderTemplateWaitlist_RespondTime]  DEFAULT ((0)) FOR [RespondTime]
GO

CREATE TABLE [dbo].[OrderTemplateEventType](
	[OrderTemplateEventTypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_OrderTemplateEventType] PRIMARY KEY CLUSTERED 
(
	[OrderTemplateEventTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET IDENTITY_INSERT OrderTemplateEventType ON 
INSERT INTO OrderTemplateEventType (OrderTemplateEventTypeId, Name, [Description])
VALUES (1, 'Ticketed Event', '(Examples: Buy Tickets, Ticket Information, Ticket Type)')
INSERT INTO OrderTemplateEventType (OrderTemplateEventTypeId, Name, [Description])
VALUES (2, 'Registration Event', '(Examples: Register, Registration Information, Registration Type)')
SET IDENTITY_INSERT OrderTemplateEventType OFF


CREATE TABLE [dbo].[OrderTemplateReceiveByType](
	[OrderTemplateReceiveByTypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderTemplateId] [bigint] NOT NULL,
	[TicketId] [bigint] NOT NULL,
	[Receive] [bit] NOT NULL,
 CONSTRAINT [PK_OrderTemplateReceiveByType] PRIMARY KEY CLUSTERED 
(
	[OrderTemplateReceiveByTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE NONCLUSTERED INDEX [FK_OrderTemplateId] ON [dbo].[OrderTemplateReceiveByType]
(
	[OrderTemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [FK_TicketId] ON [dbo].[OrderTemplateReceiveByType]
(
	[TicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrderTemplateReceiveByType] ADD  CONSTRAINT [DF_OrderTemplateReceiveByType_OrderTemplateId]  DEFAULT ((0)) FOR [OrderTemplateId]
GO

ALTER TABLE [dbo].[OrderTemplateReceiveByType] ADD  CONSTRAINT [DF_OrderTemplateReceiveByType_TicketId]  DEFAULT ((0)) FOR [TicketId]
GO

ALTER TABLE [dbo].[OrderTemplateReceiveByType] ADD  CONSTRAINT [DF_OrderTemplateReceiveByType_Receive]  DEFAULT ((0)) FOR [Receive]
GO
