

ALTER TABLE Order_Detail_T ALTER COLUMN O_variableId VARCHAR(500)

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'O_PayPal_TokenId' AND Object_ID = Object_ID(N'Order_Detail_T'))
	ALTER TABLE Order_Detail_T ADD O_PayPal_TokenId VARCHAR(100)

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'O_PayPal_PayerId' AND Object_ID = Object_ID(N'Order_Detail_T'))
	ALTER TABLE Order_Detail_T ADD O_PayPal_PayerId VARCHAR(100)

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'O_PayPal_TrancId' AND Object_ID = Object_ID(N'Order_Detail_T'))
	ALTER TABLE Order_Detail_T ADD O_PayPal_TrancId VARCHAR(100)



/****** Object:  Table [dbo].[BillingAddress]    Script Date: 2/15/2016 7:48:45 PM ******/
DROP TABLE [dbo].[BillingAddress]
GO

/****** Object:  Table [dbo].[BillingAddress]    Script Date: 2/15/2016 7:48:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

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
	[PaymentType] [varchar](50) NULL,
	[CardId] [varchar](50) NULL,
	[card_type] [varchar](150) NULL,
	[ExpirationDate] [varchar](50) NULL,
	[Cvv] [varchar](50) NULL,
 CONSTRAINT [PK_BillingAddress] PRIMARY KEY CLUSTERED 
(
	[BillingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Trigger [tgCalculateQty]    Script Date: 2/15/2016 7:48:25 PM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tgCalculateQty]') AND type in (N'TR'))
	DROP TRIGGER [dbo].[tgCalculateQty]

GO

/****** Object:  Trigger [dbo].[tgCalculateQty]    Script Date: 2/15/2016 7:48:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE TRIGGER [dbo].[tgCalculateQty] ON [dbo].[Ticket_Purchased_Detail] 
FOR INSERT
AS
	DECLARE @Qty bigint
	DECLARE @TQDId BIGINT
	DECLARE @GUID varchar(1000)
	DECLARE @OrderId varchar(11)

	SELECT  @TQDId = i.TPD_TQD_Id,@Qty= i.TPD_Purchased_Qty ,@GUID = i.TPD_GUID,@OrderId=i.TPD_Order_Id FROM inserted i

	
	UPDATE Ticket_Quantity_Detail SET TQD_Remaining_Quantity = (TQD_Remaining_Quantity - @Qty)
	WHERE TQD_Id = @TQDId 

	DELETE FROM Ticket_Locked_Detail WHERE TLD_GUID = @GUID

	DECLARE @OrderNo VARCHAR(100)

	DECLARE @cnt INT = 0;
	WHILE @cnt < @Qty
	BEGIN

	  SET @cnt = @cnt + 1;

	  SET @OrderNo=  REPLACE(STR(CAST(CAST(NEWID() AS binary(5)) AS bigint),12),0,0)
	
	insert into TicketOrderDetail(T_Order_Id,T_Guid,T_Id,T_TQD_Id)values(@OrderId,@GUID,@OrderNo,@TQDId)
	END
GO

/****** Object:  Table [dbo].[TicketOrderDetail]    Script Date: 2/15/2016 7:48:03 PM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TicketOrderDetail]') AND type in (N'U'))
	DROP TABLE [dbo].[TicketOrderDetail]
GO

/****** Object:  Table [dbo].[TicketOrderDetail]    Script Date: 2/15/2016 7:48:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TicketOrderDetail](
	[T_TQD_Id] [bigint] NULL,
	[T_Guid] [varchar](1000) NULL,
	[T_Id] [varchar](12) NOT NULL,
	[T_Order_Id] [varchar](11) NULL,
 CONSTRAINT [PK_TicketOrderDetail] PRIMARY KEY CLUSTERED 
(
	[T_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO




/****** Object:  Table [dbo].[Payment_Info]    Script Date: 2/15/2016 7:47:48 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Payment_Info]') AND type in (N'U'))
	DROP TABLE [dbo].[Payment_Info]

GO

/****** Object:  Table [dbo].[Payment_Info]    Script Date: 2/15/2016 7:47:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Payment_Info](
	[PaymentInfo_Id] [int] IDENTITY(1,1) NOT NULL,
	[PI_PaymentMethod] [varchar](1) NULL,
	[PI_AccountHolderType] [varchar](1) NULL,
	[PI_AccCompName] [varchar](1000) NULL,
	[PI_Address] [nvarchar](max) NULL,
	[PI_City] [varchar](500) NULL,
	[PI_State] [varchar](500) NULL,
	[PI_Country] [int] NULL,
	[PI_PostalCode] [varchar](10) NULL,
	[PI_BankAccInfo] [varchar](1) NULL,
	[PI_BankName] [varchar](500) NULL,
	[PI_RoutingNumber] [varchar](100) NULL,
	[PI_AccountNumber] [varchar](100) NULL,
	[PI_ReAccountNumber] [varchar](100) NULL,
	[PI_PayTo] [varchar](500) NULL,
	[PI_EventId] [bigint] NULL,
	[PI_Address2] [nvarchar](max) NULL,
 CONSTRAINT [PK_Payment_Info] PRIMARY KEY CLUSTERED 
(
	[PaymentInfo_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO










/****** Object:  Table [dbo].[Messages]    Script Date: 2/15/2016 7:54:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Messages]') AND type in (N'U'))
	DROP TABLE [dbo].[Messages]
GO
/****** Object:  Table [dbo].[Email_Tag]    Script Date: 2/15/2016 7:54:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Email_Tag]') AND type in (N'U'))
DROP TABLE [dbo].[Email_Tag]
GO
/****** Object:  Table [dbo].[Email_Tag]    Script Date: 2/15/2016 7:54:52 PM ******/
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
/****** Object:  Table [dbo].[Messages]    Script Date: 2/15/2016 7:54:52 PM ******/
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
SET IDENTITY_INSERT [dbo].[Email_Tag] OFF
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
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1097, N'ManageEvent', N'MEPublisheventSucc', N'Congratulations! Your event is live now.')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1098, N'ManageEvent', N'MEDeleteEvent', N'Are you sure you want to delete this event? This cannot be undone')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1099, N'ManageEvent', N'MEunPublish', N'Are you sure you want to unpublish event?')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1100, N'ManageEvent', N'MECancel', N'Are you sure you want to cancel the event?')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1101, N'ManageEvent', N'MEnterUrlUI', N'Url cannot be empty!!')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1102, N'PaymentInfo', N'PICompanynameUI', N'Please Enter Company name')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1103, N'PaymentInfo', N'PInameUI', N'Please Enter Name')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1104, N'PaymentInfo', N'PIAddressUI', N'Please Enter Address Details')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1105, N'PaymentInfo', N'PIBankNameUI', N'Please enter bank name')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1106, N'PaymentInfo', N'PIRoutingnoUI', N'Please enter routing number')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1107, N'PaymentInfo', N'PIAccountinfoUI', N'Please Enter Account info')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1108, N'PaymentInfo', N'PIPayeeUI', N'Please Enter Payee name')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1109, N'PaymentInfo', N'PIAccountinfocompUI', N'Account and Re account number not same')
GO
SET IDENTITY_INSERT [dbo].[Messages] OFF
GO
