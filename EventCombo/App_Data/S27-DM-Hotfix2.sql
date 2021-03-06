
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
ALTER TABLE dbo.Order_Detail_T ADD
	IsManualOrder bit NOT NULL CONSTRAINT DF_Order_Detail_T_IsManualOrder DEFAULT 0
GO
ALTER TABLE dbo.Order_Detail_T SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

GO
/****** Object:  View [dbo].[EventTicket_View]    Script Date: 12-Oct-16 2:42:43 PM ******/
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
	CASE WHEN Ticket.TicketTypeID = 2 THEN ISNULL(Ticket_Purchased_Detail.TPD_EC_Fee,0)-ISNULL(Ticket.T_EcAmount,0) ELSE 0 END AS ECFeePerTicket,
	CASE WHEN Ticket.TicketTypeID = 2 OR Ticket.TicketTypeID = 3 THEN Ticket.T_EcAmount ELSE 0 END AS MerchantFeePerTicket,
	Ticket_Purchased_Detail.Customer_Fee AS Customer_Fee, 
	TicketType.TicketTypeID, 
	TicketType.TicketType AS TicketTypeName, 
	Ticket_Purchased_Detail.TPD_PromoCodeID AS PromoCodeID, 
	Promo_Code.PC_Code AS PromoCode,
	Ticket_Purchased_Detail.TPD_PromoCodeAmount AS PromoCodeAmount,
	Order_Detail_T.O_OrderDateTime,
	Order_Detail_T.OrderStateId,
	OrderState.OrderStateName,
	Order_Detail_T.PaymentTypeId,
	Order_Detail_T.IsManualOrder

FROM Ticket_Purchased_Detail
INNER JOIN Order_Detail_T ON Order_Detail_T.O_Order_Id=Ticket_Purchased_Detail.TPD_Order_Id
INNER JOIN Ticket_Quantity_Detail ON Ticket_Quantity_Detail.TQD_Id=Ticket_Purchased_Detail.TPD_TQD_Id
INNER JOIN Ticket ON Ticket.T_Id=Ticket_Quantity_Detail.TQD_Ticket_Id
INNER JOIN TicketType ON TicketType.TicketTypeID=Ticket.TicketTypeID
INNER JOIN Profile ON Profile.UserID = Ticket_Purchased_Detail.TPD_User_Id
INNER JOIN OrderState ON ISNULL(Order_Detail_T.OrderStateId, 1) = OrderState.OrderStateId
LEFT OUTER JOIN Promo_Code ON Promo_Code.PC_id=Ticket_Purchased_Detail.TPD_PromoCodeID

GO


