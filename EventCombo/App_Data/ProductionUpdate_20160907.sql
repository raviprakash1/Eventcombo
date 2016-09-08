IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[V_EventsexpiredList]'))
DROP VIEW [dbo].[V_EventsexpiredList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[V_EventsexpiredList]'))
EXEC dbo.sp_executesql @statement = N'
CREATE VIEW [dbo].[V_EventsexpiredList]
AS
  SELECT  ROW_NUMBER() Over (Order by EventVenue.E_Startdate desc) As Sno,
          Event.EventTitle, 
          Event.UserID,
          Event.EventID,
          EventCategory.EventCategoryID,
          EventCategory.EventCategory,
          EventType.EventTypeID,
          EventType.EventType,
          (
            SELECT EventSubCategory.EventSubCategoryID 
            FROM EventSubCategory 
            WHERE EventSubCategory.EventSubCategoryID = Event.EventSubCategoryID 
                  AND EventSubCategory.EventCategoryID = EventCategory.EventCategoryID
          ) AS EventSubCategoryID,
          (
            SELECT EventSubCategory.EventSubCategory 
            FROM EventSubCategory 
            WHERE EventSubCategory.EventSubCategoryID = Event.EventSubCategoryID 
                  AND EventSubCategory.EventCategoryID=EventCategory.EventCategoryID
          ) AS EventSubCategory,
          EventVenue.EventStartDate as StartingFrom,
          EventVenue.EventStartTime,
          EventVenue.EventEndDate,
          EventVenue.EventEndTime,
          EventVenue.E_Enddate,
          EventVenue.E_Startdate,
          ISNULL(Organizer_Master.Orgnizer_Name,Profile.FirstName) AS Orgnizer_Name,
          ISNULL(Organizer_Master.Organizer_Email,Profile.Email) AS Email,
          ISNULL(Event.Feature,0) AS Feature,
          Address.ConsolidateAddress AS EventAddress,
          Address.VenueName,
          (
            SELECT CONVERT(varchar, ISNULL(SUM(TQD_Remaining_Quantity), 0)) 
            FROM Ticket_Quantity_Detail
            WHERE TQD_Event_Id=Event.EventID 
          ) AS TicketDetail,
          ISNULL(
            (
              SELECT CONVERT(varchar, ISNULL(SUM(TPD_Purchased_Qty), 0)) 
              FROM Ticket_Purchased_Detail  
              WHERE TPD_Event_Id=Event.EventID)
            ,0) AS Purchasedqty,
          CONVERT(varchar(24),  CAST(EventVenue.EventStartDate as datetime) + CAST(EventVenue.EventStartTime as datetime),	100) 
            + ''-'' + 
            CONVERT(varchar(24), CAST(EventVenue.EventEndDate as datetime) + CAST(EventVenue.EventEndTime as datetime),	100) 
            AS EventTiming
  FROM dbo.Event 
  INNER JOIN dbo.EventCategory ON dbo.Event.EventCategoryID = dbo.EventCategory.EventCategoryID 
  INNER JOIN dbo.EventType ON dbo.Event.EventTypeID = dbo.EventType.EventTypeID 
	LEFT JOIN EventVenue ON Event.EventID = EventVenue.EventID 
  LEFT JOIN Profile ON Event.UserID = Profile.UserID
	LEFT JOIN Event_Orgnizer_Detail ON Event.EventID = Event_Orgnizer_Detail.Orgnizer_Event_Id 
  INNER JOIN Organizer_Master ON Event_Orgnizer_Detail.OrganizerMaster_Id = Organizer_Master.Orgnizer_Id 
  LEFT JOIN Address on Event.EventID=Address.EventId
  WHERE EventVenue.E_Enddate < GETUTCDATE()

' 
GO


