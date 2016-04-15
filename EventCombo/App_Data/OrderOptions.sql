
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateGroupType]') AND type in (N'U'))
DROP TABLE [dbo].[OrderTemplateGroupType]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplateGroupTicket_DontDisplay]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplateGroupTicket] DROP CONSTRAINT [DF_OrderTemplateGroupTicket_DontDisplay]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplateGroupTicket_GroupOnly]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplateGroupTicket] DROP CONSTRAINT [DF_OrderTemplateGroupTicket_GroupOnly]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplateGroupTicket_EnableRegistration]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplateGroupTicket] DROP CONSTRAINT [DF_OrderTemplateGroupTicket_EnableRegistration]
END

GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateGroupTicket]') AND name = N'FK_TicketId')
DROP INDEX [FK_TicketId] ON [dbo].[OrderTemplateGroupTicket]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateGroupTicket]') AND name = N'FK_OrderTemplate')
DROP INDEX [FK_OrderTemplate] ON [dbo].[OrderTemplateGroupTicket]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateGroupTicket]') AND type in (N'U'))
DROP TABLE [dbo].[OrderTemplateGroupTicket]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ControlType]') AND type in (N'U'))
DROP TABLE [dbo].[ControlType]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[QuestionType]') AND name = N'FK_QuestionTypeGroupId')
DROP INDEX [FK_QuestionTypeGroupId] ON [dbo].[QuestionType]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuestionType]') AND type in (N'U'))
DROP TABLE [dbo].[QuestionType]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuestionTypeGroup]') AND type in (N'U'))
DROP TABLE [dbo].[QuestionTypeGroup]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Language]') AND type in (N'U'))
DROP TABLE [dbo].[Language]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateWaitlist]') AND name = N'FK_TicketId')
DROP INDEX [FK_TicketId] ON [dbo].[OrderTemplateWaitlist]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateWaitlist]') AND name = N'FK_OrderTemplateId')
DROP INDEX [FK_OrderTemplateId] ON [dbo].[OrderTemplateWaitlist]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateWaitlist]') AND type in (N'U'))
DROP TABLE [dbo].[OrderTemplateWaitlist]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateType]') AND type in (N'U'))
DROP TABLE [dbo].[OrderTemplateType]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplateTicket_DontDisplay]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplateTicket] DROP CONSTRAINT [DF_OrderTemplateTicket_DontDisplay]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplateTicket_GroupOnly]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplateTicket] DROP CONSTRAINT [DF_OrderTemplateTicket_GroupOnly]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplateTicket_EnableRegistration]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplateTicket] DROP CONSTRAINT [DF_OrderTemplateTicket_EnableRegistration]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplateTicket_Receive]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplateTicket] DROP CONSTRAINT [DF_OrderTemplateTicket_Receive]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplateTicket_CollectInformation]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplateTicket] DROP CONSTRAINT [DF_OrderTemplateTicket_CollectInformation]
END

GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateTicket]') AND name = N'FK_TicketId')
DROP INDEX [FK_TicketId] ON [dbo].[OrderTemplateTicket]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateTicket]') AND name = N'FK_OrderTemplateId')
DROP INDEX [FK_OrderTemplateId] ON [dbo].[OrderTemplateTicket]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateTicket]') AND type in (N'U'))
DROP TABLE [dbo].[OrderTemplateTicket]
GO


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateReceiveByType]') AND name = N'FK_TicketId')
DROP INDEX [FK_TicketId] ON [dbo].[OrderTemplateReceiveByType]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateReceiveByType]') AND name = N'FK_OrderTemplateId')
DROP INDEX [FK_OrderTemplateId] ON [dbo].[OrderTemplateReceiveByType]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateReceiveByType]') AND type in (N'U'))
DROP TABLE [dbo].[OrderTemplateReceiveByType]
GO


IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplateQuestionVariant_Quantity]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplateQuestionVariant] DROP CONSTRAINT [DF_OrderTemplateQuestionVariant_Quantity]
END

GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateQuestionVariant]') AND name = N'FK_OrderTemplateQuestion')
DROP INDEX [FK_OrderTemplateQuestion] ON [dbo].[OrderTemplateQuestionVariant]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateQuestionVariant]') AND type in (N'U'))
DROP TABLE [dbo].[OrderTemplateQuestionVariant]
GO


IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_Table_1_Allow]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplateQuestionTicket] DROP CONSTRAINT [DF_Table_1_Allow]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplateQuestionTicket_Allow]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplateQuestionTicket] DROP CONSTRAINT [DF_OrderTemplateQuestionTicket_Allow]
END

GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateQuestionTicket]') AND name = N'FK_TicketId')
DROP INDEX [FK_TicketId] ON [dbo].[OrderTemplateQuestionTicket]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateQuestionTicket]') AND name = N'FK_OrderTemplateQuestion')
DROP INDEX [FK_OrderTemplateQuestion] ON [dbo].[OrderTemplateQuestionTicket]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateQuestionTicket]') AND type in (N'U'))
DROP TABLE [dbo].[OrderTemplateQuestionTicket]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplateQuestion_ControlTypeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplateQuestion] DROP CONSTRAINT [DF_OrderTemplateQuestion_ControlTypeId]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplateQuestion_EnableSubquestions]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplateQuestion] DROP CONSTRAINT [DF_OrderTemplateQuestion_EnableSubquestions]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplateQuestion_SortNum]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplateQuestion] DROP CONSTRAINT [DF_OrderTemplateQuestion_SortNum]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplateQuestion_Require]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplateQuestion] DROP CONSTRAINT [DF_OrderTemplateQuestion_Require]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplateQuestion_Include]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplateQuestion] DROP CONSTRAINT [DF_OrderTemplateQuestion_Include]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplateQuestion_LimitQuantity]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplateQuestion] DROP CONSTRAINT [DF_OrderTemplateQuestion_LimitQuantity]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplateQuestion_ParentId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplateQuestion] DROP CONSTRAINT [DF_OrderTemplateQuestion_ParentId]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplateQuestion_ShowAnswer]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplateQuestion] DROP CONSTRAINT [DF_OrderTemplateQuestion_ShowAnswer]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplateQuestion_ShowForTickets]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplateQuestion] DROP CONSTRAINT [DF_OrderTemplateQuestion_ShowForTickets]
END

GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateQuestion]') AND name = N'FK_QuestionTypeId')
DROP INDEX [FK_QuestionTypeId] ON [dbo].[OrderTemplateQuestion]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateQuestion]') AND name = N'FK_ParentId')
DROP INDEX [FK_ParentId] ON [dbo].[OrderTemplateQuestion]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateQuestion]') AND name = N'FK_OrderTemplateId')
DROP INDEX [FK_OrderTemplateId] ON [dbo].[OrderTemplateQuestion]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateQuestion]') AND name = N'FK_ControlType')
DROP INDEX [FK_ControlType] ON [dbo].[OrderTemplateQuestion]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateQuestion]') AND type in (N'U'))
DROP TABLE [dbo].[OrderTemplateQuestion]
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateEventType]') AND type in (N'U'))
DROP TABLE [dbo].[OrderTemplateEventType]
GO


IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_TicketId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_TicketId]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_RespondTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_RespondTime]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_PhoneRequired]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_PhoneRequired]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_EmailRequired]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_EmailRequired]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_NameRequired]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_NameRequired]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_MaxSize]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_MaxSize]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_GroupMinutesBetween]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_GroupMinutesBetween]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_GroupAskIndividualTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_GroupAskIndividualTime]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_GroupRequireSetTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_GroupRequireSetTime]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_GroupAllowSetTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_GroupAllowSetTime]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_GroupRequirePassword]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_GroupRequirePassword]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_GroupAllowPassword]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_GroupAllowPassword]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_GroupMaxAttendees]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_GroupMaxAttendees]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_EnableWaitlist]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_EnableWaitlist]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_IncludePrintableTickets]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_IncludePrintableTickets]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_CustomIncludeSettings]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_CustomIncludeSettings]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_AcceptRefund]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_AcceptRefund]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_AllowEdit]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_AllowEdit]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_CallPickup]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_CallPickup]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_TimeLimit]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_TimeLimit]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_OrderTemplateTypeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] DROP CONSTRAINT [DF_OrderTemplate_OrderTemplateTypeId]
END

GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplate]') AND name = N'FK_WaitlistTicketId')
DROP INDEX [FK_WaitlistTicketId] ON [dbo].[OrderTemplate]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplate]') AND name = N'FK_OrderTemplateTypeId')
DROP INDEX [FK_OrderTemplateTypeId] ON [dbo].[OrderTemplate]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplate]') AND name = N'FK_OrderTemplateEventTypeID')
DROP INDEX [FK_OrderTemplateEventTypeID] ON [dbo].[OrderTemplate]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplate]') AND name = N'FK_LanguageId')
DROP INDEX [FK_LanguageId] ON [dbo].[OrderTemplate]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplate]') AND name = N'FK_GroupTypeId')
DROP INDEX [FK_GroupTypeId] ON [dbo].[OrderTemplate]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplate]') AND name = N'FK_EventID')
DROP INDEX [FK_EventID] ON [dbo].[OrderTemplate]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplate]') AND type in (N'U'))
DROP TABLE [dbo].[OrderTemplate]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplate]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OrderTemplate](
	[OrderTemplateId] [bigint] IDENTITY(1,1) NOT NULL,
	[EventID] [bigint] NOT NULL,
	[Title] [nvarchar](500) NOT NULL,
	[OrderTemplateTypeId] [bigint] NOT NULL,
	[Instruction] [nvarchar](2000) NULL,
	[TimeLimit] [int] NOT NULL,
	[AfterMessage] [nvarchar](1000) NULL,
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
	[OrderTemplateGroupTypeId] [bigint] NOT NULL,
	[GroupMaxAttendees] [bigint] NOT NULL,
	[GroupPageHeadline] [nvarchar](500) NULL,
	[GroupPageDescription] [nvarchar](2000) NULL,
	[GroupAllowPassword] [bit] NOT NULL,
	[GroupRequirePassword] [bit] NOT NULL,
	[GroupAllowSetTime] [bit] NOT NULL,
	[GroupRequireSetTime] [bit] NOT NULL,
	[GroupAskIndividualTime] [bit] NOT NULL,
	[GroupStartTime] [datetime] NULL,
	[GroupEndTime] [datetime] NULL,
	[GroupMinutesBetween] [int] NOT NULL,
	[MaxSize] [bigint] NOT NULL,
	[NameRequired] [bit] NOT NULL,
	[EmailRequired] [bit] NOT NULL,
	[PhoneRequired] [bit] NOT NULL,
	[RespondTime] [datetime] NOT NULL,
	[ResponseMessage] [nvarchar](max) NULL,
	[ReleaseMessage] [nvarchar](max) NULL,
 	[WaitlistTicketId] [bigint] NOT NULL,
CONSTRAINT [PK_OrderTemplate] PRIMARY KEY CLUSTERED 
(
	[OrderTemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplate]') AND name = N'FK_EventID')
CREATE NONCLUSTERED INDEX [FK_EventID] ON [dbo].[OrderTemplate]
(
	[EventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplate]') AND name = N'FK_GroupTypeId')
CREATE NONCLUSTERED INDEX [FK_GroupTypeId] ON [dbo].[OrderTemplate]
(
	[OrderTemplateGroupTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplate]') AND name = N'FK_LanguageId')
CREATE NONCLUSTERED INDEX [FK_LanguageId] ON [dbo].[OrderTemplate]
(
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplate]') AND name = N'FK_OrderTemplateEventTypeID')
CREATE NONCLUSTERED INDEX [FK_OrderTemplateEventTypeID] ON [dbo].[OrderTemplate]
(
	[OrderTemplateEventTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplate]') AND name = N'FK_OrderTemplateTypeId')
CREATE NONCLUSTERED INDEX [FK_OrderTemplateTypeId] ON [dbo].[OrderTemplate]
(
	[OrderTemplateTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplate]') AND name = N'FK_WaitlistTicketId')
CREATE NONCLUSTERED INDEX [FK_WaitlistTicketId] ON [dbo].[OrderTemplate]
(
	[WaitlistTicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_OrderTemplateTypeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_OrderTemplateTypeId]  DEFAULT ((0)) FOR [OrderTemplateTypeId]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_TimeLimit]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_TimeLimit]  DEFAULT ((0)) FOR [TimeLimit]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_CallPickup]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_CallPickup]  DEFAULT ((0)) FOR [AllowCallPickup]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_AllowEdit]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_AllowEdit]  DEFAULT ((1)) FOR [AllowEdit]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_AcceptRefund]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_AcceptRefund]  DEFAULT ((1)) FOR [AcceptRefund]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_CustomIncludeSettings]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_CustomIncludeSettings]  DEFAULT ((0)) FOR [CustomIncludeSettings]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_IncludePrintableTickets]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_IncludePrintableTickets]  DEFAULT ((0)) FOR [IncludePrintableTickets]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_EnableWaitlist]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_EnableWaitlist]  DEFAULT ((0)) FOR [EnableWaitlist]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_GroupMaxAttendees]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_GroupMaxAttendees]  DEFAULT ((0)) FOR [GroupMaxAttendees]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_GroupAllowPassword]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_GroupAllowPassword]  DEFAULT ((0)) FOR [GroupAllowPassword]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_GroupRequirePassword]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_GroupRequirePassword]  DEFAULT ((0)) FOR [GroupRequirePassword]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_GroupAllowSetTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_GroupAllowSetTime]  DEFAULT ((0)) FOR [GroupAllowSetTime]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_GroupRequireSetTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_GroupRequireSetTime]  DEFAULT ((0)) FOR [GroupRequireSetTime]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_GroupAskIndividualTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_GroupAskIndividualTime]  DEFAULT ((0)) FOR [GroupAskIndividualTime]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_GroupMinutesBetween]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_GroupMinutesBetween]  DEFAULT ((0)) FOR [GroupMinutesBetween]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_MaxSize]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_MaxSize]  DEFAULT ((0)) FOR [MaxSize]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_NameRequired]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_NameRequired]  DEFAULT ((1)) FOR [NameRequired]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_EmailRequired]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_EmailRequired]  DEFAULT ((1)) FOR [EmailRequired]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_PhoneRequired]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_PhoneRequired]  DEFAULT ((0)) FOR [PhoneRequired]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_RespondTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_RespondTime]  DEFAULT ((0)) FOR [RespondTime]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplate_TicketId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplate] ADD  CONSTRAINT [DF_OrderTemplate_TicketId]  DEFAULT ((0)) FOR [WaitlistTicketId]
