USE [EventCombo]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






-- Navrit,  27/Oct/2015, 

--[PublishEvent 52,''] -- Single
--[PublishEvent 79,''] -- Daily
--PublishEvent 80,'' -- Weekly
--PublishEvent 81,'' -- MOnthly --Week wise
--PublishEvent 82,'' -- MOnthly --Single Day

ALTER PROC [dbo].[PublishEvent]
(
	@EventId bigint,
	@UserId nvarchar(256)
)
AS 
BEGIN
--Also need to consider Online Event
	Declare @RowCount int
	Declare @RowIndex int
	Declare @RowId int
	DECLARE @ESch BIGINT
	
	DECLARE @StartDate varchar(50)
	DECLARE @EndDate varchar(50)
	DECLARE @StartTime varchar(50)
	DECLARE @EndTime varchar(50)


	DECLARE @Ticketids varchar(MAX)
	DECLARE @Addressids varchar(MAX)




	SET @Ticketids = ''
	SET @Addressids =''
-- Order by was not working, need to work with temp table	
	
	DECLARE @Tbl TABLE (T_Id bigint,odId int)
	INSERT INTO @Tbl
	SELECT T_Id,T_order  FROM Ticket WHERE E_id = @EventId

	SELECT @Ticketids = @Ticketids + ',' + CONVERT(VARCHAR(MAX),T_Id) FROM @Tbl order by odId

	--SELECT @Ticketids = @Ticketids + ',' + CONVERT(VARCHAR(MAX),T_Id) FROM Ticket  WHERE E_id = @EventId 
	IF (@Ticketids != '')
		SET  @Ticketids = RIGHT(@Ticketids,Len(@Ticketids)-1)

	SELECT @Addressids = @Addressids + ',' + CONVERT(VARCHAR(Max),AddressID) from [Address]  where [Address].EventId  = @EventId
	IF (@Addressids !='')
		SET  @Addressids = Right(@Addressids,Len(@Addressids)-1)

	DECLARE @SingleDate int
	SET @SingleDate =0
	SELECT @SingleDate=EventVenueID FROM EventVenue WHERE EventID = @EventId
	DECLARE @tblMerge TABLE (Id int identity,EventId bigint,TicketsIDs varchar(Max),AddressIds varchar(Max),MutipleVId bigint,EventVId bigint, StartDate varchar(50), EndDate varchar(50), StartTime varchar(50), EndTime varchar(50))
	IF (@SingleDate>0)
	BEGIN
		SELECT @StartDate = CONVERT(VARCHAR,DATEADD(dd,0,EventStartDate),107),@EndDate=CONVERT(VARCHAR,DATEADD(dd,0,EventEndDate),107), @StartTime=EventStartTime,@EndTime=EventEndTime  
		FROM EventVenue WHERE EventID = @EventId

		INSERT INTO @tblMerge VALUES
		(@EventId,@Ticketids,@Addressids,0,@SingleDate,@StartDate,@EndDate,@StartTime,@EndTime)			

	END
	ELSE
	BEGIN
		
		DECLARE @Freq VARCHAR(50)
		DECLARE @MultipleId BIGINT

		SELECT @Freq = ISNULL(Frequency,'') FROM MultipleEvent WHERE EventID = @EventId
		-- 174 

		If (@Freq = 'Daily' or @Freq = 'Custom')
		BEGIN
			DECLARE @Days INT
			Set @Days =0
			SELECT @Days = DateDiff(dd,StartingFrom ,StartingTo), @MultipleId = MultipleEventID ,
			@StartDate = StartingFrom,@EndDate=StartingTo, @StartTime=StartTime,@EndTime=EndTime 
			FROM MultipleEvent WHERE EventID = @EventId
			SET @StartDate = CONVERT(VARCHAR,DATEADD(dd,0,@StartDate),107)
			WHILE (@Days>=0)
			BEGIN
				INSERT INTO @tblMerge VALUES (@EventId,@Ticketids,@Addressids,@MultipleId,0,@StartDate,'',@StartTime,@EndTime)			
				SET @StartDate = CONVERT(VARCHAR,DATEADD(dd,1,@StartDate),107)
				SET @Days = @Days-1
			END
		END
		Else If (@Freq = 'Weekly')
		BEGIN

			DECLARE @Weekly VARCHAR(50)
			SELECT @Weekly = WeeklyDay,@MultipleId = MultipleEventID ,
			@StartDate = StartingFrom,@EndDate=StartingTo, @StartTime=StartTime,@EndTime=EndTime 
			FROM MultipleEvent WHERE EventID = @EventId
			DECLARE @DateFrom DATE
			DECLARE @DateTo DATE
			
			SET @DateFrom = CONVERT(DATE,@StartDate)
			SET @DateTo = CONVERT(DATE,@EndDate)

			DECLARE @WeekDay VARCHAR(20)
			DECLARE @tblWeek TABLE(Id INT,DayId INT) 
			INSERT INTO @tblWeek SELECT * FROM [dbo].func_Split(@Weekly,',')  
			
			
			DECLARE @DayID INT
			DECLARE @RDayID INT
			SET @RDayID=0
			SET @DayID =0
			WHILE (@DateFrom<=@DateTo)
			BEGIN
				SET @DayID = dbo.ReturnDayOfWeek(@DateFrom) 
				SELECT @RDayID = Count(*) FROM @tblWeek WHERE DayId= @DayID
				if (@RDayID >0)
				BEGIN
					INSERT INTO @tblMerge VALUES
					(@EventId,@Ticketids,@Addressids,@MultipleId,0,convert(varchar,@DateFrom,107),'',@StartTime,@EndTime)		
				END
				SET @DateFrom =   DATEADD(dd,1,@DateFrom)
			
			END
		END

		Else If (@Freq = 'Monthly')
		BEGIN
			Declare @cnt int
			Declare @Dayofweek varchar(20)
			Declare @Monthlyday varchar(50)
			Declare @Monthlyweek varchar(50)
			Declare @MonthlyweekDays varchar(50)
			Declare @day varchar(50)
			SELECT @Days = DateDiff(dd,StartingFrom ,StartingTo),@MultipleId = MultipleEventID,
			@StartDate = StartingFrom,@StartTime=StartTime,@EndTime = EndTime ,@Monthlyday=MonthlyDay,@Monthlyweek=MonthlyWeek,
			@MonthlyweekDays=MonthlyWeekDays,@EndDate=StartingTo
			FROM MultipleEvent WHERE EventID = @EventId
   			Set @DateFrom = CONVERT(DATE,@StartDate)
            Set @DateTo = CONVERT(DATE,@EndDate)

			   
			IF(ISNULL(@Monthlyday,'')!='')
			BEGIN 
				WHILE (@StartDate<=@DateTo)
				BEGIN 
					SET @day=DATEPART(dd,@StartDate) 
					if(@day=@Monthlyday)
					BEGIN 
					  SELECT  @Dayofweek=datename(dw,@StartDate)
					  INSERT INTO @tblMerge VALUES
						(@EventId,@Ticketids,@Addressids,@MultipleId,0,CONVERT(VARCHAR,@StartDate,107),'',@StartTime,@EndTime)
					END
					SET @StartDate = DATEADD(dd,1,@StartDate) 
				END
			END
			ELSE IF(ISNULL(@Monthlyweek,'')!='')
			BEGIN
				DECLARE @monthday as int
				SELECT @monthday = CASE @Monthlyweek WHEN 'First' THEN 1 WHEN 'Second' THEN 2 WHEN 'Third' THEN 3 WHEN 'Four' THEN 4 END
				DECLARE @Noofweek VARCHAR(50)
				SET @cnt=1
				SELECT @Noofweek= DATEDIFF(week, @DateFrom, @DateTo)

				WHILE @cnt <= @Noofweek
				BEGIN
					if(@cnt=@monthday)
					BEGIN
						DECLARE @newstartdate AS VARCHAR(50)
						DECLARE @newenddate AS VARCHAR(50)
						SET @newstartdate=@StartDate
						SET @newenddate=DATEADD(dd,7,@newstartdate) 
						INSERT INTO @tblWeek SELECT * FROM [dbo].func_Split(@MonthlyweekDays,',') 
						WHILE (@newstartdate<=@newenddate)
						BEGIN
							SET @WeekDay = dbo.ReturnDayOfWeek(@newstartdate) 
							Select @DayID = count(*) from @tblWeek where DayId= @WeekDay
							IF (@DayID >0)
							BEGIN
								INSERT INTO @tblMerge VALUES
								(@EventId,@Ticketids,@Addressids,@MultipleId,0,CONVERT(VARCHAR,@newstartdate,107),'',@StartTime,@EndTime)
							END
							SET @newstartdate = DATEADD(dd,1,@newstartdate) 
						END
					END
					SET @StartDate = DATEADD(dd,7,@StartDate) 
					SET @cnt=@cnt+1
				END
			END
   	
		END
	END

