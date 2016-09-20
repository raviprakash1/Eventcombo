IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GeoDistance]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GeoDistance]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GeoDistance]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================
CREATE FUNCTION [dbo].[GeoDistance] 
(
	-- Add the parameters for the function here
  @lng1 float,
	@lat1 float,
  @lng2 float,
  @lat2 float
)
RETURNS float
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result float
  DECLARE @src geography;  
  DECLARE @dst geography;  

  SET @src = ''POINT('' + CAST(@lng1 as varchar) + '' '' + CAST(@lat1 as varchar) + '')'';  
  SET @dst = ''POINT('' + CAST(@lng2 as varchar) + '' '' + CAST(@lat2 as varchar) + '')'';  

	-- Add the T-SQL statements to compute the return value here
	SELECT @Result = @src.STDistance(@dst) / 1609.34; -- convert to miles

	-- Return the result of the function
	RETURN @Result

END

' 
END

GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetNearestEvents]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetNearestEvents]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetNearestEvents]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetNearestEvents] AS' 
END
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