END

GO




IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateEventType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OrderTemplateEventType](
	[OrderTemplateEventTypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_OrderTemplateEventType] PRIMARY KEY CLUSTERED 
(
	[OrderTemplateEventTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateQuestion]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OrderTemplateQuestion](
	[OrderTemplateQuestionId] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderTemplateId] [bigint] NOT NULL,
	[QuestionTypeId] [bigint] NOT NULL,
	[Include] [bit] NOT NULL CONSTRAINT [DF_OrderTemplateQuestion_Include]  DEFAULT ((0)),
	[Require] [bit] NOT NULL CONSTRAINT [DF_OrderTemplateQuestion_Require]  DEFAULT ((0)),
	[QuestionText] [nvarchar](255) NULL,
	[SortNum] [int] NOT NULL CONSTRAINT [DF_OrderTemplateQuestion_SortNum]  DEFAULT ((0)),
	[ControlTypeId] [bigint] NOT NULL CONSTRAINT [DF_OrderTemplateQuestion_ControlTypeId]  DEFAULT ((0)),
	[ShowForTickets] [bit] NOT NULL CONSTRAINT [DF_OrderTemplateQuestion_ShowForTickets]  DEFAULT ((0)),
	[LimitQuantity] [bit] NOT NULL CONSTRAINT [DF_OrderTemplateQuestion_LimitQuantity]  DEFAULT ((0)),
	[ShowAnswer] [bit] NOT NULL CONSTRAINT [DF_OrderTemplateQuestion_ShowAnswer]  DEFAULT ((0)),
	[EnableSubquestions] [bit] NOT NULL CONSTRAINT [DF_OrderTemplateQuestion_EnableSubquestions]  DEFAULT ((0)),
	[ParentId] [bigint] NOT NULL CONSTRAINT [DF_OrderTemplateQuestion_ParentId]  DEFAULT ((0)),
 CONSTRAINT [PK_OrderTemplateQuestion] PRIMARY KEY CLUSTERED 
(
	[OrderTemplateQuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateQuestion]') AND name = N'FK_ControlType')
CREATE NONCLUSTERED INDEX [FK_ControlType] ON [dbo].[OrderTemplateQuestion]
(
	[ControlTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateQuestion]') AND name = N'FK_OrderTemplateId')
CREATE NONCLUSTERED INDEX [FK_OrderTemplateId] ON [dbo].[OrderTemplateQuestion]
(
	[OrderTemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateQuestion]') AND name = N'FK_ParentId')
CREATE NONCLUSTERED INDEX [FK_ParentId] ON [dbo].[OrderTemplateQuestion]
(
	[ParentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateQuestion]') AND name = N'FK_QuestionTypeId')
CREATE NONCLUSTERED INDEX [FK_QuestionTypeId] ON [dbo].[OrderTemplateQuestion]
(
	[QuestionTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateQuestionTicket]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OrderTemplateQuestionTicket](
	[OrderTemplateQuestionTicketId] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderTemplateQuestionId] [bigint] NOT NULL,
	[TicketId] [bigint] NOT NULL,
	[CollectInformation] [bit] NOT NULL,
 CONSTRAINT [PK_OrderTemplateQuestionTicket] PRIMARY KEY CLUSTERED 
(
	[OrderTemplateQuestionTicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateQuestionTicket]') AND name = N'FK_OrderTemplateQuestion')
CREATE NONCLUSTERED INDEX [FK_OrderTemplateQuestion] ON [dbo].[OrderTemplateQuestionTicket]
(
	[OrderTemplateQuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateQuestionTicket]') AND name = N'FK_TicketId')
CREATE NONCLUSTERED INDEX [FK_TicketId] ON [dbo].[OrderTemplateQuestionTicket]
(
	[TicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplateQuestionTicket_Allow]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplateQuestionTicket] ADD  CONSTRAINT [DF_OrderTemplateQuestionTicket_Allow]  DEFAULT ((0)) FOR [CollectInformation]
END

GO


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateQuestionVariant]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OrderTemplateQuestionVariant](
	[OrderTemplateQuestionVariantId] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderTemplateQuestionId] [bigint] NOT NULL,
	[VariantText] [nvarchar](255) NULL,
	[Quantity] [bigint] NOT NULL,
 CONSTRAINT [PK_OrderTemplateQuestionVariant] PRIMARY KEY CLUSTERED 
(
	[OrderTemplateQuestionVariantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateQuestionVariant]') AND name = N'FK_OrderTemplateQuestion')
CREATE NONCLUSTERED INDEX [FK_OrderTemplateQuestion] ON [dbo].[OrderTemplateQuestionVariant]
(
	[OrderTemplateQuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplateQuestionVariant_Quantity]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplateQuestionVariant] ADD  CONSTRAINT [DF_OrderTemplateQuestionVariant_Quantity]  DEFAULT ((0)) FOR [Quantity]
END

GO


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateTicket]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OrderTemplateTicket](
	[OrderTemplateTicketId] [bigint] IDENTITY(1,1) NOT NULL,
	[TicketId] [bigint] NOT NULL,
	[OrderTemplateId] [bigint] NOT NULL,
	[CollectInformation] [bit] NOT NULL,
	[Receive] [bit] NOT NULL,
	[EnableRegistration] [bit] NOT NULL,
	[GroupOnly] [bit] NOT NULL,
	[DontDisplay] [bit] NOT NULL,
 CONSTRAINT [PK_OrderTemplateTicket] PRIMARY KEY CLUSTERED 
(
	[OrderTemplateTicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateTicket]') AND name = N'FK_OrderTemplateId')
CREATE NONCLUSTERED INDEX [FK_OrderTemplateId] ON [dbo].[OrderTemplateTicket]
(
	[OrderTemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateTicket]') AND name = N'FK_TicketId')
CREATE NONCLUSTERED INDEX [FK_TicketId] ON [dbo].[OrderTemplateTicket]
(
	[TicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplateTicket_CollectInformation]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplateTicket] ADD  CONSTRAINT [DF_OrderTemplateTicket_CollectInformation]  DEFAULT ((0)) FOR [CollectInformation]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplateTicket_Receive]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplateTicket] ADD  CONSTRAINT [DF_OrderTemplateTicket_Receive]  DEFAULT ((0)) FOR [Receive]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplateTicket_EnableRegistration]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplateTicket] ADD  CONSTRAINT [DF_OrderTemplateTicket_EnableRegistration]  DEFAULT ((0)) FOR [EnableRegistration]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplateTicket_GroupOnly]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplateTicket] ADD  CONSTRAINT [DF_OrderTemplateTicket_GroupOnly]  DEFAULT ((0)) FOR [GroupOnly]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_OrderTemplateTicket_DontDisplay]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderTemplateTicket] ADD  CONSTRAINT [DF_OrderTemplateTicket_DontDisplay]  DEFAULT ((0)) FOR [DontDisplay]
