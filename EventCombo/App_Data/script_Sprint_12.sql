IF COL_LENGTH('Ticket','T_Customize') IS NULL
	alter table Ticket add T_Customize varchar(1)

IF COL_LENGTH('Ticket','T_Ecpercent') IS NULL
	alter table Ticket add T_Ecpercent decimal(9,2)

IF COL_LENGTH('Ticket','T_EcAmount') IS NULL
	alter table Ticket add T_EcAmount decimal(9,2)


/****** Object:  Table [dbo].[Fee_Structure]    Script Date: 3/1/2016 12:06:58 PM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Fee_Structure]') AND type in (N'U'))
	DROP TABLE [dbo].[Fee_Structure]
GO
/****** Object:  Table [dbo].[Fee_Structure]    Script Date: 3/1/2016 12:06:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Fee_Structure]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Fee_Structure](
	[FS_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FS_Type] [varchar](50) NULL,
	[FS_Amount] [numeric](18, 2) NULL,
	[FS_Percentage] [numeric](18, 2) NULL,
	[FS_Apply] [varchar](1) NULL,
 CONSTRAINT [PK_Fee_Structure] PRIMARY KEY CLUSTERED 
(
	[FS_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO







/****** Object:  Trigger [tgUpdateFee]    Script Date: 3/1/2016 12:12:02 PM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tgUpdateFee]') AND type in (N'TR'))
	DROP TRIGGER [dbo].[tgUpdateFee]

GO

/****** Object:  Trigger [dbo].[tgUpdateFee]    Script Date: 3/1/2016 12:12:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[tgUpdateFee] ON [dbo].[Fee_Structure]
FOR UPDATE
AS
	DECLARE @Amount numeric(18,2)
	DECLARE @Percent numeric(18,2)
	DECLARE @Apply varchar(1)

	SELECT  @Amount = i.[FS_Amount],@Percent= i.[FS_Percentage],@Apply=i.FS_Apply  FROM inserted i

	if(@Apply='A')
	begin

	UPDATE Ticket SET [EC_Fee] = (((Price*@Percent)/100) + @Amount),[Customer_Fee]=(((Price*@Percent)/100) + @Amount),
   [TotalPrice]=Price+(((Price*@Percent)/100) + @Amount) ,[T_Ecpercent]=@Percent,[T_EcAmount]=@Amount,
   [T_Customize]='0'
   where [E_Id]  not in (select EventID from event where ISNULL([EventCancel],'N')='Y' ) AND 
   [E_Id]  not in (select EventID from [EventVenue] where (convert(datetime,CONVERT(VARCHAR,convert(Date,[EventEndDate]),106 )) + convert(datetime,CONVERT(VARCHAR,[EventEndTime])))<GETDATE() )
   AND    [E_Id]  not in (select EventID from [MultipleEvent]  where (convert(datetime,CONVERT(VARCHAR,convert(Date,[StartingTo]),106 )) + convert(datetime,CONVERT(VARCHAR,[EndTime])))<GETDATE() )
   end
   else
   begin 
   UPDATE Ticket SET [EC_Fee] = (((Price*@Percent)/100) + @Amount),[Customer_Fee]=(((Price*@Percent)/100) + @Amount),
   [TotalPrice]=Price+(((Price*@Percent)/100) + @Amount)  ,[T_Ecpercent]=@Percent,[T_EcAmount]=@Amount
   where [E_Id]  not in (select EventID from event where ISNULL([EventCancel],'N')='Y' ) AND 
   [E_Id]  not in (select EventID from [EventVenue] where (convert(datetime,CONVERT(VARCHAR,convert(Date,[EventEndDate]),106 )) + convert(datetime,CONVERT(VARCHAR,[EventEndTime])))<GETDATE() )
   AND    [E_Id]  not in (select EventID from [MultipleEvent]  where (convert(datetime,CONVERT(VARCHAR,convert(Date,[StartingTo]),106 )) + convert(datetime,CONVERT(VARCHAR,[EndTime])))<GETDATE() )
   and isnull([T_Customize],0)=0
   end
	
	
	

	








GO







/****** Object:  Table [dbo].[Messages]    Script Date: 3/1/2016 12:31:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Messages]') AND type in (N'U'))
	DROP TABLE [dbo].[Messages]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 3/1/2016 12:31:05 PM ******/
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
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1110, N'FeeSetting', N'FSRequiredUI', N'Please Fill the required fields')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1111, N'FeeSetting', N'FSSuccess', N'Saved Successfully')
GO
SET IDENTITY_INSERT [dbo].[Messages] OFF
GO





IF NOT EXISTS (SELECT * FROM fee_structure WHERE FS_Type = 'FEE')
	INSERT INTO fee_structure (FS_Type,FS_Amount,FS_Percentage,FS_Apply)VALUES('FEE',0.99,5,'N')

	

IF NOT EXISTS (SELECT * FROM Permission_Detail WHERE Permission_Desc = 'EventType')
	INSERT INTO Permission_Detail VALUES('EventType','CMS')

IF NOT EXISTS (SELECT * FROM Permission_Detail WHERE Permission_Desc = 'EventCategory')
	INSERT INTO Permission_Detail VALUES('EventCategory','CMS')

IF NOT EXISTS (SELECT * FROM Permission_Detail WHERE Permission_Desc = 'Users')
	INSERT INTO Permission_Detail VALUES('Users','CMS')


	