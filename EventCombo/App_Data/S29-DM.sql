IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_LockOrder_Locktime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LockOrder] DROP CONSTRAINT [DF_LockOrder_Locktime]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_LockOrder_LockOrderId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LockOrder] DROP CONSTRAINT [DF_LockOrder_LockOrderId]
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LockOrder]') AND type in (N'U'))
DROP TABLE [dbo].[LockOrder]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LockOrder]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LockOrder](
	[LockOrderId] [uniqueidentifier] NOT NULL,
	[EventId] [bigint] NOT NULL,
	[Locktime] [datetime] NOT NULL,
	[UserId] [nvarchar](128) NULL,
	[IP] [varchar](15) NOT NULL,
 CONSTRAINT [PK_LockOrder] PRIMARY KEY NONCLUSTERED 
(
	[LockOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

SET ANSI_PADDING OFF
GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_LockOrder_LockOrderId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LockOrder] ADD  CONSTRAINT [DF_LockOrder_LockOrderId]  DEFAULT (newid()) FOR [LockOrderId]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_LockOrder_Locktime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LockOrder] ADD  CONSTRAINT [DF_LockOrder_Locktime]  DEFAULT (getutcdate()) FOR [Locktime]
END

GO



IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_LockTicket_CustomerFee]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LockTicket] DROP CONSTRAINT [DF_LockTicket_CustomerFee]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_LockTicket_ECFeeType]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LockTicket] DROP CONSTRAINT [DF_LockTicket_ECFeeType]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_LockTicket_ECFeePercent]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LockTicket] DROP CONSTRAINT [DF_LockTicket_ECFeePercent]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_LockTicket_ECFeeAmount]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LockTicket] DROP CONSTRAINT [DF_LockTicket_ECFeeAmount]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_LockTicket_Amount]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LockTicket] DROP CONSTRAINT [DF_LockTicket_Amount]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_LockTicket_Donate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LockTicket] DROP CONSTRAINT [DF_LockTicket_Donate]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_LockTicket_Quantity]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LockTicket] DROP CONSTRAINT [DF_LockTicket_Quantity]
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LockTicket]') AND type in (N'U'))
DROP TABLE [dbo].[LockTicket]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LockTicket]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LockTicket](
	[LockTicketId] [bigint] IDENTITY(1,1) NOT NULL,
	[LockOrderId] [uniqueidentifier] NOT NULL,
	[TicketQuantityDetailId] [bigint] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Donate] [decimal](18, 2) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[ECFeeAmount] [decimal](18, 2) NOT NULL,
	[ECFeePercent] [decimal](18, 2) NOT NULL,
	[ECFeeType] [int] NOT NULL,
	[CustomerFee] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_LockTicket] PRIMARY KEY CLUSTERED 
(
	[LockTicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_LockTicket_Quantity]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LockTicket] ADD  CONSTRAINT [DF_LockTicket_Quantity]  DEFAULT ((0)) FOR [Quantity]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_LockTicket_Donate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LockTicket] ADD  CONSTRAINT [DF_LockTicket_Donate]  DEFAULT ((0)) FOR [Donate]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_LockTicket_Amount]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LockTicket] ADD  CONSTRAINT [DF_LockTicket_Amount]  DEFAULT ((0)) FOR [Amount]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_LockTicket_ECFeeAmount]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LockTicket] ADD  CONSTRAINT [DF_LockTicket_ECFeeAmount]  DEFAULT ((0)) FOR [ECFeeAmount]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_LockTicket_ECFeePercent]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LockTicket] ADD  CONSTRAINT [DF_LockTicket_ECFeePercent]  DEFAULT ((0)) FOR [ECFeePercent]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_LockTicket_ECFeeType]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LockTicket] ADD  CONSTRAINT [DF_LockTicket_ECFeeType]  DEFAULT ((0)) FOR [ECFeeType]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_LockTicket_CustomerFee]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LockTicket] ADD  CONSTRAINT [DF_LockTicket_CustomerFee]  DEFAULT ((0)) FOR [CustomerFee]
END

GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[AvailableTicket_View]'))
DROP VIEW [dbo].[AvailableTicket_View]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[AvailableTicket_View]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[AvailableTicket_View]
AS

SELECT tqd.TQD_Id, tqd.TQD_Event_Id, tqd.TQD_AddressId, tqd.TQD_PE_Id, tqd.TQD_Quantity,
  CASE  WHEN ISNULL(tqd.TQD_Remaining_Quantity, 0) > ISNULL(lt.Quantity, 0) 
        THEN ISNULL(tqd.TQD_Remaining_Quantity, 0) - ISNULL(lt.Quantity, 0)
        ELSE 0 END RemainingQuantity,
  tqd.TQD_StartDate, tqd.TQD_StartTime, tqd.TQD_Ticket_Id
FROM Ticket_Quantity_Detail tqd
LEFT JOIN LockTicket lt ON lt.TicketQuantityDetailId = tqd.TQD_Id

' 
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteExpiredLocks]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeleteExpiredLocks]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteExpiredLocks]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[DeleteExpiredLocks] AS' 
END
GO

-- =============================================
-- Description:	Delete expired locks for tickets
-- =============================================
ALTER PROCEDURE [dbo].[DeleteExpiredLocks] 
	@minutes int
AS
BEGIN
	SET NOCOUNT ON;

    
  BEGIN TRY

    BEGIN TRANSACTION;

    SELECT lo.LockOrderId
    INTO #lo
    FROM LockOrder lo
    WHERE DATEDIFF(mm, lo.Locktime, GETUTCDATE()) > @minutes;

    DELETE 
    FROM LockTicket
    WHERE EXISTS(SELECT LockOrderId FROM #lo WHERE #lo.LockOrderId = LockTicket.LockOrderId);

    DELETE 
    FROM LockOrder
    WHERE EXISTS(SELECT LockOrderId FROM #lo WHERE #lo.LockOrderId = LockOrder.LockOrderId);

    DROP TABLE #lo;

    COMMIT TRANSACTION;
  END TRY
  BEGIN CATCH
    -- Rollback the transaction
    ROLLBACK TRANSACTION;

    -- Raise an error and return
    DECLARE @mess nvarchar(MAX);
    SET @mess = 'Error in stored procedure DeleteExpiredLocks. Code = ' + CAST(ERROR_NUMBER() AS nvarchar) + ', message: ' + ERROR_MESSAGE();
    RAISERROR (@mess, 16, 1);
  END CATCH;

END

GO

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
  tpd.TicketMerchantFee = CASE WHEN t.TicketTypeID = 1 THEN 0 ELSE ISNULL(t.T_EcAmount, 0) END,
  tpd.TicketFeeType = CAST(ISNULL(t.Fees_Type, '1') as tinyint)
FROM Ticket_Purchased_Detail tpd
INNER JOIN Ticket_Quantity_Detail tqd ON tqd.TQD_Id = tpd.TPD_TQD_Id
INNER JOIN Ticket t ON t.T_Id = tqd.TQD_Ticket_Id

GO