END

GO



IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OrderTemplateType](
	[OrderTemplateTypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderTemplateTypeName] [nvarchar](50) NULL,
 CONSTRAINT [PK_OrderTemplateType] PRIMARY KEY CLUSTERED 
(
	[OrderTemplateTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Language]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Language](
	[LanguageId] [bigint] IDENTITY(1,1) NOT NULL,
	[LanguageName] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED 
(
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuestionTypeGroup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[QuestionTypeGroup](
	[QuestionTypeGroupId] [bigint] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](600) NOT NULL,
	[SortNum] [bigint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_QuestionTypeGroup] PRIMARY KEY CLUSTERED 
(
	[QuestionTypeGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuestionType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[QuestionType](
	[QuestionTypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[QuestionTypeGroupId] [bigint] NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Visible] [bit] NOT NULL CONSTRAINT [DF_QuestionType_Visible]  DEFAULT ((0)),
	[Include] [bit] NOT NULL CONSTRAINT [DF_QuestionType_Include]  DEFAULT ((0)),
	[Require] [bit] NOT NULL CONSTRAINT [DF_QuestionType_Require]  DEFAULT ((0)),
	[MustInclude] [bit] NOT NULL CONSTRAINT [DF_QuestionType_MustInclude]  DEFAULT ((0)),
	[MustRequire] [bit] NOT NULL CONSTRAINT [DF_QuestionType_MustRequire]  DEFAULT ((0)),
	[SortNum] [bigint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_QuestionType] PRIMARY KEY CLUSTERED 
(
	[QuestionTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[QuestionType]') AND name = N'FK_QuestionTypeGroupId')
CREATE NONCLUSTERED INDEX [FK_QuestionTypeGroupId] ON [dbo].[QuestionType]
(
	[QuestionTypeGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ControlType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ControlType](
	[ControlTypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[ControlTypeName] [nvarchar](256) NOT NULL,
	[SortNum] [int] NOT NULL,
	[JSFunction] [nvarchar](1000) NULL,
 CONSTRAINT [PK_ControlType] PRIMARY KEY CLUSTERED 
(
	[ControlTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO



IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderTemplateGroupType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OrderTemplateGroupType](
	[OrderTemplateGroupTypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_OrderTemplateGroupType] PRIMARY KEY CLUSTERED 
(
	[OrderTemplateGroupTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO



SET IDENTITY_INSERT QuestionTypeGroup ON 
INSERT INTO QuestionTypeGroup (QuestionTypeGroupId, GroupName, SortNum)
VALUES (1, 'BASIC INFORMATION', 0)
INSERT INTO QuestionTypeGroup (QuestionTypeGroupId, GroupName, SortNum)
VALUES (2, 'ADDITIONAL INFORMATION', 100)
SET IDENTITY_INSERT QuestionTypeGroup OFF
GO

SET IDENTITY_INSERT QuestionType ON 
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(1, 1, 'Suffix', 1, 0, 0, 0, 0, 30)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(2, 1, 'Prefix (Mr., Mrs., etc.)', 1, 0, 0, 0, 0, 0)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(3, 1, 'First Name', 1, 1, 1, 1, 1, 10)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(4, 1, 'Last Name', 1, 1, 1, 1, 1,  20)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(5, 1, 'Email Address', 1, 1, 1, 1, 0, 40)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(6, 1, 'Home Phone', 1, 0, 0, 0, 0, 60)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(7, 2, 'Cell Phone', 1, 0, 0, 0, 0, 70)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(8, 2, 'Tax & Business Info', 1, 0, 0, 0, 0, 80)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(9, 2, 'Billing Address', 1, 1, 1, 1, 1, 90)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(10, 2, 'Card Info', 1, 1, 1, 1, 1, 100)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(11, 2, 'Home Address', 1, 0, 0, 0, 0, 50)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(12, 2, 'Shipping Address', 1, 0, 0, 0, 0, 110)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(13, 2, 'Job Title', 1, 0, 0, 0, 0, 120)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(14, 2, 'Company / Organization', 1, 0, 0, 0, 0, 130)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(15, 2, 'Work Address', 1, 0, 0, 0, 0, 140)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(16, 2, 'Work Phone', 1, 0, 0, 0, 0, 150)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(17, 2, 'Website', 1, 0, 0, 0, 0, 160)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(18, 2, 'Blog', 1, 0, 0, 0, 0, 170)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(19, 2, 'Gender', 1, 0, 0, 0, 0, 180)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(20, 2, 'Birth Date', 1, 0, 0, 0, 0, 190)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(21, 2, 'Age', 1, 0, 0, 0, 0, 200)
INSERT INTO QuestionType (QuestionTypeId, QuestionTypeGroupId, Title, Visible, [Include], Require, MustInclude, MustRequire, SortNum)
VALUES(22, 2, 'User question', 0, 0, 0, 0, 0, 210)
SET IDENTITY_INSERT QuestionType OFF
GO

SET IDENTITY_INSERT OrderTemplateType ON 
INSERT INTO [dbo].[OrderTemplateType] (OrderTemplateTypeId, OrderTemplateTypeName)
VALUES (1, 'Basic Information')
INSERT INTO [dbo].[OrderTemplateType] (OrderTemplateTypeId, OrderTemplateTypeName)
VALUES (2, 'Buyer Only')
INSERT INTO [dbo].[OrderTemplateType] (OrderTemplateTypeId, OrderTemplateTypeName)
VALUES (3, 'Each Attendee')
SET IDENTITY_INSERT OrderTemplateType OFF 
GO

SET IDENTITY_INSERT OrderTemplateEventType ON 
INSERT INTO OrderTemplateEventType (OrderTemplateEventTypeId, Name, [Description])
VALUES (1, 'Ticketed Event', '(Examples: Buy Tickets, Ticket Information, Ticket Type)')
INSERT INTO OrderTemplateEventType (OrderTemplateEventTypeId, Name, [Description])
VALUES (2, 'Registration Event', '(Examples: Register, Registration Information, Registration Type)')
SET IDENTITY_INSERT OrderTemplateEventType OFF
GO

SET IDENTITY_INSERT ControlType ON 
INSERT INTO ControlType (ControlTypeId, ControlTypeName, SortNum, JSFunction)
VALUES (1, 'Text', 10, '')
INSERT INTO ControlType (ControlTypeId, ControlTypeName, SortNum, JSFunction)
VALUES (2, 'Paragraph Text', 20, '')
INSERT INTO ControlType (ControlTypeId, ControlTypeName, SortNum, JSFunction)
VALUES (3, 'Checkbox', 30, 'CheckBoxMainContainer')
INSERT INTO ControlType (ControlTypeId, ControlTypeName, SortNum, JSFunction)
VALUES (4, 'Radio', 40, 'CheckBoxMainContainer')
INSERT INTO ControlType (ControlTypeId, ControlTypeName, SortNum, JSFunction)
VALUES (5, 'Dropdown', 50, 'CheckBoxMainContainer')
INSERT INTO ControlType (ControlTypeId, ControlTypeName, SortNum, JSFunction)
VALUES (6, 'Waiver', 60, 'waivierTxtDiv')
SET IDENTITY_INSERT ControlType OFF
GO

SET IDENTITY_INSERT [Language] ON 
INSERT INTO [Language] (LanguageId, LanguageName)
VALUES (1, 'English (US)')
INSERT INTO [Language] (LanguageId, LanguageName)
VALUES (2, 'Spanish (Spain)')
INSERT INTO [Language] (LanguageId, LanguageName)
VALUES (3, 'English (UK)')
INSERT INTO [Language] (LanguageId, LanguageName)
VALUES (4, 'German')
INSERT INTO [Language] (LanguageId, LanguageName)
VALUES (5, 'Italian')
INSERT INTO [Language] (LanguageId, LanguageName)
VALUES (6, 'Portuguese')
SET IDENTITY_INSERT [Language] OFF
GO

SET IDENTITY_INSERT [OrderTemplateGroupType] ON 
INSERT INTO [OrderTemplateGroupType] (OrderTemplateGroupTypeId, GroupName)
VALUES (1, 'Group')
INSERT INTO [OrderTemplateGroupType] (OrderTemplateGroupTypeId, GroupName)
VALUES (2, 'Team')
INSERT INTO [OrderTemplateGroupType] (OrderTemplateGroupTypeId, GroupName)
VALUES (3, 'Organization')
INSERT INTO [OrderTemplateGroupType] (OrderTemplateGroupTypeId, GroupName)
VALUES (4, 'Company')
INSERT INTO [OrderTemplateGroupType] (OrderTemplateGroupTypeId, GroupName)
VALUES (5, 'Association')
SET IDENTITY_INSERT [OrderTemplateGroupType] OFF
GO