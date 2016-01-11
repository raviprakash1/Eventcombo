
ALTER TABLE [dbo].[TicketDeliveryMethod] DROP CONSTRAINT [FK__TicketDel__Deliv__30242045]
GO
ALTER TABLE [dbo].[Ticket_Quantity_Detail] DROP CONSTRAINT [FK_Ticket_Quantity_Info_Ticket]
GO
ALTER TABLE [dbo].[Ticket] DROP CONSTRAINT [FK_Ticket_Event]
GO
ALTER TABLE [dbo].[Profile] DROP CONSTRAINT [FK_Profile_Profile]
GO
ALTER TABLE [dbo].[MultipleEvent] DROP CONSTRAINT [FK_MultipleEvent_Event]
GO
ALTER TABLE [dbo].[EventVenue] DROP CONSTRAINT [FK_EventVenue_EventVenue]
GO
ALTER TABLE [dbo].[EventVenue] DROP CONSTRAINT [FK_EventVenue_Event]
GO
ALTER TABLE [dbo].[EventSubCategory] DROP CONSTRAINT [FK__EventSubC__Event__297722B6]
GO
ALTER TABLE [dbo].[EventImage] DROP CONSTRAINT [FK_EventImage_Event]
GO
ALTER TABLE [dbo].[Event_VariableDesc] DROP CONSTRAINT [FK_Event_VariableDesc_Event]
GO
ALTER TABLE [dbo].[Event_Orgnizer_Detail] DROP CONSTRAINT [FK_Event_Orgnizer_Detail_Event]
GO
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Address] DROP CONSTRAINT [FK_Address_Event]
GO
ALTER TABLE [dbo].[Ticket_Locked_Detail] DROP CONSTRAINT [DF_Ticket_Locked_Detail_Locktime]
GO
/****** Object:  Table [dbo].[Venue]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[Venue]
GO
/****** Object:  Table [dbo].[User_Permission_Detail]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[User_Permission_Detail]
GO
/****** Object:  Table [dbo].[TimeZonesystem]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[TimeZonesystem]
GO
/****** Object:  Table [dbo].[TimeZoneDetail]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[TimeZoneDetail]
GO
/****** Object:  Table [dbo].[TicketType]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[TicketType]
GO
/****** Object:  Table [dbo].[TicketDeliveryMethod]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[TicketDeliveryMethod]
GO
/****** Object:  Table [dbo].[TicketBearer]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[TicketBearer]
GO
/****** Object:  Table [dbo].[Ticket_Quantity_Detail]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[Ticket_Quantity_Detail]
GO
/****** Object:  Table [dbo].[Ticket_Purchased_Detail]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[Ticket_Purchased_Detail]
GO
/****** Object:  Table [dbo].[Ticket_Locked_Detail]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[Ticket_Locked_Detail]
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[Ticket]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[Status]
GO
/****** Object:  Table [dbo].[ShippingAddress]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[ShippingAddress]
GO
/****** Object:  Table [dbo].[Publish_Event_Detail]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[Publish_Event_Detail]
GO
/****** Object:  Table [dbo].[Profile]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[Profile]
GO
/****** Object:  Table [dbo].[Permission_Detail]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[Permission_Detail]
GO
/****** Object:  Table [dbo].[Order_Detail_T]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[Order_Detail_T]
GO
/****** Object:  Table [dbo].[MultipleEvent]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[MultipleEvent]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[Messages]
GO
/****** Object:  Table [dbo].[Fee_Structure]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[Fee_Structure]
GO
/****** Object:  Table [dbo].[EventVote]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[EventVote]
GO
/****** Object:  Table [dbo].[EventVenue]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[EventVenue]
GO
/****** Object:  Table [dbo].[EventType]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[EventType]
GO
/****** Object:  Table [dbo].[EventSubCategory]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[EventSubCategory]
GO
/****** Object:  Table [dbo].[EventImage]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[EventImage]
GO
/****** Object:  Table [dbo].[EventFavourite]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[EventFavourite]
GO
/****** Object:  Table [dbo].[EventCategory]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[EventCategory]
GO
/****** Object:  Table [dbo].[Event_VariableDesc]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[Event_VariableDesc]
GO
/****** Object:  Table [dbo].[Event_Orgnizer_Detail]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[Event_Orgnizer_Detail]
GO
/****** Object:  Table [dbo].[Event_OrganizerMessages]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[Event_OrganizerMessages]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[Event]
GO
/****** Object:  Table [dbo].[Email_Template]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[Email_Template]
GO
/****** Object:  Table [dbo].[Email_Tag]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[Email_Tag]
GO
/****** Object:  Table [dbo].[DeliveryMethod]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[DeliveryMethod]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[Country]
GO
/****** Object:  Table [dbo].[CardDetails]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[CardDetails]
GO
/****** Object:  Table [dbo].[BillingAddress]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[BillingAddress]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[AspNetUsers]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[AspNetUserRoles]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[AspNetUserLogins]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[AspNetUserClaims]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[AspNetRoles]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[Address]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP TABLE [dbo].[__MigrationHistory]
GO
/****** Object:  UserDefinedFunction [dbo].[ReturnDayOfWeek]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP FUNCTION [dbo].[ReturnDayOfWeek]
GO
/****** Object:  UserDefinedFunction [dbo].[func_Split]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP FUNCTION [dbo].[func_Split]
GO
/****** Object:  StoredProcedure [dbo].[PublishEvent]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP PROCEDURE [dbo].[PublishEvent]
GO
/****** Object:  StoredProcedure [dbo].[GetTicketListing]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP PROCEDURE [dbo].[GetTicketListing]
GO
/****** Object:  StoredProcedure [dbo].[GetSetUserRole]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP PROCEDURE [dbo].[GetSetUserRole]
GO
/****** Object:  StoredProcedure [dbo].[GetSelectedTicketListing]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP PROCEDURE [dbo].[GetSelectedTicketListing]
GO
/****** Object:  StoredProcedure [dbo].[GetEventsListByStatus]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP PROCEDURE [dbo].[GetEventsListByStatus]
GO
/****** Object:  StoredProcedure [dbo].[GetEventListing]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP PROCEDURE [dbo].[GetEventListing]
GO
/****** Object:  StoredProcedure [dbo].[GetEventDateList]    Script Date: 1/11/2016 6:01:02 PM ******/
DROP PROCEDURE [dbo].[GetEventDateList]
GO
/****** Object:  StoredProcedure [dbo].[GetEventDateList]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--[GetEventDateList] 294
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetEventDateList]
	@EventId bigint
AS
BEGIN
	DECLARE @StartDate varchar(50)
	DECLARE @EndDate varchar(50)
	DECLARE @StartTime varchar(50)
	DECLARE @EndTime varchar(50)
	Declare @Dayofweek varchar(50)
    DECLARE @Freq VARCHAR(50)
	DECLARE @Singleevent int
	SET @Singleevent =0
    DECLARE @Days INT;
	DECLARE @cnt INT = 0;
    declare @DateFrom Date
    declare @DateTo Date

	SELECT @Singleevent=count(*) from EventVenue WHERE EventID = @EventId
	print @Singleevent 
	DECLARE @tblDates TABLE (Dayofweek varchar(50),startdate date,Time varchar(50),type varchar(50))


	if(@Singleevent>0)
	BEGIN

		SELECT @StartDate = EventStartDate ,@StartTime=EventStartTime
		FROM EventVenue WHERE EventID = @EventId
		SELECT  @Dayofweek=datename(dw,@StartDate)
	
	INSERT INTO @tblDates VALUES
		(@Dayofweek,@StartDate,@StartTime,'Single')	
	End
	else 
	begin
	select @Freq=Frequency from MultipleEvent  WHERE EventID = @EventId
	print @Freq
	If (@Freq = 'Daily')
	begin

		SELECT @Days = DateDiff(dd,StartingFrom ,StartingTo),
			@StartDate = StartingFrom,@StartTime=StartTime
			FROM MultipleEvent WHERE EventID = @EventId
			Set @DateFrom = convert(date,@StartDate)
               Set @DateTo = convert(date,@EndDate)
			WHILE @cnt <= @Days
            BEGIN
             SELECT  @Dayofweek=datename(dw,@DateFrom)
           INSERT INTO @tblDates VALUES
				(@Dayofweek,@DateFrom,@StartTime,'Daily')
						
				SET @DateFrom = DATEADD(dd,1,@DateFrom) 
				

				set @cnt=@cnt+1;
              END
		
	end
	Else If (@Freq = 'Weekly')
      Begin
	 DECLARE @Weekly varchar(50)
	    Declare @WeekDay int
	 Declare @DayID int
	  
	   SELECT @Weekly = WeeklyDay FROM MultipleEvent WHERE EventID = @EventId
	   	SELECT @Days = DateDiff(dd,StartingFrom ,StartingTo),@StartDate = StartingFrom,@StartTime=StartTime,@EndDate=StartingTo
			FROM MultipleEvent WHERE EventID = @EventId
			   Set @DateFrom = convert(date,@StartDate)
               Set @DateTo = convert(date,@EndDate)
			 Declare @tblWeek Table(Id INT,DayId INT) 
               insert into @tblWeek 
               select * From [dbo].func_Split(@Weekly,',') 
		  WHILE (@DateFrom<=@DateTo)
            
            BEGIN
			 SET @WeekDay = dbo.ReturnDayOfWeek(@DateFrom) 
			 
			 Select @DayID = count(*) from @tblWeek where DayId= @WeekDay

			 if (@DayID >0)
			 begin
			  SELECT  @Dayofweek=datename(dw,@DateFrom)
           INSERT INTO @tblDates VALUES
				(@Dayofweek,@DateFrom,@StartTime,'weekly')
	
				 
			 end
			SET @DateFrom = DATEADD(dd,1,@DateFrom) 
			
			end

   End
   Else If(@Freq = 'Monthly')
   Begin
   Declare @Monthlyday varchar(50)
     Declare @Monthlyweek varchar(50)
	   Declare @MonthlyweekDays varchar(50)
	
	 Declare @day varchar(50)
   	SELECT @Days = DateDiff(dd,StartingFrom ,StartingTo),
			@StartDate = StartingFrom,@StartTime=StartTime,@Monthlyday=MonthlyDay,@Monthlyweek=MonthlyWeek,
			@MonthlyweekDays=MonthlyWeekDays,@EndDate=StartingTo
			FROM MultipleEvent WHERE EventID = @EventId
			
 Set @DateFrom = convert(date,@StartDate)
 
               Set @DateTo = convert(date,@EndDate)
			   
if(isnull(@Monthlyday,'')!='')
begin 

 WHILE (@DateFrom<=@DateTo)
 begin 


set @day=DATEPART(dd,@DateFrom) 

if(@day=@Monthlyday)
begin 
  SELECT  @Dayofweek=datename(dw,@DateFrom)
     INSERT INTO @tblDates VALUES
				(@Dayofweek,@DateFrom,@StartTime,'Monthly')
	
end
SET @DateFrom = DATEADD(dd,1,@DateFrom) 
 end
end
if( isnull(@Monthlyweek,'')!='')
begin

Declare @monthday as int

SELECT @monthday = CASE @Monthlyweek WHEN 'First' THEN 1 WHEN 'Second' THEN 2 WHEN 'Third' THEN 3 WHEN 'Four' THEN 4 END

Declare @Noofweek varchar(50)
set @cnt=1
SELECT @Noofweek= DATEDIFF(week, @DateFrom, @DateTo)

WHILE @cnt <= @Noofweek
begin
print  convert(varchar,convert(varchar,@Noofweek)+'g'+convert(varchar,@monthday)+'g'+convert(varchar,@cnt))
if(@cnt=@monthday)
begin

Declare @newstartdate as date
Declare @newenddate as date
set @newstartdate=@StartDate
print @newstartdate
set @newenddate=DATEADD(dd,7,@newstartdate) 
               insert into @tblWeek 
               select * From [dbo].func_Split(@MonthlyweekDays,',') 


			     WHILE (@newstartdate<=@newenddate)
            
            BEGIN
			 SET @WeekDay = dbo.ReturnDayOfWeek(@newstartdate) 
			
			 Select @DayID = count(*) from @tblWeek where DayId= @WeekDay
			
			 if (@DayID >0)
			 begin
			  SELECT  @Dayofweek=datename(dw,@newstartdate)
           INSERT INTO @tblDates VALUES
				(@Dayofweek,@newstartdate,@StartTime,'Monthly')
	
				 
			 end
			SET @newstartdate = DATEADD(dd,1,@newstartdate) 
			
			end

end
SET @StartDate = DATEADD(dd,7,@StartDate) 
set @cnt=@cnt+1
 end
end
   End
	end

select Dayofweek ,convert(varchar(30),startdate,107) as Datefrom ,Time ,type from @tblDates
END



GO
/****** Object:  StoredProcedure [dbo].[GetEventListing]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--[GetEventListing] 

CREATE PROC [dbo].[GetEventListing]
(
	@EventTitle varchar(1200) =null,
	@EventType varchar(50) = null,
	@EventCat varchar(50) = null,
	@EventSubCat varchar(50) = null,
	@EventFeature varchar(10) = null,
	@EventStartdate varchar(10)=null,
	@EventTicket varchar(10)=null

)
AS 
BEGIN
	Declare @EventT int
	Declare @EventC int
	Declare @EventSC int
	Declare @EventF int
	Declare @strsql varchar(max)
	--if (ISNULL(@EventTitle,'') = null)
	--	SET @EventTitle =''
	
	--if (ISNULL(@EventType,'') = null)
	--	SET @EventT =0

	--if (ISNULL(@EventCat,'') = null)
	--	SET @EventC =0

	--if (ISNULL(@EventSubCat,'') = null)
	--	SET @EventSC =0
	
	--if (ISNULL(@EventFeature,'') = null)
	--	SET @EventF =0

	--@EventCat varchar(50) = null,
	--@EventSubCat varchar(50) = null,
	--@EventFeature varchar(10) = null
	if(@EventStartdate=0 and @EventTicket=2)
	begin
	select  EV.EventID as EventID , EventTitle,ET.EventType AS EventType,EC.EventCategory as EventCategory , ESC.EventSubCategory as EventSubCategory ,
	(CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		( convert(varchar, convert(Date,EVen.EventStartDate),106) +' ' +  convert(varchar,convert(time,EVen.EventStartTime),100)  + '-' +
		convert(varchar, convert(Date,EVen.EventEndDate),106) + ' ' + convert(varchar,convert(time,EVen.EventEndTime),100)) 
	ELSE
		( convert(varchar,convert(Date,ME.StartingFrom) ,106) + ' ' +  convert(varchar,ME.StartTime)  + '-' +
		CONVERT(VARCHAR,convert(Date,Me.StartingTo),106 ) + '-' + CONVERT(VARCHAR,Me.EndTime)) 
	END ) as EventTiming ,ME.StartingFrom as	StartingFrom ,ISNULL(Feature,0) as Feature,
(select top 1 Eod.Orgnizer_Name from  Event_Orgnizer_Detail EOD where  EOD.Orgnizer_Event_Id=Ev.EventID and EOD.DefaultOrg='Y' ) 
as  Orgnizer_Name,
(select top 1 AD.ConsolidateAddress from Address AD where  AD.EventId= EV.EventID ) as EventAddress,
(select top 1 AD.VenueName from Address AD where  AD.EventId= EV.EventID ) as Venuename,
anr.Email,
(SELECT    Category =
      CASE sum(TQD_Remaining_Quantity)
         WHEN 0 THEN 'Yes'
       
         ELSE 'No'
      END
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) as TicketDetail,
isnull((Select sum(TPD_Purchased_Qty) from Ticket_Purchased_Detail  where TPD_Event_Id=Ev.EventID),0) as Purchasedqty
 from ((((((Event Ev LEFT JOIN EventType ET
	ON Ev.EventTypeID = Et.EventTypeID ) LEFT JOIN EventCategory EC on EV.EventCategoryID =  EC.EventCategoryID)
	LEFT JOIN  EventSubCategory ESC on EV.EventSubCategoryID = ESC.EventSubCategoryID)
	LEFT JOIN EventVenue EVen
	ON Ev.EventID = EVen.EventID)
	LEFT JOIN MultipleEvent ME on EV.EventID = ME.EventID)
	Left join AspNetUsers ANR on Ev.UserID=ANR.Id )
	WHERE Ev.EventTitle like '%' + Coalesce(@EventTitle,Ev.EventTitle) + '%'
	AND isnull(Ev.EventTypeID,'')  = CASE WHEN @EventType >0 Then @EventType Else isnull(Ev.EventTypeID,'') END
	AND isnull(Ev.EventCategoryID,0)  = CASE WHEN @EventCat >0 Then @EventCat Else isnull(Ev.EventCategoryID,0) END
	AND isnull(Ev.EventSubCategoryID,0)  = CASE WHEN @EventSubCat >0 Then @EventSubCat  Else isnull(Ev.EventSubCategoryID,0) END
	AND ISNULL(Ev.Feature,0) = CASE WHEN @EventFeature >0 Then @EventFeature  Else ISNULL(Ev.Feature,0) END 
	AND (SELECT    Category =
      CASE sum(TQD_Remaining_Quantity)
         WHEN 0 THEN 'Yes'
       
         ELSE 'No'
      END
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) ='Yes' 

	end
	if(@EventStartdate=0 and @EventTicket=1)
	begin
	select  EV.EventID as EventID , EventTitle,ET.EventType AS EventType,EC.EventCategory as EventCategory , ESC.EventSubCategory as EventSubCategory ,
	(CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		( convert(varchar, convert(Date,EVen.EventStartDate),106) +' ' +  convert(varchar,convert(time,EVen.EventStartTime),100)  + '-' +
		convert(varchar, convert(Date,EVen.EventEndDate),106) + ' ' + convert(varchar,convert(time,EVen.EventEndTime),100)) 
	ELSE
		( convert(varchar,convert(Date,ME.StartingFrom) ,106) + ' ' +  convert(varchar,ME.StartTime)  + '-' +
		CONVERT(VARCHAR,convert(Date,Me.StartingTo),106 ) + '-' + CONVERT(VARCHAR,Me.EndTime)) 
	END ) as EventTiming ,ME.StartingFrom as	StartingFrom ,ISNULL(Feature,0) as Feature,
(select top 1 Eod.Orgnizer_Name from  Event_Orgnizer_Detail EOD where  EOD.Orgnizer_Event_Id=Ev.EventID and EOD.DefaultOrg='Y' ) 
as  Orgnizer_Name,
(select top 1 AD.ConsolidateAddress from Address AD where  AD.EventId= EV.EventID ) as EventAddress,
(select top 1 AD.VenueName from Address AD where  AD.EventId= EV.EventID ) as Venuename,
anr.Email,
(SELECT    Category =
      CASE sum(TQD_Remaining_Quantity)
         WHEN 0 THEN 'Yes'
       
         ELSE 'No'
      END
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) as TicketDetail,
isnull((Select sum(TPD_Purchased_Qty) from Ticket_Purchased_Detail  where TPD_Event_Id=Ev.EventID),0) as Purchasedqty
 from ((((((Event Ev LEFT JOIN EventType ET
	ON Ev.EventTypeID = Et.EventTypeID ) LEFT JOIN EventCategory EC on EV.EventCategoryID =  EC.EventCategoryID)
	LEFT JOIN  EventSubCategory ESC on EV.EventSubCategoryID = ESC.EventSubCategoryID)
	LEFT JOIN EventVenue EVen
	ON Ev.EventID = EVen.EventID)
	LEFT JOIN MultipleEvent ME on EV.EventID = ME.EventID)
	Left join AspNetUsers ANR on Ev.UserID=ANR.Id )
	WHERE Ev.EventTitle like '%' + Coalesce(@EventTitle,Ev.EventTitle) + '%'
	AND isnull(Ev.EventTypeID,'')  = CASE WHEN @EventType >0 Then @EventType Else isnull(Ev.EventTypeID,'') END
	AND isnull(Ev.EventCategoryID,0)  = CASE WHEN @EventCat >0 Then @EventCat Else isnull(Ev.EventCategoryID,0) END
	AND isnull(Ev.EventSubCategoryID,0)  = CASE WHEN @EventSubCat >0 Then @EventSubCat  Else isnull(Ev.EventSubCategoryID,0) END
	AND ISNULL(Ev.Feature,0) = CASE WHEN @EventFeature >0 Then @EventFeature  Else ISNULL(Ev.Feature,0) END 
	AND (SELECT    Category =
      CASE sum(TQD_Remaining_Quantity)
         WHEN 0 THEN 'Yes'
       
         ELSE 'No'
      END
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) ='No' 
	end
	if(@EventStartdate=0 and @EventTicket=0)
	begin
	select  EV.EventID as EventID , EventTitle,ET.EventType AS EventType,EC.EventCategory as EventCategory , ESC.EventSubCategory as EventSubCategory ,
	(CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		( convert(varchar, convert(Date,EVen.EventStartDate),106) +' ' +  convert(varchar,convert(time,EVen.EventStartTime),100)  + '-' +
		convert(varchar, convert(Date,EVen.EventEndDate),106) + ' ' + convert(varchar,convert(time,EVen.EventEndTime),100)) 
	ELSE
		( convert(varchar,convert(Date,ME.StartingFrom) ,106) + ' ' +  convert(varchar,ME.StartTime)  + '-' +
		CONVERT(VARCHAR,convert(Date,Me.StartingTo),106 ) + '-' + CONVERT(VARCHAR,Me.EndTime)) 
	END ) as EventTiming ,ME.StartingFrom as	StartingFrom ,ISNULL(Feature,0) as Feature,
(select top 1 Eod.Orgnizer_Name from  Event_Orgnizer_Detail EOD where  EOD.Orgnizer_Event_Id=Ev.EventID and EOD.DefaultOrg='Y' ) 
as  Orgnizer_Name,
(select top 1 AD.ConsolidateAddress from Address AD where  AD.EventId= EV.EventID ) as EventAddress,
(select top 1 AD.VenueName from Address AD where  AD.EventId= EV.EventID ) as Venuename,
anr.Email,
(SELECT    Category =
      CASE sum(TQD_Remaining_Quantity)
         WHEN 0 THEN 'Yes'
       
         ELSE 'No'
      END
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) as TicketDetail,
isnull((Select sum(TPD_Purchased_Qty) from Ticket_Purchased_Detail  where TPD_Event_Id=Ev.EventID),0) as Purchasedqty
 from ((((((Event Ev LEFT JOIN EventType ET
	ON Ev.EventTypeID = Et.EventTypeID ) LEFT JOIN EventCategory EC on EV.EventCategoryID =  EC.EventCategoryID)
	LEFT JOIN  EventSubCategory ESC on EV.EventSubCategoryID = ESC.EventSubCategoryID)
	LEFT JOIN EventVenue EVen
	ON Ev.EventID = EVen.EventID)
	LEFT JOIN MultipleEvent ME on EV.EventID = ME.EventID)
	Left join AspNetUsers ANR on Ev.UserID=ANR.Id )
	WHERE Ev.EventTitle like '%' + Coalesce(@EventTitle,Ev.EventTitle) + '%'
	AND isnull(Ev.EventTypeID,'')  = CASE WHEN @EventType >0 Then @EventType Else isnull(Ev.EventTypeID,'') END
	AND isnull(Ev.EventCategoryID,0)  = CASE WHEN @EventCat >0 Then @EventCat Else isnull(Ev.EventCategoryID,0) END
	AND isnull(Ev.EventSubCategoryID,0)  = CASE WHEN @EventSubCat >0 Then @EventSubCat  Else isnull(Ev.EventSubCategoryID,0) END
	AND ISNULL(Ev.Feature,0) = CASE WHEN @EventFeature >0 Then @EventFeature  Else ISNULL(Ev.Feature,0) END 
	end
 if(@EventStartdate=1 and  @EventTicket=0 )
	begin 
	select  EV.EventID as EventID , EventTitle,ET.EventType AS EventType,EC.EventCategory as EventCategory , ESC.EventSubCategory as EventSubCategory ,
	(CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		( convert(varchar, convert(Date,EVen.EventStartDate),106) +' ' +  convert(varchar,convert(time,EVen.EventStartTime),100)  + '-' +
		convert(varchar, convert(Date,EVen.EventEndDate),106) + ' ' + convert(varchar,convert(time,EVen.EventEndTime),100)) 
	ELSE
		( convert(varchar,convert(Date,ME.StartingFrom) ,106) + ' ' +  convert(varchar,ME.StartTime)  + '-' +
		CONVERT(VARCHAR,convert(Date,Me.StartingTo),106 ) + '-' + CONVERT(VARCHAR,Me.EndTime)) 
	END ) as EventTiming ,ME.StartingFrom as	StartingFrom ,ISNULL(Feature,0) as Feature,
(select top 1 Eod.Orgnizer_Name from  Event_Orgnizer_Detail EOD where  EOD.Orgnizer_Event_Id=Ev.EventID and EOD.DefaultOrg='Y' ) 
as  Orgnizer_Name,
(select top 1 AD.ConsolidateAddress from Address AD where  AD.EventId= EV.EventID ) as EventAddress,
(select top 1 AD.VenueName from Address AD where  AD.EventId= EV.EventID ) as Venuename,
anr.Email,
(SELECT    Category =
      CASE sum(TQD_Remaining_Quantity)
         WHEN 0 THEN 'Yes'
       
         ELSE 'No'
      END
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) as TicketDetail,
isnull((Select sum(TPD_Purchased_Qty) from Ticket_Purchased_Detail  where TPD_Event_Id=Ev.EventID),0) as Purchasedqty
 from ((((((Event Ev LEFT JOIN EventType ET
	ON Ev.EventTypeID = Et.EventTypeID ) LEFT JOIN EventCategory EC on EV.EventCategoryID =  EC.EventCategoryID)
	LEFT JOIN  EventSubCategory ESC on EV.EventSubCategoryID = ESC.EventSubCategoryID)
	LEFT JOIN EventVenue EVen
	ON Ev.EventID = EVen.EventID)
	LEFT JOIN MultipleEvent ME on EV.EventID = ME.EventID)
	Left join AspNetUsers ANR on Ev.UserID=ANR.Id )
	WHERE Ev.EventTitle like '%' + Coalesce(@EventTitle,Ev.EventTitle) + '%'
	AND isnull(Ev.EventTypeID,'')  = CASE WHEN @EventType >0 Then @EventType Else isnull(Ev.EventTypeID,'') END
	AND isnull(Ev.EventCategoryID,0)  = CASE WHEN @EventCat >0 Then @EventCat Else isnull(Ev.EventCategoryID,0) END
	AND isnull(Ev.EventSubCategoryID,0)  = CASE WHEN @EventSubCat >0 Then @EventSubCat  Else isnull(Ev.EventSubCategoryID,0) END
	AND ISNULL(Ev.Feature,0) = CASE WHEN @EventFeature >0 Then @EventFeature  Else ISNULL(Ev.Feature,0) END 
	AND convert(date,Me.StartingFrom)>=GETDATE()
	end
 if(@EventStartdate=2 and @EventTicket=0)
	begin 
	select  EV.EventID as EventID , EventTitle,ET.EventType AS EventType,EC.EventCategory as EventCategory , ESC.EventSubCategory as EventSubCategory ,
	(CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		( convert(varchar, convert(Date,EVen.EventStartDate),106) +' ' +  convert(varchar,convert(time,EVen.EventStartTime),100)  + '-' +
		convert(varchar, convert(Date,EVen.EventEndDate),106) + ' ' + convert(varchar,convert(time,EVen.EventEndTime),100)) 
	ELSE
		( convert(varchar,convert(Date,ME.StartingFrom) ,106) + ' ' +  convert(varchar,ME.StartTime)  + '-' +
		CONVERT(VARCHAR,convert(Date,Me.StartingTo),106 ) + '-' + CONVERT(VARCHAR,Me.EndTime)) 
	END ) as EventTiming ,ME.StartingFrom as	StartingFrom ,ISNULL(Feature,0) as Feature,
(select top 1 Eod.Orgnizer_Name from  Event_Orgnizer_Detail EOD where  EOD.Orgnizer_Event_Id=Ev.EventID and EOD.DefaultOrg='Y' ) 
as  Orgnizer_Name,
(select top 1 AD.ConsolidateAddress from Address AD where  AD.EventId= EV.EventID ) as EventAddress,
(select top 1 AD.VenueName from Address AD where  AD.EventId= EV.EventID ) as Venuename,
anr.Email,
(SELECT    Category =
      CASE sum(TQD_Remaining_Quantity)
         WHEN 0 THEN 'Yes'
       
         ELSE 'No'
      END
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) as TicketDetail,
isnull((Select sum(TPD_Purchased_Qty) from Ticket_Purchased_Detail  where TPD_Event_Id=Ev.EventID),0) as Purchasedqty
 from ((((((Event Ev LEFT JOIN EventType ET
	ON Ev.EventTypeID = Et.EventTypeID ) LEFT JOIN EventCategory EC on EV.EventCategoryID =  EC.EventCategoryID)
	LEFT JOIN  EventSubCategory ESC on EV.EventSubCategoryID = ESC.EventSubCategoryID)
	LEFT JOIN EventVenue EVen
	ON Ev.EventID = EVen.EventID)
	LEFT JOIN MultipleEvent ME on EV.EventID = ME.EventID)
	Left join AspNetUsers ANR on Ev.UserID=ANR.Id )
	WHERE Ev.EventTitle like '%' + Coalesce(@EventTitle,Ev.EventTitle) + '%'
	AND isnull(Ev.EventTypeID,'')  = CASE WHEN @EventType >0 Then @EventType Else isnull(Ev.EventTypeID,'') END
	AND isnull(Ev.EventCategoryID,0)  = CASE WHEN @EventCat >0 Then @EventCat Else isnull(Ev.EventCategoryID,0) END
	AND isnull(Ev.EventSubCategoryID,0)  = CASE WHEN @EventSubCat >0 Then @EventSubCat  Else isnull(Ev.EventSubCategoryID,0) END
	AND ISNULL(Ev.Feature,0) = CASE WHEN @EventFeature >0 Then @EventFeature  Else ISNULL(Ev.Feature,0) END 
	AND convert(date,Me.StartingFrom)<GETDATE()
	end

if(@EventStartdate=1 and @EventTicket=1)
	begin
		select  EV.EventID as EventID , EventTitle,ET.EventType AS EventType,EC.EventCategory as EventCategory , ESC.EventSubCategory as EventSubCategory ,
	(CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		( convert(varchar, convert(Date,EVen.EventStartDate),106) +' ' +  convert(varchar,convert(time,EVen.EventStartTime),100)  + '-' +
		convert(varchar, convert(Date,EVen.EventEndDate),106) + ' ' + convert(varchar,convert(time,EVen.EventEndTime),100)) 
	ELSE
		( convert(varchar,convert(Date,ME.StartingFrom) ,106) + ' ' +  convert(varchar,ME.StartTime)  + '-' +
		CONVERT(VARCHAR,convert(Date,Me.StartingTo),106 ) + '-' + CONVERT(VARCHAR,Me.EndTime)) 
	END ) as EventTiming ,ME.StartingFrom as	StartingFrom ,ISNULL(Feature,0) as Feature,
(select top 1 Eod.Orgnizer_Name from  Event_Orgnizer_Detail EOD where  EOD.Orgnizer_Event_Id=Ev.EventID and EOD.DefaultOrg='Y' ) 
as  Orgnizer_Name,
(select top 1 AD.ConsolidateAddress from Address AD where  AD.EventId= EV.EventID ) as EventAddress,
(select top 1 AD.VenueName from Address AD where  AD.EventId= EV.EventID ) as Venuename,
anr.Email,
(SELECT    Category =
      CASE sum(TQD_Remaining_Quantity)
         WHEN 0 THEN 'Yes'
       
         ELSE 'No'
      END
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) as TicketDetail,
isnull((Select sum(TPD_Purchased_Qty) from Ticket_Purchased_Detail  where TPD_Event_Id=Ev.EventID),0) as Purchasedqty
 from ((((((Event Ev LEFT JOIN EventType ET
	ON Ev.EventTypeID = Et.EventTypeID ) LEFT JOIN EventCategory EC on EV.EventCategoryID =  EC.EventCategoryID)
	LEFT JOIN  EventSubCategory ESC on EV.EventSubCategoryID = ESC.EventSubCategoryID)
	LEFT JOIN EventVenue EVen
	ON Ev.EventID = EVen.EventID)
	LEFT JOIN MultipleEvent ME on EV.EventID = ME.EventID)
	Left join AspNetUsers ANR on Ev.UserID=ANR.Id )
	WHERE Ev.EventTitle like '%' + Coalesce(@EventTitle,Ev.EventTitle) + '%'
	AND isnull(Ev.EventTypeID,'')  = CASE WHEN @EventType >0 Then @EventType Else isnull(Ev.EventTypeID,'') END
	AND isnull(Ev.EventCategoryID,0)  = CASE WHEN @EventCat >0 Then @EventCat Else isnull(Ev.EventCategoryID,0) END
	AND isnull(Ev.EventSubCategoryID,0)  = CASE WHEN @EventSubCat >0 Then @EventSubCat  Else isnull(Ev.EventSubCategoryID,0) END
	AND ISNULL(Ev.Feature,0) = CASE WHEN @EventFeature >0 Then @EventFeature  Else ISNULL(Ev.Feature,0) END 
	AND convert(date,Me.StartingFrom)>=GETDATE()
	AND (SELECT    Category =
      CASE sum(TQD_Remaining_Quantity)
         WHEN 0 THEN 'Yes'
       
         ELSE 'No'
      END
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) ='No'
	end
 if(@EventStartdate=1 and @EventTicket=2)
	begin
		select  EV.EventID as EventID , EventTitle,ET.EventType AS EventType,EC.EventCategory as EventCategory , ESC.EventSubCategory as EventSubCategory ,
	(CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		( convert(varchar, convert(Date,EVen.EventStartDate),106) +' ' +  convert(varchar,convert(time,EVen.EventStartTime),100)  + '-' +
		convert(varchar, convert(Date,EVen.EventEndDate),106) + ' ' + convert(varchar,convert(time,EVen.EventEndTime),100)) 
	ELSE
		( convert(varchar,convert(Date,ME.StartingFrom) ,106) + ' ' +  convert(varchar,ME.StartTime)  + '-' +
		CONVERT(VARCHAR,convert(Date,Me.StartingTo),106 ) + '-' + CONVERT(VARCHAR,Me.EndTime)) 
	END ) as EventTiming ,ME.StartingFrom as	StartingFrom ,ISNULL(Feature,0) as Feature,
(select top 1 Eod.Orgnizer_Name from  Event_Orgnizer_Detail EOD where  EOD.Orgnizer_Event_Id=Ev.EventID and EOD.DefaultOrg='Y' ) 
as  Orgnizer_Name,
(select top 1 AD.ConsolidateAddress from Address AD where  AD.EventId= EV.EventID ) as EventAddress,
(select top 1 AD.VenueName from Address AD where  AD.EventId= EV.EventID ) as Venuename,
anr.Email,
(SELECT    Category =
      CASE sum(TQD_Remaining_Quantity)
         WHEN 0 THEN 'Yes'
       
         ELSE 'No'
      END
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) as TicketDetail,
isnull((Select sum(TPD_Purchased_Qty) from Ticket_Purchased_Detail  where TPD_Event_Id=Ev.EventID),0) as Purchasedqty
 from ((((((Event Ev LEFT JOIN EventType ET
	ON Ev.EventTypeID = Et.EventTypeID ) LEFT JOIN EventCategory EC on EV.EventCategoryID =  EC.EventCategoryID)
	LEFT JOIN  EventSubCategory ESC on EV.EventSubCategoryID = ESC.EventSubCategoryID)
	LEFT JOIN EventVenue EVen
	ON Ev.EventID = EVen.EventID)
	LEFT JOIN MultipleEvent ME on EV.EventID = ME.EventID)
	Left join AspNetUsers ANR on Ev.UserID=ANR.Id )
	WHERE Ev.EventTitle like '%' + Coalesce(@EventTitle,Ev.EventTitle) + '%'
	AND isnull(Ev.EventTypeID,'')  = CASE WHEN @EventType >0 Then @EventType Else isnull(Ev.EventTypeID,'') END
	AND isnull(Ev.EventCategoryID,0)  = CASE WHEN @EventCat >0 Then @EventCat Else isnull(Ev.EventCategoryID,0) END
	AND isnull(Ev.EventSubCategoryID,0)  = CASE WHEN @EventSubCat >0 Then @EventSubCat  Else isnull(Ev.EventSubCategoryID,0) END
	AND ISNULL(Ev.Feature,0) = CASE WHEN @EventFeature >0 Then @EventFeature  Else ISNULL(Ev.Feature,0) END 
	AND convert(date,Me.StartingFrom)>=GETDATE()
	AND (SELECT    Category =
      CASE sum(TQD_Remaining_Quantity)
         WHEN 0 THEN 'Yes'
       
         ELSE 'No'
      END
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) ='Yes'
	end
  
   if(@EventStartdate=2 and @EventTicket=1)
	begin
		select  EV.EventID as EventID , EventTitle,ET.EventType AS EventType,EC.EventCategory as EventCategory , ESC.EventSubCategory as EventSubCategory ,
	(CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		( convert(varchar, convert(Date,EVen.EventStartDate),106) +' ' +  convert(varchar,convert(time,EVen.EventStartTime),100)  + '-' +
		convert(varchar, convert(Date,EVen.EventEndDate),106) + ' ' + convert(varchar,convert(time,EVen.EventEndTime),100)) 
	ELSE
		( convert(varchar,convert(Date,ME.StartingFrom) ,106) + ' ' +  convert(varchar,ME.StartTime)  + '-' +
		CONVERT(VARCHAR,convert(Date,Me.StartingTo),106 ) + '-' + CONVERT(VARCHAR,Me.EndTime)) 
	END ) as EventTiming ,ME.StartingFrom as	StartingFrom ,ISNULL(Feature,0) as Feature,
(select top 1 Eod.Orgnizer_Name from  Event_Orgnizer_Detail EOD where  EOD.Orgnizer_Event_Id=Ev.EventID and EOD.DefaultOrg='Y' ) 
as  Orgnizer_Name,
(select top 1 AD.ConsolidateAddress from Address AD where  AD.EventId= EV.EventID ) as EventAddress,
(select top 1 AD.VenueName from Address AD where  AD.EventId= EV.EventID ) as Venuename,
anr.Email,
(SELECT    Category =
      CASE sum(TQD_Remaining_Quantity)
         WHEN 0 THEN 'Yes'
       
         ELSE 'No'
      END
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) as TicketDetail,
isnull((Select sum(TPD_Purchased_Qty) from Ticket_Purchased_Detail  where TPD_Event_Id=Ev.EventID),0) as Purchasedqty
 from ((((((Event Ev LEFT JOIN EventType ET
	ON Ev.EventTypeID = Et.EventTypeID ) LEFT JOIN EventCategory EC on EV.EventCategoryID =  EC.EventCategoryID)
	LEFT JOIN  EventSubCategory ESC on EV.EventSubCategoryID = ESC.EventSubCategoryID)
	LEFT JOIN EventVenue EVen
	ON Ev.EventID = EVen.EventID)
	LEFT JOIN MultipleEvent ME on EV.EventID = ME.EventID)
	Left join AspNetUsers ANR on Ev.UserID=ANR.Id )
	WHERE Ev.EventTitle like '%' + Coalesce(@EventTitle,Ev.EventTitle) + '%'
	AND isnull(Ev.EventTypeID,'')  = CASE WHEN @EventType >0 Then @EventType Else isnull(Ev.EventTypeID,'') END
	AND isnull(Ev.EventCategoryID,0)  = CASE WHEN @EventCat >0 Then @EventCat Else isnull(Ev.EventCategoryID,0) END
	AND isnull(Ev.EventSubCategoryID,0)  = CASE WHEN @EventSubCat >0 Then @EventSubCat  Else isnull(Ev.EventSubCategoryID,0) END
	AND ISNULL(Ev.Feature,0) = CASE WHEN @EventFeature >0 Then @EventFeature  Else ISNULL(Ev.Feature,0) END 
	AND convert(date,Me.StartingFrom)<GETDATE()
	AND (SELECT    Category =
      CASE sum(TQD_Remaining_Quantity)
         WHEN 0 THEN 'Yes'
       
         ELSE 'No'
      END
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) ='No'
	end

 if(@EventStartdate=2 and @EventTicket=2)
	begin
		select  EV.EventID as EventID , EventTitle,ET.EventType AS EventType,EC.EventCategory as EventCategory , ESC.EventSubCategory as EventSubCategory ,
	(CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		( convert(varchar, convert(Date,EVen.EventStartDate),106) +' ' +  convert(varchar,convert(time,EVen.EventStartTime),100)  + '-' +
		convert(varchar, convert(Date,EVen.EventEndDate),106) + ' ' + convert(varchar,convert(time,EVen.EventEndTime),100)) 
	ELSE
		( convert(varchar,convert(Date,ME.StartingFrom) ,106) + ' ' +  convert(varchar,ME.StartTime)  + '-' +
		CONVERT(VARCHAR,convert(Date,Me.StartingTo),106 ) + '-' + CONVERT(VARCHAR,Me.EndTime)) 
	END ) as EventTiming ,ME.StartingFrom as	StartingFrom ,ISNULL(Feature,0) as Feature,
(select top 1 Eod.Orgnizer_Name from  Event_Orgnizer_Detail EOD where  EOD.Orgnizer_Event_Id=Ev.EventID and EOD.DefaultOrg='Y' ) 
as  Orgnizer_Name,
(select top 1 AD.ConsolidateAddress from Address AD where  AD.EventId= EV.EventID ) as EventAddress,
(select top 1 AD.VenueName from Address AD where  AD.EventId= EV.EventID ) as Venuename,
anr.Email,
(SELECT    Category =
      CASE sum(TQD_Remaining_Quantity)
         WHEN 0 THEN 'Yes'
       
         ELSE 'No'
      END
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) as TicketDetail,
isnull((Select sum(TPD_Purchased_Qty) from Ticket_Purchased_Detail  where TPD_Event_Id=Ev.EventID),0) as Purchasedqty
 from ((((((Event Ev LEFT JOIN EventType ET
	ON Ev.EventTypeID = Et.EventTypeID ) LEFT JOIN EventCategory EC on EV.EventCategoryID =  EC.EventCategoryID)
	LEFT JOIN  EventSubCategory ESC on EV.EventSubCategoryID = ESC.EventSubCategoryID)
	LEFT JOIN EventVenue EVen
	ON Ev.EventID = EVen.EventID)
	LEFT JOIN MultipleEvent ME on EV.EventID = ME.EventID)
	Left join AspNetUsers ANR on Ev.UserID=ANR.Id )
	WHERE Ev.EventTitle like '%' + Coalesce(@EventTitle,Ev.EventTitle) + '%'
	AND isnull(Ev.EventTypeID,'')  = CASE WHEN @EventType >0 Then @EventType Else isnull(Ev.EventTypeID,'') END
	AND isnull(Ev.EventCategoryID,0)  = CASE WHEN @EventCat >0 Then @EventCat Else isnull(Ev.EventCategoryID,0) END
	AND isnull(Ev.EventSubCategoryID,0)  = CASE WHEN @EventSubCat >0 Then @EventSubCat  Else isnull(Ev.EventSubCategoryID,0) END
	AND ISNULL(Ev.Feature,0) = CASE WHEN @EventFeature >0 Then @EventFeature  Else ISNULL(Ev.Feature,0) END 
	AND convert(date,Me.StartingFrom)<GETDATE()
	AND (SELECT    Category =
      CASE sum(TQD_Remaining_Quantity)
         WHEN 0 THEN 'Yes'
       
         ELSE 'No'
      END
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) ='Yes'
	end
  
	
END






GO
/****** Object:  StoredProcedure [dbo].[GetEventsListByStatus]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- GetEventsListByStatus '','LIVE','e583e85c-bf2f-43e4-9d56-5a20c73796be'
CREATE Procedure [dbo].[GetEventsListByStatus]

@EventTitle nvarchar(200),

@EventStatus varchar(20),
@UserID nvarchar(100)

as

BEGIN

IF UPPER(@EventStatus)='LIVE'
BEGIN
	With GrpEvent as (
		SELECT Max(EventId) as EV,Parent_EventID FROM EVENT WHERE ISNULL(Parent_EventID,0) >0 and UserID =@UserID GROUP BY Parent_EventID
		UNION 
		SELECT EventId as EV,Parent_EventID FROM EVENT WHERE ISNULL(Parent_EventID,0) <= 0 AND UserID =@UserID And EventID NOT IN 
		(SELECT Parent_EventID FROM EVENT)
	)

	Select * From 
	(SELECT Name,Email,Date_Time,TicketType,TicketPurchased,OrderId,EventID,EventTitle,EventDate,
	TotalTicket,TicketSold,EndTime as EventTime
	from (Select distinct  (p.FirstName+'  '+p.LastName) as Name,P.Email as EMail, a.UserID, a.EventID,
	'' as Date_Time,'' as TicketType,CONVERT(bigint, 0) as TicketPurchased ,'' as OrderId,LEFT(a.EventTitle,200) as EventTitle,
	isnull(b.StartingFrom,'')  as EventDate,
	isnull(b.StartingTo,'') as EventEnddate,
	(select ISNULL(SUM(TQD_Quantity),0) from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TotalTicket,
	(select ISNULL(SUM(TQD_Quantity)- SUM(TQD_Remaining_Quantity),0)   from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TicketSold,
	a.EventStatus,isnull(b.StartTime,'') as EndTime
	from event  a inner join MultipleEvent b on a.EventID =b.EventID 
	inner join Profile p on a.UserID=p.UserID 
	UNION 

	Select distinct (p.FirstName+'  '+p.LastName) as Name,P.Email as EMail, a.UserID, a.EventID,
	'' as Date_Time,'' as TicketType,CONVERT(bigint, 0) as TicketPurchased ,'' as OrderId, LEFT(a.EventTitle,200) as EventTitle,
	 isnull(b.EventStartDate,'')  as EventDate,isnull(b.EventEndDate,'') as EventEnddate,
	
	(select ISNULL(SUM(TQD_Quantity),0) from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TotalTicket,
	(select ISNULL(SUM(TQD_Quantity)- SUM(TQD_Remaining_Quantity),0)   from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TicketSold,
	a.EventStatus,
	isnull(b.EventStartTime,'') as EndTime
	 from event  a inner join EventVenue b on a.EventID =b.EventID 
	 inner join Profile p on a.UserID=p.UserID 

	UNION 
		 
	Select distinct (p.FirstName+'  '+p.LastName) as Name ,P.Email as EMail,a.UserID,a.EventID, 
	'' as Date_Time,'' as TicketType,CONVERT(bigint, 0) as TicketPurchased ,'' as OrderId,LEFT(a.EventTitle,200) as EventTitle,
		''  as EventDate,'' as EventEnddate,

	(select ISNULL(SUM(TQD_Quantity),0) from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TotalTicket,
	(select ISNULL(SUM(TQD_Quantity)- SUM(TQD_Remaining_Quantity),0)   from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TicketSold,
	a.EventStatus,
	'' as EndTime
	 from event  a  
	 inner join Profile p on a.UserID=p.UserID 
	  where 
	(select count(*) from EventVenue b where b.EventID=a.EventID )<=0
	and (select count(*) from MultipleEvent b where b.EventID=a.EventID )<=0    ) as xEven

	 where xEven.EventStatus='Live'  and convert(datetime,xEven.EventEnddate)>=GETDATE() and xEven.UserID=@UserID

	 and  EventTitle like '%'+ISNULL(@EventTitle,'')+'%' 
	 ) as LiveEventList where EventID in (Select Ev from GrpEvent) order by convert(datetime,EventDate) asc

	


END
ELSE IF UPPER(@EventStatus)='SAVE'

	
	select Name,Email,Date_Time,TicketType,TicketPurchased,OrderId,EventID,EventTitle,EventDate,
	TotalTicket,TicketSold,EndTime as EventTime
	 from  ( Select (p.FirstName+'  '+p.LastName) as Name,P.Email as EMail, a.UserID, a.EventID,
	'' as Date_Time,'' as TicketType,CONVERT(bigint, 0) as TicketPurchased ,'' as OrderId,LEFT(a.EventTitle,200) as EventTitle,
	 isnull(b.StartingFrom,'')  as EventDate,
 isnull(b.StartingTo,'') as EventEnddate,
(select ISNULL(SUM(TQD_Quantity),0) from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TotalTicket,
(select ISNULL(SUM(TQD_Quantity)- SUM(TQD_Remaining_Quantity),0)   from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TicketSold,
a.EventStatus,isnull(b.StartTime,'') as EndTime
 from event  a inner join MultipleEvent b on a.EventID =b.EventID 
 inner join Profile p on a.UserID=p.UserID 
	union 

	Select (p.FirstName+'  '+p.LastName) as Name,P.Email as EMail, a.UserID, a.EventID,
	'' as Date_Time,'' as TicketType,CONVERT(bigint, 0) as TicketPurchased ,'' as OrderId, LEFT(a.EventTitle,200) as EventTitle,
	 isnull(b.EventStartDate,'')  as EventDate,isnull(b.EventEndDate,'') as EventEnddate,
	
(select ISNULL(SUM(TQD_Quantity),0) from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TotalTicket,
(select ISNULL(SUM(TQD_Quantity)- SUM(TQD_Remaining_Quantity),0)   from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TicketSold,
a.EventStatus,
isnull(b.EventStartTime,'') as EndTime
 from event  a inner join EventVenue b on a.EventID =b.EventID 
 inner join Profile p on a.UserID=p.UserID 
	union 
		 
	 	Select  (p.FirstName+'  '+p.LastName) as Name ,P.Email as EMail,a.UserID,a.EventID, 
	'' as Date_Time,'' as TicketType,CONVERT(bigint, 0) as TicketPurchased ,'' as OrderId,LEFT(a.EventTitle,200) as EventTitle,
		''  as EventDate,'' as EventEnddate,

(select ISNULL(SUM(TQD_Quantity),0) from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TotalTicket,
(select ISNULL(SUM(TQD_Quantity)- SUM(TQD_Remaining_Quantity),0)   from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TicketSold,
a.EventStatus,
'' as EndTime
 from event  a  
 inner join Profile p on a.UserID=p.UserID 
  where 
(select count(*) from EventVenue b where b.EventID=a.EventID )<=0
	 and (select count(*) from MultipleEvent b where b.EventID=a.EventID )<=0    ) as xEven

	 where xEven.EventStatus='Save'  and convert(datetime,xEven.EventEnddate)>=GETDATE() and xEven.UserID=@UserID

	 and  EventTitle like '%'+ISNULL(@EventTitle,'')+'%' 
	 order by convert(datetime,xeven.EventDate) asc

	

ELSE IF UPPER(@EventStatus)='PAST'

	
	select Name,Email,Date_Time,TicketType,TicketPurchased,OrderId,EventID,EventTitle,EventDate,
	TotalTicket,TicketSold,EndTime as EventTime
	 from  ( Select (p.FirstName+'  '+p.LastName) as Name,P.Email as EMail, a.UserID, a.EventID,
	'' as Date_Time,'' as TicketType,CONVERT(bigint, 0) as TicketPurchased ,'' as OrderId,LEFT(a.EventTitle,200) as EventTitle,
	 isnull(b.StartingFrom,'')  as EventDate,
 isnull(b.StartingTo,'') as EventEnddate,
(select ISNULL(SUM(TQD_Quantity),0) from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TotalTicket,
(select ISNULL(SUM(TQD_Quantity)- SUM(TQD_Remaining_Quantity),0)   from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TicketSold,
a.EventStatus,isnull(b.StartTime,'') as EndTime
 from event  a inner join MultipleEvent b on a.EventID =b.EventID 
 inner join Profile p on a.UserID=p.UserID 
	union 

	Select (p.FirstName+'  '+p.LastName) as Name,P.Email as EMail, a.UserID, a.EventID,
	'' as Date_Time,'' as TicketType,CONVERT(bigint, 0) as TicketPurchased ,'' as OrderId, LEFT(a.EventTitle,200) as EventTitle,
	 isnull(b.EventStartDate,'')  as EventDate,isnull(b.EventEndDate,'') as EventEnddate,
	
(select ISNULL(SUM(TQD_Quantity),0) from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TotalTicket,
(select ISNULL(SUM(TQD_Quantity)- SUM(TQD_Remaining_Quantity),0)   from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TicketSold,
a.EventStatus,
isnull(b.EventStartTime,'') as EndTime
 from event  a inner join EventVenue b on a.EventID =b.EventID 
 inner join Profile p on a.UserID=p.UserID 
	union 
		 
	 	Select  (p.FirstName+'  '+p.LastName) as Name ,P.Email as EMail,a.UserID,a.EventID, 
	'' as Date_Time,'' as TicketType,CONVERT(bigint, 0) as TicketPurchased ,'' as OrderId,LEFT(a.EventTitle,200) as EventTitle,
		''  as EventDate,'' as EventEnddate,

(select ISNULL(SUM(TQD_Quantity),0) from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TotalTicket,
(select ISNULL(SUM(TQD_Quantity)- SUM(TQD_Remaining_Quantity),0)   from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TicketSold,
a.EventStatus,
'' as EndTime
 from event  a  
 inner join Profile p on a.UserID=p.UserID 
  where 
(select count(*) from EventVenue b where b.EventID=a.EventID )<=0
	 and (select count(*) from MultipleEvent b where b.EventID=a.EventID )<=0    ) as xEven

	 where convert(datetime,xEven.EventEnddate)<GETDATE() and xEven.UserID=@UserID

	 and  EventTitle like '%'+ISNULL(@EventTitle,'')+'%' 
	 order by convert(datetime,xeven.EventDate) asc

	


ELSE IF UPPER(@EventStatus)='GUEST'

	select (PR.FirstName+' '+PR.LastName) as Name,Pr.Email, LEFT(ev.EventTitle,200) as EventTitle,(evn.EventStartDate+' '+evn.EventStartTime) as Date_Time,

	tt.TicketType, SUM(TPD_Purchased_Qty) as TicketPurchased,TPD_Order_Id as OrderId,ev.EventID,'' as EventDate,'' as EventTime ,CONVERT(bigint, 0) as TotalTicket,CONVERT(bigint, 0) as TicketSold

	from Ticket_Purchased_Detail TPD inner join Profile Pr 

	on TPD.TPD_User_Id=Pr.UserID

	LEFT OUTER JOIN Event ev on TPD.TPD_Event_Id=ev.EventID

	LEFT OUTER JOIN EventVenue evn on TPD.TPD_Event_Id=evn.EventID

	LEFT OUTER JOIN Ticket_Quantity_Detail tqd on TPD.TPD_TQD_Id=tqd.TQD_Id

	LEFT OUTER JOIN Ticket t on tqd.TQD_Ticket_Id=t.T_Id

	LEFT OUTER JOIN TicketType tt on t.TicketTypeID=tt.TicketTypeID

	where ev.UserID=@UserID AND (pr.FirstName like '%'+ISNULL(@EventTitle,'') +'%' or ev.EventTitle like '%'+ISNULL(@EventTitle,'') +'%')

	group by TPD.TPD_User_Id,(PR.FirstName+' '+PR.LastName),Pr.Email,EventTitle,TPD_Order_Id,(evn.EventStartDate+' '+evn.EventStartTime),tt.TicketType,ev.EventID





END




GO
/****** Object:  StoredProcedure [dbo].[GetSelectedTicketListing]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO












--[GetSelectedTicketListing '8e55c866-ef64-4249-8a3e-b81679052fd6', 96] 

CREATE PROC [dbo].[GetSelectedTicketListing]
(
	@GUID varchar(1000),
	@EventId bigint
)
AS 
BEGIN

DECLARE @Html varchar(MAX) DECLARE @RowIndex int DECLARE @RowCount int Set @RowCount =0 Set @RowIndex =1
DECLARE @RInd int DECLARE @RCnt int Set @RCnt =0 Set @RInd =1

DECLARE @tblPublish TABLE (Id INT IDENTITY,P_Id BIGINT)
INSERT INTO @tblPublish SELECT PE_Id FROM Publish_Event_Detail WHERE PE_Event_Id =@EventId
AND PE_Id in (
SELECT TQD_PE_ID FROM Ticket_Quantity_Detail TQD INNER JOIN Ticket_Locked_Detail TLD ON TQD.TQD_Id = TLD.TLD_TQD_Id
Where TLD.TLD_GUID =@GUID)
SET @RCnt= @@ROWCOUNT

DECLARE @tblMain TABLE (Id INT IDENTITY,QTY_Id BIGINT)
DECLARE @PublishId bigint SET @PublishId=0
DECLARE @NextPublishId bigint SET @NextPublishId = 0
DECLARE @AddId bigint Set @AddId =0 DECLARE @NextAddId bigint  Set @NextAddId=0 
DECLARE @TicId bigint DECLARE @TicTypeId int
DECLARE @StartDate varchar(50)
DECLARE @Addess varchar(4000)
DECLARE @TQTY_Id bigint
DECLARE @QtyIds varchar(1000)
DECLARE @selectedQty bigint 
DECLARE @AddIdss varchar(500)
DECLARE @RunningTotal decimal(18,2) Set @RunningTotal=0
DECLARE @TQty Bigint DECLARE @Option Bigint DECLARE @RemQty Bigint DECLARE @TicketSaleEnd varchar(100) DECLARE @TicketDesc nvarchar(2000)
DECLARE @TotalPrice decimal(18,2) SET @TotalPrice =0 
DECLARE @IsShowDesc int SET @IsShowDesc =0 DECLARE @donation decimal
DECLARE @HideTicket int Set @HideTicket=0 
DECLARE @HideUntillDate DATETIME DECLARE @HideToDate DATETIME
DECLARE @HideUntillFromTime TIME DECLARE @HideUntillToTime TIME
DECLARE @Discount decimal(9,2) SET @Discount =0
SET @QtyIds =''


DECLARE @T_Name nvarchar(1000) DECLARE @T_Price DECIMAL(18,2) DECLARE @T_Fee decimal(18,2)
--table-striped evnt_tkt_list_tbl
SET @Html = '<table class="table table-striped evnt_tkt_list_tbl">'
--select * From Ticket_Quantity_Detail order by TQD_PE_Id
WHILE (@RCnt>0)
BEGIN
	SELECT @PublishId = P_Id FROM @tblPublish WHERE Id= @RInd
--	Select @PublishId
	SELECT @StartDate = (ISNULL(PE_Scheduled_Date,'') + ', ' + ISNULL(PE_Start_Time,'')), @AddIdss=isnull(PE_Address_Ids,'')
	FROM Publish_Event_Detail WHERE PE_Id = @PublishId
	--SET @Html = CONCAT(@Html,'<thead class="tkt_tbl_head">')
	
	

	SET @Html = CONCAT(@Html,'<thead class="tkt_multi_tab_date"><tr>', '<td class="book_tkt_date" colspan=4>',@StartDate, '' ,'</td></tr></thead>')
	if (RTRIM(LTRIM(@AddIdss)) ='')
	BEGIN
		SET @Html = CONCAT(@Html,'<thead class="tkt_tbl_head"> <tr>')
		SET @Html = CONCAT(@Html,'<td style="width:40%">Ticket Type</td>')
		--SET @Html = CONCAT(@Html,'<td>Sales End</td>')
		SET @Html = CONCAT(@Html,'<td align="right">', 'Price','</td>')
		SET @Html =	CONCAT(@Html,'<td align="right">Fee</td>')
		SET @Html = CONCAT(@Html,'<td align="right">Quantity</td>')
		--SET @Html = CONCAT(@Html,'<td>Price</td>')
		SET @Html = CONCAT(@Html,'</tr> </thead>')
	END
	INSERT INTO @tblMain 
	SELECT TQD_Id FROM Ticket_Quantity_Detail TQD  
	INNER JOIN Ticket_Locked_Detail TLD ON TQD.TQD_Id = TLD.TLD_TQD_Id 
	WHERE TQD_PE_Id=@PublishId AND TLD.TLD_GUID = @GUID
	SET @RowCount = @@ROWCOUNT

	WHILE (@RowCount>0)
	BEGIN
		SELECT @TQTY_Id = QTY_Id FROM @tblMain WHERE Id= @RowIndex
		SELECT @AddId = TQD_AddressId, @TicId = TQD_Ticket_Id 
		FROM Ticket_Quantity_Detail WHERE TQD_ID = @TQTY_Id
		--Select concat(@AddId,@NextAddId)
		IF (@QtyIds='') SET @QtyIds = CONCAT(@QtyIds,@TQTY_Id)  ELSE SET @QtyIds = CONCAT(@QtyIds,',',@TQTY_Id) 
		
		SELECT @T_Name = T_name,@T_Price=isnull(Price,0),@TotalPrice = isnull(TotalPrice,0), @TQty= Qty_Available, @TicketSaleEnd = CONCAT(CONVERT(VARCHAR,Sale_End_Date,107),Sale_End_Time)
		, @TicTypeId = TicketTypeID,@TicketDesc=T_Desc,@IsShowDesc = Show_T_Desc,@HideTicket = Hide_Ticket ,
		@HideUntillDate = Hide_untill_Date, @HideUntillFromTime = Hide_Untill_Time,@HideToDate= Hide_After_Date,@HideUntillToTime=Hide_After_Time,
		@Discount = ISNULL(T_Discount ,0)
		FROM Ticket WHERE T_id= @TicId



		IF (@NextAddId != @AddId)	
		BEGIN
			SET @NextAddId = @AddId 
			SELECT @Addess = ConsolidateAddress FROM ADDRESS WHERE AddressID = @AddId
			SET @Html = CONCAT(@Html,'<thead class="tkt_multi_tab_date"><tr valign="bottom">','<td valign="bottom" class="book_tkt_desc" colspan=4>',@Addess,'</td></tr></thead>')
			SET @Html = CONCAT(@Html,'<thead class="tkt_tbl_head"> <tr>')
			SET @Html = CONCAT(@Html,'<td style="width:40%">Ticket Type</td>')
			SET @Html = CONCAT(@Html,'<td align="right">', 'Price','</td>')
			SET @Html =	CONCAT(@Html,'<td align="right">Fee</td>')
			SET @Html = CONCAT(@Html,'<td align="right">Quantity</td>')
			SET @Html = CONCAT(@Html,'</tr> </thead>')
		END
		

		
		SELECT @RemQty = TQD_Remaining_Quantity FROM Ticket_Quantity_Detail WHERE TQD_Id = @TQTY_Id
		Set @donation=0
		SELECT @selectedQty=TLD_Locked_Qty,@donation = ISNULL(TLD_Donate,0) FROM Ticket_Locked_Detail 
		WHERE TLD_GUID = @GUID and TLD_TQD_Id = @TQTY_Id

		SET @T_Fee = (@TotalPrice - @T_Price)
		
		

		if (@HideTicket !=1)
		BEGIN

		if (@IsShowDesc = 1 AND isnull(@TicketDesc,'') != '')
		BEGIN
			SET @Html = CONCAT(@Html,'<tr><td style="width:40%">', @T_Name,'<br><span class="tkt_more_desc">  <i onclick="checkMore(',@TQTY_Id,')">[More Info]</i></span>', '<span style="display:none;" id="Sp_',@TQTY_Id,'"class="col-sm-12 no_pad">',@TicketDesc,'</span>' ,'</td>')
		END
		ELSE		
		BEGIN
			SET @Html = CONCAT(@Html,'<tr><td style="width:40%">', @T_Name,'<br>', '<span style="display:none;" id="Sp_',@TQTY_Id,'"class="col-sm-12 no_pad">',@TicketDesc,'</span>' ,'</td>')
		END

		

		--SET @T_Price = CASE WHEN @T_Price =  THEN 0 ELSE COnvert(decimal,@T_Price) END
		if (@donation>0) SET @Html = CONCAT(@Html,'<td align="right">$',@donation,'</td>')
		
		Else 
			BEGIN 
				if (@Discount>0)
				BEGIN
					SET @TotalPrice = (@T_Price  - @Discount) + @T_Fee 
					SET @Html = CONCAT(@Html,'<td align="right">','<strike>',@T_Price,'</strike> $',(@T_Price-@Discount),'</td>') 

				END
				ELSE
				BEGIN
					SET @Html = CONCAT(@Html,'<td align="right">$',@T_Price,'</td>') 
				END
			END

		SET @Html = CONCAT(@Html,'<td align="right">$',@T_Fee,'</td>')
		SET @Html = CONCAT(@Html,'<td align="right">',@selectedQty,'</td>')
		
		--SET @Html = CONCAT(@Html,'<td>$',(@selectedQty * @T_Price) ,'</td>')
		SET @RunningTotal = @RunningTotal  + (@selectedQty * @TotalPrice) + @donation

		--SET @Html = CONCAT(@Html,'<td>',@TicketSaleEnd,'</td>')
		
			--IF (@TicTypeId = 2)  
			--BEGIN
			--	--SET @Html = CONCAT(@Html,'<td>$',@T_Price,'</td>')
			--	--SET @Html = CONCAT(@Html,'<td>$',@T_Fee,'</td>')
			--END
			--ELSE IF (@TicTypeId = 1)  
			--BEGIN
			--	SET @Html = CONCAT(@Html,'<td>$','<span id="P_', @TQTY_Id, '">','0','</span>','</td>')
			--	SET @Html = CONCAT(@Html,'<td>$','0.00','</td>')
			--END
			--ELSE IF (@TicTypeId = 3)  
			--BEGIN
			--	SET @Html = CONCAT(@Html,'<td align="left" colspan="2">','$<input type="text"  onchange="calculateTickTotal()" placeholder="Donate" id="txtd_',@TQTY_Id,'" value="" />','</td>')
			--END
		
	--	SET @Html = CONCAT(@Html,'<td><select onchange="calculateTickTotal()" id=d_' ,CONVERT(VARCHAR,@TQTY_Id) ,'>', @TMaxQty,'</select></td>')
			SET @Html = CONCAT(@Html,'</tr>')
		END
		SET @RowCount = @RowCount  -1
		SET @RowIndex = @RowIndex  +1
	END

	--GetSelectedTicketListing 0

	

	SET @RCnt = @RCnt -1
	SET @RInd = @RInd +1
END


--declare @Html varchar(Max)
--SET @Html ='</table>'
--SET @Html = CONCAT(@Html,'First')
--SET @Html = CONCAT(@Html, 'Second')
--SET @Html += '<td>Satnam Waheguru</td>'
--SET @Html += '</tr>'
DECLARE @VariableChanges DECIMAL(18,2) SET @VariableChanges =0 DECLARE @VariableId bigint SET @VariableId =0
DECLARE @VariableName nvarchar(512) SET @VariableName =''
DECLARE @VariableDesc nvarchar(512) SET @VariableDesc =''
DECLARE @VariableType nvarchar(512) SET @VariableType =''

DECLARE @VariablePrice DECIMAL(18,2) SET @VariablePrice=0
DECLARE @GrandTotal DECIMAL(18,2) SET @GrandTotal =0
DECLARE @VarHTML nvarchar(MAX) SET @VarHTML = ''
DECLARE @TblVar TABLE (Id INT IDENTITY,vDec NVARCHAR(512),Price DECIMAL) 
DECLARE @RowCnt int  SET @RowCnt =0
Select  @VariableDesc =ISNULL(Ticket_variabledesc,'') ,@VariableType = LTRIM(RTRIM(ISNULL(Ticket_variabletype,''))) From Event where EventID = @EventId

INSERT INTO @TblVar select VariableDesc,Price From Event_VariableDesc WHERE Event_Id = @EventId  
SET @RowCount = @@ROWCOUNT
SET @RowIndex =1
SET @RowCnt = @RowCount
if (@RowCount>0)
BEGIN
	SET @VarHTML = CONCAT(@VarHTML,'<tr> <td colspan="4" style="padding:0px"><h2 style="background:#aaa;margin:0px;font-size:20px; padding:5px;color:#fff;">',@VariableDesc,'</h2></td></tr>')
END

WHILE (@RowCount>0)
BEGIN
	SELECT @VariableName = ISNULL(vDec,''),@VariablePrice = ISNULL(Price ,0) FROM @TblVar WHERE Id= @RowIndex
	if (@VariableType = 'R')
	BEGIN
		if (@RowIndex=1)
		BEGIN
			SET @VarHTML = CONCAT(@VarHTML,'<tr>')
			SET @VarHTML = CONCAT(@VarHTML,'<td class="tbl_white_bg" colspan="3" align="left">','<input type="radio" onclick="calculateTickTotal(',@VariablePrice,')" id="rd_',@RowCount,'" name="VChanges" checked="checked" /> ', @VariableName ,'</td>') 
			SET @VarHTML = CONCAT(@VarHTML,'<td class="tbl_white_bg" align="right"> $',@VariablePrice,'</td>') 
			SET @VarHTML = CONCAT(@VarHTML,'</tr>')
			SET @VariableChanges = @VariablePrice
		END
		ELSE
		BEGIN
			SET @VarHTML = CONCAT(@VarHTML,'<tr>')
			SET @VarHTML = CONCAT(@VarHTML,'<td class="tbl_white_bg" colspan="3" align="left">','<input type="radio" onclick="calculateTickTotal(',@VariablePrice,')" id="rd_',@RowCount,'" name="VChanges" /> ',@VariableName,'</td>') 
			SET @VarHTML = CONCAT(@VarHTML,'<td class="tbl_white_bg" align="right"> $',@VariablePrice,'</td>') 
			SET @VarHTML = CONCAT(@VarHTML,'</tr>')
		END
	END
	ELSE
	BEGIN
		BEGIN
			SET @VarHTML = CONCAT(@VarHTML,'<tr>')
			SET @VarHTML = CONCAT(@VarHTML,'<td class="tbl_white_bg" colspan="3" align="left">','<input type="checkbox" onclick="calculateTickTotalOptional(',@RowCnt,')" id="chk_',@RowCount,'" name="VChanges"/> ', @VariableName, '</td>') 
			SET @VarHTML = CONCAT(@VarHTML,'<td class="tbl_white_bg" align="right"> $','<span id="sp_',@RowCount,'">',@VariablePrice,'</span>','</td>') 
			SET @VarHTML = CONCAT(@VarHTML,'</tr>')
		END
	END
	

	--SELECT @VariableChanges = ISNULL(SUM(ISNULL(Price,0)),0) FROM Event_VariableDesc WHERE Event_Id = @EventId 

	SET @RowCount = @RowCount -1
	SET @RowIndex = @RowIndex +1
END


--SELECT @VariableChanges = ISNULL(SUM(ISNULL(Price,0)),0) FROM Event_VariableDesc WHERE Event_Id = @EventId 
SET @GrandTotal = @RunningTotal + @VariableChanges




SET @Html = CONCAT(@Html,'<tr> <td class="tbl_white_bg" colspan="4" align="right">','<b>','Order Total : $ ' , CONVERT(NUMERIC(18,2),@RunningTotal) ,'</b>','</td></tr>') 
SET @Html = CONCAT(@Html,@VarHTML) 
--SET @Html = CONCAT(@Html,'<tr> <td class="tbl_white_bg" colspan="5" align="right">','', 'Variable Charges $ ' , @VariableChanges ,'','</td></tr>') 
SET @Html = CONCAT(@Html,'<tr> <td class="tbl_white_bg" colspan="4" align="right">','<b>', 'Sub Total : $ ' ,'<span id="spSubTotal">' , CONVERT(NUMERIC(18,2),(@RunningTotal+@VariableChanges)) ,'</span></b>','</td></tr>') 
SET @Html = CONCAT(@Html,'<tr>','<td class="tbl_white_bg" colspan="4" align="right">', '<div class="col-sm-10">Promo Code</div>','<div class="col-sm-2 no_pad"><input type="text" class="form-control evnt_inp_cont"  placeholder="" id="txtPromoCode','" value="" />','</div></td></tr>') 
SET @Html = CONCAT(@Html,'<tr> <td class="tbl_white_bg" colspan="4" align="right">','<b>', 'Grand Total : $ ', '<span id="spGrdTotal">',CONVERT(NUMERIC(18,2),@GrandTotal) ,'</span></b>','</td></tr>') 

SET @Html = CONCAT(@Html,'</table>')
SET @Html = CONCAT(@Html,'<input id="hdIds" type="hidden" value=', @QtyIds ,' />')
SET @Html = CONCAT(@Html,'<input id="hdOrderTotal" type="hidden" value=', @RunningTotal ,' />')
SET @Html = CONCAT(@Html,'<input id="hdGrandTotal" type="hidden" value=', @GrandTotal ,' />')
SET @Html = CONCAT(@Html,'<input id="hdPromoId" type="hidden" value=', '0' ,' />')
SET @Html = CONCAT(@Html,'<input id="hdVarChanges" type="hidden" value=', @VariableChanges ,' />')
SET @Html = CONCAT(@Html,'<input id="hdVarId" type="hidden" value=', @VariableId ,' />')

Select @Html as Ticket


END























GO
/****** Object:  StoredProcedure [dbo].[GetSetUserRole]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[GetSetUserRole](
	@user_Id nvarchar(500),
	@GETSET varchar(3),
	@Role_Id varchar(50)
)
AS 
BEGIN
	Declare @RId varchar(50)
	SET @RId =''
	IF (UPPER(@GETSET) = 'GET')
	BEGIN
		SELECT @RID =RoleId FROM AspNetUserRoles WHERE UserId = @user_Id 
	END
	ELSE IF (UPPER(@GETSET) = 'SET')
	BEGIN
		SELECT @RID = Isnull(RoleId,'') FROM AspNetUserRoles WHERE USERID = @user_Id 
		IF (@RID ='')
		BEGIN
			DELETE from AspNetUserRoles where USERID = @user_Id
			INSERT INTO AspNetUserRoles (UserId,RoleId) VALUES(@user_Id,@Role_Id)
		END
		ELSE
			UPDATE AspNetUserRoles SET RoleId = @Role_Id WHERE UserId= @user_Id 
	END
	Select @RId

END



GO
/****** Object:  StoredProcedure [dbo].[GetTicketListing]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









--[GetTicketListing 303] 

CREATE PROC [dbo].[GetTicketListing]
(
	@EventId bigint
)
AS 
BEGIN

DECLARE @Html varchar(MAX) DECLARE @RowIndex int DECLARE @RowCount int Set @RowCount =0 Set @RowIndex =1
DECLARE @RInd int DECLARE @RCnt int Set @RCnt =0 Set @RInd =1

DECLARE @tblPublish TABLE (Id INT IDENTITY,P_Id BIGINT)
INSERT INTO @tblPublish SELECT PE_Id FROM Publish_Event_Detail WHERE PE_Event_Id =@EventId
SET @RCnt= @@ROWCOUNT

DECLARE @tblMain TABLE (Id INT IDENTITY,QTY_Id BIGINT)
DECLARE @PublishId bigint SET @PublishId=0
DECLARE @NextPublishId bigint SET @NextPublishId = 0
DECLARE @AddId bigint Set @AddId =0 DECLARE @NextAddId bigint  Set @NextAddId=0 
DECLARE @TicId bigint DECLARE @TicTypeId int
DECLARE @StartDate varchar(50)
DECLARE @Addess varchar(4000)
DECLARE @TQTY_Id bigint
DECLARE @QtyIds varchar(1000)
DECLARE @TMaxQty varchar(Max)
DECLARE @AddIdss varchar(500)
DECLARE @TQty Bigint DECLARE @Option Bigint DECLARE @RemQty Bigint DECLARE @TicketSaleEnd varchar(100) DECLARE @TicketDesc nvarchar(2000)
DECLARE @TotalPrice DECIMAL(18,2) SET @TotalPrice =0    
DECLARE @IsShowDesc int SET @IsShowDesc =0   DECLARE @MinQty bigint DECLARE @MaxQty bigint Set @MinQty=0 SET @MaxQty=0
DECLARE @iflag int Declare @SHowRemaingTick varchar(50) SET @SHowRemaingTick='' Declare @TRemainRuning bigint SET @TRemainRuning=0
DECLARE @HideTicket int Set @HideTicket=0
DECLARE @HideUntillDate varchar(20) DECLARE @HideToDate varchar(20)
DECLARE @HideUntillFromTime varchar(20) DECLARE @HideUntillToTime varchar(20)
DECLARE @Discount decimal(9,2) SET @Discount =0
DECLARE @SaleStartDate VARCHAR(20) SET @SaleStartDate = ''
DECLARE @SaleEndDate VARCHAR(20) SET @SaleEndDate = ''

DECLARE @SaleStartTime VARCHAR(20) SET @SaleStartTime = ''
DECLARE @SaleEndTime VARCHAR(20) SET @SaleEndTime = ''
DECLARE @FlagHide int 
DECLARE @TicktHideTypeSale int DECLARE @TickatMarkSoldOut int

SET @QtyIds =''


DECLARE @T_Name nvarchar(1000) DECLARE @T_Price decimal(18,2) DECLARE @T_Fee decimal(18,2)
--table-striped evnt_tkt_list_tbl
SELECT @SHowRemaingTick = Ticket_showremain from Event Where Eventid=@EventId

SET @Html = '<table class="table table-striped evnt_tkt_list_tbl">'
--select * From Ticket_Quantity_Detail order by TQD_PE_Id
WHILE (@RCnt>0)
BEGIN
	SELECT @PublishId = P_Id FROM @tblPublish WHERE Id= @RInd
--	Select @PublishId
	SELECT @StartDate = (ISNULL(PE_Scheduled_Date,'') + ', ' + ISNULL(PE_Start_Time,'')), @AddIdss=isnull(PE_Address_Ids,'')
	FROM Publish_Event_Detail WHERE PE_Id = @PublishId
	--SET @Html = CONCAT(@Html,'<thead class="tkt_tbl_head">')
	SET @Html = CONCAT(@Html,'<thead class="tkt_multi_tab_date"><tr>', '<td class="book_tkt_date" colspan=5>',@StartDate,'</td></tr></thead>')
	
	INSERT INTO @tblMain SELECT TQD_Id FROM Ticket_Quantity_Detail WHERE TQD_PE_Id =@PublishId
	SET @RowCount = @@ROWCOUNT
		IF (RTRIM(LTRIM(@AddIdss)) = '')	
		BEGIN
			SET @Html = CONCAT(@Html,'<thead class="tkt_tbl_head"> <tr>')
			SET @Html = CONCAT(@Html,'<td style="width:40%">Ticket Type</td>')
			SET @Html = CONCAT(@Html,'<td>Sales End</td>')
			SET @Html = CONCAT(@Html,'<td align="right">','Price','</td>')
			SET @Html =	CONCAT(@Html,'<td align="right">Fee</td>')
			SET @Html = CONCAT(@Html,'<td align="right">Quantity</td>')
			SET @Html = CONCAT(@Html,'</tr> </thead>')
		END
		


	WHILE (@RowCount>0)
	BEGIN
		SELECT @TQTY_Id = QTY_Id FROM @tblMain WHERE Id= @RowIndex
		SELECT @AddId = TQD_AddressId, @TicId = TQD_Ticket_Id 
		FROM Ticket_Quantity_Detail WHERE TQD_ID = @TQTY_Id
		--Select concat(@AddId,@NextAddId)
		IF (@QtyIds='') SET @QtyIds = CONCAT(@QtyIds,@TQTY_Id)  ELSE SET @QtyIds = CONCAT(@QtyIds,',',@TQTY_Id) 
		
		
		
		--@T_Fee =  CASE WHEN ISNULL(EC_Fee,0)>0 THEN EC_Fee ELSE ISNULL(Customer_Fee,0) END
		SELECT @T_Name = T_name,@T_Price=isnull(Price,0),@TotalPrice = isnull(TotalPrice,0)  ,@TQty= Qty_Available, @TicketSaleEnd = CONCAT(CONVERT(VARCHAR,Sale_End_Date,107),', ',Sale_End_Time)
		, @TicTypeId = TicketTypeID,@TicketDesc=T_Desc,@IsShowDesc = Show_T_Desc, @MinQty = isnull(Min_T_Qty,0),@MaxQty = isnull(Max_T_Qty,0),@HideTicket = Hide_Ticket,
		@HideUntillDate = isnull(Hide_untill_Date,''), @HideUntillFromTime = isnull(Hide_Untill_Time,''),@HideToDate= isnull(Hide_After_Date,''),@HideUntillToTime=isnull(Hide_After_Time,''),
		@SaleStartDate = ISNULL(Sale_Start_Date,''),@SaleEndDate= ISNULL(Sale_End_Date,''),@Discount = ISNULL(T_Discount ,0),
		@SaleStartTime = ISNULL(Sale_Start_Time,''),@SaleEndTime = ISNULL(Sale_End_Time ,''),
		@TicktHideTypeSale = T_AutoSechduleType ,@TickatMarkSoldOut =  ISNULL(T_Mark_SoldOut,0)
		FROM Ticket WHERE T_id= @TicId
		
		if (@SaleStartDate = '1900-01-01') SET @SaleStartDate=''
		if (@SaleEndDate = '1900-01-01') SET @SaleEndDate=''


		if (@SaleStartTime = '0') SET @SaleStartTime=''
		if (@SaleEndTime = '0') SET @SaleEndTime=''
		
		--select @SaleStartDate 
		--select @SaleEndDate
		--select @SaleStartTime
		--Select @SaleEndTime


		--SELECT @TSoldQty = (ISNULL(TQD.TQD_Remaining_Quantity,0) + isnull(TLD.TLD_Locked_Qty,0))  FROM 
		--Ticket_Quantity_Detail TQD LEFT JOIN Ticket_Locked_Detail TLD ON TQD.TQD_Id = TLD.TLD_TQD_Id
		--WHERE TQD_PE_Id = @PublishId and TQD_Event_Id = @EventId AND TQD_Ticket_Id = @TicId

		IF (@NextAddId != @AddId)	
		BEGIN
			If (@RowIndex>1 AND @SHowRemaingTick ='Y')
			BEGIN
				SET @Html = CONCAT(@Html,'<tr> <td class="tbl_white_bg" colspan="5" align="right">','<b>','Remaining Quantity : ' , @TRemainRuning ,'</b>','</td></tr>') 
				SET @TRemainRuning =0
			END
			SET @NextAddId = @AddId 
			SELECT @Addess = ConsolidateAddress FROM ADDRESS WHERE AddressID = @AddId
			
			--SET @Html = CONCAT(@Html, '<tr>','<td colspan=5 align="left"> ','','</td></tr>')
			SET @Html = CONCAT(@Html,'<thead class="tkt_multi_tab_date"><tr>','<td class="book_tkt_desc" colspan=5 > ',@Addess,'</td></tr></thead>')
			SET @Html = CONCAT(@Html,'<thead class="tkt_tbl_head"> <tr>')
			SET @Html = CONCAT(@Html,'<td style="width:40%">Ticket Type</td>')
			SET @Html = CONCAT(@Html,'<td>Sales End</td>')
			SET @Html = CONCAT(@Html,'<td align="right">', 'Price','</td>')
			SET @Html =	CONCAT(@Html,'<td align="right">Fee</td>')
			SET @Html = CONCAT(@Html,'<td align="right">Quantity</td>')
			SET @Html = CONCAT(@Html,'</tr> </thead>')
		END
		
		
		SELECT @RemQty = TQD_Remaining_Quantity 
		FROM Ticket_Quantity_Detail WHERE TQD_Id = @TQTY_Id

		SELECT @RemQty =  (@RemQty-isnull(sum(isnull(TLD_Locked_Qty,0)),0))
		FROM Ticket_Locked_Detail WHERE TLD_TQD_Id = @TQTY_Id
		SET @TRemainRuning = (@TRemainRuning + @RemQty)
		--GetTicketListing 96 

		--SELECT  @RemQty = (ISNULL(TQD.TQD_Remaining_Quantity,0) - isnull(TLD.TLD_Locked_Qty,0)) FROM 
		--Ticket_Quantity_Detail TQD LEFT JOIN Ticket_Locked_Detail TLD ON TQD.TQD_Id = TLD.TLD_TQD_Id
		--WHERE TQD_PE_Id = @PublishId and TQD_Event_Id = @EventId AND TQD_Ticket_Id = @TicId
		
		
		--SELECT  ISNULL(TQD.TQD_Remaining_Quantity,0) - isnull(TLD.TLD_Locked_Qty,0) FROM 
		--Ticket_Quantity_Detail TQD LEFT JOIN Ticket_Locked_Detail TLD ON TQD.TQD_Id = TLD.TLD_TQD_Id
		--WHERE TQD_Event_Id = 96 AND TQD_Ticket_Id = 35 and TQD_PE_Id = 4 


		--SELECT @RemQty = (ISNULL(TQD.TQD_Remaining_Quantity,0) - isnull(TLD.TLD_Locked_Qty,0))  FROM 
		--Ticket_Quantity_Detail TQD LEFT JOIN Ticket_Locked_Detail TLD ON TQD.TQD_Id = TLD.TLD_TQD_Id
		--WHERE TQD.TQD_Id = @TQTY_Id

		
		--Select @RemQty

		if (@MinQty>0) SET @iflag =1 Else SET @iflag =0

		SET @TMaxQty = '<option>0</option>'
		If (@MaxQty >0) SET @TQty = ((@MaxQty-@MinQty) +@iflag)
		
		IF (@TQty>@RemQty) SET @TQty =((@RemQty-@MinQty) +@iflag) 
		
		--Select COncat('TQTY', @TQty)

		If (@MinQty >0) 
		BEGIN
			SET @Option = @MinQty 
		END
		ELSE 
		BEGIN
			 SET @Option =1
			 
		END



		WHILE (@TQty>0)
		BEGIN
			SET @TMaxQty = CONCAT(@TMaxQty,'<option>',@Option,'</option>')
			SET @Option = @Option +1
			SET @TQty = @TQty -1
			
		END

		
		--GetTicketListing 289
		--0SELECT @RemQty

		SET @FlagHide=0
		IF (@HideTicket = 1)
		BEGIN
			if (@TicktHideTypeSale=0 AND @RemQty<=0) SET @FlagHide = 1
			ELSE if (@TicktHideTypeSale=1)
			BEGIN 
				IF (CONVERT(DATE,@HideUntillDate) > CONVERT(DATE,GETDATE()))
				BEGIN
					SET @FlagHide =1
				END
				ELSE if (CONVERT(DATE,@HideUntillDate) = CONVERT(DATE,GETDATE()))
				BEGIN
					If (CONVERT(TIME,@HideUntillFromTime) > CAST(GETDATE() AS TIME)) SET @FlagHide =1
				END
				IF (CONVERT(DATE,@HideToDate) < CONVERT(DATE,GETDATE()))
				BEGIN
					SET @FlagHide =1
				END
				ELSE If (CONVERT(DATE,@HideToDate) = CONVERT(DATE,GETDATE())) 
				BEGIN
					IF (CONVERT(TIME,@HideUntillToTime) < CAST(GETDATE() AS TIME)) SET @FlagHide =1
				END
			END
		END


		if (@FlagHide =0)
		BEGIN
		if (@IsShowDesc = 1 AND isnull(@TicketDesc,'') != '')
		BEGIN
			SET @Html = CONCAT(@Html,'<tr><td style="width:40%">', @T_Name ,'<br><span class="tkt_more_desc">  <i onclick="checkMore(',@TQTY_Id,')">[More Info]</i></span>', '<span style="display:none;" id="Sp_',@TQTY_Id,'"class="col-sm-12 no_pad">',@TicketDesc,'</span>' ,'</td>')
		END
		ELSE 
		BEGIN
			SET @Html = CONCAT(@Html,'<tr><td style="width:40%">', @T_Name ,'<br>', '<span style="display:none;" id="Sp_',@TQTY_Id,'"class="col-sm-12 no_pad">',@TicketDesc,'</span>' ,'</td>')
		END

		--SET @Html = CONCAT(@Html,'<tr><td style="width:40%">', @T_Name ,'</td>')

		SET @Html = CONCAT(@Html,'<td>',@TicketSaleEnd,'</td>')
			
			IF (@TicTypeId = 2)  
			BEGIN
				if (@Discount>0)
				BEGIN
					SET @T_Fee = (@TotalPrice - @T_Price)
					SET @TotalPrice = (@T_Price  - @Discount) + @T_Fee 
					SET @Html = CONCAT(@Html,'<td align="right">$', '<strike>',@T_Price,'</strike> $',(@T_Price-@Discount),'<span style="display:none" id="P_', @TQTY_Id, '">',@TotalPrice,'</span>','</td>')
					SET @Html = CONCAT(@Html,'<td align="right">$',@T_Fee ,'</td>')
				END
				ELSE
				BEGIN
					SET @T_Fee = (@TotalPrice - @T_Price)
					SET @Html = CONCAT(@Html,'<td align="right">$', @T_Price,'<span style="display:none" id="P_', @TQTY_Id, '">',@TotalPrice,'</span>','</td>')
					SET @Html = CONCAT(@Html,'<td align="right">$',@T_Fee ,'</td>')
				END
			END
			ELSE IF (@TicTypeId = 1)  
			BEGIN
				SET @Html = CONCAT(@Html,'<td align="right">','<span id="P_', @TQTY_Id, '">','Free','</span>','</td>')
				SET @Html = CONCAT(@Html,'<td align="right">$','0.00','</td>')
			END
			ELSE IF (@TicTypeId = 3)  
			BEGIN
				SET @Html = CONCAT(@Html,'<td align="right" colspan="2">','<span style="font-weight: normal; float: left; margin-right: 7px; margin-top:3px">$ </span> <div class="col-sm-10 no_pad"><input type="text" class="form-control evnt_inp_cont" oncopy="return false" onpaste="return false" oncut="return false" onkeydown="return checknumeric(event);" onchange="calculateTickTotal();" placeholder=" Enter Donation" autocomplete="false" name="d" value="" id="txtd_',@TQTY_Id,'"  /></div>','</td>')
			END
			
--GetTicketListing 281
			
			If (@TickatMarkSoldOut =1)
			BEGIN
				SET @Html = CONCAT(@Html,'<td align="right">Sold Out<select style="display:none" class="form-control ev_drop_label" onchange="calculateTickTotal()" id=d_' ,CONVERT(VARCHAR,@TQTY_Id) ,'>', @TMaxQty,'</select><input type="hidden" value="" id="hid_', CONVERT(VARCHAR,@TQTY_Id) ,'" /></td>')
				SET @Html = CONCAT(@Html,'</tr>')
			END
			ELSE IF (@SaleStartDate != '' AND CONVERT(DATE,@SaleStartDate) > CONVERT(DATE,GETDATE()))
			BEGIN
		
				SET @Html = CONCAT(@Html,'<td align="right">Sales Not Start Yet<select style="display:none" class="form-control ev_drop_label" onchange="calculateTickTotal()" id=d_' ,CONVERT(VARCHAR,@TQTY_Id) ,'>', @TMaxQty,'</select><input type="hidden" value="" id="hid_', CONVERT(VARCHAR,@TQTY_Id) ,'" /></td>')
				SET @Html = CONCAT(@Html,'</tr>')
			END
			ELSE if (@SaleEndDate != '' AND CONVERT(DATE,@SaleEndDate) < CONVERT(DATE,GETDATE()))
			BEGIN
				SET @Html = CONCAT(@Html,'<td align="right">Sales End<select style="display:none" class="form-control ev_drop_label" onchange="calculateTickTotal()" id=d_' ,CONVERT(VARCHAR,@TQTY_Id) ,'>', @TMaxQty,'</select><input type="hidden" value="" id="hid_', CONVERT(VARCHAR,@TQTY_Id) ,'" /></td>')
				SET @Html = CONCAT(@Html,'</tr>')
			END
			ELSE
			BEGIN
				if (@SaleStartDate != '' AND  @SaleStartTime != '' AND CONVERT(DATE,@SaleStartDate) = CONVERT(DATE,GETDATE()) AND CONVERT(TIME,@SaleStartTime) > CAST(GETDATE() AS TIME)) 
				BEGIN
					SET @Html = CONCAT(@Html,'<td align="right">Sales Not Start Yet<select style="display:none" class="form-control ev_drop_label" onchange="calculateTickTotal()" id=d_' ,CONVERT(VARCHAR,@TQTY_Id) ,'>', @TMaxQty,'</select><input type="hidden" value="" id="hid_', CONVERT(VARCHAR,@TQTY_Id) ,'" /></td>')
					SET @Html = CONCAT(@Html,'</tr>')
				END
				ELSE if (@SaleEndDate != '' AND @SaleEndTime != '' AND CONVERT(DATE,@SaleEndDate) = CONVERT(DATE,GETDATE()) AND CONVERT(TIME,@SaleEndTime) < CAST(GETDATE() AS TIME)) 
				BEGIN
					SET @Html = CONCAT(@Html,'<td align="right">Sales End<select style="display:none" class="form-control ev_drop_label" onchange="calculateTickTotal()" id=d_' ,CONVERT(VARCHAR,@TQTY_Id) ,'>', @TMaxQty,'</select><input type="hidden" value="" id="hid_', CONVERT(VARCHAR,@TQTY_Id) ,'" /></td>')
					SET @Html = CONCAT(@Html,'</tr>')
				END
				ELSE
				BEGIN
					If (@TicTypeId = 3) 
					BEGIN
						SET @Html = CONCAT(@Html,'<td align="right"><select style="width:90px;display:none" class="form-control ev_drop_label" onchange="calculateTickTotal()" id=d_' ,CONVERT(VARCHAR,@TQTY_Id) ,'>', @TMaxQty,'</select><input type="hidden" value="" id="hid_', CONVERT(VARCHAR,@TQTY_Id) ,'" /></td>')
					END
					ELSE
					BEGIN
						SET @Html = CONCAT(@Html,'<td align="right"><select style="width:90px" class="form-control ev_drop_label" onchange="calculateTickTotal()" id=d_' ,CONVERT(VARCHAR,@TQTY_Id) ,'>', @TMaxQty,'</select><input type="hidden" value="" id="hid_', CONVERT(VARCHAR,@TQTY_Id) ,'" /></td>')
					END
					SET @Html = CONCAT(@Html,'</tr>')
				END
			END
		END

		SET @RowCount = @RowCount  -1
		SET @RowIndex = @RowIndex  +1
		
		If (@AddIdss='' AND @RowCount=0 AND @SHowRemaingTick ='Y')
		BEGIN
			SET @Html = CONCAT(@Html,'<tr> <td class="tbl_white_bg" colspan="5" align="right">','<b>','Remaining Quantity : ' , @TRemainRuning ,'</b>','</td></tr>') 
			SET @TRemainRuning =0
		END

	END

	--GetTicketListing 0

	

	SET @RCnt = @RCnt -1
	SET @RInd = @RInd +1
END
	if (@AddIdss != '' AND @SHowRemaingTick ='Y')
	BEGIN
		SET @Html = CONCAT(@Html,'<tr> <td class="tbl_white_bg" colspan="5" align="right">','<b>','Remaining Quantity : ' , @TRemainRuning ,'</b>','</td></tr>') 
		SET @TRemainRuning =0
	END
--declare @Html varchar(Max)
--SET @Html ='</table>'
--SET @Html = CONCAT(@Html,'First')
--SET @Html = CONCAT(@Html, 'Second')
--SET @Html += '<td>Satnam Waheguru</td>'
--SET @Html += '</tr>'
SET @Html = CONCAT(@Html,'</table>')
SET @Html = CONCAT(@Html,'<input id="hdIds" type="hidden" value=', @QtyIds ,' />')

Select @Html as Ticket


END
























GO
/****** Object:  StoredProcedure [dbo].[PublishEvent]    Script Date: 1/11/2016 6:01:02 PM ******/
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

