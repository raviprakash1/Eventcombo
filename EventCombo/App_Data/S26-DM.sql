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


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[GetSelectedTicketListing '8e55c866-ef64-4249-8a3e-b81679052fd6', 96] 

ALTER PROC [dbo].[GetSelectedTicketListing]
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
DECLARE @TIds varchar(MAX) SET @TIds =''
SET @QtyIds =''


DECLARE @T_Name nvarchar(1000) DECLARE @T_Price DECIMAL(18,2) DECLARE @T_Fee decimal(18,2)
--table-striped evnt_tkt_list_tbl
SET @Html = '<div class="col-sm-12 mt10 no_pad">'
--select * From Ticket_Quantity_Detail order by TQD_PE_Id
WHILE (@RCnt>0)
BEGIN
	SELECT @PublishId = P_Id FROM @tblPublish WHERE Id= @RInd
--	Select @PublishId
	SELECT @StartDate = (ISNULL(PE_Scheduled_Date,'') + ', ' + ISNULL(PE_Start_Time,'')), @AddIdss=isnull(PE_Address_Ids,'')
	FROM Publish_Event_Detail WHERE PE_Id = @PublishId
	--SET @Html = CONCAT(@Html,'<thead class="tkt_tbl_head">')
	
	

	SET @Html = CONCAT(@Html,'<div class="col-sm-12 col-xs-12 no_pad divticketdate">',@StartDate, '' ,'</div>')

	if (RTRIM(LTRIM(@AddIdss)) ='')
	BEGIN
		SET @Html = CONCAT(@Html,'<div class="divTable">')
			SET @Html = CONCAT(@Html,'<div class="divTablehead">')
				SET @Html = CONCAT(@Html,'<div class="divTableCell">Ticket Type</div>')
				SET @Html = CONCAT(@Html,'<div class="divTableCell text-right">', 'Price','</div>')
				SET @Html =	CONCAT(@Html,'<div class="divTableCell text-right">Fee</div>')
				SET @Html = CONCAT(@Html,'<div class="divTableCell text-right">Qty</div>')
			SET @Html = CONCAT(@Html,'</div>')
		SET @Html = CONCAT(@Html,'</div>')
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
			
			SET @Html = CONCAT(@Html,'<div class="col-sm-12 col-xs-12 no_pad divticketaddres word-brk-tilt">',@Addess,'</div>')
			
			SET @Html = CONCAT(@Html,'<div class="divTable">')
			SET @Html = CONCAT(@Html,'<div class="divTablehead">')
				SET @Html = CONCAT(@Html,'<div class="divTableCell">Ticket Type</div>')
				SET @Html = CONCAT(@Html,'<div class="divTableCell text-right">', 'Price','</div>')
				SET @Html =	CONCAT(@Html,'<div class="divTableCell text-right">Fee</div>')
				SET @Html = CONCAT(@Html,'<div class="divTableCell text-right">Qty</div>')
			SET @Html = CONCAT(@Html,'</div>')
		SET @Html = CONCAT(@Html,'</div>')
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


		if (@TIds = '')
		BEGIN
			SET @TIds = CONCAT(@TIds,@TicId)
		END
		ELSE
		BEGIN
			SET @TIds = CONCAT(@TIds,',',@TicId)
		END

		SET @Html = CONCAT(@Html,'<div class="col-sm-12 col-xs-12 no_pad">')
		SET @Html = CONCAT(@Html,'<div class="divTable">')
		SET @Html = CONCAT(@Html,'<div class="divTableRow">')


		SET @Html = CONCAT(@Html,'<div class="midtablecell">', @T_Name,'</div>')

		if (@donation>0) SET @Html = CONCAT(@Html,'<div class="midtablecell text-right">$',@donation,'</div>')
		
		Else 
			BEGIN 
				if (@Discount>0)
				BEGIN
					SET @TotalPrice = (@T_Price  - @Discount) + @T_Fee 
					SET @Html = CONCAT(@Html,'<div class="midtablecell text-right">','<strike>',@T_Price,'</strike> $',(@T_Price-@Discount),'</div>') 

				END
				ELSE
				BEGIN
					SET @Html = CONCAT(@Html,'<div class="midtablecell text-right">$',@T_Price,'</div>') 
				END
			END
		SET @Html = CONCAT(@Html,'<span style="display:none;" id="spPrice_', @TQTY_Id,'">',@T_Price-@Discount,'</span>') 
		SET @Html = CONCAT(@Html,'<span style="display:none;" id="spQty_', @TQTY_Id,'">',@selectedQty,'</span>') 
		SET @Html = CONCAT(@Html,'<span style="display:none;" id="spFee_', @TQTY_Id,'">',@T_Fee,'</span>') 
		SET @Html = CONCAT(@Html,'<span style="display:none;" id="spDonation_', @TQTY_Id,'">',@donation,'</span>') 
		SET @Html = CONCAT(@Html,'<span style="display:none;" id="spTQDId_', @TQTY_Id,'">',@TQTY_Id,'</span>') 

		SET @Html = CONCAT(@Html,'<div class="midtablecell text-right">$',@T_Fee,'</div>')
		SET @Html = CONCAT(@Html,'<div class="midtablecell text-right">',@selectedQty,'</div>')
		SET @RunningTotal = @RunningTotal  + (@selectedQty * @TotalPrice) + @donation
		SET @Html = CONCAT(@Html,'</div></div></div>')

		SET @RowCount = @RowCount  -1
		SET @RowIndex = @RowIndex  +1
	END


	SET @RCnt = @RCnt -1
	SET @RInd = @RInd +1
