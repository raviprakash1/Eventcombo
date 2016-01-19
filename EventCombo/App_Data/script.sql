
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__TicketDel__Deliv__08411774]') AND parent_object_id = OBJECT_ID(N'[dbo].[TicketDeliveryMethod]'))
ALTER TABLE [dbo].[TicketDeliveryMethod] DROP CONSTRAINT [FK__TicketDel__Deliv__08411774]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Ticket_Quantity_Info_Ticket]') AND parent_object_id = OBJECT_ID(N'[dbo].[Ticket_Quantity_Detail]'))
ALTER TABLE [dbo].[Ticket_Quantity_Detail] DROP CONSTRAINT [FK_Ticket_Quantity_Info_Ticket]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Ticket_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Ticket]'))
ALTER TABLE [dbo].[Ticket] DROP CONSTRAINT [FK_Ticket_Event]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Profile_Profile]') AND parent_object_id = OBJECT_ID(N'[dbo].[Profile]'))
ALTER TABLE [dbo].[Profile] DROP CONSTRAINT [FK_Profile_Profile]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MultipleEvent_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[MultipleEvent]'))
ALTER TABLE [dbo].[MultipleEvent] DROP CONSTRAINT [FK_MultipleEvent_Event]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EventVenue_EventVenue]') AND parent_object_id = OBJECT_ID(N'[dbo].[EventVenue]'))
ALTER TABLE [dbo].[EventVenue] DROP CONSTRAINT [FK_EventVenue_EventVenue]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EventVenue_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[EventVenue]'))
ALTER TABLE [dbo].[EventVenue] DROP CONSTRAINT [FK_EventVenue_Event]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__EventSubC__Event__019419E5]') AND parent_object_id = OBJECT_ID(N'[dbo].[EventSubCategory]'))
ALTER TABLE [dbo].[EventSubCategory] DROP CONSTRAINT [FK__EventSubC__Event__019419E5]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EventImage_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[EventImage]'))
ALTER TABLE [dbo].[EventImage] DROP CONSTRAINT [FK_EventImage_Event]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_VariableDesc_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event_VariableDesc]'))
ALTER TABLE [dbo].[Event_VariableDesc] DROP CONSTRAINT [FK_Event_VariableDesc_Event]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_Orgnizer_Detail_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event_Orgnizer_Detail]'))
ALTER TABLE [dbo].[Event_Orgnizer_Detail] DROP CONSTRAINT [FK_Event_Orgnizer_Detail_Event]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]'))
ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]'))
ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Address_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Address]'))
ALTER TABLE [dbo].[Address] DROP CONSTRAINT [FK_Address_Event]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_Ticket_Locked_Detail_Locktime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Ticket_Locked_Detail] DROP CONSTRAINT [DF_Ticket_Locked_Detail_Locktime]
END

