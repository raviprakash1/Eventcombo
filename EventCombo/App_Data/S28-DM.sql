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
ALTER TABLE dbo.Event_Email_Invitation ADD
	Code varchar(50) NULL,
	LastUsed datetime NULL
GO
ALTER TABLE dbo.Event_Email_Invitation SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================
ALTER PROCEDURE [dbo].[GetNearestEvents] 
	-- Add the parameters for the stored procedure here
	@lng float, 
	@lat float,
  @distance float,
  @filterUpcoming bit = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

  SELECT e.*, a.AddressID, a.ConsolidateAddress, a.VenueName, a.Longitude, a.Latitude, 
    dbo.GeoDistance(ISNULL(a.Longitude, @lng), ISNULL(a.Latitude, @lat), @lng, @lat) distance,
    sd.E_Startdate,
    md.M_Startfrom,
    md.M_StartTo,
    et.EventType,
    ec.EventCategory,
    esc.EventSubCategory
  FROM Event e
  INNER JOIN EventType et ON et.EventTypeID = e.EventTypeID
  INNER JOIN EventCategory ec ON ec.EventCategoryID = e.EventCategoryID
  LEFT JOIN EventSubCategory esc ON esc.EventSubCategoryID = e.EventSubCategoryID
  LEFT JOIN [Address] a ON a.EventId = e.EventID
  LEFT JOIN EventVenue sd ON sd.EventID = e.EventID
  LEFT JOIN MultipleEvent md ON md.EventID = e.EventID
  WHERE UPPER(e.EventStatus) = 'LIVE'
    AND UPPER(ISNULL(e.EventPrivacy, '')) <> 'PRIVATE'
    AND dbo.GeoDistance(ISNULL(a.Longitude, @lng), ISNULL(a.Latitude, @lat), @lng, @lat) <= @distance 
    AND (@filterUpcoming = 0 
      OR e.EventID in 
      (
        SELECT ed.PE_Event_Id EventId
        FROM Publish_Event_Detail ed
        WHERE DATEADD(dd, 1, CAST(ed.PE_Scheduled_Date as datetime)) >= GETDATE()
      ))
  ORDER BY ISNULL(e.Feature, 0) DESC,
    CASE WHEN UPPER(ISNULL(e.AddressStatus, '')) = 'ONLINE' THEN 1 ELSE 0 END, 
    dbo.GeoDistance(ISNULL(a.Longitude, @lng), ISNULL(a.Latitude, @lat), @lng, @lat)

END
GO