--	select * From @tblMerge

	INSERT INTO [dbo].[Publish_Event_Detail]
           ([PE_Event_Id]
           ,[PE_Address_Ids]
		   ,[PE_Tickets_Ids]
           ,[PE_MultipleVenue_id]
           ,[PE_SingleVenue_Id]
           ,[PE_Scheduled_Date]
           ,[PE_Start_Time]
           ,[PE_End_Time])
	SELECT EventId,AddressIds,TicketsIDs,MutipleVId,EventVId,StartDate,StartTime,EndTime FROM @tblMerge

	--select * From [Publish_Event_Detail]
	DECLARE @tblFinal TABLE (Id INT IDENTITY,Publish_Id BIGINT, EventId BIGINT,TicketsIDs VARCHAR(MAX),AddressIds VARCHAR(MAX),StartDate VARCHAR(50), StartTime VARCHAR(50), EndTime VARCHAR(50))

	INSERT INTO @tblFinal
	SELECT PE_Id,PE_Event_Id,PE_Tickets_Ids,PE_Address_Ids,PE_Scheduled_Date,PE_Start_Time,PE_End_Time FROM Publish_Event_Detail WHERE PE_Event_Id = @EventId
	SET @RowCount = @@ROWCOUNT
	SET @RowIndex =1
	--select * From @tblFinal

	Declare @PublishId bigint
	Declare @Addresses varchar(500)
	Declare @Ticketss varchar(500)
	Declare @AddRowCot Int
	Declare @AddRowInd Int
	
	Declare @TicRowCot Int
	Declare @TicRowInd Int

	Declare @Addid int
	Declare @Tickid int
	Declare @TQuantity int
	Declare @Tpurchasedqty int
	declare @Tremaining int
	Declare @tblMVenue table (Id int,Element int)
	Declare @tblMTicket table (Id int,Element int)
	Set @PublishId=0
	Set @StartDate=''
	Set @StartTime=''
	WHILE (@RowCount >0)
	BEGIN
		SELECT @StartDate=StartDate,@PublishId=Publish_Id,@Addresses=isnull(AddressIds,''),@Ticketss=TicketsIDs,@StartTime=StartTime FROM @tblFinal WHERE Id=@RowIndex
		INSERT INTO @tblMVenue SELECT * FROM [dbo].func_Split(@Addresses,',') 
		SET @AddRowCot = @@ROWCOUNT
		SET @AddRowInd =1
		If (@Addresses != '')
		BEGIN
			WHILE (@AddRowCot>0)
			BEGIN
				SELECT @Addid = Element FROM @tblMVenue WHERE Id = @AddRowInd
				INSERT INTO @tblMTicket SELECT * FROM [dbo].func_Split(@Ticketss,',') 
				SET @TicRowCot = @@ROWCOUNT
				SET @TicRowInd =1
				WHILE (@TicRowCot>0)
				BEGIN
					SELECT @Tickid = Element FROM @tblMTicket WHERE Id = @TicRowInd
					SELECT @TQuantity = Qty_Available FROM Ticket WHERE T_Id = @Tickid
					select @Tpurchasedqty=isnull(sum(TPD_Purchased_Qty),0) from [dbo].[Ticket_Purchased_Detail] a join [Ticket_Quantity_Detail] b
                           on a.TPD_TQD_Id=b.TQD_Id  where TQD_Ticket_Id =@Tickid
				  set @Tremaining=@TQuantity-isnull(@Tpurchasedqty,0) 

					INSERT Ticket_Quantity_Detail ([TQD_PE_Id],[TQD_Event_Id],[TQD_Ticket_Id],[TQD_AddressId],[TQD_Quantity],[TQD_Remaining_Quantity],TQD_StartDate,TQD_StartTime)
					VALUES(@PublishId,@EventId,@Tickid,@Addid,@TQuantity,@Tremaining,@StartDate,@StartTime)
					SET @TicRowInd = @TicRowInd +1
					SET @TicRowCot = @TicRowCot -1
				END
				SET @AddRowInd = @AddRowInd +1
				SET @AddRowCot = @AddRowCot -1
			END
		END
		ELSE 
		BEGIN
				INSERT INTO @tblMTicket SELECT * FROM [dbo].func_Split(@Ticketss,',') 
				SET @TicRowCot = @@ROWCOUNT
				SET @TicRowInd =1
				WHILE (@TicRowCot>0)
				BEGIN
					SELECT @Tickid = Element FROM @tblMTicket WHERE Id = @TicRowInd
					SELECT @TQuantity = Qty_Available FROM Ticket WHERE T_Id = @Tickid
					select @Tpurchasedqty=isnull(sum(TPD_Purchased_Qty),0) from [dbo].[Ticket_Purchased_Detail] a join [Ticket_Quantity_Detail] b
                           on a.TPD_TQD_Id=b.TQD_Id  where TQD_Ticket_Id =@Tickid
				  set @Tremaining=@TQuantity-isnull(@Tpurchasedqty,0)

					
					
					
					INSERT Ticket_Quantity_Detail ([TQD_PE_Id],[TQD_Event_Id],[TQD_Ticket_Id],[TQD_AddressId],[TQD_Quantity],[TQD_Remaining_Quantity],TQD_StartDate,TQD_StartTime)
					VALUES(@PublishId,@EventId,@Tickid,0,@TQuantity,@Tremaining,@StartDate,@StartTime)
					SET @TicRowInd = @TicRowInd +1
					SET @TicRowCot = @TicRowCot -1
				END

		END
		SET @RowIndex = @RowIndex+1
		SET @RowCount = @RowCount-1
	END

	SELECT * FROM Ticket_Quantity_Detail WHERE TQD_Event_Id = @EventId

	--truncate table Ticket_Quantity_Detail truncate table Publish_Event_Detail
	--
END












