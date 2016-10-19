/****** Object:  View [dbo].[TicketAttendee_View]    Script Date: 19-Oct-16 4:50:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[TicketAttendee_View]
AS
SELECT        
		TicketAttendee.TicketAttendeeId, 
		TicketAttendee.TicketBearerId, 
		TicketBearer.UserId, 
		TicketBearer.OrderId, 
		TicketBearer.Guid, 
		TicketBearer.Name, 
		TicketBearer.Email, 
		TicketBearer.PhoneNumber, 
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


