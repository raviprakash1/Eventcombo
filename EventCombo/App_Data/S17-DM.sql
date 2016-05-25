-- Permission_Detail Start

DELETE FROM Permission_Detail
WHERE Permission_Id=13

SET IDENTITY_INSERT Permission_Detail ON
INSERT INTO Permission_Detail (Permission_Id, Permission_Desc, Permission_Category)
VALUES (13, 'Ticket Orders', 'CMS') 
SET IDENTITY_INSERT Permission_Detail OFF

-- Permission_Detail End
-- v_OrderList Start

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

-- v_OrderList End
-- PaymentType Start

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_PaymentType_Active]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PaymentType] DROP CONSTRAINT [DF_PaymentType_Active]
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PaymentType]') AND type in (N'U'))
DROP TABLE [dbo].[PaymentType]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PaymentType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PaymentType](
	[PaymentTypeId] [tinyint] IDENTITY(1,1) NOT NULL,
	[PaymentTypeName] [nvarchar](200) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_PaymentType] PRIMARY KEY CLUSTERED 
(
	[PaymentTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_PaymentType_Active]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PaymentType] ADD  CONSTRAINT [DF_PaymentType_Active]  DEFAULT ((1)) FOR [Active]
END

GO

SET IDENTITY_INSERT PaymentType ON
INSERT INTO PaymentType(PaymentTypeId, PaymentTypeName, Active)
VALUES(1, 'Paid with check', 1)
INSERT INTO PaymentType(PaymentTypeId, PaymentTypeName, Active)
VALUES(2, 'Paid cash', 1)
INSERT INTO PaymentType(PaymentTypeId, PaymentTypeName, Active)
VALUES(3, 'Paid via PayPal', 1)
INSERT INTO PaymentType(PaymentTypeId, PaymentTypeName, Active)
VALUES(4, 'Paid online non-PayPal', 1)
INSERT INTO PaymentType(PaymentTypeId, PaymentTypeName, Active)
VALUES(5, 'Complimentary', 1)
INSERT INTO PaymentType(PaymentTypeId, PaymentTypeName, Active)
VALUES(6, 'No payment necessary', 1)
INSERT INTO PaymentType(PaymentTypeId, PaymentTypeName, Active)
VALUES(7, 'Paid with voucher', 1)
INSERT INTO PaymentType(PaymentTypeId, PaymentTypeName, Active)
VALUES(8, 'Paid by credit card', 1)
INSERT INTO PaymentType(PaymentTypeId, PaymentTypeName, Active)
VALUES(9, 'Paid by debit card', 1)
INSERT INTO PaymentType(PaymentTypeId, PaymentTypeName, Active)
VALUES(10, 'Other', 1)
SET IDENTITY_INSERT PaymentType OFF

-- PaymentType End
-- Order_Detail_T Start

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
	PaymentTypeId tinyint NULL
GO
CREATE NONCLUSTERED INDEX FK_Order_Id ON dbo.Order_Detail_T
	(
	O_Order_Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX FK_PaymentTypeId ON dbo.Order_Detail_T
	(
	PaymentTypeId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX FK_OrderStateId ON dbo.Order_Detail_T
	(
	OrderStateId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.Order_Detail_T SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

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
	Note nvarchar(2000) NULL
GO
ALTER TABLE dbo.Order_Detail_T SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

-- Order_Detail_T End
