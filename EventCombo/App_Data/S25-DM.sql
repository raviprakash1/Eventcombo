USE [EventCombo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROC [dbo].[PublishEvent]
(
	@EventId bigint,
	@UserId nvarchar(256)
)
AS 
BEGIN
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
  ELSE IF @MultiId <> 0
  BEGIN
	  IF ((UPPER(@Freq) = 'DAILY') OR (UPPER(@Freq) = 'CUSTOM'))
	  BEGIN
      DECLARE @LastDate datetime
		  SELECT 
		    @StartDate = StartingFrom + ' ' + StartTime,
        @EndDate = StartingFrom + ' ' + EndTime,
        @LastDate = StartingTo + ' ' + EndTime
		  FROM MultipleEvent 
      WHERE MultipleEventID = @MultiId
		
		  WHILE (@EndDate <= @LastDate)
		  BEGIN
			  INSERT INTO @TSchedule (sdate, edate) 
        VALUES (@StartDate, @EndDate)			
			  SET @StartDate = DATEADD(dd, 1, @StartDate)
			  SET @EndDate = DATEADD(dd, 1, @EndDate)
		  END
	  END
    ELSE IF (UPPER(@Freq) = 'WEEKLY')
	  BEGIN
		  DECLARE @Weekly VARCHAR(50)
      DECLARE @CDate datetime
      DECLARE @StartTime varchar(20)
      DECLARE @EndTime varchar(20)

		  SELECT @Weekly = WeeklyDay,
		    @StartDate = StartingFrom + ' ' + StartTime,
        @EndDate = StartingTo + ' ' + EndTime,
        @StartTime = StartTime,
        @EndTime = EndTime
		  FROM MultipleEvent 
      WHERE MultipleEventID = @MultiId

		  DECLARE @TWeek TABLE(DayId INT) 
		  INSERT INTO @TWeek SELECT Element FROM [dbo].func_Split(@Weekly,',')  
		
      SET @CDate =  CONVERT(varchar, DATEADD(dd, -1, DATEADD(wk, DATEDIFF(wk, 0, @StartDate), 0)), 107)

      WHILE @CDate <= @EndDate
      BEGIN
        INSERT INTO @TSchedule (sdate, edate)
        SELECT CONVERT(varchar, DATEADD(dd, w.DayId, @CDate), 107) + ' ' + @StartTime sdate,
               CONVERT(varchar, DATEADD(dd, w.DayId, @CDate), 107) + ' ' + @EndTime edate
        FROM @TWeek w
        WHERE (CAST(CONVERT(varchar, DATEADD(dd, w.DayId, @CDate), 107) + ' ' + @StartTime as datetime) >= @StartDate) 
          AND (CAST(CONVERT(varchar, DATEADD(dd, w.DayId, @CDate), 107) + ' ' + @EndTime as datetime) <= @EndDate) 

        SET @CDate = DATEADD(wk, 1, @CDate);
      END
	  END ELSE IF (UPPER(@Freq) = 'MONTHLY')
	  BEGIN
      SELECT 'Not implemented yet'
    END
  END

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

  UPDATE ped
  SET ped.PE_Address_Ids = @Addresses,
      ped.PE_MultipleVenue_id = @MultiId,
      ped.PE_SingleVenue_Id = @SingleId,
      ped.PE_Tickets_Ids = @Tickets
  FROM Publish_Event_Detail ped
  WHERE ped.PE_Event_Id = @EventId

  INSERT INTO Publish_Event_Detail
    (
      PE_Event_Id, 
      PE_Address_Ids, 
      PE_MultipleVenue_id, 
      PE_SingleVenue_Id, 
      PE_Tickets_Ids, 
      PE_Scheduled_Date, 
      PE_Start_Time, 
      PE_End_Time 
    )
  SELECT @EventId EventId, @Addresses AddressesIds, @MultiId MulrtiId, @SingleId SingleId, @Tickets TicketIds,
    CONVERT(varchar, s.sdate, 107) StartDate, 
    REPLACE(SUBSTRING(CONVERT(varchar, s.sdate ,100), 13, 7), ' ', '0') StartTime, 
    REPLACE(SUBSTRING(CONVERT(varchar, s.edate ,100), 13, 7), ' ', '0') EndTime
  FROM @TSchedule s
  WHERE NOT EXISTS
    (
      SELECT ed.PE_Id
      FROM Publish_Event_Detail ed
      WHERE ed.PE_Event_Id = @EventId
        AND (s.sdate = CAST(ed.PE_Scheduled_Date + ' ' + ed.PE_Start_Time as datetime))
        AND (s.edate = CAST(CONVERT(varchar, s.edate, 107) + ' ' + ed.PE_End_Time as datetime))
    )

  DELETE tqd
  FROM Ticket_Quantity_Detail tqd
  WHERE tqd.TQD_Event_Id = @EventId 
    AND NOT EXISTS
    (
      SELECT tpd.TPD_Id
      FROM Ticket_Purchased_Detail tpd
      WHERE tpd.TPD_TQD_Id = tqd.TQD_Id
    )
    AND NOT EXISTS
    (
      SELECT ed.PE_Id
      FROM Publish_Event_Detail ed
      INNER JOIN Ticket t ON t.E_Id = ed.PE_Event_Id
      LEFT JOIN [Address] a ON a.EventId = ed.PE_Event_Id
      WHERE ed.PE_Event_Id = @EventId AND ed.PE_Id = tqd.TQD_PE_Id 
        AND t.T_Id = tqd.TQD_Ticket_Id AND ISNULL(a.AddressID, 0) = tqd.TQD_AddressId
    )

  UPDATE tqd
  SET tqd.TQD_Quantity = t.Qty_Available,
      tqd.TQD_Remaining_Quantity = CASE WHEN t.Qty_Available >= ISNULL(pt.PurchasedQty, 0) THEN t.Qty_Available - ISNULL(pt.PurchasedQty, 0) ELSE 0 END,
      tqd.TQD_StartDate = ed.PE_Scheduled_Date,
      tqd.TQD_StartTime = ed.PE_Start_Time
  FROM Ticket_Quantity_Detail tqd
  INNER JOIN Publish_Event_Detail ed ON ed.PE_Id = tqd.TQD_PE_Id
  INNER JOIN Ticket t ON t.T_Id = tqd.TQD_Ticket_Id
  LEFT JOIN
  (
    SELECT tpd.TPD_TQD_Id, SUM(tpd.TPD_Purchased_Qty) PurchasedQty
    FROM Ticket_Purchased_Detail tpd
    WHERE tpd.TPD_Event_Id = @EventId
    GROUP BY tpd.TPD_TQD_Id
  ) pt ON pt.TPD_TQD_Id = tqd.TQD_Id
  WHERE tqd.TQD_Event_Id = @EventId

  INSERT INTO Ticket_Quantity_Detail 
  (
    TQD_PE_Id, 
    TQD_Event_Id, 
    TQD_Ticket_Id, 
    TQD_AddressId, 
    TQD_Quantity, 
    TQD_Remaining_Quantity, 
    TQD_StartDate, 
    TQD_StartTime
  )
  SELECT ed.PE_Id, 
    ed.PE_Event_Id,
    t.T_Id,
    ISNULL(a.AddressID, 0) AddressId,
    t.Qty_Available,
    t.Qty_Available,
    ed.PE_Scheduled_Date,
    ed.PE_Start_Time 
  FROM Publish_Event_Detail ed
  INNER JOIN Ticket t ON t.E_Id = ed.PE_Event_Id
  LEFT JOIN [Address] a ON a.EventId = ed.PE_Event_Id
  WHERE PE_Event_Id = @EventId AND NOT EXISTS
  (
    SELECT tqd.TQD_Id
    FROM Ticket_Quantity_Detail tqd
    WHERE tqd.TQD_Event_Id = @EventId AND tqd.TQD_PE_Id = ed.PE_Id AND tqd.TQD_Ticket_Id = t.T_Id 
      AND ISNULL(tqd.TQD_AddressId, 0) = ISNULL(a.AddressID, 0)
  )
END


GO
