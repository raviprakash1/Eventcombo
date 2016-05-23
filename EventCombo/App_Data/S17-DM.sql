SET IDENTITY_INSERT Permission_Detail ON
INSERT INTO Permission_Detail (Permission_Id, Permission_Desc, Permission_Category)
VALUES (13, 'Ticket Orders', 'CMS') 
SET IDENTITY_INSERT Permission_Detail OFF

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[v_OrderList]'))
DROP VIEW [dbo].[v_OrderList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[v_OrderList]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[v_OrderList]
AS

SELECT od.O_Id OId, od.O_Order_Id OrderId, od.O_First_Name FirstName, od.O_Last_Name LastName, od.O_Email Email,
  pr.City, pr.State, od.O_OrderDateTime OrderDateTime, e.EventTitle Name, tpdsum.Amount TotalPaid, tpdsum.Quantity, 
  CAST(ped.PE_Scheduled_Date AS DateTime) EventStartDate,
  CAST(CASE WHEN ISNULL(ped.PE_MultipleVenue_id, 0) > 0 THEN ped.PE_Scheduled_Date ELSE ev.EventEndDate END AS DateTime) EventEndDate,
  CAST(CASE WHEN UPPER(e.EventCancel) = ''Y'' THEN 1 ELSE 0 END as bit) EventCancelled,
  ISNULL(od.OrderStateId, 1) OrderStateId, os.OrderStateName, e.EventID, od.O_User_Id UserId
FROM Order_Detail_T od
LEFT JOIN 
  (
    SELECT t.TPD_Order_Id, SUM(t.TPD_Amount) Amount, SUM(t.TPD_Purchased_Qty) Quantity, MIN(t.TPD_Id) TPD_Id
    FROM Ticket_Purchased_Detail t
    GROUP BY TPD_Order_Id) tpdsum ON tpdsum.TPD_Order_Id = od.O_Order_Id
LEFT JOIN  Ticket_Purchased_Detail tpd ON tpd.TPD_Id = tpdsum.TPD_Id
LEFT JOIN [Event] e ON e.EventID = tpd.TPD_Event_Id
LEFT JOIN Ticket_Quantity_Detail tqd ON tqd.TQD_Id = tpd.TPD_TQD_Id
LEFT JOIN Publish_Event_Detail ped ON ped.PE_Id = tqd.TQD_PE_Id
LEFT JOIN [Profile] pr ON pr.UserID = od.O_User_Id
LEFT JOIN EventVenue  ev ON ev.EventVenueID = ped.PE_SingleVenue_Id
LEFT JOIN OrderState os ON ISNULL(od.OrderStateId, 1) = os.OrderStateId
' 
GO