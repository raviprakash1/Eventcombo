/****** Object:  Table [dbo].[Email_Template]    Script Date: 5/25/2016 5:48:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Email_Template]') AND type in (N'U'))
DROP TABLE [dbo].[Email_Template]
GO
/****** Object:  Table [dbo].[Email_Tag]    Script Date: 5/25/2016 5:48:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Email_Tag]') AND type in (N'U'))
DROP TABLE [dbo].[Email_Tag]
GO
/****** Object:  Table [dbo].[Email_Tag]    Script Date: 5/25/2016 5:48:43 PM ******/
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
/****** Object:  Table [dbo].[Email_Template]    Script Date: 5/25/2016 5:48:43 PM ******/
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
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (32, N'CreateEventurl')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (33, N'DiscoverEventurl')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (34, N'MyTicketEventurl')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (35, N'EventDynamicTable')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (36, N'EventMapImage')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (37, N'EventLogin')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (38, N'Downloadurl')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (39, N'OrderDetail')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (40, N'Eventtypedetail')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (41, N'ClickHere')
GO
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (42, N'EventOrganiserNumber')
GO
SET IDENTITY_INSERT [dbo].[Email_Tag] OFF
GO
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag], [From_Name]) VALUES (N'1d1f7166-910e-4fd2-90ce-609ef27b726e', N'New Event Notification Template', N'shweta.sindhu@kiwitech.com', NULL, NULL, N' ¶¶EventOrganiserName¶¶ Created   ¶¶EventTitleId¶¶ in  ¶¶EventAddressID¶¶', N'<table border="0" cellpadding="1" cellspacing="1" style="width:100%">
	<tbody>
		<tr>
			<td><img alt="logo" src="http://eventcombo.com/Images/logo_vertical.png" style="height:375px; width:57px" /></td>
			<td style="vertical-align:top">&nbsp; &para;&para;EventOrganiserName&para;&para; Created &para;&para;EventTitleId&para;&para; &nbsp;in &para;&para;EventAddressID&para;&para;<br />
			Contact &para;&para;EventOrganiserName&para;&para;&nbsp;via &para;&para;EventOrganiserEmail&para;&para; &para;&para;EventOrganiserNumber&para;&para;</td>
		</tr>
	</tbody>
</table>

<p>&nbsp;</p>
', NULL, N'new_event_notification_email', NULL)
GO
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag], [From_Name]) VALUES (N'1fea48b7-f5fd-478d-ad22-3c9cec6ca29f', N'Ticket Purchase Template', N' ¶¶EventOrganiserEmail¶¶', N'tickets@eventcombo.com', NULL, N' Eventcombo Processed  ¶¶TicketQty¶¶ Tickets F or ¶¶EventTitleId¶¶ to ¶¶UserFirstNameID¶¶', N'<table align="left" border="0" cellpadding="1" cellspacing="1" style="width:100%">
	<tbody>
		<tr>
			<td><img alt="logo" src="http://eventcombo.com/Images/logo_vertical.png" style="height:375px; width:57px" /></td>
			<td style="vertical-align:top">&nbsp; &para;&para;UserFirstNameID&para;&para; &nbsp;booked &para;&para;TicketQty&para;&para; tickets for your &para;&para;EventTitleId&para;&para;. Please <a href="¶¶ClickHere¶¶">Click Here</a>&nbsp;to login to Eventcombo and view your Attendees.</td>
		</tr>
	</tbody>
</table>

<p>&nbsp;</p>
', NULL, N'email_ticket_purchase', NULL)
GO
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag], [From_Name]) VALUES (N'2ab53970-a668-4c07-ac73-3133a5447150', N'E-Ticket Template', N' ¶¶UserEmailID¶¶', NULL, NULL, N'Your order for  ¶¶EventTitleId¶¶-  ¶¶EventOrderNO¶¶', N'<table border="0" cellpadding="0" cellspacing="0" style="width:100%">
	<tbody>
		<tr>
			<td>
			<table>
				<tbody>
					<tr>
						<td><a href="¶¶CreateEventurl¶¶">Create Event</a> <a href="¶¶DiscoverEventurl¶¶">Discover Events</a> <a href="¶¶MyTicketEventurl¶¶">My Tickets</a></td>
					</tr>
				</tbody>
			</table>

			<table style="width:100%">
				<tbody>
					<tr>
						<td>
						<table>
							<tbody>
								<tr>
									<td>
									<table>
										<tbody>
											<tr>
												<td><img src="http://eventcombonew-qa.kiwireader.com/Images/logo_vertical.png" /></td>
												<td>
												<h1>Hi &para;&para;UserFirstNameID&para;&para;&nbsp;&nbsp;, this is your e-ticket confirmation for &para;&para;EventTitleId&para;&para; Organized by &para;&para;EventOrganiserName&para;&para;&nbsp;</h1>

												<p>Organized by &nbsp;&para;&para;EventOrganiserName&para;&para;&nbsp;</p>

												<p>&nbsp;</p>

												<h1>Get tickets :</h1>

												<p style="text-align:center"><img src="http://eventcombonew-qa.kiwireader.com/Images/paper_tic.png" /><br />
												Open the email attachement<br />
												or <a href="¶¶Downloadurl¶¶">download here </a></p>

												<h1>Question about this event ?</h1>

												<p>Contact the organizer at&nbsp;<a href="#">&nbsp;&para;&para;EventOrganiserEmail&para;&para;&nbsp;</a></p>
												</td>
											</tr>
											<tr>
												<td>&nbsp;</td>
												<td>&para;&para;EventDynamicTable&para;&para;</td>
											</tr>
											<tr>
												<td>&nbsp;</td>
												<td style="width:100%">
												<h1>About this event</h1>

												<p><img src="http://eventcombonew-qa.kiwireader.com/Images/em_ven_time.png" />&para;&para;EventStartDateID&para;&para;</p>

												<p style="text-align:center">To</p>

												<p>&para;&para;EventEndDateID&para;&para;</p>

												<p>Venue Name</p>

												<p><img src="http://eventcombonew-qa.kiwireader.com/Images/email_loc_icn.png" /> &para;&para;EventVenueID&para;&para;</p>
												&para;&para;EventMapImage&para;&para;

												<p>&para;&para;Eventtype&para;&para;</p>

												<p>&nbsp;</p>
												</td>
											</tr>
											<tr>
												<td>&nbsp;</td>
												<td style="width:100%"><img src="http://eventcombonew-qa.kiwireader.com/Images/login_img.png" />
												<h1>Your Account</h1>

												<p><a href="¶¶EventLogin¶¶">Log in </a> to access tickets and manage your orders.</p>
												</td>
											</tr>
											<tr>
												<td>&nbsp;</td>
												<td style="width:100%">
												<h1>Events you may also like</h1>
												</td>
											</tr>
											<tr>
												<td>&nbsp;</td>
												<td style="width:100%"><img src="http://eventcombonew-qa.kiwireader.com/Images/evnt_lik_1.png" />
												<p>Wed , Jan 2016</p>

												<h1>Startup Breakfast with an investor: Jessica Peltz</h1>

												<p>by Startup<br />
												Coworker</p>
												</td>
											</tr>
											<tr>
												<td>&nbsp;</td>
												<td style="width:100%"><img src="http://eventcombonew-qa.kiwireader.com/Images/evnt_lik_1.png" />
												<p>Wed , Jan 2016</p>

												<h1>Startup Breakfast with an investor: Jessica Peltz</h1>

												<p>by Startup<br />
												Coworker</p>
												</td>
											</tr>
											<tr>
												<td>&nbsp;</td>
												<td style="width:100%">
												<p>&nbsp;</p>

												<p>View more event on this area</p>
												</td>
											</tr>
											<tr>
												<td>&nbsp;</td>
												<td style="width:100%">
												<p>&nbsp;</p>

												<h1><a href="¶¶CreateEventurl¶¶">Create your own event</a></h1>

												<h1><a href="¶¶DiscoverEventurl¶¶">Discover great event </a></h1>
												</td>
											</tr>
											<tr>
												<td>&nbsp;</td>
												<td>
												<p>&nbsp;</p>
												<a href="#">About </a> | <a href="#">Help </a> | <a href="#">My Account</a> | <a href="#">Contact Us</a> | <a href="#">Privacy </a> | <a href="#">Terms </a>| <a href="#">Blog </a>| <a href="#"> <img src="http://eventcombonew-qa.kiwireader.com/Images/email_fb.png" /> </a> <a href="#"> <img src="http://eventcombonew-qa.kiwireader.com/Images/email_tw.png" /> </a>

												<p>&nbsp;</p>
												</td>
											</tr>
										</tbody>
									</table>
									</td>
								</tr>
							</tbody>
						</table>
						</td>
					</tr>
				</tbody>
			</table>
			</td>
		</tr>
	</tbody>
</table>
', N'shweta.sindhu@kiwitech.com', N'eticket', N'EventCombo')
GO
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag], [From_Name]) VALUES (N'2e223022-9c7e-45e0-8e5b-6c8776762021', N'Welcome Template', N' ¶¶UserEmailID¶¶ ', N'just.shweta29@gmail.com, ¶¶UserEmailID¶¶', N'navrit.singh@kiwitech.com, ¶¶UserEmailID¶¶', N'Welcome to Eventcombo   ¶¶UserEmailID¶¶, ¶¶DealOrderNumberID¶¶', N'<p>Hi ,</p>

<p>Thank you &para;&para;Ticketname&para;&para; for choosing eventcombo.Lets get started. &para;&para;EventStartDateID&para;&para;&nbsp;,,,&para;&para;EventNameID&para;&para;</p>

<p>&nbsp;</p>
', N'welcome@eventcombo.com', N'email_welcome', N'EventCombo')
GO
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag], [From_Name]) VALUES (N'405f9cc8-782b-4b87-8406-3a3c0ae4c60d', N'Lost password Template', N' ¶¶UserEmailID¶¶', N'just.shweta29@gmail.com', NULL, N'Reset Password ', N'<p><img alt="eventcombo" src="http://eventcombo.kiwireader.com/Images/logo_vertical.png" style="float:left; height:400px; margin:2px; width:86px" />&nbsp;Hello&nbsp; &para;&para;UserFirstNameID&para;&para; ,</p>

<p>We have recieved a request to reset your password for your account :&nbsp;&para;&para;UserEmailID&para;&para; .We&#39;re here to help!</p>

<p>Click on the link below to reset and create a new password:</p>

<p>&para;&para;ResetPwdUrl&para;&para;</p>

<p>Set a new Password if you didn&#39;t ask to change your password,don&#39;t worry!Your password is still safe and you can delete this email.</p>

<p>Best ,</p>

<p>The Eventcombo Team.</p>

<p>&nbsp;</p>
', N'shweta.sindhu@kiwitech.com', N'email_lost_pwd', N'EventCombo')
GO
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag], [From_Name]) VALUES (N'aa3f1945-418b-4fc8-8a6d-6c60741fa70a', N'Account Info Update Template', N' ¶¶UserEmailID¶¶', NULL, NULL, N'Your Account Info is successfuly updated', N'<p>Hi&nbsp; &para;&para;UserFirstNameID&para;&para;,</p>

<p>You Account info is updated successfully.</p>
', N'shweta.sindhu@kiwitech.com', N'acc_info_update', N'EventCombo')
GO
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag], [From_Name]) VALUES (N'edfb6c74-43f8-45e7-86d1-d19aaf7f5e37', N'', N' ¶¶UserEmailID¶¶', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag], [From_Name]) VALUES (N'fd04710c-8d72-43e5-ab83-c33da92adc15', N'Account Password Set Template', N' ¶¶UserEmailID¶¶', NULL, NULL, N'Your Password has Reset', N'<p>Hi&nbsp; &para;&para;UserFirstNameID&para;&para; ,</p>

<p>Your password has been successfully reset.</p>

<p>&nbsp;</p>

<p>&nbsp;</p>

<p>With Best Wishes,</p>

<p>The Eventcombo Team.</p>
', N'shweta.sindhu@kiwitech.com', N'acc_pwd_set', N'Eventcombo')
GO





/****** Object:  StoredProcedure [dbo].[GetSelectedTicketListing]    Script Date: 5/27/2016 8:49:32 PM ******/
DROP PROCEDURE [dbo].[GetSelectedTicketListing]
GO

/****** Object:  StoredProcedure [dbo].[GetTicketListing]    Script Date: 5/27/2016 8:49:32 PM ******/
DROP PROCEDURE [dbo].[GetTicketListing]
GO

/****** Object:  StoredProcedure [dbo].[GetTicketListing]    Script Date: 5/27/2016 8:49:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




--exec [GetTicketListing] '24425'





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

SET @Html = '<div class="col-sm-12 mt10 no_pad">'
DECLARE @TotalTicQty bigint  SET @TotalTicQty =0
WHILE (@RCnt>0)
BEGIN
	SELECT @PublishId = P_Id FROM @tblPublish WHERE Id= @RInd
	SELECT @StartDate = (ISNULL(PE_Scheduled_Date,'') + ', ' + ISNULL(PE_Start_Time,'')), @AddIdss=isnull(PE_Address_Ids,'')
	FROM Publish_Event_Detail WHERE PE_Id = @PublishId
	
	SET @Html = CONCAT(@Html,'<div class="col-sm-12 col-xs-12 no_pad divticketdate">',@StartDate,'</div>')

	INSERT INTO @tblMain SELECT TQD_Id FROM Ticket_Quantity_Detail WHERE TQD_PE_Id =@PublishId
	SET @RowCount = @@ROWCOUNT
		IF (RTRIM(LTRIM(@AddIdss)) = '')	
		BEGIN
			If (@RowIndex>1 AND @SHowRemaingTick ='Y')
			BEGIN
				SET @Html = CONCAT(@Html,' <div class="col-sm-12 col-xs-12 no_pad text-right tick_ramin_quant_main">','<div class="col-sm-12 col-xs-12 tick_ramin_quant">','Remaining Quantity : ' , @TRemainRuning ,'</div>','</div>') 
				SET @TRemainRuning =0
			END

			SET @Html = CONCAT(@Html,'<div class="col-sm-12 col-xs-12 no_pad table_div_container">')
			--SET @Html = CONCAT(@Html,'<div class="col-sm-12 col-xs-12 no_pad divticketdate">',@StartDate,'</div>')

			SET @Html = CONCAT(@Html,'<div class="divTable">')
				SET @Html = CONCAT(@Html,'<div class="divTablehead">')
					SET @Html = CONCAT(@Html,'<div class="divTableCell">Ticket Type</div>')
					SET @Html = CONCAT(@Html,'<div class="divTableCell text-right">Sales End</div>')
					SET @Html = CONCAT(@Html,'<div class="divTableCell text-right">','Price','</div>')
					SET @Html =	CONCAT(@Html,'<div class="divTableCell text-right">Fee</div>')
					SET @Html = CONCAT(@Html,'<div class="divTableCell text-right">Qty</div>')
				SET @Html = CONCAT(@Html,'</div>')
			SET @Html = CONCAT(@Html,'</div>')
		END


	WHILE (@RowCount>0)
	BEGIN		

		SELECT @TQTY_Id = QTY_Id FROM @tblMain WHERE Id= @RowIndex
		SELECT @AddId = TQD_AddressId, @TicId = TQD_Ticket_Id 
		FROM Ticket_Quantity_Detail WHERE TQD_ID = @TQTY_Id

		
		
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
		if (rtrim(ltrim(@TicketSaleEnd)) = ',') set @TicketSaleEnd=''
		
		IF (@NextAddId != @AddId)	
		BEGIN
			If (@RowIndex>1 AND @SHowRemaingTick ='Y')
			BEGIN
				SET @Html = CONCAT(@Html,' <div class="col-sm-12 col-xs-12 no_pad text-right tick_ramin_quant_main">','<div class="col-sm-12 col-xs-12 tick_ramin_quant">','Remaining Quantity : ' , @TRemainRuning ,'</div>','</div>') 
				SET @TRemainRuning =0
			END

			
			SET @Html = CONCAT(@Html,'<div class="col-sm-12 col-xs-12 no_pad table_div_container">')
			
	
			SET @NextAddId = @AddId 
			SELECT @Addess = ConsolidateAddress FROM ADDRESS WHERE AddressID = @AddId
			SET @Html = CONCAT(@Html,'<div class="col-sm-12 col-xs-12 no_pad divticketaddres word-brk-tilt">',@Addess,'</div>')
			SET @Html = CONCAT(@Html,'<div class="divTable">')
				SET @Html = CONCAT(@Html,'<div class="divTablehead">')
					SET @Html = CONCAT(@Html,'<div class="divTableCell">Ticket Type</div>')
					SET @Html = CONCAT(@Html,'<div class="divTableCell text-right">Sales End</div>')
					SET @Html = CONCAT(@Html,'<div class="divTableCell text-right">','Price','</div>')
					SET @Html =	CONCAT(@Html,'<div class="divTableCell text-right">Fee</div>')
					SET @Html = CONCAT(@Html,'<div class="divTableCell text-right">Qty</div>')
				SET @Html = CONCAT(@Html,'</div>')
			SET @Html = CONCAT(@Html,'</div>')
		END
		
		
		SELECT @RemQty = TQD_Remaining_Quantity 
		FROM Ticket_Quantity_Detail WHERE TQD_Id = @TQTY_Id

		SELECT @RemQty =  (@RemQty-isnull(sum(isnull(TLD_Locked_Qty,0)),0))
		FROM Ticket_Locked_Detail WHERE TLD_TQD_Id = @TQTY_Id
		SET @TRemainRuning = (@TRemainRuning + @RemQty)
		if (@MinQty>0) SET @iflag =1 Else SET @iflag =0

		SET @TMaxQty = '<option>0</option>'
		If (@MaxQty >0) SET @TQty = ((@MaxQty-@MinQty) +@iflag)
		
		IF (@TQty>@RemQty) SET @TQty =((@RemQty-@MinQty) +@iflag) 

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
			SET @TMaxQty = CONCAT('<option>',1,'</option>')
		END
		ELSE
		BEGIN
			SET @TotalTicQty = @TotalTicQty + @TQty
			WHILE (@TQty>0)
			BEGIN
				SET @TMaxQty = CONCAT(@TMaxQty,'<option>',@Option,'</option>')
				SET @Option = @Option +1
				SET @TQty = @TQty -1
			END
		END
		
		
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
		IF (@QtyIds='') SET @QtyIds = CONCAT(@QtyIds,@TQTY_Id)  ELSE SET @QtyIds = CONCAT(@QtyIds,',',@TQTY_Id) 
		SET @Html = CONCAT(@Html,'<div class="col-sm-12 col-xs-12 no_pad">')
		SET @Html = CONCAT(@Html,'<div class="divTable">')
		SET @Html = CONCAT(@Html,'<div class="divTableRow">')
		
		SET @Html = CONCAT(@Html,'<div class="midtablecell">', @T_Name ,'</div>')

		SET @Html = CONCAT(@Html,'<div class="midtablecell text-right">',@TicketSaleEnd,'</div>')
			
			IF (@TicTypeId = 2)  
			BEGIN
				if (@Discount>0)
				BEGIN
					SET @T_Fee = (@TotalPrice - @T_Price)
					SET @TotalPrice = (@T_Price  - @Discount) + @T_Fee 
					SET @Html = CONCAT(@Html,'<div class="midtablecell text-right">$', '<strike>',@T_Price,'</strike> $',(@T_Price-@Discount),'<span style="display:none" id="P_', @TQTY_Id, '">',@TotalPrice,'</span>','</div>')
					SET @Html = CONCAT(@Html,'<div class="midtablecell text-right">$',@T_Fee ,'</div>')
				END
				ELSE
				BEGIN
					SET @T_Fee = (@TotalPrice - @T_Price)
					SET @Html = CONCAT(@Html,'<div class="midtablecell text-right">$', @T_Price,'<span style="display:none" id="P_', @TQTY_Id, '">',@TotalPrice,'</span>','</div>')
					SET @Html = CONCAT(@Html,'<div class="midtablecell text-right">$',@T_Fee ,'</div>')
				END
			END
			ELSE IF (@TicTypeId = 1)  
			BEGIN
				SET @Html = CONCAT(@Html,'<div class="midtablecell text-right">','<span id="P_', @TQTY_Id, '">','Free','</span>','</div>')
				SET @Html = CONCAT(@Html,'<div class="midtablecell text-right">$','0.00','</div>')
			END
			ELSE IF (@TicTypeId = 3)  
			BEGIN
				SET @Html = CONCAT(@Html,'<div class="midtablecell text-right">','</div>')
				--SET @Html = CONCAT(@Html,'<div class="midtablecell text-right">','<div style="width:60%;float:right;" class="col-sm-10 no_pad"><span style="font-weight: normal; float: left; margin-right: 7px; margin-top:3px">$ </span><input type="text" style="width:80%;" class="form-control evnt_inp_cont" oncopy="return false" onpaste="return false" oncut="return false" onkeydown="return checknumeric(event);" onchange="calculateTickTotal();" placeholder=" Enter Donation" autocomplete="false" name="d" value="" id="txtd_',@TQTY_Id,'"  /></div>','</div>')
			END
			
--GetTicketListingDiv 281
			
			If (@TickatMarkSoldOut =1)
			BEGIN
				SET @Html = CONCAT(@Html,'<div class="midtablecell text-right">Sold Out<select style="display:none" class="form-control ev_drop_label" onchange="calculateTickTotal()" id=d_' ,CONVERT(VARCHAR,@TQTY_Id) ,'>', @TMaxQty,'</select><input type="hidden" value="" id="hid_', CONVERT(VARCHAR,@TQTY_Id) ,'" /></div>')
				SET @Html = CONCAT(@Html,'</div></div></div>')
			END
			ELSE IF (@SaleStartDate != '' AND CONVERT(DATE,@SaleStartDate) > CONVERT(DATE,GETDATE()))
			BEGIN
		
				SET @Html = CONCAT(@Html,'<div class="midtablecell text-right">Sales Not Start Yet<select style="display:none" class="form-control ev_drop_label" onchange="calculateTickTotal()" id=d_' ,CONVERT(VARCHAR,@TQTY_Id) ,'>', @TMaxQty,'</select><input type="hidden" value="" id="hid_', CONVERT(VARCHAR,@TQTY_Id) ,'" /></div>')
				SET @Html = CONCAT(@Html,'</div></div></div>')
			END
			ELSE if (@SaleEndDate != '' AND CONVERT(DATE,@SaleEndDate) < CONVERT(DATE,GETDATE()))
			BEGIN
				SET @Html = CONCAT(@Html,'<div class="midtablecell text-right">Sales End<select style="display:none" class="form-control ev_drop_label" onchange="calculateTickTotal()" id=d_' ,CONVERT(VARCHAR,@TQTY_Id) ,'>', @TMaxQty,'</select><input type="hidden" value="" id="hid_', CONVERT(VARCHAR,@TQTY_Id) ,'" /></div>')
				SET @Html = CONCAT(@Html,'</div></div></div>')
			END
			ELSE
			BEGIN
				if (@SaleStartDate != '' AND  @SaleStartTime != '' AND CONVERT(DATE,@SaleStartDate) = CONVERT(DATE,GETDATE()) AND CONVERT(TIME,@SaleStartTime) > CAST(GETDATE() AS TIME)) 
				BEGIN
					SET @Html = CONCAT(@Html,'<div class="midtablecell text-right">Sales Not Start Yet<select style="display:none" class="form-control ev_drop_label" onchange="calculateTickTotal()" id=d_' ,CONVERT(VARCHAR,@TQTY_Id) ,'>', @TMaxQty,'</select><input type="hidden" value="" id="hid_', CONVERT(VARCHAR,@TQTY_Id) ,'" /></div>')
					SET @Html = CONCAT(@Html,'</div></div></div>')
				END
				ELSE if (@SaleEndDate != '' AND @SaleEndTime != '' AND CONVERT(DATE,@SaleEndDate) = CONVERT(DATE,GETDATE()) AND CONVERT(TIME,@SaleEndTime) < CAST(GETDATE() AS TIME)) 
				BEGIN
					SET @Html = CONCAT(@Html,'<div class="midtablecell text-right">Sales End<select style="display:none" class="form-control ev_drop_label" onchange="calculateTickTotal()" id=d_' ,CONVERT(VARCHAR,@TQTY_Id) ,'>', @TMaxQty,'</select><input type="hidden" value="" id="hid_', CONVERT(VARCHAR,@TQTY_Id) ,'" /></div>')
					SET @Html = CONCAT(@Html,'</div></div></div>')
				END
				ELSE
				BEGIN
					If (@TicTypeId = 3) 
					BEGIN
						SET @Html = CONCAT(@Html,'<div class="midtablecell text-right"><select style="width:90px;display:none" class="form-control ev_drop_label" onchange="calculateTickTotal()" id=d_' ,CONVERT(VARCHAR,@TQTY_Id) ,'>', @TMaxQty,'</select><input type="hidden" value="" id="hid_', CONVERT(VARCHAR,@TQTY_Id) ,'" />','<input type="hidden" value="',@TicTypeId,'" id="hidType_', CONVERT(VARCHAR,@TQTY_Id) ,'" />','</div>')
						SET @Html = CONCAT(@Html,'<div class="midtablecell text-right">','<div style="width:140px;float:right;" class="col-sm-10 no_pad"><span style="font-weight: normal; float: left; margin-right: 7px; margin-top:3px">$ </span><input type="text" style="width:120px;" class="form-control evnt_inp_cont table_input" oncopy="return false" onpaste="return false" oncut="return false" onkeydown="return checknumeric(event);" onchange="calculateTickTotal();" placeholder=" Enter Donation" autocomplete="false" name="d" value="" id="txtd_',@TQTY_Id,'"  /></div>','</div>')
					END
					ELSE
					BEGIN
						SET @Html = CONCAT(@Html,'<div class="midtablecell text-right divtabledrop"><select style="width:90px; float:right;" class="form-control selectpicker" onchange="calculateTickTotal()" id=d_' ,CONVERT(VARCHAR,@TQTY_Id) ,'>', @TMaxQty,'</select><input type="hidden" value="" id="hid_', CONVERT(VARCHAR,@TQTY_Id) ,'" />','<input type="hidden" value="',@TicTypeId,'" id="hidType_', CONVERT(VARCHAR,@TQTY_Id) ,'" />','</div>')
					END
					SET @Html = CONCAT(@Html,'</div></div></div>')
				END
			END


			if (@IsShowDesc = 1 AND isnull(@TicketDesc,'') != '')
			BEGIN
				SET @Html = CONCAT(@Html,'<div class="col-sm-12 col-xs-12 no_pad ticket_tab_more">',' <span class="TktmoreDesc tkt_more_desc"><i onclick="checkMore(',@TQTY_Id,')">[More Info]</i></span>','<div class="TktdescDetl tkt_more_desc_bot"><span style="display:none;" id="Sp_',@TQTY_Id,'"class="col-sm-12 no_pad">',@TicketDesc,'</span></div>','</div>')
			END



		END

		SET @RowCount = @RowCount  -1
		SET @RowIndex = @RowIndex  +1
		
		If (@AddIdss='' AND @RowCount=0 AND @SHowRemaingTick ='Y')
		BEGIN
			SET @Html = CONCAT(@Html,' <div class="col-sm-12 col-xs-12 no_pad text-right tick_ramin_quant_main">','<div class="col-sm-12 col-xs-12 tick_ramin_quant">','Remaining Quantity : ' , @TRemainRuning ,'</div>','</div>') 
			SET @TRemainRuning =0
		END

	END

	--GetTicketListingDiv 0

	

	SET @RCnt = @RCnt -1
	SET @RInd = @RInd +1
END
	if (@AddIdss != '' AND @SHowRemaingTick ='Y')
	BEGIN
		SET @Html = CONCAT(@Html,' <div class="col-sm-12 col-xs-12 no_pad text-right tick_ramin_quant_main">','<div class="col-sm-12 col-xs-12 tick_ramin_quant">','Remaining Quantity : ' , @TRemainRuning ,'</div>','</div>') 
		SET @TRemainRuning =0
	END
--declare @Html varchar(Max)
--SET @Html ='</table>'
--SET @Html = CONCAT(@Html,'First')
--SET @Html = CONCAT(@Html, 'Second')
--SET @Html += '<td>Satnam Waheguru</td>'
--SET @Html += '</tr>'
SET @Html = CONCAT(@Html,'</div>')
SET @Html = CONCAT(@Html,'<input id="hdIds" type="hidden" value=''', @QtyIds ,''' />')
SET @Html = CONCAT(@Html,'<input id="hdremaningqty" type="hidden" value=', @TotalTicQty ,' />')


Select @Html as Ticket


END

































GO

/****** Object:  StoredProcedure [dbo].[GetSelectedTicketListing]    Script Date: 5/27/2016 8:49:32 PM ******/
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


		if (@HideTicket !=1)
		BEGIN
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

			--if (@IsShowDesc = 1 AND isnull(@TicketDesc,'') != '')
			--BEGIN
			--	SET @Html = CONCAT(@Html,'<tr><td style="width:40%">', @T_Name,'<br><span class="tkt_more_desc">  <i onclick="checkMore(',@TQTY_Id,')">[More Info]</i></span>', '<span style="display:none;" id="Sp_',@TQTY_Id,'"class="col-sm-12 no_pad">',@TicketDesc,'</span>' ,'</td>')
			--END
			--ELSE		
			--BEGIN
			--	SET @Html = CONCAT(@Html,'<tr><td style="width:40%">', @T_Name,'<br>', '<span style="display:none;" id="Sp_',@TQTY_Id,'"class="col-sm-12 no_pad">',@TicketDesc,'</span>' ,'</td>')
			--END

		

		--SET @T_Price = CASE WHEN @T_Price =  THEN 0 ELSE COnvert(decimal,@T_Price) END
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







































GO






	ALTER TABLE AspNetUsers ADD [LastLoginTime] DATETIME











