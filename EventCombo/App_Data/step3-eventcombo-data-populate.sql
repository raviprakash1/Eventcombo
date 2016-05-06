--select * from Event

/*select * from Aspnetusers
where id='0000163a-48d1-4688-b150-a450f65100b5'
*/




INSERT EC2.dbo.AspNetRoles ([Id], [Name]) VALUES (N'1', N'Super Admin')
INSERT EC2.dbo.AspNetRoles ([Id], [Name]) VALUES (N'2', N'Admin')
INSERT EC2.dbo.AspNetRoles ([Id], [Name]) VALUES (N'3', N'Member')
GO

--select * from AspNetUserRoles


INSERT INTO EC2.dbo.AspNetUserRoles (UserId, RoleId)
select id,3 from EC2.dbo.AspNetUsers 


USE EC2
GO
SET IDENTITY_INSERT EC2.dbo.Messages ON 

GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1, N'MyAccount', N'MyAccountEmailRequiredUI', N'Email Required.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (2, N'MyAccount', N'MyAccountEmailValidationUI', N'Invalid Email .')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (4, N'MyAccount', N'MyAccountSuccessInitSY', N'Your details has been saved.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (5, N'MyAccount', N'MyAccountSuccessEmailSY', N'Your email has been saved. ')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (6, N'MyAccount', N'MyAccountSuccessPasswordSY', N'Your Password has been saved. ')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (7, N'Login', N'LoginEmailValidationUI', N'Invalid Email .')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (8, N'Login', N'LoginPasswordValidationUI', N'Invalid Password.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (9, N'Login', N'LoginEmailRequiredUI', N'Email  Required.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (10, N'Login', N'LoginEmailNotExistSy', N'Email  not present in database.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (11, N'Signup', N'SignupEmailValidationUI', N'Invalid Email .')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (12, N'Signup', N'SignupPasswordValidationUI', N'Invalid Password.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (13, N'Signup', N'SignupEmailAlreadyExistSY', N'Email Already Exist.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1011, N'TicketPayment', N'TenMinWindowExpires', N'Sorry, you took too long and we have to release your order. Please try again.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1012, N'Login', N'LoginPwdNotExistSy', N'Password not matching!Use forgot pasword!')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1013, N'MyAccount', N'MyAccountFnameRequiredUI', N'Please Enter First name.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1015, N'MyAccount', N'MyAccountPasswordValidationUI', N'Invalid Password.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1018, N'MyAccount', N'MyAccountDateValidationUI', N'Invalid Date.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1019, N'MyAccount', N'MyAccountZipValidationUI', N'Invalid Zip Code.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1020, N'MyAccount', N'MyAccountImagesizeValidationUI', N'Image More than 2 MB not allowed.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1021, N'MyAccount', N'MyAccountImageCountValidationUI', N'Only 1 image allowed.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1022, N'MyAccount', N'MyAccountEmailmatchValidationSy', N'Email and email verification doesn''t match.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1023, N'MyAccount', N'MyAccountPwdmatchValidationSy', N'New password and confirm new password doesn''t match.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1024, N'MyAccount', N'MyAccountPwdValidationSys', N'Invalid current password.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1025, N'MyAccount', N'MyAccountEmailAlreadyExistSY', N'Email already exist in our system choose different email')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1026, N'ForgotPassword', N'FrgtpwdEmailValidationUI', N'Invalid Email .')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1027, N'ForgotPassword', N'FrgtpwdEmailValidationSy', N'Email Not Found in our Database')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1028, N'ForgotPassword', N'ForgotPwdSuccessInitSY', N'Please check your email for password set link')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1029, N'ResetPassword', N'PwdResetPwdValidationUI', N'Invalid Password.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1030, N'ResetPassword', N'PwdResetPwdValidationSys', N'Password and confirm password doesn''t match')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1031, N'ResetPassword', N'PwdResetSuccessInitSY', N'Password reset successfully.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1032, N'CreateEvent', N'CreateEventTitileUI', N'Please enter the name of event.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1033, N'CreateEvent', N'CreateEventOrganizerUI', N'Please enter organizer name.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1034, N'CreateEvent', N'CreateEventtypeUI', N'Please select type of event.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1035, N'CreateEvent', N'CreateEventCategoryUI', N'Please select category of event')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1036, N'CreateEvent', N'CreateEventHighlightFieldsUI', N'Please fix the highlighted fields')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1037, N'CreateEvent', N'CreateEventvariabledescUI', N'Please enter description for variable charges')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1038, N'CreateEvent', N'CreateEventPwdUI', N'Please enter valid password of min. length 6 for view event')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1039, N'CreateEvent', N'CreateEventCompareDateUI', N'Start Date can not be greater than End Date')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1040, N'CreateEvent', N'CreateEventInvalidTimeUI', N'Invalid time format')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1041, N'CreateEvent', N'CreateEventInvalidDateUI', N'Invalid date format')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1042, N'CreateEvent', N'CreateEventValidatevenueUI', N'Please enter valid venue name')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1043, N'CreateEvent', N'CreateEventurlexistUI', N'Event Url already exists.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1044, N'Messages', N'MessageReqUI', N'All Fields are required')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1045, N'Messages', N'MessageSuccSY', N'All Messages are saved successfully')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1046, N'MyAccount', N'MyAccountImagestypeValidationUI', N'Only files with extension jpg,.png,.jpeg are allowed to be uploaded.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1047, N'CreateEvent', N'CreateEventImageTypeUI', N'Only files with extension jpg,.png,.jpeg are allowed to be uploaded.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1048, N'CreateEvent', N'CreateEventImageSizeUI', N'Image More than 2 MB not allowed.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1049, N'CreateEvent', N'CreateEventImagecountUI', N'Only 5 image allowed.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1050, N'CreateEvent', N'CreateEventOrganizercountUI', N'Limit Exceeded, System Allow Only 25 Orgnizer.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1051, N'CreateEvent', N'CreateEventProbleminApllUI', N'Sorry there is some problem.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1052, N'CreateEvent', N'CreateEventEnternumberUI', N'Please enter number')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1053, N'CreateEvent', N'CreateEventDeleteUI', N'Are you sure you want to delete it?')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1054, N'CMSUsers', N'UsersOrgMerStatusSucc', N'Record updated Successfully.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1055, N'CreateEvent', N'CreateEventsavedsucc', N'Congratulations, your event is on Eventcombo!')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1056, N'CMSUsers', N'UsersDeleteUI', N'Are you sure you want to delete it ?')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1058, N'EventConfirmation', N'Evtconfirmsucc', N'Congratulation Your Event is Live now')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1059, N'ViewEvent', N'ViewEventExpiredSy', N'Your Event has Ended')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1060, N'ViewEvent', N'ViewEventNoQtyUI', N'Please Select Atleast one qty')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1061, N'TicketPayment', N'TPayValidateEmailUI', N'Invalid Email')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1062, N'TicketPayment', N'TPayValidateConfirmMatchEmailUI', N'Confirm Email Doesnot match Email')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1063, N'TicketPayment', N'TPayValidateconfirmPwdMatchUI', N'Confirm Password Doesnot match Password')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1064, N'TicketPayment', N'TPayCardValidationUI', N'Invalid Card Number')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1065, N'TicketPayment', N'TPayValidateBillingAddressUI', N'Please Fill the Billing Address Details')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1070, N'TicketPayment', N'TPayValidateShippingAddressUI', N'Please Fill the Shipping Address Details')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1071, N'TicketPayment', N'TPayValidateEmailSy', N'Email already exist,Please use Sign in')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1072, N'TicketPayment', N'TPayValidatebearernameUI', N'Please enter valid Name')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1073, N'TicketPayment', N'TPayValidatebeareremailUI', N'Please enter valid Email')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1074, N'TicketPayment', N'TPayValidatePwdUI', N'Invalid Password')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1075, N'TicketPayment', N'TPayFnameValidateUI', N'Please Enter Name')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1076, N'CreateEvent', N'CreateEventQtyValidateUI', N'Ticket Qty cannot be less than equal to zero')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1077, N'Email', N'EmailSuccessSy', N'Template Saved Successfully')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1080, N'ViewEvent', N'OverSelling', N'One of your selected ticket has been already sold.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1081, N'ViewEvent', N'EventNotLive', N'You cannot purchase ticket as this event is not live.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1082, N'ViewEvent', N'VoteOnce', N'You can only vote once.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1083, N'ViewEvent', N'ThanksForVote', N'Thank you for voting!')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1084, N'ViewEvent', N'eventSaved', N'Your Event have been saved!')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1085, N'ViewEvent', N'eventnotsaved', N'Your Event have not been saved!')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1086, N'CreateEvent', N'CreateEventSubDescUI', N'Please Enter Variable Sub Description')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1087, N'CreateEvent', N'CreateEventSubQtyUI', N'Variable Sub qty can''t be zero')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1088, N'CMSUsers', N'NoEvents', N'User has not created any event')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1089, N'CreateEvent', N'PriceNotzeroUI', N'Invalid Price')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1090, N'CreateEvent', N'NoTicketUI', N'Select atleast one ticket')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1091, N'CreateEvent', N'Validdate', N'Please add valid date and time')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1092, N'EventType', N'EventtypeDeleteUI', N'Are you sure you want to delete it ?')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1093, N'EventCatogary', N'EventCatDeleteUi', N'Are you sure you want to delete it ?')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1094, N'CreateEvent', N'CreateeventUpdated', N'Your Event is successfully updated!!')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1095, N'MyAccount', N'AccEmailPwdchangesys', N'Your Email and Password has been saved. ')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1096, N'TicketPayment', N'TpayMaxTicketReciepts', N'Maximum limit to add ticket recepients:')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1097, N'ManageEvent', N'MEPublisheventSucc', N'Congratulations! Your event is live now.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1098, N'ManageEvent', N'MEDeleteEvent', N'Are you sure you want to delete this event? This cannot be undone')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1099, N'ManageEvent', N'MEunPublish', N'Are you sure you want to unpublish event?')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1100, N'ManageEvent', N'MECancel', N'Are you sure you want to cancel the event?')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1101, N'ManageEvent', N'MEnterUrlUI', N'Url cannot be empty!!')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1102, N'PaymentInfo', N'PICompanynameUI', N'Please Enter Company name')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1103, N'PaymentInfo', N'PInameUI', N'Please Enter Name')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1104, N'PaymentInfo', N'PIAddressUI', N'Please Enter Address Details')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1105, N'PaymentInfo', N'PIBankNameUI', N'Please enter bank name')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1106, N'PaymentInfo', N'PIRoutingnoUI', N'Please enter routing number')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1107, N'PaymentInfo', N'PIAccountinfoUI', N'Please Enter Account info')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1108, N'PaymentInfo', N'PIPayeeUI', N'Please Enter Payee name')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1109, N'PaymentInfo', N'PIAccountinfocompUI', N'Account and Re account number not same')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1110, N'FeeSetting', N'FSRequiredUI', N'Please Fill the required fields')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1111, N'FeeSetting', N'FSSuccess', N'Saved Successfully')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1113, N'OrganizerMaster', N'OrgNameUi', N'Please enter name')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1114, N'OrganizerMaster', N'OrgEmailUi', N'Please enter email')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1115, N'OrganizerMaster', N'OrgvalidEmailUi', N'Please enter valid email')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1116, N'OrganizerMaster', N'OrgvalidZipUi', N'Please enter valid zip/pin code')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1117, N'OrganizerMaster', N'OrgsaveSucc', N'Congratulation!!Organizer saved successfully')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1118, N'OrganizerMaster', N'OrgsaveEdit', N'Congratulation!!Organizer updated successfully')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1119, N'OrganizerMaster', N'OrgDelete', N'Organiser deleted successfully')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1120, N'OrganizerMaster', N'OrgDeleteMessage', N'Do you want to delete the organizer!')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1121, N'OrganizerMaster', N'OrgImagesizeValidationUI', N'Image More than 2 MB not allowed.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1122, N'OrganizerMaster', N'OrgImageCountValidationUI', N'Only 1 image allowed!!!!!')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1123, N'OrganizerMaster', N'OrgImagestypeValidationUI', N'Only files with extension jpg,.png,.jpeg are allowed to be uploaded.')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1124, N'PromotionalCode', N'PCInavlidfile', N'Invalid file format!!')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1125, N'PromotionalCode', N'PCselectcheckbox', N'Please select atleast one ticket type')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1126, N'PromotionalCode', N'PCValidpccode', N'Please Enter a valid Promotional  Code')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1127, N'PromotionalCode', N'PCValidpccodefile', N'Please upload valid txt or csv file')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1128, N'PromotionalCode', N'PCValidamount', N'Please Enter either a amount or percentage  off the ticket price')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1129, N'PromotionalCode', N'PCDeleetmsg', N'Are You sure you want to delete this code')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1130, N'PromotionalCode', N'PCvaiddate', N'Start date cannot be greater than end date')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1131, N'CreateEvent', N'CreateEventQtyPurchaseValidateUI', N'Ticket Qty cannot be less than equal to Purchased Qty')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1132, N'CreateInvitations', N'EmailValidationUI', N'Please Enter Valid Email Address')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1133, N'CreateInvitations', N'DeleteConfirmUI', N'Are you sure ?')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1134, N'CreateInvitations', N'SenderNameValidateUI', N'Please enter valid sender name')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1135, N'CreateInvitations', N'SubjectValidateUI', N'Please enter valid subject')
GO
INSERT EC2.dbo.Messages ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1136, N'ViewEvent', N'VEPasswordIncorrect', N'You have entered incorrect password')
GO
SET IDENTITY_INSERT EC2.dbo.Messages OFF
GO
----------------------

