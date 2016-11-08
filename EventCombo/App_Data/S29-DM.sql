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
	[Quantity] [int] NOT NULL CONSTRAINT [DF_LockTicket_Quantity]  DEFAULT ((0)),
	[Donate] [decimal](18, 2) NOT NULL CONSTRAINT [DF_LockTicket_Donate]  DEFAULT ((0)),
	[Price] [decimal](18, 2) NOT NULL CONSTRAINT [DF_LockTicket_Amount]  DEFAULT ((0)),
	[ECFeeAmount] [decimal](18, 2) NOT NULL CONSTRAINT [DF_LockTicket_ECFeeAmount]  DEFAULT ((0)),
	[ECFeePercent] [decimal](18, 2) NOT NULL CONSTRAINT [DF_LockTicket_ECFeePercent]  DEFAULT ((0)),
	[ECFeeType] [int] NOT NULL CONSTRAINT [DF_LockTicket_ECFeeType]  DEFAULT ((0)),
	[CustomerFee] [decimal](18, 2) NOT NULL CONSTRAINT [DF_LockTicket_CustomerFee]  DEFAULT ((0)),
	[Discount] [decimal](18, 2) NOT NULL CONSTRAINT [DF_LockTicket_Discount]  DEFAULT ((0)),
 CONSTRAINT [PK_LockTicket] PRIMARY KEY CLUSTERED 
(
	[LockTicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
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

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[State]') AND type in (N'U'))
DROP TABLE [dbo].[State]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[State]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[State](
	[StateId] [int] IDENTITY(1,1) NOT NULL,
	[StateName] [nvarchar](200) NOT NULL,
	[StateShortName] [nvarchar](200) NULL,
	[CountryId] [tinyint] NOT NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO


TRUNCATE TABLE [State]
GO
SET IDENTITY_INSERT [State] ON
GO
INSERT INTO [State] (StateId, StateName, StateShortName, CountryId) 
VALUES 
(1, 'Alabama', 'AL', 1),
(2, 'Alaska', 'AK', 1),
(3, 'Arizona', 'AZ', 1),
(4, 'Arkansas', 'AR', 1),
(5, 'California', 'CA', 1),
(6, 'Colorado', 'CO', 1),
(7, 'Connecticut', 'CT', 1),
(8, 'Delaware', 'DE', 1),
(9, 'Florida', 'FL', 1),
(10, 'Georgia', 'GA', 1),
(11, 'Hawaii', 'HI', 1),
(12, 'Idaho', 'ID', 1),
(13, 'Illinois', 'IL', 1),
(14, 'Indiana', 'IN', 1),
(15, 'Iowa', 'IA', 1),
(16, 'Kansas', 'KS', 1),
(17, 'Kentucky', 'KY', 1),
(18, 'Louisiana', 'LA', 1),
(19, 'Maine', 'ME', 1),
(20, 'Maryland', 'MD', 1),
(21, 'Massachusetts', 'MA', 1),
(22, 'Michigan', 'MI', 1),
(23, 'Minnesota', 'MN', 1),
(24, 'Mississippi', 'MS', 1),
(25, 'Missouri', 'MO', 1),
(26, 'Montana', 'MT', 1),
(27, 'Nebraska', 'NE', 1),
(28, 'Nevada', 'NV', 1),
(29, 'New Hampshire', 'NH', 1),
(30, 'New Jersey', 'NJ', 1),
(31, 'New Mexico', 'NM', 1),
(32, 'New York', 'NY', 1),
(33, 'North Carolina', 'NC', 1),
(34, 'North Dakota', 'ND', 1),
(35, 'Ohio', 'OH', 1),
(36, 'Oklahoma', 'OK', 1),
(37, 'Oregon', 'OR', 1),
(38, 'Pennsylvania', 'PA', 1),
(39, 'Rhode Island', 'RI', 1),
(40, 'South Carolina', 'SC', 1),
(41, 'South Dakota', 'SD', 1),
(42, 'Tennessee', 'TN', 1),
(43, 'Texas', 'TX', 1),
(44, 'Utah', 'UT', 1),
(45, 'Vermont', 'VT', 1),
(46, 'Virginia', 'VA', 1),
(47, 'Washington', 'WA', 1),
(48, 'West Virginia', 'WV', 1),
(49, 'Wisconsin', 'WI', 1),
(50, 'Wyoming', 'WY', 1),
(51, 'Washington DC', 'DC', 1),
(52, 'Puerto Rico', 'PR', 1),
(53, 'U.S. Virgin Islands', 'VI', 1),
(54, 'American Samoa', 'AS', 1),
(55, 'Guam', 'GU', 1),
(56, 'Northern Mariana Islands', 'MP', 1),
(60, 'Alberta', 'AB', 2),
(61, 'British Columbia', 'BC', 2),
(62, 'Manitoba', 'MB', 2),
(63, 'New Brunswick', 'NB', 2),
(64, 'Newfoundland and Labrador', 'NL', 2),
(65, 'Nova Scotia', 'NS', 2),
(66, 'Ontario', 'ON', 2),
(67, 'Prince Edward Island', 'PE', 2),
(68, 'Quebec', 'QC', 2),
(69, 'Saskatchewan', 'SK', 2),
(70, 'Northwest Territories', 'NT', 2),
(71, 'Nunavut', 'NU', 2),
(72, 'Yukon Territory', 'YT', 2)

GO
SET IDENTITY_INSERT [State] OFF
GO

