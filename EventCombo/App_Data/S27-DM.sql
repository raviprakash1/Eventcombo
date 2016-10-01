SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BusinessPageECImage](
	[BusinessPageECImageId] [bigint] IDENTITY(1,1) NOT NULL,
	[BusinessPageId] [bigint] NOT NULL,
	[ECImageId] [bigint] NOT NULL,
 CONSTRAINT [PK_BusinessPageECImage] PRIMARY KEY CLUSTERED 
(
	[BusinessPageECImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[BusinessPageECImage]  WITH CHECK ADD  CONSTRAINT [FK_BusinessPageECImage_BusinessPage] FOREIGN KEY([BusinessPageId])
REFERENCES [dbo].[BusinessPage] ([BusinessPageID])
GO

ALTER TABLE [dbo].[BusinessPageECImage] CHECK CONSTRAINT [FK_BusinessPageECImage_BusinessPage]
GO

ALTER TABLE [dbo].[BusinessPageECImage]  WITH CHECK ADD  CONSTRAINT [FK_BusinessPageECImage_ECImage] FOREIGN KEY([ECImageId])
REFERENCES [dbo].[ECImage] ([ECImageId])
GO

ALTER TABLE [dbo].[BusinessPageECImage] CHECK CONSTRAINT [FK_BusinessPageECImage_ECImage]
GO

DELETE FROM [dbo].[Email_Tag]
WHERE Tag_Id in (46, 47)
GO
SET IDENTITY_INSERT [dbo].[Email_Tag] ON 
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (46, N'CategoryName')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (47, N'SubCategoryName')
GO
SET IDENTITY_INSERT [dbo].[Email_Tag] OFF
GO

/****** Object:  View [dbo].[EventTicket_View]    Script Date: 29-Sep-16 11:17:38 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[EventTicket_View]
AS
SELECT 
	Ticket_Purchased_Detail.TPD_Id,
	Ticket_Purchased_Detail.TPD_Event_Id AS EventID,
	Ticket.T_name AS TicketName,
	Order_Detail_T.O_Id OId, 
	Order_Detail_T.O_Order_Id OrderId, 
	Order_Detail_T.O_First_Name FirstName, 
	Order_Detail_T.O_Last_Name LastName, 
	Order_Detail_T.O_Email Email,     
	Order_Detail_T.O_OrderAmount AS OrderAmount,
	Order_Detail_T.O_VariableAmount AS VariableAmount,  
	Order_Detail_T.O_VariableId AS VariableIds,
	Ticket_Quantity_Detail.TQD_Quantity AS TotalQuantity,
	Ticket_Purchased_Detail.TPD_Purchased_Qty AS PurchasedQuantity, 
	Ticket_Purchased_Detail.TPD_Amount AS PaidAmount, 
	CASE WHEN Ticket.TicketTypeID = 2 THEN ISNULL(Ticket.EC_Fee,0)-ISNULL(Ticket.T_EcAmount,0) ELSE 0 END AS ECFeePerTicket,
	CASE WHEN Ticket.TicketTypeID = 2 OR Ticket.TicketTypeID = 3 THEN Ticket.T_EcAmount ELSE 0 END AS MerchantFeePerTicket,
	Ticket.Customer_Fee AS Customer_Fee, 
	TicketType.TicketTypeID, 
	TicketType.TicketType AS TicketTypeName, 
	Ticket_Purchased_Detail.TPD_PromoCodeID AS PromoCodeID, 
	Promo_Code.PC_Code AS PromoCode,
	Ticket_Purchased_Detail.TPD_PromoCodeAmount AS PromoCodeAmount,
	Order_Detail_T.O_OrderDateTime,
	Order_Detail_T.OrderStateId,
	OrderState.OrderStateName,
	Order_Detail_T.PaymentTypeId

FROM Ticket_Purchased_Detail
INNER JOIN Order_Detail_T ON Order_Detail_T.O_Order_Id=Ticket_Purchased_Detail.TPD_Order_Id
INNER JOIN Ticket_Quantity_Detail ON Ticket_Quantity_Detail.TQD_Id=Ticket_Purchased_Detail.TPD_TQD_Id
INNER JOIN Ticket ON Ticket.T_Id=Ticket_Quantity_Detail.TQD_Ticket_Id
INNER JOIN TicketType ON TicketType.TicketTypeID=Ticket.TicketTypeID
INNER JOIN Profile ON Profile.UserID = Ticket_Purchased_Detail.TPD_User_Id
INNER JOIN OrderState ON ISNULL(Order_Detail_T.OrderStateId, 1) = OrderState.OrderStateId
LEFT OUTER JOIN Promo_Code ON Promo_Code.PC_id=Ticket_Purchased_Detail.TPD_PromoCodeID

GO

/****** Object:  View [dbo].[TicketBearer_View]    Script Date: 06-Sep-16 10:52:12 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[TicketBearer_View]
AS
SELECT 0 TicketbearerId, t.OrderId, t.UserId, null as Guid, ISNULL(p.FirstName + ' ' + p.LastName, p.Email) Name, p.Email
FROM
(
  SELECT tpd.TPD_Order_Id OrderId, tpd.TPD_User_Id UserId, SUM(tpd.TPD_Purchased_Qty) qty
  FROM Ticket_Purchased_Detail tpd
  GROUP BY tpd.TPD_Order_Id, tpd.TPD_User_Id
) t
INNER JOIN Profile p ON p.UserID = t.UserId
LEFT JOIN
(
  SELECT tb.OrderId, tb.UserId, COUNT(*) qty
  FROM TicketBearer tb
  GROUP BY tb.OrderId, tb.UserId
) att ON t.OrderId = att.OrderId AND t.UserId = att.UserId AND t.qty > att.qty
 
UNION ALL
 
SELECT tb.TicketbearerId,  tb.OrderId, tb.UserId, tb.Guid, tb.Name, tb.Email
FROM TicketBearer tb


GO

/****** Object:  Table [dbo].[AttendeeEmail]    Script Date: 09-Sep-16 10:01:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttendeeEmail](
	[AttendeeEmailId] [bigint] IDENTITY(1,1) NOT NULL,
	[ScheduledEmailId] [bigint] NOT NULL,
	[TicketbearerId] [bigint] NULL,
	[EventID] [bigint] NOT NULL,
 CONSTRAINT [PK_AttendeeEmail] PRIMARY KEY CLUSTERED 
(
	[AttendeeEmailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmailType]    Script Date: 09-Sep-16 10:01:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EmailType](
	[EmailTypeId] [tinyint] IDENTITY(1,1) NOT NULL,
	[EmailType] [varchar](50) NOT NULL,
	[EmailTypeDesc] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_EmailType] PRIMARY KEY CLUSTERED 
(
	[EmailTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScheduledEmail]    Script Date: 09-Sep-16 10:01:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScheduledEmail](
	[ScheduledEmailId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[EmailTypeId] [tinyint] NOT NULL,
	[SendFrom] [nvarchar](300) NOT NULL,
	[SendTo] [nvarchar](max) NOT NULL,
	[ReplyTo] [nvarchar](300) NOT NULL,
	[CC] [nvarchar](300) NOT NULL,
	[BCC] [nvarchar](300) NOT NULL,
	[Subject] [nvarchar](500) NOT NULL,
	[Body] [nvarchar](max) NOT NULL,
	[ScheduledDate] [datetime] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[SendDate] [datetime] NULL,
	[IsEmailSend] [bit] NOT NULL,
 CONSTRAINT [PK_ScheduledEmail] PRIMARY KEY CLUSTERED 
(
	[ScheduledEmailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[EmailType] ON 

GO
INSERT [dbo].[EmailType] ([EmailTypeId], [EmailType], [EmailTypeDesc]) VALUES (1, N'Attendee', N'Attendee')
GO
SET IDENTITY_INSERT [dbo].[EmailType] OFF
GO
ALTER TABLE [dbo].[ScheduledEmail] ADD  CONSTRAINT [DF_ScheduledEmail_CreateDate]  DEFAULT (getutcdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[ScheduledEmail] ADD  CONSTRAINT [DF_ScheduledEmail_IsEmailSend]  DEFAULT ((0)) FOR [IsEmailSend]
GO
ALTER TABLE [dbo].[AttendeeEmail]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeEmail_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([EventID])
GO
ALTER TABLE [dbo].[AttendeeEmail] CHECK CONSTRAINT [FK_AttendeeEmail_Event]
GO
ALTER TABLE [dbo].[AttendeeEmail]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeEmail_ScheduledEmail] FOREIGN KEY([ScheduledEmailId])
REFERENCES [dbo].[ScheduledEmail] ([ScheduledEmailId])
GO
ALTER TABLE [dbo].[AttendeeEmail] CHECK CONSTRAINT [FK_AttendeeEmail_ScheduledEmail]
GO
ALTER TABLE [dbo].[ScheduledEmail]  WITH CHECK ADD  CONSTRAINT [FK_ScheduledEmail_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[ScheduledEmail] CHECK CONSTRAINT [FK_ScheduledEmail_AspNetUsers]
GO
ALTER TABLE [dbo].[ScheduledEmail]  WITH CHECK ADD  CONSTRAINT [FK_ScheduledEmail_EmailType] FOREIGN KEY([EmailTypeId])
REFERENCES [dbo].[EmailType] ([EmailTypeId])
GO
ALTER TABLE [dbo].[ScheduledEmail] CHECK CONSTRAINT [FK_ScheduledEmail_EmailType]
GO


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

		SET @T_Fee = (@TotalPrice - @T_Price +@Discount)
		
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
					SET @Html = CONCAT(@Html,'<div class="midtablecell text-right">','<strike>',@T_Price ,'</strike> $',(@T_Price - @Discount),'</div>') 
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
ALTER TABLE dbo.Ticket_Purchased_Detail ADD
	Customer_Fee numeric(18, 2) NOT NULL CONSTRAINT DF_Ticket_Purchased_Detail_Customer_Fee DEFAULT 0
GO
ALTER TABLE dbo.Ticket_Purchased_Detail SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

GO

UPDATE Ticket
SET Customer_Fee = 0
WHERE EC_Fee = Customer_Fee

UPDATE tpd
SET tpd.TPD_Amount = t.TotalPrice * tpd.TPD_Purchased_Qty 
FROM Ticket_Purchased_Detail tpd
INNER JOIN Ticket_Quantity_Detail tqd ON tqd.TQD_Id = tpd.TPD_TQD_Id
INNER JOIN Ticket t ON t.T_Id = tqd.TQD_Ticket_Id
INNER JOIN Order_Detail_T o ON o.O_Order_Id = tpd.TPD_Order_Id and o.O_OrderDateTime >= '20160915'
WHERE (t.TotalPrice * tpd.TPD_Purchased_Qty) <> tpd.TPD_Amount