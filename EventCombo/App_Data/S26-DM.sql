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
ALTER TABLE dbo.City ADD
	IsOnHomepage bit NOT NULL CONSTRAINT DF_City_IsOnHomepage DEFAULT 0,
	StateId nvarchar(50) NULL,
	ShortName nvarchar(50) NULL,
  Position smallint DEFAULT 0 
GO
ALTER TABLE dbo.City SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

UPDATE City 
SET IsOnHomepage = 1, StateId = 'GA', ShortName = CityName, Position = 5
WHERE CityId = 1
UPDATE City 
SET IsOnHomepage = 1, StateId = 'TX', ShortName = CityName, Position = 9
WHERE CityId = 2
UPDATE City 
SET IsOnHomepage = 1, StateId = 'MA', ShortName = CityName, Position = 11
WHERE CityId = 3
UPDATE City 
SET IsOnHomepage = 0, StateId = 'NC', ShortName = CityName, Position = 0
WHERE CityId = 4
UPDATE City 
SET IsOnHomepage = 1, StateId = 'IL', ShortName = CityName, Position = 4
WHERE CityId = 5
UPDATE City 
SET IsOnHomepage = 1, StateId = 'TX', ShortName = CityName, Position = 10
WHERE CityId = 6
UPDATE City 
SET IsOnHomepage = 1, StateId = 'CA', ShortName = 'L.A.', Position = 2
WHERE CityId = 7
UPDATE City 
SET IsOnHomepage = 0, StateId = 'TN', ShortName = CityName, Position = 0
WHERE CityId = 8
UPDATE City 
SET IsOnHomepage = 0, StateId = 'LA', ShortName = CityName, Position = 0
WHERE CityId = 9
UPDATE City 
SET IsOnHomepage = 1, StateId = 'NY', ShortName = 'N.Y.', Position = 1
WHERE CityId = 10
UPDATE City 
SET IsOnHomepage = 0, StateId = 'NJ', ShortName = CityName, Position = 0
WHERE CityId = 11
UPDATE City 
SET IsOnHomepage = 1, StateId = 'PA', ShortName = 'PHILLY', Position = 6
WHERE CityId = 12
UPDATE City 
SET IsOnHomepage = 1, StateId = 'CA', ShortName = CityName, Position = 8
WHERE CityId = 13
UPDATE City 
SET IsOnHomepage = 1, StateId = 'WA', ShortName = CityName, Position = 7
WHERE CityId = 14
UPDATE City 
SET IsOnHomepage = 1, StateId = 'DC', ShortName = 'D.C.', Position = 3
WHERE CityId = 15

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
ALTER TABLE dbo.EventType ADD
	IsOnHomepage bit NOT NULL CONSTRAINT DF_EventType_IsOnHomepage DEFAULT 0,
	Position smallint NOT NULL CONSTRAINT DF_EventType_Position DEFAULT 0
GO
ALTER TABLE dbo.EventType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

UPDATE EventType
SET IsOnHomepage = 1, Position = 1
WHERE EventTypeID = 1
UPDATE EventType
SET IsOnHomepage = 1, Position = 2
WHERE EventTypeID = 2
UPDATE EventType
SET IsOnHomepage = 1, Position = 3
WHERE EventTypeID = 5
UPDATE EventType
SET IsOnHomepage = 1, Position = 4
WHERE EventTypeID = 20
UPDATE EventType
SET IsOnHomepage = 1, Position = 5
WHERE EventTypeID = 7
UPDATE EventType
SET IsOnHomepage = 1, Position = 6
WHERE EventTypeID = 8
UPDATE EventType
SET IsOnHomepage = 1, Position = 7
WHERE EventTypeID = 18
UPDATE EventType
SET IsOnHomepage = 1, Position = 8
WHERE EventTypeID = 6
UPDATE EventType
SET IsOnHomepage = 1, Position = 9
WHERE EventTypeID = 9
UPDATE EventType
SET IsOnHomepage = 1, Position = 10
WHERE EventTypeID = 24
UPDATE EventType
SET IsOnHomepage = 1, Position = 11
WHERE EventTypeID = 11

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HomepageWord]') AND type in (N'U'))
DROP TABLE [dbo].[HomepageWord]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HomepageWord]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[HomepageWord](
	[HomepageWordId] [int] IDENTITY(1,1) NOT NULL,
	[Word] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_HomepageWord] PRIMARY KEY CLUSTERED 
(
	[HomepageWordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

TRUNCATE TABLE[HomepageWord]
INSERT INTO [HomepageWord] (Word) VALUES ('event')
INSERT INTO [HomepageWord] (Word) VALUES ('business')
INSERT INTO [HomepageWord] (Word) VALUES ('festival')
INSERT INTO [HomepageWord] (Word) VALUES ('race')
INSERT INTO [HomepageWord] (Word) VALUES ('concert')
INSERT INTO [HomepageWord] (Word) VALUES ('class')
INSERT INTO [HomepageWord] (Word) VALUES ('savings')
INSERT INTO [HomepageWord] (Word) VALUES ('show')
INSERT INTO [HomepageWord] (Word) VALUES ('workshop')
INSERT INTO [HomepageWord] (Word) VALUES ('party')
INSERT INTO [HomepageWord] (Word) VALUES ('play')
INSERT INTO [HomepageWord] (Word) VALUES ('performance')
INSERT INTO [HomepageWord] (Word) VALUES ('music')
INSERT INTO [HomepageWord] (Word) VALUES ('current platform')
INSERT INTO [HomepageWord] (Word) VALUES ('promotions')
INSERT INTO [HomepageWord] (Word) VALUES ('marketing')
INSERT INTO [HomepageWord] (Word) VALUES ('customers')
INSERT INTO [HomepageWord] (Word) VALUES ('clients')
INSERT INTO [HomepageWord] (Word) VALUES ('attendees')
INSERT INTO [HomepageWord] (Word) VALUES ('sport')
INSERT INTO [HomepageWord] (Word) VALUES ('fundraiser')
INSERT INTO [HomepageWord] (Word) VALUES ('night')
INSERT INTO [HomepageWord] (Word) VALUES ('day')
INSERT INTO [HomepageWord] (Word) VALUES ('cause')
INSERT INTO [HomepageWord] (Word) VALUES ('planning')
INSERT INTO [HomepageWord] (Word) VALUES ('venue')
INSERT INTO [HomepageWord] (Word) VALUES ('meeting')
INSERT INTO [HomepageWord] (Word) VALUES ('tour')
INSERT INTO [HomepageWord] (Word) VALUES ('gig')
INSERT INTO [HomepageWord] (Word) VALUES ('campaign')
INSERT INTO [HomepageWord] (Word) VALUES ('meeting')
INSERT INTO [HomepageWord] (Word) VALUES ('appearance')
INSERT INTO [HomepageWord] (Word) VALUES ('job')
