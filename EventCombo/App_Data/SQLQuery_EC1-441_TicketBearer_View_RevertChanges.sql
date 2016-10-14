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


