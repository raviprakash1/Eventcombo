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
ALTER TABLE dbo.Ticket_Purchased_Detail ADD
       TicketDiscount numeric(18, 2) NOT NULL CONSTRAINT DF_Ticket_Purchased_Detail_Discount DEFAULT 0,
       TicketPrice numeric(18, 2) NOT NULL CONSTRAINT DF_Ticket_Purchased_Detail_TicketPrice DEFAULT 0,
       TicketECFee numeric(18, 2) NOT NULL CONSTRAINT DF_Ticket_Purchased_Detail_TicketECFee DEFAULT 0,
       TicketMerchantFee numeric(18, 2) NOT NULL CONSTRAINT DF_Ticket_Purchased_Detail_TicketMerchantFee DEFAULT 0,
       TicketFeeType tinyint NOT NULL CONSTRAINT DF_Ticket_Purchased_Detail_TicketFeeType DEFAULT 1
GO
ALTER TABLE dbo.Ticket_Purchased_Detail SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
 
GO
 
-- Initialization for existing tickets
UPDATE tpd
SET
  tpd.TicketDiscount = ISNULL(t.T_Discount, 0),
  tpd.TicketPrice = ISNULL(t.Price, 0),
  tpd.TicketECFee = CASE WHEN ISNULL(tpd.TPD_EC_Fee, 0) > ISNULL(t.T_EcAmount, 0)
        THEN ISNULL(tpd.TPD_EC_Fee, 0) - ISNULL(t.T_EcAmount, 0)
        ELSE CAST(ROUND(ISNULL(t.Price * t.T_Ecpercent / 100, 0), 2) as decimal(18,2)) END,
  tpd.TicketMerchantFee = CASE WHEN t.TicketTypeID = 1 THEN 0 ELSE ISNULL(t.T_EcAmount, 0) END ,
  tpd.TicketFeeType = CAST(ISNULL(t.Fees_Type, '1') as tinyint)
FROM Ticket_Purchased_Detail tpd
INNER JOIN Ticket_Quantity_Detail tqd ON tqd.TQD_Id = tpd.TPD_TQD_Id
INNER JOIN Ticket t ON t.T_Id = tqd.TQD_Ticket_Id
 
GO