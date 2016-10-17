/****** Object:  Table [dbo].[TicketAttendee]    Script Date: 14-Oct-16 3:27:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TicketAttendee](
	[TicketAttendeeId] [bigint] IDENTITY(1,1) NOT NULL,
	[TicketBearerId] [bigint] NOT NULL,
	[PurchasedTicketId] [bigint] NOT NULL,
	[Quantity] [bigint] NOT NULL,
 CONSTRAINT [PK_TicketAttendee] PRIMARY KEY CLUSTERED 
(
	[TicketAttendeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TicketAttendee]  WITH CHECK ADD  CONSTRAINT [FK_TicketAttendee_Ticket_Purchased_Detail] FOREIGN KEY([PurchasedTicketId])
REFERENCES [dbo].[Ticket_Purchased_Detail] ([TPD_Id])
GO

ALTER TABLE [dbo].[TicketAttendee] CHECK CONSTRAINT [FK_TicketAttendee_Ticket_Purchased_Detail]
GO

ALTER TABLE [dbo].[TicketAttendee]  WITH CHECK ADD  CONSTRAINT [FK_TicketAttendee_TicketBearer] FOREIGN KEY([TicketBearerId])
REFERENCES [dbo].[TicketBearer] ([TicketbearerId])
GO

ALTER TABLE [dbo].[TicketAttendee] CHECK CONSTRAINT [FK_TicketAttendee_TicketBearer]
GO


