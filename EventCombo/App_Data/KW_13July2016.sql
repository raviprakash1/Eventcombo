/****** Object:  View [dbo].[V_EventsListUpcoming]    Script Date: 7/13/2016 7:35:54 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[V_EventsListUpcoming]
AS
  SELECT  ROW_NUMBER() OVER (ORDER BY EventVenue.E_Startdate) AS Sno,
    [Event].EventTitle, [Event].UserID, [Event].EventID, EventCategory.EventCategoryID, 
    EventCategory.EventCategory, EventType.EventTypeID, EventType.EventType,
    (
      SELECT EventSubCategory.EventSubCategoryID 
      FROM EventSubCategory 
      WHERE EventSubCategory.EventSubCategoryID=[Event].EventSubCategoryID 
            AND EventSubCategory.EventCategoryID=EventCategory.EventCategoryID
    ) AS EventSubCategoryID,
    (
      SELECT EventSubCategory.EventSubCategory 
      FROM EventSubCategory 
      WHERE EventSubCategory.EventSubCategoryID=[Event].EventSubCategoryID 
            AND EventSubCategory.EventCategoryID=EventCategory.EventCategoryID
    ) AS EventSubCategory,
    EventVenue.EventStartDate AS StartingFrom, EventVenue.EventStartTime, EventVenue.EventEndDate, EventVenue.EventEndTime,
    EventVenue.E_Enddate, EventVenue.E_Startdate, 
    ISNULL(Organizer_Master.Orgnizer_Name,[Profile].FirstName) AS Orgnizer_Name,
    ISNULL(Organizer_Master.Organizer_Email,[Profile].Email) AS Email,
    ISNULL([Event].Feature,0) AS Feature,
    [Address].ConsolidateAddress AS EventAddress,[Address].VenueName,
    (
      SELECT CONVERT(VARCHAR, ISNULL(SUM(TQD_Remaining_Quantity), 0)) 
      FROM Ticket_Quantity_Detail 
      WHERE TQD_Event_Id=[Event].EventID
    ) AS TicketDetail,
    ISNULL((
      SELECT CONVERT(VARCHAR, ISNULL(SUM(TPD_Purchased_Qty), 0)) 
      FROM Ticket_Purchased_Detail 
      WHERE TPD_Event_Id=[Event].EventID
    ), 0) AS Purchasedqty,
    CONVERT(VARCHAR(24), CAST(EventVenue.EventStartDate AS DATETIME) + CAST(EventVenue.EventStartTime AS DATETIME),	100) + '-' +
      CONVERT(VARCHAR(24), CAST(EventVenue.EventEndDate AS DATETIME) + CAST(EventVenue.EventEndTime AS DATETIME), 100) AS EventTiming
  FROM dbo.[Event] 
  INNER JOIN dbo.EventCategory ON dbo.[Event].EventCategoryID = dbo.EventCategory.EventCategoryID 
  INNER JOIN dbo.EventType ON dbo.[Event].EventTypeID = dbo.EventType.EventTypeID 
  LEFT JOIN EventVenue ON [Event].EventID=EventVenue.EventID 
  LEFT JOIN [Profile] ON [Event].UserID=[Profile].UserID
  LEFT JOIN Event_Orgnizer_Detail ON [Event].EventID=Event_Orgnizer_Detail.Orgnizer_Event_Id 
  INNER JOIN Organizer_Master ON Event_Orgnizer_Detail.OrganizerMaster_Id=Organizer_Master.Orgnizer_Id 
  LEFT JOIN [Address] ON [Event].EventID=[Address].EventId
  WHERE EventVenue.E_Enddate>=GETUTCDATE() 


  GO