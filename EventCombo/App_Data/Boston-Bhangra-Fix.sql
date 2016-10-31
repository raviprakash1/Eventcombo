
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RecalcTicketQuantity]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RecalcTicketQuantity]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RecalcTicketQuantity]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[RecalcTicketQuantity] AS' 
END
GO

-- =============================================
-- Author:		Dmitriy Sisoev
-- Create date: 
-- Description:	Update remaining ticket quantity in the Ticket_Quantity_Detail
-- =============================================
ALTER PROCEDURE [dbo].[RecalcTicketQuantity] 
	-- Add the parameters for the stored procedure here
	@EventId bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

  UPDATE tqd
  SET tqd.TQD_Remaining_Quantity = tqd.TQD_Quantity - ISNULL(t.Quantity, 0)
  FROM Ticket_Quantity_Detail tqd
  LEFT JOIN 
  (
    SELECT tpd.TPD_TQD_Id, SUM(tpd.TPD_Purchased_Qty) Quantity
    FROM Ticket_Purchased_Detail tpd
    INNER JOIN Order_Detail_T o ON (o.O_Order_Id = tpd.TPD_Order_Id) AND (ISNULL(o.OrderStateId, 0) not in (2, 3))
    WHERE tpd.TPD_Event_Id = @EventId
    GROUP BY tpd.TPD_TQD_Id
  ) t ON tqd.TQD_Id = t.TPD_TQD_Id
  WHERE tqd.TQD_Event_Id = @EventId

END

GO


	DECLARE @EventId bigint
	DECLARE @UserId nvarchar(256)

  SET @EventId = 25916
  SELECT @UserId = e.UserID
  FROM [Event] e
  WHERE e.EventID = @EventId

  DECLARE @Addresses varchar(500)
  DECLARE @Tickets varchar(500)
  DECLARE @SingleId bigint
  DECLARE @MultiId bigint
  DECLARE @Freq VARCHAR(50)
  DECLARE @Days int
  DECLARE @StartDate datetime
  DECLARE @EndDate datetime

  SET @Addresses = ''
  SET @Tickets = ''

  SELECT @Addresses = @Addresses + CAST(a.AddressID as varchar) + ','
  FROM [Address] a
  WHERE a.EventId = @EventId
  SET @Addresses = SUBSTRING(@Addresses, 0, LEN(@Addresses) )

  SELECT @Tickets = @Tickets + CAST(t.T_Id as varchar) + ','
  FROM [Ticket] t
  WHERE t.E_Id = @EventId
  SET @Tickets = SUBSTRING(@Tickets, 0, LEN(@Tickets))

  SELECT @SingleId = ev.EventVenueID
  FROM EventVenue ev
  WHERE ev.EventID = @EventId
  SET @SingleId = ISNULL(@SingleId, 0)

  SELECT @MultiId = me.MultipleEventID, @Freq = me.Frequency
  FROM MultipleEvent me
  WHERE me.EventID = @EventId
  SET @MultiId = ISNULL(@MultiId, 0)

  DECLARE @TSchedule TABLE
  (
    sdate datetime,
    edate datetime
  )

  IF @SingleId <> 0
  BEGIN
    INSERT INTO @TSchedule (sdate, edate)
    SELECT ev.EventStartDate + ' ' + ev.EventStartTime sdate, ev.EventEndDate + ' ' + ev.EventEndTime edate
    FROM EventVenue ev
    WHERE ev.EventVenueID = @SingleId
  END

  DECLARE @TQDMerge TABLE
  (
    srcId bigint,
    dstId bigint
  )

  INSERT INTO @TQDMerge(srcId, dstId)
  SELECT tqd_src.TQD_Id srcId, tqd_dst.TQD_Id dstId 
  FROM @TSchedule s
  INNER JOIN Publish_Event_Detail ped_src ON ped_src.PE_Event_Id = @EventId 
        AND ((s.sdate <> CAST(ped_src.PE_Scheduled_Date + ' ' + ped_src.PE_Start_Time as datetime))
              OR (s.edate <> CAST(CONVERT(varchar, s.edate, 107) + ' ' + ped_src.PE_End_Time as datetime)))
        AND ped_src.PE_MultipleVenue_id = 0
  INNER JOIN Publish_Event_Detail ped_dst ON ped_dst.PE_Event_Id = @EventId 
        AND (s.sdate = CAST(ped_dst.PE_Scheduled_Date + ' ' + ped_dst.PE_Start_Time as datetime))
        AND (s.edate = CAST(CONVERT(varchar, s.edate, 107) + ' ' + ped_dst.PE_End_Time as datetime))
        AND ped_dst.PE_MultipleVenue_id = 0
  INNER JOIN Ticket_Quantity_Detail tqd_src ON tqd_src.TQD_PE_Id = ped_src.PE_Id
  INNER JOIN Ticket_Quantity_Detail tqd_dst ON tqd_dst.TQD_PE_Id = ped_dst.PE_Id AND tqd_src.TQD_Ticket_Id = tqd_dst.TQD_Ticket_Id

  UPDATE tpd
  SET tpd.TPD_TQD_Id = tqd.dstId
  FROM Ticket_Purchased_Detail tpd
  INNER JOIN @TQDMerge tqd ON tpd.TPD_TQD_Id = tqd.srcId

  EXEC RecalcTicketQuantity @EventId

  DELETE tqd
  FROM Ticket_Quantity_Detail tqd
  WHERE tqd.TQD_PE_Id IN
  (
    SELECT ped.PE_Id
    FROM Publish_Event_Detail ped
    WHERE ped.PE_Event_Id = @EventId AND NOT EXISTS 
      (
        SELECT tpd.TPD_Id 
        FROM Ticket_Purchased_Detail tpd 
        INNER JOIN Ticket_Quantity_Detail tqd ON tqd.TQD_Id = tpd.TPD_TQD_Id AND tqd.TQD_PE_Id = ped.PE_Id
      ) AND NOT EXISTS
      (
        SELECT s.sdate
        FROM @TSchedule s
        WHERE (s.sdate = CAST(ped.PE_Scheduled_Date + ' ' + ped.PE_Start_Time as datetime))
          AND (s.edate = CAST(CONVERT(varchar, s.edate, 107) + ' ' + ped.PE_End_Time as datetime))
      )
  )

  DELETE ped 
  FROM Publish_Event_Detail ped
  WHERE ped.PE_Event_Id = @EventId AND NOT EXISTS 
    (
      SELECT tpd.TPD_Id 
      FROM Ticket_Purchased_Detail tpd 
      INNER JOIN Ticket_Quantity_Detail tqd ON tqd.TQD_Id = tpd.TPD_TQD_Id AND tqd.TQD_PE_Id = ped.PE_Id
    ) AND NOT EXISTS
    (
      SELECT s.sdate
      FROM @TSchedule s
      WHERE (s.sdate = CAST(ped.PE_Scheduled_Date + ' ' + ped.PE_Start_Time as datetime))
        AND (s.edate = CAST(CONVERT(varchar, s.edate, 107) + ' ' + ped.PE_End_Time as datetime))
    )

    