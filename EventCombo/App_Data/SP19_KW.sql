

/****** Object:  View [dbo].[V_EventsList]    Script Date: 6/3/2016 2:47:42 PM ******/
DROP VIEW [dbo].[V_EventsList]
GO

/****** Object:  View [dbo].[V_EventsexpiredList]    Script Date: 6/3/2016 2:47:42 PM ******/
DROP VIEW [dbo].[V_EventsexpiredList]
GO

/****** Object:  View [dbo].[V_EventsexpiredList]    Script Date: 6/3/2016 2:47:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






create VIEW [dbo].[V_EventsexpiredList]
AS
SELECT    ROW_NUMBER() Over (Order by EventVenue.E_Startdate desc) As Sno,  Event.EventTitle, Event.UserID,  Event.EventID,EventCategory.EventCategoryID, EventCategory.EventCategory,EventType.EventTypeID, EventType.EventType,(select EventSubCategory.EventSubCategoryID from EventSubCategory where EventSubCategory.EventSubCategoryID=Event.EventSubCategoryID and EventSubCategory.EventCategoryID=EventCategory.EventCategoryID) as EventSubCategoryID,(select EventSubCategory.EventSubCategory from EventSubCategory where EventSubCategory.EventSubCategoryID=Event.EventSubCategoryID and EventSubCategory.EventCategoryID=EventCategory.EventCategoryID) as EventSubCategory,
EventVenue.EventStartDate as StartingFrom,EventVenue.EventStartTime,EventVenue.EventEndDate,EventVenue.EventEndTime,
EventVenue.E_Enddate,EventVenue.E_Startdate,ISNULL(Organizer_Master.Orgnizer_Name,Profile.FirstName) AS Orgnizer_Name,
ISNULL(Organizer_Master.Organizer_Email,Profile.Email) as Email,isnull(Event.Feature,0) as Feature,
Address.ConsolidateAddress as EventAddress,Address.VenueName,
(SELECT   convert(varchar,isnull( sum(TQD_Remaining_Quantity),0)) 
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Event.EventID ) as TicketDetail,

isnull((Select convert(varchar,isnull(sum(TPD_Purchased_Qty),0)) from Ticket_Purchased_Detail  where TPD_Event_Id=Event.EventID),0) as Purchasedqty,
convert(varchar(24),cast(EventVenue.EventStartDate as datetime) + cast(EventVenue.EventStartTime as datetime),	100) + '-' +
		convert(varchar(24),cast(EventVenue.EventEndDate as datetime) + cast(EventVenue.EventEndTime as datetime),	100) as EventTiming



FROM            dbo.Event INNER JOIN
                         dbo.EventCategory ON dbo.Event.EventCategoryID = dbo.EventCategory.EventCategoryID INNER JOIN
                         dbo.EventType ON dbo.Event.EventTypeID = dbo.EventType.EventTypeID 
						 LEFT JOIN EventVenue ON Event.EventID=EventVenue.EventID left join Profile on Event.UserID=Profile.UserID
						 left join Event_Orgnizer_Detail on Event.EventID=Event_Orgnizer_Detail.Orgnizer_Event_Id inner join Organizer_Master on 
						 Event_Orgnizer_Detail.OrganizerMaster_Id=Organizer_Master.Orgnizer_Id left join Address on Event.EventID=Address.EventId

						 where EventVenue.E_Enddate<GETUTCDATE() and (SELECT   convert(varchar,isnull( sum(TQD_Remaining_Quantity),0)) 
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Event.EventID)<=0
						











GO

/****** Object:  View [dbo].[V_EventsList]    Script Date: 6/3/2016 2:47:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[V_EventsList]
AS
SELECT    ROW_NUMBER() Over (Order by EventVenue.E_Startdate) As Sno,  Event.EventTitle, Event.UserID,  Event.EventID,EventCategory.EventCategoryID, EventCategory.EventCategory,EventType.EventTypeID, EventType.EventType,(select EventSubCategory.EventSubCategoryID from EventSubCategory where EventSubCategory.EventSubCategoryID=Event.EventSubCategoryID and EventSubCategory.EventCategoryID=EventCategory.EventCategoryID) as EventSubCategoryID,(select EventSubCategory.EventSubCategory from EventSubCategory where EventSubCategory.EventSubCategoryID=Event.EventSubCategoryID and EventSubCategory.EventCategoryID=EventCategory.EventCategoryID) as EventSubCategory,
EventVenue.EventStartDate as StartingFrom,EventVenue.EventStartTime,EventVenue.EventEndDate,EventVenue.EventEndTime,
EventVenue.E_Enddate,EventVenue.E_Startdate,ISNULL(Organizer_Master.Orgnizer_Name,Profile.FirstName) AS Orgnizer_Name,
ISNULL(Organizer_Master.Organizer_Email,Profile.Email) as Email,isnull(Event.Feature,0) as Feature,
Address.ConsolidateAddress as EventAddress,Address.VenueName,
(SELECT   convert(varchar,isnull( sum(TQD_Remaining_Quantity),0)) 
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Event.EventID ) as TicketDetail,

isnull((Select convert(varchar,isnull(sum(TPD_Purchased_Qty),0)) from Ticket_Purchased_Detail  where TPD_Event_Id=Event.EventID),0) as Purchasedqty,
convert(varchar(24),cast(EventVenue.EventStartDate as datetime) + cast(EventVenue.EventStartTime as datetime),	100) + '-' +
		convert(varchar(24),cast(EventVenue.EventEndDate as datetime) + cast(EventVenue.EventEndTime as datetime),	100) as EventTiming



FROM            dbo.Event INNER JOIN
                         dbo.EventCategory ON dbo.Event.EventCategoryID = dbo.EventCategory.EventCategoryID INNER JOIN
                         dbo.EventType ON dbo.Event.EventTypeID = dbo.EventType.EventTypeID 
						 LEFT JOIN EventVenue ON Event.EventID=EventVenue.EventID left join Profile on Event.UserID=Profile.UserID
						 left join Event_Orgnizer_Detail on Event.EventID=Event_Orgnizer_Detail.Orgnizer_Event_Id inner join Organizer_Master on 
						 Event_Orgnizer_Detail.OrganizerMaster_Id=Organizer_Master.Orgnizer_Id left join Address on Event.EventID=Address.EventId
						








GO





UPDATE Order_Detail_T set O_Order_Id='T' + RIGHT('00000000' + convert(varchar,O_Id), 9) WHERE O_Order_Id=''