CREATE PROC [dbo].[PublishEvent]
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

					INSERT Ticket_Quantity_Detail ([TQD_PE_Id],[TQD_Event_Id],[TQD_Ticket_Id],[TQD_AddressId],[TQD_Quantity],[TQD_Remaining_Quantity],TQD_StartDate,TQD_StartTime)
					VALUES(@PublishId,@EventId,@Tickid,@Addid,@TQuantity,@TQuantity,@StartDate,@StartTime)
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

					INSERT Ticket_Quantity_Detail ([TQD_PE_Id],[TQD_Event_Id],[TQD_Ticket_Id],[TQD_AddressId],[TQD_Quantity],[TQD_Remaining_Quantity],TQD_StartDate,TQD_StartTime)
					VALUES(@PublishId,@EventId,@Tickid,0,@TQuantity,@TQuantity,@StartDate,@StartTime)
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












GO
/****** Object:  UserDefinedFunction [dbo].[func_Split]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[func_Split] 
    (   
    @DelimitedString    varchar(8000),
    @Delimiter              varchar(100) 
    )
RETURNS @tblArray TABLE
    (
    ElementID   int IDENTITY(1,1),  -- Array index
    Element     varchar(1000)               -- Array element contents
    )
AS
BEGIN

    -- Local Variable Declarations
    -- ---------------------------
    DECLARE @Index      smallint,
                    @Start      smallint,
                    @DelSize    smallint

    SET @DelSize = LEN(@Delimiter)

    -- Loop through source string and add elements to destination table array
    -- ----------------------------------------------------------------------
    WHILE LEN(@DelimitedString) > 0
    BEGIN

        SET @Index = CHARINDEX(@Delimiter, @DelimitedString)

        IF @Index = 0
            BEGIN

                INSERT INTO
                    @tblArray 
                    (Element)
                VALUES
                    (LTRIM(RTRIM(@DelimitedString)))

                BREAK
            END
        ELSE
            BEGIN

                INSERT INTO
                    @tblArray 
                    (Element)
                VALUES
                    (LTRIM(RTRIM(SUBSTRING(@DelimitedString, 1,@Index - 1))))

                SET @Start = @Index + @DelSize
                SET @DelimitedString = SUBSTRING(@DelimitedString, @Start , LEN(@DelimitedString) - @Start + 1)

            END
    END

    RETURN
END


GO
/****** Object:  UserDefinedFunction [dbo].[ReturnDayOfWeek]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[ReturnDayOfWeek](@dtDate DATETIME)
RETURNS VARCHAR(10)
AS
BEGIN
DECLARE @rtDayofWeek VARCHAR(10)
SELECT @rtDayofWeek = CASE datename(dw,@dtDate)
WHEN 'Sunday' THEN 7
WHEN 'Monday' THEN 1
WHEN 'Tuesday' THEN 2
WHEN 'Wednesday' THEN 3
WHEN 'Thursday' THEN 4
WHEN 'Friday' THEN 5
WHEN 'Saturday' THEN 6
END
RETURN (@rtDayofWeek)
END



GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Address]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Address](
	[AddressID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Address1] [varchar](200) NOT NULL,
	[Address2] [varchar](200) NOT NULL,
	[City] [varchar](200) NOT NULL,
	[State] [varchar](200) NOT NULL,
	[Zip] [varchar](10) NOT NULL,
	[CountryID] [tinyint] NOT NULL,
	[VenueName] [varchar](500) NULL,
	[UserId] [nvarchar](256) NULL,
	[EventId] [bigint] NULL,
	[ConsolidateAddress] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[AddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [varchar](150) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [varchar](150) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[LoginStatus] [varchar](1) NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BillingAddress]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BillingAddress](
	[BillingId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [nchar](128) NULL,
	[OrderId] [varchar](1000) NULL,
	[Guid] [nchar](128) NULL,
	[Fname] [varchar](50) NULL,
	[Lname] [varchar](50) NULL,
	[Phone_Number] [varchar](50) NULL,
	[Address1] [varchar](256) NULL,
	[Address2] [varchar](256) NULL,
	[City] [varchar](256) NULL,
	[State] [varchar](256) NULL,
	[Zip] [varchar](256) NULL,
	[Country] [varchar](26) NULL,
 CONSTRAINT [PK_BillingAddress] PRIMARY KEY CLUSTERED 
(
	[BillingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CardDetails]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CardDetails](
	[CardId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [nchar](128) NULL,
	[OrderId] [varchar](1000) NULL,
	[Guid] [nchar](128) NULL,
	[CardNumber] [varchar](50) NULL,
	[ExpirationDate] [varchar](50) NULL,
	[Cvv] [varchar](50) NULL,
 CONSTRAINT [PK_CardDetails] PRIMARY KEY CLUSTERED 
(
	[CardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Country]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Country](
	[CountryID] [tinyint] IDENTITY(1,1) NOT NULL,
	[Country] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DeliveryMethod]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DeliveryMethod](
	[DeliveryMethodID] [bigint] IDENTITY(1,1) NOT NULL,
	[DeliveryMethod] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DeliveryMethodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Email_Tag]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Email_Tag](
	[Tag_Id] [nchar](150) NOT NULL,
	[Tag_Name] [nvarchar](200) NULL,
 CONSTRAINT [PK_Email_Tag] PRIMARY KEY CLUSTERED 
(
	[Tag_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Email_Template]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Email_Template](
	[TemplateId] [varchar](150) NOT NULL,
	[Template_Name] [varchar](300) NULL,
	[To] [varchar](300) NULL,
	[CC] [varchar](300) NULL,
	[Bcc] [varchar](300) NULL,
	[Subject] [varchar](300) NULL,
	[TemplateHtml] [nvarchar](max) NULL,
	[From] [varchar](300) NULL,
	[Template_Tag] [varchar](300) NULL,
 CONSTRAINT [PK_Email_Template] PRIMARY KEY CLUSTERED 
(
	[TemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Event]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Event](
	[EventID] [bigint] IDENTITY(1,1) NOT NULL,
	[EventTypeID] [bigint] NOT NULL,
	[EventCategoryID] [bigint] NOT NULL,
	[EventSubCategoryID] [bigint] NOT NULL,
	[UserID] [nvarchar](256) NULL,
	[EventTitle] [nvarchar](600) NULL,
	[DisplayStartTime] [varchar](1) NULL,
	[DisplayEndTime] [varchar](1) NULL,
	[DisplayTimeZone] [varchar](1) NULL,
	[EventDescription] [nvarchar](max) NULL,
	[EventPrivacy] [varchar](20) NULL,
	[Private_ShareOnFB] [varchar](1) NULL,
	[Private_GuestOnly] [varchar](1) NULL,
	[Private_Password] [varchar](50) NULL,
	[EventUrl] [nvarchar](2000) NULL,
	[PublishOnFB] [varchar](1) NULL,
	[EventStatus] [varchar](20) NULL,
	[AddedOn] [datetime] NULL,
	[UpdateOn] [datetime] NULL,
	[IsMultipleEvent] [varchar](1) NULL,
	[TimeZone] [nvarchar](1000) NULL,
	[FBUrl] [nvarchar](1000) NULL,
	[TwitterUrl] [nvarchar](1000) NULL,
	[AddressStatus] [varchar](100) NULL,
	[LastLocationAddress] [bigint] NULL,
	[EnableFBDiscussion] [varchar](1) NULL,
	[Feature] [int] NULL,
	[Ticket_DAdress] [varchar](1) NULL,
	[Ticket_showremain] [varchar](1) NULL,
	[Ticket_showvariable] [varchar](1) NULL,
	[Ticket_variabledesc] [varchar](256) NULL,
	[Ticket_variabletype] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifyDate] [datetime] NULL,
	[ShowMap] [varchar](1) NULL,
	[Parent_EventID] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[EventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Event_OrganizerMessages]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Event_OrganizerMessages](
	[EventId] [bigint] NULL,
	[OrganizerId] [bigint] NULL,
	[Userid] [varchar](150) NULL,
	[Name] [varchar](256) NULL,
	[Email] [varchar](256) NULL,
	[Message] [nvarchar](max) NULL,
	[MessageId] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Event_OrganizerMessages] PRIMARY KEY CLUSTERED 
(
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Event_Orgnizer_Detail]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Event_Orgnizer_Detail](
	[Orgnizer_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Orgnizer_Event_Id] [bigint] NULL,
	[Orgnizer_Name] [varchar](200) NULL,
	[Orgnizer_Desc] [nvarchar](max) NULL,
	[FBLink] [varchar](100) NULL,
	[Twitter] [varchar](100) NULL,
	[UserId] [nvarchar](256) NULL,
	[DefaultOrg] [varchar](1) NULL,
	[Linkedin] [varchar](100) NULL,
 CONSTRAINT [PK_Event_Orgnizer_Detail] PRIMARY KEY CLUSTERED 
(
	[Orgnizer_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Event_VariableDesc]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event_VariableDesc](
	[VariableDesc] [nvarchar](256) NULL,
	[Price] [numeric](18, 2) NULL,
	[Event_Id] [bigint] NULL,
	[Variable_Id] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Event_VariableDesc] PRIMARY KEY CLUSTERED 
(
	[Variable_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EventCategory]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EventCategory](
	[EventCategoryID] [bigint] IDENTITY(1,1) NOT NULL,
	[EventCategory] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EventCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EventFavourite]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventFavourite](
	[UserID] [nvarchar](128) NULL,
	[eventId] [bigint] NULL,
	[FavId] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_EventFavourite] PRIMARY KEY CLUSTERED 
(
	[FavId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EventImage]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EventImage](
	[EventImageID] [bigint] IDENTITY(1,1) NOT NULL,
	[EventID] [bigint] NOT NULL,
	[EventImageUrl] [varchar](200) NULL,
	[ImageType] [varchar](50) NULL,
 CONSTRAINT [PK_EventImage] PRIMARY KEY CLUSTERED 
(
	[EventImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EventSubCategory]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EventSubCategory](
	[EventSubCategoryID] [bigint] IDENTITY(1,1) NOT NULL,
	[EventCategoryID] [bigint] NOT NULL,
	[EventSubCategory] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EventSubCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EventType]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EventType](
	[EventTypeID] [bigint] IDENTITY(1,1) NOT NULL,
	[EventType] [varchar](200) NOT NULL,
	[EventHide] [char](1) NULL,
PRIMARY KEY CLUSTERED 
(
	[EventTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EventVenue]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EventVenue](
	[EventVenueID] [bigint] IDENTITY(1,1) NOT NULL,
	[EventID] [bigint] NOT NULL,
	[AddressId] [bigint] NOT NULL,
	[EventStartDate] [varchar](50) NULL,
	[EventEndDate] [varchar](50) NULL,
	[EventStartTime] [varchar](50) NULL,
	[EventEndTime] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[EventVenueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EventVote]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventVote](
	[UserID] [nvarchar](128) NULL,
	[eventId] [bigint] NULL,
	[VoteId] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_EventVote] PRIMARY KEY CLUSTERED 
(
	[VoteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Fee_Structure]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Fee_Structure](
	[FeeId] [bigint] IDENTITY(1,1) NOT NULL,
	[FeeType] [varchar](50) NULL,
	[FeeAmount] [numeric](18, 0) NULL,
 CONSTRAINT [PK_Fee_Structure] PRIMARY KEY CLUSTERED 
(
	[FeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Messages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Form_Name] [varchar](200) NULL,
	[Form_Tag] [varchar](200) NULL,
	[Message] [varchar](500) NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MultipleEvent]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MultipleEvent](
	[MultipleEventID] [bigint] IDENTITY(1,1) NOT NULL,
	[EventID] [bigint] NOT NULL,
	[Frequency] [varchar](20) NOT NULL,
	[StartingFrom] [varchar](50) NULL,
	[StartingTo] [varchar](50) NULL,
	[WeeklyDay] [varchar](100) NULL,
	[MonthlyDay] [int] NULL,
	[StartTime] [varchar](50) NULL,
	[EndTime] [varchar](50) NULL,
	[MonthlyWeek] [varchar](10) NULL,
	[MonthlyWeekDays] [varchar](100) NULL,
 CONSTRAINT [PK__Multiple__8A680315196CFEEE] PRIMARY KEY CLUSTERED 
(
	[MultipleEventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Order_Detail_T]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Order_Detail_T](
	[O_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[O_Order_Id] [varchar](100) NULL,
	[O_User_Id] [nvarchar](256) NULL,
	[O_TotalAmount] [numeric](18, 2) NULL,
	[O_OrderAmount] [numeric](18, 2) NULL,
	[O_VariableId] [bigint] NULL,
	[O_VariableAmount] [numeric](18, 2) NULL,
	[O_PromoCodeId] [bigint] NULL,
 CONSTRAINT [PK_Order_Detail_T] PRIMARY KEY CLUSTERED 
(
	[O_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Permission_Detail]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Permission_Detail](
	[Permission_Id] [int] IDENTITY(1,1) NOT NULL,
	[Permission_Desc] [varchar](100) NULL,
	[Permission_Category] [varchar](50) NULL,
 CONSTRAINT [PK_Permission_Detail] PRIMARY KEY CLUSTERED 
(
	[Permission_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Profile]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Profile](
	[ProfileID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](128) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Email] [varchar](100) NULL,
	[StreetAddressLine1] [varchar](256) NULL,
	[StreetAddressLine2] [varchar](256) NULL,
	[City] [varchar](256) NULL,
	[State] [varchar](256) NULL,
	[Zip] [varchar](10) NULL,
	[CountryID] [tinyint] NOT NULL,
	[MainPhone] [varchar](20) NULL,
	[SecondPhone] [varchar](20) NULL,
	[WebsiteURL] [varchar](100) NULL,
	[TwitterURL] [varchar](100) NULL,
	[FacebookURL] [varchar](100) NULL,
	[GooglePlusURL] [varchar](100) NULL,
	[LinkedInURL] [varchar](100) NULL,
	[UserProfileImage] [varchar](300) NULL,
	[OrganizerName] [varchar](50) NULL,
	[SecondaryEmail] [varchar](50) NULL,
	[OrganizerDescription] [varchar](max) NULL,
	[AddedOn] [datetime] NULL,
	[UpdateOn] [datetime] NULL,
	[WorkPhone] [varchar](20) NULL,
	[ContentType] [varchar](50) NULL,
	[Gender] [varchar](50) NULL,
	[DateofBirth] [varchar](50) NULL,
	[Organiser] [varchar](2) NULL,
	[Merchant] [varchar](2) NULL,
	[UserStatus] [varchar](2) NULL,
	[SendCur_EventDetail] [char](1) NULL,
	[Ipcountry] [varchar](100) NULL,
	[IpState] [varchar](100) NULL,
	[Ipcity] [varchar](100) NULL,
 CONSTRAINT [PK__Profile__290C888415502E78] PRIMARY KEY CLUSTERED 
(
	[ProfileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Publish_Event_Detail]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Publish_Event_Detail](
	[PE_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PE_Event_Id] [bigint] NULL,
	[PE_Address_Ids] [varchar](100) NULL,
	[PE_MultipleVenue_id] [bigint] NULL,
	[PE_SingleVenue_Id] [bigint] NULL,
	[PE_Tickets_Ids] [varchar](100) NULL,
	[PE_Scheduled_Date] [varchar](50) NULL,
	[PE_Start_Time] [varchar](50) NULL,
	[PE_End_Time] [varchar](50) NULL,
 CONSTRAINT [PK_Publish_Event_Detail] PRIMARY KEY CLUSTERED 
(
	[PE_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ShippingAddress]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ShippingAddress](
	[ShippingId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [nchar](128) NULL,
	[OrderId] [varchar](1000) NULL,
	[Guid] [nchar](128) NULL,
	[Fname] [varchar](50) NULL,
	[Lname] [varchar](50) NULL,
	[Phone_Number] [varchar](50) NULL,
	[Address1] [varchar](256) NULL,
	[Address2] [varchar](256) NULL,
	[City] [varchar](256) NULL,
	[State] [varchar](256) NULL,
	[Zip] [varchar](256) NULL,
	[Country] [varchar](26) NULL,
 CONSTRAINT [PK_ShippingAddress] PRIMARY KEY CLUSTERED 
(
	[ShippingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Status]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Status](
	[StatusID] [bigint] IDENTITY(1,1) NOT NULL,
	[StatusName] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ticket](
	[T_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[E_Id] [bigint] NULL,
	[T_name] [nvarchar](256) NULL,
	[Qty_Available] [bigint] NOT NULL,
	[Price] [decimal](9, 2) NULL,
	[TicketTypeID] [bigint] NULL,
	[T_Sold] [varchar](1) NULL,
	[Registration_Recorded] [varchar](1) NULL,
	[T_Desc] [nvarchar](1000) NULL,
	[Show_T_Desc] [varchar](1) NULL,
	[Fees_Type] [varchar](50) NULL,
	[Sale_Start_Date] [date] NULL,
	[Sale_Start_Time] [varchar](50) NULL,
	[Sale_End_Date] [date] NULL,
	[Sale_End_Time] [varchar](50) NULL,
	[Hide_Ticket] [varchar](1) NULL,
	[Auto_Hide_Sche] [varchar](1) NULL,
	[Hide_Untill_Date] [date] NULL,
	[Hide_Untill_Time] [varchar](50) NULL,
	[Hide_After_Date] [date] NULL,
	[Hide_After_Time] [varchar](50) NULL,
	[Min_T_Qty] [numeric](6, 0) NULL,
	[Max_T_Qty] [numeric](6, 0) NULL,
	[T_Disable] [varchar](1) NULL,
	[T_Mark_SoldOut] [varchar](1) NULL,
	[T_Sold_Qty] [bigint] NULL,
	[T_order] [tinyint] NULL,
	[EC_Fee] [decimal](9, 2) NULL,
	[Customer_Fee] [decimal](9, 2) NULL,
	[T_Displayremaining] [varchar](50) NULL,
	[T_AutoSechduleType] [varchar](1) NULL,
	[Additional_Fee] [decimal](9, 2) NULL,
	[T_Discount] [decimal](9, 2) NULL,
	[TotalPrice] [decimal](9, 2) NULL,
 CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED 
(
	[T_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ticket_Locked_Detail]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ticket_Locked_Detail](
	[TLD_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TLD_User_Id] [nvarchar](256) NULL,
	[TLD_Locked_Qty] [bigint] NULL,
	[TLD_Event_Id] [bigint] NULL,
	[TLD_TQD_Id] [bigint] NULL,
	[Locktime] [datetime] NULL,
	[TLD_GUID] [varchar](1000) NULL,
	[TLD_Donate] [decimal](18, 0) NULL,
	[TicketAmount] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Ticket_Locked_Detail] PRIMARY KEY CLUSTERED 
(
	[TLD_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ticket_Purchased_Detail]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ticket_Purchased_Detail](
	[TPD_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TPD_User_Id] [nvarchar](256) NULL,
	[TPD_Order_Id] [varchar](11) NULL,
	[TPD_Purchased_Qty] [bigint] NULL,
	[TPD_TQD_Id] [bigint] NULL,
	[TPD_Event_Id] [bigint] NULL,
	[TPD_Amount] [numeric](9, 2) NULL,
	[TPD_Donate] [decimal](18, 0) NULL,
	[TPD_GUID] [varchar](1000) NULL,
 CONSTRAINT [PK_Ticket_Purchased_Detail] PRIMARY KEY CLUSTERED 
(
	[TPD_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ticket_Quantity_Detail]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ticket_Quantity_Detail](
	[TQD_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TQD_PE_Id] [bigint] NULL,
	[TQD_Event_Id] [bigint] NULL,
	[TQD_Ticket_Id] [bigint] NULL,
	[TQD_AddressId] [bigint] NULL,
	[TQD_Quantity] [bigint] NULL,
	[TQD_Remaining_Quantity] [bigint] NULL,
	[TQD_StartDate] [varchar](50) NULL,
	[TQD_StartTime] [varchar](50) NULL,
 CONSTRAINT [PK_Ticket_Quantity_Info] PRIMARY KEY CLUSTERED 
(
	[TQD_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TicketBearer]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TicketBearer](
	[TicketbearerId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [nchar](128) NULL,
	[OrderId] [varchar](1000) NULL,
	[Guid] [nchar](128) NULL,
	[Name] [nchar](128) NULL,
	[Email] [nchar](256) NULL,
 CONSTRAINT [PK_TicketBearer] PRIMARY KEY CLUSTERED 
(
	[TicketbearerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TicketDeliveryMethod]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TicketDeliveryMethod](
	[TicketDeliveryMethodID] [bigint] IDENTITY(1,1) NOT NULL,
	[DeliveryMethodID] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TicketDeliveryMethodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TicketType]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TicketType](
	[TicketTypeID] [bigint] IDENTITY(1,1) NOT NULL,
	[TicketType] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TicketTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TimeZoneDetail]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TimeZoneDetail](
	[TimeZone_Id] [int] IDENTITY(1,1) NOT NULL,
	[TimeZone_Name] [varchar](500) NULL,
	[TimeZone] [varchar](500) NULL,
 CONSTRAINT [PK_TimeZoneDetail] PRIMARY KEY CLUSTERED 
(
	[TimeZone_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TimeZonesystem]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TimeZonesystem](
	[DisplayName] [varchar](400) NULL,
	[Id] [varchar](400) NOT NULL,
 CONSTRAINT [PK_TimeZonesystem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User_Permission_Detail]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Permission_Detail](
	[UP_Id] [int] IDENTITY(1,1) NOT NULL,
	[UP_User_Id] [nvarchar](128) NULL,
	[UP_Permission_Id] [int] NOT NULL,
	[UP_Flag] [nchar](1) NULL,
 CONSTRAINT [PK_User_Permission_Detail] PRIMARY KEY CLUSTERED 
(
	[UP_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Venue]    Script Date: 1/11/2016 6:01:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Venue](
	[VenueID] [bigint] IDENTITY(1,1) NOT NULL,
	[AddressID] [bigint] NOT NULL,
	[VenueName] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[VenueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'201508241139135_InitialCreate', N'EventKombo.Models.ApplicationDbContext', 0x1F8B0800000000000400DD5CDB6EE436127D5F60FF41D0D3EEC269F99219CC1AED044EDBDE3532BE60DA13ECDB802DB1DBC248A422518E8D205F96877C527E618B1275E345976EB9BB1D0C30B0C8E2A962B148168BC5FEF3F73FA6DF3F8781F584E3C4A7E4CC3E9A1CDA16262EF57CB23AB353B6FCE683FDFD777FFFDBF4D20B9FAD9F0ABA134E072D4972663F32169D3A4EE23EE2102593D077639AD0259BB8347490479DE3C3C37F3B47470E06081BB02C6BFA2925CC0F71F6019F334A5C1CB1140537D4C34122CAA1669EA15AB728C449845C7C665F3E61C27EA4E1824E7262DB3A0F7C0482CC71B0B42D440865888198A79F133C673125AB7904052878788930D02D51906021FE6945DEB72787C7BC274ED5B08072D384D17020E0D189508D23375F4BC176A93A50DE252899BDF05E670A3CB3AF3D9C157DA201284066783A0B624E7C66DF942CCE93E816B349D17092435EC500F70B8DBF4EEA880756EF7607A5291D4F0EF9BF036B96062C8DF119C1298B517060DDA78BC0777FC42F0FF42B266727478BE5C98777EF9177F2FE5B7CF2AEDE53E82BD0350AA0E83EA6118E4136BC2CFB6F5B4EB39D23372C9BD5DAE45A015B8259615B37E8F923262BF608F3E5F8836D5DF9CFD82B4A84717D263E4C2268C4E2143E6FD320408B0097F54E2B4FFE7F0BD7E377EF47E17A8B9EFC5536F4127F983831CCAB4F38C86A93473FCAA75763BCBF08B2AB9886FCBB695F79ED97394D639777861A491E50BCC2AC29DDD4A98CB7974973A8F1CDBA40DD7FD3E692AAE6AD25E51D5A6726142CB63D1B0A795F976F6F8B3B8F2218BCCCB4B846DA0C4ED9AB2652E303AB22A90CE7A8AFE110E8D05F791DBC0C911F8CB010F6E0022EC8D28F435CF6F2070A6687C86099EF5192C03AE0FD17258F2DA2C39F23883EC76E1A8379CE190AA357E776FF4809BE4DC305B7FAEDF11A6D681E7EA157C86534BE24BCD5C6781FA9FB95A6EC92781788E1CFCC2D00F9E7831FF60718459C73D7C5497205C68CBD19050FBB00BC26ECE478301C5F9F76ED88CC02E4877A4F445A49BF14A49537A2A7503C120399CE2B6913F5235DF9A49FA805A959D49CA25354413654540ED64F524169163423E89433A71ACDCFCB46687C472F83DD7F4F6FB3CDDBB416D4D438871512FF07131CC332E6DD23C6704CAA11E8B36EECC259C8868F337DF5BD29E3F4130AD2B159AD351BB24560FCD990C1EEFF6CC8C484E227DFE35E498FE34F410CF0BDE8F527ABEE392749B6EDE9D0E8E6B6996F670D304D97F324A1AE9FCD024DE04B842D9AF2830F6775C730F2DEC87110E81818BACFB73C2881BED9B251DD910B1C6086AD73370F0CCE50E2224F552374C81B2058B1A36A04ABE2214DE1FEA5F0044BC7316F84F821288199EA13A64E0B9FB87E84824E2D492D7B6E61BCEF250FB9E602479870869D9AE8C35C1FFEE002947CA441E9D2D0D4A9595CBB211ABC56D39877B9B0D5B82B5189ADD86487EF6CB04BE1BFBD8A61B66B6C0BC6D9AE923E02184379BB30507156E96B00F2C165DF0C543A31190C54B8545B31D0A6C67660A04D95BC3903CD8FA87DC75F3AAFEE9B79360FCADBDFD65BD5B503DB6CE863CF4C33F73DA10D83163856CDF362C12BF133D31CCE404E713E4B84AB2B9B08079F63D60CD954FEAED60F75DA4164236A03AC0CAD03545C022A40CA841A205C11CB6B954E781103608BB85B2BAC58FB25D89A0DA8D8F5CBD01AA1F9CA5436CE5EA78FB267A5352846DEEBB050C3D11884BC78353BDE4329A6B8ACAA983EBEF0106FB8D63131182D0AEAF05C0D4A2A3A33BA960AD3ECD692CE211BE2926DA425C97D3268A9E8CCE85A1236DAAD248D5330C02DD84845CD2D7CA4C956443ACADDA6AC9B3A798A9428983A865CAAE90D8A229FAC6AB955A2C49AE78955B36FE6C3538EC21CC371134DE651296DC989D118ADB0540BAC41D22B3F4ED805626881789C67E6850A99766F352CFF05CBFAF6A90E62B10F14D4FC6F7129AC5CDD37B65AD517111057D0C1903B3459145D33FCFAE6164F7543018A3581FB190DD29098FD2B73EBFCFAAEDE3E2F5111A68E24BFE23F29CA52BCDCA6E67B8D8B3A27C619A3D27B597F9CCC10266D17BE675DDF267FD48C5284A7EA28A690D5CEC6CDE4C60C192BD9411C3E549D08AF33AB44564A1D40140DC4A825362860B5BAFEA8CDDC933A66B3A63FA29460528794AA0648594F23690859AF580BCFA0513D457F0E6AE2481D5DADED8FAC4921A9436BAAD7C0D6C82CD7F547D56499D48135D5FDB1AB9413790DDDE37DCB786C5977E3CA0FB69BED5C068CD75910C7D9F86AF7F775A05AF1402C7143AF8089F2BD3426E3E96E5D63CAC3199B199301C3BCEE342EBE9BCB4EEB6DBD19B3719BDD58DADB6EF3CD78C34CF6550D4339DBC92425F7F28C279DE5A6E25CD5FD78463968E524B655A811B6F59784E170C20926F39F8359E063BE8817043788F84B9CB03C83C33E3E3C3A961EE0ECCF63182749BC40732E35BD88698ED91692B1C8138ADD4714ABA9111B3C18A94095A8F335F1F0F399FD6BD6EA340B60F0BFB2E203EB3AF94CFC9F53A87888536CFDA6A67A8E9340DF7EC2DAD3E70EFDB57AFDBF2F79D303EB2E8619736A1D4ABA5C67849B8F2006499337DD409AB59F46BCDD09D57879A0459526C4FA0F0D163E1BE5914121E53F42F4FCCFA1A2691F126C84A8792C3016DE282A343D065807CBF810C0834F963D0418D659FDC3807544333E0AF0C97030F94940FF65A868B9C3AD467324DAC69294E9B933A57AA3FCCA5DEF4D4AE6F546135DCDAE1E00B74106F51A96F1C6928F47DB1D35B9C5A361EFD2B45F3DA1785F7288ABEC8EDDA60E6F335BB8E54EE82F9524BC07696D9A349DDDA7026FDBD64C61DC3DCFA71C96F0BB67C62692B7769FD6BB6D63338579F7DCD80625EFEE99ADED6AFFDCB1A5F5DE42779E8AAB661519AE6374B1E0AE54DB3C700E27FC050523C83DCAFC85A43EB7CBC4AC321623C38AC4CCD49C54263356268EC257A168673BACAF62C36FEDACA069676B48C56CE32DD6FF56DE82A69DB721C1711749C2DA14435DE276C73AD69601F59692821B3DE9C841EFF2595BEFD6DF520EF0284A69CC1EC31DF1DB49F91D4525634E9D0129BEEA752FEC9DB55F5484FD3BF1571504FF7D4582DDC6AE59D25C93252D366F49A282448AD0DC60863CD852CF63E62F91CBA09AC798B327DE59DC8EDF742CB0774DEE5216A50CBA8CC345D008787127A08D7F96C7DC94797A1765BF56324617404C9FC7E6EFC80FA91F78A5DC579A989001827B1722A2CBC792F1C8EEEAA544BAA5A42790505FE9143DE0300A002CB92373F484D7910DCCEF235E21F7A58A009A40BA07A2A9F6E9858F56310A138151B5874FB0612F7CFEEEFFB9B517EC58540000, N'6.1.3-40302')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1', N'Super Admin')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'2', N'Admin')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3', N'Member')
GO
INSERT [dbo].[AspNetUserLogins] ([LoginProvider], [ProviderKey], [UserId]) VALUES (N'Google', N'104044562490672561869', N'5d7f9411-04cf-4431-9eb2-13efc37b6d33')
GO
INSERT [dbo].[AspNetUserLogins] ([LoginProvider], [ProviderKey], [UserId]) VALUES (N'Google', N'107515312229663167748', N'92587a71-6a50-4a85-911f-789c97580446')
GO
INSERT [dbo].[AspNetUserLogins] ([LoginProvider], [ProviderKey], [UserId]) VALUES (N'Google', N'112550343958679869633', N'16724f9c-389d-43a3-b6b2-bc83989b9102')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0ea7644e-f55e-47db-ad21-687465c6cf24', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'19bdbd0b-6fa3-4461-9eab-6e99c789476c', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1bd462fd-9fb2-45c9-941e-146b3a436f48', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'24556b52-131a-46ad-b6a8-2897417b40c0', N'2')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2dd29514-6025-4649-8d71-5ce2e117fa69', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2f52d635-8b42-4bb7-a08f-81102f023932', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'31f226aa-f64b-4ddf-a0f3-2ccd26886400', N'1')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'34b7f0f9-8776-4628-a690-8efa1a2bd425', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'36e34ed6-c4f9-4fae-bb0d-a6f5ec440350', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'376369a7-9d4d-4374-bd93-2e85dae03766', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'40cae138-f961-40cc-9dc4-2424b51f8a75', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'412beaf4-fcff-4fda-935b-23985ea250e2', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4b6dd2bc-6a21-480d-9249-034b7d1e76dc', N'2')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4f8d0f9a-e6f9-4a2d-8228-3486529687ab', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'50f99aaf-3975-417f-adf8-fc41b671c3f2', N'2')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'56d81941-25fa-4ac9-ae86-879b1d03e594', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5d7f9411-04cf-4431-9eb2-13efc37b6d33', N'1')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5efeb250-156b-44da-a474-f50b55c5dcb0', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6fcab4d1-8beb-469b-a801-cda2ffe6efb3', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'735b57df-6a29-48d6-87d3-e851aa3245c0', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'746ffe19-8552-4ef1-8138-3962cbc20575', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7ba72509-927a-4f58-9d7f-a41e3660204d', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7bd93525-a288-43fd-84f8-40ec4b7c50bd', N'2')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7d3d080d-934a-49c7-9c5c-01fd9f29d8ea', N'1')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7db0fbe0-e140-4288-b43b-f0a892343043', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7fde72cd-c8ec-41bb-8dd4-3c67a9f54e95', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'92587a71-6a50-4a85-911f-789c97580446', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9e10f676-b5d0-4520-9930-d52102d42d9a', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a5c9a386-95d2-4f47-bd11-853bbc993caa', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b413ab1f-4cf8-485a-bfb2-00b0236a5a41', N'1')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b68f6b28-3465-4b34-a75d-2958da20ee7b', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'bb9d07ec-0a08-4646-bc11-cb3e7f6480a3', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'bea62686-6748-494d-9898-b9d2d6f85cfa', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c09a9cd0-22cf-46f1-a40e-2c9c1f30a7c9', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c10cde59-184b-4244-9a7c-5b209e38eaae', N'1')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c91be264-0cca-4ade-91cd-c55ca1a184e8', N'1')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e5ac394c-c3bf-4f3b-aa29-feec9ee37629', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'eb7d6159-2475-4cd1-8a75-d5c986d3161a', N'1')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f3dff988-adb7-440d-9f17-8e37f108e131', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f4ee24cd-0194-45eb-ae30-0c92e151a7dc', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f59003aa-ef8d-4c6f-a92a-ab4fe081c6c2', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f8aa26e4-31d6-4cf4-8cec-708913931fcd', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fdad64ff-80f5-4a1e-96af-9948fece5967', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ffc2e2de-572f-4361-82df-02ae9af3a753', N'3')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'0ea7644e-f55e-47db-ad21-687465c6cf24', N'f12@f.com', 0, N'AJwZv4D77nhjWzsscK3kKXSXtA5J9DYGFMpR5qOWLcRisEH/aWULdco6EhGGyQEcDg==', N'ba8218cc-84f4-42bb-aaf2-7d95239b748a', NULL, 0, 0, NULL, 1, 0, N'f12@f.com', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'just.shweta29@gmail.com', 0, NULL, N'bec3e317-68bc-452f-8cd2-a86908f63c3e', NULL, 0, 0, NULL, 1, 0, N'just.shweta29@gmail.com', N'Y')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'19bdbd0b-6fa3-4461-9eab-6e99c789476c', N'navrit.singh@hotmail.com', 0, N'AG2Cg8GcrwW9XZbCY9LiloZASX44uhNXGj4LHcMkMKPI5bOitZWTPWAkkdBAJDSo4w==', N'78bb3c81-44b7-475a-aec1-4cea12df6673', NULL, 0, 0, NULL, 1, 0, N'navrit.singh@hotmail.com', N'Y')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'1bd462fd-9fb2-45c9-941e-146b3a436f48', N'kj@kjn.com', 0, N'AI4lBtHrQm9eFT1cJes/3TffidvtEDkhGVTOa4xy6+A9vthOwwoxLcnSq8VAUfhE/A==', N'52afd0fe-f9ff-4b6b-9021-0099b7e1d8eb', NULL, 0, 0, NULL, 1, 0, N'kj@kjn.com', N'Y')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'24556b52-131a-46ad-b6a8-2897417b40c0', N'f@f.com', 0, N'AFw1a3Iu45lKJTVe0FJfRM+roTQupSAffFyXgPLojK71Cr6k74Wuia4pSztTIEzGUA==', N'1c237586-0154-408c-8263-2dd4c8c1de60', NULL, 0, 0, NULL, 1, 0, N'f@f.com', N'Y')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'2dd29514-6025-4649-8d71-5ce2e117fa69', N'navi@yahoo.com', 0, N'AIVVDnWmQsc/LqdarbZGoyiTbTwVSJ9qYJGFSpldtMVuCxpsboZHdvN/KgwDX4Cqrw==', N'e682a189-d41d-46a2-a505-230732327450', NULL, 0, 0, NULL, 1, 0, N'navi@yahoo.com', N'Y')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'2f52d635-8b42-4bb7-a08f-81102f023932', N'khj@kjh.com', 0, N'APwMgYVnrR9Ugcn5pA+sQA8oPhkM1VS9ys2YzjIg9ywP28EhOCnwnQznvnltIFsxBg==', N'fc9bd76d-a322-4b39-8424-997fae65423a', NULL, 0, 0, NULL, 1, 0, N'khj@kjh.com', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'31f226aa-f64b-4ddf-a0f3-2ccd26886400', N'editor@eventcombo.com', 0, N'AADzRvLly/8WTBMmYabyAMqt77V0G5Jc4iPedTcSlfT1P8Og2/kFgs31+DaP/cg9sw==', N'e691b65c-6315-4b14-8240-c8f403089aca', NULL, 0, 0, NULL, 1, 0, N'editor@eventcombo.com', N'N')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'34b7f0f9-8776-4628-a690-8efa1a2bd425', N'g23h@gh.com', 0, N'ABjyoKFRqbwdSmRoqUNpLR6zzJonOqQslNcjGGVC6f3PXjPWdiRU5xr0ygZmBtu5DQ==', N'81870463-515a-4d15-b1f4-60729e457d5a', NULL, 0, 0, NULL, 1, 0, N'g23h@gh.com', N'Y')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'36e34ed6-c4f9-4fae-bb0d-a6f5ec440350', N'gh@gh.com', 0, N'AIDn45jsEQRRx0GSN/2O1G9RDFS9Xt9T90+A2tuDBJuv6TQyvxtJGaMJO/1/ssJNPA==', N'ddbf5a90-0875-4f7e-8cfe-8be8ef09a895', NULL, 0, 0, NULL, 1, 0, N'gh@gh.com', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'376369a7-9d4d-4374-bd93-2e85dae03766', N'Navi@new.com', 0, N'AK6fTnIOg/LllnTnpBLMfTJ8tR2tOPiw+CaMoEedRD2qIkxEEaj18xoNecNVz5NiXA==', N'7ff7ae01-ca24-458d-b627-fac350d11a16', NULL, 0, 0, NULL, 1, 0, N'Navi@new.com', N'N')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'40cae138-f961-40cc-9dc4-2424b51f8a75', N'kumar.prateek@gmail.com', 0, N'AKfyCZM+nbbuAlUNS0cHgAn1sisPxVounmvZ08c09SDKpKpadV7/aEAQRIdHNgwDNA==', N'2acd6eef-65e3-4c90-a1eb-bee6933dcbc8', NULL, 0, 0, NULL, 1, 0, N'kumar.prateek@gmail.com', N'N')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'412beaf4-fcff-4fda-935b-23985ea250e2', N'jh@jh.com', 0, N'AB2Rft1uPkHdwEAJ7ae9h9fw4VAYfxWZa2tEz2bsKqIWZBnuIP1nmqubdHQEqFJyKA==', N'06cf4373-9548-45e1-bddc-9fde6a30ac83', NULL, 0, 0, NULL, 1, 0, N'jh@jh.com', N'Y')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'4b6dd2bc-6a21-480d-9249-034b7d1e76dc', N'radhika@kiwitech.com', 0, N'AMHPUMRxbTcpg3yFBKp+puumx785Yha0DTctS+MnO2LgN5jGGnhygc+lDK8/IT2Rgg==', N'20d528a2-2b48-47b9-a090-5e1ebc050765', NULL, 0, 0, NULL, 1, 0, N'radhika@kiwitech.com', N'Y')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'4f8d0f9a-e6f9-4a2d-8228-3486529687ab', N'prateek.kumar@kiwitech.com', 0, N'AMa5RkUMudQzvHGAMQiCwd7EfxlrmsS5EalMyow4167e5CTnOC3l1T1r3/9KNzZpWg==', N'96375396-0031-43a8-8914-643bd09c33ce', NULL, 0, 0, NULL, 1, 0, N'prateek.kumar@kiwitech.com', N'N')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'50f99aaf-3975-417f-adf8-fc41b671c3f2', N'navi@navi.com', 0, N'AFXbODhaA0xJrvARy7uWKtmoijeqRNO/snZfBOEPMoy8TBhsAG86OBj+u+7YKZK/lg==', N'59c6963a-138a-4dbb-a8e7-1f4d14b9020d', NULL, 0, 0, NULL, 1, 0, N'navi@navi.com', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'56d81941-25fa-4ac9-ae86-879b1d03e594', N'kl@kl.com', 0, N'AAnNGFMVAGdHSDkI41klFVFCM3CahjP9FhPnLW1uoW0NVX7UGANn2GhbrwxRsAIqAQ==', N'd4635dee-3ef3-471c-869d-65fb0de82883', NULL, 0, 0, NULL, 1, 0, N'kl@kl.com', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'5d7f9411-04cf-4431-9eb2-13efc37b6d33', N'shweta.sindhu@kiwitech.com', 0, NULL, N'9cbc5c60-b7e2-484d-8930-e5abf846f73e', NULL, 0, 0, NULL, 1, 0, N'shweta.sindhu@kiwitech.com', N'Y')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'5efeb250-156b-44da-a474-f50b55c5dcb0', N'shwfgsgndhu@kiwitech.com', 0, N'AC6rpQHi1UxRfOod2b4vWRyNGxtbEZmfuNrqGK+L9PB4aqnefzxoTNu+jbClmg4cgg==', N'85b29958-79b3-4d05-9e10-0de0c5907d57', NULL, 0, 0, NULL, 1, 0, N'shwfgsgndhu@kiwitech.com', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'6fcab4d1-8beb-469b-a801-cda2ffe6efb3', N'ravi.prince88@gmail.com', 0, N'APLOrXP/jiOQuY2GM8rEZ+6odY45hKhkQDdKaiMHERREB6bxsdIYoDwsL2dcWV3V8A==', N'39841449-8177-4244-a9b2-51d6036713da', NULL, 0, 0, NULL, 1, 0, N'ravi.prince88@gmail.com', N'N')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'735b57df-6a29-48d6-87d3-e851aa3245c0', N'navrit.singh@kiwitech.com', 0, N'APuecX7sL2PVSLvr+QHnGFNXsdIFqMaFLgevN1VcslYlMswu44aUGfAj+qMdSXXLIg==', N'04263e9c-2653-49c3-9fff-243d7d36faae', NULL, 0, 0, NULL, 1, 0, N'navrit.singh@kiwitech.com', N'Y')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'746ffe19-8552-4ef1-8138-3962cbc20575', N'ghf@ghf.com', 0, N'AAj6Kop5gHdwbHpm0dYP9YhfORdKHIeKTOdNyXqxPNyYL21Qs934zsRabmcDJHYO1A==', N'5c6ef349-bd7b-4b1b-ba6b-16ef4d3307e5', NULL, 0, 0, NULL, 1, 0, N'ghf@ghf.com', N'Y')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'7ba72509-927a-4f58-9d7f-a41e3660204d', N'sh@sh.com', 0, N'AHb0G/jJZtdtMkPzjaJLTqB09Jg2+VJaKTKIU0bPMevYWMUjkZjZNrw/AQtKBb+dCg==', N'70e3b8d5-3891-4ccb-9a40-f73fc2eeefe4', NULL, 0, 0, NULL, 1, 0, N'sh@sh.com', N'N')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'7bd93525-a288-43fd-84f8-40ec4b7c50bd', N'shweta.sindhu123@kiwitech.com', 0, NULL, N'fd19a647-60d3-4a05-8ca3-3d5ce6f0309f', NULL, 0, 0, NULL, 1, 0, N'shweta.sindhu123@kiwitech.com', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'7d3d080d-934a-49c7-9c5c-01fd9f29d8ea', N'shweta.sindhu123567@kiwitech.com', 0, N'AFMyApJH2slITa4orP0HwC5o4X3WirWz3Jm1xA/Opd6s41rgM5GOCzHHDEUB+ZeGFA==', N'67754a82-b62d-400c-b995-a5a0a652fce7', NULL, 0, 0, NULL, 1, 0, N'shweta.sindhu123567@kiwitech.com', N'Y')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'7db0fbe0-e140-4288-b43b-f0a892343043', N'sdf@sdf.com', 0, N'AIxx/T2MThkInfCONnwNuME8EVYeW2xgZM8J+nojRws4jh0LJVWT3619q4IKw0SxBQ==', N'ab31a6c2-29f8-4a5a-9295-368d1df3f3be', NULL, 0, 0, NULL, 1, 0, N'sdf@sdf.com', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'7fde72cd-c8ec-41bb-8dd4-3c67a9f54e95', N'23d@d.com', 0, N'AJLBKftpO34+OnNLn5gGcjztW/LVvcx+8fLwOW856WENTR2q7dAGQ1LME4bb+DF2pA==', N'86f0036b-0c64-4b9f-9b17-fabb860ea8e4', NULL, 0, 0, NULL, 1, 0, N'23d@d.com', N'N')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'92587a71-6a50-4a85-911f-789c97580446', N'pintu.sah@kiwitech.com', 0, NULL, N'6889d160-1bd3-4838-b8d4-44bec5a84a93', NULL, 0, 0, NULL, 1, 0, N'pintu.sah@kiwitech.com', N'Y')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'9e10f676-b5d0-4520-9930-d52102d42d9a', N'gh@ghd.com', 0, N'AAAkHE0bxJvZiYceJWaWx58CFHDFMVWo8n6JiCucQUxKzl7xk4pGVDtu3N01Ip/98A==', N'71b95517-78ef-4147-a934-e770d679bcda', NULL, 0, 0, NULL, 1, 0, N'gh@ghd.com', N'N')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'a5c9a386-95d2-4f47-bd11-853bbc993caa', N'jhd@jhs.com', 0, N'AJmEQ48u0YlVn22CKbgAt49cziwMn9jnPyN2iqUThZpNBNcMZQ7zh+j5k3iQYU6Nng==', N'bb8d61ce-cc6e-4629-ae4f-4490e4fb6b71', NULL, 0, 0, NULL, 1, 0, N'jhd@jhs.com', N'Y')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'b413ab1f-4cf8-485a-bfb2-00b0236a5a41', N'rohit.kesarwani@kiwitech.com', 0, N'APukZHLCwOBF61xTH2DVwep7awh9H4y+pqe9zbCpWn0hJKmV0TrP9t5hIgKW8Bjm4g==', N'22b517d8-9982-473c-8b10-a614e451ff87', NULL, 0, 0, NULL, 1, 0, N'rohit.kesarwani@kiwitech.com', N'N')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'b68f6b28-3465-4b34-a75d-2958da20ee7b', N'ns@yahoo.com', 0, N'ADdzG18GBbKzVmTwQFwTUyV7vGWZivn9PRDHSoMQi8vDRbJeEKaJ5oMKraeMh7Oweg==', N'c57eb773-924c-4da4-bbdf-9f0472804795', NULL, 0, 0, NULL, 1, 0, N'ns@yahoo.com', N'Y')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'bb9d07ec-0a08-4646-bc11-cb3e7f6480a3', N'ravi.prakash@kiwitech.com', 0, N'AGE3VbOO3VISCuEXhxiqA3PE349wjYIwlf6IzNPJg6awMxv9xDr4MTWGtohy+66bIg==', N'58a2ee29-a341-46f3-ba5e-91ce5d3f2b4f', NULL, 0, 0, NULL, 1, 0, N'ravi.prakash@kiwitech.com', N'Y')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'bea62686-6748-494d-9898-b9d2d6f85cfa', N'lw@lw.com', 0, N'ABEpiI5T9ve5uZ4d+AzWJ//8Oi8JvYPlkiTTgIU295mRckyziELZ46R/a0lCXl/aQA==', N'c11526e9-0d4a-4e47-a2cc-6c0ccd680622', NULL, 0, 0, NULL, 1, 0, N'lw@lw.com', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'c09a9cd0-22cf-46f1-a40e-2c9c1f30a7c9', N'kumar.prat@gmail.com', 0, N'AOv+oXUaC3huGT60G8MslvXhTtTLsYkYjqAuKI1avOs8pi2+7MZGPQTzGE1u9e7eyQ==', N'9bd056a0-2f58-4bf0-8c2e-d117389f6d24', NULL, 0, 0, NULL, 1, 0, N'kumar.prat@gmail.com', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'c10cde59-184b-4244-9a7c-5b209e38eaae', N'saroosh@eventcombo.com', 0, N'ABJtrIrpMTgBHTJJ40GQnC77G76moVeIvKYYVba4BYBzSSgND+7ZNi8ZHfIQC6kiHQ==', N'c4170f35-4c1a-4b90-9b40-c1409425a06f', NULL, 0, 0, NULL, 1, 0, N'saroosh@eventcombo.com', N'N')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'c91be264-0cca-4ade-91cd-c55ca1a184e8', N'Navrit.Singh@kiwi.com', 0, N'AOVI6vVD8TgOqmCMMxb65iKYtsMt56fDC6tSYJ5EznWwKGxmPZStghsyZHNRaQYDww==', N'10991409-57f3-4808-b414-4fda3dba90b8', NULL, 0, 0, NULL, 1, 0, N'Navrit.Singh@kiwi.com', N'N')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Navi@g.com', 0, N'AChtT8o2E46ipcYYm1zYkRDO/v0anYcD2bBsYgT3GGfchDnWg9Qt4hyyQ00qwycu+g==', N'37ae5641-4668-4394-994d-3c14fcb14e66', NULL, 0, 0, NULL, 1, 0, N'Navi@g.com', N'Y')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'e5ac394c-c3bf-4f3b-aa29-feec9ee37629', N'just@d.com', 0, N'ABmv4SJ7OgerLSrUC+g87DOD9qEbFk8mibt+8Wv+6mWiABvqc8xO0ZfJoihLycEXYw==', N'bf755a47-7928-4aa5-8965-c1e680954355', NULL, 0, 0, NULL, 1, 0, N'just@d.com', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'eb7d6159-2475-4cd1-8a75-d5c986d3161a', N'mayank.srivastava@kiwitech.com', 0, N'AHpgqY7efLQ8+p9ykSNr/PZq1Pl3ZNKqmNh1doKm+OXFXfrio8gLSjRb1//fBPSLPA==', N'bf9da4ee-98a6-4554-bb8b-9d8222010a02', NULL, 0, 0, NULL, 1, 0, N'mayank.srivastava@kiwitech.com', N'N')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'f3dff988-adb7-440d-9f17-8e37f108e131', N'hj@hj.com', 0, N'AFdIRaR+UDH2XfTKcyL9sd7WcUdIkzs06BEDlVd9FNbXeFdlbyeHgnYfedkSG8xrfQ==', N'0f32ccc8-b9a5-4d9b-a2e9-37b8abb057bb', NULL, 0, 0, NULL, 1, 0, N'hj@hj.com', N'N')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'f4ee24cd-0194-45eb-ae30-0c92e151a7dc', N'd@d.com', 0, N'AKipGcVpMb89zzRR8p1/qG/4RlU6HmZ2tvI2vp60w33LPDqe+obA8UeYMSLOiF/Vqg==', N'532b3cbf-1e26-405e-a3ae-a0d44540e460', NULL, 0, 0, NULL, 1, 0, N'd@d.com', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'f59003aa-ef8d-4c6f-a92a-ab4fe081c6c2', N'just.shweta@k.com', 0, N'AJsCJEBdjyE6cZH3bh5hsvmM1vueTE8+rr2HdnPXCHOuVNRLEnRuv5P+OEq3pXMz3Q==', N'0831d943-49cd-4c1a-a24c-74230bbc082f', NULL, 0, 0, NULL, 1, 0, N'just.shweta@k.com', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'f8aa26e4-31d6-4cf4-8cec-708913931fcd', N'kj@kjh.com', 0, N'ALr2rxtwavZ7wRnYwkfP+4CxVvSc9yxL+9sd4TNhBoEcHeq//5WIouYEVakWaZrR1A==', N'ffec69b7-d2e4-4c90-8642-70d807bf8dd6', NULL, 0, 0, NULL, 1, 0, N'kj@kjh.com', N'Y')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'fdad64ff-80f5-4a1e-96af-9948fece5967', N'pinto@g.com', 0, N'AMw39yaJVUHdSd4dexJoIhSqVWrCPgUFO/u8WILvpCo/IpoK7bmHlkLNXMLxe6B3rg==', N'37fde9fd-da62-4895-a43d-026a9e7d4132', NULL, 0, 0, NULL, 1, 0, N'pinto@g.com', N'Y')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'ffc2e2de-572f-4361-82df-02ae9af3a753', N'just.s@h.com', 0, N'ACeb/TTNQQikcryqCNsPs2XMK2rzDl2tsvK0ZXN2JQhpGb+EZcHEBOrwlfYnqFm1NQ==', N'1e5f5f40-77b2-4aec-bd2e-c1c595dd4e30', NULL, 0, 0, NULL, 1, 0, N'just.s@h.com', NULL)
GO
SET IDENTITY_INSERT [dbo].[Country] ON 

GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (1, N' United States                         ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (2, N' Afghanistan                           ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (3, N' Albania                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (4, N' Algeria                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (5, N' American Samoa                        ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (6, N' Andorra                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (7, N' Angola                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (8, N' Anguilla                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (9, N' Antigua and Barbuda                   ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (10, N' Argentina                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (11, N' Armenia                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (12, N' Aruba                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (13, N' Australia                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (14, N' Austria                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (15, N' Azerbaijan                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (16, N' The Bahamas                           ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (17, N' Bahrain                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (18, N' Bangladesh                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (19, N' Barbados                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (20, N' Belarus                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (21, N' Belgium                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (22, N' Belize                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (23, N' Benin                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (24, N' Bermuda                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (25, N' Bhutan                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (26, N' Bolivia                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (27, N' Bosnia and Herzegovina                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (28, N' Botswana                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (29, N' Brazil                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (30, N' Brunei                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (31, N' Bulgaria                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (32, N' Burkina Faso                          ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (33, N' Burundi                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (34, N' Cambodia                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (35, N' Cameroon                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (36, N' Canada                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (37, N' Cape Verde                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (38, N' Cayman Islands                        ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (39, N' Central African Republic              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (40, N' Chad                                  ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (41, N' Chile                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (42, N' People Republic of China            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (43, N' Republic of China                     ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (44, N' Christmas Island                      ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (45, N' Cocos (Keeling) Islands               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (46, N' Colombia                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (47, N' Comoros                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (48, N' Congo                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (49, N' Cook Islands                          ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (50, N' Costa Rica                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (51, N' Cote dIvoire                         ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (52, N' Croatia                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (53, N' Cuba                                  ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (54, N' Cyprus                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (55, N' Czech Republic                        ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (56, N' Denmark                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (57, N' Djibouti                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (58, N' Dominica                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (59, N' Dominican Republic                    ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (60, N' Ecuador                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (61, N' Egypt                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (62, N' El Salvador                           ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (63, N' Equatorial Guinea                     ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (64, N' Eritrea                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (65, N' Estonia                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (66, N' Ethiopia                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (67, N' Falkland Islands                      ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (68, N' Faroe Islands                         ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (69, N' Fiji                                  ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (70, N' Finland                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (71, N' France                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (72, N' French Polynesia                      ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (73, N' Gabon                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (74, N' The Gambia                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (75, N' Georgia                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (76, N' Germany                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (77, N' Ghana                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (78, N' Gibraltar                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (79, N' Greece                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (80, N' Greenland                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (81, N' Grenada                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (82, N' Guadeloupe                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (83, N' Guam                                  ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (84, N' Guatemala                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (85, N' Guernsey                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (86, N' Guinea                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (87, N' Guinea-Bissau                         ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (88, N' Guyana                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (89, N' Haiti                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (90, N' Honduras                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (91, N' Hong Kong                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (92, N' Hungary                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (93, N' Iceland                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (94, N' India                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (95, N' Indonesia                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (96, N' Iran                                  ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (97, N' Iraq                                  ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (98, N' Ireland                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (99, N' Israel                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (100, N' Italy                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (101, N' Jamaica                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (102, N' Japan                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (103, N' Jersey                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (104, N' Jordan                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (105, N' Kazakhstan                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (106, N' Kenya                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (107, N' Kiribati                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (108, N' North Korea                           ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (109, N' South Korea                           ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (110, N' Kosovo                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (111, N' Kuwait                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (112, N' Kyrgyzstan                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (113, N' Laos                                  ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (114, N' Latvia                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (115, N' Lebanon                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (116, N' Lesotho                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (117, N' Liberia                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (118, N' Libya                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (119, N' Liechtenstein                         ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (120, N' Lithuania                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (121, N' Luxembourg                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (122, N' Macau                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (123, N' Macedonia                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (124, N' Madagascar                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (125, N' Malawi                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (126, N' Malaysia                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (127, N' Maldives                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (128, N' Mali                                  ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (129, N' Malta                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (130, N' Marshall Islands                      ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (131, N' Martinique                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (132, N' Mauritania                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (133, N' Mauritius                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (134, N' Mayotte                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (135, N' Mexico                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (136, N' Micronesia                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (137, N' Moldova                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (138, N' Monaco                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (139, N' Mongolia                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (140, N' Montenegro                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (141, N' Montserrat                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (142, N' Morocco                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (143, N' Mozambique                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (144, N' Myanmar                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (145, N' Nagorno-Karabakh                      ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (146, N' Namibia                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (147, N' Nauru                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (148, N' Nepal                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (149, N' Netherlands                           ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (150, N' Netherlands Antilles                  ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (151, N' New Caledonia                         ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (152, N' New Zealand                           ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (153, N' Nicaragua                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (154, N' Niger                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (155, N' Nigeria                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (156, N' Niue                                  ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (157, N' Norfolk Island                        ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (158, N' Turkish Republic of Northern Cyprus   ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (159, N' Northern Mariana                      ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (160, N' Norway                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (161, N' Oman                                  ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (162, N' Pakistan                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (163, N' Palau                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (164, N' Palestine                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (165, N' Panama                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (166, N' Papua New Guinea                      ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (167, N' Paraguay                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (168, N' Peru                                  ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (169, N' Philippines                           ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (170, N' Pitcairn Islands                      ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (171, N' Poland                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (172, N' Portugal                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (173, N' Puerto Rico                           ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (174, N' Qatar                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (175, N' Romania                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (176, N' Russia                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (177, N' Rwanda                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (178, N' Saint Barthelemy                      ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (179, N' Saint Helena                          ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (180, N' Saint Kitts and Nevis                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (181, N' Saint Lucia                           ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (182, N' Saint Martin                          ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (183, N' Saint Pierre and Miquelon             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (184, N' Saint Vincent and the Grenadines      ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (185, N' Samoa                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (186, N' San Marino                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (187, N' Sao Tome and Principe                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (188, N' Saudi Arabia                          ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (189, N' Senegal                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (190, N' Serbia                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (191, N' Seychelles                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (192, N' Sierra Leone                          ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (193, N' Singapore                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (194, N' Slovakia                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (195, N' Slovenia                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (196, N' Solomon Islands                       ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (197, N' Somalia                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (198, N' Somaliland                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (199, N' South Africa                          ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (200, N' South Ossetia                         ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (201, N' Spain                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (202, N' Sri Lanka                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (203, N' Sudan                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (204, N' Suriname                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (205, N' Svalbard                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (206, N' Swaziland                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (207, N' Sweden                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (208, N' Switzerland                           ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (209, N' Syria                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (210, N' Taiwan                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (211, N' Tajikistan                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (212, N' Tanzania                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (213, N' Thailand                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (214, N' Timor-Leste                           ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (215, N' Togo                                  ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (216, N' Tokelau                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (217, N' Tonga                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (218, N' Transnistria Pridnestrovie            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (219, N' Trinidad and Tobago                   ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (220, N' Tristan da Cunha                      ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (221, N' Tunisia                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (222, N' Turkey                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (223, N' Turkmenistan                          ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (224, N' Turks and Caicos Islands              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (225, N' Tuvalu                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (226, N' Uganda                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (227, N' Ukraine                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (228, N' United Arab Emirates                  ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (229, N' United Kingdom                        ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (230, N' Uruguay                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (231, N' Uzbekistan                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (232, N' Vanuatu                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (233, N' Vatican City                          ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (234, N' Venezuela                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (235, N' Vietnam                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (236, N' British Virgin Islands                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (237, N' US Virgin Islands                     ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (238, N' Wallis and Futuna                     ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (239, N' Western Sahara                        ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (240, N' Yemen                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (241, N' Zambia                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (242, N' Zimbabwe                              ')
GO
SET IDENTITY_INSERT [dbo].[Country] OFF
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (N'1                                                                                                                                                     ', N'UserEmailID')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (N'10                                                                                                                                                    ', N'EventAddressID')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (N'11                                                                                                                                                    ', N'TicketOrderNumberID')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (N'12                                                                                                                                                    ', N'DealOrderNumberID')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (N'13                                                                                                                                                    ', N'ResetPwdUrl')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (N'14                                                                                                                                                    ', N'EventOrderNO')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (N'15                                                                                                                                                    ', N'EventBarcodeId')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (N'16                                                                                                                                                    ', N'EventTitleId')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (N'17                                                                                                                                                    ', N'EventImageId')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (N'18                                                                                                                                                    ', N'Tickettype')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (N'19                                                                                                                                                    ', N'TicketPrice')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (N'2                                                                                                                                                     ', N'UserFirstNameID')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (N'20                                                                                                                                                    ', N'TicketOrderDateId')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (N'21                                                                                                                                                    ', N'EventTimeZone')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (N'22                                                                                                                                                    ', N'Ticketname')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (N'23                                                                                                                                                    ', N'EventQrCode')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (N'3                                                                                                                                                     ', N'UserLastNameID')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (N'4                                                                                                                                                     ', N'EventNameID')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (N'5                                                                                                                                                     ', N'EventStartDateID')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (N'6                                                                                                                                                     ', N'EventEndDateID')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (N'7                                                                                                                                                     ', N'EventStartTimeID')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (N'8                                                                                                                                                     ', N'EventEndTimeID')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (N'9                                                                                                                                                     ', N'EventVenueID')
GO
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag]) VALUES (N'2ab53970-a668-4c07-ac73-3133a5447150', N'E-Ticket Template', N' ¶¶UserEmailID¶¶', NULL, NULL, N'Your Tickets for  ¶¶EventTitleId¶¶ Eventcombo Order no:  ¶¶EventOrderNO¶¶, ¶¶EventStartDateID¶¶. ¶¶EventVenueID¶¶', N'<p>&nbsp;</p>

<table border="0" style="width:700px">
	<tbody>
		<tr>
			<td colspan="2">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &para;&para;EventOrderNO&para;&para;</td>
		</tr>
		<tr>
			<td><img alt="" src="http://eventcombo.kiwireader.com/Images/logo_vertical.gif" style="height:479px; width:101px" /></td>
			<td>
			<table border="0">
				<tbody>
					<tr>
						<td>&nbsp;</td>
						<td>&para;&para;EventBarcodeId&para;&para;&nbsp;</td>
					</tr>
				</tbody>
			</table>
			&para;&para;UserFirstNameID&para;&para;

			<p>&nbsp;</p>

			<table border="0">
				<tbody>
					<tr>
						<td>&nbsp; &nbsp; &para;&para;EventQrCode&para;&para; &nbsp;</td>
						<td>
						<p>&para;&para;EventTitleId&para;&para;</p>

						<p>Start date:&para;&para;EventStartDateID&para;&para; Start time: &para;&para;EventStartTimeID&para;&para;</p>

						<p>Event Frequency:</p>

						<p>&para;&para;EventVenueID&para;&para;</p>

						<p>&nbsp;</p>
						</td>
					</tr>
					<tr>
						<td colspan="2">
						<p>Ticket type: &para;&para;Tickettype&para;&para;&nbsp; &para;&para;TicketPrice&para;&para;</p>
						</td>
					</tr>
					<tr>
						<td colspan="2">
						<p>Order &nbsp;&para;&para;EventOrderNO&para;&para;&nbsp;Ordered &nbsp;by &para;&para;UserFirstNameID&para;&para;&nbsp;on &para;&para;TicketOrderDateId&para;&para;&nbsp;</p>
						</td>
					</tr>
					<tr>
						<td colspan="2">
						<h1>The Fine Print</h1>

						<p>Limit 1 per person, may buy 2 additional as gifts. Limit 1 per table. Valid only for option purchased. Dine-in only. Not valid for shakes, fish &amp; chips, or lahori charga. Tax and gratuity not included.</p>

						<h1>Rules</h1>

						<p>These are the restrictions that apply to every Groupon voucher (unless the Fine Print specifies an exception) Value of the Groupon Voucher This voucher has two separate values: (a) the amount paid; and (b) the promotional value. The amount paid will never expire and may be applied toward any goods or services offered by the merchant if the original goods or services specified on the voucher are no longer available. The promotional value is the additional value beyond the amount paid, and it will expire on the expiration date above (unless prohibited by law).<br />
						<br />
						Not redeemable for cash (unless required by law). Doesn&#39;t cover tax or gratuity. Promotional value can&#39;t be combined with other offers. Not reloadable. Unauthorized reproduction, resale, modification, or trade prohibited. Silk Hookah Lounge &amp; Grill is the issuer of this voucher. Purchase, use, or acceptance of this voucher constitutes acceptance of these terms. This voucher is transferable. For more information, visit <a href="http://www.groupon.com/terms">http://www.groupon.com/terms.</a></p>
						</td>
					</tr>
				</tbody>
			</table>
			</td>
		</tr>
	</tbody>
</table>

<p>&nbsp;</p>
', N'shweta.sindhu@kiwitech.com', N'eticket')
GO
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag]) VALUES (N'2e223022-9c7e-45e0-8e5b-6c8776762021', N'Welcome Template', N' ¶¶UserEmailID¶¶', N'just.shweta29@gmail.com, ¶¶UserEmailID¶¶', N'navrit.singh@kiwitech.com, ¶¶UserEmailID¶¶', N'Welcome to Eventcombo   ¶¶UserEmailID¶¶, ¶¶DealOrderNumberID¶¶', N'<p>Hi ,</p>

<p>Thank you &para;&para;Ticketname&para;&para; for choosing eventcombo.Lets get started. &para;&para;EventStartDateID&para;&para;&nbsp;,,,&para;&para;EventNameID&para;&para;</p>
', N'welcome@eventcombo.com', N'email_welcome')
GO
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag]) VALUES (N'405f9cc8-782b-4b87-8406-3a3c0ae4c60d', N'', N' ¶¶UserEmailID¶¶', N'just.shweta29@gmail.com', NULL, N'Reset Password ', N'<p><img alt="eventcombo" src="http://eventcombo.kiwireader.com/Images/logo_vertical.png" style="float:left; height:400px; margin:2px; width:86px" />&nbsp;Hello&nbsp; &para;&para;UserFirstNameID&para;&para; ,</p>

<p>We have recieved a request to reset your password for your account :&nbsp;&para;&para;UserEmailID&para;&para; .We&#39;re here to help!</p>

<p>Click on the link below to reset and create a new password:</p>

<p>&para;&para;ResetPwdUrl&para;&para;</p>

<p>Set a new Password if you didn&#39;t ask to change your password,don&#39;t worry!Your password is still safe and you can delete this email.</p>

<p>Best ,</p>

<p>The Eventcombo Team.</p>

<p>&nbsp;</p>
', N'shweta.sindhu@kiwitech.com', N'email_lost_pwd')
GO
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag]) VALUES (N'aa3f1945-418b-4fc8-8a6d-6c60741fa70a', N'', N' ¶¶UserEmailID¶¶', NULL, NULL, N'Your Account Info is successfuly updated', N'<p>Hi&nbsp; &para;&para;UserFirstNameID&para;&para;,</p>

<p>You Account info is updated successfully.</p>
', N'shweta.sindhu@kiwitech.com', N'acc_info_update')
GO
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag]) VALUES (N'edfb6c74-43f8-45e7-86d1-d19aaf7f5e37', N'', N' ¶¶UserEmailID¶¶', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag]) VALUES (N'fd04710c-8d72-43e5-ab83-c33da92adc15', N'', N' ¶¶UserEmailID¶¶', NULL, NULL, N'Your Password has Reset', N'<p>Hi&nbsp; &para;&para;UserFirstNameID&para;&para; ,</p>

<p>Your password has been successfully reset.</p>

<p>&nbsp;</p>

<p>&nbsp;</p>

<p>With Best Wishes,</p>

<p>The Eventcombo Team.</p>
', N'shweta.sindhu@kiwitech.com', N'acc_pwd_set')
GO
SET IDENTITY_INSERT [dbo].[Event] ON 

GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (304, 1, 2, 21, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Navrit Testing Variable charges Prb', N'N', N'N', N'N', N'Event Desc
', N'Public', N'N', N'N', NULL, NULL, N'N', N'Live', NULL, NULL, NULL, N'1', NULL, NULL, NULL, 0, N'N', NULL, N'N', N'N', N'Y', N'Variables ', N'R', CAST(0x0000A57800DEA5B8 AS DateTime), NULL, N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (305, 1, 2, 21, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Navreet Testing without variable charges', N'N', N'N', N'N', N'Event Desc
', N'Public', N'N', N'N', NULL, NULL, N'N', N'Live', NULL, NULL, NULL, N'1', NULL, NULL, NULL, 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A57800ED2946 AS DateTime), NULL, N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (306, 1, 1, 1, N'24556b52-131a-46ad-b6a8-2897417b40c0', N'dffdg', N'Y', N'Y', N'Y', N'

', N'Public', N'N', N'N', NULL, NULL, N'Y', N'Live', NULL, NULL, NULL, N'20', NULL, NULL, N'Single', 0, N'Y', NULL, N'Y', N'Y', N'Y', N'sdf', N'R', CAST(0x0000A578010081F7 AS DateTime), NULL, N'Y', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (307, 1, 1, 1, N'24556b52-131a-46ad-b6a8-2897417b40c0', N'dffdg', N'Y', N'Y', N'Y', N'

', N'Public', N'N', N'N', NULL, NULL, N'Y', N'Live', NULL, NULL, NULL, N'20', NULL, NULL, N'Single', 0, N'Y', NULL, N'Y', N'Y', N'Y', N'sdf', N'R', NULL, CAST(0x0000A5780106B7E0 AS DateTime), N'Y', 306)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (308, 1, 1, 1, N'24556b52-131a-46ad-b6a8-2897417b40c0', N'this is test', N'Y', N'Y', N'Y', N'

', N'Public', N'N', N'N', NULL, NULL, N'Y', N'Save', NULL, NULL, NULL, N'15', NULL, NULL, N'Multiple', 0, N'Y', NULL, N'N', N'N', N'Y', N'23', N'R', CAST(0x0000A5780108C588 AS DateTime), NULL, N'Y', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (309, 2, 2, 4, N'', N'dfgdfg', N'N', N'N', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'Y', N'Save', NULL, NULL, NULL, N'3', NULL, NULL, N'Single', 0, N'Y', NULL, N'Y', N'Y', N'Y', N'gh', N'O', CAST(0x0000A578010D4DF5 AS DateTime), CAST(0x0000A57801192898 AS DateTime), N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (310, 2, 1, 1, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'this is test', N'Y', N'Y', N'Y', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Save', NULL, NULL, NULL, N'7', NULL, NULL, N'Single', 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A5780119AAC6 AS DateTime), CAST(0x0000A57801430BDE AS DateTime), N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (311, 42, 2, 4, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Navreet testing Confirmation Page', N'N', N'Y', N'N', N'Event Desc
', N'Public', N'N', N'N', NULL, NULL, N'N', N'Live', NULL, NULL, NULL, N'1', NULL, NULL, NULL, 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A578011A7CFC AS DateTime), NULL, N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (312, 2, 1, 1, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'this is test', N'Y', N'Y', N'Y', N'sdf
', N'Public', N'N', N'N', NULL, NULL, N'N', N'Live', NULL, NULL, NULL, N'13', NULL, NULL, N'Online', 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A578011A7CB5 AS DateTime), CAST(0x0000A578011B2404 AS DateTime), N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (313, 1, 1, 2, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'this is test', N'Y', N'Y', N'Y', N'

', N'Public', N'N', N'N', NULL, NULL, N'Y', N'Save', NULL, NULL, NULL, N'19', NULL, NULL, N'PastLocation', 11, N'Y', NULL, N'Y', N'Y', N'N', NULL, NULL, CAST(0x0000A578011AEAC0 AS DateTime), NULL, N'Y', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (314, 2, 1, 1, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Navrit Singh Grewal', N'N', N'N', N'N', N'Event Desc
', N'Public', N'N', N'N', NULL, NULL, N'N', N'Live', NULL, NULL, NULL, N'13', NULL, NULL, NULL, 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A57801206966 AS DateTime), NULL, N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (315, 42, 10, 25, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Navreet Singh Grewal Testing Donation', N'N', N'N', N'N', N'Event Desc
', N'Public', N'N', N'N', NULL, NULL, N'N', N'Live', NULL, NULL, NULL, N'10', NULL, NULL, NULL, 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A5780121B58E AS DateTime), NULL, N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (316, 1, 1, 1, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'dsff', N'N', N'N', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Save', NULL, NULL, NULL, N'1', NULL, NULL, N'Single', 0, N'N', NULL, N'N', N'N', N'Y', N'jkk', N'O', CAST(0x0000A5780125E040 AS DateTime), CAST(0x0000A57E00F96341 AS DateTime), N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (317, 1, 1, 1, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'dsf', N'N', N'N', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Save', NULL, NULL, NULL, N'1', NULL, NULL, N'Single', 0, N'N', NULL, N'N', N'N', N'Y', N'dfsdf', N'O', CAST(0x0000A57801268831 AS DateTime), CAST(0x0000A57E00FAE533 AS DateTime), N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (318, 1, 1, 1, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'this', N'N', N'N', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Save', NULL, NULL, NULL, N'1', NULL, NULL, N'Single', 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A5780127FDCA AS DateTime), NULL, N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (319, 2, 1, 1, N'2dd29514-6025-4649-8d71-5ce2e117fa69', N'sdfsdf', N'N', N'N', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Save', NULL, NULL, NULL, N'1', NULL, NULL, N'Single', 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A5780137EABD AS DateTime), NULL, N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (320, 2, 1, 1, N'34b7f0f9-8776-4628-a690-8efa1a2bd425', N'fdgdfg', N'N', N'N', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Save', NULL, NULL, NULL, N'1', NULL, NULL, NULL, 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A578013DFE4C AS DateTime), CAST(0x0000A578013F526C AS DateTime), N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (321, 2, 2, 0, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'fgfdg', N'N', N'N', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Save', NULL, NULL, NULL, N'1', NULL, NULL, N'Single', 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A5780140FB31 AS DateTime), CAST(0x0000A578014152AE AS DateTime), N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (322, 1, 1, 1, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'this is test', N'N', N'N', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Save', NULL, NULL, NULL, N'1', NULL, NULL, NULL, 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A57D00EDCF78 AS DateTime), NULL, N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (323, 1, 1, 2, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'This is past location test', N'N', N'N', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Save', NULL, NULL, NULL, N'1', NULL, NULL, N'PastLocation', 23, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A57D00F0F9DC AS DateTime), CAST(0x0000A57D00FF3E61 AS DateTime), N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (324, 42, 10, 25, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Navreet Testing Event Listing', N'N', N'N', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'Y', N'Live', NULL, NULL, NULL, N'1', NULL, NULL, N'Single', 0, N'Y', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A57D00FE881F AS DateTime), NULL, N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (325, 42, 10, 25, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Navreet Testing Event Listing Modify', N'N', N'N', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'Y', N'Live', NULL, NULL, NULL, N'1', NULL, NULL, N'Single', 0, N'Y', NULL, N'N', N'N', N'N', NULL, NULL, NULL, CAST(0x0000A57D0100ABC9 AS DateTime), N'N', 324)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (326, 42, 10, 25, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Navreet Testing Event Listing Modify', N'N', N'N', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'Y', N'Live', NULL, NULL, NULL, N'1', NULL, NULL, N'Single', 0, N'Y', NULL, N'N', N'N', N'N', NULL, NULL, NULL, CAST(0x0000A57D0100C613 AS DateTime), N'N', 324)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (327, 42, 10, 25, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Navreet Testing Event Listing Modify', N'N', N'N', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'Y', N'Live', NULL, NULL, NULL, N'1', NULL, NULL, N'Single', 0, N'Y', NULL, N'N', N'N', N'N', NULL, NULL, NULL, CAST(0x0000A57D0100D801 AS DateTime), N'N', 324)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (328, 42, 10, 25, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Navreet Testing Event Listing Modify', N'N', N'N', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'Y', N'Live', NULL, NULL, NULL, N'1', NULL, NULL, N'Single', 0, N'Y', NULL, N'N', N'N', N'N', NULL, NULL, NULL, CAST(0x0000A57D0100F014 AS DateTime), N'N', 324)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (329, 42, 10, 25, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Navreet Testing Event Listing Modify again', N'N', N'N', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'Y', N'Live', NULL, NULL, NULL, N'1', NULL, NULL, N'Single', 0, N'Y', NULL, N'N', N'N', N'N', NULL, NULL, NULL, CAST(0x0000A57E00FCCC58 AS DateTime), N'N', 324)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (330, 42, 1, 2, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Navreet Testing Payment Page', N'N', N'N', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Save', NULL, NULL, NULL, N'1', NULL, NULL, N'Single', 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A57E011B0B72 AS DateTime), NULL, N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (331, 1, 1, 1, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'this is test', N'Y', N'Y', N'Y', N'this is siisnss d
', N'Public', N'N', N'N', NULL, NULL, N'N', N'Live', NULL, NULL, NULL, N'70', NULL, NULL, NULL, 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A58500D0DCBC AS DateTime), NULL, N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (332, 1, 2, 4, N'1bd462fd-9fb2-45c9-941e-146b3a436f48', N'dfgdfg', N'N', N'N', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Save', NULL, NULL, NULL, N'1', NULL, NULL, NULL, 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A58600C49334 AS DateTime), NULL, N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (333, 58, 2, 21, N'24556b52-131a-46ad-b6a8-2897417b40c0', N'gfgfgfhgfh', N'N', N'N', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Save', NULL, NULL, NULL, N'1', NULL, NULL, N'', 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A58601472B12 AS DateTime), CAST(0x0000A586014735F2 AS DateTime), N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (334, 1, 1, 1, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'this is test', N'N', N'N', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Save', NULL, NULL, NULL, N'1', NULL, NULL, NULL, 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A5870101BF51 AS DateTime), NULL, N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (335, 42, 2, 4, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'private test shweta', N'Y', N'Y', N'Y', N'

', N'Private', N'N', N'N', N'shweta', NULL, N'Y', N'Save', NULL, NULL, NULL, N'17', NULL, NULL, N'Single', 0, N'Y', NULL, N'N', N'N', N'Y', N'xcv', N'O', CAST(0x0000A5870103D045 AS DateTime), CAST(0x0000A58701048649 AS DateTime), N'N', 0)
GO
SET IDENTITY_INSERT [dbo].[Event] OFF
GO
SET IDENTITY_INSERT [dbo].[Fee_Structure] ON 

GO
INSERT [dbo].[Fee_Structure] ([FeeId], [FeeType], [FeeAmount]) VALUES (1, N'EC Fee', CAST(3 AS Numeric(18, 0)))
GO
INSERT [dbo].[Fee_Structure] ([FeeId], [FeeType], [FeeAmount]) VALUES (2, N'Customer Fee', CAST(3 AS Numeric(18, 0)))
GO
SET IDENTITY_INSERT [dbo].[Fee_Structure] OFF
GO
SET IDENTITY_INSERT [dbo].[Messages] ON 

GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1, N'MyAccount', N'MyAccountEmailRequiredUI', N'Email Required.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (2, N'MyAccount', N'MyAccountEmailValidationUI', N'Invalid Email .')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (4, N'MyAccount', N'MyAccountSuccessInitSY', N'Updated Successsfully')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (5, N'MyAccount', N'MyAccountSuccessEmailSY', N'Email  changed successfully.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (6, N'MyAccount', N'MyAccountSuccessPasswordSY', N'Password changed successfully.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (7, N'Login', N'LoginEmailValidationUI', N'Invalid Email .')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (8, N'Login', N'LoginPasswordValidationUI', N'Invalid Password.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (9, N'Login', N'LoginEmailRequiredUI', N'Email  Required.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (10, N'Login', N'LoginEmailNotExistSy', N'Email  not present in database.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (11, N'Signup', N'SignupEmailValidationUI', N'Invalid Email .')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (12, N'Signup', N'SignupPasswordValidationUI', N'Invalid Password.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (13, N'Signup', N'SignupEmailAlreadyExistSY', N'Email Already Exist.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1011, N'TicketPayment', N'TenMinWindowExpires', N'Sorry, you took too long and we have to release your order. Please try again.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1012, N'Login', N'LoginPwdNotExistSy', N'Password not matching!Use forgot pasword!')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1013, N'MyAccount', N'MyAccountFnameRequiredUI', N'Please Enter First name.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1015, N'MyAccount', N'MyAccountPasswordValidationUI', N'Invalid Password.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1018, N'MyAccount', N'MyAccountDateValidationUI', N'Invalid Date.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1019, N'MyAccount', N'MyAccountZipValidationUI', N'Invalid Zip Code.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1020, N'MyAccount', N'MyAccountImagesizeValidationUI', N'Image More than 2 MB not allowed.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1021, N'MyAccount', N'MyAccountImageCountValidationUI', N'Only 1 image allowed.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1022, N'MyAccount', N'MyAccountEmailmatchValidationSy', N'Email and email verification doesn''t match.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1023, N'MyAccount', N'MyAccountPwdmatchValidationSy', N'New password and confirm new password doesn''t match.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1024, N'MyAccount', N'MyAccountPwdValidationSys', N'Invalid current password.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1025, N'MyAccount', N'MyAccountEmailAlreadyExistSY', N'Email already exist in our system choose different email')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1026, N'ForgotPassword', N'FrgtpwdEmailValidationUI', N'Invalid Email .')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1027, N'ForgotPassword', N'FrgtpwdEmailValidationSy', N'Email Not Found in our Database')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1028, N'ForgotPassword', N'ForgotPwdSuccessInitSY', N'Please check your email for password set link')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1029, N'ResetPassword', N'PwdResetPwdValidationUI', N'Invalid Password.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1030, N'ResetPassword', N'PwdResetPwdValidationSys', N'Password and confirm password doesn''t match')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1031, N'ResetPassword', N'PwdResetSuccessInitSY', N'Password reset successfully.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1032, N'CreateEvent', N'CreateEventTitileUI', N'Please enter the name of event.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1033, N'CreateEvent', N'CreateEventOrganizerUI', N'Please enter organizer name.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1034, N'CreateEvent', N'CreateEventtypeUI', N'Please select type of event.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1035, N'CreateEvent', N'CreateEventCategoryUI', N'Please select category of event')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1036, N'CreateEvent', N'CreateEventHighlightFieldsUI', N'Please fix the highlighted fields')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1037, N'CreateEvent', N'CreateEventvariabledescUI', N'Please enter description for variable charges')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1038, N'CreateEvent', N'CreateEventPwdUI', N'Please enter password for view event')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1039, N'CreateEvent', N'CreateEventCompareDateUI', N'Start Date can not be greater than End Date')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1040, N'CreateEvent', N'CreateEventInvalidTimeUI', N'Invalid time format')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1041, N'CreateEvent', N'CreateEventInvalidDateUI', N'Invalid date format')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1042, N'CreateEvent', N'CreateEventValidatevenueUI', N'Please enter valid venue name')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1043, N'CreateEvent', N'CreateEventurlexistUI', N'Event Url already exists.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1044, N'Messages', N'MessageReqUI', N'All Fields are required')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1045, N'Messages', N'MessageSuccSY', N'All Messages are saved successfully')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1046, N'MyAccount', N'MyAccountImagestypeValidationUI', N'Only files with extension jpg,.png,.jpeg are allowed to be uploaded.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1047, N'CreateEvent', N'CreateEventImageTypeUI', N'Only files with extension jpg,.png,.jpeg are allowed to be uploaded.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1048, N'CreateEvent', N'CreateEventImageSizeUI', N'Image More than 2 MB not allowed.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1049, N'CreateEvent', N'CreateEventImagecountUI', N'Only 5 image allowed.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1050, N'CreateEvent', N'CreateEventOrganizercountUI', N'Limit Exceeded, System Allow Only 25 Orgnizer.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1051, N'CreateEvent', N'CreateEventProbleminApllUI', N'Sorry there is some problem.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1052, N'CreateEvent', N'CreateEventEnternumberUI', N'Please enter number')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1053, N'CreateEvent', N'CreateEventDeleteUI', N'Are you sure you want to delete it?')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1054, N'CMSUsers', N'UsersOrgMerStatusSucc', N'Record updated Successfully.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1055, N'CreateEvent', N'CreateEventsavedsucc', N'Congratulations, your event is on Eventcombo!')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1056, N'CMSUsers', N'UsersDeleteUI', N'Are you sure you want to delete it ?')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1058, N'EventConfirmation', N'Evtconfirmsucc', N'Congratulation Your Event is Live now')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1059, N'ViewEvent', N'ViewEventExpiredSy', N'Your Event has Ended')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1060, N'ViewEvent', N'ViewEventNoQtyUI', N'Please Select Atleast one qty')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1061, N'TicketPayment', N'TPayValidateEmailUI', N'Invalid Email')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1062, N'TicketPayment', N'TPayValidateConfirmMatchEmailUI', N'Confirm Email Doesnot match Email')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1063, N'TicketPayment', N'TPayValidateconfirmPwdMatchUI', N'Confirm Password Doesnot match Password')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1064, N'TicketPayment', N'TPayCardValidationUI', N'Invalid Card Number')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1065, N'TicketPayment', N'TPayValidateBillingAddressUI', N'Please Fill the Billing Address Details')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1070, N'TicketPayment', N'TPayValidateShippingAddressUI', N'Please Fill the Shipping Address Details')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1071, N'TicketPayment', N'TPayValidateEmailSy', N'Email already exist,Please use Sign in')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1072, N'TicketPayment', N'TPayValidatebearernameUI', N'Please enter valid Name')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1073, N'TicketPayment', N'TPayValidatebeareremailUI', N'Please enter valid Email')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1074, N'TicketPayment', N'TPayValidatePwdUI', N'Invalid Password')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1075, N'TicketPayment', N'TPayFnameValidateUI', N'Please Enter Name')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1076, N'CreateEvent', N'CreateEventQtyValidateUI', N'Ticket Qty cannot be less than equal to zero')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1077, N'Email', N'EmailSuccessSy', N'Template Saved Successfully')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1080, N'ViewEvent', N'OverSelling', N'One of your selected ticket has been already sold.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1081, N'ViewEvent', N'EventNotLive', N'You cannot purchase ticket as this event is not live.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1082, N'ViewEvent', N'VoteOnce', N'You can only vote once.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1083, N'ViewEvent', N'ThanksForVote', N'Thank you for voting!')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1084, N'ViewEvent', N'eventSaved', N'Your Event have been saved!')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1085, N'ViewEvent', N'eventnotsaved', N'Your Event have not been saved!')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1086, N'CreateEvent', N'CreateEventSubDescUI', N'Please Enter Variable Sub Description')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1087, N'CreateEvent', N'CreateEventSubQtyUI', N'Variable Sub qty can''t be zero')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1088, N'CMSUsers', N'NoEvents', N'User has not created any event')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1089, N'CreateEvent', N'PriceNotzeroUI', N'Invalid Price')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1090, N'CreateEvent', N'NoTicketUI', N'Select atleast one ticket')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1091, N'CreateEvent', N'Validdate', N'Please add valid date and time')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1092, N'EventType', N'EventtypeDeleteUI', N'Are you sure you want to delete it ?')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1093, N'EventCatogary', N'EventCatDeleteUi', N'Are you sure you want to delete it ?')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1094, N'CreateEvent', N'CreateeventUpdated', N'Your Event is successfully updated!!')
GO
SET IDENTITY_INSERT [dbo].[Messages] OFF
GO
SET IDENTITY_INSERT [dbo].[Permission_Detail] ON 

GO
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (1, N'Event', N'APP')
GO
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (2, N'Deal', N'APP')
GO
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (3, N'Users', N'CMS')
GO
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (4, N'Events', N'CMS')
GO
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (5, N'Tickets', N'CMS')
GO
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (6, N'EMails', N'CMS')
GO
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (7, N'Messages', N'CMS')
GO
SET IDENTITY_INSERT [dbo].[Permission_Detail] OFF
GO
SET IDENTITY_INSERT [dbo].[Profile] ON 

GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (33, N'7bd93525-a288-43fd-84f8-40ec4b7c50bd', N'James', N'Martin', N'shweta.sindhu123@kiwitech.com', N'fddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddgdddddddddddddddddddddddddddddddddddddddddddddddddddddddddbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnn', N'fddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddgdddddddddddddddddddddddddddddddddddddddddddddddddddddddddbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnn', NULL, NULL, NULL, 1, N'121-212-1212', NULL, N'http:www.url.com', NULL, NULL, NULL, NULL, N'DSC01345.JPG', NULL, NULL, NULL, NULL, NULL, N'121-212-1212', N'image/jpeg', NULL, N'--', N'Y', N'N', N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (36, N'5efeb250-156b-44da-a474-f50b55c5dcb0', N'Steve', N'whey', N'shwfgsgndhu@kiwitech.com', NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'--', N'Y', N'N', N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (40, N'7d3d080d-934a-49c7-9c5c-01fd9f29d8ea', N'Super', N'Admin', N'shweta.sindhu123567@kiwitech.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (43, N'50f99aaf-3975-417f-adf8-fc41b671c3f2', N'Navrit', NULL, N'navi@navi.com', NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'50f99aaf-3975-417f-adf8-fc41b671c3f2_ProfImage3.Jpeg', NULL, NULL, NULL, NULL, NULL, NULL, N'image/jpeg', NULL, N'1-1-1980', N'Y', NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (44, N'24556b52-131a-46ad-b6a8-2897417b40c0', N'shweta', N'sindhu', N'f@f.com', NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, N'f@f.com', NULL, NULL, NULL, NULL, N'', NULL, NULL, NULL, NULL, NULL, NULL, N'', N'Female', N'28-2-2015', N'N', NULL, N'Y', N'Y', N'', N'', N'')
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (45, N'5d7f9411-04cf-4431-9eb2-13efc37b6d33', N'Shweta', N'Sindhu', N'shweta.sindhu@kiwitech.com', NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'', N'', N'')
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10046, N'0ea7644e-f55e-47db-ad21-687465c6cf24', NULL, NULL, N'f12@f.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', N'Y', N'N', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10047, N'f4ee24cd-0194-45eb-ae30-0c92e151a7dc', N'ssss', N'ssss', NULL, NULL, NULL, N'111', N'11', N'111', 1, N'111-111-1111', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10048, N'7db0fbe0-e140-4288-b43b-f0a892343043', N'sdf', N'sdf', NULL, NULL, NULL, N'fdf', N'dfdf', N'dfdf', 16, N'111-111-1111', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10049, N'36e34ed6-c4f9-4fae-bb0d-a6f5ec440350', N'dfgdfg', N'dfgdfg', NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10050, N'412beaf4-fcff-4fda-935b-23985ea250e2', N'Shweta ', N'Sindhu', N'jh@jh.com', NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10051, N'56d81941-25fa-4ac9-ae86-879b1d03e594', NULL, NULL, N'kl@kl.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10052, N'bea62686-6748-494d-9898-b9d2d6f85cfa', NULL, NULL, N'lw@lw.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10053, N'2f52d635-8b42-4bb7-a08f-81102f023932', NULL, NULL, N'khj@kjh.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10061, N'4f8d0f9a-e6f9-4a2d-8228-3486529687ab', N'Prateek', N'Kumar', N'prateek.kumar@kiwitech.com', N'dsfdsd', NULL, NULL, NULL, NULL, 1, N'135-645-3534', N'232-245-6099', NULL, NULL, NULL, NULL, NULL, N'', NULL, NULL, NULL, NULL, NULL, N'436-456-7232', N'', N'Male', N'1-1-1980', NULL, NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10068, N'92587a71-6a50-4a85-911f-789c97580446', N'Pintu', N'Sah', N'pintu.sah@kiwitech.com', NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, N'f@f.com', NULL, NULL, NULL, NULL, N'', NULL, NULL, NULL, NULL, NULL, NULL, N'', NULL, N'1-1-1980', NULL, NULL, N'Y', NULL, N'', N'', N'')
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10069, N'c91be264-0cca-4ade-91cd-c55ca1a184e8', NULL, NULL, N'Navrit.Singh@kiwi.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10070, N'31f226aa-f64b-4ddf-a0f3-2ccd26886400', NULL, NULL, N'editor@eventcombo.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10071, N'c10cde59-184b-4244-9a7c-5b209e38eaae', NULL, NULL, N'saroosh@eventcombo.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10072, N'eb7d6159-2475-4cd1-8a75-d5c986d3161a', NULL, NULL, N'mayank.srivastava@kiwitech.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10073, N'b413ab1f-4cf8-485a-bfb2-00b0236a5a41', NULL, NULL, N'rohit.kesarwani@kiwitech.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10074, N'735b57df-6a29-48d6-87d3-e851aa3245c0', N'Navreet', N'Singh', N'navrit.singh@kiwitech.com', NULL, NULL, N'asd', N'das', N'15211', 1, N'555-555-5555', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10079, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'shweta', N'Sindhu', N'just.shweta29@gmail.com', N'gtfrdg', N'dfgdfg', N'Harrisburg', N'South Dakota', N'ssss', 1, N'122-222-2222', N'222-222-2222', NULL, NULL, NULL, NULL, NULL, N'16724f9c-389d-43a3-b6b2-bc83989b9102_ProfImage6.Jpeg', NULL, NULL, NULL, NULL, NULL, NULL, N'image/jpeg', N'Female', N'1-1-1980', NULL, NULL, N'Y', NULL, N'', N'', N'')
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10080, N'7ba72509-927a-4f58-9d7f-a41e3660204d', NULL, NULL, N'sh@sh.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10081, N'7fde72cd-c8ec-41bb-8dd4-3c67a9f54e95', NULL, NULL, N'23d@d.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10082, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Navreet  singh', N'ghgjgjh', N'Navi@g.com', N'drfgdfg', N'dfgdfg', N'Pittsburgh', N'Pennsylvania', N'15216', 1, N'999-999-9999', NULL, NULL, NULL, NULL, NULL, NULL, N'e583e85c-bf2f-43e4-9d56-5a20c73796be_ProfImage6.Jpeg', NULL, NULL, NULL, NULL, NULL, NULL, N'image/jpeg', N'Male', N'1-1-1980', NULL, NULL, N'Y', NULL, N'', N'', N'')
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10083, N'a5c9a386-95d2-4f47-bd11-853bbc993caa', N'Radhika', NULL, N'jhd@jhs.com', NULL, NULL, N'Watford', N'Hertfordshire', N'wd17', 229, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'a5c9a386-95d2-4f47-bd11-853bbc993caa_ProfImage3.Jpeg', NULL, NULL, NULL, NULL, NULL, NULL, N'image/jpeg', NULL, N'1-1-1980', NULL, NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10084, N'19bdbd0b-6fa3-4461-9eab-6e99c789476c', NULL, NULL, N'navrit.singh@hotmail.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10085, N'c09a9cd0-22cf-46f1-a40e-2c9c1f30a7c9', NULL, NULL, N'kumar.prat@gmail.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10086, N'40cae138-f961-40cc-9dc4-2424b51f8a75', NULL, NULL, N'kumar.prateek@gmail.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10087, N'f3dff988-adb7-440d-9f17-8e37f108e131', NULL, NULL, N'hj@hj.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10088, N'9e10f676-b5d0-4520-9930-d52102d42d9a', NULL, NULL, N'gh@ghd.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10089, N'746ffe19-8552-4ef1-8138-3962cbc20575', NULL, NULL, N'ghf@ghf.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10101, N'376369a7-9d4d-4374-bd93-2e85dae03766', N'Navreet', N'Singh', N'Navi@new.com', NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', NULL, NULL, NULL, NULL, NULL, NULL, N'', N'Male', N'1-1-1980', NULL, NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10102, N'6fcab4d1-8beb-469b-a801-cda2ffe6efb3', NULL, NULL, N'ravi.prince88@gmail.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10104, N'fdad64ff-80f5-4a1e-96af-9948fece5967', NULL, NULL, N'pinto@g.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10105, N'b68f6b28-3465-4b34-a75d-2958da20ee7b', NULL, NULL, N'ns@yahoo.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10106, N'4b6dd2bc-6a21-480d-9249-034b7d1e76dc', N'Radhika', N'Apte', N'radhika@kiwitech.com', NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'4b6dd2bc-6a21-480d-9249-034b7d1e76dc_ProfImage4.Jpeg', NULL, NULL, NULL, NULL, NULL, NULL, N'image/jpeg', NULL, N'1-1-1980', NULL, NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10107, N'2dd29514-6025-4649-8d71-5ce2e117fa69', NULL, NULL, N'navi@yahoo.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10108, N'34b7f0f9-8776-4628-a690-8efa1a2bd425', NULL, NULL, N'g23h@gh.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10109, N'f8aa26e4-31d6-4cf4-8cec-708913931fcd', NULL, NULL, N'kj@kjh.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'', N'', N'')
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10110, N'1bd462fd-9fb2-45c9-941e-146b3a436f48', NULL, NULL, N'kj@kjn.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'', N'', N'')
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10116, N'bb9d07ec-0a08-4646-bc11-cb3e7f6480a3', N'Ravi', N'Prakash', N'ravi.prakash@kiwitech.com', NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', NULL, NULL, NULL, NULL, NULL, NULL, N'', NULL, N'1-1-1980', NULL, NULL, N'Y', NULL, N'', N'', N'')
GO
SET IDENTITY_INSERT [dbo].[Profile] OFF
GO
SET IDENTITY_INSERT [dbo].[Status] ON 

GO
INSERT [dbo].[Status] ([StatusID], [StatusName]) VALUES (1, N'Active')
GO
INSERT [dbo].[Status] ([StatusID], [StatusName]) VALUES (2, N'Not Active')
GO
INSERT [dbo].[Status] ([StatusID], [StatusName]) VALUES (3, N'Pending')
GO
SET IDENTITY_INSERT [dbo].[Status] OFF
GO
SET IDENTITY_INSERT [dbo].[Ticket] ON 

GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (174, 304, N'Paid ', 500, CAST(15.00 AS Decimal(9, 2)), 2, NULL, NULL, N'Tickt Desc', N'1', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.45 AS Decimal(9, 2)), NULL, N'0', NULL, CAST(2.00 AS Decimal(9, 2)), CAST(15.45 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (175, 304, N'Free ', 100, NULL, 1, NULL, NULL, N'This is a free ticket', N'1', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 1, CAST(0.00 AS Decimal(9, 2)), NULL, NULL, N'0', NULL, NULL, NULL)
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (176, 305, N'Paid Tickets', 500, CAST(12.00 AS Decimal(9, 2)), 2, NULL, NULL, N'Paid Ticket', N'1', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.36 AS Decimal(9, 2)), NULL, N'0', NULL, CAST(2.00 AS Decimal(9, 2)), CAST(12.36 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (177, 305, N'Free Ticket', 400, NULL, 1, NULL, NULL, N'Ticket desc', N'1', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 1, CAST(0.00 AS Decimal(9, 2)), NULL, NULL, N'0', NULL, NULL, NULL)
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (178, 306, N'dfg', 34, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), NULL, NULL, N'0', NULL, NULL, NULL)
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (179, 307, N'dfg', 34, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.00 AS Decimal(9, 2)), NULL, N'0', NULL, NULL, CAST(0.00 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (180, 307, N'ghgh', 89, CAST(2222.22 AS Decimal(9, 2)), 2, NULL, NULL, NULL, N'0', N'0', NULL, N'7:00pm', NULL, N'7:00pm', N'0', N'0', NULL, N'7:00pm', NULL, N'7:00pm', NULL, NULL, N'0', N'0', NULL, 1, CAST(0.00 AS Decimal(9, 2)), CAST(66.67 AS Decimal(9, 2)), NULL, N'0', NULL, NULL, CAST(2288.89 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (181, 308, N'sdf', 23, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), NULL, NULL, N'0', NULL, NULL, NULL)
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (182, 308, N'dsf', 23, CAST(2222.22 AS Decimal(9, 2)), 2, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 1, CAST(0.00 AS Decimal(9, 2)), CAST(66.67 AS Decimal(9, 2)), NULL, N'0', NULL, CAST(23.00 AS Decimal(9, 2)), CAST(2288.89 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (183, 309, N'dfg', 34, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.00 AS Decimal(9, 2)), N'0', N'0', NULL, NULL, CAST(0.00 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (184, 310, N'dff', 23, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.00 AS Decimal(9, 2)), N'0', N'0', NULL, NULL, CAST(0.00 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (185, 311, N'Paid ', 500, CAST(14.00 AS Decimal(9, 2)), 2, NULL, NULL, N'Paid Desc', N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.42 AS Decimal(9, 2)), NULL, N'0', NULL, CAST(3.00 AS Decimal(9, 2)), CAST(14.42 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (186, 312, N'sdf', 324, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.00 AS Decimal(9, 2)), N'0', N'0', NULL, NULL, CAST(0.00 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (187, 313, N'wer', 23, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), NULL, NULL, N'0', NULL, NULL, NULL)
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (188, 314, N'Paid', 1500, CAST(15.00 AS Decimal(9, 2)), 2, NULL, NULL, NULL, N'0', N'0', CAST(0xD33A0B00 AS Date), N'6:30pm', CAST(0xFA3A0B00 AS Date), N'10:00pm', N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.45 AS Decimal(9, 2)), NULL, N'0', NULL, NULL, CAST(15.45 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (189, 315, N'Only VIP ', 400, CAST(500.00 AS Decimal(9, 2)), 2, NULL, NULL, N'Only VIP persons alowed', N'1', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(15.00 AS Decimal(9, 2)), NULL, N'0', NULL, CAST(2.00 AS Decimal(9, 2)), CAST(515.00 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (190, 315, N'Donation Ticket', 100, NULL, 3, NULL, NULL, N'Donation tickets', N'1', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 1, CAST(0.00 AS Decimal(9, 2)), NULL, NULL, N'0', NULL, NULL, NULL)
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (191, 316, N'ret', 34, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.00 AS Decimal(9, 2)), N'0', N'0', NULL, NULL, CAST(0.00 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (192, 317, N'sdf', 34, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.00 AS Decimal(9, 2)), N'0', N'0', NULL, NULL, CAST(0.00 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (193, 318, N'dfg', 34, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), NULL, NULL, N'0', NULL, NULL, NULL)
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (194, 319, N'sdf', 234, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), NULL, NULL, N'0', NULL, NULL, NULL)
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (195, 320, N'dfg', 34, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.00 AS Decimal(9, 2)), N'0', N'0', NULL, NULL, CAST(0.00 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (196, 321, N'dfg', 34, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.00 AS Decimal(9, 2)), N'0', N'0', NULL, NULL, CAST(0.00 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (197, 321, N'dfg', 23, CAST(23.00 AS Decimal(9, 2)), 2, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 1, CAST(0.00 AS Decimal(9, 2)), CAST(0.69 AS Decimal(9, 2)), N'0', N'0', NULL, CAST(2.00 AS Decimal(9, 2)), CAST(23.69 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (198, 322, N'vxcv', 23, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), NULL, NULL, N'0', NULL, NULL, NULL)
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (199, 323, N'test ', 12, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.00 AS Decimal(9, 2)), N'0', N'0', NULL, NULL, CAST(0.00 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (200, 324, N'Paid', 1500, CAST(15.00 AS Decimal(9, 2)), 2, NULL, NULL, N'Paid', N'1', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.45 AS Decimal(9, 2)), NULL, N'0', NULL, CAST(2.00 AS Decimal(9, 2)), CAST(15.45 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (201, 323, N'fgfg', 23, CAST(12.36 AS Decimal(9, 2)), 2, NULL, NULL, NULL, N'0', N'0', NULL, N'7:00pm', NULL, N'7:00pm', N'0', N'0', NULL, N'7:00pm', NULL, N'7:00pm', NULL, NULL, N'0', N'0', NULL, 1, CAST(0.00 AS Decimal(9, 2)), CAST(0.37 AS Decimal(9, 2)), N'0', N'0', NULL, CAST(2.00 AS Decimal(9, 2)), CAST(12.73 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (202, 325, N'Paid', 1500, CAST(15.00 AS Decimal(9, 2)), 2, NULL, NULL, N'Paid', N'1', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.45 AS Decimal(9, 2)), NULL, N'0', NULL, CAST(2.00 AS Decimal(9, 2)), CAST(15.45 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (203, 326, N'Paid', 1500, CAST(15.00 AS Decimal(9, 2)), 2, NULL, NULL, N'Paid', N'1', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.45 AS Decimal(9, 2)), NULL, N'0', NULL, CAST(2.00 AS Decimal(9, 2)), CAST(15.45 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (204, 327, N'Paid', 1500, CAST(15.00 AS Decimal(9, 2)), 2, NULL, NULL, N'Paid', N'1', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.45 AS Decimal(9, 2)), NULL, N'0', NULL, CAST(2.00 AS Decimal(9, 2)), CAST(15.45 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (205, 328, N'Paid', 1500, CAST(15.00 AS Decimal(9, 2)), 2, NULL, NULL, N'Paid', N'1', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.45 AS Decimal(9, 2)), NULL, N'0', NULL, CAST(2.00 AS Decimal(9, 2)), CAST(15.45 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (206, 316, N'hghg', 12, CAST(22.22 AS Decimal(9, 2)), 2, NULL, NULL, NULL, N'0', N'0', NULL, N'7:00pm', NULL, N'7:00pm', N'0', N'0', NULL, N'7:00pm', NULL, N'7:00pm', NULL, NULL, N'0', N'0', NULL, 1, CAST(0.00 AS Decimal(9, 2)), CAST(0.67 AS Decimal(9, 2)), N'0', N'0', NULL, CAST(2.00 AS Decimal(9, 2)), CAST(22.89 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (207, 329, N'Paid', 1500, CAST(15.00 AS Decimal(9, 2)), 2, NULL, NULL, N'Paid', N'1', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.45 AS Decimal(9, 2)), NULL, N'0', NULL, CAST(2.00 AS Decimal(9, 2)), CAST(15.45 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (208, 330, N'Paid Ticket', 500, CAST(15.00 AS Decimal(9, 2)), 2, NULL, NULL, N'Paid TIcket ', N'1', NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.00 AS Decimal(9, 2)), NULL, N'0', NULL, CAST(2.00 AS Decimal(9, 2)), CAST(15.45 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (209, 331, N'test', 10, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), NULL, NULL, N'0', NULL, NULL, NULL)
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (210, 331, N'test again', 10, CAST(20.00 AS Decimal(9, 2)), 2, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 1, CAST(0.00 AS Decimal(9, 2)), CAST(0.60 AS Decimal(9, 2)), NULL, N'0', NULL, NULL, CAST(20.60 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (211, 332, N'dfg', 23, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), NULL, NULL, N'0', NULL, NULL, NULL)
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (212, 333, N'ggdfg', 10, NULL, 1, NULL, NULL, NULL, N'0', N'1', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.00 AS Decimal(9, 2)), N'0', N'0', NULL, NULL, CAST(0.00 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (213, 334, N'dsf', 23, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), NULL, NULL, N'0', NULL, NULL, NULL)
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (214, 335, N'dsafsf', 23, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.00 AS Decimal(9, 2)), N'0', N'0', NULL, NULL, CAST(0.00 AS Decimal(9, 2)))
GO
SET IDENTITY_INSERT [dbo].[Ticket] OFF
GO
SET IDENTITY_INSERT [dbo].[TicketType] ON 

GO
INSERT [dbo].[TicketType] ([TicketTypeID], [TicketType]) VALUES (1, N'Free')
GO
INSERT [dbo].[TicketType] ([TicketTypeID], [TicketType]) VALUES (2, N'Paid')
GO
INSERT [dbo].[TicketType] ([TicketTypeID], [TicketType]) VALUES (3, N'Donation')
GO
SET IDENTITY_INSERT [dbo].[TicketType] OFF
GO
SET IDENTITY_INSERT [dbo].[TimeZoneDetail] ON 

GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (1, N'(GMT-12:00) International Date Line West', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (2, N'(GMT-11:00) Midway Island, Samoa', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (3, N'(GMT-10:00) Hawaii', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (4, N'(GMT-09:00) Alaska', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (5, N'(GMT-08:00) Pacific Time (US & Canada)', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (6, N'(GMT-08:00) Tijuana, Baja California', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (7, N'(GMT-07:00) Arizona', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (8, N'(GMT-07:00) Chihuahua, La Paz, Mazatlan', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (9, N'(GMT-07:00) Mountain Time (US & Canada)', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (10, N'(GMT-06:00) Central America', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (11, N'(GMT-06:00) Central Time (US & Canada)', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (12, N'(GMT-06:00) Guadalajara, Mexico City, Monterrey', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (13, N'(GMT-06:00) Saskatchewan', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (14, N'(GMT-05:00) Bogota, Lima, Quito, Rio Branco', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (15, N'(GMT-05:00) Eastern Time (US & Canada)', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (16, N'(GMT-05:00) Indiana (East)', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (17, N'(GMT-04:00) Atlantic Time (Canada)', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (18, N'(GMT-04:00) Caracas, La Paz', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (19, N'(GMT-04:00) Manaus', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (20, N'(GMT-04:00) Santiago', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (21, N'(GMT-03:30) Newfoundland', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (22, N'(GMT-03:00) Brasilia', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (23, N'(GMT-03:00) Buenos Aires, Georgetown', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (24, N'(GMT-03:00) Greenland', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (25, N'(GMT-03:00) Montevideo', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (26, N'(GMT-02:00) Mid-Atlantic', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (27, N'(GMT-01:00) Cape Verde Is.', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (28, N'(GMT-01:00) Azores', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (29, N'(GMT+00:00) Casablanca, Monrovia, Reykjavik', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (30, N'(GMT+00:00) Greenwich Mean Time : Dublin, Edinburgh, Lisbon, London', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (31, N'(GMT+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (32, N'(GMT+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (33, N'(GMT+01:00) Brussels, Copenhagen, Madrid, Paris', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (34, N'(GMT+01:00) Sarajevo, Skopje, Warsaw, Zagreb', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (35, N'(GMT+01:00) West Central Africa', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (36, N'(GMT+02:00) Amman', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (37, N'(GMT+02:00) Athens, Bucharest, Istanbul', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (38, N'(GMT+02:00) Beirut', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (39, N'(GMT+02:00) Cairo', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (40, N'(GMT+02:00) Harare, Pretoria', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (41, N'(GMT+02:00) Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (42, N'(GMT+02:00) Jerusalem', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (43, N'(GMT+02:00) Minsk', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (44, N'(GMT+02:00) Windhoek', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (45, N'(GMT+03:00) Kuwait, Riyadh, Baghdad', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (46, N'(GMT+03:00) Moscow, St. Petersburg, Volgograd', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (47, N'(GMT+03:00) Nairobi', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (48, N'(GMT+03:00) Tbilisi', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (49, N'(GMT+03:30) Tehran', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (50, N'(GMT+04:00) Abu Dhabi, Muscat', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (51, N'(GMT+04:00) Baku', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (52, N'(GMT+04:00) Yerevan', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (53, N'(GMT+04:30) Kabul', N'Afghanistan Standard Time')
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (54, N'(GMT+05:00) Yekaterinburg', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (55, N'(GMT+05:00) Islamabad, Karachi, Tashkent', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (56, N'(GMT+05:30) Sri Jayawardenapura', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (57, N'(GMT+05:30) Chennai, Kolkata, Mumbai, New Delhi', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (58, N'(GMT+05:45) Kathmandu', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (59, N'(GMT+06:00) Almaty, Novosibirsk', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (60, N'(GMT+06:00) Astana, Dhaka', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (61, N'(GMT+06:30) Yangon (Rangoon)', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (62, N'(GMT+07:00) Bangkok, Hanoi, Jakarta', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (63, N'(GMT+07:00) Krasnoyarsk', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (64, N'(GMT+08:00) Beijing, Chongqing, Hong Kong, Urumqi', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (65, N'(GMT+08:00) Kuala Lumpur, Singapore', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (66, N'(GMT+08:00) Irkutsk, Ulaan Bataar', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (67, N'(GMT+08:00) Perth', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (68, N'(GMT+08:00) Taipei', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (69, N'(GMT+09:00) Osaka, Sapporo, Tokyo', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (70, N'(GMT+09:00) Seoul', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (71, N'(GMT+09:00) Yakutsk', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (72, N'(GMT+09:30) Adelaide', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (73, N'(GMT+09:30) Darwin', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (74, N'(GMT+10:00) Brisbane', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (75, N'(GMT+10:00) Canberra, Melbourne, Sydney', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (76, N'(GMT+10:00) Hobart', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (77, N'(GMT+10:00) Guam, Port Moresby', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (78, N'(GMT+10:00) Vladivostok', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (79, N'(GMT+11:00) Magadan, Solomon Is., New Caledonia', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (80, N'(GMT+12:00) Auckland, Wellington', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (81, N'(GMT+12:00) Fiji, Kamchatka, Marshall Is.', NULL)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (82, N'(GMT+13:00) Nuku''alofa', NULL)
GO
SET IDENTITY_INSERT [dbo].[TimeZoneDetail] OFF
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+04:30) Kabul', N'Afghanistan Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-09:00) Alaska', N'Alaskan Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+03:00) Kuwait, Riyadh', N'Arab Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+04:00) Abu Dhabi, Muscat', N'Arabian Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+03:00) Baghdad', N'Arabic Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-03:00) Buenos Aires', N'Argentina Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-04:00) Atlantic Time (Canada)', N'Atlantic Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+09:30) Darwin', N'AUS Central Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+10:00) Canberra, Melbourne, Sydney', N'AUS Eastern Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+04:00) Baku', N'Azerbaijan Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-01:00) Azores', N'Azores Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+06:00) Dhaka', N'Bangladesh Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-06:00) Saskatchewan', N'Canada Central Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-01:00) Cape Verde Is.', N'Cape Verde Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+04:00) Yerevan', N'Caucasus Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+09:30) Adelaide', N'Cen. Australia Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-06:00) Central America', N'Central America Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+06:00) Astana', N'Central Asia Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-04:00) Cuiaba', N'Central Brazilian Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague', N'Central Europe Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb', N'Central European Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+11:00) Solomon Is., New Caledonia', N'Central Pacific Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-06:00) Central Time (US & Canada)', N'Central Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-06:00) Guadalajara, Mexico City, Monterrey', N'Central Standard Time (Mexico)')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+08:00) Beijing, Chongqing, Hong Kong, Urumqi', N'China Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-12:00) International Date Line West', N'Dateline Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+03:00) Nairobi', N'E. Africa Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+10:00) Brisbane', N'E. Australia Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+02:00) Minsk', N'E. Europe Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-03:00) Brasilia', N'E. South America Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-05:00) Eastern Time (US & Canada)', N'Eastern Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+02:00) Cairo', N'Egypt Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+05:00) Ekaterinburg', N'Ekaterinburg Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+12:00) Fiji', N'Fiji Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+02:00) Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius', N'FLE Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+04:00) Tbilisi', N'Georgian Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC) Dublin, Edinburgh, Lisbon, London', N'GMT Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-03:00) Greenland', N'Greenland Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC) Monrovia, Reykjavik', N'Greenwich Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+02:00) Athens, Bucharest, Istanbul', N'GTB Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-10:00) Hawaii', N'Hawaiian Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+05:30) Chennai, Kolkata, Mumbai, New Delhi', N'India Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+03:30) Tehran', N'Iran Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+02:00) Jerusalem', N'Israel Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+02:00) Amman', N'Jordan Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+12:00) Petropavlovsk-Kamchatsky - Old', N'Kamchatka Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+09:00) Seoul', N'Korea Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+11:00) Magadan', N'Magadan Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+04:00) Port Louis', N'Mauritius Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-02:00) Mid-Atlantic', N'Mid-Atlantic Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+02:00) Beirut', N'Middle East Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-03:00) Montevideo', N'Montevideo Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC) Casablanca', N'Morocco Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-07:00) Mountain Time (US & Canada)', N'Mountain Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-07:00) Chihuahua, La Paz, Mazatlan', N'Mountain Standard Time (Mexico)')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+06:30) Yangon (Rangoon)', N'Myanmar Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+06:00) Novosibirsk', N'N. Central Asia Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+01:00) Windhoek', N'Namibia Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+05:45) Kathmandu', N'Nepal Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+12:00) Auckland, Wellington', N'New Zealand Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-03:30) Newfoundland', N'Newfoundland Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+08:00) Irkutsk', N'North Asia East Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+07:00) Krasnoyarsk', N'North Asia Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-04:00) Santiago', N'Pacific SA Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-08:00) Pacific Time (US & Canada)', N'Pacific Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-08:00) Baja California', N'Pacific Standard Time (Mexico)')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+05:00) Islamabad, Karachi', N'Pakistan Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-04:00) Asuncion', N'Paraguay Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+01:00) Brussels, Copenhagen, Madrid, Paris', N'Romance Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+03:00) Moscow, St. Petersburg, Volgograd', N'Russian Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-03:00) Cayenne, Fortaleza', N'SA Eastern Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-05:00) Bogota, Lima, Quito', N'SA Pacific Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-04:00) Georgetown, La Paz, Manaus, San Juan', N'SA Western Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-11:00) Samoa', N'Samoa Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+07:00) Bangkok, Hanoi, Jakarta', N'SE Asia Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+08:00) Kuala Lumpur, Singapore', N'Singapore Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+02:00) Harare, Pretoria', N'South Africa Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+05:30) Sri Jayawardenepura', N'Sri Lanka Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+02:00) Damascus', N'Syria Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+08:00) Taipei', N'Taipei Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+10:00) Hobart', N'Tasmania Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+09:00) Osaka, Sapporo, Tokyo', N'Tokyo Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+13:00) Nuku''alofa', N'Tonga Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+08:00) Ulaanbaatar', N'Ulaanbaatar Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-05:00) Indiana (East)', N'US Eastern Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-07:00) Arizona', N'US Mountain Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC) Coordinated Universal Time', N'UTC')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+12:00) Coordinated Universal Time+12', N'UTC+12')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-02:00) Coordinated Universal Time-02', N'UTC-02')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-11:00) Coordinated Universal Time-11', N'UTC-11')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC-04:30) Caracas', N'Venezuela Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+10:00) Vladivostok', N'Vladivostok Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+08:00) Perth', N'W. Australia Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+01:00) West Central Africa', N'W. Central Africa Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna', N'W. Europe Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+05:00) Tashkent', N'West Asia Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+10:00) Guam, Port Moresby', N'West Pacific Standard Time')
GO
INSERT [dbo].[TimeZonesystem] ([DisplayName], [Id]) VALUES (N'(UTC+09:00) Yakutsk', N'Yakutsk Standard Time')
GO
SET IDENTITY_INSERT [dbo].[User_Permission_Detail] ON 

GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (53, N'5c64d07f-bf17-40df-9853-53fdee89659c', 3, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (61, N'266bce4b-f1de-4c49-a0dc-9b2cddaee09c', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (62, N'266bce4b-f1de-4c49-a0dc-9b2cddaee09c', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (63, N'266bce4b-f1de-4c49-a0dc-9b2cddaee09c', 3, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (64, N'266bce4b-f1de-4c49-a0dc-9b2cddaee09c', 4, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (65, N'266bce4b-f1de-4c49-a0dc-9b2cddaee09c', 5, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (66, N'266bce4b-f1de-4c49-a0dc-9b2cddaee09c', 7, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (74, N'50f99aaf-3975-417f-adf8-fc41b671c3f2', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (75, N'50f99aaf-3975-417f-adf8-fc41b671c3f2', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (76, N'50f99aaf-3975-417f-adf8-fc41b671c3f2', 3, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (77, N'7bd93525-a288-43fd-84f8-40ec4b7c50bd', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (78, N'7bd93525-a288-43fd-84f8-40ec4b7c50bd', 3, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (79, N'7bd93525-a288-43fd-84f8-40ec4b7c50bd', 4, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (80, N'7bd93525-a288-43fd-84f8-40ec4b7c50bd', 5, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (81, N'7bd93525-a288-43fd-84f8-40ec4b7c50bd', 6, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (88, N'5d7f9411-04cf-4431-9eb2-13efc37b6d33', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (89, N'5d7f9411-04cf-4431-9eb2-13efc37b6d33', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1076, N'0ea7644e-f55e-47db-ad21-687465c6cf24', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1077, N'0ea7644e-f55e-47db-ad21-687465c6cf24', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1080, N'7db0fbe0-e140-4288-b43b-f0a892343043', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1081, N'7db0fbe0-e140-4288-b43b-f0a892343043', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1084, N'412beaf4-fcff-4fda-935b-23985ea250e2', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1085, N'412beaf4-fcff-4fda-935b-23985ea250e2', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1086, N'56d81941-25fa-4ac9-ae86-879b1d03e594', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1087, N'56d81941-25fa-4ac9-ae86-879b1d03e594', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1088, N'bea62686-6748-494d-9898-b9d2d6f85cfa', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1089, N'bea62686-6748-494d-9898-b9d2d6f85cfa', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1090, N'2f52d635-8b42-4bb7-a08f-81102f023932', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1091, N'2f52d635-8b42-4bb7-a08f-81102f023932', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1166, N'7d3d080d-934a-49c7-9c5c-01fd9f29d8ea', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1167, N'7d3d080d-934a-49c7-9c5c-01fd9f29d8ea', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1168, N'7d3d080d-934a-49c7-9c5c-01fd9f29d8ea', 3, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1218, N'4f8d0f9a-e6f9-4a2d-8228-3486529687ab', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1219, N'4f8d0f9a-e6f9-4a2d-8228-3486529687ab', 3, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1280, N'5efeb250-156b-44da-a474-f50b55c5dcb0', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1281, N'5efeb250-156b-44da-a474-f50b55c5dcb0', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1282, N'5efeb250-156b-44da-a474-f50b55c5dcb0', 3, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1283, N'24556b52-131a-46ad-b6a8-2897417b40c0', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1284, N'24556b52-131a-46ad-b6a8-2897417b40c0', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1285, N'24556b52-131a-46ad-b6a8-2897417b40c0', 3, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1286, N'24556b52-131a-46ad-b6a8-2897417b40c0', 4, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1287, N'24556b52-131a-46ad-b6a8-2897417b40c0', 5, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1288, N'24556b52-131a-46ad-b6a8-2897417b40c0', 6, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1289, N'24556b52-131a-46ad-b6a8-2897417b40c0', 7, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1290, N'92587a71-6a50-4a85-911f-789c97580446', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1291, N'92587a71-6a50-4a85-911f-789c97580446', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1304, N'36e34ed6-c4f9-4fae-bb0d-a6f5ec440350', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1305, N'36e34ed6-c4f9-4fae-bb0d-a6f5ec440350', 3, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1312, N'c91be264-0cca-4ade-91cd-c55ca1a184e8', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1313, N'31f226aa-f64b-4ddf-a0f3-2ccd26886400', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1314, N'31f226aa-f64b-4ddf-a0f3-2ccd26886400', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1315, N'c10cde59-184b-4244-9a7c-5b209e38eaae', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1316, N'c10cde59-184b-4244-9a7c-5b209e38eaae', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1317, N'eb7d6159-2475-4cd1-8a75-d5c986d3161a', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1318, N'eb7d6159-2475-4cd1-8a75-d5c986d3161a', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1319, N'b413ab1f-4cf8-485a-bfb2-00b0236a5a41', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1320, N'b413ab1f-4cf8-485a-bfb2-00b0236a5a41', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1321, N'735b57df-6a29-48d6-87d3-e851aa3245c0', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1322, N'735b57df-6a29-48d6-87d3-e851aa3245c0', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1331, N'16724f9c-389d-43a3-b6b2-bc83989b9102', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1332, N'16724f9c-389d-43a3-b6b2-bc83989b9102', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1335, N'7fde72cd-c8ec-41bb-8dd4-3c67a9f54e95', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1336, N'7fde72cd-c8ec-41bb-8dd4-3c67a9f54e95', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1337, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1338, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1339, N'a5c9a386-95d2-4f47-bd11-853bbc993caa', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1340, N'a5c9a386-95d2-4f47-bd11-853bbc993caa', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1341, N'19bdbd0b-6fa3-4461-9eab-6e99c789476c', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1342, N'19bdbd0b-6fa3-4461-9eab-6e99c789476c', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1345, N'40cae138-f961-40cc-9dc4-2424b51f8a75', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1346, N'40cae138-f961-40cc-9dc4-2424b51f8a75', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1351, N'746ffe19-8552-4ef1-8138-3962cbc20575', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1352, N'746ffe19-8552-4ef1-8138-3962cbc20575', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1353, N'9e10f676-b5d0-4520-9930-d52102d42d9a', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1354, N'9e10f676-b5d0-4520-9930-d52102d42d9a', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1355, N'c09a9cd0-22cf-46f1-a40e-2c9c1f30a7c9', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1356, N'f3dff988-adb7-440d-9f17-8e37f108e131', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1357, N'7ba72509-927a-4f58-9d7f-a41e3660204d', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1374, N'f4ee24cd-0194-45eb-ae30-0c92e151a7dc', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1397, N'376369a7-9d4d-4374-bd93-2e85dae03766', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1398, N'376369a7-9d4d-4374-bd93-2e85dae03766', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1399, N'6fcab4d1-8beb-469b-a801-cda2ffe6efb3', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1400, N'6fcab4d1-8beb-469b-a801-cda2ffe6efb3', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1403, N'fdad64ff-80f5-4a1e-96af-9948fece5967', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1404, N'fdad64ff-80f5-4a1e-96af-9948fece5967', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1405, N'b68f6b28-3465-4b34-a75d-2958da20ee7b', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1406, N'b68f6b28-3465-4b34-a75d-2958da20ee7b', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1409, N'4b6dd2bc-6a21-480d-9249-034b7d1e76dc', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1410, N'4b6dd2bc-6a21-480d-9249-034b7d1e76dc', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1411, N'4b6dd2bc-6a21-480d-9249-034b7d1e76dc', 4, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1412, N'4b6dd2bc-6a21-480d-9249-034b7d1e76dc', 5, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1413, N'4b6dd2bc-6a21-480d-9249-034b7d1e76dc', 6, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1414, N'4b6dd2bc-6a21-480d-9249-034b7d1e76dc', 7, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1415, N'2dd29514-6025-4649-8d71-5ce2e117fa69', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1416, N'2dd29514-6025-4649-8d71-5ce2e117fa69', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1417, N'34b7f0f9-8776-4628-a690-8efa1a2bd425', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1418, N'34b7f0f9-8776-4628-a690-8efa1a2bd425', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1419, N'f59003aa-ef8d-4c6f-a92a-ab4fe081c6c2', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1420, N'f59003aa-ef8d-4c6f-a92a-ab4fe081c6c2', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1421, N'ffc2e2de-572f-4361-82df-02ae9af3a753', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1422, N'ffc2e2de-572f-4361-82df-02ae9af3a753', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1423, N'e5ac394c-c3bf-4f3b-aa29-feec9ee37629', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1424, N'e5ac394c-c3bf-4f3b-aa29-feec9ee37629', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1425, N'f8aa26e4-31d6-4cf4-8cec-708913931fcd', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1426, N'f8aa26e4-31d6-4cf4-8cec-708913931fcd', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1427, N'1bd462fd-9fb2-45c9-941e-146b3a436f48', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1428, N'1bd462fd-9fb2-45c9-941e-146b3a436f48', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1439, N'bb9d07ec-0a08-4646-bc11-cb3e7f6480a3', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1440, N'bb9d07ec-0a08-4646-bc11-cb3e7f6480a3', 2, NULL)
GO
SET IDENTITY_INSERT [dbo].[User_Permission_Detail] OFF
GO
ALTER TABLE [dbo].[Ticket_Locked_Detail] ADD  CONSTRAINT [DF_Ticket_Locked_Detail_Locktime]  DEFAULT (getdate()) FOR [Locktime]
GO
ALTER TABLE [dbo].[Address]  WITH NOCHECK ADD  CONSTRAINT [FK_Address_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([EventID])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_Event]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Event_Orgnizer_Detail]  WITH NOCHECK ADD  CONSTRAINT [FK_Event_Orgnizer_Detail_Event] FOREIGN KEY([Orgnizer_Event_Id])
REFERENCES [dbo].[Event] ([EventID])
GO
ALTER TABLE [dbo].[Event_Orgnizer_Detail] CHECK CONSTRAINT [FK_Event_Orgnizer_Detail_Event]
GO
ALTER TABLE [dbo].[Event_VariableDesc]  WITH NOCHECK ADD  CONSTRAINT [FK_Event_VariableDesc_Event] FOREIGN KEY([Event_Id])
REFERENCES [dbo].[Event] ([EventID])
GO
ALTER TABLE [dbo].[Event_VariableDesc] CHECK CONSTRAINT [FK_Event_VariableDesc_Event]
GO
ALTER TABLE [dbo].[EventImage]  WITH NOCHECK ADD  CONSTRAINT [FK_EventImage_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([EventID])
GO
ALTER TABLE [dbo].[EventImage] CHECK CONSTRAINT [FK_EventImage_Event]
GO
ALTER TABLE [dbo].[EventSubCategory]  WITH CHECK ADD FOREIGN KEY([EventCategoryID])
REFERENCES [dbo].[EventCategory] ([EventCategoryID])
GO
ALTER TABLE [dbo].[EventVenue]  WITH NOCHECK ADD  CONSTRAINT [FK_EventVenue_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([EventID])
GO
ALTER TABLE [dbo].[EventVenue] CHECK CONSTRAINT [FK_EventVenue_Event]
GO
ALTER TABLE [dbo].[EventVenue]  WITH CHECK ADD  CONSTRAINT [FK_EventVenue_EventVenue] FOREIGN KEY([EventVenueID])
REFERENCES [dbo].[EventVenue] ([EventVenueID])
GO
ALTER TABLE [dbo].[EventVenue] CHECK CONSTRAINT [FK_EventVenue_EventVenue]
GO
ALTER TABLE [dbo].[MultipleEvent]  WITH NOCHECK ADD  CONSTRAINT [FK_MultipleEvent_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([EventID])
GO
ALTER TABLE [dbo].[MultipleEvent] CHECK CONSTRAINT [FK_MultipleEvent_Event]
GO
ALTER TABLE [dbo].[Profile]  WITH CHECK ADD  CONSTRAINT [FK_Profile_Profile] FOREIGN KEY([UserID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Profile] CHECK CONSTRAINT [FK_Profile_Profile]
GO
ALTER TABLE [dbo].[Ticket]  WITH NOCHECK ADD  CONSTRAINT [FK_Ticket_Event] FOREIGN KEY([E_Id])
REFERENCES [dbo].[Event] ([EventID])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Event]
GO
ALTER TABLE [dbo].[Ticket_Quantity_Detail]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Quantity_Info_Ticket] FOREIGN KEY([TQD_Ticket_Id])
REFERENCES [dbo].[Ticket] ([T_Id])
GO
ALTER TABLE [dbo].[Ticket_Quantity_Detail] CHECK CONSTRAINT [FK_Ticket_Quantity_Info_Ticket]
GO
ALTER TABLE [dbo].[TicketDeliveryMethod]  WITH CHECK ADD FOREIGN KEY([DeliveryMethodID])
REFERENCES [dbo].[DeliveryMethod] ([DeliveryMethodID])
GO