GO
/****** Object:  Table [dbo].[Venue]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Venue]') AND type in (N'U'))
DROP TABLE [dbo].[Venue]
GO
/****** Object:  Table [dbo].[User_Permission_Detail]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User_Permission_Detail]') AND type in (N'U'))
DROP TABLE [dbo].[User_Permission_Detail]
GO
/****** Object:  Table [dbo].[TimeZoneDetail1]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TimeZoneDetail1]') AND type in (N'U'))
DROP TABLE [dbo].[TimeZoneDetail1]
GO
/****** Object:  Table [dbo].[TimeZoneDetail]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TimeZoneDetail]') AND type in (N'U'))
DROP TABLE [dbo].[TimeZoneDetail]
GO
/****** Object:  Table [dbo].[TicketType]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TicketType]') AND type in (N'U'))
DROP TABLE [dbo].[TicketType]
GO
/****** Object:  Table [dbo].[TicketDeliveryMethod]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TicketDeliveryMethod]') AND type in (N'U'))
DROP TABLE [dbo].[TicketDeliveryMethod]
GO
/****** Object:  Table [dbo].[TicketBearer]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TicketBearer]') AND type in (N'U'))
DROP TABLE [dbo].[TicketBearer]
GO
/****** Object:  Table [dbo].[Ticket_Quantity_Detail]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ticket_Quantity_Detail]') AND type in (N'U'))
DROP TABLE [dbo].[Ticket_Quantity_Detail]
GO
/****** Object:  Table [dbo].[Ticket_Purchased_Detail]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ticket_Purchased_Detail]') AND type in (N'U'))
DROP TABLE [dbo].[Ticket_Purchased_Detail]
GO
/****** Object:  Table [dbo].[Ticket_Locked_Detail]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ticket_Locked_Detail]') AND type in (N'U'))
DROP TABLE [dbo].[Ticket_Locked_Detail]
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ticket]') AND type in (N'U'))
DROP TABLE [dbo].[Ticket]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Status]') AND type in (N'U'))
DROP TABLE [dbo].[Status]
GO
/****** Object:  Table [dbo].[ShippingAddress]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ShippingAddress]') AND type in (N'U'))
DROP TABLE [dbo].[ShippingAddress]
GO
/****** Object:  Table [dbo].[Publish_Event_Detail]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Publish_Event_Detail]') AND type in (N'U'))
DROP TABLE [dbo].[Publish_Event_Detail]
GO
/****** Object:  Table [dbo].[Profile]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Profile]') AND type in (N'U'))
DROP TABLE [dbo].[Profile]
GO
/****** Object:  Table [dbo].[Permission_Detail]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Permission_Detail]') AND type in (N'U'))
DROP TABLE [dbo].[Permission_Detail]
GO
/****** Object:  Table [dbo].[Order_Detail_T]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Order_Detail_T]') AND type in (N'U'))
DROP TABLE [dbo].[Order_Detail_T]
GO
/****** Object:  Table [dbo].[MultipleEvent]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MultipleEvent]') AND type in (N'U'))
DROP TABLE [dbo].[MultipleEvent]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Messages]') AND type in (N'U'))
DROP TABLE [dbo].[Messages]
GO
/****** Object:  Table [dbo].[Fee_Structure]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Fee_Structure]') AND type in (N'U'))
DROP TABLE [dbo].[Fee_Structure]
GO
/****** Object:  Table [dbo].[EventVote]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventVote]') AND type in (N'U'))
DROP TABLE [dbo].[EventVote]
GO
/****** Object:  Table [dbo].[EventVenue]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventVenue]') AND type in (N'U'))
DROP TABLE [dbo].[EventVenue]
GO
/****** Object:  Table [dbo].[EventType]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventType]') AND type in (N'U'))
DROP TABLE [dbo].[EventType]
GO
/****** Object:  Table [dbo].[EventSubCategory]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventSubCategory]') AND type in (N'U'))
DROP TABLE [dbo].[EventSubCategory]
GO
/****** Object:  Table [dbo].[EventImage]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventImage]') AND type in (N'U'))
DROP TABLE [dbo].[EventImage]
GO
/****** Object:  Table [dbo].[EventFavourite]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventFavourite]') AND type in (N'U'))
DROP TABLE [dbo].[EventFavourite]
GO
/****** Object:  Table [dbo].[EventCategory]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventCategory]') AND type in (N'U'))
DROP TABLE [dbo].[EventCategory]
GO
/****** Object:  Table [dbo].[Event_VariableDesc]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event_VariableDesc]') AND type in (N'U'))
DROP TABLE [dbo].[Event_VariableDesc]
GO
/****** Object:  Table [dbo].[Event_Orgnizer_Detail]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event_Orgnizer_Detail]') AND type in (N'U'))
DROP TABLE [dbo].[Event_Orgnizer_Detail]
GO
/****** Object:  Table [dbo].[Event_OrganizerMessages]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event_OrganizerMessages]') AND type in (N'U'))
DROP TABLE [dbo].[Event_OrganizerMessages]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event]') AND type in (N'U'))
DROP TABLE [dbo].[Event]
GO
/****** Object:  Table [dbo].[Email_Template]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Email_Template]') AND type in (N'U'))
DROP TABLE [dbo].[Email_Template]
GO
/****** Object:  Table [dbo].[Email_Tag]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Email_Tag]') AND type in (N'U'))
DROP TABLE [dbo].[Email_Tag]
GO
/****** Object:  Table [dbo].[DeliveryMethod]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeliveryMethod]') AND type in (N'U'))
DROP TABLE [dbo].[DeliveryMethod]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Country]') AND type in (N'U'))
DROP TABLE [dbo].[Country]
GO
/****** Object:  Table [dbo].[CardDetails]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CardDetails]') AND type in (N'U'))
DROP TABLE [dbo].[CardDetails]
GO
/****** Object:  Table [dbo].[BillingAddress]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BillingAddress]') AND type in (N'U'))
DROP TABLE [dbo].[BillingAddress]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUsers]') AND type in (N'U'))
DROP TABLE [dbo].[AspNetUsers]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND type in (N'U'))
DROP TABLE [dbo].[AspNetUserRoles]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]') AND type in (N'U'))
DROP TABLE [dbo].[AspNetUserLogins]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]') AND type in (N'U'))
DROP TABLE [dbo].[AspNetUserClaims]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoles]') AND type in (N'U'))
DROP TABLE [dbo].[AspNetRoles]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Address]') AND type in (N'U'))
DROP TABLE [dbo].[Address]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__MigrationHistory]') AND type in (N'U'))
DROP TABLE [dbo].[__MigrationHistory]
GO
/****** Object:  UserDefinedFunction [dbo].[ReturnDayOfWeek]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReturnDayOfWeek]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[ReturnDayOfWeek]
GO
/****** Object:  UserDefinedFunction [dbo].[func_Split]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[func_Split]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[func_Split]
GO
/****** Object:  StoredProcedure [dbo].[PublishEvent]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PublishEvent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PublishEvent]
GO
/****** Object:  StoredProcedure [dbo].[GetTicketListing]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTicketListing]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetTicketListing]
GO
/****** Object:  StoredProcedure [dbo].[GetSetUserRole]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSetUserRole]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetSetUserRole]
GO
/****** Object:  StoredProcedure [dbo].[GetSelectedTicketListing]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSelectedTicketListing]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetSelectedTicketListing]
GO
/****** Object:  StoredProcedure [dbo].[GetEventsListByStatus]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetEventsListByStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetEventsListByStatus]
GO
/****** Object:  StoredProcedure [dbo].[GetEventListing]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetEventListing]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetEventListing]
GO
/****** Object:  StoredProcedure [dbo].[GetEventDateList]    Script Date: 1/18/2016 8:02:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetEventDateList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetEventDateList]
GO
/****** Object:  StoredProcedure [dbo].[GetEventDateList]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetEventDateList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'--[GetEventDateList] 294
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
		(@Dayofweek,@StartDate,@StartTime,''Single'')	
	End
	else 
	begin
	select @Freq=Frequency from MultipleEvent  WHERE EventID = @EventId
	print @Freq
	If (@Freq = ''Daily'')
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
				(@Dayofweek,@DateFrom,@StartTime,''Daily'')
						
				SET @DateFrom = DATEADD(dd,1,@DateFrom) 
				

				set @cnt=@cnt+1;
              END
		
	end
	Else If (@Freq = ''Weekly'')
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
               select * From [dbo].func_Split(@Weekly,'','') 
		  WHILE (@DateFrom<=@DateTo)
            
            BEGIN
			 SET @WeekDay = dbo.ReturnDayOfWeek(@DateFrom) 
			 
			 Select @DayID = count(*) from @tblWeek where DayId= @WeekDay

			 if (@DayID >0)
			 begin
			  SELECT  @Dayofweek=datename(dw,@DateFrom)
           INSERT INTO @tblDates VALUES
				(@Dayofweek,@DateFrom,@StartTime,''weekly'')
	
				 
			 end
			SET @DateFrom = DATEADD(dd,1,@DateFrom) 
			
			end

   End
   Else If(@Freq = ''Monthly'')
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
			   
if(isnull(@Monthlyday,'''')!='''')
begin 

 WHILE (@DateFrom<=@DateTo)
 begin 


set @day=DATEPART(dd,@DateFrom) 

if(@day=@Monthlyday)
begin 
  SELECT  @Dayofweek=datename(dw,@DateFrom)
     INSERT INTO @tblDates VALUES
				(@Dayofweek,@DateFrom,@StartTime,''Monthly'')
	
end
SET @DateFrom = DATEADD(dd,1,@DateFrom) 
 end
end
if( isnull(@Monthlyweek,'''')!='''')
begin

Declare @monthday as int

SELECT @monthday = CASE @Monthlyweek WHEN ''First'' THEN 1 WHEN ''Second'' THEN 2 WHEN ''Third'' THEN 3 WHEN ''Four'' THEN 4 END

Declare @Noofweek varchar(50)
set @cnt=1
SELECT @Noofweek= DATEDIFF(week, @DateFrom, @DateTo)

WHILE @cnt <= @Noofweek
begin
print  convert(varchar,convert(varchar,@Noofweek)+''g''+convert(varchar,@monthday)+''g''+convert(varchar,@cnt))
if(@cnt=@monthday)
begin

Declare @newstartdate as date
Declare @newenddate as date
set @newstartdate=@StartDate
print @newstartdate
set @newenddate=DATEADD(dd,7,@newstartdate) 
               insert into @tblWeek 
               select * From [dbo].func_Split(@MonthlyweekDays,'','') 


			     WHILE (@newstartdate<=@newenddate)
            
            BEGIN
			 SET @WeekDay = dbo.ReturnDayOfWeek(@newstartdate) 
			
			 Select @DayID = count(*) from @tblWeek where DayId= @WeekDay
			
			 if (@DayID >0)
			 begin
			  SELECT  @Dayofweek=datename(dw,@newstartdate)
           INSERT INTO @tblDates VALUES
				(@Dayofweek,@newstartdate,@StartTime,''Monthly'')
	
				 
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

' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetEventListing]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetEventListing]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

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
	--if (ISNULL(@EventTitle,'''') = null)
	--	SET @EventTitle =''''
	
	--if (ISNULL(@EventType,'''') = null)
	--	SET @EventT =0

	--if (ISNULL(@EventCat,'''') = null)
	--	SET @EventC =0

	--if (ISNULL(@EventSubCat,'''') = null)
	--	SET @EventSC =0
	
	--if (ISNULL(@EventFeature,'''') = null)
	--	SET @EventF =0

	--@EventCat varchar(50) = null,
	--@EventSubCat varchar(50) = null,
	--@EventFeature varchar(10) = null
	if(@EventStartdate=0 and @EventTicket=2)
	begin
	select  EV.EventID as EventID , EventTitle,ET.EventType AS EventType,EC.EventCategory as EventCategory , ESC.EventSubCategory as EventSubCategory ,
	(CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		( convert(varchar, convert(Date,EVen.EventStartDate),106) +'' '' +  convert(varchar,convert(time,EVen.EventStartTime),100)  + ''-'' +
		convert(varchar, convert(Date,EVen.EventEndDate),106) + '' '' + convert(varchar,convert(time,EVen.EventEndTime),100)) 
	ELSE
		( convert(varchar,convert(Date,ME.StartingFrom) ,106) + '' '' +  convert(varchar,ME.StartTime)  + ''-'' +
		CONVERT(VARCHAR,convert(Date,Me.StartingTo),106 ) + ''-'' + CONVERT(VARCHAR,Me.EndTime)) 
	END ) as EventTiming ,ME.StartingFrom as	StartingFrom ,ISNULL(Feature,0) as Feature,
(select top 1 Eod.Orgnizer_Name from  Event_Orgnizer_Detail EOD where  EOD.Orgnizer_Event_Id=Ev.EventID and EOD.DefaultOrg=''Y'' ) 
as  Orgnizer_Name,
(select top 1 AD.ConsolidateAddress from Address AD where  AD.EventId= EV.EventID ) as EventAddress,
(select top 1 AD.VenueName from Address AD where  AD.EventId= EV.EventID ) as Venuename,
anr.Email,
(SELECT   convert(varchar,isnull( sum(TQD_Remaining_Quantity),0)) FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) as TicketDetail,
isnull((Select sum(TPD_Purchased_Qty) from Ticket_Purchased_Detail  where TPD_Event_Id=Ev.EventID),0) as Purchasedqty
 from ((((((Event Ev LEFT JOIN EventType ET
	ON Ev.EventTypeID = Et.EventTypeID ) LEFT JOIN EventCategory EC on EV.EventCategoryID =  EC.EventCategoryID)
	LEFT JOIN  EventSubCategory ESC on EV.EventSubCategoryID = ESC.EventSubCategoryID)
	LEFT JOIN EventVenue EVen
	ON Ev.EventID = EVen.EventID)
	LEFT JOIN MultipleEvent ME on EV.EventID = ME.EventID)
	Left join AspNetUsers ANR on Ev.UserID=ANR.Id )
	WHERE Ev.EventTitle like ''%'' + Coalesce(@EventTitle,Ev.EventTitle) + ''%''
	AND isnull(Ev.EventTypeID,'''')  = CASE WHEN @EventType >0 Then @EventType Else isnull(Ev.EventTypeID,'''') END
	AND isnull(Ev.EventCategoryID,0)  = CASE WHEN @EventCat >0 Then @EventCat Else isnull(Ev.EventCategoryID,0) END
	AND isnull(Ev.EventSubCategoryID,0)  = CASE WHEN @EventSubCat >0 Then @EventSubCat  Else isnull(Ev.EventSubCategoryID,0) END
	AND ISNULL(Ev.Feature,0) = CASE WHEN @EventFeature >0 Then @EventFeature  Else ISNULL(Ev.Feature,0) END 
	AND (SELECT   isnull(sum(TQD_Remaining_Quantity),0)
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) >0 

	end
	if(@EventStartdate=0 and @EventTicket=1)
	begin
	select  EV.EventID as EventID , EventTitle,ET.EventType AS EventType,EC.EventCategory as EventCategory , ESC.EventSubCategory as EventSubCategory ,
	(CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		( convert(varchar, convert(Date,EVen.EventStartDate),106) +'' '' +  convert(varchar,convert(time,EVen.EventStartTime),100)  + ''-'' +
		convert(varchar, convert(Date,EVen.EventEndDate),106) + '' '' + convert(varchar,convert(time,EVen.EventEndTime),100)) 
	ELSE
		( convert(varchar,convert(Date,ME.StartingFrom) ,106) + '' '' +  convert(varchar,ME.StartTime)  + ''-'' +
		CONVERT(VARCHAR,convert(Date,Me.StartingTo),106 ) + ''-'' + CONVERT(VARCHAR,Me.EndTime)) 
	END ) as EventTiming ,ME.StartingFrom as	StartingFrom ,ISNULL(Feature,0) as Feature,
(select top 1 Eod.Orgnizer_Name from  Event_Orgnizer_Detail EOD where  EOD.Orgnizer_Event_Id=Ev.EventID and EOD.DefaultOrg=''Y'' ) 
as  Orgnizer_Name,
(select top 1 AD.ConsolidateAddress from Address AD where  AD.EventId= EV.EventID ) as EventAddress,
(select top 1 AD.VenueName from Address AD where  AD.EventId= EV.EventID ) as Venuename,
anr.Email,
(SELECT   convert(varchar,isnull( sum(TQD_Remaining_Quantity),0)) 
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
	WHERE Ev.EventTitle like ''%'' + Coalesce(@EventTitle,Ev.EventTitle) + ''%''
	AND isnull(Ev.EventTypeID,'''')  = CASE WHEN @EventType >0 Then @EventType Else isnull(Ev.EventTypeID,'''') END
	AND isnull(Ev.EventCategoryID,0)  = CASE WHEN @EventCat >0 Then @EventCat Else isnull(Ev.EventCategoryID,0) END
	AND isnull(Ev.EventSubCategoryID,0)  = CASE WHEN @EventSubCat >0 Then @EventSubCat  Else isnull(Ev.EventSubCategoryID,0) END
	AND ISNULL(Ev.Feature,0) = CASE WHEN @EventFeature >0 Then @EventFeature  Else ISNULL(Ev.Feature,0) END 
	AND (SELECT   isnull( sum(TQD_Remaining_Quantity),0)
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) <=0
	end
	if(@EventStartdate=0 and @EventTicket=0)
	begin
	select  EV.EventID as EventID , EventTitle,ET.EventType AS EventType,EC.EventCategory as EventCategory , ESC.EventSubCategory as EventSubCategory ,
	(CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		( convert(varchar, convert(Date,EVen.EventStartDate),106) +'' '' +  convert(varchar,convert(time,EVen.EventStartTime),100)  + ''-'' +
		convert(varchar, convert(Date,EVen.EventEndDate),106) + '' '' + convert(varchar,convert(time,EVen.EventEndTime),100)) 
	ELSE
		( convert(varchar,convert(Date,ME.StartingFrom) ,106) + '' '' +  convert(varchar,ME.StartTime)  + ''-'' +
		CONVERT(VARCHAR,convert(Date,Me.StartingTo),106 ) + ''-'' + CONVERT(VARCHAR,Me.EndTime)) 
	END ) as EventTiming ,ME.StartingFrom as	StartingFrom ,ISNULL(Feature,0) as Feature,
(select top 1 Eod.Orgnizer_Name from  Event_Orgnizer_Detail EOD where  EOD.Orgnizer_Event_Id=Ev.EventID and EOD.DefaultOrg=''Y'' ) 
as  Orgnizer_Name,
(select top 1 AD.ConsolidateAddress from Address AD where  AD.EventId= EV.EventID ) as EventAddress,
(select top 1 AD.VenueName from Address AD where  AD.EventId= EV.EventID ) as Venuename,
anr.Email,
(SELECT   convert(varchar,isnull( sum(TQD_Remaining_Quantity),0)) 
   
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
	WHERE Ev.EventTitle like ''%'' + Coalesce(@EventTitle,Ev.EventTitle) + ''%''
	AND isnull(Ev.EventTypeID,'''')  = CASE WHEN @EventType >0 Then @EventType Else isnull(Ev.EventTypeID,'''') END
	AND isnull(Ev.EventCategoryID,0)  = CASE WHEN @EventCat >0 Then @EventCat Else isnull(Ev.EventCategoryID,0) END
	AND isnull(Ev.EventSubCategoryID,0)  = CASE WHEN @EventSubCat >0 Then @EventSubCat  Else isnull(Ev.EventSubCategoryID,0) END
	AND ISNULL(Ev.Feature,0) = CASE WHEN @EventFeature >0 Then @EventFeature  Else ISNULL(Ev.Feature,0) END 
	end
 if(@EventStartdate=1 and  @EventTicket=0 )
	begin 
	select  EV.EventID as EventID , EventTitle,ET.EventType AS EventType,EC.EventCategory as EventCategory , ESC.EventSubCategory as EventSubCategory ,
	(CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		( convert(varchar, convert(Date,EVen.EventStartDate),106) +'' '' +  convert(varchar,convert(time,EVen.EventStartTime),100)  + ''-'' +
		convert(varchar, convert(Date,EVen.EventEndDate),106) + '' '' + convert(varchar,convert(time,EVen.EventEndTime),100)) 
	ELSE
		( convert(varchar,convert(Date,ME.StartingFrom) ,106) + '' '' +  convert(varchar,ME.StartTime)  + ''-'' +
		CONVERT(VARCHAR,convert(Date,Me.StartingTo),106 ) + ''-'' + CONVERT(VARCHAR,Me.EndTime)) 
	END ) as EventTiming ,ME.StartingFrom as	StartingFrom ,ISNULL(Feature,0) as Feature,
(select top 1 Eod.Orgnizer_Name from  Event_Orgnizer_Detail EOD where  EOD.Orgnizer_Event_Id=Ev.EventID and EOD.DefaultOrg=''Y'' ) 
as  Orgnizer_Name,
(select top 1 AD.ConsolidateAddress from Address AD where  AD.EventId= EV.EventID ) as EventAddress,
(select top 1 AD.VenueName from Address AD where  AD.EventId= EV.EventID ) as Venuename,
anr.Email,
(SELECT   convert(varchar,isnull( sum(TQD_Remaining_Quantity),0)) 
   
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
	WHERE Ev.EventTitle like ''%'' + Coalesce(@EventTitle,Ev.EventTitle) + ''%''
	AND isnull(Ev.EventTypeID,'''')  = CASE WHEN @EventType >0 Then @EventType Else isnull(Ev.EventTypeID,'''') END
	AND isnull(Ev.EventCategoryID,0)  = CASE WHEN @EventCat >0 Then @EventCat Else isnull(Ev.EventCategoryID,0) END
	AND isnull(Ev.EventSubCategoryID,0)  = CASE WHEN @EventSubCat >0 Then @EventSubCat  Else isnull(Ev.EventSubCategoryID,0) END
	AND ISNULL(Ev.Feature,0) = CASE WHEN @EventFeature >0 Then @EventFeature  Else ISNULL(Ev.Feature,0) END 
	AND convert(date,Me.StartingFrom)>=GETDATE()
	end
 if(@EventStartdate=2 and @EventTicket=0)
	begin 
	select  EV.EventID as EventID , EventTitle,ET.EventType AS EventType,EC.EventCategory as EventCategory , ESC.EventSubCategory as EventSubCategory ,
	(CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		( convert(varchar, convert(Date,EVen.EventStartDate),106) +'' '' +  convert(varchar,convert(time,EVen.EventStartTime),100)  + ''-'' +
		convert(varchar, convert(Date,EVen.EventEndDate),106) + '' '' + convert(varchar,convert(time,EVen.EventEndTime),100)) 
	ELSE
		( convert(varchar,convert(Date,ME.StartingFrom) ,106) + '' '' +  convert(varchar,ME.StartTime)  + ''-'' +
		CONVERT(VARCHAR,convert(Date,Me.StartingTo),106 ) + ''-'' + CONVERT(VARCHAR,Me.EndTime)) 
	END ) as EventTiming ,ME.StartingFrom as	StartingFrom ,ISNULL(Feature,0) as Feature,
(select top 1 Eod.Orgnizer_Name from  Event_Orgnizer_Detail EOD where  EOD.Orgnizer_Event_Id=Ev.EventID and EOD.DefaultOrg=''Y'' ) 
as  Orgnizer_Name,
(select top 1 AD.ConsolidateAddress from Address AD where  AD.EventId= EV.EventID ) as EventAddress,
(select top 1 AD.VenueName from Address AD where  AD.EventId= EV.EventID ) as Venuename,
anr.Email,
(SELECT   convert(varchar,isnull( sum(TQD_Remaining_Quantity),0)) 
   
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
	WHERE Ev.EventTitle like ''%'' + Coalesce(@EventTitle,Ev.EventTitle) + ''%''
	AND isnull(Ev.EventTypeID,'''')  = CASE WHEN @EventType >0 Then @EventType Else isnull(Ev.EventTypeID,'''') END
	AND isnull(Ev.EventCategoryID,0)  = CASE WHEN @EventCat >0 Then @EventCat Else isnull(Ev.EventCategoryID,0) END
	AND isnull(Ev.EventSubCategoryID,0)  = CASE WHEN @EventSubCat >0 Then @EventSubCat  Else isnull(Ev.EventSubCategoryID,0) END
	AND ISNULL(Ev.Feature,0) = CASE WHEN @EventFeature >0 Then @EventFeature  Else ISNULL(Ev.Feature,0) END 
	AND convert(date,Me.StartingFrom)<GETDATE()
	end

if(@EventStartdate=1 and @EventTicket=1)
	begin
		select  EV.EventID as EventID , EventTitle,ET.EventType AS EventType,EC.EventCategory as EventCategory , ESC.EventSubCategory as EventSubCategory ,
	(CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		( convert(varchar, convert(Date,EVen.EventStartDate),106) +'' '' +  convert(varchar,convert(time,EVen.EventStartTime),100)  + ''-'' +
		convert(varchar, convert(Date,EVen.EventEndDate),106) + '' '' + convert(varchar,convert(time,EVen.EventEndTime),100)) 
	ELSE
		( convert(varchar,convert(Date,ME.StartingFrom) ,106) + '' '' +  convert(varchar,ME.StartTime)  + ''-'' +
		CONVERT(VARCHAR,convert(Date,Me.StartingTo),106 ) + ''-'' + CONVERT(VARCHAR,Me.EndTime)) 
	END ) as EventTiming ,ME.StartingFrom as	StartingFrom ,ISNULL(Feature,0) as Feature,
(select top 1 Eod.Orgnizer_Name from  Event_Orgnizer_Detail EOD where  EOD.Orgnizer_Event_Id=Ev.EventID and EOD.DefaultOrg=''Y'' ) 
as  Orgnizer_Name,
(select top 1 AD.ConsolidateAddress from Address AD where  AD.EventId= EV.EventID ) as EventAddress,
(select top 1 AD.VenueName from Address AD where  AD.EventId= EV.EventID ) as Venuename,
anr.Email,
(SELECT    convert(varchar,isnull( sum(TQD_Remaining_Quantity),0)) 
   
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
	WHERE Ev.EventTitle like ''%'' + Coalesce(@EventTitle,Ev.EventTitle) + ''%''
	AND isnull(Ev.EventTypeID,'''')  = CASE WHEN @EventType >0 Then @EventType Else isnull(Ev.EventTypeID,'''') END
	AND isnull(Ev.EventCategoryID,0)  = CASE WHEN @EventCat >0 Then @EventCat Else isnull(Ev.EventCategoryID,0) END
	AND isnull(Ev.EventSubCategoryID,0)  = CASE WHEN @EventSubCat >0 Then @EventSubCat  Else isnull(Ev.EventSubCategoryID,0) END
	AND ISNULL(Ev.Feature,0) = CASE WHEN @EventFeature >0 Then @EventFeature  Else ISNULL(Ev.Feature,0) END 
	AND convert(date,Me.StartingFrom)>=GETDATE()
	AND (SELECT    isnull( sum(TQD_Remaining_Quantity),0)
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) <=''No''
	end
 if(@EventStartdate=1 and @EventTicket=2)
	begin
		select  EV.EventID as EventID , EventTitle,ET.EventType AS EventType,EC.EventCategory as EventCategory , ESC.EventSubCategory as EventSubCategory ,
	(CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		( convert(varchar, convert(Date,EVen.EventStartDate),106) +'' '' +  convert(varchar,convert(time,EVen.EventStartTime),100)  + ''-'' +
		convert(varchar, convert(Date,EVen.EventEndDate),106) + '' '' + convert(varchar,convert(time,EVen.EventEndTime),100)) 
	ELSE
		( convert(varchar,convert(Date,ME.StartingFrom) ,106) + '' '' +  convert(varchar,ME.StartTime)  + ''-'' +
		CONVERT(VARCHAR,convert(Date,Me.StartingTo),106 ) + ''-'' + CONVERT(VARCHAR,Me.EndTime)) 
	END ) as EventTiming ,ME.StartingFrom as	StartingFrom ,ISNULL(Feature,0) as Feature,
(select top 1 Eod.Orgnizer_Name from  Event_Orgnizer_Detail EOD where  EOD.Orgnizer_Event_Id=Ev.EventID and EOD.DefaultOrg=''Y'' ) 
as  Orgnizer_Name,
(select top 1 AD.ConsolidateAddress from Address AD where  AD.EventId= EV.EventID ) as EventAddress,
(select top 1 AD.VenueName from Address AD where  AD.EventId= EV.EventID ) as Venuename,
anr.Email,
(SELECT    convert(varchar,isnull( sum(TQD_Remaining_Quantity),0)) 
   
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
	WHERE Ev.EventTitle like ''%'' + Coalesce(@EventTitle,Ev.EventTitle) + ''%''
	AND isnull(Ev.EventTypeID,'''')  = CASE WHEN @EventType >0 Then @EventType Else isnull(Ev.EventTypeID,'''') END
	AND isnull(Ev.EventCategoryID,0)  = CASE WHEN @EventCat >0 Then @EventCat Else isnull(Ev.EventCategoryID,0) END
	AND isnull(Ev.EventSubCategoryID,0)  = CASE WHEN @EventSubCat >0 Then @EventSubCat  Else isnull(Ev.EventSubCategoryID,0) END
	AND ISNULL(Ev.Feature,0) = CASE WHEN @EventFeature >0 Then @EventFeature  Else ISNULL(Ev.Feature,0) END 
	AND convert(date,Me.StartingFrom)>=GETDATE()
	AND (SELECT   isnull( sum(TQD_Remaining_Quantity),0)
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) >0
	end
  
   if(@EventStartdate=2 and @EventTicket=1)
	begin
		select  EV.EventID as EventID , EventTitle,ET.EventType AS EventType,EC.EventCategory as EventCategory , ESC.EventSubCategory as EventSubCategory ,
	(CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		( convert(varchar, convert(Date,EVen.EventStartDate),106) +'' '' +  convert(varchar,convert(time,EVen.EventStartTime),100)  + ''-'' +
		convert(varchar, convert(Date,EVen.EventEndDate),106) + '' '' + convert(varchar,convert(time,EVen.EventEndTime),100)) 
	ELSE
		( convert(varchar,convert(Date,ME.StartingFrom) ,106) + '' '' +  convert(varchar,ME.StartTime)  + ''-'' +
		CONVERT(VARCHAR,convert(Date,Me.StartingTo),106 ) + ''-'' + CONVERT(VARCHAR,Me.EndTime)) 
	END ) as EventTiming ,ME.StartingFrom as	StartingFrom ,ISNULL(Feature,0) as Feature,
(select top 1 Eod.Orgnizer_Name from  Event_Orgnizer_Detail EOD where  EOD.Orgnizer_Event_Id=Ev.EventID and EOD.DefaultOrg=''Y'' ) 
as  Orgnizer_Name,
(select top 1 AD.ConsolidateAddress from Address AD where  AD.EventId= EV.EventID ) as EventAddress,
(select top 1 AD.VenueName from Address AD where  AD.EventId= EV.EventID ) as Venuename,
anr.Email,
(SELECT    convert(varchar,isnull( sum(TQD_Remaining_Quantity),0)) 
   
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
	WHERE Ev.EventTitle like ''%'' + Coalesce(@EventTitle,Ev.EventTitle) + ''%''
	AND isnull(Ev.EventTypeID,'''')  = CASE WHEN @EventType >0 Then @EventType Else isnull(Ev.EventTypeID,'''') END
	AND isnull(Ev.EventCategoryID,0)  = CASE WHEN @EventCat >0 Then @EventCat Else isnull(Ev.EventCategoryID,0) END
	AND isnull(Ev.EventSubCategoryID,0)  = CASE WHEN @EventSubCat >0 Then @EventSubCat  Else isnull(Ev.EventSubCategoryID,0) END
	AND ISNULL(Ev.Feature,0) = CASE WHEN @EventFeature >0 Then @EventFeature  Else ISNULL(Ev.Feature,0) END 
	AND convert(date,Me.StartingFrom)<GETDATE()
	AND (SELECT   isnull( sum(TQD_Remaining_Quantity),0)
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) <=0
	end

 if(@EventStartdate=2 and @EventTicket=2)
	begin
		select  EV.EventID as EventID , EventTitle,ET.EventType AS EventType,EC.EventCategory as EventCategory , ESC.EventSubCategory as EventSubCategory ,
	(CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		( convert(varchar, convert(Date,EVen.EventStartDate),106) +'' '' +  convert(varchar,convert(time,EVen.EventStartTime),100)  + ''-'' +
		convert(varchar, convert(Date,EVen.EventEndDate),106) + '' '' + convert(varchar,convert(time,EVen.EventEndTime),100)) 
	ELSE
		( convert(varchar,convert(Date,ME.StartingFrom) ,106) + '' '' +  convert(varchar,ME.StartTime)  + ''-'' +
		CONVERT(VARCHAR,convert(Date,Me.StartingTo),106 ) + ''-'' + CONVERT(VARCHAR,Me.EndTime)) 
	END ) as EventTiming ,ME.StartingFrom as	StartingFrom ,ISNULL(Feature,0) as Feature,
(select top 1 Eod.Orgnizer_Name from  Event_Orgnizer_Detail EOD where  EOD.Orgnizer_Event_Id=Ev.EventID and EOD.DefaultOrg=''Y'' ) 
as  Orgnizer_Name,
(select top 1 AD.ConsolidateAddress from Address AD where  AD.EventId= EV.EventID ) as EventAddress,
(select top 1 AD.VenueName from Address AD where  AD.EventId= EV.EventID ) as Venuename,
anr.Email,
(SELECT    convert(varchar,isnull( sum(TQD_Remaining_Quantity),0)) 
   
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
	WHERE Ev.EventTitle like ''%'' + Coalesce(@EventTitle,Ev.EventTitle) + ''%''
	AND isnull(Ev.EventTypeID,'''')  = CASE WHEN @EventType >0 Then @EventType Else isnull(Ev.EventTypeID,'''') END
	AND isnull(Ev.EventCategoryID,0)  = CASE WHEN @EventCat >0 Then @EventCat Else isnull(Ev.EventCategoryID,0) END
	AND isnull(Ev.EventSubCategoryID,0)  = CASE WHEN @EventSubCat >0 Then @EventSubCat  Else isnull(Ev.EventSubCategoryID,0) END
	AND ISNULL(Ev.Feature,0) = CASE WHEN @EventFeature >0 Then @EventFeature  Else ISNULL(Ev.Feature,0) END 
	AND convert(date,Me.StartingFrom)<GETDATE()
	AND (SELECT   isnull( sum(TQD_Remaining_Quantity),0)
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) >0
	end
  
	
END




' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetEventsListByStatus]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetEventsListByStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- GetEventsListByStatus '''',''LIVE'',''e583e85c-bf2f-43e4-9d56-5a20c73796be''
CREATE Procedure [dbo].[GetEventsListByStatus]

@EventTitle nvarchar(200),

@EventStatus varchar(20),
@UserID nvarchar(100)

as

BEGIN

IF UPPER(@EventStatus)=''LIVE''
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
	from (Select distinct  (p.FirstName+''  ''+p.LastName) as Name,P.Email as EMail, a.UserID, a.EventID,
	'''' as Date_Time,'''' as TicketType,CONVERT(bigint, 0) as TicketPurchased ,'''' as OrderId,LEFT(a.EventTitle,200) as EventTitle,
	isnull(b.StartingFrom,'''')  as EventDate,
	isnull(b.StartingTo,'''') as EventEnddate,
	(select ISNULL(SUM(TQD_Quantity),0) from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TotalTicket,
	(select ISNULL(SUM(TQD_Quantity)- SUM(TQD_Remaining_Quantity),0)   from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TicketSold,
	a.EventStatus,isnull(b.StartTime,'''') as EndTime
	from event  a inner join MultipleEvent b on a.EventID =b.EventID 
	inner join Profile p on a.UserID=p.UserID 
	UNION 

	Select distinct (p.FirstName+''  ''+p.LastName) as Name,P.Email as EMail, a.UserID, a.EventID,
	'''' as Date_Time,'''' as TicketType,CONVERT(bigint, 0) as TicketPurchased ,'''' as OrderId, LEFT(a.EventTitle,200) as EventTitle,
	 isnull(b.EventStartDate,'''')  as EventDate,isnull(b.EventEndDate,'''') as EventEnddate,
	
	(select ISNULL(SUM(TQD_Quantity),0) from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TotalTicket,
	(select ISNULL(SUM(TQD_Quantity)- SUM(TQD_Remaining_Quantity),0)   from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TicketSold,
	a.EventStatus,
	isnull(b.EventStartTime,'''') as EndTime
	 from event  a inner join EventVenue b on a.EventID =b.EventID 
	 inner join Profile p on a.UserID=p.UserID 

	UNION 
		 
	Select distinct (p.FirstName+''  ''+p.LastName) as Name ,P.Email as EMail,a.UserID,a.EventID, 
	'''' as Date_Time,'''' as TicketType,CONVERT(bigint, 0) as TicketPurchased ,'''' as OrderId,LEFT(a.EventTitle,200) as EventTitle,
		''''  as EventDate,'''' as EventEnddate,

	(select ISNULL(SUM(TQD_Quantity),0) from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TotalTicket,
	(select ISNULL(SUM(TQD_Quantity)- SUM(TQD_Remaining_Quantity),0)   from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TicketSold,
	a.EventStatus,
	'''' as EndTime
	 from event  a  
	 inner join Profile p on a.UserID=p.UserID 
	  where 
	(select count(*) from EventVenue b where b.EventID=a.EventID )<=0
	and (select count(*) from MultipleEvent b where b.EventID=a.EventID )<=0    ) as xEven

	 where xEven.EventStatus=''Live''  and convert(datetime,xEven.EventEnddate)>=GETDATE() and xEven.UserID=@UserID

	 and  EventTitle like ''%''+ISNULL(@EventTitle,'''')+''%'' 
	 ) as LiveEventList where EventID in (Select Ev from GrpEvent) order by convert(datetime,EventDate) asc

	


END
ELSE IF UPPER(@EventStatus)=''SAVE''

	
	select Name,Email,Date_Time,TicketType,TicketPurchased,OrderId,EventID,EventTitle,EventDate,
	TotalTicket,TicketSold,EndTime as EventTime
	 from  ( Select (p.FirstName+''  ''+p.LastName) as Name,P.Email as EMail, a.UserID, a.EventID,
	'''' as Date_Time,'''' as TicketType,CONVERT(bigint, 0) as TicketPurchased ,'''' as OrderId,LEFT(a.EventTitle,200) as EventTitle,
	 isnull(b.StartingFrom,'''')  as EventDate,
 isnull(b.StartingTo,'''') as EventEnddate,
(select ISNULL(SUM(TQD_Quantity),0) from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TotalTicket,
(select ISNULL(SUM(TQD_Quantity)- SUM(TQD_Remaining_Quantity),0)   from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TicketSold,
a.EventStatus,isnull(b.StartTime,'''') as EndTime
 from event  a inner join MultipleEvent b on a.EventID =b.EventID 
 inner join Profile p on a.UserID=p.UserID 
	union 

	Select (p.FirstName+''  ''+p.LastName) as Name,P.Email as EMail, a.UserID, a.EventID,
	'''' as Date_Time,'''' as TicketType,CONVERT(bigint, 0) as TicketPurchased ,'''' as OrderId, LEFT(a.EventTitle,200) as EventTitle,
	 isnull(b.EventStartDate,'''')  as EventDate,isnull(b.EventEndDate,'''') as EventEnddate,
	
(select ISNULL(SUM(TQD_Quantity),0) from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TotalTicket,
(select ISNULL(SUM(TQD_Quantity)- SUM(TQD_Remaining_Quantity),0)   from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TicketSold,
a.EventStatus,
isnull(b.EventStartTime,'''') as EndTime
 from event  a inner join EventVenue b on a.EventID =b.EventID 
 inner join Profile p on a.UserID=p.UserID 
	union 
		 
	 	Select  (p.FirstName+''  ''+p.LastName) as Name ,P.Email as EMail,a.UserID,a.EventID, 
	'''' as Date_Time,'''' as TicketType,CONVERT(bigint, 0) as TicketPurchased ,'''' as OrderId,LEFT(a.EventTitle,200) as EventTitle,
		''''  as EventDate,'''' as EventEnddate,

(select ISNULL(SUM(TQD_Quantity),0) from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TotalTicket,
(select ISNULL(SUM(TQD_Quantity)- SUM(TQD_Remaining_Quantity),0)   from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TicketSold,
a.EventStatus,
'''' as EndTime
 from event  a  
 inner join Profile p on a.UserID=p.UserID 
  where 
(select count(*) from EventVenue b where b.EventID=a.EventID )<=0
	 and (select count(*) from MultipleEvent b where b.EventID=a.EventID )<=0    ) as xEven

	 where xEven.EventStatus=''Save''  and convert(datetime,xEven.EventEnddate)>=GETDATE() and xEven.UserID=@UserID

	 and  EventTitle like ''%''+ISNULL(@EventTitle,'''')+''%'' 
	 order by convert(datetime,xeven.EventDate) asc

	

ELSE IF UPPER(@EventStatus)=''PAST''

	
	select Name,Email,Date_Time,TicketType,TicketPurchased,OrderId,EventID,EventTitle,EventDate,
	TotalTicket,TicketSold,EndTime as EventTime
	 from  ( Select (p.FirstName+''  ''+p.LastName) as Name,P.Email as EMail, a.UserID, a.EventID,
	'''' as Date_Time,'''' as TicketType,CONVERT(bigint, 0) as TicketPurchased ,'''' as OrderId,LEFT(a.EventTitle,200) as EventTitle,
	 isnull(b.StartingFrom,'''')  as EventDate,
 isnull(b.StartingTo,'''') as EventEnddate,
(select ISNULL(SUM(TQD_Quantity),0) from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TotalTicket,
(select ISNULL(SUM(TQD_Quantity)- SUM(TQD_Remaining_Quantity),0)   from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TicketSold,
a.EventStatus,isnull(b.StartTime,'''') as EndTime
 from event  a inner join MultipleEvent b on a.EventID =b.EventID 
 inner join Profile p on a.UserID=p.UserID 
	union 

	Select (p.FirstName+''  ''+p.LastName) as Name,P.Email as EMail, a.UserID, a.EventID,
	'''' as Date_Time,'''' as TicketType,CONVERT(bigint, 0) as TicketPurchased ,'''' as OrderId, LEFT(a.EventTitle,200) as EventTitle,
	 isnull(b.EventStartDate,'''')  as EventDate,isnull(b.EventEndDate,'''') as EventEnddate,
	
(select ISNULL(SUM(TQD_Quantity),0) from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TotalTicket,
(select ISNULL(SUM(TQD_Quantity)- SUM(TQD_Remaining_Quantity),0)   from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TicketSold,
a.EventStatus,
isnull(b.EventStartTime,'''') as EndTime
 from event  a inner join EventVenue b on a.EventID =b.EventID 
 inner join Profile p on a.UserID=p.UserID 
	union 
		 
	 	Select  (p.FirstName+''  ''+p.LastName) as Name ,P.Email as EMail,a.UserID,a.EventID, 
	'''' as Date_Time,'''' as TicketType,CONVERT(bigint, 0) as TicketPurchased ,'''' as OrderId,LEFT(a.EventTitle,200) as EventTitle,
		''''  as EventDate,'''' as EventEnddate,

(select ISNULL(SUM(TQD_Quantity),0) from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TotalTicket,
(select ISNULL(SUM(TQD_Quantity)- SUM(TQD_Remaining_Quantity),0)   from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TicketSold,
a.EventStatus,
'''' as EndTime
 from event  a  
 inner join Profile p on a.UserID=p.UserID 
  where 
(select count(*) from EventVenue b where b.EventID=a.EventID )<=0
	 and (select count(*) from MultipleEvent b where b.EventID=a.EventID )<=0    ) as xEven

	 where convert(datetime,xEven.EventEnddate)<GETDATE() and xEven.UserID=@UserID

	 and  EventTitle like ''%''+ISNULL(@EventTitle,'''')+''%'' 
	 order by convert(datetime,xeven.EventDate) asc

	


ELSE IF UPPER(@EventStatus)=''GUEST''

	select (PR.FirstName+'' ''+PR.LastName) as Name,Pr.Email, LEFT(ev.EventTitle,200) as EventTitle,(evn.EventStartDate+'' ''+evn.EventStartTime) as Date_Time,

	tt.TicketType, SUM(TPD_Purchased_Qty) as TicketPurchased,TPD_Order_Id as OrderId,ev.EventID,'''' as EventDate,'''' as EventTime ,CONVERT(bigint, 0) as TotalTicket,CONVERT(bigint, 0) as TicketSold

	from Ticket_Purchased_Detail TPD inner join Profile Pr 

	on TPD.TPD_User_Id=Pr.UserID

	LEFT OUTER JOIN Event ev on TPD.TPD_Event_Id=ev.EventID

	LEFT OUTER JOIN EventVenue evn on TPD.TPD_Event_Id=evn.EventID

	LEFT OUTER JOIN Ticket_Quantity_Detail tqd on TPD.TPD_TQD_Id=tqd.TQD_Id

	LEFT OUTER JOIN Ticket t on tqd.TQD_Ticket_Id=t.T_Id

	LEFT OUTER JOIN TicketType tt on t.TicketTypeID=tt.TicketTypeID

	where ev.UserID=@UserID AND (pr.FirstName like ''%''+ISNULL(@EventTitle,'''') +''%'' or ev.EventTitle like ''%''+ISNULL(@EventTitle,'''') +''%'')

	group by TPD.TPD_User_Id,(PR.FirstName+'' ''+PR.LastName),Pr.Email,EventTitle,TPD_Order_Id,(evn.EventStartDate+'' ''+evn.EventStartTime),tt.TicketType,ev.EventID





END


' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetSelectedTicketListing]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSelectedTicketListing]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'












--[GetSelectedTicketListing ''8e55c866-ef64-4249-8a3e-b81679052fd6'', 96] 

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
DECLARE @PaidTicketQty bigint SET @PaidTicketQty =0
SET @QtyIds =''''


DECLARE @T_Name nvarchar(1000) DECLARE @T_Price DECIMAL(18,2) DECLARE @T_Fee decimal(18,2)
--table-striped evnt_tkt_list_tbl
SET @Html = ''<table class="table table-striped evnt_tkt_list_tbl">''
--select * From Ticket_Quantity_Detail order by TQD_PE_Id
WHILE (@RCnt>0)
BEGIN
	SELECT @PublishId = P_Id FROM @tblPublish WHERE Id= @RInd
--	Select @PublishId
	SELECT @StartDate = (ISNULL(PE_Scheduled_Date,'''') + '', '' + ISNULL(PE_Start_Time,'''')), @AddIdss=isnull(PE_Address_Ids,'''')
	FROM Publish_Event_Detail WHERE PE_Id = @PublishId
	--SET @Html = CONCAT(@Html,''<thead class="tkt_tbl_head">'')
	
	

	SET @Html = CONCAT(@Html,''<thead class="tkt_multi_tab_date"><tr>'', ''<td class="book_tkt_date" colspan=4>'',@StartDate, '''' ,''</td></tr></thead>'')
	if (RTRIM(LTRIM(@AddIdss)) ='''')
	BEGIN
		SET @Html = CONCAT(@Html,''<thead class="tkt_tbl_head"> <tr>'')
		SET @Html = CONCAT(@Html,''<td style="width:40%">Ticket Type</td>'')
		--SET @Html = CONCAT(@Html,''<td>Sales End</td>'')
		SET @Html = CONCAT(@Html,''<td align="right">'', ''Price'',''</td>'')
		SET @Html =	CONCAT(@Html,''<td align="right">Fee</td>'')
		SET @Html = CONCAT(@Html,''<td align="right">Quantity</td>'')
		--SET @Html = CONCAT(@Html,''<td>Price</td>'')
		SET @Html = CONCAT(@Html,''</tr> </thead>'')
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
		IF (@QtyIds='''') SET @QtyIds = CONCAT(@QtyIds,@TQTY_Id)  ELSE SET @QtyIds = CONCAT(@QtyIds,'','',@TQTY_Id) 
		
		SELECT @T_Name = T_name,@T_Price=isnull(Price,0),@TotalPrice = isnull(TotalPrice,0), @TQty= Qty_Available, @TicketSaleEnd = CONCAT(CONVERT(VARCHAR,Sale_End_Date,107),Sale_End_Time)
		, @TicTypeId = TicketTypeID,@TicketDesc=T_Desc,@IsShowDesc = Show_T_Desc,@HideTicket = Hide_Ticket ,
		@HideUntillDate = Hide_untill_Date, @HideUntillFromTime = Hide_Untill_Time,@HideToDate= Hide_After_Date,@HideUntillToTime=Hide_After_Time,
		@Discount = ISNULL(T_Discount ,0)
		FROM Ticket WHERE T_id= @TicId



		IF (@NextAddId != @AddId)	
		BEGIN
			SET @NextAddId = @AddId 
			SELECT @Addess = ConsolidateAddress FROM ADDRESS WHERE AddressID = @AddId
			SET @Html = CONCAT(@Html,''<thead class="tkt_multi_tab_date"><tr valign="bottom">'',''<td valign="bottom" class="book_tkt_desc" colspan=4>'',@Addess,''</td></tr></thead>'')
			SET @Html = CONCAT(@Html,''<thead class="tkt_tbl_head"> <tr>'')
			SET @Html = CONCAT(@Html,''<td style="width:40%">Ticket Type</td>'')
			SET @Html = CONCAT(@Html,''<td align="right">'', ''Price'',''</td>'')
			SET @Html =	CONCAT(@Html,''<td align="right">Fee</td>'')
			SET @Html = CONCAT(@Html,''<td align="right">Quantity</td>'')
			SET @Html = CONCAT(@Html,''</tr> </thead>'')
		END
		

		
		SELECT @RemQty = TQD_Remaining_Quantity FROM Ticket_Quantity_Detail WHERE TQD_Id = @TQTY_Id
		Set @donation=0
		SELECT @selectedQty=TLD_Locked_Qty,@donation = ISNULL(TLD_Donate,0) FROM Ticket_Locked_Detail 
		WHERE TLD_GUID = @GUID and TLD_TQD_Id = @TQTY_Id

		SET @T_Fee = (@TotalPrice - @T_Price)
		
		IF (@TicTypeId = 2 or @TicTypeId = 1)  
		Begin
			SET @PaidTicketQty = @PaidTicketQty + @selectedQty
		END


		if (@HideTicket !=1)
		BEGIN

			if (@IsShowDesc = 1 AND isnull(@TicketDesc,'''') != '''')
			BEGIN
				SET @Html = CONCAT(@Html,''<tr><td style="width:40%">'', @T_Name,''<br><span class="tkt_more_desc">  <i onclick="checkMore('',@TQTY_Id,'')">[More Info]</i></span>'', ''<span style="display:none;" id="Sp_'',@TQTY_Id,''"class="col-sm-12 no_pad">'',@TicketDesc,''</span>'' ,''</td>'')
			END
			ELSE		
			BEGIN
				SET @Html = CONCAT(@Html,''<tr><td style="width:40%">'', @T_Name,''<br>'', ''<span style="display:none;" id="Sp_'',@TQTY_Id,''"class="col-sm-12 no_pad">'',@TicketDesc,''</span>'' ,''</td>'')
			END

		

		--SET @T_Price = CASE WHEN @T_Price =  THEN 0 ELSE COnvert(decimal,@T_Price) END
		if (@donation>0) SET @Html = CONCAT(@Html,''<td align="right">$'',@donation,''</td>'')
		
		Else 
			BEGIN 
				if (@Discount>0)
				BEGIN
					SET @TotalPrice = (@T_Price  - @Discount) + @T_Fee 
					SET @Html = CONCAT(@Html,''<td align="right">'',''<strike>'',@T_Price,''</strike> $'',(@T_Price-@Discount),''</td>'') 

				END
				ELSE
				BEGIN
					SET @Html = CONCAT(@Html,''<td align="right">$'',@T_Price,''</td>'') 
				END
			END

		SET @Html = CONCAT(@Html,''<td align="right">$'',@T_Fee,''</td>'')
		SET @Html = CONCAT(@Html,''<td align="right">'',@selectedQty,''</td>'')
		
		--SET @Html = CONCAT(@Html,''<td>$'',(@selectedQty * @T_Price) ,''</td>'')
		SET @RunningTotal = @RunningTotal  + (@selectedQty * @TotalPrice) + @donation

		--SET @Html = CONCAT(@Html,''<td>'',@TicketSaleEnd,''</td>'')
		
			--IF (@TicTypeId = 2)  
			--BEGIN
			--	--SET @Html = CONCAT(@Html,''<td>$'',@T_Price,''</td>'')
			--	--SET @Html = CONCAT(@Html,''<td>$'',@T_Fee,''</td>'')
			--END
			--ELSE IF (@TicTypeId = 1)  
			--BEGIN
			--	SET @Html = CONCAT(@Html,''<td>$'',''<span id="P_'', @TQTY_Id, ''">'',''0'',''</span>'',''</td>'')
			--	SET @Html = CONCAT(@Html,''<td>$'',''0.00'',''</td>'')
			--END
			--ELSE IF (@TicTypeId = 3)  
			--BEGIN
			--	SET @Html = CONCAT(@Html,''<td align="left" colspan="2">'',''$<input type="text"  onchange="calculateTickTotal()" placeholder="Donate" id="txtd_'',@TQTY_Id,''" value="" />'',''</td>'')
			--END
		
			--SET @Html = CONCAT(@Html,''<td><select onchange="calculateTickTotal()" id=d_'' ,CONVERT(VARCHAR,@TQTY_Id) ,''>'', @TMaxQty,''</select></td>'')

			SET @Html = CONCAT(@Html,''</tr>'')
		END
		SET @RowCount = @RowCount  -1
		SET @RowIndex = @RowIndex  +1
	END

	--GetSelectedTicketListing 0

	

	SET @RCnt = @RCnt -1
	SET @RInd = @RInd +1
END


--declare @Html varchar(Max)
--SET @Html =''</table>''
--SET @Html = CONCAT(@Html,''First'')
--SET @Html = CONCAT(@Html, ''Second'')
--SET @Html += ''<td>Satnam Waheguru</td>''
--SET @Html += ''</tr>''
DECLARE @VariableChanges DECIMAL(18,2) SET @VariableChanges =0 DECLARE @VariableId bigint SET @VariableId =0
DECLARE @VariableName nvarchar(512) SET @VariableName =''''
DECLARE @VariableDesc nvarchar(512) SET @VariableDesc =''''
DECLARE @VariableType nvarchar(512) SET @VariableType =''''

DECLARE @VariablePrice DECIMAL(18,2) SET @VariablePrice=0
DECLARE @GrandTotal DECIMAL(18,2) SET @GrandTotal =0
DECLARE @VarHTML nvarchar(MAX) SET @VarHTML = ''''
DECLARE @TblVar TABLE (Id INT IDENTITY,vDec NVARCHAR(512),Price DECIMAL) 
DECLARE @RowCnt int  SET @RowCnt =0
Select  @VariableDesc =ISNULL(Ticket_variabledesc,'''') ,@VariableType = LTRIM(RTRIM(ISNULL(Ticket_variabletype,''''))) From Event where EventID = @EventId

INSERT INTO @TblVar select VariableDesc,Price From Event_VariableDesc WHERE Event_Id = @EventId  
SET @RowCount = @@ROWCOUNT
SET @RowIndex =1
SET @RowCnt = @RowCount
if (@RowCount>0)
BEGIN
	SET @VarHTML = CONCAT(@VarHTML,''<tr> <td colspan="4" style="padding:0px"><h2 style="background:#aaa;margin:0px;font-size:20px; padding:5px;color:#fff;">'',@VariableDesc,''</h2></td></tr>'')
END

WHILE (@RowCount>0)
BEGIN
	SELECT @VariableName = ISNULL(vDec,''''),@VariablePrice = ISNULL(Price ,0) FROM @TblVar WHERE Id= @RowIndex
	if (@VariableType = ''R'')
	BEGIN
		if (@RowIndex=1)
		BEGIN
			SET @VarHTML = CONCAT(@VarHTML,''<tr>'')
			SET @VarHTML = CONCAT(@VarHTML,''<td class="tbl_white_bg" colspan="3" align="left">'',''<input type="radio" onclick="calculateTickTotal('',@VariablePrice,'')" id="rd_'',@RowCount,''" name="VChanges" checked="checked" /> '', @VariableName ,''</td>'') 
			SET @VarHTML = CONCAT(@VarHTML,''<td class="tbl_white_bg" align="right"> $'',@VariablePrice,''</td>'') 
			SET @VarHTML = CONCAT(@VarHTML,''</tr>'')
			SET @VariableChanges = @VariablePrice
		END
		ELSE
		BEGIN
			SET @VarHTML = CONCAT(@VarHTML,''<tr>'')
			SET @VarHTML = CONCAT(@VarHTML,''<td class="tbl_white_bg" colspan="3" align="left">'',''<input type="radio" onclick="calculateTickTotal('',@VariablePrice,'')" id="rd_'',@RowCount,''" name="VChanges" /> '',@VariableName,''</td>'') 
			SET @VarHTML = CONCAT(@VarHTML,''<td class="tbl_white_bg" align="right"> $'',@VariablePrice,''</td>'') 
			SET @VarHTML = CONCAT(@VarHTML,''</tr>'')
		END
	END
	ELSE
	BEGIN
		BEGIN
			SET @VarHTML = CONCAT(@VarHTML,''<tr>'')
			SET @VarHTML = CONCAT(@VarHTML,''<td class="tbl_white_bg" colspan="3" align="left">'',''<input type="checkbox" onclick="calculateTickTotalOptional('',@RowCnt,'')" id="chk_'',@RowCount,''" name="VChanges"/> '', @VariableName, ''</td>'') 
			SET @VarHTML = CONCAT(@VarHTML,''<td class="tbl_white_bg" align="right"> $'',''<span id="sp_'',@RowCount,''">'',@VariablePrice,''</span>'',''</td>'') 
			SET @VarHTML = CONCAT(@VarHTML,''</tr>'')
		END
	END
	

	--SELECT @VariableChanges = ISNULL(SUM(ISNULL(Price,0)),0) FROM Event_VariableDesc WHERE Event_Id = @EventId 

	SET @RowCount = @RowCount -1
	SET @RowIndex = @RowIndex +1
END


--SELECT @VariableChanges = ISNULL(SUM(ISNULL(Price,0)),0) FROM Event_VariableDesc WHERE Event_Id = @EventId 
SET @GrandTotal = @RunningTotal + @VariableChanges




SET @Html = CONCAT(@Html,''<tr> <td class="tbl_white_bg" colspan="4" align="right">'',''<b>'',''Order Total : $ '' , CONVERT(NUMERIC(18,2),@RunningTotal) ,''</b>'',''</td></tr>'') 
SET @Html = CONCAT(@Html,@VarHTML) 
--SET @Html = CONCAT(@Html,''<tr> <td class="tbl_white_bg" colspan="5" align="right">'','''', ''Variable Charges $ '' , @VariableChanges ,'''',''</td></tr>'') 
SET @Html = CONCAT(@Html,''<tr> <td class="tbl_white_bg" colspan="4" align="right">'',''<b>'', ''Sub Total : $ '' ,''<span id="spSubTotal">'' , CONVERT(NUMERIC(18,2),(@RunningTotal+@VariableChanges)) ,''</span></b>'',''</td></tr>'') 
SET @Html = CONCAT(@Html,''<tr>'',''<td class="tbl_white_bg" colspan="4" align="right">'', ''<div class="col-sm-10">Promo Code</div>'',''<div class="col-sm-2 no_pad"><input type="text" class="form-control evnt_inp_cont"  placeholder="" id="txtPromoCode'',''" value="" />'',''</div></td></tr>'') 
SET @Html = CONCAT(@Html,''<tr> <td class="tbl_white_bg" colspan="4" align="right">'',''<b>'', ''Grand Total : $ '', ''<span id="spGrdTotal">'',CONVERT(NUMERIC(18,2),@GrandTotal) ,''</span></b>'',''</td></tr>'') 

SET @Html = CONCAT(@Html,''</table>'')
SET @Html = CONCAT(@Html,''<input id="hdIds" type="hidden" value='', @QtyIds ,'' />'')
SET @Html = CONCAT(@Html,''<input id="hdOrderTotal" type="hidden" value='', @RunningTotal ,'' />'')
SET @Html = CONCAT(@Html,''<input id="hdGrandTotal" type="hidden" value='', @GrandTotal ,'' />'')
SET @Html = CONCAT(@Html,''<input id="hdPromoId" type="hidden" value='', ''0'' ,'' />'')
SET @Html = CONCAT(@Html,''<input id="hdVarChanges" type="hidden" value='', @VariableChanges ,'' />'')
SET @Html = CONCAT(@Html,''<input id="hdVarId" type="hidden" value='', @VariableId ,'' />'')
SET @Html = CONCAT(@Html,''<input id="hidQty" type="hidden" value='', @PaidTicketQty ,'' />'')

Select @Html as Ticket


END






















' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetSetUserRole]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSetUserRole]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
create proc [dbo].[GetSetUserRole](
	@user_Id nvarchar(500),
	@GETSET varchar(3),
	@Role_Id varchar(50)
)
AS 
BEGIN
	Declare @RId varchar(50)
	SET @RId =''''
	IF (UPPER(@GETSET) = ''GET'')
	BEGIN
		SELECT @RID =RoleId FROM AspNetUserRoles WHERE UserId = @user_Id 
	END
	ELSE IF (UPPER(@GETSET) = ''SET'')
	BEGIN
		SELECT @RID = Isnull(RoleId,'''') FROM AspNetUserRoles WHERE USERID = @user_Id 
		IF (@RID ='''')
		BEGIN
			DELETE from AspNetUserRoles where USERID = @user_Id
			INSERT INTO AspNetUserRoles (UserId,RoleId) VALUES(@user_Id,@Role_Id)
		END
		ELSE
			UPDATE AspNetUserRoles SET RoleId = @Role_Id WHERE UserId= @user_Id 
	END
	Select @RId

END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetTicketListing]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTicketListing]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'








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
DECLARE @iflag int Declare @SHowRemaingTick varchar(50) SET @SHowRemaingTick='''' Declare @TRemainRuning bigint SET @TRemainRuning=0
DECLARE @HideTicket int Set @HideTicket=0
DECLARE @HideUntillDate varchar(20) DECLARE @HideToDate varchar(20)
DECLARE @HideUntillFromTime varchar(20) DECLARE @HideUntillToTime varchar(20)
DECLARE @Discount decimal(9,2) SET @Discount =0
DECLARE @SaleStartDate VARCHAR(20) SET @SaleStartDate = ''''
DECLARE @SaleEndDate VARCHAR(20) SET @SaleEndDate = ''''

DECLARE @SaleStartTime VARCHAR(20) SET @SaleStartTime = ''''
DECLARE @SaleEndTime VARCHAR(20) SET @SaleEndTime = ''''
DECLARE @FlagHide int 
DECLARE @TicktHideTypeSale int DECLARE @TickatMarkSoldOut int

SET @QtyIds =''''


DECLARE @T_Name nvarchar(1000) DECLARE @T_Price decimal(18,2) DECLARE @T_Fee decimal(18,2)
--table-striped evnt_tkt_list_tbl
SELECT @SHowRemaingTick = Ticket_showremain from Event Where Eventid=@EventId

SET @Html = ''<table class="table table-striped evnt_tkt_list_tbl">''
--select * From Ticket_Quantity_Detail order by TQD_PE_Id
WHILE (@RCnt>0)
BEGIN
	SELECT @PublishId = P_Id FROM @tblPublish WHERE Id= @RInd
--	Select @PublishId
	SELECT @StartDate = (ISNULL(PE_Scheduled_Date,'''') + '', '' + ISNULL(PE_Start_Time,'''')), @AddIdss=isnull(PE_Address_Ids,'''')
	FROM Publish_Event_Detail WHERE PE_Id = @PublishId
	--SET @Html = CONCAT(@Html,''<thead class="tkt_tbl_head">'')
	SET @Html = CONCAT(@Html,''<thead class="tkt_multi_tab_date"><tr>'', ''<td class="book_tkt_date" colspan=5>'',@StartDate,''</td></tr></thead>'')
	
	INSERT INTO @tblMain SELECT TQD_Id FROM Ticket_Quantity_Detail WHERE TQD_PE_Id =@PublishId
	SET @RowCount = @@ROWCOUNT
		IF (RTRIM(LTRIM(@AddIdss)) = '''')	
		BEGIN
			SET @Html = CONCAT(@Html,''<thead class="tkt_tbl_head"> <tr>'')
			SET @Html = CONCAT(@Html,''<td style="width:40%">Ticket Type</td>'')
			SET @Html = CONCAT(@Html,''<td>Sales End</td>'')
			SET @Html = CONCAT(@Html,''<td align="right">'',''Price'',''</td>'')
			SET @Html =	CONCAT(@Html,''<td align="right">Fee</td>'')
			SET @Html = CONCAT(@Html,''<td align="right">Quantity</td>'')
			SET @Html = CONCAT(@Html,''</tr> </thead>'')
		END
		


	WHILE (@RowCount>0)
	BEGIN
		SELECT @TQTY_Id = QTY_Id FROM @tblMain WHERE Id= @RowIndex
		SELECT @AddId = TQD_AddressId, @TicId = TQD_Ticket_Id 
		FROM Ticket_Quantity_Detail WHERE TQD_ID = @TQTY_Id
		--Select concat(@AddId,@NextAddId)
		IF (@QtyIds='''') SET @QtyIds = CONCAT(@QtyIds,@TQTY_Id)  ELSE SET @QtyIds = CONCAT(@QtyIds,'','',@TQTY_Id) 
		
		
		
		--@T_Fee =  CASE WHEN ISNULL(EC_Fee,0)>0 THEN EC_Fee ELSE ISNULL(Customer_Fee,0) END
		SELECT @T_Name = T_name,@T_Price=isnull(Price,0),@TotalPrice = isnull(TotalPrice,0)  ,@TQty= Qty_Available, @TicketSaleEnd = CONCAT(CONVERT(VARCHAR,Sale_End_Date,107),'', '',Sale_End_Time)
		, @TicTypeId = TicketTypeID,@TicketDesc=T_Desc,@IsShowDesc = Show_T_Desc, @MinQty = isnull(Min_T_Qty,0),@MaxQty = isnull(Max_T_Qty,0),@HideTicket = Hide_Ticket,
		@HideUntillDate = isnull(Hide_untill_Date,''''), @HideUntillFromTime = isnull(Hide_Untill_Time,''''),@HideToDate= isnull(Hide_After_Date,''''),@HideUntillToTime=isnull(Hide_After_Time,''''),
		@SaleStartDate = ISNULL(Sale_Start_Date,''''),@SaleEndDate= ISNULL(Sale_End_Date,''''),@Discount = ISNULL(T_Discount ,0),
		@SaleStartTime = ISNULL(Sale_Start_Time,''''),@SaleEndTime = ISNULL(Sale_End_Time ,''''),
		@TicktHideTypeSale = T_AutoSechduleType ,@TickatMarkSoldOut =  ISNULL(T_Mark_SoldOut,0)
		FROM Ticket WHERE T_id= @TicId
		
		if (@SaleStartDate = ''1900-01-01'') SET @SaleStartDate=''''
		if (@SaleEndDate = ''1900-01-01'') SET @SaleEndDate=''''


		if (@SaleStartTime = ''0'') SET @SaleStartTime=''''
		if (@SaleEndTime = ''0'') SET @SaleEndTime=''''
		
		--select @SaleStartDate 
		--select @SaleEndDate
		--select @SaleStartTime
		--Select @SaleEndTime


		--SELECT @TSoldQty = (ISNULL(TQD.TQD_Remaining_Quantity,0) + isnull(TLD.TLD_Locked_Qty,0))  FROM 
		--Ticket_Quantity_Detail TQD LEFT JOIN Ticket_Locked_Detail TLD ON TQD.TQD_Id = TLD.TLD_TQD_Id
		--WHERE TQD_PE_Id = @PublishId and TQD_Event_Id = @EventId AND TQD_Ticket_Id = @TicId

		IF (@NextAddId != @AddId)	
		BEGIN
			If (@RowIndex>1 AND @SHowRemaingTick =''Y'')
			BEGIN
				SET @Html = CONCAT(@Html,''<tr> <td class="tbl_white_bg" colspan="5" align="right">'',''<b>'',''Remaining Quantity : '' , @TRemainRuning ,''</b>'',''</td></tr>'') 
				SET @TRemainRuning =0
			END
			SET @NextAddId = @AddId 
			SELECT @Addess = ConsolidateAddress FROM ADDRESS WHERE AddressID = @AddId
			
			--SET @Html = CONCAT(@Html, ''<tr>'',''<td colspan=5 align="left"> '','''',''</td></tr>'')
			SET @Html = CONCAT(@Html,''<thead class="tkt_multi_tab_date"><tr>'',''<td class="book_tkt_desc" colspan=5 > '',@Addess,''</td></tr></thead>'')
			SET @Html = CONCAT(@Html,''<thead class="tkt_tbl_head"> <tr>'')
			SET @Html = CONCAT(@Html,''<td style="width:40%">Ticket Type</td>'')
			SET @Html = CONCAT(@Html,''<td>Sales End</td>'')
			SET @Html = CONCAT(@Html,''<td align="right">'', ''Price'',''</td>'')
			SET @Html =	CONCAT(@Html,''<td align="right">Fee</td>'')
			SET @Html = CONCAT(@Html,''<td align="right">Quantity</td>'')
			SET @Html = CONCAT(@Html,''</tr> </thead>'')
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

		SET @TMaxQty = ''<option>0</option>''
		If (@MaxQty >0) SET @TQty = ((@MaxQty-@MinQty) +@iflag)
		
		IF (@TQty>@RemQty) SET @TQty =((@RemQty-@MinQty) +@iflag) 
		
		--Select COncat(''TQTY'', @TQty)

		If (@MinQty >0) 
		BEGIN
			SET @Option = @MinQty 
		END
		ELSE 
		BEGIN
			 SET @Option =1
			 
		END


		if (@TicTypeId = 3) 
		BEGIN
			SET @TMaxQty = CONCAT(''<option>'',1,''</option>'')
		END
		ELSE
		BEGIN
			WHILE (@TQty>0)
			BEGIN
				SET @TMaxQty = CONCAT(@TMaxQty,''<option>'',@Option,''</option>'')
				SET @Option = @Option +1
				SET @TQty = @TQty -1
			END
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
		if (@IsShowDesc = 1 AND isnull(@TicketDesc,'''') != '''')
		BEGIN
			SET @Html = CONCAT(@Html,''<tr><td style="width:40%">'', @T_Name ,''<br><span class="tkt_more_desc">  <i onclick="checkMore('',@TQTY_Id,'')">[More Info]</i></span>'', ''<span style="display:none;" id="Sp_'',@TQTY_Id,''"class="col-sm-12 no_pad">'',@TicketDesc,''</span>'' ,''</td>'')
		END
		ELSE 
		BEGIN
			SET @Html = CONCAT(@Html,''<tr><td style="width:40%">'', @T_Name ,''<br>'', ''<span style="display:none;" id="Sp_'',@TQTY_Id,''"class="col-sm-12 no_pad">'',@TicketDesc,''</span>'' ,''</td>'')
		END

		--SET @Html = CONCAT(@Html,''<tr><td style="width:40%">'', @T_Name ,''</td>'')

		SET @Html = CONCAT(@Html,''<td>'',@TicketSaleEnd,''</td>'')
			
			IF (@TicTypeId = 2)  
			BEGIN
				if (@Discount>0)
				BEGIN
					SET @T_Fee = (@TotalPrice - @T_Price)
					SET @TotalPrice = (@T_Price  - @Discount) + @T_Fee 
					SET @Html = CONCAT(@Html,''<td align="right">$'', ''<strike>'',@T_Price,''</strike> $'',(@T_Price-@Discount),''<span style="display:none" id="P_'', @TQTY_Id, ''">'',@TotalPrice,''</span>'',''</td>'')
					SET @Html = CONCAT(@Html,''<td align="right">$'',@T_Fee ,''</td>'')
				END
				ELSE
				BEGIN
					SET @T_Fee = (@TotalPrice - @T_Price)
					SET @Html = CONCAT(@Html,''<td align="right">$'', @T_Price,''<span style="display:none" id="P_'', @TQTY_Id, ''">'',@TotalPrice,''</span>'',''</td>'')
					SET @Html = CONCAT(@Html,''<td align="right">$'',@T_Fee ,''</td>'')
				END
			END
			ELSE IF (@TicTypeId = 1)  
			BEGIN
				SET @Html = CONCAT(@Html,''<td align="right">'',''<span id="P_'', @TQTY_Id, ''">'',''Free'',''</span>'',''</td>'')
				SET @Html = CONCAT(@Html,''<td align="right">$'',''0.00'',''</td>'')
			END
			ELSE IF (@TicTypeId = 3)  
			BEGIN
				SET @Html = CONCAT(@Html,''<td align="right"  colspan="2">'',''<div style="width:60%;float:right;" class="col-sm-10 no_pad"><span style="font-weight: normal; float: left; margin-right: 7px; margin-top:3px">$ </span><input type="text" style="width:80%;" class="form-control evnt_inp_cont" oncopy="return false" onpaste="return false" oncut="return false" onkeydown="return checknumeric(event);" onchange="calculateTickTotal();" placeholder=" Enter Donation" autocomplete="false" name="d" value="" id="txtd_'',@TQTY_Id,''"  /></div>'',''</td>'')
			END
			
--GetTicketListing 281
			
			If (@TickatMarkSoldOut =1)
			BEGIN
				SET @Html = CONCAT(@Html,''<td align="right">Sold Out<select style="display:none" class="form-control ev_drop_label" onchange="calculateTickTotal()" id=d_'' ,CONVERT(VARCHAR,@TQTY_Id) ,''>'', @TMaxQty,''</select><input type="hidden" value="" id="hid_'', CONVERT(VARCHAR,@TQTY_Id) ,''" /></td>'')
				SET @Html = CONCAT(@Html,''</tr>'')
			END
			ELSE IF (@SaleStartDate != '''' AND CONVERT(DATE,@SaleStartDate) > CONVERT(DATE,GETDATE()))
			BEGIN
		
				SET @Html = CONCAT(@Html,''<td align="right">Sales Not Start Yet<select style="display:none" class="form-control ev_drop_label" onchange="calculateTickTotal()" id=d_'' ,CONVERT(VARCHAR,@TQTY_Id) ,''>'', @TMaxQty,''</select><input type="hidden" value="" id="hid_'', CONVERT(VARCHAR,@TQTY_Id) ,''" /></td>'')
				SET @Html = CONCAT(@Html,''</tr>'')
			END
			ELSE if (@SaleEndDate != '''' AND CONVERT(DATE,@SaleEndDate) < CONVERT(DATE,GETDATE()))
			BEGIN
				SET @Html = CONCAT(@Html,''<td align="right">Sales End<select style="display:none" class="form-control ev_drop_label" onchange="calculateTickTotal()" id=d_'' ,CONVERT(VARCHAR,@TQTY_Id) ,''>'', @TMaxQty,''</select><input type="hidden" value="" id="hid_'', CONVERT(VARCHAR,@TQTY_Id) ,''" /></td>'')
				SET @Html = CONCAT(@Html,''</tr>'')
			END
			ELSE
			BEGIN
				if (@SaleStartDate != '''' AND  @SaleStartTime != '''' AND CONVERT(DATE,@SaleStartDate) = CONVERT(DATE,GETDATE()) AND CONVERT(TIME,@SaleStartTime) > CAST(GETDATE() AS TIME)) 
				BEGIN
					SET @Html = CONCAT(@Html,''<td align="right">Sales Not Start Yet<select style="display:none" class="form-control ev_drop_label" onchange="calculateTickTotal()" id=d_'' ,CONVERT(VARCHAR,@TQTY_Id) ,''>'', @TMaxQty,''</select><input type="hidden" value="" id="hid_'', CONVERT(VARCHAR,@TQTY_Id) ,''" /></td>'')
					SET @Html = CONCAT(@Html,''</tr>'')
				END
				ELSE if (@SaleEndDate != '''' AND @SaleEndTime != '''' AND CONVERT(DATE,@SaleEndDate) = CONVERT(DATE,GETDATE()) AND CONVERT(TIME,@SaleEndTime) < CAST(GETDATE() AS TIME)) 
				BEGIN
					SET @Html = CONCAT(@Html,''<td align="right">Sales End<select style="display:none" class="form-control ev_drop_label" onchange="calculateTickTotal()" id=d_'' ,CONVERT(VARCHAR,@TQTY_Id) ,''>'', @TMaxQty,''</select><input type="hidden" value="" id="hid_'', CONVERT(VARCHAR,@TQTY_Id) ,''" /></td>'')
					SET @Html = CONCAT(@Html,''</tr>'')
				END
				ELSE
				BEGIN
					If (@TicTypeId = 3) 
					BEGIN
						SET @Html = CONCAT(@Html,''<td align="right" style="width:90px;"><select style="width:90px;" class="form-control ev_drop_label" onchange="calculateTickTotal()" id=d_'' ,CONVERT(VARCHAR,@TQTY_Id) ,''>'', @TMaxQty,''</select><input type="hidden" value="" id="hid_'', CONVERT(VARCHAR,@TQTY_Id) ,''" /></td>'')
					END
					ELSE
					BEGIN
						SET @Html = CONCAT(@Html,''<td align="right"><select style="width:90px;" class="form-control ev_drop_label" onchange="calculateTickTotal()" id=d_'' ,CONVERT(VARCHAR,@TQTY_Id) ,''>'', @TMaxQty,''</select><input type="hidden" value="" id="hid_'', CONVERT(VARCHAR,@TQTY_Id) ,''" /></td>'')
					END
					SET @Html = CONCAT(@Html,''</tr>'')
				END
			END
		END

		SET @RowCount = @RowCount  -1
		SET @RowIndex = @RowIndex  +1
		
		If (@AddIdss='''' AND @RowCount=0 AND @SHowRemaingTick =''Y'')
		BEGIN
			SET @Html = CONCAT(@Html,''<tr> <td class="tbl_white_bg" colspan="5" align="right">'',''<b>'',''Remaining Quantity : '' , @TRemainRuning ,''</b>'',''</td></tr>'') 
			SET @TRemainRuning =0
		END

	END

	--GetTicketListing 0

	

	SET @RCnt = @RCnt -1
	SET @RInd = @RInd +1
END
	if (@AddIdss != '''' AND @SHowRemaingTick =''Y'')
	BEGIN
		SET @Html = CONCAT(@Html,''<tr> <td class="tbl_white_bg" colspan="5" align="right">'',''<b>'',''Remaining Quantity : '' , @TRemainRuning ,''</b>'',''</td></tr>'') 
		SET @TRemainRuning =0
	END
--declare @Html varchar(Max)
--SET @Html =''</table>''
--SET @Html = CONCAT(@Html,''First'')
--SET @Html = CONCAT(@Html, ''Second'')
--SET @Html += ''<td>Satnam Waheguru</td>''
--SET @Html += ''</tr>''
SET @Html = CONCAT(@Html,''</table>'')
SET @Html = CONCAT(@Html,''<input id="hdIds" type="hidden" value='', @QtyIds ,'' />'')

Select @Html as Ticket


END






















' 
END
GO
/****** Object:  StoredProcedure [dbo].[PublishEvent]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PublishEvent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'





-- Navrit,  27/Oct/2015, 

--[PublishEvent 52,''''] -- Single
--[PublishEvent 79,''''] -- Daily
--PublishEvent 80,'''' -- Weekly
--PublishEvent 81,'''' -- MOnthly --Week wise
--PublishEvent 82,'''' -- MOnthly --Single Day

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




	SET @Ticketids = ''''
	SET @Addressids =''''
-- Order by was not working, need to work with temp table	
	
	DECLARE @Tbl TABLE (T_Id bigint,odId int)
	INSERT INTO @Tbl
	SELECT T_Id,T_order  FROM Ticket WHERE E_id = @EventId

	SELECT @Ticketids = @Ticketids + '','' + CONVERT(VARCHAR(MAX),T_Id) FROM @Tbl order by odId

	--SELECT @Ticketids = @Ticketids + '','' + CONVERT(VARCHAR(MAX),T_Id) FROM Ticket  WHERE E_id = @EventId 
	IF (@Ticketids != '''')
		SET  @Ticketids = RIGHT(@Ticketids,Len(@Ticketids)-1)

	SELECT @Addressids = @Addressids + '','' + CONVERT(VARCHAR(Max),AddressID) from [Address]  where [Address].EventId  = @EventId
	IF (@Addressids !='''')
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

		SELECT @Freq = ISNULL(Frequency,'''') FROM MultipleEvent WHERE EventID = @EventId
		-- 174 

		If (@Freq = ''Daily'' or @Freq = ''Custom'')
		BEGIN
			DECLARE @Days INT
			Set @Days =0
			SELECT @Days = DateDiff(dd,StartingFrom ,StartingTo), @MultipleId = MultipleEventID ,
			@StartDate = StartingFrom,@EndDate=StartingTo, @StartTime=StartTime,@EndTime=EndTime 
			FROM MultipleEvent WHERE EventID = @EventId
			SET @StartDate = CONVERT(VARCHAR,DATEADD(dd,0,@StartDate),107)
			WHILE (@Days>=0)
			BEGIN
				INSERT INTO @tblMerge VALUES (@EventId,@Ticketids,@Addressids,@MultipleId,0,@StartDate,'''',@StartTime,@EndTime)			
				SET @StartDate = CONVERT(VARCHAR,DATEADD(dd,1,@StartDate),107)
				SET @Days = @Days-1
			END
		END
		Else If (@Freq = ''Weekly'')
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
			INSERT INTO @tblWeek SELECT * FROM [dbo].func_Split(@Weekly,'','')  
			
			
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
					(@EventId,@Ticketids,@Addressids,@MultipleId,0,convert(varchar,@DateFrom,107),'''',@StartTime,@EndTime)		
				END
				SET @DateFrom =   DATEADD(dd,1,@DateFrom)
			
			END
		END
		Else If (@Freq = ''Monthly'')
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

			   
			IF(ISNULL(@Monthlyday,'''')!='''')
			BEGIN 
				WHILE (@StartDate<=@DateTo)
				BEGIN 
					SET @day=DATEPART(dd,@StartDate) 
					if(@day=@Monthlyday)
					BEGIN 
					  SELECT  @Dayofweek=datename(dw,@StartDate)
					  INSERT INTO @tblMerge VALUES
						(@EventId,@Ticketids,@Addressids,@MultipleId,0,CONVERT(VARCHAR,@StartDate,107),'''',@StartTime,@EndTime)
					END
					SET @StartDate = DATEADD(dd,1,@StartDate) 
				END
			END
			ELSE IF(ISNULL(@Monthlyweek,'''')!='''')
			BEGIN
				DECLARE @monthday as int
				SELECT @monthday = CASE @Monthlyweek WHEN ''First'' THEN 1 WHEN ''Second'' THEN 2 WHEN ''Third'' THEN 3 WHEN ''Four'' THEN 4 END
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
						INSERT INTO @tblWeek SELECT * FROM [dbo].func_Split(@MonthlyweekDays,'','') 
						WHILE (@newstartdate<=@newenddate)
						BEGIN
							SET @WeekDay = dbo.ReturnDayOfWeek(@newstartdate) 
							Select @DayID = count(*) from @tblWeek where DayId= @WeekDay
							IF (@DayID >0)
							BEGIN
								INSERT INTO @tblMerge VALUES
								(@EventId,@Ticketids,@Addressids,@MultipleId,0,CONVERT(VARCHAR,@newstartdate,107),'''',@StartTime,@EndTime)
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
	Set @StartDate=''''
	Set @StartTime=''''
	WHILE (@RowCount >0)
	BEGIN
		SELECT @StartDate=StartDate,@PublishId=Publish_Id,@Addresses=isnull(AddressIds,''''),@Ticketss=TicketsIDs,@StartTime=StartTime FROM @tblFinal WHERE Id=@RowIndex
		INSERT INTO @tblMVenue SELECT * FROM [dbo].func_Split(@Addresses,'','') 
		SET @AddRowCot = @@ROWCOUNT
		SET @AddRowInd =1
		If (@Addresses != '''')
		BEGIN
			WHILE (@AddRowCot>0)
			BEGIN
				SELECT @Addid = Element FROM @tblMVenue WHERE Id = @AddRowInd
				INSERT INTO @tblMTicket SELECT * FROM [dbo].func_Split(@Ticketss,'','') 
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
				INSERT INTO @tblMTicket SELECT * FROM [dbo].func_Split(@Ticketss,'','') 
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










' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[func_Split]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[func_Split]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[func_Split] 
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
' 
END

GO
/****** Object:  UserDefinedFunction [dbo].[ReturnDayOfWeek]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReturnDayOfWeek]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'-- =============================================
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
WHEN ''Sunday'' THEN 7
WHEN ''Monday'' THEN 1
WHEN ''Tuesday'' THEN 2
WHEN ''Wednesday'' THEN 3
WHEN ''Thursday'' THEN 4
WHEN ''Friday'' THEN 5
WHEN ''Saturday'' THEN 6
END
RETURN (@rtDayofWeek)
END

' 
END

GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__MigrationHistory]') AND type in (N'U'))
BEGIN
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
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Address]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Address]') AND type in (N'U'))
BEGIN
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
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [varchar](150) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]') AND type in (N'U'))
BEGIN
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
END
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]') AND type in (N'U'))
BEGIN
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
END
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [varchar](150) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUsers]') AND type in (N'U'))
BEGIN
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
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BillingAddress]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BillingAddress]') AND type in (N'U'))
BEGIN
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
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CardDetails]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CardDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CardDetails](
	[CardId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [nchar](128) NULL,
	[OrderId] [varchar](1000) NULL,
	[Guid] [nchar](128) NULL,
	[CardNumber] [varchar](50) NULL,
	[ExpirationDate] [varchar](50) NULL,
	[Cvv] [varchar](50) NULL,
	[card_type] [varchar](150) NULL,
 CONSTRAINT [PK_CardDetails] PRIMARY KEY CLUSTERED 
(
	[CardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Country]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Country]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Country](
	[CountryID] [tinyint] IDENTITY(1,1) NOT NULL,
	[Country] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DeliveryMethod]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeliveryMethod]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DeliveryMethod](
	[DeliveryMethodID] [bigint] IDENTITY(1,1) NOT NULL,
	[DeliveryMethod] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DeliveryMethodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Email_Tag]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Email_Tag]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Email_Tag](
	[Tag_Id] [int] IDENTITY(1,1) NOT NULL,
	[Tag_Name] [nvarchar](200) NULL,
 CONSTRAINT [PK_Email_Tag] PRIMARY KEY CLUSTERED 
(
	[Tag_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Email_Template]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Email_Template]') AND type in (N'U'))
BEGIN
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
	[From_Name] [varchar](300) NULL,
 CONSTRAINT [PK_Email_Template] PRIMARY KEY CLUSTERED 
(
	[TemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Event]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event]') AND type in (N'U'))
BEGIN
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
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Event_OrganizerMessages]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event_OrganizerMessages]') AND type in (N'U'))
BEGIN
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
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Event_Orgnizer_Detail]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event_Orgnizer_Detail]') AND type in (N'U'))
BEGIN
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
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Event_VariableDesc]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event_VariableDesc]') AND type in (N'U'))
BEGIN
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
END
GO
/****** Object:  Table [dbo].[EventCategory]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventCategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EventCategory](
	[EventCategoryID] [bigint] IDENTITY(1,1) NOT NULL,
	[EventCategory] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EventCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EventFavourite]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventFavourite]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EventFavourite](
	[UserID] [nvarchar](128) NULL,
	[eventId] [bigint] NULL,
	[FavId] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_EventFavourite] PRIMARY KEY CLUSTERED 
(
	[FavId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[EventImage]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventImage]') AND type in (N'U'))
BEGIN
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
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EventSubCategory]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventSubCategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EventSubCategory](
	[EventSubCategoryID] [bigint] IDENTITY(1,1) NOT NULL,
	[EventCategoryID] [bigint] NOT NULL,
	[EventSubCategory] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EventSubCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EventType]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EventType](
	[EventTypeID] [bigint] IDENTITY(1,1) NOT NULL,
	[EventType] [varchar](200) NOT NULL,
	[EventHide] [char](1) NULL,
PRIMARY KEY CLUSTERED 
(
	[EventTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EventVenue]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventVenue]') AND type in (N'U'))
BEGIN
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
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EventVote]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventVote]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EventVote](
	[UserID] [nvarchar](128) NULL,
	[eventId] [bigint] NULL,
	[VoteId] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_EventVote] PRIMARY KEY CLUSTERED 
(
	[VoteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Fee_Structure]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Fee_Structure]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Fee_Structure](
	[FeeId] [bigint] IDENTITY(1,1) NOT NULL,
	[FeeType] [varchar](50) NULL,
	[FeeAmount] [numeric](18, 0) NULL,
 CONSTRAINT [PK_Fee_Structure] PRIMARY KEY CLUSTERED 
(
	[FeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Messages]') AND type in (N'U'))
BEGIN
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
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MultipleEvent]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MultipleEvent]') AND type in (N'U'))
BEGIN
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
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Order_Detail_T]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Order_Detail_T]') AND type in (N'U'))
BEGIN
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
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Permission_Detail]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Permission_Detail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Permission_Detail](
	[Permission_Id] [int] IDENTITY(1,1) NOT NULL,
	[Permission_Desc] [varchar](100) NULL,
	[Permission_Category] [varchar](50) NULL,
 CONSTRAINT [PK_Permission_Detail] PRIMARY KEY CLUSTERED 
(
	[Permission_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Profile]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Profile]') AND type in (N'U'))
BEGIN
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
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Publish_Event_Detail]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Publish_Event_Detail]') AND type in (N'U'))
BEGIN
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
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ShippingAddress]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ShippingAddress]') AND type in (N'U'))
BEGIN
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
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Status]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Status]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Status](
	[StatusID] [bigint] IDENTITY(1,1) NOT NULL,
	[StatusName] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ticket]') AND type in (N'U'))
BEGIN
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
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ticket_Locked_Detail]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ticket_Locked_Detail]') AND type in (N'U'))
BEGIN
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
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ticket_Purchased_Detail]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ticket_Purchased_Detail]') AND type in (N'U'))
BEGIN
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
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ticket_Quantity_Detail]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ticket_Quantity_Detail]') AND type in (N'U'))
BEGIN
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
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TicketBearer]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TicketBearer]') AND type in (N'U'))
BEGIN
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
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TicketDeliveryMethod]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TicketDeliveryMethod]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TicketDeliveryMethod](
	[TicketDeliveryMethodID] [bigint] IDENTITY(1,1) NOT NULL,
	[DeliveryMethodID] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TicketDeliveryMethodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[TicketType]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TicketType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TicketType](
	[TicketTypeID] [bigint] IDENTITY(1,1) NOT NULL,
	[TicketType] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TicketTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TimeZoneDetail]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TimeZoneDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TimeZoneDetail](
	[TimeZone_Name] [varchar](500) NULL,
	[TimeZone] [varchar](500) NOT NULL,
	[TimeZone_Id] [int] IDENTITY(1,1) NOT NULL,
 --CONSTRAINT [PK_TimeZonesystem] PRIMARY KEY CLUSTERED 
--(
--	[TimeZone_Id] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TimeZoneDetail1]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TimeZoneDetail1]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TimeZoneDetail1](
	[TimeZone_Id] [int] IDENTITY(1,1) NOT NULL,
	[TimeZone_Name] [varchar](500) NULL,
	[TimeZone] [varchar](500) NULL,
 CONSTRAINT [PK_TimeZoneDetail] PRIMARY KEY CLUSTERED 
(
	[TimeZone_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User_Permission_Detail]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User_Permission_Detail]') AND type in (N'U'))
BEGIN
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
END
GO
/****** Object:  Table [dbo].[Venue]    Script Date: 1/18/2016 8:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Venue]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Venue](
	[VenueID] [bigint] IDENTITY(1,1) NOT NULL,
	[AddressID] [bigint] NOT NULL,
	[VenueName] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[VenueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'201508241139135_InitialCreate', N'EventKombo.Models.ApplicationDbContext', 0x1F8B0800000000000400DD5CDB6EE436127D5F60FF41D0D3EEC269F99219CC1AED044EDBDE3532BE60DA13ECDB802DB1DBC248A422518E8D205F96877C527E618B1275E345976EB9BB1D0C30B0C8E2A962B148168BC5FEF3F73FA6DF3F8781F584E3C4A7E4CC3E9A1CDA16262EF57CB23AB353B6FCE683FDFD777FFFDBF4D20B9FAD9F0ABA134E072D4972663F32169D3A4EE23EE2102593D077639AD0259BB8347490479DE3C3C37F3B47470E06081BB02C6BFA2925CC0F71F6019F334A5C1CB1140537D4C34122CAA1669EA15AB728C449845C7C665F3E61C27EA4E1824E7262DB3A0F7C0482CC71B0B42D440865888198A79F133C673125AB7904052878788930D02D51906021FE6945DEB72787C7BC274ED5B08072D384D17020E0D189508D23375F4BC176A93A50DE252899BDF05E670A3CB3AF3D9C157DA201284066783A0B624E7C66DF942CCE93E816B349D17092435EC500F70B8DBF4EEA880756EF7607A5291D4F0EF9BF036B96062C8DF119C1298B517060DDA78BC0777FC42F0FF42B266727478BE5C98777EF9177F2FE5B7CF2AEDE53E82BD0350AA0E83EA6118E4136BC2CFB6F5B4EB39D23372C9BD5DAE45A015B8259615B37E8F923262BF608F3E5F8836D5DF9CFD82B4A84717D263E4C2268C4E2143E6FD320408B0097F54E2B4FFE7F0BD7E377EF47E17A8B9EFC5536F4127F983831CCAB4F38C86A93473FCAA75763BCBF08B2AB9886FCBB695F79ED97394D639777861A491E50BCC2AC29DDD4A98CB7974973A8F1CDBA40DD7FD3E692AAE6AD25E51D5A6726142CB63D1B0A795F976F6F8B3B8F2218BCCCB4B846DA0C4ED9AB2652E303AB22A90CE7A8AFE110E8D05F791DBC0C911F8CB010F6E0022EC8D28F435CF6F2070A6687C86099EF5192C03AE0FD17258F2DA2C39F23883EC76E1A8379CE190AA357E776FF4809BE4DC305B7FAEDF11A6D681E7EA157C86534BE24BCD5C6781FA9FB95A6EC92781788E1CFCC2D00F9E7831FF60718459C73D7C5497205C68CBD19050FBB00BC26ECE478301C5F9F76ED88CC02E4877A4F445A49BF14A49537A2A7503C120399CE2B6913F5235DF9A49FA805A959D49CA25354413654540ED64F524169163423E89433A71ACDCFCB46687C472F83DD7F4F6FB3CDDBB416D4D438871512FF07131CC332E6DD23C6704CAA11E8B36EECC259C8868F337DF5BD29E3F4130AD2B159AD351BB24560FCD990C1EEFF6CC8C484E227DFE35E498FE34F410CF0BDE8F527ABEE392749B6EDE9D0E8E6B6996F670D304D97F324A1AE9FCD024DE04B842D9AF2830F6775C730F2DEC87110E81818BACFB73C2881BED9B251DD910B1C6086AD73370F0CCE50E2224F552374C81B2058B1A36A04ABE2214DE1FEA5F0044BC7316F84F821288199EA13A64E0B9FB87E84824E2D492D7B6E61BCEF250FB9E602479870869D9AE8C35C1FFEE002947CA441E9D2D0D4A9595CBB211ABC56D39877B9B0D5B82B5189ADD86487EF6CB04BE1BFBD8A61B66B6C0BC6D9AE923E02184379BB30507156E96B00F2C165DF0C543A31190C54B8545B31D0A6C67660A04D95BC3903CD8FA87DC75F3AAFEE9B79360FCADBDFD65BD5B503DB6CE863CF4C33F73DA10D83163856CDF362C12BF133D31CCE404E713E4B84AB2B9B08079F63D60CD954FEAED60F75DA4164236A03AC0CAD03545C022A40CA841A205C11CB6B954E781103608BB85B2BAC58FB25D89A0DA8D8F5CBD01AA1F9CA5436CE5EA78FB267A5352846DEEBB050C3D11884BC78353BDE4329A6B8ACAA983EBEF0106FB8D63131182D0AEAF05C0D4A2A3A33BA960AD3ECD692CE211BE2926DA425C97D3268A9E8CCE85A1236DAAD248D5330C02DD84845CD2D7CA4C956443ACADDA6AC9B3A798A9428983A865CAAE90D8A229FAC6AB955A2C49AE78955B36FE6C3538EC21CC371134DE651296DC989D118ADB0540BAC41D22B3F4ED805626881789C67E6850A99766F352CFF05CBFAF6A90E62B10F14D4FC6F7129AC5CDD37B65AD517111057D0C1903B3459145D33FCFAE6164F7543018A3581FB190DD29098FD2B73EBFCFAAEDE3E2F5111A68E24BFE23F29CA52BCDCA6E67B8D8B3A27C619A3D27B597F9CCC10266D17BE675DDF267FD48C5284A7EA28A690D5CEC6CDE4C60C192BD9411C3E549D08AF33AB44564A1D40140DC4A825362860B5BAFEA8CDDC933A66B3A63FA29460528794AA0648594F23690859AF580BCFA0513D457F0E6AE2481D5DADED8FAC4921A9436BAAD7C0D6C82CD7F547D56499D48135D5FDB1AB9413790DDDE37DCB786C5977E3CA0FB69BED5C068CD75910C7D9F86AF7F775A05AF1402C7143AF8089F2BD3426E3E96E5D63CAC3199B199301C3BCEE342EBE9BCB4EEB6DBD19B3719BDD58DADB6EF3CD78C34CF6550D4339DBC92425F7F28C279DE5A6E25CD5FD78463968E524B655A811B6F59784E170C20926F39F8359E063BE8817043788F84B9CB03C83C33E3E3C3A961EE0ECCF63182749BC40732E35BD88698ED91692B1C8138ADD4714ABA9111B3C18A94095A8F335F1F0F399FD6BD6EA340B60F0BFB2E203EB3AF94CFC9F53A87888536CFDA6A67A8E9340DF7EC2DAD3E70EFDB57AFDBF2F79D303EB2E8619736A1D4ABA5C67849B8F2006499337DD409AB59F46BCDD09D57879A0459526C4FA0F0D163E1BE5914121E53F42F4FCCFA1A2691F126C84A8792C3016DE282A343D065807CBF810C0834F963D0418D659FDC3807544333E0AF0C97030F94940FF65A868B9C3AD467324DAC69294E9B933A57AA3FCCA5DEF4D4AE6F546135DCDAE1E00B74106F51A96F1C6928F47DB1D35B9C5A361EFD2B45F3DA1785F7288ABEC8EDDA60E6F335BB8E54EE82F9524BC07696D9A349DDDA7026FDBD64C61DC3DCFA71C96F0BB67C62692B7769FD6BB6D63338579F7DCD80625EFEE99ADED6AFFDCB1A5F5DE42779E8AAB661519AE6374B1E0AE54DB3C700E27FC050523C83DCAFC85A43EB7CBC4AC321623C38AC4CCD49C54263356268EC257A168673BACAF62C36FEDACA069676B48C56CE32DD6FF56DE82A69DB721C1711749C2DA14435DE276C73AD69601F59692821B3DE9C841EFF2595BEFD6DF520EF0284A69CC1EC31DF1DB49F91D4525634E9D0129BEEA752FEC9DB55F5484FD3BF1571504FF7D4582DDC6AE59D25C93252D366F49A282448AD0DC60863CD852CF63E62F91CBA09AC798B327DE59DC8EDF742CB0774DEE5216A50CBA8CC345D008787127A08D7F96C7DC94797A1765BF56324617404C9FC7E6EFC80FA91F78A5DC579A989001827B1722A2CBC792F1C8EEEAA544BAA5A42790505FE9143DE0300A002CB92373F484D7910DCCEF235E21F7A58A009A40BA07A2A9F6E9858F56310A138151B5874FB0612F7CFEEEFFB9B517EC58540000, N'6.1.3-40302')
GO
SET IDENTITY_INSERT [dbo].[Address] ON 

GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (4, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', 304, N'Delaware, USA')
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (5, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', 305, N'Calle D, Hombres Blancos, 83570 Sonoita, Son., Mexico')
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (6, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'24556b52-131a-46ad-b6a8-2897417b40c0', 306, N'University of Washington Tower Building T, 4333 Brooklyn Ave NE, Seattle, WA 98105, USA')
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (7, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'24556b52-131a-46ad-b6a8-2897417b40c0', 307, N'University of Washington Tower Building T, 4333 Brooklyn Ave NE, Seattle, WA 98105, USA')
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (8, N'', N'wer', N'wer', N'wer', N'wer', N'wer', 1, N'wer', N'24556b52-131a-46ad-b6a8-2897417b40c0', 308, N'wer,wer,wer,wer,wer,wer, United States                          ')
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (9, N'', N'cvb', N'cvb', N'cb', N'cvb', N'cvb', 1, N'cv', N'24556b52-131a-46ad-b6a8-2897417b40c0', 308, N'cv,cvb,cvb,cb,cvb,cvb, United States                          ')
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (12, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', 311, N'Calle D, Hombres Blancos, 83570 Sonoita, Son., Mexico')
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (13, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', 314, N'Calle D, Hombres Blancos, 83570 Sonoita, Son., Mexico')
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (14, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', 315, N'Calle D, Hombres Blancos, 83570 Sonoita, Son., Mexico')
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (17, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'16724f9c-389d-43a3-b6b2-bc83989b9102', 318, NULL)
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (18, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'2dd29514-6025-4649-8d71-5ce2e117fa69', 319, NULL)
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (21, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'16724f9c-389d-43a3-b6b2-bc83989b9102', 321, N'Ghaziabad, Uttar Pradesh, India')
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (22, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'16724f9c-389d-43a3-b6b2-bc83989b9102', 321, N'Ghaziabad, Uttar Pradesh, India')
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (23, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'16724f9c-389d-43a3-b6b2-bc83989b9102', 310, N'Taj Mahal, Dharmapuri, Tajganj, Agra, Uttar Pradesh, India')
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (27, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', 324, N'Národní, 110 00 Praha-Praha 1, Czech Republic')
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (28, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', 325, N'Národní, 110 00 Praha-Praha 1, Czech Republic')
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (29, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', 326, N'Národní, 110 00 Praha-Praha 1, Czech Republic')
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (30, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', 327, N'Národní, 110 00 Praha-Praha 1, Czech Republic')
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (31, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', 328, N'Národní, 110 00 Praha-Praha 1, Czech Republic')
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (32, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', 329, N'Národní, 110 00 Praha-Praha 1, Czech Republic')
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (33, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', 330, N'Calle D, Hombres Blancos, 83570 Sonoita, Son., Mexico')
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (38, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'16724f9c-389d-43a3-b6b2-bc83989b9102', 335, N'Noid Rd, Mason, WI 54856, USA')
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (39, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', 336, N'Gera, Germany')
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (40, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', 337, N'Gera, Germany')
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (41, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', 340, N'S, Richfield, KS 67953, USA')
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (42, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', 341, N'S, Richfield, KS 67953, USA')
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (43, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', 342, N'S, Richfield, KS 67953, USA')
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (44, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', 345, N'Calle F, Playa del Carmen, Q.R., Mexico')
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (45, N'', N'zxc', N'zxc', N'zxc', N'zxc', N'zxc', 1, N'Goldinganj Railway Station, Rasalpura, Bihar 841211, India', N'16724f9c-389d-43a3-b6b2-bc83989b9102', 347, N'Goldinganj Railway Station, Rasalpura, Bihar 841211, India,zxc,zxc,zxc,zxc,zxc, United States                          ')
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (46, N'', N'zxc', N'zxc', N'zxc', N'xzc', N'zxc', 1, N'cvc', N'16724f9c-389d-43a3-b6b2-bc83989b9102', 347, N'cvc,zxc,zxc,zxc,xzc,zxc, United States                          ')
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (48, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'16724f9c-389d-43a3-b6b2-bc83989b9102', 348, N'(44.8876317, -92.43552)')
GO
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress]) VALUES (49, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'16724f9c-389d-43a3-b6b2-bc83989b9102', 350, N'(44.8876317, -92.43552)')
GO
SET IDENTITY_INSERT [dbo].[Address] OFF
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1', N'Super Admin')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'2', N'Admin')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3', N'Member')
GO
INSERT [dbo].[AspNetUserLogins] ([LoginProvider], [ProviderKey], [UserId]) VALUES (N'Facebook', N'1956731254551382', N'16724f9c-389d-43a3-b6b2-bc83989b9102')
GO
INSERT [dbo].[AspNetUserLogins] ([LoginProvider], [ProviderKey], [UserId]) VALUES (N'Google', N'104044562490672561869', N'5d7f9411-04cf-4431-9eb2-13efc37b6d33')
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
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5f158983-4660-4237-929c-70baf1279b56', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6b9b5454-b3fb-4a28-9226-32d588f8d9a8', N'3')
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
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'93e82fc1-2e9d-46eb-ae31-6dff9c5cece8', N'3')
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
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'bea62686-6748-494d-9898-b9d2d6f85cfa', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c09a9cd0-22cf-46f1-a40e-2c9c1f30a7c9', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c10cde59-184b-4244-9a7c-5b209e38eaae', N'1')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c91be264-0cca-4ade-91cd-c55ca1a184e8', N'1')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e142bbfb-ef9c-450b-8d8c-38d41966dca3', N'3')
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
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'just.shweta29@gmail.com', 0, N'AANZBSDHN/pxpM63iN7lIiNctuhCr7LveTDTSU6DfGWeDTYuCZMC7/Gyn9GGmWM0eQ==', N'02f81cf1-6d4b-4a9e-8c43-352c052cc8e6', NULL, 0, 0, NULL, 1, 0, N'just.shweta29@gmail.com', N'Y')
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
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'5f158983-4660-4237-929c-70baf1279b56', N'fth@fth.com', 0, N'AEOBf6A6ElmBNGoVOJRTo6XgpoXEKB2kUgT+hMBnvSkigNmKvMgORvaL7062wD4I7w==', N'e7de8c12-42c8-4070-88a4-9a619aad2a0b', NULL, 0, 0, NULL, 1, 0, N'fth@fth.com', N'Y')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'6b9b5454-b3fb-4a28-9226-32d588f8d9a8', N'jk@jk.com', 0, N'AJbGoE6SFYS+FDZtiddGHiE0h+0IJhHzzIRov+BRufVFa9tzv937pJF8cCIKhH67Og==', N'9ab10160-f037-4349-ba6b-af501bf38574', NULL, 0, 0, NULL, 1, 0, N'jk@jk.com', N'Y')
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
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'93e82fc1-2e9d-46eb-ae31-6dff9c5cece8', N'ghj@ghj.com', 0, N'AJQXMO7JCuHLVxNfDSRA0l4SP8IL3n+gBvmbZfASwIWQutCLJCO6MmuTsB48ox4QIA==', N'6c864df5-19d3-42ba-ab52-9f8647beac5a', NULL, 0, 0, NULL, 1, 0, N'ghj@ghj.com', N'Y')
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
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', N'pintu.sah@kiwitech.com', 0, N'ABtMhGHGHVJ2LNb8pbnzU9oWPkmnJ8v1ui7A8KnOvlrOFIzBDrJfstg0jZYl1AGLrA==', N'43e25f5b-df41-49c5-9507-1a1b3e4a4b53', NULL, 0, 0, NULL, 1, 0, N'pintu.sah@kiwitech.com', N'Y')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'bea62686-6748-494d-9898-b9d2d6f85cfa', N'lw@lw.com', 0, N'ABEpiI5T9ve5uZ4d+AzWJ//8Oi8JvYPlkiTTgIU295mRckyziELZ46R/a0lCXl/aQA==', N'c11526e9-0d4a-4e47-a2cc-6c0ccd680622', NULL, 0, 0, NULL, 1, 0, N'lw@lw.com', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'c09a9cd0-22cf-46f1-a40e-2c9c1f30a7c9', N'kumar.prat@gmail.com', 0, N'AOv+oXUaC3huGT60G8MslvXhTtTLsYkYjqAuKI1avOs8pi2+7MZGPQTzGE1u9e7eyQ==', N'9bd056a0-2f58-4bf0-8c2e-d117389f6d24', NULL, 0, 0, NULL, 1, 0, N'kumar.prat@gmail.com', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'c10cde59-184b-4244-9a7c-5b209e38eaae', N'saroosh@eventcombo.com', 0, N'ABJtrIrpMTgBHTJJ40GQnC77G76moVeIvKYYVba4BYBzSSgND+7ZNi8ZHfIQC6kiHQ==', N'c4170f35-4c1a-4b90-9b40-c1409425a06f', NULL, 0, 0, NULL, 1, 0, N'saroosh@eventcombo.com', N'N')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'c91be264-0cca-4ade-91cd-c55ca1a184e8', N'Navrit.Singh@kiwi.com', 0, N'AOVI6vVD8TgOqmCMMxb65iKYtsMt56fDC6tSYJ5EznWwKGxmPZStghsyZHNRaQYDww==', N'10991409-57f3-4808-b414-4fda3dba90b8', NULL, 0, 0, NULL, 1, 0, N'Navrit.Singh@kiwi.com', N'N')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'e142bbfb-ef9c-450b-8d8c-38d41966dca3', N'hjn@hjn.com', 0, N'AGH05UfZ2mHoZquOpLyQMrR/mlOSSaDmvic0Px2Syxcp27NLBBAy7CqUr2i139aCJw==', N'be317084-8d30-4c29-aaad-a027e2c85d40', NULL, 0, 0, NULL, 1, 0, N'hjn@hjn.com', N'Y')
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
SET IDENTITY_INSERT [dbo].[BillingAddress] ON 

GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (1, N'f4ee24cd-0194-45eb-ae30-0c92e151a7dc                                                                                            ', N'0', N'f4ee24cd-0194-45eb-ae30-0c92e151a7dc                                                                                            ', N'dfd', N'ddd', N'222-222-2222', N'dfsd', N'sdfsdf', N'2222', N'222', N'2222', N'16')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (2, N'                                                                                                                                ', N'0', N'                                                                                                                                ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (3, N'                                                                                                                                ', N'0', N'                                                                                                                                ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (4, N'                                                                                                                                ', N'0', N'                                                                                                                                ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (5, N'7db0fbe0-e140-4288-b43b-f0a892343043                                                                                            ', N'0', N'7db0fbe0-e140-4288-b43b-f0a892343043                                                                                            ', N'1111', N'111', N'111-111-1111', N'1111', NULL, N'111', N'111', N'111', N'18')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (6, N'                                                                                                                                ', N'0', N'                                                                                                                                ', N'1111', N'111', N'111-111-1111', N'1111', NULL, N'111', N'111', N'111', N'18')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (7, N'36e34ed6-c4f9-4fae-bb0d-a6f5ec440350                                                                                            ', N'0', N'36e34ed6-c4f9-4fae-bb0d-a6f5ec440350                                                                                            ', N'dfgdfg', N'dfg', N'232-323-2323', N'fgdfgd', NULL, N'dfgdf', N'fdgfd', N'dfgdf', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (8, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'0', N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'FIrst', N'Last', N'978-898-9898', N'jkjkjk', N'jkjkjk', N'Abohar', N'Stay', N'4512', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (9, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'0', N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'sdf', N'sdsdfsdf', N'499-966-9544', N'sdfdsf', N'sdfsd', N'fsdfsdf', N'sdfsdf', N'sdfsd', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (10, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'0', N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'dfgdfg', N'dfgdf', N'989-789-8988', N'dfgdfg', N'ddfgdfg', N'dfgdfgfdg', N'dfgfdg', N'1212', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (11, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'0', N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'dfgd', N'dfg', N'455-454-5454', N'dsfdsf', N'dfgf', N'dfgdf', N'dfgdfq', N'dfgfg', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (12, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'0', N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'sdfds', N'fdsfds', N'999-999-9999', N'ds', N'dsf', N'dsf', N'sdf', N'232', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (13, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'0', N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'cvbb', N'ccvb', N'999-999-9999', N'cvcvbcv', N'bcvb', N'cvb', N'cvb', N'2323', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (14, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'0', N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'dfgdfgdfg', N'dgfdf', N'343-434-3443', N'dfgdfgdfg', N'dfgdfgdfg', N'sdfdsfdsf', N'sdfsf', N'2323', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (15, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'0', N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'rtyrty', N' tryrtyrt', N'999-999-9999', N'rtyrtyrty', N'rtyrtyrty', N'rtyrtyrt', N'yrtyrty', N'yrtyr', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (16, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'0', N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'sdfsd', N'fsf', N'999-999-9999', N'sdfdsf', N'sfdsf', N'sfdsfdsf', N'dffsf', N'34343', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (17, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'0', N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'dfggdf', N'dfgdf', N'999-999-9999', N'dfgdfg', N'dgdfgdf', N'sdfgdfgdfg', N'dfgdfg', N'43434', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (18, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'T000000002', N'de3a3bce-be3d-46aa-81ac-6b86c96f6c2a                                                                                            ', N'222', N'222', N'222-222-2222', N'222', N'222', N'222', N'22', N'22', N'19')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (19, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'T000000003', N'1f13b8f1-01fd-4d98-bf66-ccfd8a9fc336                                                                                            ', N'dsfsd', N'fdsf', N'999-999-9999', N'gfhgfh', N'gfhgfh', N'gfhgfh', N'gfhgfh', N'fghgf', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (20, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'T000000004', N'ad6ad51d-b9ef-4df6-b6c1-474bbcb37700                                                                                            ', N'fdg', N'gdfg', N'999-999-9999', N'yui', N'yui', N'yui', N'yui', N'yui', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (21, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'T000000005', N'd6da5292-51ae-41c9-befc-5fa4e9a6cfc6                                                                                            ', N'dsfds', N'dsfds', N'999-999-9999', N'fghgfh', N'fghfgh', N'fghgfh', N'gfhgfh', N'fghgf', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (22, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'T000000006', N'ff6c0c2e-0b30-4ae6-b697-7f3638179009                                                                                            ', N'dfgfgh', N'fghgf', N'999-999-9999', N'ghjghjgh', N'ghjghj', N'ghjghjgh', N'jghjghj', N'ghjgh', N'2')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (23, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'T000000007', N'08204da2-6265-439d-a3a3-c4d467cd16aa                                                                                            ', N'dsfdsf', N'sdfdsf', N'999-999-9999', N'fghgfhgf', N'gfhgfh', N'gfhgfh', N'gfhgfhgf', N'hgfhf', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (24, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'T000000008', N'66b03493-2264-451a-bbce-dbeada1769a7                                                                                            ', N'dsfsdf', N'sdfdsf', N'999-999-9999', N'dfghdfgd', N'dfgdfgd', N'retret', N'ertert', N'34534', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (25, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'T000000009', N'5318b94b-7e29-446b-b387-dd9f6a428517                                                                                            ', N'dsfdsf', N'sdfdsf', N'999-999-9999', N'fghgfh', N'gfhgfh', N'fghgfh', N'gfhgfh', N'65656', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (26, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'T000000010', N'58970f0a-6d57-445f-9f7e-c80f7114fb0a                                                                                            ', N'jkjk', N'jkjk', N'999-999-9999', N'99jklkjl', N'kjlkjl', N'kjlkjl', N'jklkjl', N'jklkj', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (27, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'T000000011', N'257149fc-9684-4bc5-bb42-e318bca72efd                                                                                            ', N'gfhgh', N'fghfgh', N'777-777-7777', N'fghfg', N'hfghfgh', N'fghgfh', N'fghgfh', N'56565', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (28, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'T000000012', N'bddc31b3-f690-44be-92a6-1807cef82ca5                                                                                            ', N'sss', N'sss', N'222-222-2222', N'22', N'222', N'222', N'2222', N'222', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (29, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'T000000013', N'7bc5e857-0b19-4281-83ff-3ffe2df07ba3                                                                                            ', N'cxbvg', N'vdfgfg', N'999-999-9999', N'hjkjkhjk', N'hjkhkh', N'hjkhjk', N'hjkhjk', N'34', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (30, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'T000000015', N'28fd8718-51b8-46e8-b199-0505425ead40                                                                                            ', N'dfs', N'sdfs', N'999-999-9999', N'dfgdfgdf', N'dfgdfg', N'dfgdfg', N'dfgdfgdf', N'3434', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (31, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'T000000016', N'28fd8718-51b8-46e8-b199-0505425ead40                                                                                            ', N'dfs', N'sdfs', N'999-999-9999', N'dfgdfgdf', N'dfgdfg', N'dfgdfg', N'dfgdfgdf', N'3434', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (32, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'T000000017', N'544261e1-7fb9-46be-913a-b9938533e3e0                                                                                            ', N'dsfsdf', N'dsf', N'999-999-9999', N'dfgfdg', N'dfgdfg', N'dfgg', N'dfgdfg', N'43434', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (33, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'T000000018', N'4a68eab7-a984-466b-8597-b0a93ff32574                                                                                            ', N'gdfg', N'dfgdfg', N'999-999-9999', N'sdfdf', N'gfdgf', N'dfgdf', N'dfgf', N'12451', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (34, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'T000000019', N'8242ede9-3f3d-45d5-b44b-05d647d58b37                                                                                            ', N'ghgfhf', N'gfhgfh', N'999-999-9999', N'hjkhjk', N'hjkhjk', N'hjkhjk', N'hjkhjk', N'56787', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (35, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'T000000020', N'454528bd-7395-4209-bc16-43f3a1613848                                                                                            ', N'jhkhk', N'hjkjhk', N'999-999-9999', N'hjk', N'hjk', N'hjk', N'hjk', N'8989', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (36, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'T000000021', N'88723932-6d51-492a-8135-66ec17787684                                                                                            ', N'vc', N'xcv', N'333-333-3333', N'cxvxcv', N'xcvxcv', N'xcv', N'xcvxcv', N'xcvxc', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (37, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'T000000022', N'852eeb19-a3cc-4bfd-8d83-466b77f8e143                                                                                            ', N'fgfg', N'hh', N'111-111-1111', N'ghg', N'hjh', N'hjh', N'bnb', N'hgh', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (38, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'T000000023', N'76f47dce-b915-4323-b7c8-fcc21e3c4e46                                                                                            ', N'dvfsxcf', N'sdfsd', N'222-222-2222', N'dsfsdf', N'sdfsdf', N'sdfs', N'dfsdf', N'sdfsd', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (39, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'T000000024', N'365235b0-7784-4b33-afa3-d1c7394eae9d                                                                                            ', N'df', N'df', N'222-222-2222', N'dfgdf', N'gdfg', N'dfg', N'dfgd', N'fgf', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (40, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'T000000025', N'00bb9988-7186-426d-b555-746ea02e42f6                                                                                            ', N'fdf', N'ghg', N'111-111-1111', N'jkj', NULL, N'hjhj', N'bjnbhjn', N'jmn', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (41, N'24556b52-131a-46ad-b6a8-2897417b40c0                                                                                            ', N'T000000029', N'1572dfdc-c872-4e81-b30d-fb620877c1e6                                                                                            ', N'dsfgfdg', N'dfgdfg', N'999-999-9999', N'jljkl', N'jkljkl', N'345345', N'345345', N'32323', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (42, N'4db67acb-df7c-44b2-b156-ceae1c6649bc                                                                                            ', N'T000000032', N'4f34e3e7-bdb0-4784-b957-4687469c826c                                                                                            ', N'sss', N'sss', N'111-111-1111', N'dfsdf', N'sdfsdf', N'dfsdf', N'sdfsdf', N'sdfsd', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (43, N'4db67acb-df7c-44b2-b156-ceae1c6649bc                                                                                            ', N'T000000033', N'5fda93b2-35e0-4f51-93b8-2329106a7ab4                                                                                            ', N'dfasd', N'asdasd', N'222-222-2222', N'sdfdsf', N'dsfsdf', N'sdfsdf', N'dsfsdf', N'sdfsd', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (44, N'4db67acb-df7c-44b2-b156-ceae1c6649bc                                                                                            ', N'T000000034', N'88ae870b-ff70-44be-b80c-68da5bd0fccb                                                                                            ', N'ddd', N'dddd', N'222-222-2222', N'2222', N'222', N'222', N'222', N'2222', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (45, N'4db67acb-df7c-44b2-b156-ceae1c6649bc                                                                                            ', N'T000000035', N'd8276236-7501-4c3d-bb26-16bf3c71aa00                                                                                            ', N'sfgsfg', N'fdgfdg', N'222-222-2222', N'fdgdfg', N'dfgdfg', N'fdgdfg', N'dfgdfg', N'fdgdf', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (46, N'4db67acb-df7c-44b2-b156-ceae1c6649bc                                                                                            ', N'T000000038', N'ea2e204a-05e8-4c3c-b3aa-3f902a4766b9                                                                                            ', N'rtytuyt', N'utyutyu', N'676-767-6767', N'fghfghfgh', N'fghgfhfgh', N'fhfghfgh', N'fghfghfgh', N'fgh76', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (47, N'4db67acb-df7c-44b2-b156-ceae1c6649bc                                                                                            ', N'T000000039', N'9618ee7d-eed2-47b3-b600-2d859467cfa0                                                                                            ', N'dfgdfg', N'dfgdf', N'444-444-4444', N'dfgfdg', N'dfg', N'dfgfdg', N'dfgdf', N'gdf45', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (48, N'4db67acb-df7c-44b2-b156-ceae1c6649bc                                                                                            ', N'T000000040', N'f44854df-92b2-4df3-8ae7-3998e6d0879b                                                                                            ', N'FIrst Name', N'sdfsdf', N'333-333-3333', N'sdfdfsd', N'sdfdsfdsf', N'ewrer', N'dsfsdf', N'34343', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (49, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000053', N'a51b4371-4648-4a57-b108-ee574b07b7cd                                                                                            ', N'dsff', N'sdf', N'555-555-5555', N'rdfgds', N'sdfsdf', N'dsfsdf', N'3sdfsdf', N'sdfsd', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (50, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000054', N'd6b23655-6ea8-4337-adbb-4702dd835d9f                                                                                            ', N'sdffdf', N'sdfsdf', N'344-545-4545', N'dfgdfgdfg', N'dfgdgfg', N'fgdfgdfg', N'fdgdfg', N'45454', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (51, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000056', N'fea5debb-6a4c-4ead-92c5-6f1ba92b682f                                                                                            ', N'dsfdsf', N'sdfdsfds', N'343-434-3434', N'dfgdfgdfg', N'dfgfdgdg', N'fdgdfgdfg', N'dfgfdg', N'regfd', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (52, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000057', N'db2e7b34-94c2-4471-8b67-9a7b781d0953                                                                                            ', N'dsfsdf', N'sdfsdfds', N'444-444-4444', N'fdgdfgdfg', N'dfgfdgdfg', N'dfgdfg', N'dfgfdg', N'dfgdf', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (53, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000058', N'5df4d69c-fa02-4e2d-8184-34cf1d9a340d                                                                                            ', N'dfdsf', N'sdfdsf', N'444-444-4444', N'dfgdfg', N'dfgdfgfdg', N'dfgfdgf', N'ddgdfgdf', N'dfgfd', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (54, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000059', N'216e4819-d7b2-4828-8679-fd5799c024c9                                                                                            ', N'dfgdfg', N'dfgdfg', N'444-444-4444', N'fgdg', N'dfgdfg', N'dfgfdg', N'dfgfdg', N'dfgfd', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (55, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000060', N'efa32348-abb8-493e-85b4-4ff287fdffae                                                                                            ', N'dfghfhf', N'dghdhd', N'888-888-8888', N'hkjlhkjhk', N'gjgjhgjh', N'hkjhkjhk', N'gkghjkgh', N'gfjhg', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (56, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000064', N'42c1d561-fd0f-4e93-9562-012551af804c                                                                                            ', N'dfgdgf', N'dfgdfg', N'444-444-4444', N'dfgdfg', N'dfgdfg', N'dfg', N'dfgdfg', N'34343', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (57, N'a5c9a386-95d2-4f47-bd11-853bbc993caa                                                                                            ', N'T000000066', N'773c3137-10db-407e-ac73-8a6097b92340                                                                                            ', N'sdf', N'sdf', N'232-222-2222', N'fgvdfgdf', N'dfgdf', N'Watford', N'Hertfordshire', N'wd17', N'229')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (58, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000068', N'36130f89-8628-463c-8347-03208e603d48                                                                                            ', N'ghgfh', N'fghfgh', N'999-999-9999', N'9hkh', N'hjkh', N'hjk', N'hjk', N'7878', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (59, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000069', N'b706305c-591a-4d7d-89f5-099f2f90cf90                                                                                            ', N'ravi', N'asd', N'123-123-1231', N'12312edas', N'das', N'adad', N'sdffsdf', N'23213', N'13')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (60, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000070', N'64541080-a97c-4a79-902e-8718619fa6af                                                                                            ', N'asd', N'ads', N'213-213-1232', N'asd', N'adsasd', N'adsasd', N'ads23', N'21321', N'18')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (61, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000071', N'ed7f3529-ba6b-4324-8e59-d569159f2812                                                                                            ', N'asd', N'das', N'213-123-2132', N'das', N'das', N'das', N'ads', N'21323', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (62, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000072', N'5747abe7-ecc4-4ea2-b4e7-ebf3ab4b53df                                                                                            ', N'asd', N'ads', N'213-213-1231', N'asdas', N'asd', N'ads', N'adsdas', N'adsa2', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (63, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000073', N'982686fd-e2c0-4111-a936-14e91ddc2bb8                                                                                            ', N'asd', N'asd', N'123-213-1232', N'adsas', N'dasad', N'2das', N'das', N'a3213', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (64, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000075', N'7e9c7500-7d46-4927-8cfe-ac92a7af37b9                                                                                            ', N'das', N'das', N'212-131-2312', N'fdsdf', N'dfsf', N'asd', N'fswefw', N'21312', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (65, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000076', N'3812f5ef-8938-4fac-bb54-cb10def9e91a                                                                                            ', N'asdas', N'dsasd', N'123-123-1231', N'dasasd', N'asd', N'Richmond', N'Virginia', N'23232', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (66, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000077', N'57d45e16-4c53-43cf-a770-a0ef0bb25292                                                                                            ', N'asda', N'das', N'123-213-2131', N'das', N'das', N'dasdas', N'dasdasdas', N'21312', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (67, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000079', N'f69db80a-370b-4492-a71c-4fa7976b1270                                                                                            ', N'ss', N'ss', N'222-222-2222', N'ss', N'sss', N'222', N'2222', N'222', N'15')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (68, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000080', N'5c040140-733a-4ac5-b847-96acc06fc4f1                                                                                            ', N'gf', N'fdg', N'333-333-3333', N'fdgdfg', N'dfgdf', N'erer', N'erer', N'3233', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (69, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000081', N'6c1a09f5-de46-4332-bcd1-53ec7b20937c                                                                                            ', N'fgdfg', N'dfgdfg', N'333-333-3333', N'fgdfg', N'dfgdfg', N'sdfsdf', N'sfsdf', N'222', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (70, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000082', N'79ca54bd-6174-473b-9012-d54a395f9b4c                                                                                            ', N'dfgfdg', N'dfg', N'333-333-3333', N'vbh', N'gffg', N'333', N'3333', N'ffff', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (71, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000083', N'e6a9cb90-04f6-4545-bde4-61d486b985c6                                                                                            ', N'sss', N'Sss', N'111-111-1111', N'1d', N'dd', N'dsd', N'sdd', N'sdsd', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (72, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000088', N'029c4dbd-2e7b-41b1-ae4d-c48be99b1b59                                                                                            ', N'fgh', N'dgh', N'333-333-3333', N'dfg', N'dfg', N'23', N'23', N'dfg', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (73, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000110', N'c97e0674-37c7-4fcf-9ac5-35aaa1ccecf3                                                                                            ', N'asd', N'ads', N'213-213-1231', N'das', N'21dfas', N'ad', N'dasdas', N'21312', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (74, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000128', N'efa21d57-068a-417c-81b8-a07724e29b30                                                                                            ', N'dfsdf', N'sdfsd', N'222-222-2222', N'sdfsdf', N'sdfsd', N'Louisville', N'Kentucky', N'sdf', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (75, N'735b57df-6a29-48d6-87d3-e851aa3245c0                                                                                            ', N'T000000131', N'ea5355df-77e8-474c-82cb-d889f9bac559                                                                                            ', N'FIrst', N'sdfsdf', N'333-333-3333', N'sdfdsf', N'sdfdsf', N'Asasa', N'asasas', N'20103', N'3')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (76, N'735b57df-6a29-48d6-87d3-e851aa3245c0                                                                                            ', N'T000000133', N'173328df-2447-4cdd-b7f6-4ba61844aa4d                                                                                            ', N'gfhgfh', N'fghfg', N'999-999-9999', N'ghjkghj', N'ghjghj', N'Pittsburgh', N'Pennsylvania', N'15216', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (77, N'735b57df-6a29-48d6-87d3-e851aa3245c0                                                                                            ', N'T000000135', N'257c44cb-f927-4066-8219-41ea9140bb6e                                                                                            ', N'gfhg', N'ffghgf', N'523-323-2322', N'gfjhg', N'fgjg', N'Pittsburgh', N'Pennsylvania', N'15216', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (78, N'735b57df-6a29-48d6-87d3-e851aa3245c0                                                                                            ', N'T000000136', N'f8757e7d-5c82-4069-8a6e-ed6c3e6d36a0                                                                                            ', N'sdfgdf', N'gdfgdf', N'555-555-5555', N'fghgfh', N'fghfgh', N'Pittsburgh', N'Pennsylvania', N'15216', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (79, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000157', N'ad0b82d3-46c3-4392-8902-aa2e39b1b297                                                                                            ', N'dsfsdf', N'sdfsdf', N'999-999-9999', N'dfgdfg', N'dfgdfg', N'tryrty', N'tryrty', N'45454', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (80, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000158', N'ebefd53e-cdf8-4cd0-873c-295900760441                                                                                            ', N'gfdgfdg', N'dfgdfg', N'999-999-9999', N'hjkjhk', N'hjkhjk', N'Nashua', N'Minnesota', N'56565', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (81, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000159', N'8182120b-19ef-48f0-b561-e5f53efb4fd3                                                                                            ', N'fghfgh', N'fghfgh', N'999-999-9999', N'kjlkj', N'kjl', N'tryrtyrt', N'rtyrty', N'46546', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (82, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000182', N'85ef568b-1b2c-499a-824c-876f53e1eff8                                                                                            ', N'sdsad', N'asdsad', N'555-555-5555', N'fghfghfgh', N'fghfgh', N'fghfghf', N'fghfghfgh', N'gfhfg', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (83, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000183', N'd3dd6efb-1e65-413e-a877-ad8fbee5afab                                                                                            ', N'sddfg', N'dfgdfg', N'555-555-5555', N'dfgfd', N'gdgdfg', N'Pittsburgh', N'Pennsylvania', N'15216', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (84, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000184', N'4e731bae-3166-46cc-9c03-53f9aa243c64                                                                                            ', N'fdgdfg', N'dfgdfg', N'666-666-6666', N'fghfgh', N'fghfgh', N'Pittsburgh', N'Pennsylvania', N'15216', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (85, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000185', N'6f3057ac-afd8-4a1d-99da-1005f9f53b8f                                                                                            ', N'dgdfg', N'dfgdfg', N'555-555-5555', N'fghfgh', N'fghfgh', N'Pittsburgh', N'Pennsylvania', N'15216', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (86, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000186', N'7e108be6-0541-495f-822e-4c0dcf272237                                                                                            ', N'svcxv', N'xcvxc', N'444-444-4444', N'dfg', N'dfg', N'Pittsburgh', N'Pennsylvania', N'15216', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (87, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000187', N'e49bd5a3-4339-4438-9028-1dd0e5198ee7                                                                                            ', N'ghjj', N'ghjgh', N'566-666-6666', N'ghjhj', N'ghj', N'Pittsburgh', N'Pennsylvania', N'15216', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (88, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000188', N'977bddbe-d34a-4587-8c2f-cfef0556516f                                                                                            ', N'sdfdsf', N'sdfsdf', N'666-666-6666', N'trytry', N'trytr', N'Pittsburgh', N'Pennsylvania', N'15211', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (89, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000189', N'bfaf6c01-4d7c-40b5-879f-b07d3ab163a0                                                                                            ', N'fghgf', N'fghgfh', N'999-999-9999', N'gfhgfh', N'fghfgh', N'Pittsburgh', N'Pennsylvania', N'15216', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (90, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000190', N'ddc71ab6-7eaa-4de6-aad2-4eeb57d1916a                                                                                            ', N'Navreet', N'ghjgjgjhgj', N'999-999-9999', N'hjkhkhkjhg', N'ghjkhgkj', N'Pittsburgh', N'Pennsylvania', N'15216', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (91, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000192', N'ab78139b-5336-48a9-b99e-c459b5f64a82                                                                                            ', N'fdfdf', N'dfdf', N'999-999-9999', N'fgh', N'fghfg', N'Pittsburgh', N'Pennsylvania', N'15211', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (92, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000193', N'7b034e7d-9b3a-4cb7-98da-a6e3696fe38d                                                                                            ', N'fff', N'fff', N'999-999-9999', N'sf', N'fsdf', N'Pittsburgh', N'Pennsylvania', N'15216', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (93, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000194', N'eb224c3e-5ba0-4e5c-861a-562d6e530823                                                                                            ', N'cvcx', N'vxcv', N'999-999-9999', N'gfhfgh', N'fghfg', N'Pittsburgh', N'Pennsylvania', N'15216', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (94, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000006', N'7d5bc66e-4a26-4c00-bef5-cb0927876c8e                                                                                            ', N'fghgf', N'hfghfgh', N'555-555-5555', N'fghgfh', N'fghfgh', N'Pittsburgh', N'Pennsylvania', N'15216', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (95, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000007', N'8ca437bc-9f6a-40e6-be88-431c31bbd74d                                                                                            ', N'First', N'ghgj', N'999-999-9999', N'hkhkh', N'kkhkjh', N'Pittsburgh', N'Pennsylvania', N'15216', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (96, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000008', N'0dce9cb7-cca0-4e15-843c-8f0cf432e611                                                                                            ', N'fgh', N'fghfgh', N'555-555-5555', N'5fghgfh', N'fghfgh', N'Pittsburgh', N'Pennsylvania', N'15216', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (97, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000009', N'a4ebc8ae-c5aa-430f-9c61-5f129bad71be                                                                                            ', N'Navrit ', N'Singh', N'999-999-9999', N'adi', N'Adi2', N'Pittsburgh', N'Pennsylvania', N'15216', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (98, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000010', N'358adf81-e90d-484e-a556-0bf5a7cf57d7                                                                                            ', N'fvs', N'sdf', N'222-222-2222', N'asdad', N'asdfdf', N'Bronx', N'New York', N'10463', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (99, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000011', N'03309785-f76a-4811-b8d9-909c723c32e8                                                                                            ', N'sds', N'sd', N'222-222-2222', N'dfdf', N'dfdf', N'Schenectady', N'New York', N'12345', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (100, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000012', N'1785b16f-5743-4b2e-b15b-89fddf953dda                                                                                            ', N'sdfds', N'fdsfdsf', N'777-777-7777', N'hgjg', N'ghjghj', N'Pittsburgh', N'Pennsylvania', N'15216', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (101, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000013', N'160dec41-bc31-41c9-8fc0-a402bf1c6d77                                                                                            ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (102, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'', N'53f19093-bec4-44ed-bd4c-3576e0efa773                                                                                            ', N'sss', N'Sindhu', N'085-869-3853', N'282', NULL, N'ss', N'sss', N'wsss', N'229')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (103, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'', N'864af49c-5026-4af4-b618-16caa470b9cc                                                                                            ', N'dd', N'ddd', N'333-333-3333', N'cccc', NULL, N'sdf', N'New Mexico', N'10643', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (104, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'', N'f8e35f5f-54e3-4833-8a87-daaab49cb0d4                                                                                            ', N'sdf', N'Sdf', N'222-222-2222', N'fsd', NULL, N'dsf', N'New Mexico', N'10643', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (105, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'', N'b826e6d9-474e-4716-b656-de2c3142a510                                                                                            ', N'xvbcv', N'xcvv', N'222-222-2222', N'fgdgdfg', NULL, N'fdg', N'fg', N'222', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (106, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'', N'129330c7-c8fe-481b-91ed-99e0f36b62c0                                                                                            ', N'df', N'dfg', N'222-222-2222', N'fsdgdfg', NULL, N'sdf', N'dsf', N'2222', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (107, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'', N'335343b4-1344-4988-8951-f7f04140b8b0                                                                                            ', N'dsf', N'sdf', N'222-222-2222', N'dfg', NULL, N'3', N'33', N'333', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (108, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'', N'17fc0693-c139-471f-a828-457f23d3d6e6                                                                                            ', N'sdf', N'sdf', N'222-222-2222', N'sdfsadf', N'sdf', N'22', N'22', N'22', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (109, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'', N'91cb20de-c869-4488-ad40-57478302ab11                                                                                            ', N'fgfdg', N'dfg', N'333-333-3333', N'dfg', NULL, N'sdf', N'New Mexico', N'10643', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (110, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'', N'6a02eb03-1939-46d9-a153-c08491817d5d                                                                                            ', N'fdg', N'asd', N'333-333-3333', N'fgdfg', NULL, N'dfg', N'fdg', N'3443', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (111, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'', N'3ecbdd61-6fa0-4180-8c39-28fc97e4bf45                                                                                            ', N'fdgdsfg', N'dfvgd', N'232-222-2222', N'dfgdfg', N'dfg', N'dfg', N'New Mexico', N'10643', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (112, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'', N'd35d9959-8ec9-4334-911f-9d53249a4fc3                                                                                            ', N'ads', N'asd', N'222-222-2222', N'sdasd', NULL, N'34', N'34', N'2334', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (113, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000025', N'9c8975d7-3e99-40f0-a383-d103b1d65976                                                                                            ', N'zxc', N'zxc', N'222-222-2222', N'sdfsdf', NULL, N'erdf', N'dsfdsf', N'222', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (114, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000026', N'cf2fbe8f-cf1c-4c7c-bae8-8e9a4f48ef7a                                                                                            ', N'xgdfg', N'sdfsfdg', N'222-222-2222', N'dfg', NULL, N'dfs', N'fdg', N'3434', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (115, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000027', N'07127abd-3cc5-4f98-bdf7-543c0a7047de                                                                                            ', N'dfg', N'fdg', N'333-333-3333', N'sdf', NULL, N'Louisville', N'Kentucky', N'sdf', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (116, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000028', N'638f28d5-bdd7-4f44-8d19-ad7ac92e296f                                                                                            ', N'asd', N'asd', N'222-222-2222', N'sdfsfd', NULL, N'Mossy Head', N'Florida', N'32434', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (117, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000029', N'e727223b-7a00-4764-82f9-ed18d2e40eae                                                                                            ', N'dgdfg', N'dfg', N'333-333-3333', N'dfg', NULL, N'ert', N'ert', N'3434', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (118, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000030', N'33912bc6-fe0c-49c8-b97c-75ac6d8446c7                                                                                            ', N'zxczc', N'zxc', N'222-222-2222', N'dfg', NULL, N'dfg', N'New York', N'dsf', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (119, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000031', N'97829b43-b9e2-44a6-819f-f9affdff0bce                                                                                            ', N'ghjg', N'gh', N'444-444-4444', N'fgfh', NULL, N'gh', N'fgh', N'4545', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (120, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000032', N'b75909c3-3990-4ea6-82eb-b62ed7c2f1e3                                                                                            ', N'asd', N'asd', N'222-222-2222', N'sdf', NULL, N'Louisville', N'Kentucky', N'sdf', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (121, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000033', N'c2cd6685-1fbb-4933-9a72-b47aea98c673                                                                                            ', N'dg', N'df', N'333-333-3333', N'dfg', NULL, N'dfg', N'dfg', N'dfg', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (122, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000034', N'fe8a7e7d-4cfd-4d41-93ee-06b0ab570705                                                                                            ', N'sfsdf', N'sdf', N'232-222-2222', N'dfsdfsdf', NULL, N'qwe', N'wer', N'ewe', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (123, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000035', N'2af19d1d-24e8-49d9-b338-92691aa4bc7b                                                                                            ', N'dfgdg', N'dfg', N'333-333-3333', N'dfgdg', NULL, N'dfg', N'dfg', N'dfg', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (124, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000036', N'db5df513-7b83-43e0-8c7d-b7bb9a7f814f                                                                                            ', N'dgfd', N'dfg', N'222-222-2222', N'dfgdg', NULL, N'dfg', N'dfg', N'dfs', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (125, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000037', N'ce4af788-f35b-4506-8078-f2e3121e34b5                                                                                            ', N'dsf', N'sdf', N'222-222-2222', N'sdf', NULL, N'Louisville', N'Kentucky', N'sdf', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (126, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000038', N'f3f47565-7dbc-4271-ac3c-cb9ea5a06b65                                                                                            ', N'cvb', N'cvb', N'434-444-4444', N'fgdfg', NULL, N'dfg', N'dfg', N'dfg', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (127, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000039', N'54919902-d251-4ccc-af6f-e9e10bab1ab8                                                                                            ', N'gfh', N'dfg', N'333-333-3333', N'dfg', NULL, N'Duarte', N'California', N'fdg', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (128, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000040', N'668f65cb-40fc-447e-ba4c-9c8fcbdd9433                                                                                            ', N'xcv', N'xcv', N'323-333-3333', N'sdf', NULL, N'Louisville', N'Kentucky', N'sdf', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (129, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000041', N'f5baa6b5-08dc-421f-b641-1a2e2451e08d                                                                                            ', N'vbn', N'vbn', N'444-444-4444', N'fgh', NULL, N'Canton', N'Ohio', N'fgh', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (130, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000042', N'1449a942-5dbe-4a97-9e84-12c12aa149d7                                                                                            ', N'tyht', N'fgh', N'333-333-3333', N'fgh', NULL, N'Canton', N'Ohio', N'fgh', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (131, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000043', N'45cea9ce-9faa-434e-a38a-49511e0a5282                                                                                            ', N'sdf', N'sdf', N'333-333-3333', N'ghjj', N'dfsd', N'Louisville', N'Kentucky', N'sdf', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (132, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000044', N'bc0e1f28-f598-4ba4-94a4-3ff6c75d789f                                                                                            ', N'fsdf', N'sdf', N'222-222-2222', N'zxc', NULL, N'zxc', N'czxc', N'zxc', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (133, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000045', N'a0c543e5-7cfa-43ee-b253-5ecd9dbe205b                                                                                            ', N'sdf', N'asd', N'222-222-2222', N'dfs', NULL, N'Louisville', N'Kentucky', N'sdf', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (134, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b                                                                                            ', N'T000000046', N'6e013a72-07b5-4eb6-9786-0d7e9c27c0c8                                                                                            ', N'gfdgdgdf', N'gfdgdfg', N'321-323-2132', N'dfgdfg', N'gf', N'delhi', N'delhi', N'23213', N'19')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (135, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b                                                                                            ', N'T000000047', N'8279e2fa-8764-4825-bfc9-1068c38ea3c7                                                                                            ', N'fsfds', N'dsffdsf', N'423-434-2342', N'fdsf', N'3423424', N'delhi', N'delhi', N'23313', N'17')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (136, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b                                                                                            ', N'T000000048', N'd66d84ee-727a-4263-b05c-aeb0cf1693e5                                                                                            ', N'dsds', N'fdfsfds', N'232-131-3132', N'dfassfs', N'ewqwweewe', N'delhi', N'delhi', N'42342', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (137, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b                                                                                            ', N'T000000049', N'fe5e7fa0-3bcb-40d0-bc8b-232cf9b6748d                                                                                            ', N'rrere', N'rerwre', N'313-231-3213', N'wewer', N'erwrwrew', N'delhi', N'delhi', N'31332', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (138, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b                                                                                            ', N'T000000050', N'69aa57f2-ea9c-49ac-b1bc-bd8e7cfd6c71                                                                                            ', N'dsasff', N'dsfsfs', N'232-133-2132', N'rewr', N'fdssdf', N'delhi', N'delhi', N'23232', N'20')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (139, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b                                                                                            ', N'T000000051', N'e3ed5c5c-5843-414a-a19f-8a3a73a53746                                                                                            ', N'dasda', N'dasasd', N'113-313-1313', N'ddsdsf', N'dffdsf', N'delhi', N'delhi', N'24242', N'56')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (140, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b                                                                                            ', N'T000000052', N'2128731e-4743-438f-93d6-926ba387cec5                                                                                            ', N'fsfs', N'dssgg', N'423-424-2342', N'rewrewrew', N'errewr', N'delhi', N'Georgiafdsfs', N'fdsfs', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (141, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b                                                                                            ', N'T000000053', N'9e34026a-708e-4ec1-95e2-72725c03d77a                                                                                            ', N'tyhtryrty', N'ytyryry', N'342-444-2342', N'dfgddg', N'fgddfgdfg', N'delhi', N'delhi', N'22222', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (142, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000054', N'524ebc29-3ab6-46bc-96f2-bd73293062e9                                                                                            ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (143, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000055', N'480d14c1-b8ac-4f04-8bab-e43a2b4def91                                                                                            ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (144, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000056', N'72356c0b-b825-49fe-8811-0092ce5522a9                                                                                            ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (145, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000057', N'658a30ba-aaba-4809-8f4e-8cf404f680f5                                                                                            ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (146, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000058', N'5849b73b-6693-4b69-bba4-7ef40c9f62ee                                                                                            ', N'fgh', N'fgh', N'444-444-4444', N'fgh', NULL, N'Louisville', N'Kentucky', N'sdf', N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (147, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000059', N'e690bed3-3f81-4728-8ea4-26de91efccc8                                                                                            ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (148, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000060', N'e690bed3-3f81-4728-8ea4-26de91efccc8                                                                                            ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (149, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000061', N'c9893c2d-6b29-459b-8261-3ffc548543ef                                                                                            ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (150, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000062', N'ccdcd81d-b6f5-4ebf-9aea-ccf519e697df                                                                                            ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (151, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000063', N'24e2c733-7416-4d0b-8f48-3a386cf50314                                                                                            ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (152, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000064', N'3912dc3a-cf8b-43bc-8c19-07dee9151a68                                                                                            ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (153, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000065', N'a4684da7-0328-447a-a193-6afd27d49bd4                                                                                            ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (154, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000066', N'a4684da7-0328-447a-a193-6afd27d49bd4                                                                                            ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (155, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000067', N'd2ed54fb-93af-44ba-ad1f-30515ae217a6                                                                                            ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (156, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000068', N'06eb106b-1dd1-4168-aefc-24a7888ad666                                                                                            ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (157, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000069', N'b639e905-d4ae-428e-9e96-0b73c0bc8ac1                                                                                            ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (158, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000070', N'dff3a045-e33c-4c5c-8c90-9bd6ef9fae05                                                                                            ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (159, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000071', N'6f0a3842-0efe-4a9d-a9c5-644d196cff7f                                                                                            ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (160, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000072', N'ce59a4e4-cf0f-4ce5-ba26-2b21f15000f9                                                                                            ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1')
GO
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (161, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000073', N'9cf3a2d9-605a-4b9a-bbcf-fdfed89649c2                                                                                            ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1')
GO
SET IDENTITY_INSERT [dbo].[BillingAddress] OFF
GO
SET IDENTITY_INSERT [dbo].[CardDetails] ON 

GO
INSERT [dbo].[CardDetails] ([CardId], [UserId], [OrderId], [Guid], [CardNumber], [ExpirationDate], [Cvv], [card_type]) VALUES (13, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'', N'f8e35f5f-54e3-4833-8a87-daaab49cb0d4                                                                                            ', N'378282246310005', N'11/11', N'1111', N'amex')
GO
INSERT [dbo].[CardDetails] ([CardId], [UserId], [OrderId], [Guid], [CardNumber], [ExpirationDate], [Cvv], [card_type]) VALUES (14, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'', N'129330c7-c8fe-481b-91ed-99e0f36b62c0                                                                                            ', N'6011111111111117', N'22/22', N' 1111', N'discover')
GO
INSERT [dbo].[CardDetails] ([CardId], [UserId], [OrderId], [Guid], [CardNumber], [ExpirationDate], [Cvv], [card_type]) VALUES (15, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'', N'335343b4-1344-4988-8951-f7f04140b8b0                                                                                            ', N' 5555555555554444', N'33/33', N' 3344', N'mastercard')
GO
INSERT [dbo].[CardDetails] ([CardId], [UserId], [OrderId], [Guid], [CardNumber], [ExpirationDate], [Cvv], [card_type]) VALUES (16, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'', N'17fc0693-c139-471f-a828-457f23d3d6e6                                                                                            ', N' 4111111111111111', N'11/11', N' 1111', N'visa')
GO
INSERT [dbo].[CardDetails] ([CardId], [UserId], [OrderId], [Guid], [CardNumber], [ExpirationDate], [Cvv], [card_type]) VALUES (17, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'', N'91cb20de-c869-4488-ad40-57478302ab11                                                                                            ', N'371449635398431', N'23/23', N' 2323', N'amex')
GO
INSERT [dbo].[CardDetails] ([CardId], [UserId], [OrderId], [Guid], [CardNumber], [ExpirationDate], [Cvv], [card_type]) VALUES (18, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b                                                                                            ', N'T000000046', N'6e013a72-07b5-4eb6-9786-0d7e9c27c0c8                                                                                            ', N' 4242424242424242', N'32/13', N' 3324', N'visa')
GO
SET IDENTITY_INSERT [dbo].[CardDetails] OFF
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
SET IDENTITY_INSERT [dbo].[Email_Tag] ON 

GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (1, N'UserEmailID')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (2, N'UserFirstNameID')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (3, N'UserLastNameID')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (4, N'EventNameID')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (5, N'EventStartDateID')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (6, N'EventEndDateID')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (7, N'EventStartTimeID')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (8, N'EventEndTimeID')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (9, N'EventVenueID')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (10, N'EventAddressID')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (11, N'TicketOrderNumberID')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (12, N'DealOrderNumberID')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (13, N'ResetPwdUrl')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (14, N'EventOrderNO')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (15, N'EventBarcodeId')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (16, N'EventTitleId')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (17, N'EventImageId')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (18, N'Tickettype')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (19, N'TicketPrice')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (20, N'TicketOrderDateId')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (21, N'EventTimeZone')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (22, N'Ticketname')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (23, N'EventQrCode')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (24, N'EventOrganiserName')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (25, N'EventOrganiserEmail')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (26, N'EventImage')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (27, N'EventDescription')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (28, N'EventLogo')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (29, N'EventdayId')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (30, N'Eventtype')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (31, N'TicketQty')
GO
SET IDENTITY_INSERT [dbo].[Email_Tag] OFF
GO
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag], [From_Name]) VALUES (N'2ab53970-a668-4c07-ac73-3133a5447150', N'E-Ticket Template', N' ¶¶UserEmailID¶¶', NULL, NULL, N'Your Tickets for  ¶¶EventTitleId¶¶ Eventcombo Order no:  ¶¶EventOrderNO¶¶, ¶¶EventStartDateID¶¶. ¶¶EventVenueID¶¶', N'<table border="0" style="width:700px">
	<tbody>
		<tr>
			<td colspan="2">&para;&para;EventOrderNO&para;&para;</td>
		</tr>
		<tr>
			<td>&para;&para;EventLogo&para;&para;
			<p>&para;&para;EventBarcodeId&para;&para;</p>
			</td>
			<td>
			<table border="0">
				<tbody>
					<tr>
						<td>&para;&para;EventQrCode&para;&para;</td>
						<td>&para;&para;UserFirstNameID&para;&para; &para;&para;UserLastNameID&para;&para;</td>
					</tr>
				</tbody>
			</table>

			<table border="0">
				<tbody>
					<tr>
						<td>&para;&para;EventImage&para;&para;</td>
						<td>
						<p>&para;&para;EventTitleId&para;&para;</p>

						<p>&para;&para;EventdayId&para;&para; &para;&para;EventStartDateID&para;&para; , &para;&para;EventStartTimeID&para;&para; (&para;&para;Eventtype&para;&para;)</p>

						<p>&para;&para;EventVenueID&para;&para;</p>
						</td>
					</tr>
					<tr>
						<td colspan="2">
						<p>Organizer Name: &para;&para;EventOrganiserName&para;&para;</p>
						</td>
					</tr>
					<tr>
						<td colspan="2">
						<p>&para;&para;EventOrganiserEmail&para;&para;</p>
						</td>
					</tr>
					<tr>
						<td colspan="2">
						<p>&para;&para;EventTitleId&para;&para;</p>
						</td>
					</tr>
					<tr>
						<td colspan="2">
						<p>&para;&para;EventDescription&para;&para;</p>
						</td>
					</tr>
					<tr>
						<td colspan="2">
						<p>Ticket type: &para;&para;Tickettype&para;&para;</p>
						</td>
					</tr>
					<tr>
						<td colspan="2">
						<p>Order &para;&para;EventOrderNO&para;&para;. Ordered by &para;&para;UserFirstNameID&para;&para; &para;&para;UserLastNameID&para;&para; on &para;&para;TicketOrderDateId&para;&para; for amount (&para;&para;TicketPrice&para;&para;) Quantity(&para;&para;TicketQty&para;&para;)</p>
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

<table border="0" style="width:700px">
	<tbody>
		<tr>
			<td>Footer Text</td>
		</tr>
	</tbody>
</table>
', N'shweta.sindhu@kiwitech.com', N'eticket', NULL)
GO
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag], [From_Name]) VALUES (N'2e223022-9c7e-45e0-8e5b-6c8776762021', N'Welcome Template', N' ¶¶UserEmailID¶¶ ', N'just.shweta29@gmail.com, ¶¶UserEmailID¶¶', N'navrit.singh@kiwitech.com, ¶¶UserEmailID¶¶', N'Welcome to Eventcombo   ¶¶UserEmailID¶¶, ¶¶DealOrderNumberID¶¶', N'<p>Hi ,</p>

<p>Thank you &para;&para;Ticketname&para;&para; for choosing eventcombo.Lets get started. &para;&para;EventStartDateID&para;&para;&nbsp;,,,&para;&para;EventNameID&para;&para;</p>
', N'welcome@eventcombo.com', N'email_welcome', N'EventCombo')
GO
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag], [From_Name]) VALUES (N'405f9cc8-782b-4b87-8406-3a3c0ae4c60d', N'', N' ¶¶UserEmailID¶¶', N'just.shweta29@gmail.com', NULL, N'Reset Password ', N'<p><img alt="eventcombo" src="http://eventcombo.kiwireader.com/Images/logo_vertical.png" style="float:left; height:400px; margin:2px; width:86px" />&nbsp;Hello&nbsp; &para;&para;UserFirstNameID&para;&para; ,</p>

<p>We have recieved a request to reset your password for your account :&nbsp;&para;&para;UserEmailID&para;&para; .We&#39;re here to help!</p>

<p>Click on the link below to reset and create a new password:</p>

<p>&para;&para;ResetPwdUrl&para;&para;</p>

<p>Set a new Password if you didn&#39;t ask to change your password,don&#39;t worry!Your password is still safe and you can delete this email.</p>

<p>Best ,</p>

<p>The Eventcombo Team.</p>

<p>&nbsp;</p>
', N'shweta.sindhu@kiwitech.com', N'email_lost_pwd', NULL)
GO
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag], [From_Name]) VALUES (N'aa3f1945-418b-4fc8-8a6d-6c60741fa70a', N'', N' ¶¶UserEmailID¶¶', NULL, NULL, N'Your Account Info is successfuly updated', N'<p>Hi&nbsp; &para;&para;UserFirstNameID&para;&para;,</p>

<p>You Account info is updated successfully.</p>
', N'shweta.sindhu@kiwitech.com', N'acc_info_update', NULL)
GO
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag], [From_Name]) VALUES (N'edfb6c74-43f8-45e7-86d1-d19aaf7f5e37', N'', N' ¶¶UserEmailID¶¶', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag], [From_Name]) VALUES (N'fd04710c-8d72-43e5-ab83-c33da92adc15', N'', N' ¶¶UserEmailID¶¶', NULL, NULL, N'Your Password has Reset', N'<p>Hi&nbsp; &para;&para;UserFirstNameID&para;&para; ,</p>

<p>Your password has been successfully reset.</p>

<p>&nbsp;</p>

<p>&nbsp;</p>

<p>With Best Wishes,</p>

<p>The Eventcombo Team.</p>
', N'shweta.sindhu@kiwitech.com', N'acc_pwd_set', NULL)
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
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (336, 1, 1, 1, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Navreet-Singh%testing#outty&ioi*!', N'Y', N'N', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Live', NULL, NULL, NULL, N'1', NULL, NULL, N'Single', 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A58B00FD92D4 AS DateTime), NULL, N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (337, 42, 2, 21, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Navrit Singh Grewal Testing URL Routing', N'N', N'Y', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Live', NULL, NULL, NULL, N'1', NULL, NULL, N'Single', 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A58B011EF7B4 AS DateTime), NULL, N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (338, 1, 1, 2, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'this is test for hide ticket', N'N', N'N', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Save', NULL, NULL, NULL, N'1', NULL, NULL, NULL, 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A58B012C7BDC AS DateTime), NULL, N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (339, 1, 1, 1, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'this is test for hide unhide', N'N', N'N', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Live', NULL, NULL, NULL, N'1', NULL, NULL, N'', 0, N'N', NULL, N'N', N'N', N'Y', N'mnbm', N'O', CAST(0x0000A58B012D09DE AS DateTime), CAST(0x0000A58C00F87CAD AS DateTime), N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (340, 1, 2, 21, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Navrit#Singh$Grewal^url*Routing', N'N', N'Y', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Live', NULL, NULL, NULL, N'1', NULL, NULL, N'Single', 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A58B013CEE44 AS DateTime), NULL, N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (341, 1, 2, 21, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Navrit#Singh$Grewal^url*Routing()Duplicate', N'N', N'Y', N'Y', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Live', NULL, NULL, NULL, N'1', NULL, NULL, N'Single', 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, NULL, CAST(0x0000A58C00E76B91 AS DateTime), N'N', 340)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (342, 1, 2, 21, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Navrit#Singh$Grewal^url*Routing()Duplicate', N'N', N'Y', N'Y', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Save', NULL, NULL, NULL, N'1', NULL, NULL, N'Single', 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A58C00FABB43 AS DateTime), NULL, N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (343, 1, 1, 1, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'this is test for donation', N'N', N'N', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Live', NULL, NULL, NULL, N'1', NULL, NULL, NULL, 0, N'N', NULL, N'N', N'N', N'Y', N'charges', N'R', CAST(0x0000A58C00FDF3D0 AS DateTime), NULL, N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (344, 1, 1, 1, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'this is test for variable', N'N', N'N', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Save', NULL, NULL, NULL, N'1', NULL, NULL, N'', 0, N'N', NULL, N'N', N'N', N'Y', N'sf', N'O', CAST(0x0000A58C010F3631 AS DateTime), CAST(0x0000A58C01107ADA AS DateTime), N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (345, 42, 2, 21, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Navrit Testing Donate Case', N'N', N'Y', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'Y', N'Live', NULL, NULL, NULL, N'5', NULL, NULL, N'Single', 0, N'Y', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A58C0115DC1B AS DateTime), NULL, N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (346, 1, 1, 0, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'sdf', N'N', N'N', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Live', NULL, NULL, NULL, N'1', NULL, NULL, N'', 0, N'N', NULL, N'N', N'N', N'Y', N'sdf', N'R', CAST(0x0000A58D00CD7174 AS DateTime), CAST(0x0000A58D00CD7FF6 AS DateTime), N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (347, 1, 1, 1, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'this is test for free,donate,paid', N'Y', N'Y', N'Y', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Live', NULL, NULL, NULL, N'16', NULL, NULL, N'Multiple', 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A58E010FB965 AS DateTime), CAST(0x0000A58E010FD73E AS DateTime), N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (348, 1, 1, 0, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'this is test for timezone', N'Y', N'Y', N'Y', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Live', NULL, NULL, NULL, N'42', NULL, NULL, N'Single', 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A58F00D4D89F AS DateTime), CAST(0x0000A58F00D4DB2C AS DateTime), N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (349, 1, 1, 0, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'this is card detai', N'N', N'N', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Live', NULL, NULL, NULL, N'1', NULL, NULL, N'', 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A58F00E0AA04 AS DateTime), CAST(0x0000A58F00E0AE33 AS DateTime), N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (350, 1, 1, 1, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'this is card test', N'N', N'N', N'Y', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Live', NULL, NULL, NULL, N'1', NULL, NULL, N'Single', 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A58F00E801A5 AS DateTime), NULL, N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (351, 1, 1, 2, N'93e82fc1-2e9d-46eb-ae31-6dff9c5cece8', N'this is test', N'N', N'N', N'Y', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Live', NULL, NULL, NULL, N'26', NULL, NULL, N'', 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A58F01017677 AS DateTime), CAST(0x0000A58F0104621D AS DateTime), N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (352, 1, 1, 1, N'93e82fc1-2e9d-46eb-ae31-6dff9c5cece8', N'this is test ', N'N', N'N', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Live', NULL, NULL, NULL, N'26', NULL, NULL, NULL, 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A58F01049E4E AS DateTime), NULL, N'N', 0)
GO
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID]) VALUES (353, 42, 10, 0, N'5f158983-4660-4237-929c-70baf1279b56', N'this is test', N'N', N'N', N'N', N'

', N'Public', N'N', N'N', NULL, NULL, N'N', N'Live', NULL, NULL, NULL, N'26', NULL, NULL, NULL, 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(0x0000A58F0108A850 AS DateTime), NULL, N'N', 0)
GO
SET IDENTITY_INSERT [dbo].[Event] OFF
GO
SET IDENTITY_INSERT [dbo].[Event_OrganizerMessages] ON 

GO
INSERT [dbo].[Event_OrganizerMessages] ([EventId], [OrganizerId], [Userid], [Name], [Email], [Message], [MessageId]) VALUES (74, 6, N'', N's', N's', N's', 1)
GO
INSERT [dbo].[Event_OrganizerMessages] ([EventId], [OrganizerId], [Userid], [Name], [Email], [Message], [MessageId]) VALUES (74, 6, N'', NULL, NULL, NULL, 2)
GO
INSERT [dbo].[Event_OrganizerMessages] ([EventId], [OrganizerId], [Userid], [Name], [Email], [Message], [MessageId]) VALUES (74, 6, N'', NULL, NULL, NULL, 3)
GO
INSERT [dbo].[Event_OrganizerMessages] ([EventId], [OrganizerId], [Userid], [Name], [Email], [Message], [MessageId]) VALUES (104, 11, N'24556b52-131a-46ad-b6a8-2897417b40c0', N's', N's@s.com', N's', 4)
GO
INSERT [dbo].[Event_OrganizerMessages] ([EventId], [OrganizerId], [Userid], [Name], [Email], [Message], [MessageId]) VALUES (74, 6, N'', N's', N's@s.com', N's', 5)
GO
INSERT [dbo].[Event_OrganizerMessages] ([EventId], [OrganizerId], [Userid], [Name], [Email], [Message], [MessageId]) VALUES (74, 6, N'', N'd', N'd@d.com', N'd', 6)
GO
INSERT [dbo].[Event_OrganizerMessages] ([EventId], [OrganizerId], [Userid], [Name], [Email], [Message], [MessageId]) VALUES (74, 6, N'', N's', N's@s.com', N's', 7)
GO
INSERT [dbo].[Event_OrganizerMessages] ([EventId], [OrganizerId], [Userid], [Name], [Email], [Message], [MessageId]) VALUES (96, 5, N'4db67acb-df7c-44b2-b156-ceae1c6649bc', N'shweta', N'shweta.sindhu@gmail.com', N'werwerwer', 8)
GO
INSERT [dbo].[Event_OrganizerMessages] ([EventId], [OrganizerId], [Userid], [Name], [Email], [Message], [MessageId]) VALUES (96, 5, N'4db67acb-df7c-44b2-b156-ceae1c6649bc', N'dsf', N'sdf@gmail.com', N'sdf', 9)
GO
SET IDENTITY_INSERT [dbo].[Event_OrganizerMessages] OFF
GO
SET IDENTITY_INSERT [dbo].[Event_Orgnizer_Detail] ON 

GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (1, 304, N'Grewal Shab', N'desc', N'fb.com', N'tw.come', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Y', N'link.com')
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (2, 305, N'Grewal Shab', N'desc', N'fb.com', N'tw.come', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (3, 306, N'dfg', N'dfg', N'df', N'df', N'24556b52-131a-46ad-b6a8-2897417b40c0', N'Y', N'df')
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (4, 307, N'dfg', N'dfg', N'df', N'df', N'24556b52-131a-46ad-b6a8-2897417b40c0', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (5, 308, N'dfg', N'dfg', N'df', N'df', N'24556b52-131a-46ad-b6a8-2897417b40c0', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (6, 309, N'dfg', N'dfg', N'df', N'df', N'', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (7, 310, N'd', N'd', NULL, NULL, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (8, 311, N'Grewal Shab', N'desc', N'fb.com', N'tw.come', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (9, 312, N'd', N'd', NULL, NULL, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (10, 313, N'd', N'd', NULL, NULL, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (11, 314, N'Grewal Shab', N'desc', N'fb.com', N'tw.come', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (12, 315, N'Grewal Shab', N'desc', N'fb.com', N'tw.come', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (13, 316, N'd', N'd', N'dsf', N'sdf', N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (14, 317, N'd', N'd', N'dsf', N'sdf', N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'Y', N'linekdin')
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (15, 318, N'd', N'd', N'dsf', N'sdf', N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'Y', N'linekdin1')
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (16, 319, N'sdf', N'sdf', NULL, NULL, N'2dd29514-6025-4649-8d71-5ce2e117fa69', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (17, 320, N'dfg', N'dfg', NULL, NULL, N'34b7f0f9-8776-4628-a690-8efa1a2bd425', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (18, 321, N'd', N'd', N'dsf', N'sdf', N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'Y', N'linekdin1')
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (19, 322, N'd', N'd', N'dsf', N'sdf', N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'Y', N'linekdin1')
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (20, 323, N'd', N'd', N'dsf', N'sdf', N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'Y', N'linekdin1')
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (21, 324, N'Grewal Shab', N'desc', N'fb.com', N'tw.come', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (22, 325, N'Grewal Shab', N'desc', N'fb.com', N'tw.come', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (23, 326, N'Grewal Shab', N'desc', N'fb.com', N'tw.come', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (24, 327, N'Grewal Shab', N'desc', N'fb.com', N'tw.come', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (25, 328, N'Grewal Shab', N'desc', N'fb.com', N'tw.come', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (26, 329, N'Grewal Shab', N'desc', N'fb.com', N'tw.come', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (27, 330, N'Grewal Shab', N'desc', N'fb.com', N'tw.come', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (28, 331, N'd', N'd', N'dsf', N'sdf', N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'Y', N'linekdin1')
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (29, 332, N'fg', N'fg', NULL, NULL, N'1bd462fd-9fb2-45c9-941e-146b3a436f48', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (30, 333, N'dfg', N'dfg', N'df', N'df', N'24556b52-131a-46ad-b6a8-2897417b40c0', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (31, 334, N'd', N'd', N'dsf', N'sdf', N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'Y', N'linekdin1')
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (32, 335, N'd', N'd', N'dsf', N'sdf', N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'Y', N'linekdin1')
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (33, 336, N'Grewal Shab', N'desc', N'fb.com', N'tw.come', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (34, 337, N'Grewal Shab', N'desc', N'fb.com', N'tw.come', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (35, 338, N'd', N'd', N'dsf', N'sdf', N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'Y', N'linekdin1')
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (36, 339, N'd', N'd', N'dsf', N'sdf', N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'Y', N'linekdin1')
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (37, 340, N'Grewal Shab', N'desc', N'fb.com', N'tw.come', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (38, 341, N'Grewal Shab', N'desc', N'fb.com', N'tw.come', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (39, 342, N'Grewal Shab', N'desc', N'fb.com', N'tw.come', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (40, 343, N'd', N'd', N'dsf', N'sdf', N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'Y', N'linekdin1')
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (41, 344, N'd', N'd', N'dsf', N'sdf', N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'Y', N'linekdin1')
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (42, 345, N'Grewal Shab', N'desc', N'fb.com', N'tw.come', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (43, 346, N'd', N'd', N'dsf', N'sdf', N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'Y', N'linekdin1')
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (44, 347, N'd', N'd', N'dsf', N'sdf', N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'Y', N'linekdin1')
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (45, 348, N'd', N'd', N'dsf', N'sdf', N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'Y', N'linekdin1')
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (46, 349, N'd', N'd', N'dsf', N'sdf', N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'Y', N'linekdin1')
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (47, 350, N'd', N'd', N'dsf', N'sdf', N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'Y', N'linekdin1')
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (48, 351, N'sdf', N'sdf', NULL, NULL, N'93e82fc1-2e9d-46eb-ae31-6dff9c5cece8', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (49, 352, N'sdf', N'sdf', NULL, NULL, N'93e82fc1-2e9d-46eb-ae31-6dff9c5cece8', N'Y', NULL)
GO
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [Orgnizer_Name], [Orgnizer_Desc], [FBLink], [Twitter], [UserId], [DefaultOrg], [Linkedin]) VALUES (50, 353, N'asd', N'asd', NULL, NULL, N'5f158983-4660-4237-929c-70baf1279b56', N'Y', NULL)
GO
SET IDENTITY_INSERT [dbo].[Event_Orgnizer_Detail] OFF
GO
SET IDENTITY_INSERT [dbo].[Event_VariableDesc] ON 

GO
INSERT [dbo].[Event_VariableDesc] ([VariableDesc], [Price], [Event_Id], [Variable_Id]) VALUES (N'COD', CAST(2.00 AS Numeric(18, 2)), 304, 1)
GO
INSERT [dbo].[Event_VariableDesc] ([VariableDesc], [Price], [Event_Id], [Variable_Id]) VALUES (N'Extra ', CAST(3.00 AS Numeric(18, 2)), 304, 2)
GO
INSERT [dbo].[Event_VariableDesc] ([VariableDesc], [Price], [Event_Id], [Variable_Id]) VALUES (N'By Post', CAST(4.00 AS Numeric(18, 2)), 304, 3)
GO
INSERT [dbo].[Event_VariableDesc] ([VariableDesc], [Price], [Event_Id], [Variable_Id]) VALUES (N'sdf', CAST(2323.00 AS Numeric(18, 2)), 306, 4)
GO
INSERT [dbo].[Event_VariableDesc] ([VariableDesc], [Price], [Event_Id], [Variable_Id]) VALUES (N'sdf', CAST(23.23 AS Numeric(18, 2)), 307, 5)
GO
INSERT [dbo].[Event_VariableDesc] ([VariableDesc], [Price], [Event_Id], [Variable_Id]) VALUES (N'sss', CAST(22222.00 AS Numeric(18, 2)), 308, 6)
GO
INSERT [dbo].[Event_VariableDesc] ([VariableDesc], [Price], [Event_Id], [Variable_Id]) VALUES (N'dfgdfg', CAST(2222.22 AS Numeric(18, 2)), 309, 7)
GO
INSERT [dbo].[Event_VariableDesc] ([VariableDesc], [Price], [Event_Id], [Variable_Id]) VALUES (N'jkjkl', CAST(12.00 AS Numeric(18, 2)), 316, 8)
GO
INSERT [dbo].[Event_VariableDesc] ([VariableDesc], [Price], [Event_Id], [Variable_Id]) VALUES (N'fsdf', CAST(12.12 AS Numeric(18, 2)), 317, 9)
GO
INSERT [dbo].[Event_VariableDesc] ([VariableDesc], [Price], [Event_Id], [Variable_Id]) VALUES (N'dfsg', CAST(23.00 AS Numeric(18, 2)), 335, 11)
GO
INSERT [dbo].[Event_VariableDesc] ([VariableDesc], [Price], [Event_Id], [Variable_Id]) VALUES (N'charges  Description 1', CAST(22.32 AS Numeric(18, 2)), 339, 14)
GO
INSERT [dbo].[Event_VariableDesc] ([VariableDesc], [Price], [Event_Id], [Variable_Id]) VALUES (N'charges  Description 2', CAST(34.34 AS Numeric(18, 2)), 339, 15)
GO
INSERT [dbo].[Event_VariableDesc] ([VariableDesc], [Price], [Event_Id], [Variable_Id]) VALUES (N'req 1', CAST(22.22 AS Numeric(18, 2)), 343, 16)
GO
INSERT [dbo].[Event_VariableDesc] ([VariableDesc], [Price], [Event_Id], [Variable_Id]) VALUES (N'req 2', CAST(333.33 AS Numeric(18, 2)), 343, 17)
GO
INSERT [dbo].[Event_VariableDesc] ([VariableDesc], [Price], [Event_Id], [Variable_Id]) VALUES (N'sfdf', CAST(22.22 AS Numeric(18, 2)), 344, 20)
GO
INSERT [dbo].[Event_VariableDesc] ([VariableDesc], [Price], [Event_Id], [Variable_Id]) VALUES (N'fsgdfg', CAST(444.44 AS Numeric(18, 2)), 344, 21)
GO
INSERT [dbo].[Event_VariableDesc] ([VariableDesc], [Price], [Event_Id], [Variable_Id]) VALUES (N'gbhfghfg', CAST(222.22 AS Numeric(18, 2)), 344, 22)
GO
INSERT [dbo].[Event_VariableDesc] ([VariableDesc], [Price], [Event_Id], [Variable_Id]) VALUES (N'sdf', CAST(23.00 AS Numeric(18, 2)), 346, 23)
GO
INSERT [dbo].[Event_VariableDesc] ([VariableDesc], [Price], [Event_Id], [Variable_Id]) VALUES (N'fjh', CAST(45.00 AS Numeric(18, 2)), 346, 24)
GO
SET IDENTITY_INSERT [dbo].[Event_VariableDesc] OFF
GO
SET IDENTITY_INSERT [dbo].[EventCategory] ON 

GO
INSERT [dbo].[EventCategory] ([EventCategoryID], [EventCategory]) VALUES (1, N'Category 2')
GO
INSERT [dbo].[EventCategory] ([EventCategoryID], [EventCategory]) VALUES (2, N'Category 1')
GO
INSERT [dbo].[EventCategory] ([EventCategoryID], [EventCategory]) VALUES (10, N'Category 3')
GO
SET IDENTITY_INSERT [dbo].[EventCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[EventImage] ON 

GO
INSERT [dbo].[EventImage] ([EventImageID], [EventID], [EventImageUrl], [ImageType]) VALUES (1, 306, N'391817logo.jpg', N'image/jpeg')
GO
INSERT [dbo].[EventImage] ([EventImageID], [EventID], [EventImageUrl], [ImageType]) VALUES (2, 306, N'affiliate-program_FPO.jpg', N'image/jpeg')
GO
INSERT [dbo].[EventImage] ([EventImageID], [EventID], [EventImageUrl], [ImageType]) VALUES (3, 306, N'ANALYTICS_FPO.jpg', N'image/jpeg')
GO
INSERT [dbo].[EventImage] ([EventImageID], [EventID], [EventImageUrl], [ImageType]) VALUES (4, 306, N'analytics-fpo.png', N'image/png')
GO
INSERT [dbo].[EventImage] ([EventImageID], [EventID], [EventImageUrl], [ImageType]) VALUES (5, 306, N'attendee-survey-FPO.jpg', N'image/jpeg')
GO
INSERT [dbo].[EventImage] ([EventImageID], [EventID], [EventImageUrl], [ImageType]) VALUES (6, 307, N'391817logo.jpg', N'image/jpeg')
GO
INSERT [dbo].[EventImage] ([EventImageID], [EventID], [EventImageUrl], [ImageType]) VALUES (7, 307, N'affiliate-program_FPO.jpg', N'image/jpeg')
GO
INSERT [dbo].[EventImage] ([EventImageID], [EventID], [EventImageUrl], [ImageType]) VALUES (8, 307, N'ANALYTICS_FPO.jpg', N'image/jpeg')
GO
INSERT [dbo].[EventImage] ([EventImageID], [EventID], [EventImageUrl], [ImageType]) VALUES (9, 307, N'analytics-fpo.png', N'image/png')
GO
INSERT [dbo].[EventImage] ([EventImageID], [EventID], [EventImageUrl], [ImageType]) VALUES (10, 307, N'attendee-survey-FPO.jpg', N'image/jpeg')
GO
INSERT [dbo].[EventImage] ([EventImageID], [EventID], [EventImageUrl], [ImageType]) VALUES (11, 308, N'391817logo.jpg', N'image/jpeg')
GO
INSERT [dbo].[EventImage] ([EventImageID], [EventID], [EventImageUrl], [ImageType]) VALUES (12, 308, N'bg1a.jpg', N'image/jpeg')
GO
INSERT [dbo].[EventImage] ([EventImageID], [EventID], [EventImageUrl], [ImageType]) VALUES (13, 308, N'bg1b.jpg', N'image/jpeg')
GO
INSERT [dbo].[EventImage] ([EventImageID], [EventID], [EventImageUrl], [ImageType]) VALUES (14, 308, N'bg2.jpg', N'image/jpeg')
GO
INSERT [dbo].[EventImage] ([EventImageID], [EventID], [EventImageUrl], [ImageType]) VALUES (15, 308, N'bg2a.jpg', N'image/jpeg')
GO
INSERT [dbo].[EventImage] ([EventImageID], [EventID], [EventImageUrl], [ImageType]) VALUES (16, 334, N'391817logo.jpg', N'image/jpeg')
GO
INSERT [dbo].[EventImage] ([EventImageID], [EventID], [EventImageUrl], [ImageType]) VALUES (17, 334, N'affiliate-program_FPO.jpg', N'image/jpeg')
GO
INSERT [dbo].[EventImage] ([EventImageID], [EventID], [EventImageUrl], [ImageType]) VALUES (18, 334, N'AMEX.png', N'image/png')
GO
INSERT [dbo].[EventImage] ([EventImageID], [EventID], [EventImageUrl], [ImageType]) VALUES (19, 334, N'ANALYTICS_FPO.jpg', N'image/jpeg')
GO
INSERT [dbo].[EventImage] ([EventImageID], [EventID], [EventImageUrl], [ImageType]) VALUES (20, 334, N'analytics-fpo.png', N'image/png')
GO
INSERT [dbo].[EventImage] ([EventImageID], [EventID], [EventImageUrl], [ImageType]) VALUES (30, 335, N'391817logo.jpg', N'image/jpeg')
GO
INSERT [dbo].[EventImage] ([EventImageID], [EventID], [EventImageUrl], [ImageType]) VALUES (31, 335, N'bg7.jpg', N'image/jpeg')
GO
SET IDENTITY_INSERT [dbo].[EventImage] OFF
GO
SET IDENTITY_INSERT [dbo].[EventSubCategory] ON 

GO
INSERT [dbo].[EventSubCategory] ([EventSubCategoryID], [EventCategoryID], [EventSubCategory]) VALUES (1, 1, N'Sub Category 1')
GO
INSERT [dbo].[EventSubCategory] ([EventSubCategoryID], [EventCategoryID], [EventSubCategory]) VALUES (2, 1, N'Sub Category 2')
GO
INSERT [dbo].[EventSubCategory] ([EventSubCategoryID], [EventCategoryID], [EventSubCategory]) VALUES (4, 2, N'Sub Category 4')
GO
INSERT [dbo].[EventSubCategory] ([EventSubCategoryID], [EventCategoryID], [EventSubCategory]) VALUES (21, 2, N'Sub Category 3')
GO
INSERT [dbo].[EventSubCategory] ([EventSubCategoryID], [EventCategoryID], [EventSubCategory]) VALUES (25, 10, N'Sub Category 1')
GO
SET IDENTITY_INSERT [dbo].[EventSubCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[EventType] ON 

GO
INSERT [dbo].[EventType] ([EventTypeID], [EventType], [EventHide]) VALUES (1, N'DJ Party', NULL)
GO
INSERT [dbo].[EventType] ([EventTypeID], [EventType], [EventHide]) VALUES (2, N'Company Party', N'Y')
GO
INSERT [dbo].[EventType] ([EventTypeID], [EventType], [EventHide]) VALUES (42, N'Musical Concert', NULL)
GO
INSERT [dbo].[EventType] ([EventTypeID], [EventType], [EventHide]) VALUES (58, N'Event Test Type', NULL)
GO
INSERT [dbo].[EventType] ([EventTypeID], [EventType], [EventHide]) VALUES (72, N'Test Event type', NULL)
GO
SET IDENTITY_INSERT [dbo].[EventType] OFF
GO
SET IDENTITY_INSERT [dbo].[EventVenue] ON 

GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (1, 304, 0, N'03/01/2016', N'03/01/2016', N'7:00pm', N'7:30pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (2, 305, 0, N'02/01/2016', N'02/01/2016', N'7:00pm', N'7:30pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (3, 306, 0, N'12/25/2015', N'12/25/2015', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (4, 307, 0, N'12/25/2015', N'12/25/2015', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (6, 309, 0, N'12/25/2015', N'12/25/2015', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (8, 311, 0, N'02/01/2016', N'02/01/2016', N'7:00pm', N'8:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (9, 314, 0, N'02/01/2016', N'02/01/2016', N'7:00pm', N'7:30pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (10, 315, 0, N'02/01/2016', N'02/01/2016', N'7:00pm', N'10:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (14, 318, 0, N'12/25/2015', N'12/25/2015', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (15, 319, 0, N'12/24/2015', N'12/24/2015', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (19, 320, 0, N'12/25/2015', N'12/25/2015', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (21, 321, 0, N'12/25/2015', N'12/25/2015', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (24, 310, 0, N'12/25/2015', N'12/25/2015', N'8:00pm', N'8:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (25, 322, 0, N'12/30/2015', N'01/31/2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (49, 324, 0, N'02/01/2016', N'02/01/2016', N'7:00pm', N'11:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (50, 323, 0, N'12/29/2015', N'01/31/2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (51, 325, 0, N'02/01/2016', N'02/01/2016', N'7:00pm', N'11:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (52, 326, 0, N'02/01/2016', N'02/01/2016', N'7:00pm', N'11:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (53, 327, 0, N'02/01/2016', N'02/01/2016', N'7:00pm', N'11:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (54, 328, 0, N'02/01/2016', N'02/01/2016', N'7:00pm', N'11:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (60, 316, 0, N'12/24/2015', N'12/31/2015', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (62, 317, 0, N'12/25/2015', N'12/31/2015', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (63, 329, 0, N'02/01/2016', N'02/01/2016', N'7:00pm', N'11:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (64, 330, 0, N'02/06/2016', N'02/06/2016', N'7:00pm', N'8:30pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (65, 331, 0, N'01/06/2016', N'01/31/2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (66, 332, 0, N'01/07/2016', N'01/07/2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (68, 333, 0, N'01/13/2016', N'01/13/2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (69, 334, 0, N'01/08/2016', N'01/08/2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (75, 335, 0, N'01/08/2016', N'01/08/2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (76, 336, 0, N'03/01/2016', N'03/01/2016', N'7:00pm', N'8:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (77, 337, 0, N'05/01/2016', N'05/01/2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (78, 338, 0, N'01/12/2016', N'01/31/2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (88, 340, 0, N'03/01/2016', N'03/01/2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (89, 341, 0, N'03/01/2016', N'03/01/2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (93, 339, 0, N'01/12/2016', N'01/31/2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (94, 342, 0, N'03/01/2016', N'03/01/2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (95, 343, 0, N'01/13/2016', N'01/31/2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (97, 344, 0, N'01/13/2016', N'01/13/2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (98, 345, 0, N'03/01/2016', N'03/01/2016', N'7:00pm', N'8:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (100, 346, 0, N'01/14/2016', N'01/31/2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (102, 348, 0, N'01/16/2016', N'01/16/2016', N'12:00am', N'1:05pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (104, 349, 0, N'01/16/2016', N'01/31/2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (105, 350, 0, N'01/16/2016', N'01/31/2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (107, 351, 0, N'01/16/2016', N'01/31/2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (108, 352, 0, N'01/16/2016', N'01/31/2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (109, 353, 0, N'01/16/2016', N'01/31/2016', N'7:00pm', N'7:00pm')
GO
SET IDENTITY_INSERT [dbo].[EventVenue] OFF
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
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (4, N'MyAccount', N'MyAccountSuccessInitSY', N'Your details has been saved.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (5, N'MyAccount', N'MyAccountSuccessEmailSY', N'Your email has been saved. ')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (6, N'MyAccount', N'MyAccountSuccessPasswordSY', N'Your Password has been saved. ')
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
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1095, N'MyAccount', N'AccEmailPwdchangesys', N'Your Email and Password has been saved. ')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1096, N'TicketPayment', N'TpayMaxTicketReciepts', N'Maximum limit to add ticket recepients:')
GO
SET IDENTITY_INSERT [dbo].[Messages] OFF
GO
SET IDENTITY_INSERT [dbo].[MultipleEvent] ON 

GO
INSERT [dbo].[MultipleEvent] ([MultipleEventID], [EventID], [Frequency], [StartingFrom], [StartingTo], [WeeklyDay], [MonthlyDay], [StartTime], [EndTime], [MonthlyWeek], [MonthlyWeekDays]) VALUES (3, 308, N'Weekly', N'12/24/2015', N'12/24/2015', N'1,2,3', NULL, N'7:00pm', N'7:00pm', NULL, NULL)
GO
INSERT [dbo].[MultipleEvent] ([MultipleEventID], [EventID], [Frequency], [StartingFrom], [StartingTo], [WeeklyDay], [MonthlyDay], [StartTime], [EndTime], [MonthlyWeek], [MonthlyWeekDays]) VALUES (5, 313, N'Monthly', N'12/24/2015', N'12/31/2015', NULL, NULL, N'7:00pm', N'7:00pm', N'First', N'1,2,3')
GO
INSERT [dbo].[MultipleEvent] ([MultipleEventID], [EventID], [Frequency], [StartingFrom], [StartingTo], [WeeklyDay], [MonthlyDay], [StartTime], [EndTime], [MonthlyWeek], [MonthlyWeekDays]) VALUES (6, 312, N'Weekly', N'12/25/2015', N'12/31/2015', N'1,2,3', NULL, N'7:00pm', N'7:00pm', NULL, NULL)
GO
INSERT [dbo].[MultipleEvent] ([MultipleEventID], [EventID], [Frequency], [StartingFrom], [StartingTo], [WeeklyDay], [MonthlyDay], [StartTime], [EndTime], [MonthlyWeek], [MonthlyWeekDays]) VALUES (9, 347, N'Weekly', N'01/15/2016', N'02/29/2016', N'1,3,6', NULL, N'7:00pm', N'11:30pm', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[MultipleEvent] OFF
GO
SET IDENTITY_INSERT [dbo].[Order_Detail_T] ON 

GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (1, N'T000000001', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(2.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), 0, CAST(2.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (2, N'T000000002', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(2.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), 0, CAST(2.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (3, N'T000000003', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(2.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), 0, CAST(2.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (4, N'T000000004', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(2.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), 0, CAST(2.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (5, N'T000000005', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(2.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), 0, CAST(2.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (6, N'T000000006', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', CAST(46.35 AS Numeric(18, 2)), CAST(46.35 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (7, N'T000000007', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', CAST(40.35 AS Numeric(18, 2)), CAST(40.35 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (8, N'T000000008', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', CAST(40.35 AS Numeric(18, 2)), CAST(40.35 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (9, N'T000000009', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', CAST(67.25 AS Numeric(18, 2)), CAST(67.25 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (10, N'T000000010', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(206.00 AS Numeric(18, 2)), CAST(206.00 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (11, N'T000000011', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(30.90 AS Numeric(18, 2)), CAST(30.90 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (12, N'T000000012', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', CAST(13.45 AS Numeric(18, 2)), CAST(13.45 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (13, N'T000000013', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', CAST(13.45 AS Numeric(18, 2)), CAST(13.45 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (14, N'', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(13.45 AS Numeric(18, 2)), CAST(13.45 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (15, N'', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(13.45 AS Numeric(18, 2)), CAST(13.45 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (16, N'', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(13.45 AS Numeric(18, 2)), CAST(13.45 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (17, N'', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(13.45 AS Numeric(18, 2)), CAST(13.45 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (18, N'', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(13.45 AS Numeric(18, 2)), CAST(13.45 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (19, N'', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(13.45 AS Numeric(18, 2)), CAST(13.45 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (20, N'', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(13.45 AS Numeric(18, 2)), CAST(13.45 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (21, N'', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(13.45 AS Numeric(18, 2)), CAST(13.45 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (22, N'', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(84.46 AS Numeric(18, 2)), CAST(84.46 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (23, N'', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(84.46 AS Numeric(18, 2)), CAST(84.46 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (24, N'', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(84.46 AS Numeric(18, 2)), CAST(84.46 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (25, N'T000000025', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(184.46 AS Numeric(18, 2)), CAST(184.46 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (26, N'T000000026', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(42.23 AS Numeric(18, 2)), CAST(42.23 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (27, N'T000000027', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(42.23 AS Numeric(18, 2)), CAST(42.23 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (28, N'T000000028', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(65.23 AS Numeric(18, 2)), CAST(65.23 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (29, N'T000000029', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(65.23 AS Numeric(18, 2)), CAST(65.23 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (30, N'T000000030', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(142.23 AS Numeric(18, 2)), CAST(142.23 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (31, N'T000000031', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(142.23 AS Numeric(18, 2)), CAST(142.23 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (32, N'T000000032', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(42.23 AS Numeric(18, 2)), CAST(42.23 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (33, N'T000000033', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(42.23 AS Numeric(18, 2)), CAST(42.23 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (34, N'T000000034', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(42.23 AS Numeric(18, 2)), CAST(42.23 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (35, N'T000000035', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(42.23 AS Numeric(18, 2)), CAST(42.23 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (36, N'T000000036', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(42.23 AS Numeric(18, 2)), CAST(42.23 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (37, N'T000000037', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(84.46 AS Numeric(18, 2)), CAST(84.46 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (38, N'T000000038', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(84.46 AS Numeric(18, 2)), CAST(84.46 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (39, N'T000000039', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(84.46 AS Numeric(18, 2)), CAST(84.46 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (40, N'T000000040', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(84.46 AS Numeric(18, 2)), CAST(84.46 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (41, N'T000000041', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(84.46 AS Numeric(18, 2)), CAST(84.46 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (42, N'T000000042', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(135.96 AS Numeric(18, 2)), CAST(135.96 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (43, N'T000000043', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(169.95 AS Numeric(18, 2)), CAST(169.95 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (44, N'T000000044', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(169.95 AS Numeric(18, 2)), CAST(169.95 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (45, N'T000000045', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(67.98 AS Numeric(18, 2)), CAST(67.98 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (46, N'T000000046', N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', CAST(33.99 AS Numeric(18, 2)), CAST(33.99 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (47, N'T000000047', N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', CAST(407.88 AS Numeric(18, 2)), CAST(407.88 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (48, N'T000000048', N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', CAST(101.97 AS Numeric(18, 2)), CAST(101.97 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (49, N'T000000049', N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', CAST(67.98 AS Numeric(18, 2)), CAST(67.98 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (50, N'T000000050', N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', CAST(441.87 AS Numeric(18, 2)), CAST(441.87 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (51, N'T000000051', N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', CAST(101.97 AS Numeric(18, 2)), CAST(101.97 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (52, N'T000000052', N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', CAST(101.97 AS Numeric(18, 2)), CAST(101.97 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (53, N'T000000053', N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', CAST(101.97 AS Numeric(18, 2)), CAST(101.97 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (54, N'T000000054', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', CAST(84.46 AS Numeric(18, 2)), CAST(84.46 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (55, N'T000000055', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', CAST(42.23 AS Numeric(18, 2)), CAST(42.23 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (56, N'T000000056', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', CAST(42.23 AS Numeric(18, 2)), CAST(42.23 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (57, N'T000000057', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', CAST(42.23 AS Numeric(18, 2)), CAST(42.23 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (58, N'T000000058', N'16724f9c-389d-43a3-b6b2-bc83989b9102', CAST(35.02 AS Numeric(18, 2)), CAST(35.02 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (59, N'T000000059', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', CAST(42.23 AS Numeric(18, 2)), CAST(42.23 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (60, N'T000000060', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', CAST(42.23 AS Numeric(18, 2)), CAST(42.23 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (61, N'T000000061', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', CAST(42.23 AS Numeric(18, 2)), CAST(42.23 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (62, N'T000000062', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', CAST(42.23 AS Numeric(18, 2)), CAST(42.23 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (63, N'T000000063', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', CAST(42.23 AS Numeric(18, 2)), CAST(42.23 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (64, N'T000000064', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', CAST(42.23 AS Numeric(18, 2)), CAST(42.23 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (65, N'T000000065', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', CAST(42.23 AS Numeric(18, 2)), CAST(42.23 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (66, N'T000000066', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', CAST(42.23 AS Numeric(18, 2)), CAST(42.23 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (67, N'T000000067', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', CAST(42.23 AS Numeric(18, 2)), CAST(42.23 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (68, N'T000000068', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', CAST(42.23 AS Numeric(18, 2)), CAST(42.23 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (69, N'T000000069', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', CAST(42.23 AS Numeric(18, 2)), CAST(42.23 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (70, N'T000000070', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', CAST(42.23 AS Numeric(18, 2)), CAST(42.23 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (71, N'T000000071', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', CAST(42.23 AS Numeric(18, 2)), CAST(42.23 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (72, N'T000000072', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', CAST(42.23 AS Numeric(18, 2)), CAST(42.23 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId]) VALUES (73, N'T000000073', N'e583e85c-bf2f-43e4-9d56-5a20c73796be', CAST(42.23 AS Numeric(18, 2)), CAST(42.23 AS Numeric(18, 2)), 0, CAST(0.00 AS Numeric(18, 2)), 0)
GO
SET IDENTITY_INSERT [dbo].[Order_Detail_T] OFF
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
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10117, N'e142bbfb-ef9c-450b-8d8c-38d41966dca3', N'Shavanya', N'del', N'hjn@hjn.com', N'xzc', N'zxc', N'Bronx', N'New York', N'10463', 1, N'444-444-4444', N'111-111-1111', NULL, NULL, NULL, NULL, NULL, N'', NULL, NULL, NULL, NULL, NULL, NULL, N'', N'Female', N'1-1-1980', NULL, NULL, N'Y', NULL, N'', N'', N'')
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10119, N'6b9b5454-b3fb-4a28-9226-32d588f8d9a8', NULL, NULL, N'jk@jk.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'', N'', N'')
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10120, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', N'Sah', N'ame', N'pintu.sah@kiwitech.com', NULL, NULL, N'Quinby', N'Virginia', N'23423', 1, N'313-312-3321', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'', N'', N'')
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10121, N'93e82fc1-2e9d-46eb-ae31-6dff9c5cece8', NULL, NULL, N'ghj@ghj.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'', N'', N'')
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10122, N'5f158983-4660-4237-929c-70baf1279b56', NULL, NULL, N'fth@fth.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'', N'', N'')
GO
SET IDENTITY_INSERT [dbo].[Profile] OFF
GO
SET IDENTITY_INSERT [dbo].[Publish_Event_Detail] ON 

GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (15, 303, N'3', 0, 2, N'172,173', NULL, NULL, NULL)
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (16, 304, N'4', 0, 1, N'174,175', N'Mar 01, 2016', N'7:00pm', N'7:30pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (17, 305, N'5', 0, 2, N'176,177', N'Feb 01, 2016', N'7:00pm', N'7:30pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (18, 306, N'6', 0, 3, N'178', N'Dec 25, 2015', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (19, 307, N'7', 0, 4, N'179,180', N'Dec 25, 2015', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (22, 311, N'12', 0, 8, N'185', N'Feb 01, 2016', N'7:00pm', N'8:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (26, 313, N'', 5, 0, N'187', N'Dec 28 2015 12:00AM', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (27, 313, N'', 5, 0, N'187', N'Dec 29 2015 12:00AM', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (28, 313, N'', 5, 0, N'187', N'Dec 30 2015 12:00AM', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (29, 312, N'', 6, 0, N'186', N'Dec 28, 2015', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (30, 312, N'', 6, 0, N'186', N'Dec 29, 2015', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (31, 312, N'', 6, 0, N'186', N'Dec 30, 2015', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (32, 314, N'13', 0, 9, N'188', N'Feb 01, 2016', N'7:00pm', N'7:30pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (33, 315, N'14', 0, 10, N'189,190', N'Feb 01, 2016', N'7:00pm', N'10:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (37, 318, N'17', 0, 14, N'193', N'Dec 25, 2015', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (38, 319, N'18', 0, 15, N'194', N'Dec 24, 2015', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (42, 320, N'19,20', 0, 19, N'195', N'Dec 25, 2015', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (44, 321, N'21,22', 0, 21, N'196,197', N'Dec 25, 2015', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (47, 310, N'23', 0, 24, N'184', N'Dec 25, 2015', N'8:00pm', N'8:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (48, 322, N'', 0, 25, N'198', N'Dec 30, 2015', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (72, 324, N'27', 0, 49, N'200', N'Feb 01, 2016', N'7:00pm', N'11:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (73, 323, N'', 0, 50, N'199,201', N'Dec 29, 2015', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (74, 325, N'28', 0, 51, N'202', N'Feb 01, 2016', N'7:00pm', N'11:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (75, 326, N'29', 0, 52, N'203', N'Feb 01, 2016', N'7:00pm', N'11:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (76, 327, N'30', 0, 53, N'204', N'Feb 01, 2016', N'7:00pm', N'11:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (77, 328, N'31', 0, 54, N'205', N'Feb 01, 2016', N'7:00pm', N'11:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (83, 316, N'', 0, 60, N'191,206', N'Dec 24, 2015', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (85, 317, N'', 0, 62, N'192', N'Dec 25, 2015', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (86, 329, N'32', 0, 63, N'207', N'Feb 01, 2016', N'7:00pm', N'11:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (87, 330, N'33', 0, 64, N'208', N'Feb 06, 2016', N'7:00pm', N'8:30pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (88, 331, N'', 0, 65, N'209,210', N'Jan 06, 2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (89, 332, N'', 0, 66, N'211', N'Jan 07, 2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (91, 333, N'', 0, 68, N'212', N'Jan 13, 2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (92, 334, N'', 0, 69, N'213', N'Jan 08, 2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (98, 335, N'38', 0, 75, N'214', N'Jan 08, 2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (99, 336, N'39', 0, 76, N'215', N'Mar 01, 2016', N'7:00pm', N'8:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (100, 337, N'40', 0, 77, N'216', N'May 01, 2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (101, 338, N'', 0, 78, N'217', N'Jan 12, 2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (111, 340, N'41', 0, 88, N'220', N'Mar 01, 2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (112, 341, N'42', 0, 89, N'221', N'Mar 01, 2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (116, 339, N'', 0, 93, N'218,219', N'Jan 12, 2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (117, 342, N'43', 0, 94, N'222', N'Mar 01, 2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (118, 343, N'', 0, 95, N'223', N'Jan 13, 2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (120, 344, N'', 0, 97, N'224', N'Jan 13, 2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (121, 345, N'44', 0, 98, N'225,226', N'Mar 01, 2016', N'7:00pm', N'8:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (123, 346, N'', 0, 100, N'227', N'Jan 14, 2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (164, 347, N'45,46', 9, 0, N'228,229,230', N'Jan 16, 2016', N'7:00pm', N'11:30pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (165, 347, N'45,46', 9, 0, N'228,229,230', N'Jan 18, 2016', N'7:00pm', N'11:30pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (166, 347, N'45,46', 9, 0, N'228,229,230', N'Jan 20, 2016', N'7:00pm', N'11:30pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (167, 347, N'45,46', 9, 0, N'228,229,230', N'Jan 23, 2016', N'7:00pm', N'11:30pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (168, 347, N'45,46', 9, 0, N'228,229,230', N'Jan 25, 2016', N'7:00pm', N'11:30pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (169, 347, N'45,46', 9, 0, N'228,229,230', N'Jan 27, 2016', N'7:00pm', N'11:30pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (170, 347, N'45,46', 9, 0, N'228,229,230', N'Jan 30, 2016', N'7:00pm', N'11:30pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (171, 347, N'45,46', 9, 0, N'228,229,230', N'Feb 01, 2016', N'7:00pm', N'11:30pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (172, 347, N'45,46', 9, 0, N'228,229,230', N'Feb 03, 2016', N'7:00pm', N'11:30pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (173, 347, N'45,46', 9, 0, N'228,229,230', N'Feb 06, 2016', N'7:00pm', N'11:30pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (174, 347, N'45,46', 9, 0, N'228,229,230', N'Feb 08, 2016', N'7:00pm', N'11:30pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (175, 347, N'45,46', 9, 0, N'228,229,230', N'Feb 10, 2016', N'7:00pm', N'11:30pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (176, 347, N'45,46', 9, 0, N'228,229,230', N'Feb 13, 2016', N'7:00pm', N'11:30pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (177, 347, N'45,46', 9, 0, N'228,229,230', N'Feb 15, 2016', N'7:00pm', N'11:30pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (178, 347, N'45,46', 9, 0, N'228,229,230', N'Feb 17, 2016', N'7:00pm', N'11:30pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (179, 347, N'45,46', 9, 0, N'228,229,230', N'Feb 20, 2016', N'7:00pm', N'11:30pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (180, 347, N'45,46', 9, 0, N'228,229,230', N'Feb 22, 2016', N'7:00pm', N'11:30pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (181, 347, N'45,46', 9, 0, N'228,229,230', N'Feb 24, 2016', N'7:00pm', N'11:30pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (182, 347, N'45,46', 9, 0, N'228,229,230', N'Feb 27, 2016', N'7:00pm', N'11:30pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (183, 347, N'45,46', 9, 0, N'228,229,230', N'Feb 29, 2016', N'7:00pm', N'11:30pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (185, 348, N'48', 0, 102, N'231', N'Jan 16, 2016', N'12:00am', N'1:05pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (187, 349, N'', 0, 104, N'232', N'Jan 16, 2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (188, 350, N'49', 0, 105, N'233,234', N'Jan 16, 2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (190, 351, N'', 0, 107, N'235', N'Jan 16, 2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (191, 352, N'', 0, 108, N'236', N'Jan 16, 2016', N'7:00pm', N'7:00pm')
GO
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (192, 353, N'', 0, 109, N'237', N'Jan 16, 2016', N'7:00pm', N'7:00pm')
GO
SET IDENTITY_INSERT [dbo].[Publish_Event_Detail] OFF
GO
SET IDENTITY_INSERT [dbo].[ShippingAddress] ON 

GO
INSERT [dbo].[ShippingAddress] ([ShippingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (1, N'f4ee24cd-0194-45eb-ae30-0c92e151a7dc                                                                                            ', N'0', N'f4ee24cd-0194-45eb-ae30-0c92e151a7dc                                                                                            ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1')
GO
INSERT [dbo].[ShippingAddress] ([ShippingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (2, N'4db67acb-df7c-44b2-b156-ceae1c6649bc                                                                                            ', N'T000000032', N'4f34e3e7-bdb0-4784-b957-4687469c826c                                                                                            ', N'sss', N'sss', N'111-111-1111', N'dfsdf', N'sdfsdf', N'dfsdf', N'sdfsdf', N'sdfsd', N'1')
GO
INSERT [dbo].[ShippingAddress] ([ShippingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (3, N'4db67acb-df7c-44b2-b156-ceae1c6649bc                                                                                            ', N'T000000033', N'5fda93b2-35e0-4f51-93b8-2329106a7ab4                                                                                            ', N'sdfsdf', N'fsdf', N'111-111-1111', N'dsfsdf', N'sdfsdf', N'sdfsdf', N'sdfsdf', N'sdfsd', N'1')
GO
INSERT [dbo].[ShippingAddress] ([ShippingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (4, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000079', N'f69db80a-370b-4492-a71c-4fa7976b1270                                                                                            ', N'ss', N'ss', N'222-222-2222', N'ss', N'sss', N'222', N'2222', N'222', N'15')
GO
INSERT [dbo].[ShippingAddress] ([ShippingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (5, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000080', N'5c040140-733a-4ac5-b847-96acc06fc4f1                                                                                            ', N'gf', N'fdg', N'333-333-3333', N'fdgdfg', N'dfgdf', N'erer', N'erer', N'3233', N'1')
GO
INSERT [dbo].[ShippingAddress] ([ShippingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (6, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000081', N'6c1a09f5-de46-4332-bcd1-53ec7b20937c                                                                                            ', N'fgdfg', N'dfgdfg', N'333-333-3333', N'fgdfg', N'dfgdfg', N'sdfsdf', N'sfsdf', N'222', N'1')
GO
INSERT [dbo].[ShippingAddress] ([ShippingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (7, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000082', N'79ca54bd-6174-473b-9012-d54a395f9b4c                                                                                            ', N'dfgfdg', N'dfg', N'333-333-3333', N'vbh', N'gffg', N'333', N'3333', N'ffff', N'1')
GO
INSERT [dbo].[ShippingAddress] ([ShippingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (8, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000083', N'e6a9cb90-04f6-4545-bde4-61d486b985c6                                                                                            ', N'sss', N'Sss', N'111-111-1111', N'1d', N'dd', N'dsd', N'sdd', N'sdsd', N'1')
GO
INSERT [dbo].[ShippingAddress] ([ShippingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (9, N'16724f9c-389d-43a3-b6b2-bc83989b9102                                                                                            ', N'T000000088', N'029c4dbd-2e7b-41b1-ae4d-c48be99b1b59                                                                                            ', N'fgh', N'dgh', N'333-333-3333', N'dfg', N'dfg', N'23', N'23', N'dfg', N'1')
GO
INSERT [dbo].[ShippingAddress] ([ShippingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (10, N'e583e85c-bf2f-43e4-9d56-5a20c73796be                                                                                            ', N'T000000185', N'6f3057ac-afd8-4a1d-99da-1005f9f53b8f                                                                                            ', N'dgdfg', N'dfgdfg', N'555-555-5555', N'fghfgh', N'fghfgh', N'Pittsburgh', N'Pennsylvania', N'15216', N'1')
GO
SET IDENTITY_INSERT [dbo].[ShippingAddress] OFF
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
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (215, 336, N'Paid amount', 500, CAST(85.00 AS Decimal(9, 2)), 2, NULL, NULL, N'desc', N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(2.55 AS Decimal(9, 2)), NULL, N'0', NULL, CAST(5.00 AS Decimal(9, 2)), CAST(87.55 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (216, 337, N'Paid Ticket', 500, CAST(12.00 AS Decimal(9, 2)), 2, NULL, NULL, N'Desc', N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.36 AS Decimal(9, 2)), NULL, N'0', NULL, CAST(2.00 AS Decimal(9, 2)), CAST(12.36 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (217, 338, N'this is', 23, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'1', N'1', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), NULL, NULL, N'0', NULL, NULL, NULL)
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (218, 339, N'this ', 23, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'1', N'1', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.00 AS Decimal(9, 2)), N'0', N'0', NULL, NULL, CAST(0.00 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (219, 339, N'cv', 34, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, N'7:00pm', NULL, N'7:00pm', N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 1, CAST(0.00 AS Decimal(9, 2)), CAST(0.00 AS Decimal(9, 2)), N'0', N'0', NULL, NULL, CAST(0.00 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (220, 340, N'Paid TIcketr', 150, CAST(18.00 AS Decimal(9, 2)), 2, NULL, NULL, N'Desc', N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.54 AS Decimal(9, 2)), NULL, N'0', NULL, CAST(3.00 AS Decimal(9, 2)), CAST(18.54 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (221, 341, N'Paid TIcketr', 150, CAST(18.00 AS Decimal(9, 2)), 2, NULL, NULL, N'Desc', N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.54 AS Decimal(9, 2)), NULL, N'0', NULL, CAST(3.00 AS Decimal(9, 2)), CAST(18.54 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (222, 342, N'Paid TIcketr', 150, CAST(18.00 AS Decimal(9, 2)), 2, NULL, NULL, N'Desc', N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.54 AS Decimal(9, 2)), NULL, N'0', NULL, CAST(3.00 AS Decimal(9, 2)), CAST(18.54 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (223, 343, N'donation test', 23, NULL, 3, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), NULL, NULL, N'0', NULL, NULL, NULL)
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (224, 344, N'sdf', 23, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.00 AS Decimal(9, 2)), N'0', N'0', NULL, NULL, CAST(0.00 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (225, 345, N'Paid ', 450, CAST(41.00 AS Decimal(9, 2)), 2, NULL, NULL, N'paid', N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(1.23 AS Decimal(9, 2)), NULL, N'0', NULL, NULL, CAST(42.23 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (226, 345, N'Donation Only', 5, NULL, 3, NULL, NULL, N'Donation', N'1', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 1, CAST(0.00 AS Decimal(9, 2)), NULL, NULL, N'0', NULL, NULL, NULL)
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (227, 346, N'zdf', 23, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.00 AS Decimal(9, 2)), N'0', N'0', NULL, NULL, CAST(0.00 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (228, 347, N'zxc', 42, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.00 AS Decimal(9, 2)), N'0', N'0', NULL, NULL, CAST(0.00 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (229, 347, N'dfg', 56, CAST(33.00 AS Decimal(9, 2)), 2, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 1, CAST(0.00 AS Decimal(9, 2)), CAST(0.99 AS Decimal(9, 2)), N'0', N'0', NULL, NULL, CAST(33.99 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (230, 347, N'fgh', 34, NULL, 3, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 2, CAST(0.00 AS Decimal(9, 2)), CAST(0.99 AS Decimal(9, 2)), N'0', N'0', NULL, NULL, NULL)
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (231, 348, N'adfsdf', 234, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.00 AS Decimal(9, 2)), N'0', N'0', NULL, NULL, CAST(0.00 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (232, 349, N'gbc', 45, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.00 AS Decimal(9, 2)), N'0', N'0', NULL, NULL, CAST(0.00 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (233, 350, N'sdf', 34, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), NULL, NULL, N'0', NULL, NULL, NULL)
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (234, 350, N'xcvb', 34, CAST(34.00 AS Decimal(9, 2)), 2, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 1, CAST(0.00 AS Decimal(9, 2)), CAST(1.02 AS Decimal(9, 2)), NULL, N'0', NULL, NULL, CAST(35.02 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (235, 351, N'dsf', 23, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), CAST(0.00 AS Decimal(9, 2)), N'0', N'0', NULL, NULL, CAST(0.00 AS Decimal(9, 2)))
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (236, 352, N'sdf', 23, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), NULL, NULL, N'0', NULL, NULL, NULL)
GO
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice]) VALUES (237, 353, N'asd', 23, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(0.00 AS Decimal(9, 2)), NULL, NULL, N'0', NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Ticket] OFF
GO
SET IDENTITY_INSERT [dbo].[Ticket_Locked_Detail] ON 

GO
INSERT [dbo].[Ticket_Locked_Detail] ([TLD_Id], [TLD_User_Id], [TLD_Locked_Qty], [TLD_Event_Id], [TLD_TQD_Id], [Locktime], [TLD_GUID], [TLD_Donate], [TicketAmount]) VALUES (63, N'', 2, 345, 130, CAST(0x0000A58D01351F01 AS DateTime), N'6a02eb03-1939-46d9-a153-c08491817d5d', CAST(0 AS Decimal(18, 0)), CAST(84.46 AS Decimal(18, 2)))
GO
INSERT [dbo].[Ticket_Locked_Detail] ([TLD_Id], [TLD_User_Id], [TLD_Locked_Qty], [TLD_Event_Id], [TLD_TQD_Id], [Locktime], [TLD_GUID], [TLD_Donate], [TicketAmount]) VALUES (64, N'', 2, 345, 130, CAST(0x0000A58D01365E5E AS DateTime), N'3ecbdd61-6fa0-4180-8c39-28fc97e4bf45', CAST(0 AS Decimal(18, 0)), CAST(84.46 AS Decimal(18, 2)))
GO
INSERT [dbo].[Ticket_Locked_Detail] ([TLD_Id], [TLD_User_Id], [TLD_Locked_Qty], [TLD_Event_Id], [TLD_TQD_Id], [Locktime], [TLD_GUID], [TLD_Donate], [TicketAmount]) VALUES (65, N'', 0, 345, 131, CAST(0x0000A58D01365E60 AS DateTime), N'3ecbdd61-6fa0-4180-8c39-28fc97e4bf45', CAST(0 AS Decimal(18, 0)), CAST(0.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Ticket_Locked_Detail] ([TLD_Id], [TLD_User_Id], [TLD_Locked_Qty], [TLD_Event_Id], [TLD_TQD_Id], [Locktime], [TLD_GUID], [TLD_Donate], [TicketAmount]) VALUES (85, N'', 1, 345, 130, CAST(0x0000A58E00DA8BC6 AS DateTime), N'33f8f215-36f1-4197-bd1d-8b58b4efa407', CAST(0 AS Decimal(18, 0)), CAST(42.23 AS Decimal(18, 2)))
GO
INSERT [dbo].[Ticket_Locked_Detail] ([TLD_Id], [TLD_User_Id], [TLD_Locked_Qty], [TLD_Event_Id], [TLD_TQD_Id], [Locktime], [TLD_GUID], [TLD_Donate], [TicketAmount]) VALUES (235, N'', 1, 345, 130, CAST(0x0000A58F00DDC4D6 AS DateTime), N'891a7cf1-5916-4ff1-8371-61bc9a765ff5', CAST(0 AS Decimal(18, 0)), CAST(42.23 AS Decimal(18, 2)))
GO
INSERT [dbo].[Ticket_Locked_Detail] ([TLD_Id], [TLD_User_Id], [TLD_Locked_Qty], [TLD_Event_Id], [TLD_TQD_Id], [Locktime], [TLD_GUID], [TLD_Donate], [TicketAmount]) VALUES (245, N'16724f9c-389d-43a3-b6b2-bc83989b9102', 1, 350, 499, CAST(0x0000A58F00E81820 AS DateTime), N'83dfc91e-acd2-4e39-b281-79029e12542d', CAST(0 AS Decimal(18, 0)), CAST(35.02 AS Decimal(18, 2)))
GO
INSERT [dbo].[Ticket_Locked_Detail] ([TLD_Id], [TLD_User_Id], [TLD_Locked_Qty], [TLD_Event_Id], [TLD_TQD_Id], [Locktime], [TLD_GUID], [TLD_Donate], [TicketAmount]) VALUES (247, N'', 1, 345, 130, CAST(0x0000A58F00E972D0 AS DateTime), N'8083f99e-83c2-4c35-be7c-7cb1d9050e11', CAST(0 AS Decimal(18, 0)), CAST(42.23 AS Decimal(18, 2)))
GO
INSERT [dbo].[Ticket_Locked_Detail] ([TLD_Id], [TLD_User_Id], [TLD_Locked_Qty], [TLD_Event_Id], [TLD_TQD_Id], [Locktime], [TLD_GUID], [TLD_Donate], [TicketAmount]) VALUES (253, N'', 1, 345, 130, CAST(0x0000A58F00FDB452 AS DateTime), N'8096d67a-2bfd-4717-a20b-d89a52ca95ab', CAST(0 AS Decimal(18, 0)), CAST(42.23 AS Decimal(18, 2)))
GO
INSERT [dbo].[Ticket_Locked_Detail] ([TLD_Id], [TLD_User_Id], [TLD_Locked_Qty], [TLD_Event_Id], [TLD_TQD_Id], [Locktime], [TLD_GUID], [TLD_Donate], [TicketAmount]) VALUES (257, N'', 2, 347, 375, CAST(0x0000A58F01027AB2 AS DateTime), N'a8e3e195-c208-482c-b2ea-c33a99bd5e2c', CAST(0 AS Decimal(18, 0)), CAST(67.98 AS Decimal(18, 2)))
GO
INSERT [dbo].[Ticket_Locked_Detail] ([TLD_Id], [TLD_User_Id], [TLD_Locked_Qty], [TLD_Event_Id], [TLD_TQD_Id], [Locktime], [TLD_GUID], [TLD_Donate], [TicketAmount]) VALUES (260, N'', 1, 345, 130, CAST(0x0000A58F01077218 AS DateTime), N'8c3587f1-3ee4-489f-a2a8-e348a0539c05', CAST(0 AS Decimal(18, 0)), CAST(42.23 AS Decimal(18, 2)))
GO
INSERT [dbo].[Ticket_Locked_Detail] ([TLD_Id], [TLD_User_Id], [TLD_Locked_Qty], [TLD_Event_Id], [TLD_TQD_Id], [Locktime], [TLD_GUID], [TLD_Donate], [TicketAmount]) VALUES (265, N'', 1, 345, 130, CAST(0x0000A59100D0E428 AS DateTime), N'0cd258ca-64c9-47f1-95d6-ce8e313c8776', CAST(0 AS Decimal(18, 0)), CAST(42.23 AS Decimal(18, 2)))
GO
INSERT [dbo].[Ticket_Locked_Detail] ([TLD_Id], [TLD_User_Id], [TLD_Locked_Qty], [TLD_Event_Id], [TLD_TQD_Id], [Locktime], [TLD_GUID], [TLD_Donate], [TicketAmount]) VALUES (268, N'', 1, 345, 130, CAST(0x0000A59100F92178 AS DateTime), N'c466ada4-f77f-42af-a190-729022642b96', CAST(0 AS Decimal(18, 0)), CAST(42.23 AS Decimal(18, 2)))
GO
INSERT [dbo].[Ticket_Locked_Detail] ([TLD_Id], [TLD_User_Id], [TLD_Locked_Qty], [TLD_Event_Id], [TLD_TQD_Id], [Locktime], [TLD_GUID], [TLD_Donate], [TicketAmount]) VALUES (271, N'', 2, 345, 130, CAST(0x0000A59100FAE951 AS DateTime), N'd93f756e-87df-47b7-9ce0-684d1e70cfa7', CAST(0 AS Decimal(18, 0)), CAST(84.46 AS Decimal(18, 2)))
GO
INSERT [dbo].[Ticket_Locked_Detail] ([TLD_Id], [TLD_User_Id], [TLD_Locked_Qty], [TLD_Event_Id], [TLD_TQD_Id], [Locktime], [TLD_GUID], [TLD_Donate], [TicketAmount]) VALUES (274, N'', 1, 345, 130, CAST(0x0000A591010E6B60 AS DateTime), N'c3b1954f-e0b4-453e-9df4-c648380f8fac', CAST(0 AS Decimal(18, 0)), CAST(42.23 AS Decimal(18, 2)))
GO
INSERT [dbo].[Ticket_Locked_Detail] ([TLD_Id], [TLD_User_Id], [TLD_Locked_Qty], [TLD_Event_Id], [TLD_TQD_Id], [Locktime], [TLD_GUID], [TLD_Donate], [TicketAmount]) VALUES (291, N'', 1, 345, 130, CAST(0x0000A59101428809 AS DateTime), N'bce075d8-36f9-47a0-b1c8-a07145065e86', CAST(0 AS Decimal(18, 0)), CAST(42.23 AS Decimal(18, 2)))
GO
INSERT [dbo].[Ticket_Locked_Detail] ([TLD_Id], [TLD_User_Id], [TLD_Locked_Qty], [TLD_Event_Id], [TLD_TQD_Id], [Locktime], [TLD_GUID], [TLD_Donate], [TicketAmount]) VALUES (292, N'', 1, 345, 130, CAST(0x0000A5910142BA2B AS DateTime), N'79cd7e2a-2104-43bb-968f-f0dd9f0b8f43', CAST(0 AS Decimal(18, 0)), CAST(42.23 AS Decimal(18, 2)))
GO
INSERT [dbo].[Ticket_Locked_Detail] ([TLD_Id], [TLD_User_Id], [TLD_Locked_Qty], [TLD_Event_Id], [TLD_TQD_Id], [Locktime], [TLD_GUID], [TLD_Donate], [TicketAmount]) VALUES (293, N'', 1, 345, 130, CAST(0x0000A591014335A1 AS DateTime), N'2040d77b-c0c1-41b2-a24f-be512a76e485', CAST(0 AS Decimal(18, 0)), CAST(42.23 AS Decimal(18, 2)))
GO
INSERT [dbo].[Ticket_Locked_Detail] ([TLD_Id], [TLD_User_Id], [TLD_Locked_Qty], [TLD_Event_Id], [TLD_TQD_Id], [Locktime], [TLD_GUID], [TLD_Donate], [TicketAmount]) VALUES (294, N'', 1, 345, 130, CAST(0x0000A5910143B590 AS DateTime), N'b9fdd016-8115-4479-aeda-f000da407c0f', CAST(0 AS Decimal(18, 0)), CAST(42.23 AS Decimal(18, 2)))
GO
SET IDENTITY_INSERT [dbo].[Ticket_Locked_Detail] OFF
GO
SET IDENTITY_INSERT [dbo].[Ticket_Purchased_Detail] ON 

GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (1, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000001', 1, 2, 304, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'bb9e5d98-cfc9-4781-b268-08ea20154f7c')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (2, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000002', 2, 2, 304, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'7ce0663a-09c4-4b69-9319-9811d177cb91')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (3, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000003', 1, 2, 304, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'e045a1b2-8db7-4fde-ace6-5470cbc0c64c')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (4, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000004', 1, 2, 304, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1311eb23-fac0-45c1-8c74-4450f2157e36')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (5, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000005', 1, 2, 304, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'f08b6a27-6a0f-4aab-9762-d9df9457deda')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (6, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'T000000006', 3, 20, 314, CAST(46.34 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'7d5bc66e-4a26-4c00-bef5-cb0927876c8e')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (7, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'T000000007', 3, 66, 324, CAST(40.34 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'8ca437bc-9f6a-40e6-be88-431c31bbd74d')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (8, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'T000000008', 3, 87, 329, CAST(40.34 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'0dce9cb7-cca0-4e15-843c-8f0cf432e611')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (9, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'T000000009', 5, 87, 329, CAST(67.25 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'a4ebc8ae-c5aa-430f-9c61-5f129bad71be')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (10, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000010', 10, 89, 331, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'358adf81-e90d-484e-a556-0bf5a7cf57d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (11, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000010', 10, 90, 331, CAST(206.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'358adf81-e90d-484e-a556-0bf5a7cf57d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (12, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000011', 2, 20, 314, CAST(30.90 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'03309785-f76a-4811-b8d9-909c723c32e8')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (13, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'T000000012', 1, 87, 329, CAST(13.45 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1785b16f-5743-4b2e-b15b-89fddf953dda')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (14, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'T000000013', 1, 87, 329, CAST(13.45 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'160dec41-bc31-41c9-8fc0-a402bf1c6d77')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (15, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'', 1, 87, 329, CAST(13.45 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'53f19093-bec4-44ed-bd4c-3576e0efa773')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (16, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'', 1, 87, 329, CAST(13.45 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'864af49c-5026-4af4-b618-16caa470b9cc')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (17, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'', 1, 87, 329, CAST(13.45 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'f8e35f5f-54e3-4833-8a87-daaab49cb0d4')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (18, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'', 1, 87, 329, CAST(13.45 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'b826e6d9-474e-4716-b656-de2c3142a510')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (19, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'', 1, 87, 329, CAST(13.45 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'335343b4-1344-4988-8951-f7f04140b8b0')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (20, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'', 1, 87, 329, CAST(13.45 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'17fc0693-c139-471f-a828-457f23d3d6e6')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (21, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'', 1, 87, 329, CAST(13.45 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'91cb20de-c869-4488-ad40-57478302ab11')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (22, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'', 2, 130, 345, CAST(84.46 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'6a02eb03-1939-46d9-a153-c08491817d5d')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (23, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'', 2, 130, 345, CAST(84.46 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'3ecbdd61-6fa0-4180-8c39-28fc97e4bf45')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (24, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'', 0, 131, 345, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'3ecbdd61-6fa0-4180-8c39-28fc97e4bf45')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (25, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'', 2, 130, 345, CAST(84.46 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'd35d9959-8ec9-4334-911f-9d53249a4fc3')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (26, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000025', 2, 130, 345, CAST(84.46 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'9c8975d7-3e99-40f0-a383-d103b1d65976')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (27, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000025', 1, 131, 345, CAST(0.00 AS Numeric(9, 2)), CAST(100 AS Decimal(18, 0)), N'9c8975d7-3e99-40f0-a383-d103b1d65976')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (28, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000026', 1, 130, 345, CAST(42.23 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'cf2fbe8f-cf1c-4c7c-bae8-8e9a4f48ef7a')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (29, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000026', 0, 131, 345, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'cf2fbe8f-cf1c-4c7c-bae8-8e9a4f48ef7a')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (30, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000027', 1, 130, 345, CAST(42.23 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'07127abd-3cc5-4f98-bdf7-543c0a7047de')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (31, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000028', 1, 130, 345, CAST(42.23 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'638f28d5-bdd7-4f44-8d19-ad7ac92e296f')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (32, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000028', 1, 131, 345, CAST(0.00 AS Numeric(9, 2)), CAST(23 AS Decimal(18, 0)), N'638f28d5-bdd7-4f44-8d19-ad7ac92e296f')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (33, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000029', 1, 130, 345, CAST(42.23 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'e727223b-7a00-4764-82f9-ed18d2e40eae')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (34, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000029', 1, 131, 345, CAST(0.00 AS Numeric(9, 2)), CAST(23 AS Decimal(18, 0)), N'e727223b-7a00-4764-82f9-ed18d2e40eae')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (35, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000030', 1, 130, 345, CAST(42.23 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'33912bc6-fe0c-49c8-b97c-75ac6d8446c7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (36, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000030', 1, 131, 345, CAST(0.00 AS Numeric(9, 2)), CAST(100 AS Decimal(18, 0)), N'33912bc6-fe0c-49c8-b97c-75ac6d8446c7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (37, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000031', 1, 130, 345, CAST(42.23 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'97829b43-b9e2-44a6-819f-f9affdff0bce')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (38, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000031', 1, 131, 345, CAST(0.00 AS Numeric(9, 2)), CAST(100 AS Decimal(18, 0)), N'97829b43-b9e2-44a6-819f-f9affdff0bce')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (39, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000032', 1, 130, 345, CAST(42.23 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'b75909c3-3990-4ea6-82eb-b62ed7c2f1e3')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (40, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000032', 0, 131, 345, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'b75909c3-3990-4ea6-82eb-b62ed7c2f1e3')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (41, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000033', 1, 130, 345, CAST(42.23 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'c2cd6685-1fbb-4933-9a72-b47aea98c673')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (42, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000034', 1, 130, 345, CAST(42.23 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'fe8a7e7d-4cfd-4d41-93ee-06b0ab570705')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (43, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000034', 0, 131, 345, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'fe8a7e7d-4cfd-4d41-93ee-06b0ab570705')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (44, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000035', 1, 130, 345, CAST(42.23 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'2af19d1d-24e8-49d9-b338-92691aa4bc7b')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (45, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000036', 1, 130, 345, CAST(42.23 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'db5df513-7b83-43e0-8c7d-b7bb9a7f814f')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (46, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000036', 0, 131, 345, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'db5df513-7b83-43e0-8c7d-b7bb9a7f814f')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (47, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000037', 2, 130, 345, CAST(84.46 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'ce4af788-f35b-4506-8078-f2e3121e34b5')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (48, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000037', 0, 131, 345, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'ce4af788-f35b-4506-8078-f2e3121e34b5')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (49, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000038', 2, 130, 345, CAST(84.46 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'f3f47565-7dbc-4271-ac3c-cb9ea5a06b65')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (50, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000039', 2, 130, 345, CAST(84.46 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'54919902-d251-4ccc-af6f-e9e10bab1ab8')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (51, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000039', 0, 131, 345, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'54919902-d251-4ccc-af6f-e9e10bab1ab8')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (52, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000040', 2, 130, 345, CAST(84.46 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'668f65cb-40fc-447e-ba4c-9c8fcbdd9433')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (53, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000040', 0, 131, 345, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'668f65cb-40fc-447e-ba4c-9c8fcbdd9433')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (54, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000041', 2, 130, 345, CAST(84.46 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'f5baa6b5-08dc-421f-b641-1a2e2451e08d')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (55, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 374, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (56, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 2, 375, 347, CAST(67.98 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (57, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 377, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (58, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 2, 378, 347, CAST(67.98 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (59, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 382, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (60, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 385, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (61, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 388, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (62, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 391, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (63, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 394, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (64, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 397, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (65, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 400, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (66, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 403, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (67, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 406, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (68, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 409, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (69, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 412, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (70, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 415, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (71, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 418, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (72, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 421, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (73, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 424, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (74, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 427, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (75, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 430, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (76, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 433, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (77, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 436, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (78, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 439, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (79, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 442, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (80, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 445, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (81, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 448, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (82, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 451, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (83, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 454, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (84, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 457, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (85, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 460, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (86, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 463, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (87, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 466, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (88, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 469, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (89, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 472, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (90, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 475, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (91, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 478, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (92, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 481, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (93, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 484, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (94, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 487, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (95, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 490, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (96, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000042', 0, 493, 347, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'1449a942-5dbe-4a97-9e84-12c12aa149d7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (97, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000043', 1, 375, 347, CAST(33.99 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'45cea9ce-9faa-434e-a38a-49511e0a5282')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (98, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000043', 1, 378, 347, CAST(33.99 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'45cea9ce-9faa-434e-a38a-49511e0a5282')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (99, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000043', 1, 381, 347, CAST(33.99 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'45cea9ce-9faa-434e-a38a-49511e0a5282')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (100, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000043', 1, 426, 347, CAST(33.99 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'45cea9ce-9faa-434e-a38a-49511e0a5282')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (101, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000043', 1, 492, 347, CAST(33.99 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'45cea9ce-9faa-434e-a38a-49511e0a5282')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (102, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000044', 1, 375, 347, CAST(33.99 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'bc0e1f28-f598-4ba4-94a4-3ff6c75d789f')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (103, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000044', 1, 378, 347, CAST(33.99 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'bc0e1f28-f598-4ba4-94a4-3ff6c75d789f')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (104, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000044', 1, 381, 347, CAST(33.99 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'bc0e1f28-f598-4ba4-94a4-3ff6c75d789f')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (105, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000044', 1, 426, 347, CAST(33.99 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'bc0e1f28-f598-4ba4-94a4-3ff6c75d789f')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (106, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000044', 1, 492, 347, CAST(33.99 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'bc0e1f28-f598-4ba4-94a4-3ff6c75d789f')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (107, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000045', 1, 375, 347, CAST(33.99 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'a0c543e5-7cfa-43ee-b253-5ecd9dbe205b')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (108, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000045', 1, 378, 347, CAST(33.99 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'a0c543e5-7cfa-43ee-b253-5ecd9dbe205b')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (109, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', N'T000000046', 1, 375, 347, CAST(33.99 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'6e013a72-07b5-4eb6-9786-0d7e9c27c0c8')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (110, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', N'T000000047', 12, 375, 347, CAST(407.88 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'8279e2fa-8764-4825-bfc9-1068c38ea3c7')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (111, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', N'T000000048', 1, 375, 347, CAST(33.99 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'd66d84ee-727a-4263-b05c-aeb0cf1693e5')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (112, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', N'T000000048', 2, 381, 347, CAST(67.98 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'd66d84ee-727a-4263-b05c-aeb0cf1693e5')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (113, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', N'T000000049', 1, 375, 347, CAST(33.99 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'fe5e7fa0-3bcb-40d0-bc8b-232cf9b6748d')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (114, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', N'T000000049', 1, 381, 347, CAST(33.99 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'fe5e7fa0-3bcb-40d0-bc8b-232cf9b6748d')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (115, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', N'T000000050', 1, 375, 347, CAST(33.99 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'69aa57f2-ea9c-49ac-b1bc-bd8e7cfd6c71')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (116, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', N'T000000050', 11, 378, 347, CAST(373.89 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'69aa57f2-ea9c-49ac-b1bc-bd8e7cfd6c71')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (117, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', N'T000000050', 1, 381, 347, CAST(33.99 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'69aa57f2-ea9c-49ac-b1bc-bd8e7cfd6c71')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (118, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', N'T000000051', 1, 375, 347, CAST(33.99 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'e3ed5c5c-5843-414a-a19f-8a3a73a53746')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (119, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', N'T000000051', 1, 378, 347, CAST(33.99 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'e3ed5c5c-5843-414a-a19f-8a3a73a53746')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (120, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', N'T000000051', 1, 381, 347, CAST(33.99 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'e3ed5c5c-5843-414a-a19f-8a3a73a53746')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (121, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', N'T000000052', 1, 375, 347, CAST(33.99 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'2128731e-4743-438f-93d6-926ba387cec5')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (122, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', N'T000000052', 1, 378, 347, CAST(33.99 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'2128731e-4743-438f-93d6-926ba387cec5')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (123, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', N'T000000052', 1, 381, 347, CAST(33.99 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'2128731e-4743-438f-93d6-926ba387cec5')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (124, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', N'T000000053', 1, 375, 347, CAST(33.99 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'9e34026a-708e-4ec1-95e2-72725c03d77a')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (125, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', N'T000000053', 1, 378, 347, CAST(33.99 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'9e34026a-708e-4ec1-95e2-72725c03d77a')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (126, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', N'T000000053', 1, 381, 347, CAST(33.99 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'9e34026a-708e-4ec1-95e2-72725c03d77a')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (127, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'T000000054', 2, 130, 345, CAST(84.46 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'524ebc29-3ab6-46bc-96f2-bd73293062e9')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (128, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'T000000055', 1, 130, 345, CAST(42.23 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'480d14c1-b8ac-4f04-8bab-e43a2b4def91')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (129, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'T000000056', 1, 130, 345, CAST(42.23 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'72356c0b-b825-49fe-8811-0092ce5522a9')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (130, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'T000000057', 1, 130, 345, CAST(42.23 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'658a30ba-aaba-4809-8f4e-8cf404f680f5')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (131, N'16724f9c-389d-43a3-b6b2-bc83989b9102', N'T000000058', 1, 499, 350, CAST(35.02 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'5849b73b-6693-4b69-bba4-7ef40c9f62ee')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (132, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'T000000061', 1, 130, 345, CAST(42.23 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'c9893c2d-6b29-459b-8261-3ffc548543ef')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (133, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'T000000062', 1, 130, 345, CAST(42.23 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'ccdcd81d-b6f5-4ebf-9aea-ccf519e697df')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (134, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'T000000063', 1, 130, 345, CAST(42.23 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'24e2c733-7416-4d0b-8f48-3a386cf50314')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (135, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'T000000064', 1, 130, 345, CAST(42.23 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'3912dc3a-cf8b-43bc-8c19-07dee9151a68')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (136, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'T000000065', 1, 130, 345, CAST(42.23 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'a4684da7-0328-447a-a193-6afd27d49bd4')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (137, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'T000000067', 1, 130, 345, CAST(42.23 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'd2ed54fb-93af-44ba-ad1f-30515ae217a6')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (138, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'T000000068', 1, 130, 345, CAST(42.23 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'06eb106b-1dd1-4168-aefc-24a7888ad666')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (139, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'T000000069', 1, 130, 345, CAST(42.23 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'b639e905-d4ae-428e-9e96-0b73c0bc8ac1')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (140, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'T000000070', 1, 130, 345, CAST(42.23 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'dff3a045-e33c-4c5c-8c90-9bd6ef9fae05')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (141, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'T000000071', 1, 130, 345, CAST(42.23 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'6f0a3842-0efe-4a9d-a9c5-644d196cff7f')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (142, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'T000000072', 1, 130, 345, CAST(42.23 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'ce59a4e4-cf0f-4ce5-ba26-2b21f15000f9')
GO
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID]) VALUES (143, N'e583e85c-bf2f-43e4-9d56-5a20c73796be', N'T000000073', 1, 130, 345, CAST(42.23 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'9cf3a2d9-605a-4b9a-bbcf-fdfed89649c2')
GO
SET IDENTITY_INSERT [dbo].[Ticket_Purchased_Detail] OFF
GO
SET IDENTITY_INSERT [dbo].[Ticket_Quantity_Detail] ON 

GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (1, 16, 304, 174, 4, 500, 500, N'Mar 01, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (2, 16, 304, 175, 4, 100, 94, N'Mar 01, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (3, 17, 305, 176, 5, 500, 500, N'Feb 01, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (4, 17, 305, 177, 5, 400, 400, N'Feb 01, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (5, 18, 306, 178, 6, 34, 34, N'Dec 25, 2015', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (6, 19, 307, 179, 7, 34, 34, N'Dec 25, 2015', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (7, 19, 307, 180, 7, 89, 89, N'Dec 25, 2015', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (10, 22, 311, 185, 12, 500, 500, N'Feb 01, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (14, 26, 313, 187, 0, 23, 23, N'Dec 28 2015 12:00AM', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (15, 27, 313, 187, 0, 23, 23, N'Dec 29 2015 12:00AM', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (16, 28, 313, 187, 0, 23, 23, N'Dec 30 2015 12:00AM', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (17, 29, 312, 186, 0, 324, 324, N'Dec 28, 2015', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (18, 30, 312, 186, 0, 324, 324, N'Dec 29, 2015', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (19, 31, 312, 186, 0, 324, 324, N'Dec 30, 2015', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (20, 32, 314, 188, 13, 1500, 1495, N'Feb 01, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (21, 33, 315, 189, 14, 400, 400, N'Feb 01, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (22, 33, 315, 190, 14, 100, 100, N'Feb 01, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (26, 37, 318, 193, 17, 34, 34, N'Dec 25, 2015', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (27, 38, 319, 194, 18, 234, 234, N'Dec 24, 2015', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (31, 42, 320, 195, 19, 34, 34, N'Dec 25, 2015', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (32, 42, 320, 195, 20, 34, 34, N'Dec 25, 2015', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (35, 44, 321, 196, 21, 34, 34, N'Dec 25, 2015', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (36, 44, 321, 197, 21, 23, 23, N'Dec 25, 2015', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (37, 44, 321, 196, 22, 34, 34, N'Dec 25, 2015', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (38, 44, 321, 197, 22, 23, 23, N'Dec 25, 2015', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (41, 47, 310, 184, 23, 23, 23, N'Dec 25, 2015', N'8:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (42, 48, 322, 198, 0, 23, 23, N'Dec 30, 2015', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (66, 72, 324, 200, 27, 1500, 1497, N'Feb 01, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (67, 73, 323, 199, 0, 12, 12, N'Dec 29, 2015', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (68, 73, 323, 201, 0, 23, 23, N'Dec 29, 2015', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (69, 74, 325, 202, 28, 1500, 1500, N'Feb 01, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (70, 75, 326, 203, 29, 1500, 1500, N'Feb 01, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (71, 76, 327, 204, 30, 1500, 1500, N'Feb 01, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (72, 77, 328, 205, 31, 1500, 1500, N'Feb 01, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (83, 83, 316, 191, 0, 34, 34, N'Dec 24, 2015', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (84, 83, 316, 206, 0, 12, 12, N'Dec 24, 2015', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (86, 85, 317, 192, 0, 34, 34, N'Dec 25, 2015', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (87, 86, 329, 207, 32, 1500, 1490, N'Feb 01, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (88, 87, 330, 208, 33, 500, 500, N'Feb 06, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (89, 88, 331, 209, 0, 10, 0, N'Jan 06, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (90, 88, 331, 210, 0, 10, 0, N'Jan 06, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (91, 89, 332, 211, 0, 23, 23, N'Jan 07, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (93, 91, 333, 212, 0, 10, 10, N'Jan 13, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (94, 92, 334, 213, 0, 23, 23, N'Jan 08, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (100, 98, 335, 214, 38, 23, 23, N'Jan 08, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (101, 99, 336, 215, 39, 500, 500, N'Mar 01, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (102, 100, 337, 216, 40, 500, 500, N'May 01, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (103, 101, 338, 217, 0, 23, 23, N'Jan 12, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (116, 111, 340, 220, 41, 150, 150, N'Mar 01, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (117, 112, 341, 221, 42, 150, 150, N'Mar 01, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (124, 116, 339, 218, 0, 23, 23, N'Jan 12, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (125, 116, 339, 219, 0, 34, 34, N'Jan 12, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (126, 117, 342, 222, 43, 150, 150, N'Mar 01, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (127, 118, 343, 223, 0, 23, 23, N'Jan 13, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (129, 120, 344, 224, 0, 23, 23, N'Jan 13, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (130, 121, 345, 225, 44, 450, 410, N'Mar 01, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (131, 121, 345, 226, 44, 5, 0, N'Mar 01, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (133, 123, 346, 227, 0, 23, 23, N'Jan 14, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (374, 164, 347, 228, 45, 42, 42, N'Jan 16, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (375, 164, 347, 229, 45, 56, 32, N'Jan 16, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (376, 164, 347, 230, 45, 34, 34, N'Jan 16, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (377, 164, 347, 228, 46, 42, 42, N'Jan 16, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (378, 164, 347, 229, 46, 56, 37, N'Jan 16, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (379, 164, 347, 230, 46, 34, 34, N'Jan 16, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (380, 165, 347, 228, 45, 42, 42, N'Jan 18, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (381, 165, 347, 229, 45, 56, 47, N'Jan 18, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (382, 165, 347, 230, 45, 34, 34, N'Jan 18, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (383, 165, 347, 228, 46, 42, 42, N'Jan 18, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (384, 165, 347, 229, 46, 56, 56, N'Jan 18, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (385, 165, 347, 230, 46, 34, 34, N'Jan 18, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (386, 166, 347, 228, 45, 42, 42, N'Jan 20, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (387, 166, 347, 229, 45, 56, 56, N'Jan 20, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (388, 166, 347, 230, 45, 34, 34, N'Jan 20, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (389, 166, 347, 228, 46, 42, 42, N'Jan 20, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (390, 166, 347, 229, 46, 56, 56, N'Jan 20, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (391, 166, 347, 230, 46, 34, 34, N'Jan 20, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (392, 167, 347, 228, 45, 42, 42, N'Jan 23, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (393, 167, 347, 229, 45, 56, 56, N'Jan 23, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (394, 167, 347, 230, 45, 34, 34, N'Jan 23, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (395, 167, 347, 228, 46, 42, 42, N'Jan 23, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (396, 167, 347, 229, 46, 56, 56, N'Jan 23, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (397, 167, 347, 230, 46, 34, 34, N'Jan 23, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (398, 168, 347, 228, 45, 42, 42, N'Jan 25, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (399, 168, 347, 229, 45, 56, 56, N'Jan 25, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (400, 168, 347, 230, 45, 34, 34, N'Jan 25, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (401, 168, 347, 228, 46, 42, 42, N'Jan 25, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (402, 168, 347, 229, 46, 56, 56, N'Jan 25, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (403, 168, 347, 230, 46, 34, 34, N'Jan 25, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (404, 169, 347, 228, 45, 42, 42, N'Jan 27, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (405, 169, 347, 229, 45, 56, 56, N'Jan 27, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (406, 169, 347, 230, 45, 34, 34, N'Jan 27, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (407, 169, 347, 228, 46, 42, 42, N'Jan 27, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (408, 169, 347, 229, 46, 56, 56, N'Jan 27, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (409, 169, 347, 230, 46, 34, 34, N'Jan 27, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (410, 170, 347, 228, 45, 42, 42, N'Jan 30, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (411, 170, 347, 229, 45, 56, 56, N'Jan 30, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (412, 170, 347, 230, 45, 34, 34, N'Jan 30, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (413, 170, 347, 228, 46, 42, 42, N'Jan 30, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (414, 170, 347, 229, 46, 56, 56, N'Jan 30, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (415, 170, 347, 230, 46, 34, 34, N'Jan 30, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (416, 171, 347, 228, 45, 42, 42, N'Feb 01, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (417, 171, 347, 229, 45, 56, 56, N'Feb 01, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (418, 171, 347, 230, 45, 34, 34, N'Feb 01, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (419, 171, 347, 228, 46, 42, 42, N'Feb 01, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (420, 171, 347, 229, 46, 56, 56, N'Feb 01, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (421, 171, 347, 230, 46, 34, 34, N'Feb 01, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (422, 172, 347, 228, 45, 42, 42, N'Feb 03, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (423, 172, 347, 229, 45, 56, 56, N'Feb 03, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (424, 172, 347, 230, 45, 34, 34, N'Feb 03, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (425, 172, 347, 228, 46, 42, 42, N'Feb 03, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (426, 172, 347, 229, 46, 56, 54, N'Feb 03, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (427, 172, 347, 230, 46, 34, 34, N'Feb 03, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (428, 173, 347, 228, 45, 42, 42, N'Feb 06, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (429, 173, 347, 229, 45, 56, 56, N'Feb 06, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (430, 173, 347, 230, 45, 34, 34, N'Feb 06, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (431, 173, 347, 228, 46, 42, 42, N'Feb 06, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (432, 173, 347, 229, 46, 56, 56, N'Feb 06, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (433, 173, 347, 230, 46, 34, 34, N'Feb 06, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (434, 174, 347, 228, 45, 42, 42, N'Feb 08, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (435, 174, 347, 229, 45, 56, 56, N'Feb 08, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (436, 174, 347, 230, 45, 34, 34, N'Feb 08, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (437, 174, 347, 228, 46, 42, 42, N'Feb 08, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (438, 174, 347, 229, 46, 56, 56, N'Feb 08, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (439, 174, 347, 230, 46, 34, 34, N'Feb 08, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (440, 175, 347, 228, 45, 42, 42, N'Feb 10, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (441, 175, 347, 229, 45, 56, 56, N'Feb 10, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (442, 175, 347, 230, 45, 34, 34, N'Feb 10, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (443, 175, 347, 228, 46, 42, 42, N'Feb 10, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (444, 175, 347, 229, 46, 56, 56, N'Feb 10, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (445, 175, 347, 230, 46, 34, 34, N'Feb 10, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (446, 176, 347, 228, 45, 42, 42, N'Feb 13, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (447, 176, 347, 229, 45, 56, 56, N'Feb 13, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (448, 176, 347, 230, 45, 34, 34, N'Feb 13, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (449, 176, 347, 228, 46, 42, 42, N'Feb 13, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (450, 176, 347, 229, 46, 56, 56, N'Feb 13, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (451, 176, 347, 230, 46, 34, 34, N'Feb 13, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (452, 177, 347, 228, 45, 42, 42, N'Feb 15, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (453, 177, 347, 229, 45, 56, 56, N'Feb 15, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (454, 177, 347, 230, 45, 34, 34, N'Feb 15, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (455, 177, 347, 228, 46, 42, 42, N'Feb 15, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (456, 177, 347, 229, 46, 56, 56, N'Feb 15, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (457, 177, 347, 230, 46, 34, 34, N'Feb 15, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (458, 178, 347, 228, 45, 42, 42, N'Feb 17, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (459, 178, 347, 229, 45, 56, 56, N'Feb 17, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (460, 178, 347, 230, 45, 34, 34, N'Feb 17, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (461, 178, 347, 228, 46, 42, 42, N'Feb 17, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (462, 178, 347, 229, 46, 56, 56, N'Feb 17, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (463, 178, 347, 230, 46, 34, 34, N'Feb 17, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (464, 179, 347, 228, 45, 42, 42, N'Feb 20, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (465, 179, 347, 229, 45, 56, 56, N'Feb 20, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (466, 179, 347, 230, 45, 34, 34, N'Feb 20, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (467, 179, 347, 228, 46, 42, 42, N'Feb 20, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (468, 179, 347, 229, 46, 56, 56, N'Feb 20, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (469, 179, 347, 230, 46, 34, 34, N'Feb 20, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (470, 180, 347, 228, 45, 42, 42, N'Feb 22, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (471, 180, 347, 229, 45, 56, 56, N'Feb 22, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (472, 180, 347, 230, 45, 34, 34, N'Feb 22, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (473, 180, 347, 228, 46, 42, 42, N'Feb 22, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (474, 180, 347, 229, 46, 56, 56, N'Feb 22, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (475, 180, 347, 230, 46, 34, 34, N'Feb 22, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (476, 181, 347, 228, 45, 42, 42, N'Feb 24, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (477, 181, 347, 229, 45, 56, 56, N'Feb 24, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (478, 181, 347, 230, 45, 34, 34, N'Feb 24, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (479, 181, 347, 228, 46, 42, 42, N'Feb 24, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (480, 181, 347, 229, 46, 56, 56, N'Feb 24, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (481, 181, 347, 230, 46, 34, 34, N'Feb 24, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (482, 182, 347, 228, 45, 42, 42, N'Feb 27, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (483, 182, 347, 229, 45, 56, 56, N'Feb 27, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (484, 182, 347, 230, 45, 34, 34, N'Feb 27, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (485, 182, 347, 228, 46, 42, 42, N'Feb 27, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (486, 182, 347, 229, 46, 56, 56, N'Feb 27, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (487, 182, 347, 230, 46, 34, 34, N'Feb 27, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (488, 183, 347, 228, 45, 42, 42, N'Feb 29, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (489, 183, 347, 229, 45, 56, 56, N'Feb 29, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (490, 183, 347, 230, 45, 34, 34, N'Feb 29, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (491, 183, 347, 228, 46, 42, 42, N'Feb 29, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (492, 183, 347, 229, 46, 56, 54, N'Feb 29, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (493, 183, 347, 230, 46, 34, 34, N'Feb 29, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (495, 185, 348, 231, 48, 234, 234, N'Jan 16, 2016', N'12:00am')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (497, 187, 349, 232, 0, 45, 45, N'Jan 16, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (498, 188, 350, 233, 49, 34, 34, N'Jan 16, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (499, 188, 350, 234, 49, 34, 33, N'Jan 16, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (501, 190, 351, 235, 0, 23, 23, N'Jan 16, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (502, 191, 352, 236, 0, 23, 23, N'Jan 16, 2016', N'7:00pm')
GO
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (503, 192, 353, 237, 0, 23, 23, N'Jan 16, 2016', N'7:00pm')
GO
SET IDENTITY_INSERT [dbo].[Ticket_Quantity_Detail] OFF
GO
SET IDENTITY_INSERT [dbo].[TicketBearer] ON 

GO
INSERT [dbo].[TicketBearer] ([TicketbearerId], [UserId], [OrderId], [Guid], [Name], [Email]) VALUES (1, N'f4ee24cd-0194-45eb-ae30-0c92e151a7dc                                                                                            ', N'0', N'f4ee24cd-0194-45eb-ae30-0c92e151a7dc                                                                                            ', N'ewerf                                                                                                                           ', N'sdfsdf                                                                                                                                                                                                                                                          ')
GO
SET IDENTITY_INSERT [dbo].[TicketBearer] OFF
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
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+04:30) Kabul', N'Afghanistan Standard Time', 1)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-09:00) Alaska', N'Alaskan Standard Time', 2)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+03:00) Kuwait, Riyadh', N'Arab Standard Time', 3)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+04:00) Abu Dhabi, Muscat', N'Arabian Standard Time', 4)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+03:00) Baghdad', N'Arabic Standard Time', 5)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-03:00) Buenos Aires', N'Argentina Standard Time', 6)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-04:00) Atlantic Time (Canada)', N'Atlantic Standard Time', 7)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+09:30) Darwin', N'AUS Central Standard Time', 8)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+10:00) Canberra, Melbourne, Sydney', N'AUS Eastern Standard Time', 9)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+04:00) Baku', N'Azerbaijan Standard Time', 10)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-01:00) Azores', N'Azores Standard Time', 11)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+06:00) Dhaka', N'Bangladesh Standard Time', 12)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-06:00) Saskatchewan', N'Canada Central Standard Time', 13)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-01:00) Cape Verde Is.', N'Cape Verde Standard Time', 14)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+04:00) Yerevan', N'Caucasus Standard Time', 15)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+09:30) Adelaide', N'Cen. Australia Standard Time', 16)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-06:00) Central America', N'Central America Standard Time', 17)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+06:00) Astana', N'Central Asia Standard Time', 18)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-04:00) Cuiaba', N'Central Brazilian Standard Time', 19)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague', N'Central Europe Standard Time', 20)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+01:00) Sarajevo, Skopje, Warsaw, Zagreb', N'Central European Standard Time', 21)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+11:00) Solomon Is., New Caledonia', N'Central Pacific Standard Time', 22)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-06:00) Central Time (US & Canada)', N'Central Standard Time', 23)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-06:00) Guadalajara, Mexico City, Monterrey', N'Central Standard Time (Mexico)', 24)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+08:00) Beijing, Chongqing, Hong Kong, Urumqi', N'China Standard Time', 25)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-12:00) International Date Line West', N'Dateline Standard Time', 26)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+03:00) Nairobi', N'E. Africa Standard Time', 27)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+10:00) Brisbane', N'E. Australia Standard Time', 28)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+02:00) Minsk', N'E. Europe Standard Time', 29)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-03:00) Brasilia', N'E. South America Standard Time', 30)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-05:00) Eastern Time (US & Canada)', N'Eastern Standard Time', 31)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+02:00) Cairo', N'Egypt Standard Time', 32)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+05:00) Ekaterinburg', N'Ekaterinburg Standard Time', 33)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+12:00) Fiji', N'Fiji Standard Time', 34)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+02:00) Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius', N'FLE Standard Time', 35)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+04:00) Tbilisi', N'Georgian Standard Time', 36)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT) Dublin, Edinburgh, Lisbon, London', N'GMT Standard Time', 37)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-03:00) Greenland', N'Greenland Standard Time', 38)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT) Monrovia, Reykjavik', N'Greenwich Standard Time', 39)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+02:00) Athens, Bucharest, Istanbul', N'GTB Standard Time', 40)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-10:00) Hawaii', N'Hawaiian Standard Time', 41)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+05:30) Chennai, Kolkata, Mumbai, New Delhi', N'India Standard Time', 42)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+03:30) Tehran', N'Iran Standard Time', 43)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+02:00) Jerusalem', N'Israel Standard Time', 44)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+02:00) Amman', N'Jordan Standard Time', 45)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+12:00) Petropavlovsk-Kamchatsky - Old', N'Kamchatka Standard Time', 46)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+09:00) Seoul', N'Korea Standard Time', 47)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+11:00) Magadan', N'Magadan Standard Time', 48)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+04:00) Port Louis', N'Mauritius Standard Time', 49)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-02:00) Mid-Atlantic', N'Mid-Atlantic Standard Time', 50)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+02:00) Beirut', N'Middle East Standard Time', 51)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-03:00) Montevideo', N'Montevideo Standard Time', 52)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT) Casablanca', N'Morocco Standard Time', 53)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-07:00) Mountain Time (US & Canada)', N'Mountain Standard Time', 54)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-07:00) Chihuahua, La Paz, Mazatlan', N'Mountain Standard Time (Mexico)', 55)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+06:30) Yangon (Rangoon)', N'Myanmar Standard Time', 56)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+06:00) Novosibirsk', N'N. Central Asia Standard Time', 57)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+01:00) Windhoek', N'Namibia Standard Time', 58)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+05:45) Kathmandu', N'Nepal Standard Time', 59)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+12:00) Auckland, Wellington', N'New Zealand Standard Time', 60)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-03:30) Newfoundland', N'Newfoundland Standard Time', 61)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+08:00) Irkutsk', N'North Asia East Standard Time', 62)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+07:00) Krasnoyarsk', N'North Asia Standard Time', 63)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-04:00) Santiago', N'Pacific SA Standard Time', 64)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-08:00) Pacific Time (US & Canada)', N'Pacific Standard Time', 65)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-08:00) Baja California', N'Pacific Standard Time (Mexico)', 66)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+05:00) Islamabad, Karachi', N'Pakistan Standard Time', 67)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-04:00) Asuncion', N'Paraguay Standard Time', 68)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+01:00) Brussels, Copenhagen, Madrid, Paris', N'Romance Standard Time', 69)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+03:00) Moscow, St. Petersburg, Volgograd', N'Russian Standard Time', 70)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-03:00) Cayenne, Fortaleza', N'SA Eastern Standard Time', 71)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-05:00) Bogota, Lima, Quito', N'SA Pacific Standard Time', 72)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-04:00) Georgetown, La Paz, Manaus, San Juan', N'SA Western Standard Time', 73)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-11:00) Samoa', N'Samoa Standard Time', 74)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+07:00) Bangkok, Hanoi, Jakarta', N'SE Asia Standard Time', 75)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+08:00) Kuala Lumpur, Singapore', N'Singapore Standard Time', 76)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+02:00) Harare, Pretoria', N'South Africa Standard Time', 77)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+05:30) Sri Jayawardenepura', N'Sri Lanka Standard Time', 78)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+02:00) Damascus', N'Syria Standard Time', 79)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+08:00) Taipei', N'Taipei Standard Time', 80)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+10:00) Hobart', N'Tasmania Standard Time', 81)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+09:00) Osaka, Sapporo, Tokyo', N'Tokyo Standard Time', 82)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+13:00) Nuku''alofa', N'Tonga Standard Time', 83)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+08:00) Ulaanbaatar', N'Ulaanbaatar Standard Time', 84)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-05:00) Indiana (East)', N'US Eastern Standard Time', 85)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-07:00) Arizona', N'US Mountain Standard Time', 86)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT) Coordinated Universal Time', N'UTC', 87)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+12:00) Coordinated Universal Time+12', N'UTC+12', 88)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-02:00) Coordinated Universal Time-02', N'UTC-02', 89)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-11:00) Coordinated Universal Time-11', N'UTC-11', 90)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-04:30) Caracas', N'Venezuela Standard Time', 91)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+10:00) Vladivostok', N'Vladivostok Standard Time', 92)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+08:00) Perth', N'W. Australia Standard Time', 93)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+01:00) West Central Africa', N'W. Central Africa Standard Time', 94)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna', N'W. Europe Standard Time', 95)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+05:00) Tashkent', N'West Asia Standard Time', 96)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+10:00) Guam, Port Moresby', N'West Pacific Standard Time', 97)
GO
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+09:00) Yakutsk', N'Yakutsk Standard Time', 98)
GO
SET IDENTITY_INSERT [dbo].[TimeZoneDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[TimeZoneDetail1] ON 

GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (1, N'(GMT-12:00) International Date Line West', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (2, N'(GMT-11:00) Midway Island, Samoa', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (3, N'(GMT-10:00) Hawaii', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (4, N'(GMT-09:00) Alaska', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (5, N'(GMT-08:00) Pacific Time (US & Canada)', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (6, N'(GMT-08:00) Tijuana, Baja California', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (7, N'(GMT-07:00) Arizona', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (8, N'(GMT-07:00) Chihuahua, La Paz, Mazatlan', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (9, N'(GMT-07:00) Mountain Time (US & Canada)', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (10, N'(GMT-06:00) Central America', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (11, N'(GMT-06:00) Central Time (US & Canada)', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (12, N'(GMT-06:00) Guadalajara, Mexico City, Monterrey', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (13, N'(GMT-06:00) Saskatchewan', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (14, N'(GMT-05:00) Bogota, Lima, Quito, Rio Branco', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (15, N'(GMT-05:00) Eastern Time (US & Canada)', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (16, N'(GMT-05:00) Indiana (East)', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (17, N'(GMT-04:00) Atlantic Time (Canada)', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (18, N'(GMT-04:00) Caracas, La Paz', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (19, N'(GMT-04:00) Manaus', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (20, N'(GMT-04:00) Santiago', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (21, N'(GMT-03:30) Newfoundland', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (22, N'(GMT-03:00) Brasilia', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (23, N'(GMT-03:00) Buenos Aires, Georgetown', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (24, N'(GMT-03:00) Greenland', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (25, N'(GMT-03:00) Montevideo', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (26, N'(GMT-02:00) Mid-Atlantic', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (27, N'(GMT-01:00) Cape Verde Is.', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (28, N'(GMT-01:00) Azores', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (29, N'(GMT+00:00) Casablanca, Monrovia, Reykjavik', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (30, N'(GMT+00:00) Greenwich Mean Time : Dublin, Edinburgh, Lisbon, London', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (31, N'(GMT+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (32, N'(GMT+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (33, N'(GMT+01:00) Brussels, Copenhagen, Madrid, Paris', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (34, N'(GMT+01:00) Sarajevo, Skopje, Warsaw, Zagreb', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (35, N'(GMT+01:00) West Central Africa', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (36, N'(GMT+02:00) Amman', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (37, N'(GMT+02:00) Athens, Bucharest, Istanbul', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (38, N'(GMT+02:00) Beirut', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (39, N'(GMT+02:00) Cairo', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (40, N'(GMT+02:00) Harare, Pretoria', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (41, N'(GMT+02:00) Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (42, N'(GMT+02:00) Jerusalem', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (43, N'(GMT+02:00) Minsk', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (44, N'(GMT+02:00) Windhoek', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (45, N'(GMT+03:00) Kuwait, Riyadh, Baghdad', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (46, N'(GMT+03:00) Moscow, St. Petersburg, Volgograd', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (47, N'(GMT+03:00) Nairobi', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (48, N'(GMT+03:00) Tbilisi', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (49, N'(GMT+03:30) Tehran', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (50, N'(GMT+04:00) Abu Dhabi, Muscat', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (51, N'(GMT+04:00) Baku', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (52, N'(GMT+04:00) Yerevan', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (53, N'(GMT+04:30) Kabul', N'Afghanistan Standard Time')
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (54, N'(GMT+05:00) Yekaterinburg', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (55, N'(GMT+05:00) Islamabad, Karachi, Tashkent', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (56, N'(GMT+05:30) Sri Jayawardenapura', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (57, N'(GMT+05:30) Chennai, Kolkata, Mumbai, New Delhi', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (58, N'(GMT+05:45) Kathmandu', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (59, N'(GMT+06:00) Almaty, Novosibirsk', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (60, N'(GMT+06:00) Astana, Dhaka', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (61, N'(GMT+06:30) Yangon (Rangoon)', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (62, N'(GMT+07:00) Bangkok, Hanoi, Jakarta', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (63, N'(GMT+07:00) Krasnoyarsk', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (64, N'(GMT+08:00) Beijing, Chongqing, Hong Kong, Urumqi', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (65, N'(GMT+08:00) Kuala Lumpur, Singapore', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (66, N'(GMT+08:00) Irkutsk, Ulaan Bataar', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (67, N'(GMT+08:00) Perth', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (68, N'(GMT+08:00) Taipei', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (69, N'(GMT+09:00) Osaka, Sapporo, Tokyo', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (70, N'(GMT+09:00) Seoul', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (71, N'(GMT+09:00) Yakutsk', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (72, N'(GMT+09:30) Adelaide', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (73, N'(GMT+09:30) Darwin', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (74, N'(GMT+10:00) Brisbane', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (75, N'(GMT+10:00) Canberra, Melbourne, Sydney', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (76, N'(GMT+10:00) Hobart', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (77, N'(GMT+10:00) Guam, Port Moresby', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (78, N'(GMT+10:00) Vladivostok', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (79, N'(GMT+11:00) Magadan, Solomon Is., New Caledonia', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (80, N'(GMT+12:00) Auckland, Wellington', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (81, N'(GMT+12:00) Fiji, Kamchatka, Marshall Is.', NULL)
GO
INSERT [dbo].[TimeZoneDetail1] ([TimeZone_Id], [TimeZone_Name], [TimeZone]) VALUES (82, N'(GMT+13:00) Nuku''alofa', NULL)
GO
SET IDENTITY_INSERT [dbo].[TimeZoneDetail1] OFF
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
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1441, N'e142bbfb-ef9c-450b-8d8c-38d41966dca3', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1442, N'e142bbfb-ef9c-450b-8d8c-38d41966dca3', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1443, N'af8209bc-66a1-4797-8984-c57e156ad37c', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1444, N'af8209bc-66a1-4797-8984-c57e156ad37c', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1445, N'6b9b5454-b3fb-4a28-9226-32d588f8d9a8', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1446, N'6b9b5454-b3fb-4a28-9226-32d588f8d9a8', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1447, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1448, N'be7cced0-82e6-46ab-97fe-a3758d7b6e2b', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1449, N'93e82fc1-2e9d-46eb-ae31-6dff9c5cece8', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1450, N'93e82fc1-2e9d-46eb-ae31-6dff9c5cece8', 2, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1451, N'5f158983-4660-4237-929c-70baf1279b56', 1, NULL)
GO
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (1452, N'5f158983-4660-4237-929c-70baf1279b56', 2, NULL)
GO
SET IDENTITY_INSERT [dbo].[User_Permission_Detail] OFF
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_Ticket_Locked_Detail_Locktime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Ticket_Locked_Detail] ADD  CONSTRAINT [DF_Ticket_Locked_Detail_Locktime]  DEFAULT (getdate()) FOR [Locktime]
END

GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Address_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Address]'))
ALTER TABLE [dbo].[Address]  WITH NOCHECK ADD  CONSTRAINT [FK_Address_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([EventID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Address_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Address]'))
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_Event]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]'))
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]'))
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]'))
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]'))
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_Orgnizer_Detail_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event_Orgnizer_Detail]'))
ALTER TABLE [dbo].[Event_Orgnizer_Detail]  WITH NOCHECK ADD  CONSTRAINT [FK_Event_Orgnizer_Detail_Event] FOREIGN KEY([Orgnizer_Event_Id])
REFERENCES [dbo].[Event] ([EventID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_Orgnizer_Detail_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event_Orgnizer_Detail]'))
ALTER TABLE [dbo].[Event_Orgnizer_Detail] CHECK CONSTRAINT [FK_Event_Orgnizer_Detail_Event]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_VariableDesc_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event_VariableDesc]'))
ALTER TABLE [dbo].[Event_VariableDesc]  WITH NOCHECK ADD  CONSTRAINT [FK_Event_VariableDesc_Event] FOREIGN KEY([Event_Id])
REFERENCES [dbo].[Event] ([EventID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_VariableDesc_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event_VariableDesc]'))
ALTER TABLE [dbo].[Event_VariableDesc] CHECK CONSTRAINT [FK_Event_VariableDesc_Event]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EventImage_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[EventImage]'))
ALTER TABLE [dbo].[EventImage]  WITH NOCHECK ADD  CONSTRAINT [FK_EventImage_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([EventID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EventImage_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[EventImage]'))
ALTER TABLE [dbo].[EventImage] CHECK CONSTRAINT [FK_EventImage_Event]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__EventSubC__Event__019419E5]') AND parent_object_id = OBJECT_ID(N'[dbo].[EventSubCategory]'))
ALTER TABLE [dbo].[EventSubCategory]  WITH CHECK ADD FOREIGN KEY([EventCategoryID])
REFERENCES [dbo].[EventCategory] ([EventCategoryID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EventVenue_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[EventVenue]'))
ALTER TABLE [dbo].[EventVenue]  WITH NOCHECK ADD  CONSTRAINT [FK_EventVenue_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([EventID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EventVenue_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[EventVenue]'))
ALTER TABLE [dbo].[EventVenue] CHECK CONSTRAINT [FK_EventVenue_Event]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EventVenue_EventVenue]') AND parent_object_id = OBJECT_ID(N'[dbo].[EventVenue]'))
ALTER TABLE [dbo].[EventVenue]  WITH CHECK ADD  CONSTRAINT [FK_EventVenue_EventVenue] FOREIGN KEY([EventVenueID])
REFERENCES [dbo].[EventVenue] ([EventVenueID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EventVenue_EventVenue]') AND parent_object_id = OBJECT_ID(N'[dbo].[EventVenue]'))
ALTER TABLE [dbo].[EventVenue] CHECK CONSTRAINT [FK_EventVenue_EventVenue]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MultipleEvent_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[MultipleEvent]'))
ALTER TABLE [dbo].[MultipleEvent]  WITH NOCHECK ADD  CONSTRAINT [FK_MultipleEvent_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([EventID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MultipleEvent_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[MultipleEvent]'))
ALTER TABLE [dbo].[MultipleEvent] CHECK CONSTRAINT [FK_MultipleEvent_Event]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Profile_Profile]') AND parent_object_id = OBJECT_ID(N'[dbo].[Profile]'))
ALTER TABLE [dbo].[Profile]  WITH CHECK ADD  CONSTRAINT [FK_Profile_Profile] FOREIGN KEY([UserID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Profile_Profile]') AND parent_object_id = OBJECT_ID(N'[dbo].[Profile]'))
ALTER TABLE [dbo].[Profile] CHECK CONSTRAINT [FK_Profile_Profile]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Ticket_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Ticket]'))
ALTER TABLE [dbo].[Ticket]  WITH NOCHECK ADD  CONSTRAINT [FK_Ticket_Event] FOREIGN KEY([E_Id])
REFERENCES [dbo].[Event] ([EventID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Ticket_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Ticket]'))
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Event]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Ticket_Quantity_Info_Ticket]') AND parent_object_id = OBJECT_ID(N'[dbo].[Ticket_Quantity_Detail]'))
ALTER TABLE [dbo].[Ticket_Quantity_Detail]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Quantity_Info_Ticket] FOREIGN KEY([TQD_Ticket_Id])
REFERENCES [dbo].[Ticket] ([T_Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Ticket_Quantity_Info_Ticket]') AND parent_object_id = OBJECT_ID(N'[dbo].[Ticket_Quantity_Detail]'))
ALTER TABLE [dbo].[Ticket_Quantity_Detail] CHECK CONSTRAINT [FK_Ticket_Quantity_Info_Ticket]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__TicketDel__Deliv__08411774]') AND parent_object_id = OBJECT_ID(N'[dbo].[TicketDeliveryMethod]'))
ALTER TABLE [dbo].[TicketDeliveryMethod]  WITH CHECK ADD FOREIGN KEY([DeliveryMethodID])
REFERENCES [dbo].[DeliveryMethod] ([DeliveryMethodID])
GO