END



DECLARE @VariableChanges DECIMAL(18,2) SET @VariableChanges =0 DECLARE @VariableIds varchar(500) SET @VariableIds =''
DECLARE @VariableName nvarchar(512) SET @VariableName =''
DECLARE @VariableDesc nvarchar(512) SET @VariableDesc =''
DECLARE @VariableType nvarchar(512) SET @VariableType =''

DECLARE @VariablePrice DECIMAL(18,2) SET @VariablePrice=0
DECLARE @GrandTotal DECIMAL(18,2) SET @GrandTotal =0
DECLARE @VarHTML nvarchar(MAX) SET @VarHTML = ''
DECLARE @TblVar TABLE (Id INT IDENTITY,vDec NVARCHAR(512),Price numeric(18,2),VariableId bigint) 
DECLARE @RowCnt int  SET @RowCnt =0
Select  @VariableDesc =ISNULL(Ticket_variabledesc,'') ,@VariableType = LTRIM(RTRIM(ISNULL(Ticket_variabletype,''))) From Event where EventID = @EventId

INSERT INTO @TblVar select VariableDesc,Price,Variable_Id From Event_VariableDesc WHERE Event_Id = @EventId  
SET @RowCount = @@ROWCOUNT
SET @RowIndex =1
SET @RowCnt = @RowCount
if (@RowCount>0)
BEGIN
	
	SET @VarHTML = CONCAT(@VarHTML,'<div class="divTable">')
	SET @VarHTML = CONCAT(@VarHTML,'<div class="divticketdate">')
		SET @VarHTML = CONCAT(@VarHTML,'<p class="ticketsubtotal text-left">', @VariableDesc ,'</p>')

END
DECLARE @DefaultVarIdOptional BIGINT SET @DefaultVarIdOptional=0
DECLARE @VarId bigint
WHILE (@RowCount>0)
BEGIN
	SELECT @VariableName = ISNULL(vDec,''),@VariablePrice = ISNULL(Price ,0),@VarId=VariableId FROM @TblVar WHERE Id= @RowIndex
	if (@VariableIds = '')
		Set @VariableIds = @VarId
	else
		Set @VariableIds = CONCAT(@VariableIds,',',@VarId)

	if (@VariableType = 'R')
	BEGIN
		if (@RowIndex=1)
		BEGIN
			SET @VarHTML = CONCAT(@VarHTML,'<p class="ticketsubtotal text-right">','<input type="radio" onclick="calculateTickTotal(',@VariablePrice,',',@VarId,')" id="rd_',@VarId,'" name="VChanges" checked="checked" /> ', @VariableName , ' $ ',@VariablePrice ,'</p>') 
			--SET @VarHTML = CONCAT(@VarHTML,'<p class="ticketsubtotal text-right">$',@VariablePrice,'</p>') 
			SET @VariableChanges = @VariablePrice
			SET @DefaultVarIdOptional = @VarId
		END
		ELSE
		BEGIN
			SET @VarHTML = CONCAT(@VarHTML,'<p class="ticketsubtotal text-right">','<input type="radio" onclick="calculateTickTotal(',@VariablePrice, ',',@VarId,')" id="rd_',@VarId,'" name="VChanges" /> ',@VariableName , ' $ ', @VariablePrice,'</p>') 
			--SET @VarHTML = CONCAT(@VarHTML,'<p class="ticketsubtotal text-right">$',@VariablePrice,'</p>') 
		END
	END
	ELSE
	BEGIN
		BEGIN
			SET @VarHTML = CONCAT(@VarHTML,'<p class="ticketsubtotal text-right">','<input type="checkbox" onclick="calculateTickTotalOptional(',@RowCnt,')" id="chk_',@VarId,'" name="VChanges"/> ', @VariableName , ' $ ', '<span id="sp_',@VarId,'">',@VariablePrice,'</span>' , '</p>') 
			--SET @VarHTML = CONCAT(@VarHTML,'<p class="ticketsubtotal text-right">$','<span id="sp_',@VarId,'">',@VariablePrice,'</span>','</p>') 
		END
	END
	

	--SELECT @VariableChanges = ISNULL(SUM(ISNULL(Price,0)),0) FROM Event_VariableDesc WHERE Event_Id = @EventId 

	SET @RowCount = @RowCount -1
	SET @RowIndex = @RowIndex +1
