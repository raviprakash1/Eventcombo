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


/****** Object:  View [dbo].[TicketBearer_View]    Script Date: 14-Oct-16 11:30:41 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER VIEW [dbo].[TicketBearer_View]
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


/****** Object:  View [dbo].[TicketAttendee_View]    Script Date: 14-Oct-16 3:27:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[TicketAttendee_View]
AS
SELECT        
		TicketAttendee.TicketAttendeeId, 
		TicketAttendee.TicketBearerId, 
		TicketBearer.UserId, 
		TicketBearer.OrderId, 
		TicketBearer.Guid, 
		TicketBearer.Name, 
		TicketBearer.Email, 
		TicketAttendee.PurchasedTicketId, 
		Ticket_Purchased_Detail.TPD_User_Id, 
		Ticket_Purchased_Detail.TPD_Order_Id, 
		Ticket_Purchased_Detail.TPD_Purchased_Qty, 
		Ticket_Purchased_Detail.TPD_TQD_Id, 
		Ticket_Purchased_Detail.TPD_Event_Id, 
		Ticket_Purchased_Detail.TPD_Amount, 
		Ticket_Purchased_Detail.TPD_Donate, 
		Ticket_Purchased_Detail.TPD_GUID, 
		Ticket_Purchased_Detail.TPD_EC_Fee, 
		Ticket_Purchased_Detail.TPD_PromoCodeID, 
		Ticket_Purchased_Detail.TPD_PromoCodeAmount, 
		Ticket_Purchased_Detail.Customer_Fee, 
		TicketAttendee.Quantity,
		Ticket.T_name

FROM  TicketAttendee 
INNER JOIN TicketBearer ON TicketAttendee.TicketBearerId = TicketBearer.TicketbearerId 
INNER JOIN Ticket_Purchased_Detail ON TicketAttendee.PurchasedTicketId = Ticket_Purchased_Detail.TPD_Id
INNER JOIN Ticket_Quantity_Detail ON Ticket_Quantity_Detail.TQD_Id = Ticket_Purchased_Detail.TPD_TQD_Id
INNER JOIN Ticket ON Ticket.T_Id = Ticket_Quantity_Detail.TQD_Ticket_Id

GO

UPDATE Email_Template
SET TemplateHtml='<table border="0" cellpadding="0" cellspacing="0" style="width:100%">
	<tbody>
		<tr>
			<td>
			<table>
				<tbody>
					<tr>
						<td><a href="¶¶CreateEventurl¶¶">Create Event</a> <a href="¶¶DiscoverEventurl¶¶">Discover Events</a> <a href="¶¶MyTicketEventurl¶¶">My Tickets</a></td>
					</tr>
				</tbody>
			</table>

			<table style="width:100%">
				<tbody>
					<tr>
						<td>
						<table>
							<tbody>
								<tr>
									<td>
									<table>
										<tbody>
											<tr>
												<td><img src="http://eventcombonew-qa.kiwireader.com/Images/logo_vertical.png" /></td>
												<td>
												<h1>Hi &para;&para;UserFirstNameID&para;&para;&nbsp;&nbsp;, this is your e-ticket confirmation for &para;&para;EventTitleId&para;&para; Organized by &para;&para;EventOrganiserName&para;&para;&nbsp;</h1>

												<p>Organized by &nbsp;&para;&para;EventOrganiserName&para;&para;&nbsp;</p>

												<p>&nbsp;</p>

												<h1>Get tickets :</h1>

												<p style="text-align:center"><img src="http://eventcombonew-qa.kiwireader.com/Images/paper_tic.png" /><br />
												Open the email attachement<br />
												or <a href="¶¶Downloadurl¶¶">download here </a></p>

												<h1>Question about this event ?</h1>

												<p>Contact the organizer at&nbsp;<a href="#">&nbsp;&para;&para;EventOrganiserEmail&para;&para;&nbsp;</a></p>
												</td>
											</tr>
											<tr>
												<td>&nbsp;</td>
												<td>&para;&para;EventDynamicTable&para;&para;</td>
											</tr>
											<tr>
												<td>&nbsp;</td>
												<td style="width:100%">
												<h1>About this event</h1>

												<p><img src="http://eventcombonew-qa.kiwireader.com/Images/em_ven_time.png" />&para;&para;EventStartDateID&para;&para;</p>

												<p style="text-align:center">To</p>

												<p>&para;&para;EventEndDateID&para;&para;</p>

												<p>Venue Name</p>

												<p><img src="http://eventcombonew-qa.kiwireader.com/Images/email_loc_icn.png" /> &para;&para;EventVenueID&para;&para;</p>
												&para;&para;EventMapImage&para;&para;

												<p>&para;&para;Eventtype&para;&para;</p>

												<p>&nbsp;</p>
												</td>
											</tr>
											<tr>
												<td>&nbsp;</td>
												<td style="width:100%"><img src="http://eventcombonew-qa.kiwireader.com/Images/login_img.png" />
												<h1>Your Account</h1>

												<p><a href="¶¶EventLogin¶¶">Log in </a> to access tickets and manage your orders.</p>
												</td>
											</tr>
										</tbody>
									</table>
									</td>
								</tr>
							</tbody>
						</table>
						</td>
					</tr>
				</tbody>
			</table>
			</td>
		</tr>
	</tbody>
</table>
'
WHERE Template_Tag='eticket' 

GO