SET IDENTITY_INSERT EC2.dbo.Email_Tag ON 

GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (1, N'UserEmailID')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (2, N'UserFirstNameID')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (3, N'UserLastNameID')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (4, N'EventNameID')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (5, N'EventStartDateID')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (6, N'EventEndDateID')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (7, N'EventStartTimeID')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (8, N'EventEndTimeID')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (9, N'EventVenueID')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (10, N'EventAddressID')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (11, N'TicketOrderNumberID')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (12, N'DealOrderNumberID')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (13, N'ResetPwdUrl')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (14, N'EventOrderNO')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (15, N'EventBarcodeId')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (16, N'EventTitleId')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (17, N'EventImageId')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (18, N'Tickettype')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (19, N'TicketPrice')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (20, N'TicketOrderDateId')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (21, N'EventTimeZone')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (22, N'Ticketname')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (23, N'EventQrCode')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (24, N'EventOrganiserName')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (25, N'EventOrganiserEmail')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (26, N'EventImage')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (27, N'EventDescription')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (28, N'EventLogo')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (29, N'EventdayId')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (30, N'Eventtype')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (31, N'TicketQty')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (32, N'CreateEventurl')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (33, N'DiscoverEventurl')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (34, N'MyTicketEventurl')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (35, N'EventDynamicTable')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (36, N'EventMapImage')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (37, N'EventLogin')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (38, N'Downloadurl')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (39, N'OrderDetail')
GO
INSERT EC2.dbo.Email_Tag ([Tag_Id], [Tag_Name]) VALUES (40, N'Eventtypedetail')
GO
SET IDENTITY_INSERT EC2.dbo.Email_Tag OFF
GO
INSERT EC2.dbo.Email_Template ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag], [From_Name]) VALUES (N'2ab53970-a668-4c07-ac73-3133a5447150', N'E-Ticket Template', N' ¶¶UserEmailID¶¶', NULL, NULL, N'Your order for  ¶¶EventTitleId¶¶-  ¶¶EventOrderNO¶¶', N'<table border="0" cellpadding="0" cellspacing="0" style="width:100%">
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
INSERT EC2.dbo.Email_Template ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag], [From_Name]) VALUES (N'2e223022-9c7e-45e0-8e5b-6c8776762021', N'Welcome Template', N' ¶¶UserEmailID¶¶ ', N'just.shweta29@gmail.com, ¶¶UserEmailID¶¶', N'navrit.singh@kiwitech.com, ¶¶UserEmailID¶¶', N'Welcome to Eventcombo   ¶¶UserEmailID¶¶, ¶¶DealOrderNumberID¶¶', N'<p>Hi ,</p>

<p>Thank you &para;&para;Ticketname&para;&para; for choosing eventcombo.Lets get started. &para;&para;EventStartDateID&para;&para;&nbsp;,,,&para;&para;EventNameID&para;&para;</p>

<p>&nbsp;</p>
', N'welcome@eventcombo.com', N'email_welcome', N'EventCombo')
GO
INSERT EC2.dbo.Email_Template ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag], [From_Name]) VALUES (N'405f9cc8-782b-4b87-8406-3a3c0ae4c60d', N'Lost password Template', N' ¶¶UserEmailID¶¶', N'just.shweta29@gmail.com', NULL, N'Reset Password ', N'<p><img alt="eventcombo" src="http://eventcombo.kiwireader.com/Images/logo_vertical.png" style="float:left; height:400px; margin:2px; width:86px" />&nbsp;Hello&nbsp; &para;&para;UserFirstNameID&para;&para; ,</p>

<p>We have recieved a request to reset your password for your account :&nbsp;&para;&para;UserEmailID&para;&para; .We&#39;re here to help!</p>

<p>Click on the link below to reset and create a new password:</p>

<p>&para;&para;ResetPwdUrl&para;&para;</p>

<p>Set a new Password if you didn&#39;t ask to change your password,don&#39;t worry!Your password is still safe and you can delete this email.</p>

<p>Best ,</p>

<p>The Eventcombo Team.</p>

<p>&nbsp;</p>
', N'shweta.sindhu@kiwitech.com', N'email_lost_pwd', N'EventCombo')
GO
INSERT EC2.dbo.Email_Template ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag], [From_Name]) VALUES (N'aa3f1945-418b-4fc8-8a6d-6c60741fa70a', N'Account Info Update Template', N' ¶¶UserEmailID¶¶', NULL, NULL, N'Your Account Info is successfuly updated', N'<p>Hi&nbsp; &para;&para;UserFirstNameID&para;&para;,</p>

<p>You Account info is updated successfully.</p>
', N'shweta.sindhu@kiwitech.com', N'acc_info_update', N'EventCombo')
GO
INSERT EC2.dbo.Email_Template ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag], [From_Name]) VALUES (N'edfb6c74-43f8-45e7-86d1-d19aaf7f5e37', N'', N' ¶¶UserEmailID¶¶', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT EC2.dbo.Email_Template ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag], [From_Name]) VALUES (N'fd04710c-8d72-43e5-ab83-c33da92adc15', N'Account Password Set Template', N' ¶¶UserEmailID¶¶', NULL, NULL, N'Your Password has Reset', N'<p>Hi&nbsp; &para;&para;UserFirstNameID&para;&para; ,</p>

<p>Your password has been successfully reset.</p>

<p>&nbsp;</p>

<p>&nbsp;</p>

<p>With Best Wishes,</p>

<p>The Eventcombo Team.</p>
', N'shweta.sindhu@kiwitech.com', N'acc_pwd_set', N'Eventcombo')
GO



SET IDENTITY_INSERT EC2.dbo.TimeZoneDetail ON 

GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+04:30) Kabul', N'Afghanistan Standard Time', 1, 55)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-09:00) Alaska', N'Alaskan Standard Time', 2, 9)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+03:00) Kuwait, Riyadh', N'Arab Standard Time', 3, 50)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+04:00) Abu Dhabi, Muscat', N'Arabian Standard Time', 4, 56)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+03:00) Baghdad', N'Arabic Standard Time', 5, 51)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-03:00) Buenos Aires', N'Argentina Standard Time', 6, 23)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-04:00) Atlantic Time (Canada)', N'Atlantic Standard Time', 7, 1)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+09:30) Darwin', N'AUS Central Standard Time', 8, 82)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+10:00) Canberra, Melbourne, Sydney', N'AUS Eastern Standard Time', 9, 87)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+04:00) Baku', N'Azerbaijan Standard Time', 10, 57)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-01:00) Azores', N'Azores Standard Time', 11, 30)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+06:00) Dhaka', N'Bangladesh Standard Time', 12, 67)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-06:00) Saskatchewan', N'Canada Central Standard Time', 13, 13)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-01:00) Cape Verde Is.', N'Cape Verde Standard Time', 14, 30)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+04:00) Yerevan', N'Caucasus Standard Time', 15, 58)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+09:30) Adelaide', N'Cen. Australia Standard Time', 16, 83)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-06:00) Central America', N'Central America Standard Time', 17, 14)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+06:00) Astana', N'Central Asia Standard Time', 18, 68)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-04:00) Cuiaba', N'Central Brazilian Standard Time', 19, 18)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague', N'Central Europe Standard Time', 20, 35)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+01:00) Sarajevo, Skopje, Warsaw, Zagreb', N'Central European Standard Time', 21, 36)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+11:00) Solomon Is., New Caledonia', N'Central Pacific Standard Time', 22, 90)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-06:00) Central Time (US & Canada)', N'Central Standard Time', 23, 2)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-06:00) Guadalajara, Mexico City, Monterrey', N'Central Standard Time (Mexico)', 24, 15)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+08:00) Beijing, Chongqing, Hong Kong, Urumqi', N'China Standard Time', 25, 73)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-12:00) International Date Line West', N'Dateline Standard Time', 26, 5)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+03:00) Nairobi', N'E. Africa Standard Time', 27, 52)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+10:00) Brisbane', N'E. Australia Standard Time', 28, 88)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+02:00) Minsk', N'E. Europe Standard Time', 29, 41)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-03:00) Brasilia', N'E. South America Standard Time', 30, 24)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-05:00) Eastern Time (US & Canada)', N'Eastern Standard Time', 31, 0)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+02:00) Cairo', N'Egypt Standard Time', 32, 42)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+05:00) Ekaterinburg', N'Ekaterinburg Standard Time', 33, 61)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+12:00) Fiji', N'Fiji Standard Time', 34, 91)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+02:00) Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius', N'FLE Standard Time', 35, 43)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+04:00) Tbilisi', N'Georgian Standard Time', 36, 59)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT) Dublin, Edinburgh, Lisbon, London', N'GMT Standard Time', 37, 31)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-03:00) Greenland', N'Greenland Standard Time', 38, 25)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT) Monrovia, Reykjavik', N'Greenwich Standard Time', 39, 32)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+02:00) Athens, Bucharest, Istanbul', N'GTB Standard Time', 40, 44)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-10:00) Hawaii', N'Hawaiian Standard Time', 41, 8)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+05:30) Chennai, Kolkata, Mumbai, New Delhi', N'India Standard Time', 42, 64)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+03:30) Tehran', N'Iran Standard Time', 43, 53)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+02:00) Jerusalem', N'Israel Standard Time', 44, 45)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+02:00) Amman', N'Jordan Standard Time', 45, 46)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+12:00) Petropavlovsk-Kamchatsky - Old', N'Kamchatka Standard Time', 46, 92)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+09:00) Seoul', N'Korea Standard Time', 47, 79)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+11:00) Magadan', N'Magadan Standard Time', 48, 89)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+04:00) Port Louis', N'Mauritius Standard Time', 49, 60)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-02:00) Mid-Atlantic', N'Mid-Atlantic Standard Time', 50, 29)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+02:00) Beirut', N'Middle East Standard Time', 51, 47)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-03:00) Montevideo', N'Montevideo Standard Time', 52, 26)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT) Casablanca', N'Morocco Standard Time', 53, 33)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-07:00) Mountain Time (US & Canada)', N'Mountain Standard Time', 54, 3)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-07:00) Chihuahua, La Paz, Mazatlan', N'Mountain Standard Time (Mexico)', 55, 11)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+06:30) Yangon (Rangoon)', N'Myanmar Standard Time', 56, 69)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+06:00) Novosibirsk', N'N. Central Asia Standard Time', 57, 70)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+01:00) Windhoek', N'Namibia Standard Time', 58, 37)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+05:45) Kathmandu', N'Nepal Standard Time', 59, 66)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+12:00) Auckland, Wellington', N'New Zealand Standard Time', 60, 93)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-03:30) Newfoundland', N'Newfoundland Standard Time', 61, 27)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+08:00) Irkutsk', N'North Asia East Standard Time', 62, 74)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+07:00) Krasnoyarsk', N'North Asia Standard Time', 63, 71)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-04:00) Santiago', N'Pacific SA Standard Time', 64, 19)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-08:00) Pacific Time (US & Canada)', N'Pacific Standard Time', 65, 4)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-08:00) Baja California', N'Pacific Standard Time (Mexico)', 66, 10)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+05:00) Islamabad, Karachi', N'Pakistan Standard Time', 67, 62)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-04:00) Asuncion', N'Paraguay Standard Time', 68, 20)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+01:00) Brussels, Copenhagen, Madrid, Paris', N'Romance Standard Time', 69, 38)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+03:00) Moscow, St. Petersburg, Volgograd', N'Russian Standard Time', 70, 54)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-03:00) Cayenne, Fortaleza', N'SA Eastern Standard Time', 71, 28)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-05:00) Bogota, Lima, Quito', N'SA Pacific Standard Time', 72, 16)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-04:00) Georgetown, La Paz, Manaus, San Juan', N'SA Western Standard Time', 73, 21)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-11:00) Samoa', N'Samoa Standard Time', 74, 6)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+07:00) Bangkok, Hanoi, Jakarta', N'SE Asia Standard Time', 75, 72)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+08:00) Kuala Lumpur, Singapore', N'Singapore Standard Time', 76, 75)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+02:00) Harare, Pretoria', N'South Africa Standard Time', 77, 48)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+05:30) Sri Jayawardenepura', N'Sri Lanka Standard Time', 78, 65)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+02:00) Damascus', N'Syria Standard Time', 79, 49)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+08:00) Taipei', N'Taipei Standard Time', 80, 76)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+10:00) Hobart', N'Tasmania Standard Time', 81, 84)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+09:00) Osaka, Sapporo, Tokyo', N'Tokyo Standard Time', 82, 80)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+13:00) Nuku''alofa', N'Tonga Standard Time', 83, 95)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+08:00) Ulaanbaatar', N'Ulaanbaatar Standard Time', 84, 77)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-05:00) Indiana (East)', N'US Eastern Standard Time', 85, 17)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-07:00) Arizona', N'US Mountain Standard Time', 86, 12)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT) Coordinated Universal Time', N'UTC', 87, 34)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+12:00) Coordinated Universal Time+12', N'UTC+12', 88, 94)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-02:00) Coordinated Universal Time-02', N'UTC-02', 89, 29)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-11:00) Coordinated Universal Time-11', N'UTC-11', 90, 7)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT-04:30) Caracas', N'Venezuela Standard Time', 91, 22)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+10:00) Vladivostok', N'Vladivostok Standard Time', 92, 85)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+08:00) Perth', N'W. Australia Standard Time', 93, 78)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+01:00) West Central Africa', N'W. Central Africa Standard Time', 94, 39)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna', N'W. Europe Standard Time', 95, 40)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+05:00) Tashkent', N'West Asia Standard Time', 96, 63)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+10:00) Guam, Port Moresby', N'West Pacific Standard Time', 97, 86)
GO
INSERT EC2.dbo.TimeZoneDetail ([TimeZone_Name], [TimeZone], [TimeZone_Id], [Timezone_order]) VALUES (N'(GMT+09:00) Yakutsk', N'Yakutsk Standard Time', 98, 81)
GO
SET IDENTITY_INSERT EC2.dbo.TimeZoneDetail OFF
GO