END


--SELECT @VariableChanges = ISNULL(SUM(ISNULL(Price,0)),0) FROM Event_VariableDesc WHERE Event_Id = @EventId 
SET @GrandTotal = @RunningTotal + @VariableChanges



if (RTRIM(LTRIM(@VarHTML)) != '')
BEGIN
	SET @Html = CONCAT(@Html,'<p class="ticketsubtotal text-right">','<b>','Order Total : $ ' , CONVERT(NUMERIC(18,2),@RunningTotal) ,'</b>','</p>') 
		SET @Html = CONCAT(@Html,@VarHTML)
	SET @Html = CONCAT(@Html,'</div></div>')
END
ELSE
BEGIN
	SET @VarHTML = CONCAT(@VarHTML,'<div class="divTable">')
	SET @VarHTML = CONCAT(@VarHTML,'<div class="divticketdate">')
		SET @Html = CONCAT(@Html,'<p class="ticketsubtotal text-right">','<b>','Order Total : $ ' , CONVERT(NUMERIC(18,2),@RunningTotal) ,'</b>','</p>') 
END
 

SET @Html = CONCAT(@Html,'<p id="dvHash" class="ticketsubtotal text-right">','<b>', 'Sub Total : $ ' ,'<span id="spSubTotal">' , CONVERT(NUMERIC(18,2),(@RunningTotal+@VariableChanges)) ,'</span></b>','</p>') 
SET @Html = CONCAT(@Html,'<p class="ticketsubtotal text-right">', 'Promo Code ','<input type="text" class="form-control evnt_inp_cont table_promo_input" maxlength="20" placeholder="" id="txtPromoCode','" value="" /> <input type="button" id="btApply" class="btn def_btn acnt_btn promo_code_btn" value="Apply" onclick="CalculatePromoCode();" name="Apply" /> <input type="button" style="display:none;" id="btCancel" class="btn def_btn acnt_btn promo_code_btn" value="Cancel" onclick="CancelPromoCode();" name="Cancel" />','</p>') 
SET @Html = CONCAT(@Html,'<p class="ticketsubtotalerror text-right">', '','<span class="promoerror" id="spValidation"></span>' ,'</p>') 
SET @Html = CONCAT(@Html,'<p class="ticketsubtotal text-right">','<b>', 'Grand Total : $ ', '<span id="spGrdTotal">',CONVERT(NUMERIC(18,2),@GrandTotal) ,'</span></b>','</p>')

SET @Html = CONCAT(@Html,'<input id="hdIds" type="hidden" value=', @QtyIds ,' />')
SET @Html = CONCAT(@Html,'<input id="hdOrderTotal" type="hidden" value=', @RunningTotal ,' />')
SET @Html = CONCAT(@Html,'<input id="hdGrandTotal" type="hidden" value=', @GrandTotal ,' />')
SET @Html = CONCAT(@Html,'<input id="hdGrandTotalWithoutPromo" type="hidden" value=', @GrandTotal ,' />')
SET @Html = CONCAT(@Html,'<input id="hdPromoId" type="hidden" value=', '0' ,' />')
SET @Html = CONCAT(@Html,'<input id="hdVarChanges" type="hidden" value=', @VariableChanges ,' />')
SET @Html = CONCAT(@Html,'<input id="hdVarId" type="hidden" value="', @VariableIds ,'" />')
SET @Html = CONCAT(@Html,'<input id="hidQty" type="hidden" value=', @PaidTicketQty ,' />')
SET @Html = CONCAT(@Html,'<input id="hidTicketIds" type="hidden" value=', @TIds ,' />')
SET @Html = CONCAT(@Html,'<input id="hdVChg" type="hidden" value=', @DefaultVarIdOptional ,' />')

--<img class"PromoLoaderHide" id="imgPromoLoader"  src="~/Images/PromoCodeLoader.gif" />
SET @Html = CONCAT(@Html,'</div></div>')
Select @Html as Ticket


END

