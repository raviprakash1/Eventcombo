

/****** Object:  StoredProcedure [dbo].[PublishEvent]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PublishEvent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PublishEvent]
GO
/****** Object:  StoredProcedure [dbo].[GetTicketListing]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTicketListing]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetTicketListing]
GO
/****** Object:  StoredProcedure [dbo].[GetSetUserRole]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSetUserRole]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetSetUserRole]
GO
/****** Object:  StoredProcedure [dbo].[GetSelectedTicketListing]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSelectedTicketListing]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetSelectedTicketListing]
GO
/****** Object:  StoredProcedure [dbo].[GetOrganizerEventid]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetOrganizerEventid]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetOrganizerEventid]
GO
/****** Object:  StoredProcedure [dbo].[GetLantLong]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetLantLong]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetLantLong]
GO
/****** Object:  StoredProcedure [dbo].[GetEventsListByStatus]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetEventsListByStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetEventsListByStatus]
GO
/****** Object:  StoredProcedure [dbo].[GetEventListing]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetEventListing]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetEventListing]
GO
/****** Object:  StoredProcedure [dbo].[GetEventDateList]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetEventDateList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetEventDateList]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__TicketDel__Deliv__461E4E5C]') AND parent_object_id = OBJECT_ID(N'[dbo].[TicketDeliveryMethod]'))
ALTER TABLE [dbo].[TicketDeliveryMethod] DROP CONSTRAINT [FK__TicketDel__Deliv__461E4E5C]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Ticket_Quantity_Info_Ticket]') AND parent_object_id = OBJECT_ID(N'[dbo].[Ticket_Quantity_Detail]'))
ALTER TABLE [dbo].[Ticket_Quantity_Detail] DROP CONSTRAINT [FK_Ticket_Quantity_Info_Ticket]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Ticket_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Ticket]'))
ALTER TABLE [dbo].[Ticket] DROP CONSTRAINT [FK_Ticket_Event]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Profile_Profile]') AND parent_object_id = OBJECT_ID(N'[dbo].[Profile]'))
ALTER TABLE [dbo].[Profile] DROP CONSTRAINT [FK_Profile_Profile]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MultipleEvent_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[MultipleEvent]'))
ALTER TABLE [dbo].[MultipleEvent] DROP CONSTRAINT [FK_MultipleEvent_Event]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EventVenue_EventVenue]') AND parent_object_id = OBJECT_ID(N'[dbo].[EventVenue]'))
ALTER TABLE [dbo].[EventVenue] DROP CONSTRAINT [FK_EventVenue_EventVenue]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EventVenue_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[EventVenue]'))
ALTER TABLE [dbo].[EventVenue] DROP CONSTRAINT [FK_EventVenue_Event]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__EventSubC__Event__3F7150CD]') AND parent_object_id = OBJECT_ID(N'[dbo].[EventSubCategory]'))
ALTER TABLE [dbo].[EventSubCategory] DROP CONSTRAINT [FK__EventSubC__Event__3F7150CD]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EventImage_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[EventImage]'))
ALTER TABLE [dbo].[EventImage] DROP CONSTRAINT [FK_EventImage_Event]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_VariableDesc_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event_VariableDesc]'))
ALTER TABLE [dbo].[Event_VariableDesc] DROP CONSTRAINT [FK_Event_VariableDesc_Event]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_Orgnizer_Detail_Organizer_Master]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event_Orgnizer_Detail]'))
ALTER TABLE [dbo].[Event_Orgnizer_Detail] DROP CONSTRAINT [FK_Event_Orgnizer_Detail_Organizer_Master]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_Orgnizer_Detail_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event_Orgnizer_Detail]'))
ALTER TABLE [dbo].[Event_Orgnizer_Detail] DROP CONSTRAINT [FK_Event_Orgnizer_Detail_Event]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]'))
ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]'))
ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Address_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Address]'))
ALTER TABLE [dbo].[Address] DROP CONSTRAINT [FK_Address_Event]
GO
/****** Object:  Table [dbo].[Venue]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Venue]') AND type in (N'U'))
DROP TABLE [dbo].[Venue]
GO
/****** Object:  Table [dbo].[User_Permission_Detail]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User_Permission_Detail]') AND type in (N'U'))
DROP TABLE [dbo].[User_Permission_Detail]
GO
/****** Object:  Table [dbo].[TimeZoneDetail1]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TimeZoneDetail1]') AND type in (N'U'))
DROP TABLE [dbo].[TimeZoneDetail1]
GO
/****** Object:  Table [dbo].[TimeZoneDetail]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TimeZoneDetail]') AND type in (N'U'))
DROP TABLE [dbo].[TimeZoneDetail]
GO
/****** Object:  Table [dbo].[TicketType]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TicketType]') AND type in (N'U'))
DROP TABLE [dbo].[TicketType]
GO
/****** Object:  Table [dbo].[TicketOrderDetail]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TicketOrderDetail]') AND type in (N'U'))
DROP TABLE [dbo].[TicketOrderDetail]
GO
/****** Object:  Table [dbo].[TicketDeliveryMethod]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TicketDeliveryMethod]') AND type in (N'U'))
DROP TABLE [dbo].[TicketDeliveryMethod]
GO
/****** Object:  Table [dbo].[TicketBearer]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TicketBearer]') AND type in (N'U'))
DROP TABLE [dbo].[TicketBearer]
GO
/****** Object:  Table [dbo].[Ticket_Quantity_Detail]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ticket_Quantity_Detail]') AND type in (N'U'))
DROP TABLE [dbo].[Ticket_Quantity_Detail]
GO
/****** Object:  Table [dbo].[Ticket_Purchased_Detail]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ticket_Purchased_Detail]') AND type in (N'U'))
DROP TABLE [dbo].[Ticket_Purchased_Detail]
GO
/****** Object:  Table [dbo].[Ticket_Locked_Detail]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ticket_Locked_Detail]') AND type in (N'U'))
DROP TABLE [dbo].[Ticket_Locked_Detail]
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ticket]') AND type in (N'U'))
DROP TABLE [dbo].[Ticket]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Status]') AND type in (N'U'))
DROP TABLE [dbo].[Status]
GO
/****** Object:  Table [dbo].[ShippingAddress]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ShippingAddress]') AND type in (N'U'))
DROP TABLE [dbo].[ShippingAddress]
GO
/****** Object:  Table [dbo].[Publish_Event_Detail]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Publish_Event_Detail]') AND type in (N'U'))
DROP TABLE [dbo].[Publish_Event_Detail]
GO
/****** Object:  Table [dbo].[Profile]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Profile]') AND type in (N'U'))
DROP TABLE [dbo].[Profile]
GO
/****** Object:  Table [dbo].[Permission_Detail]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Permission_Detail]') AND type in (N'U'))
DROP TABLE [dbo].[Permission_Detail]
GO
/****** Object:  Table [dbo].[Payment_Info]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Payment_Info]') AND type in (N'U'))
DROP TABLE [dbo].[Payment_Info]
GO
/****** Object:  Table [dbo].[Organizer_Master]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Organizer_Master]') AND type in (N'U'))
DROP TABLE [dbo].[Organizer_Master]
GO
/****** Object:  Table [dbo].[Order_Detail_T]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Order_Detail_T]') AND type in (N'U'))
DROP TABLE [dbo].[Order_Detail_T]
GO
/****** Object:  Table [dbo].[MultipleEvent]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MultipleEvent]') AND type in (N'U'))
DROP TABLE [dbo].[MultipleEvent]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Messages]') AND type in (N'U'))
DROP TABLE [dbo].[Messages]
GO
/****** Object:  Table [dbo].[Fee_Structure]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Fee_Structure]') AND type in (N'U'))
DROP TABLE [dbo].[Fee_Structure]
GO
/****** Object:  Table [dbo].[EventVote]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventVote]') AND type in (N'U'))
DROP TABLE [dbo].[EventVote]
GO
/****** Object:  Table [dbo].[EventVenue]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventVenue]') AND type in (N'U'))
DROP TABLE [dbo].[EventVenue]
GO
/****** Object:  Table [dbo].[EventType]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventType]') AND type in (N'U'))
DROP TABLE [dbo].[EventType]
GO
/****** Object:  Table [dbo].[EventTempImage]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventTempImage]') AND type in (N'U'))
DROP TABLE [dbo].[EventTempImage]
GO
/****** Object:  Table [dbo].[EventSubCategory]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventSubCategory]') AND type in (N'U'))
DROP TABLE [dbo].[EventSubCategory]
GO
/****** Object:  Table [dbo].[Events_Hit]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Events_Hit]') AND type in (N'U'))
DROP TABLE [dbo].[Events_Hit]
GO
/****** Object:  Table [dbo].[EventImage]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventImage]') AND type in (N'U'))
DROP TABLE [dbo].[EventImage]
GO
/****** Object:  Table [dbo].[EventFavourite]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventFavourite]') AND type in (N'U'))
DROP TABLE [dbo].[EventFavourite]
GO
/****** Object:  Table [dbo].[EventCategory]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventCategory]') AND type in (N'U'))
DROP TABLE [dbo].[EventCategory]
GO
/****** Object:  Table [dbo].[Event_VariableDesc]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event_VariableDesc]') AND type in (N'U'))
DROP TABLE [dbo].[Event_VariableDesc]
GO
/****** Object:  Table [dbo].[Event_Orgnizer_Detail]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event_Orgnizer_Detail]') AND type in (N'U'))
DROP TABLE [dbo].[Event_Orgnizer_Detail]
GO
/****** Object:  Table [dbo].[Event_OrganizerMessages]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event_OrganizerMessages]') AND type in (N'U'))
DROP TABLE [dbo].[Event_OrganizerMessages]
GO
/****** Object:  Table [dbo].[Event_Detail]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event_Detail]') AND type in (N'U'))
DROP TABLE [dbo].[Event_Detail]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event]') AND type in (N'U'))
DROP TABLE [dbo].[Event]
GO
/****** Object:  Table [dbo].[Email_Template]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Email_Template]') AND type in (N'U'))
DROP TABLE [dbo].[Email_Template]
GO
/****** Object:  Table [dbo].[Email_Tag]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Email_Tag]') AND type in (N'U'))
DROP TABLE [dbo].[Email_Tag]
GO
/****** Object:  Table [dbo].[DeliveryMethod]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeliveryMethod]') AND type in (N'U'))
DROP TABLE [dbo].[DeliveryMethod]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Country]') AND type in (N'U'))
DROP TABLE [dbo].[Country]
GO
/****** Object:  Table [dbo].[CardDetails]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CardDetails]') AND type in (N'U'))
DROP TABLE [dbo].[CardDetails]
GO
/****** Object:  Table [dbo].[BillingAddress]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BillingAddress]') AND type in (N'U'))
DROP TABLE [dbo].[BillingAddress]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUsers]') AND type in (N'U'))
DROP TABLE [dbo].[AspNetUsers]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND type in (N'U'))
DROP TABLE [dbo].[AspNetUserRoles]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]') AND type in (N'U'))
DROP TABLE [dbo].[AspNetUserLogins]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]') AND type in (N'U'))
DROP TABLE [dbo].[AspNetUserClaims]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoles]') AND type in (N'U'))
DROP TABLE [dbo].[AspNetRoles]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Address]') AND type in (N'U'))
DROP TABLE [dbo].[Address]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__MigrationHistory]') AND type in (N'U'))
DROP TABLE [dbo].[__MigrationHistory]
GO
/****** Object:  UserDefinedFunction [dbo].[ReturnDayOfWeek]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReturnDayOfWeek]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[ReturnDayOfWeek]
GO
/****** Object:  UserDefinedFunction [dbo].[func_Split]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[func_Split]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[func_Split]
GO
/****** Object:  UserDefinedFunction [dbo].[Distance]    Script Date: 3/15/2016 12:07:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Distance]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[Distance]
GO
/****** Object:  UserDefinedFunction [dbo].[Distance]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Distance]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
create function [dbo].[Distance]( @lat1 float , @long1 float , @lat2 float , @long2 float)
returns float

as

begin

declare @DegToRad as float
declare @Ans as float
declare @Miles as float

set @DegToRad = 57.29577951
set @Ans = 0
set @Miles = 0

if @lat1 is null or @lat1 = 0 or @long1 is null or @long1 = 0 or @lat2 is
null or @lat2 = 0 or @long2 is null or @long2 = 0

begin

return ( @Miles )

end

set @Ans = SIN(@lat1 / @DegToRad) * SIN(@lat2 / @DegToRad) + COS(@lat1 / @DegToRad ) * COS( @lat2 / @DegToRad ) * COS(ABS(@long2 - @long1 )/@DegToRad)

set @Miles = 3959 * ATAN(SQRT(1 - SQUARE(@Ans)) / @Ans)

set @Miles = CEILING(@Miles)

return ( @Miles )

end

--select dbo.distance(28.6139, 77.2090, 10.5200, 76.2100)
' 
END

GO
/****** Object:  UserDefinedFunction [dbo].[func_Split]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[func_Split]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[func_Split] 
    (   
    @DelimitedString    varchar(8000),
    @Delimiter              varchar(100) 
    )
RETURNS @tblArray TABLE
    (
    ElementID   int IDENTITY(1,1),  -- Array index
    Element     varchar(1000)               -- Array element contents
    )
AS
BEGIN

    -- Local Variable Declarations
    -- ---------------------------
    DECLARE @Index      smallint,
                    @Start      smallint,
                    @DelSize    smallint

    SET @DelSize = LEN(@Delimiter)

    -- Loop through source string and add elements to destination table array
    -- ----------------------------------------------------------------------
    WHILE LEN(@DelimitedString) > 0
    BEGIN

        SET @Index = CHARINDEX(@Delimiter, @DelimitedString)

        IF @Index = 0
            BEGIN

                INSERT INTO
                    @tblArray 
                    (Element)
                VALUES
                    (LTRIM(RTRIM(@DelimitedString)))

                BREAK
            END
        ELSE
            BEGIN

                INSERT INTO
                    @tblArray 
                    (Element)
                VALUES
                    (LTRIM(RTRIM(SUBSTRING(@DelimitedString, 1,@Index - 1))))

                SET @Start = @Index + @DelSize
                SET @DelimitedString = SUBSTRING(@DelimitedString, @Start , LEN(@DelimitedString) - @Start + 1)

            END
    END

    RETURN
END

' 
END

GO
/****** Object:  UserDefinedFunction [dbo].[ReturnDayOfWeek]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReturnDayOfWeek]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[ReturnDayOfWeek](@dtDate DATETIME)
RETURNS VARCHAR(10)
AS
BEGIN
DECLARE @rtDayofWeek VARCHAR(10)
SELECT @rtDayofWeek = CASE datename(dw,@dtDate)
WHEN ''Sunday'' THEN 7
WHEN ''Monday'' THEN 1
WHEN ''Tuesday'' THEN 2
WHEN ''Wednesday'' THEN 3
WHEN ''Thursday'' THEN 4
WHEN ''Friday'' THEN 5
WHEN ''Saturday'' THEN 6
END
RETURN (@rtDayofWeek)
END


' 
END

GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__MigrationHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Address]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Address]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Address](
	[AddressID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Address1] [varchar](200) NOT NULL,
	[Address2] [varchar](200) NOT NULL,
	[City] [varchar](200) NOT NULL,
	[State] [varchar](200) NOT NULL,
	[Zip] [varchar](10) NOT NULL,
	[CountryID] [tinyint] NOT NULL,
	[VenueName] [varchar](500) NULL,
	[UserId] [nvarchar](256) NULL,
	[EventId] [bigint] NULL,
	[ConsolidateAddress] [nvarchar](max) NULL,
	[Latitude] [varchar](50) NULL,
	[Longitude] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[AddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [varchar](150) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [varchar](150) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUsers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[LoginStatus] [varchar](1) NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BillingAddress]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BillingAddress]') AND type in (N'U'))
BEGIN
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
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CardDetails]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CardDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CardDetails](
	[CardId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [nchar](128) NULL,
	[OrderId] [varchar](1000) NULL,
	[Guid] [nchar](128) NULL,
	[CardNumber] [varchar](50) NULL,
	[ExpirationDate] [varchar](50) NULL,
	[Cvv] [varchar](50) NULL,
	[card_type] [varchar](150) NULL,
 CONSTRAINT [PK_CardDetails] PRIMARY KEY CLUSTERED 
(
	[CardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Country]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Country]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Country](
	[CountryID] [tinyint] IDENTITY(1,1) NOT NULL,
	[Country] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DeliveryMethod]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeliveryMethod]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DeliveryMethod](
	[DeliveryMethodID] [bigint] IDENTITY(1,1) NOT NULL,
	[DeliveryMethod] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DeliveryMethodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Email_Tag]    Script Date: 3/15/2016 12:07:25 PM ******/
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
/****** Object:  Table [dbo].[Email_Template]    Script Date: 3/15/2016 12:07:25 PM ******/
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
/****** Object:  Table [dbo].[Event]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Event](
	[EventID] [bigint] IDENTITY(1,1) NOT NULL,
	[EventTypeID] [bigint] NOT NULL,
	[EventCategoryID] [bigint] NOT NULL,
	[EventSubCategoryID] [bigint] NOT NULL,
	[UserID] [nvarchar](256) NULL,
	[EventTitle] [nvarchar](600) NULL,
	[DisplayStartTime] [varchar](1) NULL,
	[DisplayEndTime] [varchar](1) NULL,
	[DisplayTimeZone] [varchar](1) NULL,
	[EventDescription] [nvarchar](max) NULL,
	[EventPrivacy] [varchar](20) NULL,
	[Private_ShareOnFB] [varchar](1) NULL,
	[Private_GuestOnly] [varchar](1) NULL,
	[Private_Password] [varchar](50) NULL,
	[EventUrl] [nvarchar](2000) NULL,
	[PublishOnFB] [varchar](1) NULL,
	[EventStatus] [varchar](20) NULL,
	[AddedOn] [datetime] NULL,
	[UpdateOn] [datetime] NULL,
	[IsMultipleEvent] [varchar](1) NULL,
	[TimeZone] [nvarchar](1000) NULL,
	[FBUrl] [nvarchar](1000) NULL,
	[TwitterUrl] [nvarchar](1000) NULL,
	[AddressStatus] [varchar](100) NULL,
	[LastLocationAddress] [bigint] NULL,
	[EnableFBDiscussion] [varchar](1) NULL,
	[Feature] [int] NULL,
	[Ticket_DAdress] [varchar](1) NULL,
	[Ticket_showremain] [varchar](1) NULL,
	[Ticket_showvariable] [varchar](1) NULL,
	[Ticket_variabledesc] [varchar](256) NULL,
	[Ticket_variabletype] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifyDate] [datetime] NULL,
	[ShowMap] [varchar](1) NULL,
	[Parent_EventID] [bigint] NULL,
	[EventCancel] [varchar](2) NULL CONSTRAINT [DF_Event_EventCancel]  DEFAULT ('N'),
PRIMARY KEY CLUSTERED 
(
	[EventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Event_Detail]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event_Detail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Event_Detail](
	[E_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[E_Title] [nvarchar](520) NULL,
	[E_StartDate] [date] NULL,
	[E_StartTime] [time](7) NULL,
	[E_EndDate] [date] NULL,
	[E_EndTime] [time](7) NULL,
	[E_DisplayStartTime] [varchar](1) NULL,
	[E_DisplayEndTime] [varchar](1) NULL,
	[E_DisplayTimeZone] [varchar](1) NULL,
	[E_EventPrivacy] [varchar](10) NULL,
	[E_ShareOnFB] [varchar](1) NULL,
	[E_GuestOnly] [varchar](1) NULL,
	[E_Password] [nchar](10) NULL,
 CONSTRAINT [PK_Event_Detail] PRIMARY KEY CLUSTERED 
(
	[E_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Event_OrganizerMessages]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event_OrganizerMessages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Event_OrganizerMessages](
	[EventId] [bigint] NULL,
	[OrganizerId] [bigint] NULL,
	[Userid] [varchar](150) NULL,
	[Name] [varchar](256) NULL,
	[Email] [varchar](256) NULL,
	[Message] [nvarchar](max) NULL,
	[MessageId] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Event_OrganizerMessages] PRIMARY KEY CLUSTERED 
(
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Event_Orgnizer_Detail]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event_Orgnizer_Detail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Event_Orgnizer_Detail](
	[Orgnizer_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Orgnizer_Event_Id] [bigint] NULL,
	[UserId] [nvarchar](256) NULL,
	[DefaultOrg] [varchar](1) NULL,
	[OrganizerMaster_Id] [bigint] NOT NULL,
 CONSTRAINT [PK_Event_Orgnizer_Detail] PRIMARY KEY CLUSTERED 
(
	[Orgnizer_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Event_VariableDesc]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Event_VariableDesc]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Event_VariableDesc](
	[VariableDesc] [nvarchar](256) NULL,
	[Price] [numeric](18, 2) NULL,
	[Event_Id] [bigint] NULL,
	[Variable_Id] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Event_VariableDesc] PRIMARY KEY CLUSTERED 
(
	[Variable_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[EventCategory]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventCategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EventCategory](
	[EventCategoryID] [bigint] IDENTITY(1,1) NOT NULL,
	[EventCategory] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EventCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EventFavourite]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventFavourite]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EventFavourite](
	[UserID] [nvarchar](128) NULL,
	[eventId] [bigint] NULL,
	[FavId] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_EventFavourite] PRIMARY KEY CLUSTERED 
(
	[FavId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[EventImage]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventImage]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EventImage](
	[EventImageID] [bigint] IDENTITY(1,1) NOT NULL,
	[EventID] [bigint] NOT NULL,
	[EventImageUrl] [varchar](200) NULL,
	[ImageType] [varchar](50) NULL,
 CONSTRAINT [PK_EventImage] PRIMARY KEY CLUSTERED 
(
	[EventImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Events_Hit]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Events_Hit]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Events_Hit](
	[EventHit_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[EventHit_EventId] [bigint] NULL,
	[EventHit_Hits] [decimal](18, 0) NULL,
	[EventHitDateTime] [datetime] NULL CONSTRAINT [DF_Events_Hit_EventHitDateTime]  DEFAULT (getdate()),
 CONSTRAINT [PK_Events_Hit] PRIMARY KEY CLUSTERED 
(
	[EventHit_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[EventSubCategory]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventSubCategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EventSubCategory](
	[EventSubCategoryID] [bigint] IDENTITY(1,1) NOT NULL,
	[EventCategoryID] [bigint] NOT NULL,
	[EventSubCategory] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EventSubCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EventTempImage]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventTempImage]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EventTempImage](
	[UserId] [varchar](300) NOT NULL,
	[EvenUniqueid] [varchar](300) NOT NULL,
	[EventImageUrl] [varchar](300) NULL,
	[ImageType] [varchar](50) NULL,
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_EventTempImage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EventType]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EventType](
	[EventTypeID] [bigint] IDENTITY(1,1) NOT NULL,
	[EventType] [varchar](200) NOT NULL,
	[EventHide] [char](1) NULL,
PRIMARY KEY CLUSTERED 
(
	[EventTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EventVenue]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventVenue]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EventVenue](
	[EventVenueID] [bigint] IDENTITY(1,1) NOT NULL,
	[EventID] [bigint] NOT NULL,
	[AddressId] [bigint] NOT NULL,
	[EventStartDate] [varchar](50) NULL,
	[EventEndDate] [varchar](50) NULL,
	[EventStartTime] [varchar](50) NULL,
	[EventEndTime] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[EventVenueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EventVote]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventVote]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EventVote](
	[UserID] [nvarchar](128) NULL,
	[eventId] [bigint] NULL,
	[VoteId] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_EventVote] PRIMARY KEY CLUSTERED 
(
	[VoteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Fee_Structure]    Script Date: 3/15/2016 12:07:25 PM ******/
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
/****** Object:  Table [dbo].[Messages]    Script Date: 3/15/2016 12:07:25 PM ******/
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
/****** Object:  Table [dbo].[MultipleEvent]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MultipleEvent]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MultipleEvent](
	[MultipleEventID] [bigint] IDENTITY(1,1) NOT NULL,
	[EventID] [bigint] NOT NULL,
	[Frequency] [varchar](20) NOT NULL,
	[StartingFrom] [varchar](50) NULL,
	[StartingTo] [varchar](50) NULL,
	[WeeklyDay] [varchar](100) NULL,
	[MonthlyDay] [int] NULL,
	[StartTime] [varchar](50) NULL,
	[EndTime] [varchar](50) NULL,
	[MonthlyWeek] [varchar](10) NULL,
	[MonthlyWeekDays] [varchar](100) NULL,
 CONSTRAINT [PK__Multiple__8A680315196CFEEE] PRIMARY KEY CLUSTERED 
(
	[MultipleEventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Order_Detail_T]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Order_Detail_T]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Order_Detail_T](
	[O_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[O_Order_Id] [varchar](100) NULL,
	[O_User_Id] [nvarchar](256) NULL,
	[O_TotalAmount] [numeric](18, 2) NULL,
	[O_OrderAmount] [numeric](18, 2) NULL,
	[O_VariableId] [varchar](500) NULL,
	[O_VariableAmount] [numeric](18, 2) NULL,
	[O_PromoCodeId] [bigint] NULL,
	[O_OrderDateTime] [datetime] NULL CONSTRAINT [DF_Order_Detail_T_O_OrderDateTime]  DEFAULT (getdate()),
	[O_PayPal_TokenId] [varchar](100) NULL,
	[O_PayPal_PayerId] [varchar](100) NULL,
	[O_PayPal_TrancId] [varchar](100) NULL,
 CONSTRAINT [PK_Order_Detail_T] PRIMARY KEY CLUSTERED 
(
	[O_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Organizer_Master]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Organizer_Master]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Organizer_Master](
	[Orgnizer_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Orgnizer_Name] [varchar](200) NULL,
	[Organizer_Desc] [nvarchar](max) NULL,
	[Organizer_FBLink] [varchar](200) NULL,
	[Organizer_Twitter] [varchar](200) NULL,
	[Organizer_Linkedin] [varchar](200) NULL,
	[UserId] [varchar](256) NULL,
	[Organizer_Image] [varchar](300) NULL,
	[Organizer_Address1] [varchar](256) NULL,
	[Organizer_Address2] [varchar](256) NULL,
	[Organizer_City] [varchar](256) NULL,
	[Organizer_State] [varchar](256) NULL,
	[Organizer_Zipcode] [varchar](8) NULL,
	[Organizer_CountryId] [tinyint] NULL,
	[Organizer_Email] [varchar](100) NULL,
	[Organizer_Phoneno] [varchar](20) NULL,
	[Organizer_Websiteurl] [varchar](256) NULL,
	[Organizer_Status] [varchar](2) NULL,
	[contenttype] [varchar](50) NULL,
	[Imagepath] [varchar](500) NULL,
 CONSTRAINT [PK_Organizer_Master] PRIMARY KEY CLUSTERED 
(
	[Orgnizer_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Payment_Info]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Payment_Info]') AND type in (N'U'))
BEGIN
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
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Permission_Detail]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Permission_Detail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Permission_Detail](
	[Permission_Id] [int] IDENTITY(1,1) NOT NULL,
	[Permission_Desc] [varchar](100) NULL,
	[Permission_Category] [varchar](50) NULL,
 CONSTRAINT [PK_Permission_Detail] PRIMARY KEY CLUSTERED 
(
	[Permission_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Profile]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Profile]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Profile](
	[ProfileID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](128) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Email] [varchar](100) NULL,
	[StreetAddressLine1] [varchar](256) NULL,
	[StreetAddressLine2] [varchar](256) NULL,
	[City] [varchar](256) NULL,
	[State] [varchar](256) NULL,
	[Zip] [varchar](10) NULL,
	[CountryID] [tinyint] NOT NULL,
	[MainPhone] [varchar](20) NULL,
	[SecondPhone] [varchar](20) NULL,
	[WebsiteURL] [varchar](100) NULL,
	[TwitterURL] [varchar](100) NULL,
	[FacebookURL] [varchar](100) NULL,
	[GooglePlusURL] [varchar](100) NULL,
	[LinkedInURL] [varchar](100) NULL,
	[UserProfileImage] [varchar](300) NULL,
	[OrganizerName] [varchar](50) NULL,
	[SecondaryEmail] [varchar](50) NULL,
	[OrganizerDescription] [varchar](max) NULL,
	[AddedOn] [datetime] NULL,
	[UpdateOn] [datetime] NULL,
	[WorkPhone] [varchar](20) NULL,
	[ContentType] [varchar](50) NULL,
	[Gender] [varchar](50) NULL,
	[DateofBirth] [varchar](50) NULL,
	[Organiser] [varchar](2) NULL,
	[Merchant] [varchar](2) NULL,
	[UserStatus] [varchar](2) NULL,
	[SendCur_EventDetail] [char](1) NULL,
	[Ipcountry] [varchar](100) NULL,
	[IpState] [varchar](100) NULL,
	[Ipcity] [varchar](100) NULL,
 CONSTRAINT [PK__Profile__290C888415502E78] PRIMARY KEY CLUSTERED 
(
	[ProfileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Publish_Event_Detail]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Publish_Event_Detail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Publish_Event_Detail](
	[PE_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PE_Event_Id] [bigint] NULL,
	[PE_Address_Ids] [varchar](100) NULL,
	[PE_MultipleVenue_id] [bigint] NULL,
	[PE_SingleVenue_Id] [bigint] NULL,
	[PE_Tickets_Ids] [varchar](100) NULL,
	[PE_Scheduled_Date] [varchar](50) NULL,
	[PE_Start_Time] [varchar](50) NULL,
	[PE_End_Time] [varchar](50) NULL,
 CONSTRAINT [PK_Publish_Event_Detail] PRIMARY KEY CLUSTERED 
(
	[PE_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ShippingAddress]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ShippingAddress]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ShippingAddress](
	[ShippingId] [bigint] IDENTITY(1,1) NOT NULL,
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
 CONSTRAINT [PK_ShippingAddress] PRIMARY KEY CLUSTERED 
(
	[ShippingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Status]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Status]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Status](
	[StatusID] [bigint] IDENTITY(1,1) NOT NULL,
	[StatusName] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ticket]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Ticket](
	[T_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[E_Id] [bigint] NULL,
	[T_name] [nvarchar](256) NULL,
	[Qty_Available] [bigint] NOT NULL,
	[Price] [decimal](9, 2) NULL,
	[TicketTypeID] [bigint] NULL,
	[T_Sold] [varchar](1) NULL,
	[Registration_Recorded] [varchar](1) NULL,
	[T_Desc] [nvarchar](1000) NULL,
	[Show_T_Desc] [varchar](1) NULL,
	[Fees_Type] [varchar](50) NULL,
	[Sale_Start_Date] [date] NULL,
	[Sale_Start_Time] [varchar](50) NULL,
	[Sale_End_Date] [date] NULL,
	[Sale_End_Time] [varchar](50) NULL,
	[Hide_Ticket] [varchar](1) NULL,
	[Auto_Hide_Sche] [varchar](1) NULL,
	[Hide_Untill_Date] [date] NULL,
	[Hide_Untill_Time] [varchar](50) NULL,
	[Hide_After_Date] [date] NULL,
	[Hide_After_Time] [varchar](50) NULL,
	[Min_T_Qty] [numeric](6, 0) NULL,
	[Max_T_Qty] [numeric](6, 0) NULL,
	[T_Disable] [varchar](1) NULL,
	[T_Mark_SoldOut] [varchar](1) NULL,
	[T_Sold_Qty] [bigint] NULL,
	[T_order] [tinyint] NULL,
	[EC_Fee] [decimal](9, 2) NULL,
	[Customer_Fee] [decimal](9, 2) NULL,
	[T_Displayremaining] [varchar](50) NULL,
	[T_AutoSechduleType] [varchar](1) NULL,
	[Additional_Fee] [decimal](9, 2) NULL,
	[T_Discount] [decimal](9, 2) NULL,
	[TotalPrice] [decimal](9, 2) NULL,
	[T_Customize] [varchar](1) NULL,
	[T_Ecpercent] [decimal](9, 2) NULL,
	[T_EcAmount] [decimal](9, 2) NULL,
 CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED 
(
	[T_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ticket_Locked_Detail]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ticket_Locked_Detail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Ticket_Locked_Detail](
	[TLD_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TLD_User_Id] [nvarchar](256) NULL,
	[TLD_Locked_Qty] [bigint] NULL,
	[TLD_Event_Id] [bigint] NULL,
	[TLD_TQD_Id] [bigint] NULL,
	[Locktime] [datetime] NULL CONSTRAINT [DF_Ticket_Locked_Detail_Locktime]  DEFAULT (getdate()),
	[TLD_GUID] [varchar](1000) NULL,
	[TLD_Donate] [decimal](18, 0) NULL,
	[TicketAmount] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Ticket_Locked_Detail] PRIMARY KEY CLUSTERED 
(
	[TLD_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ticket_Purchased_Detail]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ticket_Purchased_Detail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Ticket_Purchased_Detail](
	[TPD_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TPD_User_Id] [nvarchar](256) NULL,
	[TPD_Order_Id] [varchar](11) NULL,
	[TPD_Purchased_Qty] [bigint] NULL,
	[TPD_TQD_Id] [bigint] NULL,
	[TPD_Event_Id] [bigint] NULL,
	[TPD_Amount] [numeric](9, 2) NULL,
	[TPD_Donate] [decimal](18, 0) NULL,
	[TPD_GUID] [varchar](1000) NULL,
	[TPD_Ec_Fee] [numeric](18, 2) NULL,
 CONSTRAINT [PK_Ticket_Purchased_Detail] PRIMARY KEY CLUSTERED 
(
	[TPD_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ticket_Quantity_Detail]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ticket_Quantity_Detail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Ticket_Quantity_Detail](
	[TQD_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TQD_PE_Id] [bigint] NULL,
	[TQD_Event_Id] [bigint] NULL,
	[TQD_Ticket_Id] [bigint] NULL,
	[TQD_AddressId] [bigint] NULL,
	[TQD_Quantity] [bigint] NULL,
	[TQD_Remaining_Quantity] [bigint] NULL,
	[TQD_StartDate] [varchar](50) NULL,
	[TQD_StartTime] [varchar](50) NULL,
 CONSTRAINT [PK_Ticket_Quantity_Info] PRIMARY KEY CLUSTERED 
(
	[TQD_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TicketBearer]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TicketBearer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TicketBearer](
	[TicketbearerId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [nchar](128) NULL,
	[OrderId] [varchar](1000) NULL,
	[Guid] [nchar](128) NULL,
	[Name] [nchar](128) NULL,
	[Email] [nchar](256) NULL,
 CONSTRAINT [PK_TicketBearer] PRIMARY KEY CLUSTERED 
(
	[TicketbearerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TicketDeliveryMethod]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TicketDeliveryMethod]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TicketDeliveryMethod](
	[TicketDeliveryMethodID] [bigint] IDENTITY(1,1) NOT NULL,
	[DeliveryMethodID] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TicketDeliveryMethodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[TicketOrderDetail]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TicketOrderDetail]') AND type in (N'U'))
BEGIN
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
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TicketType]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TicketType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TicketType](
	[TicketTypeID] [bigint] IDENTITY(1,1) NOT NULL,
	[TicketType] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TicketTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TimeZoneDetail]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TimeZoneDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TimeZoneDetail](
	[TimeZone_Name] [varchar](500) NULL,
	[TimeZone] [varchar](500) NOT NULL,
	[TimeZone_Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_TimeZonesystem] PRIMARY KEY CLUSTERED 
(
	[TimeZone_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TimeZoneDetail1]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TimeZoneDetail1]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TimeZoneDetail1](
	[TimeZone_Id] [int] IDENTITY(1,1) NOT NULL,
	[TimeZone_Name] [varchar](500) NULL,
	[TimeZone] [varchar](500) NULL,
 CONSTRAINT [PK_TimeZoneDetail] PRIMARY KEY CLUSTERED 
(
	[TimeZone_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User_Permission_Detail]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User_Permission_Detail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[User_Permission_Detail](
	[UP_Id] [int] IDENTITY(1,1) NOT NULL,
	[UP_User_Id] [nvarchar](128) NULL,
	[UP_Permission_Id] [int] NOT NULL,
	[UP_Flag] [nchar](1) NULL,
 CONSTRAINT [PK_User_Permission_Detail] PRIMARY KEY CLUSTERED 
(
	[UP_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Venue]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Venue]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Venue](
	[VenueID] [bigint] IDENTITY(1,1) NOT NULL,
	[AddressID] [bigint] NOT NULL,
	[VenueName] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[VenueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'201508241139135_InitialCreate', N'EventKombo.Models.ApplicationDbContext', 0x1F8B0800000000000400DD5CDB6EE436127D5F60FF41D0D3EEC269F99219CC1AED044EDBDE3532BE60DA13ECDB802DB1DBC248A422518E8D205F96877C527E618B1275E345976EB9BB1D0C30B0C8E2A962B148168BC5FEF3F73FA6DF3F8781F584E3C4A7E4CC3E9A1CDA16262EF57CB23AB353B6FCE683FDFD777FFFDBF4D20B9FAD9F0ABA134E072D4972663F32169D3A4EE23EE2102593D077639AD0259BB8347490479DE3C3C37F3B47470E06081BB02C6BFA2925CC0F71F6019F334A5C1CB1140537D4C34122CAA1669EA15AB728C449845C7C665F3E61C27EA4E1824E7262DB3A0F7C0482CC71B0B42D440865888198A79F133C673125AB7904052878788930D02D51906021FE6945DEB72787C7BC274ED5B08072D384D17020E0D189508D23375F4BC176A93A50DE252899BDF05E670A3CB3AF3D9C157DA201284066783A0B624E7C66DF942CCE93E816B349D17092435EC500F70B8DBF4EEA880756EF7607A5291D4F0EF9BF036B96062C8DF119C1298B517060DDA78BC0777FC42F0FF42B266727478BE5C98777EF9177F2FE5B7CF2AEDE53E82BD0350AA0E83EA6118E4136BC2CFB6F5B4EB39D23372C9BD5DAE45A015B8259615B37E8F923262BF608F3E5F8836D5DF9CFD82B4A84717D263E4C2268C4E2143E6FD320408B0097F54E2B4FFE7F0BD7E377EF47E17A8B9EFC5536F4127F983831CCAB4F38C86A93473FCAA75763BCBF08B2AB9886FCBB695F79ED97394D639777861A491E50BCC2AC29DDD4A98CB7974973A8F1CDBA40DD7FD3E692AAE6AD25E51D5A6726142CB63D1B0A795F976F6F8B3B8F2218BCCCB4B846DA0C4ED9AB2652E303AB22A90CE7A8AFE110E8D05F791DBC0C911F8CB010F6E0022EC8D28F435CF6F2070A6687C86099EF5192C03AE0FD17258F2DA2C39F23883EC76E1A8379CE190AA357E776FF4809BE4DC305B7FAEDF11A6D681E7EA157C86534BE24BCD5C6781FA9FB95A6EC92781788E1CFCC2D00F9E7831FF60718459C73D7C5497205C68CBD19050FBB00BC26ECE478301C5F9F76ED88CC02E4877A4F445A49BF14A49537A2A7503C120399CE2B6913F5235DF9A49FA805A959D49CA25354413654540ED64F524169163423E89433A71ACDCFCB46687C472F83DD7F4F6FB3CDDBB416D4D438871512FF07131CC332E6DD23C6704CAA11E8B36EECC259C8868F337DF5BD29E3F4130AD2B159AD351BB24560FCD990C1EEFF6CC8C484E227DFE35E498FE34F410CF0BDE8F527ABEE392749B6EDE9D0E8E6B6996F670D304D97F324A1AE9FCD024DE04B842D9AF2830F6775C730F2DEC87110E81818BACFB73C2881BED9B251DD910B1C6086AD73370F0CCE50E2224F552374C81B2058B1A36A04ABE2214DE1FEA5F0044BC7316F84F821288199EA13A64E0B9FB87E84824E2D492D7B6E61BCEF250FB9E602479870869D9AE8C35C1FFEE002947CA441E9D2D0D4A9595CBB211ABC56D39877B9B0D5B82B5189ADD86487EF6CB04BE1BFBD8A61B66B6C0BC6D9AE923E02184379BB30507156E96B00F2C165DF0C543A31190C54B8545B31D0A6C67660A04D95BC3903CD8FA87DC75F3AAFEE9B79360FCADBDFD65BD5B503DB6CE863CF4C33F73DA10D83163856CDF362C12BF133D31CCE404E713E4B84AB2B9B08079F63D60CD954FEAED60F75DA4164236A03AC0CAD03545C022A40CA841A205C11CB6B954E781103608BB85B2BAC58FB25D89A0DA8D8F5CBD01AA1F9CA5436CE5EA78FB267A5352846DEEBB050C3D11884BC78353BDE4329A6B8ACAA983EBEF0106FB8D63131182D0AEAF05C0D4A2A3A33BA960AD3ECD692CE211BE2926DA425C97D3268A9E8CCE85A1236DAAD248D5330C02DD84845CD2D7CA4C956443ACADDA6AC9B3A798A9428983A865CAAE90D8A229FAC6AB955A2C49AE78955B36FE6C3538EC21CC371134DE651296DC989D118ADB0540BAC41D22B3F4ED805626881789C67E6850A99766F352CFF05CBFAF6A90E62B10F14D4FC6F7129AC5CDD37B65AD517111057D0C1903B3459145D33FCFAE6164F7543018A3581FB190DD29098FD2B73EBFCFAAEDE3E2F5111A68E24BFE23F29CA52BCDCA6E67B8D8B3A27C619A3D27B597F9CCC10266D17BE675DDF267FD48C5284A7EA28A690D5CEC6CDE4C60C192BD9411C3E549D08AF33AB44564A1D40140DC4A825362860B5BAFEA8CDDC933A66B3A63FA29460528794AA0648594F23690859AF580BCFA0513D457F0E6AE2481D5DADED8FAC4921A9436BAAD7C0D6C82CD7F547D56499D48135D5FDB1AB9413790DDDE37DCB786C5977E3CA0FB69BED5C068CD75910C7D9F86AF7F775A05AF1402C7143AF8089F2BD3426E3E96E5D63CAC3199B199301C3BCEE342EBE9BCB4EEB6DBD19B3719BDD58DADB6EF3CD78C34CF6550D4339DBC92425F7F28C279DE5A6E25CD5FD78463968E524B655A811B6F59784E170C20926F39F8359E063BE8817043788F84B9CB03C83C33E3E3C3A961EE0ECCF63182749BC40732E35BD88698ED91692B1C8138ADD4714ABA9111B3C18A94095A8F335F1F0F399FD6BD6EA340B60F0BFB2E203EB3AF94CFC9F53A87888536CFDA6A67A8E9340DF7EC2DAD3E70EFDB57AFDBF2F79D303EB2E8619736A1D4ABA5C67849B8F2006499337DD409AB59F46BCDD09D57879A0459526C4FA0F0D163E1BE5914121E53F42F4FCCFA1A2691F126C84A8792C3016DE282A343D065807CBF810C0834F963D0418D659FDC3807544333E0AF0C97030F94940FF65A868B9C3AD467324DAC69294E9B933A57AA3FCCA5DEF4D4AE6F546135DCDAE1E00B74106F51A96F1C6928F47DB1D35B9C5A361EFD2B45F3DA1785F7288ABEC8EDDA60E6F335BB8E54EE82F9524BC07696D9A349DDDA7026FDBD64C61DC3DCFA71C96F0BB67C62692B7769FD6BB6D63338579F7DCD80625EFEE99ADED6AFFDCB1A5F5DE42779E8AAB661519AE6374B1E0AE54DB3C700E27FC050523C83DCAFC85A43EB7CBC4AC321623C38AC4CCD49C54263356268EC257A168673BACAF62C36FEDACA069676B48C56CE32DD6FF56DE82A69DB721C1711749C2DA14435DE276C73AD69601F59692821B3DE9C841EFF2595BEFD6DF520EF0284A69CC1EC31DF1DB49F91D4525634E9D0129BEEA752FEC9DB55F5484FD3BF1571504FF7D4582DDC6AE59D25C93252D366F49A282448AD0DC60863CD852CF63E62F91CBA09AC798B327DE59DC8EDF742CB0774DEE5216A50CBA8CC345D008787127A08D7F96C7DC94797A1765BF56324617404C9FC7E6EFC80FA91F78A5DC579A989001827B1722A2CBC792F1C8EEEAA544BAA5A42790505FE9143DE0300A002CB92373F484D7910DCCEF235E21F7A58A009A40BA07A2A9F6E9858F56310A138151B5874FB0612F7CFEEEFFB9B517EC58540000, N'6.1.3-40302')
SET IDENTITY_INSERT [dbo].[Address] ON 

INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress], [Latitude], [Longitude]) VALUES (70, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'800f9add-e483-4c68-b7ec-528c742e5407', 10055, N'Tilak Nagar, New Delhi, Delhi, India', NULL, NULL)
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress], [Latitude], [Longitude]) VALUES (79, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'800f9add-e483-4c68-b7ec-528c742e5407', 10064, N'Vasundhara, Ghaziabad, Uttar Pradesh, India', N'28.6636281', N'77.3697672')
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress], [Latitude], [Longitude]) VALUES (80, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'800f9add-e483-4c68-b7ec-528c742e5407', 10063, N'Vasundhara, Ghaziabad, Uttar Pradesh, India', N'28.6636281', N'77.3697672')
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress], [Latitude], [Longitude]) VALUES (81, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', 10065, N'Noida, Uttar Pradesh, India', N'28.5355161', N'77.3910265')
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress], [Latitude], [Longitude]) VALUES (82, N'', N'', N'', N'', N'', N'', 0, N'Mumbai, Maharashtra, India', N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', 10066, N'Mumbai, Maharashtra, India ', N'19.0759837', N'72.8776559')
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress], [Latitude], [Longitude]) VALUES (83, N'', N'', N'', N'Gautam Buddh Nagar', N'India', N'', 1, N'Noida', N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', 10066, N'Noida,Gautam Buddh Nagar,India, United States                          ', N'28.5355161', N'77.3910265')
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress], [Latitude], [Longitude]) VALUES (84, N'', N'', N'', N'', N'', N'', 0, N'Mumbai, Maharashtra, India', N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', 10067, N'Mumbai, Maharashtra, India ', N'19.0759837', N'72.8776559')
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress], [Latitude], [Longitude]) VALUES (85, N'', N'', N'', N'Gautam Buddh Nagar', N'India', N'', 1, N'Noida', N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', 10067, N'Noida,Gautam Buddh Nagar,India, United States                          ', N'28.5355161', N'77.3910265')
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress], [Latitude], [Longitude]) VALUES (86, N'', N'', N'', N'', N'', N'', 0, N'Mumbai, Maharashtra, India', N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', 10068, N'Mumbai, Maharashtra, India ', N'19.0759837', N'72.8776559')
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress], [Latitude], [Longitude]) VALUES (87, N'', N'', N'', N'Gautam Buddh Nagar', N'India', N'', 1, N'Noida', N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', 10068, N'Noida,Gautam Buddh Nagar,India, United States                          ', N'28.5355161', N'77.3910265')
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress], [Latitude], [Longitude]) VALUES (88, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'800f9add-e483-4c68-b7ec-528c742e5407', 10069, N'Defence Colony, New Delhi, Delhi, India', N'28.5726334', N'77.23251')
INSERT [dbo].[Address] ([AddressID], [Name], [Address1], [Address2], [City], [State], [Zip], [CountryID], [VenueName], [UserId], [EventId], [ConsolidateAddress], [Latitude], [Longitude]) VALUES (89, N'', N' ', N' ', N' ', N' ', N' ', 0, N' ', N'909e0dae-2f54-4fe6-84da-c1a810ee7ab7', 10070, N'Noida, Uttar Pradesh, India', N'28.5355161', N'77.3910265')
SET IDENTITY_INSERT [dbo].[Address] OFF
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1', N'Super Admin')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'2', N'Admin')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3', N'Member')
INSERT [dbo].[AspNetUserLogins] ([LoginProvider], [ProviderKey], [UserId]) VALUES (N'Facebook', N'1040202169335532', N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1')
INSERT [dbo].[AspNetUserLogins] ([LoginProvider], [ProviderKey], [UserId]) VALUES (N'Facebook', N'1052370014793267', N'800f9add-e483-4c68-b7ec-528c742e5407')
INSERT [dbo].[AspNetUserLogins] ([LoginProvider], [ProviderKey], [UserId]) VALUES (N'Google', N'112550343958679869633', N'800f9add-e483-4c68-b7ec-528c742e5407')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0aa92a22-0b7a-4055-bc2b-8ff72a92adf1', N'1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0c2935d4-cfaf-4792-8569-a07d624b762f', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0cb998d7-c1e1-4019-8381-45132347d9af', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'17f0863a-156f-4d34-bea3-fd247d0c7d62', N'1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'250d1f3f-6bac-463d-9ae6-53bd8dda317b', N'1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2ecda86e-1d74-4cc4-b05b-97dba9111ac0', N'1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3fd06a6a-9b9c-46a4-84db-e2f5f5168ad6', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'57af65d8-f739-4e09-a39e-f7b0ae2715f0', N'1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'766ab684-8ea6-4ff7-b09c-1a99cec69e22', N'1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'800f9add-e483-4c68-b7ec-528c742e5407', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8dcc551e-dd63-4130-a138-21ed5ce3ecaa', N'2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8dee8fd1-a0e0-403f-9c19-f2e271f0444c', N'1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'909e0dae-2f54-4fe6-84da-c1a810ee7ab7', N'1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9288c7f0-e234-4489-971d-e8d029ededcc', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a06cd8b0-1d3e-4dfe-92d4-0d4bc62a5076', N'1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a60e2f31-b772-428e-9bf5-2c5e6dcd56d7', N'1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ab798d10-5c11-4c4e-b206-6da5ac9045f1', N'1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd60e166b-0713-475c-8ca4-9d749aed86f3', N'1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd8757720-1948-4fe5-8d55-0aa7530a82f1', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'df32bef3-022e-4c62-a695-dfa21892321f', N'1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e25f3b4b-4c09-43df-82a0-c72054417035', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', N'3')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'0aa92a22-0b7a-4055-bc2b-8ff72a92adf1', N'mayank.srivastava@kiwitech.com', 0, N'AIZEWdTXBAKwdHfzYCH+V+AK7N2j2KbVpeTiLLd4YzBO/hHX2jqwybbvKSgwW3necg==', N'002dfff5-957e-4097-82c4-7de5119d1fdf', NULL, 0, 0, NULL, 1, 0, N'mayank.srivastava@kiwitech.com', N'Y')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'0c2935d4-cfaf-4792-8569-a07d624b762f', N'fun@ickld.com', 0, N'APNt4sfk34SMMw24kvFTAyGYCKxHa4Py/3f3g9gWiBl/F5FmbE9qLiHLgymR3RxtaA==', N'5332ea9c-e005-480a-979a-1a5d2ec07dd6', NULL, 0, 0, NULL, 1, 0, N'fun@ickld.com', N'Y')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'0cb998d7-c1e1-4019-8381-45132347d9af', N'piyush.gupta123@kiwitech.com', 0, N'AEv5HKDHsmbnI0IgOFEAKYK77/Stl89O7yMpI4qMsKBid22pu4R4bs90FrDZc3hdzQ==', N'787861e9-984e-4f80-a854-1c2e399a46af', NULL, 0, 0, NULL, 1, 0, N'piyush.gupta123@kiwitech.com', N'Y')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'17f0863a-156f-4d34-bea3-fd247d0c7d62', N'saurabh.bhardwaj@kiwitech.com', 0, N'AEAkSwqgwVDtGqXeUkH49HHPgYTviQi5y0LTbGNinVXZRaSFgGBbfpxfOzk4Uwog6A==', N'8dd3d058-afc8-4bb4-ad23-d62b72945f80', NULL, 0, 0, NULL, 1, 0, N'saurabh.bhardwaj@kiwitech.com', N'N')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'250d1f3f-6bac-463d-9ae6-53bd8dda317b', N'shweta.sindu@kiwitech.com', 0, N'ADgr920ZL4cSusGMYxoc7lAoF2Cd5JJ0bmQq7CaFF8OVcIQfdiUlIJwUGqC4ROrQkg==', N'01ef5221-e5f6-4da4-8366-1f4d7b833b1f', NULL, 0, 0, NULL, 1, 0, N'shweta.sindu@kiwitech.com', N'N')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'2ecda86e-1d74-4cc4-b05b-97dba9111ac0', N'saroosh@eventcombo.com', 0, N'AOwr13Gi2GXPp1CLtRx0SvgDQvNHmwCPqKcF8ESp0APBGeS1+qNs7PAXe1dwVhDHZg==', N'22c4b46c-465b-404b-a458-0eaee74c221f', NULL, 0, 0, NULL, 1, 0, N'saroosh@eventcombo.com', N'N')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'3fd06a6a-9b9c-46a4-84db-e2f5f5168ad6', N'krk@outlook.in', 0, N'ADLtClQPtlOSE96Gv21fXjrtzQffOa0uDyBbRu2swNMyF+XSO4xS8GXSJRu0Fh0Uig==', N'854ee79c-c296-4102-8b37-3e35d61c4e12', NULL, 0, 0, NULL, 1, 0, N'krk@outlook.in', N'Y')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'57af65d8-f739-4e09-a39e-f7b0ae2715f0', N'shweta.sindhu123567@kiwitech.com', 0, N'AG/iFfLcQqV1X9Pl/I9mXOQJ93LW3dBhCiqcKg2HDu+4Xjg2VRJ7rjX0CifGPFXnJA==', N'57ba901c-2c57-47a9-9bdd-cc23c4a6b1f2', NULL, 0, 0, NULL, 1, 0, N'shweta.sindhu123567@kiwitech.com', N'Y')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'766ab684-8ea6-4ff7-b09c-1a99cec69e22', N'apavlov@gmail.com', 0, N'AE56K/uByetkYWj3j38jABcSs2SqPr8Er7SwoXfBMlK+HPY3gWgKkBDcO6SBRN4SbA==', N'9d975099-adad-4784-8558-adb613e6b8c0', NULL, 0, 0, NULL, 1, 0, N'apavlov@gmail.com', N'Y')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'800f9add-e483-4c68-b7ec-528c742e5407', N'just.shweta29@gmail.com', 0, N'AJ1TL2fzm/jZSoiy/z8H8UZpkj0Nn1NAnxJJ2m1Mm4R70kUaMG4WgAgIo17diFuAsQ==', N'48e7f825-eaa1-48af-8eac-c05786204870', NULL, 0, 0, NULL, 1, 0, N'just.shweta29@gmail.com', N'Y')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'8dcc551e-dd63-4130-a138-21ed5ce3ecaa', N'piyush.gupta12@kiwitech.com', 0, N'ADvgohH4EtI0u6UPUs8pgZok9fMo/mcPrVRDIlZi7CVTSnaWNYINeO2ygoXX7Mnviw==', N'496f5bf0-1234-4268-a6bd-8d3ff4692a1c', NULL, 0, 0, NULL, 1, 0, N'piyush.gupta12@kiwitech.com', N'Y')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'8dee8fd1-a0e0-403f-9c19-f2e271f0444c', N'pintu.sah@kiwitech.com', 0, N'ANd7C80BO3mgOgP0TATZNKzR6nKhxTRiAw0s+tlSkeXVlnsGLC39xlU5lana9su9WQ==', N'e5b1b1c3-23e9-473e-9b30-4b7385b5735f', NULL, 0, 0, NULL, 1, 0, N'pintu.sah@kiwitech.com', N'N')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'909e0dae-2f54-4fe6-84da-c1a810ee7ab7', N'piyush.gupta@kiwitech.com', 0, N'ANg6Xpyr/lff4v/krmVTu2UyAxij7B7dzFHHlKr7TuasQVw8PUvTp8T5f5sJ8UKBng==', N'1dbe82a1-a5fa-4de4-990c-efc43f377d3e', NULL, 0, 0, NULL, 1, 0, N'piyush.gupta@kiwitech.com', N'Y')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'9288c7f0-e234-4489-971d-e8d029ededcc', N'kesarwanirohit@rocketmail.com', 0, N'AL2hQA8tN6YHoagDVlB+ro4vpU4cpRa3okv9VD+L9eQYNPshl/8jsALcx9iJK7v8dQ==', N'92e659f9-9078-4473-97f1-48210935d780', NULL, 0, 0, NULL, 1, 0, N'kesarwanirohit@rocketmail.com', N'Y')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'a06cd8b0-1d3e-4dfe-92d4-0d4bc62a5076', N'sarooshgull@gmail.com', 0, N'AMPjVG0t/uiz/1G3wkx93iKNQ/9tVPhp0G46Yz0x/kdEmvu/xRyddUTDNHCgF5VXPg==', N'96936b79-3c0e-4195-b41a-be7f244b88b5', NULL, 0, 0, NULL, 1, 0, N'sarooshgull@gmail.com', N'N')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'a60e2f31-b772-428e-9bf5-2c5e6dcd56d7', N'editor@eventcombo.com', 0, N'AC7ajv67BWJKTd+CxJ4eFr/enxrpGTV5OnypUnZ/bSKlx3PR5ghDHnNN6Vl8lZe9uA==', N'd8ba0b79-e687-4785-bcb3-489141111a10', NULL, 0, 0, NULL, 1, 0, N'editor@eventcombo.com', N'N')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'ab798d10-5c11-4c4e-b206-6da5ac9045f1', N'navrit.singh@kiwitech.com', 0, N'AMZqiwj8/zZ7TztcMmpUf5MZUKGg8LxcUm772NZIsomrpOXfZ3oNqYgy4XVVyMNfQg==', N'483d4a83-14b9-4384-99d6-8ad33d9590dc', NULL, 0, 0, NULL, 1, 0, N'navrit.singh@kiwitech.com', N'Y')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'd60e166b-0713-475c-8ca4-9d749aed86f3', N'kannan.k@kiwitech.com', 0, N'AJ4/UMYtfAK5UwvhT6NHRB84h+wIZ98q1MWHsNXCFIqxaXKKKtwb7ersD3lJDDlVkQ==', N'67f0f2c8-d53c-4f30-b99f-0f2461556cc8', NULL, 0, 0, NULL, 1, 0, N'kannan.k@kiwitech.com', N'N')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'd8757720-1948-4fe5-8d55-0aa7530a82f1', N'mckeonc@seas.upenn.edu', 0, N'AHeH2ijgVHs6gftk11Y+CgFW5DXD5cqiYXq6XejhmrBi/0Fl5VcqPt7OmoF9OnzeFw==', N'18a47ea9-b3b3-4920-8f9b-9f885301bc57', NULL, 0, 0, NULL, 1, 0, N'mckeonc@seas.upenn.edu', N'Y')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'df32bef3-022e-4c62-a695-dfa21892321f', N'rohit.kesarwani@kiwitech.com', 0, N'AD5Lpa6BeZ/bxetoHJ4IA/PqBVZpZjJ+XTRwSNf9sNr1t/loCd8MWW7/8XPkr7AXFQ==', N'81803367-64bd-4b8c-b507-874633edc191', NULL, 0, 0, NULL, 1, 0, N'rohit.kesarwani@kiwitech.com', N'Y')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'e25f3b4b-4c09-43df-82a0-c72054417035', N'mayank.srivastav@kiwitech.com', 0, N'AOfyZiauCwOQ7OlUISJnF5XnEgP5ec+iaIRLOmaHGbqgLwwozSUgH0n8of60Y9uRtA==', N'94c3f877-fb20-4904-9f01-1c1f9d0c164f', NULL, 0, 0, NULL, 1, 0, N'mayank.srivastav@kiwitech.com', N'N')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LoginStatus]) VALUES (N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', N'guptapiyush21@gmail.com', 0, N'AG6yKcLMhj1MbhxCk4GCrmkeCcl6pGqv4eMrGQG86ipk2HmWiNpczkw5Ka92eHPmIw==', N'365f020f-ac12-460d-8858-595e7713f28d', NULL, 0, 0, NULL, 1, 0, N'guptapiyush21@gmail.com', N'N')
SET IDENTITY_INSERT [dbo].[BillingAddress] ON 

INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country], [PaymentType], [CardId], [card_type], [ExpirationDate], [Cvv]) VALUES (5, N'800f9add-e483-4c68-b7ec-528c742e5407                                                                                            ', N'T000000002', N'0f5ba609-752b-4de4-b2c1-e3a69d0e2bc1                                                                                            ', N'sd', N'sad', N'222-222-2222', N'sdd', N'sfsdf', N'Louisville', N'Kentucky', N'sdf', N'1', N'C', N' 378282246310005', N'amex', N'22/22', N'222')
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country], [PaymentType], [CardId], [card_type], [ExpirationDate], [Cvv]) VALUES (6, N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1                                                                                            ', N'T000000004', N'2ba28e9d-72d3-45c9-bdb9-80be326ec1ea                                                                                            ', N'Piyush', N'Gupta', N'123-456-7899', N'Noida', N'Sector-3', N'Columbia', N'Maryland', N'21045', N'1', N'C', N'5105105105105100', N'mastercard', N'01/18', N'1001')
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country], [PaymentType], [CardId], [card_type], [ExpirationDate], [Cvv]) VALUES (7, N'800f9add-e483-4c68-b7ec-528c742e5407                                                                                            ', N'T000000006', N'd28739d8-76a6-4fa1-87dd-6133baee50af                                                                                            ', N'asd', N'asd', N'122-222-2222', N'sad', NULL, N'Louisville', N'Kentucky', N'sdf', N'1', N'C', N'4111111111111111', N'visa', N'22/22', N'222')
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country], [PaymentType], [CardId], [card_type], [ExpirationDate], [Cvv]) VALUES (8, N'800f9add-e483-4c68-b7ec-528c742e5407                                                                                            ', N'T000000010', N'cc72712c-98b9-41be-ba57-068321ed6def                                                                                            ', N'asdf', N'asd', N'111-111-1111', N'asd', NULL, N'Louisville', N'Kentucky', N'sdf', N'1', N'C', N'4111111111111111', N'visa', N'22/22', N'222')
INSERT [dbo].[BillingAddress] ([BillingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country], [PaymentType], [CardId], [card_type], [ExpirationDate], [Cvv]) VALUES (9, N'800f9add-e483-4c68-b7ec-528c742e5407                                                                                            ', N'T000000011', N'6a549230-e3a6-4d76-a3eb-dde758f5b14c                                                                                            ', N'xasd', N'qde', N'122-222-2222', N'sdf', NULL, N'Louisville', N'Kentucky', N'sdf', N'1', N'C', N'4111111111111111', N'visa', N'22/22', N'222')
SET IDENTITY_INSERT [dbo].[BillingAddress] OFF
SET IDENTITY_INSERT [dbo].[CardDetails] ON 

INSERT [dbo].[CardDetails] ([CardId], [UserId], [OrderId], [Guid], [CardNumber], [ExpirationDate], [Cvv], [card_type]) VALUES (5, N'800f9add-e483-4c68-b7ec-528c742e5407                                                                                            ', N'T000000010', N'cc72712c-98b9-41be-ba57-068321ed6def                                                                                            ', N'4111111111111111', N'22/22', N'222', N'visa')
SET IDENTITY_INSERT [dbo].[CardDetails] OFF
SET IDENTITY_INSERT [dbo].[Country] ON 

INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (1, N' United States                         ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (2, N' Afghanistan                           ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (3, N' Albania                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (4, N' Algeria                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (5, N' American Samoa                        ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (6, N' Andorra                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (7, N' Angola                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (8, N' Anguilla                              ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (9, N' Antigua and Barbuda                   ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (10, N' Argentina                             ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (11, N' Armenia                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (12, N' Aruba                                 ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (13, N' Australia                             ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (14, N' Austria                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (15, N' Azerbaijan                            ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (16, N' The Bahamas                           ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (17, N' Bahrain                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (18, N' Bangladesh                            ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (19, N' Barbados                              ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (20, N' Belarus                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (21, N' Belgium                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (22, N' Belize                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (23, N' Benin                                 ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (24, N' Bermuda                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (25, N' Bhutan                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (26, N' Bolivia                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (27, N' Bosnia and Herzegovina                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (28, N' Botswana                              ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (29, N' Brazil                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (30, N' Brunei                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (31, N' Bulgaria                              ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (32, N' Burkina Faso                          ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (33, N' Burundi                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (34, N' Cambodia                              ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (35, N' Cameroon                              ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (36, N' Canada                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (37, N' Cape Verde                            ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (38, N' Cayman Islands                        ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (39, N' Central African Republic              ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (40, N' Chad                                  ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (41, N' Chile                                 ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (42, N' People Republic of China            ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (43, N' Republic of China                     ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (44, N' Christmas Island                      ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (45, N' Cocos (Keeling) Islands               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (46, N' Colombia                              ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (47, N' Comoros                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (48, N' Congo                                 ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (49, N' Cook Islands                          ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (50, N' Costa Rica                            ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (51, N' Cote dIvoire                         ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (52, N' Croatia                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (53, N' Cuba                                  ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (54, N' Cyprus                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (55, N' Czech Republic                        ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (56, N' Denmark                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (57, N' Djibouti                              ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (58, N' Dominica                              ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (59, N' Dominican Republic                    ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (60, N' Ecuador                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (61, N' Egypt                                 ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (62, N' El Salvador                           ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (63, N' Equatorial Guinea                     ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (64, N' Eritrea                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (65, N' Estonia                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (66, N' Ethiopia                              ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (67, N' Falkland Islands                      ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (68, N' Faroe Islands                         ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (69, N' Fiji                                  ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (70, N' Finland                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (71, N' France                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (72, N' French Polynesia                      ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (73, N' Gabon                                 ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (74, N' The Gambia                            ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (75, N' Georgia                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (76, N' Germany                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (77, N' Ghana                                 ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (78, N' Gibraltar                             ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (79, N' Greece                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (80, N' Greenland                             ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (81, N' Grenada                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (82, N' Guadeloupe                            ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (83, N' Guam                                  ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (84, N' Guatemala                             ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (85, N' Guernsey                              ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (86, N' Guinea                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (87, N' Guinea-Bissau                         ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (88, N' Guyana                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (89, N' Haiti                                 ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (90, N' Honduras                              ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (91, N' Hong Kong                             ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (92, N' Hungary                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (93, N' Iceland                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (94, N' India                                 ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (95, N' Indonesia                             ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (96, N' Iran                                  ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (97, N' Iraq                                  ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (98, N' Ireland                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (99, N' Israel                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (100, N' Italy                                 ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (101, N' Jamaica                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (102, N' Japan                                 ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (103, N' Jersey                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (104, N' Jordan                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (105, N' Kazakhstan                            ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (106, N' Kenya                                 ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (107, N' Kiribati                              ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (108, N' North Korea                           ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (109, N' South Korea                           ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (110, N' Kosovo                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (111, N' Kuwait                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (112, N' Kyrgyzstan                            ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (113, N' Laos                                  ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (114, N' Latvia                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (115, N' Lebanon                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (116, N' Lesotho                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (117, N' Liberia                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (118, N' Libya                                 ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (119, N' Liechtenstein                         ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (120, N' Lithuania                             ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (121, N' Luxembourg                            ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (122, N' Macau                                 ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (123, N' Macedonia                             ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (124, N' Madagascar                            ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (125, N' Malawi                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (126, N' Malaysia                              ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (127, N' Maldives                              ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (128, N' Mali                                  ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (129, N' Malta                                 ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (130, N' Marshall Islands                      ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (131, N' Martinique                            ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (132, N' Mauritania                            ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (133, N' Mauritius                             ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (134, N' Mayotte                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (135, N' Mexico                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (136, N' Micronesia                            ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (137, N' Moldova                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (138, N' Monaco                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (139, N' Mongolia                              ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (140, N' Montenegro                            ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (141, N' Montserrat                            ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (142, N' Morocco                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (143, N' Mozambique                            ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (144, N' Myanmar                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (145, N' Nagorno-Karabakh                      ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (146, N' Namibia                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (147, N' Nauru                                 ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (148, N' Nepal                                 ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (149, N' Netherlands                           ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (150, N' Netherlands Antilles                  ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (151, N' New Caledonia                         ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (152, N' New Zealand                           ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (153, N' Nicaragua                             ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (154, N' Niger                                 ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (155, N' Nigeria                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (156, N' Niue                                  ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (157, N' Norfolk Island                        ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (158, N' Turkish Republic of Northern Cyprus   ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (159, N' Northern Mariana                      ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (160, N' Norway                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (161, N' Oman                                  ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (162, N' Pakistan                              ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (163, N' Palau                                 ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (164, N' Palestine                             ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (165, N' Panama                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (166, N' Papua New Guinea                      ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (167, N' Paraguay                              ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (168, N' Peru                                  ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (169, N' Philippines                           ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (170, N' Pitcairn Islands                      ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (171, N' Poland                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (172, N' Portugal                              ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (173, N' Puerto Rico                           ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (174, N' Qatar                                 ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (175, N' Romania                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (176, N' Russia                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (177, N' Rwanda                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (178, N' Saint Barthelemy                      ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (179, N' Saint Helena                          ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (180, N' Saint Kitts and Nevis                 ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (181, N' Saint Lucia                           ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (182, N' Saint Martin                          ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (183, N' Saint Pierre and Miquelon             ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (184, N' Saint Vincent and the Grenadines      ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (185, N' Samoa                                 ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (186, N' San Marino                            ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (187, N' Sao Tome and Principe                 ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (188, N' Saudi Arabia                          ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (189, N' Senegal                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (190, N' Serbia                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (191, N' Seychelles                            ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (192, N' Sierra Leone                          ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (193, N' Singapore                             ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (194, N' Slovakia                              ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (195, N' Slovenia                              ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (196, N' Solomon Islands                       ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (197, N' Somalia                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (198, N' Somaliland                            ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (199, N' South Africa                          ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (200, N' South Ossetia                         ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (201, N' Spain                                 ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (202, N' Sri Lanka                             ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (203, N' Sudan                                 ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (204, N' Suriname                              ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (205, N' Svalbard                              ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (206, N' Swaziland                             ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (207, N' Sweden                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (208, N' Switzerland                           ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (209, N' Syria                                 ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (210, N' Taiwan                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (211, N' Tajikistan                            ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (212, N' Tanzania                              ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (213, N' Thailand                              ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (214, N' Timor-Leste                           ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (215, N' Togo                                  ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (216, N' Tokelau                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (217, N' Tonga                                 ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (218, N' Transnistria Pridnestrovie            ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (219, N' Trinidad and Tobago                   ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (220, N' Tristan da Cunha                      ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (221, N' Tunisia                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (222, N' Turkey                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (223, N' Turkmenistan                          ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (224, N' Turks and Caicos Islands              ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (225, N' Tuvalu                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (226, N' Uganda                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (227, N' Ukraine                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (228, N' United Arab Emirates                  ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (229, N' United Kingdom                        ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (230, N' Uruguay                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (231, N' Uzbekistan                            ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (232, N' Vanuatu                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (233, N' Vatican City                          ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (234, N' Venezuela                             ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (235, N' Vietnam                               ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (236, N' British Virgin Islands                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (237, N' US Virgin Islands                     ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (238, N' Wallis and Futuna                     ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (239, N' Western Sahara                        ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (240, N' Yemen                                 ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (241, N' Zambia                                ')
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (242, N' Zimbabwe                              ')
SET IDENTITY_INSERT [dbo].[Country] OFF
SET IDENTITY_INSERT [dbo].[Email_Tag] ON 

INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (1, N'UserEmailID')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (2, N'UserFirstNameID')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (3, N'UserLastNameID')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (4, N'EventNameID')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (5, N'EventStartDateID')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (6, N'EventEndDateID')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (7, N'EventStartTimeID')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (8, N'EventEndTimeID')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (9, N'EventVenueID')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (10, N'EventAddressID')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (11, N'TicketOrderNumberID')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (12, N'DealOrderNumberID')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (13, N'ResetPwdUrl')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (14, N'EventOrderNO')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (15, N'EventBarcodeId')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (16, N'EventTitleId')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (17, N'EventImageId')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (18, N'Tickettype')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (19, N'TicketPrice')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (20, N'TicketOrderDateId')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (21, N'EventTimeZone')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (22, N'Ticketname')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (23, N'EventQrCode')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (24, N'EventOrganiserName')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (25, N'EventOrganiserEmail')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (26, N'EventImage')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (27, N'EventDescription')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (28, N'EventLogo')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (29, N'EventdayId')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (30, N'Eventtype')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (31, N'TicketQty')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (32, N'CreateEventurl')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (33, N'DiscoverEventurl')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (34, N'MyTicketEventurl')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (35, N'EventDynamicTable')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (36, N'EventMapImage')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (37, N'EventLogin')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (38, N'Downloadurl')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (39, N'OrderDetail')
INSERT [dbo].[Email_Tag] ([Tag_Id], [Tag_Name]) VALUES (40, N'Eventtypedetail')
SET IDENTITY_INSERT [dbo].[Email_Tag] OFF
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
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag], [From_Name]) VALUES (N'2e223022-9c7e-45e0-8e5b-6c8776762021', N'Welcome Template', N' ¶¶UserEmailID¶¶ ', N'just.shweta29@gmail.com, ¶¶UserEmailID¶¶', N'navrit.singh@kiwitech.com, ¶¶UserEmailID¶¶', N'Welcome to Eventcombo   ¶¶UserEmailID¶¶, ¶¶DealOrderNumberID¶¶', N'<p>Hi ,</p>

<p>Thank you &para;&para;Ticketname&para;&para; for choosing eventcombo.Lets get started. &para;&para;EventStartDateID&para;&para;&nbsp;,,,&para;&para;EventNameID&para;&para;</p>
', N'welcome@eventcombo.com', N'email_welcome', N'EventCombo')
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag], [From_Name]) VALUES (N'405f9cc8-782b-4b87-8406-3a3c0ae4c60d', N'Lost password Template', N' ¶¶UserEmailID¶¶', N'just.shweta29@gmail.com', NULL, N'Reset Password ', N'<p><img alt="eventcombo" src="http://eventcombo.kiwireader.com/Images/logo_vertical.png" style="float:left; height:400px; margin:2px; width:86px" />&nbsp;Hello&nbsp; &para;&para;UserFirstNameID&para;&para; ,</p>

<p>We have recieved a request to reset your password for your account :&nbsp;&para;&para;UserEmailID&para;&para; .We&#39;re here to help!</p>

<p>Click on the link below to reset and create a new password:</p>

<p>&para;&para;ResetPwdUrl&para;&para;</p>

<p>Set a new Password if you didn&#39;t ask to change your password,don&#39;t worry!Your password is still safe and you can delete this email.</p>

<p>Best ,</p>

<p>The Eventcombo Team.</p>

<p>&nbsp;</p>
', N'shweta.sindhu@kiwitech.com', N'email_lost_pwd', N'EventCombo')
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag], [From_Name]) VALUES (N'aa3f1945-418b-4fc8-8a6d-6c60741fa70a', N'Account Info Update Template', N' ¶¶UserEmailID¶¶', NULL, NULL, N'Your Account Info is successfuly updated', N'<p>Hi&nbsp; &para;&para;UserFirstNameID&para;&para;,</p>

<p>You Account info is updated successfully.</p>
', N'shweta.sindhu@kiwitech.com', N'acc_info_update', N'EventCombo')
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag], [From_Name]) VALUES (N'edfb6c74-43f8-45e7-86d1-d19aaf7f5e37', N'', N' ¶¶UserEmailID¶¶', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag], [From_Name]) VALUES (N'fd04710c-8d72-43e5-ab83-c33da92adc15', N'Account Password Set Template', N' ¶¶UserEmailID¶¶', NULL, NULL, N'Your Password has Reset', N'<p>Hi&nbsp; &para;&para;UserFirstNameID&para;&para; ,</p>

<p>Your password has been successfully reset.</p>

<p>&nbsp;</p>

<p>&nbsp;</p>

<p>With Best Wishes,</p>

<p>The Eventcombo Team.</p>
', N'shweta.sindhu@kiwitech.com', N'acc_pwd_set', N'Eventcombo')
SET IDENTITY_INSERT [dbo].[Event] ON 

INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID], [EventCancel]) VALUES (10055, 58, 1, 0, N'800f9add-e483-4c68-b7ec-528c742e5407', N'test event', N'Y', N'Y', N'Y', N'<p>test<br></p>', N'Public', N'N', N'N', NULL, NULL, N'N', N'Live', NULL, NULL, NULL, N'42', NULL, NULL, N'Single', 0, N'N', NULL, N'Y', N'Y', N'Y', N'var 1', N'O', CAST(N'2016-03-07 13:03:13.250' AS DateTime), CAST(N'2016-03-07 13:03:30.627' AS DateTime), N'N', 0, N'N')
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID], [EventCancel]) VALUES (10063, 116, 1, 0, N'800f9add-e483-4c68-b7ec-528c742e5407', N'this is test', N'Y', N'Y', N'Y', N'<p><br></p>', N'Public', N'N', N'N', NULL, NULL, N'N', N'Save', NULL, NULL, NULL, N'42', NULL, NULL, N'Single', 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(N'2016-03-14 15:09:56.060' AS DateTime), CAST(N'2016-03-14 15:18:07.327' AS DateTime), N'N', 0, N'N')
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID], [EventCancel]) VALUES (10064, 116, 1, 0, N'800f9add-e483-4c68-b7ec-528c742e5407', N'this is test', N'Y', N'Y', N'Y', N'<p><br></p>', N'Public', N'N', N'N', NULL, NULL, N'N', N'Save', NULL, NULL, NULL, N'42', NULL, NULL, N'Single', 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(N'2016-03-14 15:09:57.920' AS DateTime), NULL, N'N', 0, N'N')
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID], [EventCancel]) VALUES (10065, 117, 22, 0, N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', N'Bike Event', N'Y', N'Y', N'Y', N'<p>Bike</p>', N'Public', N'N', N'N', NULL, N'Bike', N'N', N'Live', NULL, NULL, NULL, N'42', NULL, NULL, N'Single', 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(N'2016-03-14 19:38:15.820' AS DateTime), NULL, N'N', 0, N'N')
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID], [EventCancel]) VALUES (10066, 117, 22, 0, N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', N'Invincible', N'Y', N'Y', N'Y', N'<p>tested&nbsp;&nbsp;&nbsp;&nbsp;</p>', N'Public', N'N', N'N', NULL, N'invincible', N'N', N'Live', NULL, NULL, NULL, N'42', NULL, NULL, N'Multiple', 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(N'2016-03-14 19:46:10.117' AS DateTime), NULL, N'N', 0, N'N')
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID], [EventCancel]) VALUES (10067, 117, 22, 0, N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', N'Invincible', N'Y', N'Y', N'Y', N'<p>tested</p>', N'Public', N'N', N'N', NULL, N'invincible1', N'N', N'Live', NULL, NULL, NULL, N'42', NULL, NULL, N'Multiple', 0, N'N', NULL, N'N', N'N', N'Y', N'Tested', N'O', CAST(N'2016-03-14 19:50:39.460' AS DateTime), NULL, N'N', 0, N'N')
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID], [EventCancel]) VALUES (10068, 58, 15, 0, N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', N'Invincible123', N'Y', N'Y', N'Y', N'%3Cp%3Etested%3C/p%3E', N'Public', N'N', N'N', NULL, N'invincible12', N'N', N'Live', NULL, NULL, NULL, N'42', NULL, NULL, N'Multiple', 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, NULL, CAST(N'2016-03-14 19:56:10.757' AS DateTime), N'N', 10067, NULL)
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID], [EventCancel]) VALUES (10069, 58, 1, 0, N'800f9add-e483-4c68-b7ec-528c742e5407', N'this is test event', N'Y', N'Y', N'Y', N'<p><br></p>', N'Public', N'N', N'N', NULL, NULL, N'N', N'Save', NULL, NULL, NULL, N'42', NULL, NULL, N'Single', 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(N'2016-03-15 16:50:01.343' AS DateTime), NULL, N'N', 0, N'N')
INSERT [dbo].[Event] ([EventID], [EventTypeID], [EventCategoryID], [EventSubCategoryID], [UserID], [EventTitle], [DisplayStartTime], [DisplayEndTime], [DisplayTimeZone], [EventDescription], [EventPrivacy], [Private_ShareOnFB], [Private_GuestOnly], [Private_Password], [EventUrl], [PublishOnFB], [EventStatus], [AddedOn], [UpdateOn], [IsMultipleEvent], [TimeZone], [FBUrl], [TwitterUrl], [AddressStatus], [LastLocationAddress], [EnableFBDiscussion], [Feature], [Ticket_DAdress], [Ticket_showremain], [Ticket_showvariable], [Ticket_variabledesc], [Ticket_variabletype], [CreateDate], [ModifyDate], [ShowMap], [Parent_EventID], [EventCancel]) VALUES (10070, 58, 22, 0, N'909e0dae-2f54-4fe6-84da-c1a810ee7ab7', N'Nike', N'Y', N'Y', N'Y', N'<p>Yes</p>', N'Public', N'N', N'N', NULL, N'Nike', N'N', N'Live', NULL, NULL, NULL, N'42', NULL, NULL, N'Single', 0, N'N', NULL, N'N', N'N', N'N', NULL, NULL, CAST(N'2016-03-15 17:30:46.843' AS DateTime), NULL, N'N', 0, N'N')
SET IDENTITY_INSERT [dbo].[Event] OFF
SET IDENTITY_INSERT [dbo].[Event_Orgnizer_Detail] ON 

INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [UserId], [DefaultOrg], [OrganizerMaster_Id]) VALUES (2, 10064, N'800f9add-e483-4c68-b7ec-528c742e5407', N'Y', 1)
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [UserId], [DefaultOrg], [OrganizerMaster_Id]) VALUES (3, 10063, N'800f9add-e483-4c68-b7ec-528c742e5407', N'Y', 9)
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [UserId], [DefaultOrg], [OrganizerMaster_Id]) VALUES (4, 10065, N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', N'Y', 11)
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [UserId], [DefaultOrg], [OrganizerMaster_Id]) VALUES (5, 10066, N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', N'Y', 12)
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [UserId], [DefaultOrg], [OrganizerMaster_Id]) VALUES (6, 10067, N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', N'Y', 11)
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [UserId], [DefaultOrg], [OrganizerMaster_Id]) VALUES (7, 10068, N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', N'Y', 12)
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [UserId], [DefaultOrg], [OrganizerMaster_Id]) VALUES (8, 10069, N'800f9add-e483-4c68-b7ec-528c742e5407', N'Y', 13)
INSERT [dbo].[Event_Orgnizer_Detail] ([Orgnizer_Id], [Orgnizer_Event_Id], [UserId], [DefaultOrg], [OrganizerMaster_Id]) VALUES (9, 10070, N'909e0dae-2f54-4fe6-84da-c1a810ee7ab7', N'Y', 14)
SET IDENTITY_INSERT [dbo].[Event_Orgnizer_Detail] OFF
SET IDENTITY_INSERT [dbo].[Event_VariableDesc] ON 

INSERT [dbo].[Event_VariableDesc] ([VariableDesc], [Price], [Event_Id], [Variable_Id]) VALUES (N'vardescv', CAST(22.22 AS Numeric(18, 2)), 10055, 3)
INSERT [dbo].[Event_VariableDesc] ([VariableDesc], [Price], [Event_Id], [Variable_Id]) VALUES (N'var desc 2', CAST(2222.22 AS Numeric(18, 2)), 10055, 4)
INSERT [dbo].[Event_VariableDesc] ([VariableDesc], [Price], [Event_Id], [Variable_Id]) VALUES (N'opt', CAST(10.00 AS Numeric(18, 2)), 10067, 9)
SET IDENTITY_INSERT [dbo].[Event_VariableDesc] OFF
SET IDENTITY_INSERT [dbo].[EventCategory] ON 

INSERT [dbo].[EventCategory] ([EventCategoryID], [EventCategory]) VALUES (1, N'Category 2')
INSERT [dbo].[EventCategory] ([EventCategoryID], [EventCategory]) VALUES (2, N'Category 1')
INSERT [dbo].[EventCategory] ([EventCategoryID], [EventCategory]) VALUES (13, N'Category 3')
INSERT [dbo].[EventCategory] ([EventCategoryID], [EventCategory]) VALUES (14, N'Category 6')
INSERT [dbo].[EventCategory] ([EventCategoryID], [EventCategory]) VALUES (15, N'category 7')
INSERT [dbo].[EventCategory] ([EventCategoryID], [EventCategory]) VALUES (19, N'01')
INSERT [dbo].[EventCategory] ([EventCategoryID], [EventCategory]) VALUES (21, N'02')
INSERT [dbo].[EventCategory] ([EventCategoryID], [EventCategory]) VALUES (22, N'Test 123')
SET IDENTITY_INSERT [dbo].[EventCategory] OFF
SET IDENTITY_INSERT [dbo].[EventImage] ON 

INSERT [dbo].[EventImage] ([EventImageID], [EventID], [EventImageUrl], [ImageType]) VALUES (36, 10065, N'bike-kv.jpg', N'image/jpeg')
INSERT [dbo].[EventImage] ([EventImageID], [EventID], [EventImageUrl], [ImageType]) VALUES (37, 10065, N'hqdefault.jpg', N'image/jpeg')
INSERT [dbo].[EventImage] ([EventImageID], [EventID], [EventImageUrl], [ImageType]) VALUES (38, 10065, N'v-closeup.jpg', N'image/jpeg')
INSERT [dbo].[EventImage] ([EventImageID], [EventID], [EventImageUrl], [ImageType]) VALUES (39, 10066, N'v-closeup.jpg', N'image/jpeg')
INSERT [dbo].[EventImage] ([EventImageID], [EventID], [EventImageUrl], [ImageType]) VALUES (40, 10067, N'v-closeup.jpg', N'image/jpeg')
INSERT [dbo].[EventImage] ([EventImageID], [EventID], [EventImageUrl], [ImageType]) VALUES (41, 10068, N'v-closeup.jpg', N'image/jpeg')
INSERT [dbo].[EventImage] ([EventImageID], [EventID], [EventImageUrl], [ImageType]) VALUES (42, 10070, N'bike-kv.jpg', N'image/jpeg')
SET IDENTITY_INSERT [dbo].[EventImage] OFF
SET IDENTITY_INSERT [dbo].[Events_Hit] ON 

INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10109, 10055, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-07 07:33:41.127' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10110, 10055, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-07 07:38:57.283' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10111, 10055, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-07 08:26:54.613' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10112, 10055, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-07 09:23:56.287' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10113, 10055, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-07 09:25:07.427' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10114, 10055, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-07 13:26:02.763' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10115, 10055, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-08 05:31:25.137' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10116, 10055, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-08 05:52:17.573' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10117, 10055, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-08 23:37:33.340' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10118, 10055, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-12 03:41:53.323' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10119, 10055, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-12 17:21:39.770' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10120, 10055, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-13 08:40:59.110' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10121, 10055, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-13 16:46:53.907' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10122, 10055, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-13 22:32:13.667' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10123, 10055, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-14 02:37:10.220' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10124, 10055, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-14 11:16:17.330' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10125, 10064, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-14 11:59:35.143' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10126, 10064, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-14 12:01:43.677' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10127, 10063, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-14 12:09:23.893' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10128, 10055, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-14 12:09:27.817' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10129, 10055, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-14 12:09:34.567' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10130, 10063, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-14 12:09:38.160' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10131, 10064, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-14 12:09:42.863' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10132, 10063, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-14 13:28:09.630' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10133, 10064, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-14 13:28:20.803' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10134, 10064, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-14 13:28:30.647' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10135, 10065, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-14 14:08:20.883' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10136, 10065, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-14 14:08:27.587' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10137, 10065, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-14 14:09:30.787' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10138, 10065, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-14 14:11:26.553' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10139, 10065, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-14 14:13:08.447' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10140, 10066, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-14 14:16:13.507' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10141, 10067, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-14 14:20:42.507' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10142, 10068, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-14 14:26:14.040' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10143, 10068, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-14 14:30:41.523' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10144, 10068, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-14 14:31:11.430' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10145, 10068, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-14 16:24:07.153' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10146, 10068, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-15 07:37:25.243' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10147, 10068, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-15 09:37:00.183' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10148, 10068, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-15 09:38:14.340' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10149, 10068, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-15 09:39:37.653' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10150, 10068, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-15 09:47:29.123' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10151, 10068, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-15 09:50:16.310' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10152, 10068, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-15 09:53:56.293' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10153, 10068, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-15 09:55:37.590' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10154, 10068, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-15 09:56:15.433' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10155, 10068, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-15 09:57:18.933' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10156, 10068, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-15 11:36:40.703' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10157, 10066, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-15 11:50:34.987' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10158, 10069, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-15 11:53:38.377' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10159, 10069, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-15 11:53:49.547' AS DateTime))
INSERT [dbo].[Events_Hit] ([EventHit_Id], [EventHit_EventId], [EventHit_Hits], [EventHitDateTime]) VALUES (10160, 10070, CAST(1 AS Decimal(18, 0)), CAST(N'2016-03-15 12:00:51.970' AS DateTime))
SET IDENTITY_INSERT [dbo].[Events_Hit] OFF
SET IDENTITY_INSERT [dbo].[EventSubCategory] ON 

INSERT [dbo].[EventSubCategory] ([EventSubCategoryID], [EventCategoryID], [EventSubCategory]) VALUES (1, 19, N'test 1')
SET IDENTITY_INSERT [dbo].[EventSubCategory] OFF
SET IDENTITY_INSERT [dbo].[EventTempImage] ON 

INSERT [dbo].[EventTempImage] ([UserId], [EvenUniqueid], [EventImageUrl], [ImageType], [Id]) VALUES (N'45cf7381-096e-4421-88ad-865fcb7a7292', N'a1f16d96-9d8a-41e1-8f8c-905d0b90f6cd', N'Desert.jpg', N'image/jpeg', 8)
INSERT [dbo].[EventTempImage] ([UserId], [EvenUniqueid], [EventImageUrl], [ImageType], [Id]) VALUES (N'45cf7381-096e-4421-88ad-865fcb7a7292', N'03aba668-9ba2-4518-92e5-b8c1d95475d6', N'Desert.jpg', N'image/jpeg', 9)
INSERT [dbo].[EventTempImage] ([UserId], [EvenUniqueid], [EventImageUrl], [ImageType], [Id]) VALUES (N'45cf7381-096e-4421-88ad-865fcb7a7292', N'03aba668-9ba2-4518-92e5-b8c1d95475d6', N'Desert.jpg', N'image/jpeg', 10)
INSERT [dbo].[EventTempImage] ([UserId], [EvenUniqueid], [EventImageUrl], [ImageType], [Id]) VALUES (N'24556b52-131a-46ad-b6a8-2897417b40c0', N'74c5cd74-20cf-419e-804d-1482e4b93f5d', N'Desert.jpg', N'image/jpeg', 11)
INSERT [dbo].[EventTempImage] ([UserId], [EvenUniqueid], [EventImageUrl], [ImageType], [Id]) VALUES (N'24556b52-131a-46ad-b6a8-2897417b40c0', N'5bff4b3b-5ae3-437e-957b-e2e1207dbf00', N'Desert.jpg', N'image/jpeg', 12)
INSERT [dbo].[EventTempImage] ([UserId], [EvenUniqueid], [EventImageUrl], [ImageType], [Id]) VALUES (N'24556b52-131a-46ad-b6a8-2897417b40c0', N'4f7b8394-196a-40a0-8c68-23458275788a', N'Desert.jpg', N'image/jpeg', 13)
INSERT [dbo].[EventTempImage] ([UserId], [EvenUniqueid], [EventImageUrl], [ImageType], [Id]) VALUES (N'24556b52-131a-46ad-b6a8-2897417b40c0', N'4f7b8394-196a-40a0-8c68-23458275788a', N'Desert.jpg', N'image/jpeg', 14)
INSERT [dbo].[EventTempImage] ([UserId], [EvenUniqueid], [EventImageUrl], [ImageType], [Id]) VALUES (N'24556b52-131a-46ad-b6a8-2897417b40c0', N'4f7b8394-196a-40a0-8c68-23458275788a', N'Desert.jpg', N'image/jpeg', 15)
INSERT [dbo].[EventTempImage] ([UserId], [EvenUniqueid], [EventImageUrl], [ImageType], [Id]) VALUES (N'24556b52-131a-46ad-b6a8-2897417b40c0', N'4f7b8394-196a-40a0-8c68-23458275788a', N'Hydrangeas.jpg', N'image/jpeg', 16)
INSERT [dbo].[EventTempImage] ([UserId], [EvenUniqueid], [EventImageUrl], [ImageType], [Id]) VALUES (N'24556b52-131a-46ad-b6a8-2897417b40c0', N'4f7b8394-196a-40a0-8c68-23458275788a', N'Jellyfish.jpg', N'image/jpeg', 17)
INSERT [dbo].[EventTempImage] ([UserId], [EvenUniqueid], [EventImageUrl], [ImageType], [Id]) VALUES (N'24556b52-131a-46ad-b6a8-2897417b40c0', N'f21bffdd-7bc0-4806-9b36-387ac74151d3', N'Desert.jpg', N'image/jpeg', 18)
INSERT [dbo].[EventTempImage] ([UserId], [EvenUniqueid], [EventImageUrl], [ImageType], [Id]) VALUES (N'24556b52-131a-46ad-b6a8-2897417b40c0', N'f8a3bf69-7b39-47a7-8382-88007398d162', N'Desert.jpg', N'image/jpeg', 19)
SET IDENTITY_INSERT [dbo].[EventTempImage] OFF
SET IDENTITY_INSERT [dbo].[EventType] ON 

INSERT [dbo].[EventType] ([EventTypeID], [EventType], [EventHide]) VALUES (1, N'DJ Party', N'Y')
INSERT [dbo].[EventType] ([EventTypeID], [EventType], [EventHide]) VALUES (2, N'Company Party modified', N'Y')
INSERT [dbo].[EventType] ([EventTypeID], [EventType], [EventHide]) VALUES (42, N'Musical Concert', N'Y')
INSERT [dbo].[EventType] ([EventTypeID], [EventType], [EventHide]) VALUES (58, N'Event Test Type', NULL)
INSERT [dbo].[EventType] ([EventTypeID], [EventType], [EventHide]) VALUES (72, N'Test Event type', N'Y')
INSERT [dbo].[EventType] ([EventTypeID], [EventType], [EventHide]) VALUES (116, N'Medical Updated', NULL)
INSERT [dbo].[EventType] ([EventTypeID], [EventType], [EventHide]) VALUES (117, N'Sports Collection', NULL)
SET IDENTITY_INSERT [dbo].[EventType] OFF
SET IDENTITY_INSERT [dbo].[EventVenue] ON 

INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (4, 10055, 0, N'03/07/2016', N'03/31/2016', N'7:00pm', N'7:00pm')
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (13, 10064, 0, N'03/14/2016', N'03/31/2016', N'7:00pm', N'7:00pm')
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (14, 10063, 0, N'03/14/2016', N'03/31/2016', N'7:00pm', N'7:00pm')
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (15, 10065, 0, N'03/14/2016', N'03/14/2016', N'7:00pm', N'7:00pm')
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (16, 10066, 0, N'03/14/2016', N'03/16/2016', N'7:00am', N'11:00pm')
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (17, 10067, 0, N'03/14/2016', N'03/16/2016', N'6:00am', N'7:30pm')
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (18, 10068, 0, N'03/14/2016', N'03/16/2016', N'6:00am', N'7:30pm')
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (19, 10069, 0, N'03/15/2016', N'03/31/2016', N'7:00pm', N'7:00pm')
INSERT [dbo].[EventVenue] ([EventVenueID], [EventID], [AddressId], [EventStartDate], [EventEndDate], [EventStartTime], [EventEndTime]) VALUES (20, 10070, 0, N'03/15/2016', N'03/16/2016', N'7:00pm', N'7:00pm')
SET IDENTITY_INSERT [dbo].[EventVenue] OFF
SET IDENTITY_INSERT [dbo].[Fee_Structure] ON 

INSERT [dbo].[Fee_Structure] ([FS_Id], [FS_Type], [FS_Amount], [FS_Percentage], [FS_Apply]) VALUES (1, N'Fee', CAST(0.99 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), N'A')
SET IDENTITY_INSERT [dbo].[Fee_Structure] OFF
SET IDENTITY_INSERT [dbo].[Messages] ON 

INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1, N'MyAccount', N'MyAccountEmailRequiredUI', N'Email Required.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (2, N'MyAccount', N'MyAccountEmailValidationUI', N'Invalid Email .')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (4, N'MyAccount', N'MyAccountSuccessInitSY', N'Your details has been saved.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (5, N'MyAccount', N'MyAccountSuccessEmailSY', N'Your email has been saved. ')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (6, N'MyAccount', N'MyAccountSuccessPasswordSY', N'Your Password has been saved. ')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (7, N'Login', N'LoginEmailValidationUI', N'Invalid Email .')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (8, N'Login', N'LoginPasswordValidationUI', N'Invalid Password.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (9, N'Login', N'LoginEmailRequiredUI', N'Email  Required.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (10, N'Login', N'LoginEmailNotExistSy', N'Email  not present in database.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (11, N'Signup', N'SignupEmailValidationUI', N'Invalid Email .')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (12, N'Signup', N'SignupPasswordValidationUI', N'Invalid Password.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (13, N'Signup', N'SignupEmailAlreadyExistSY', N'Email Already Exist.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1011, N'TicketPayment', N'TenMinWindowExpires', N'Sorry, you took too long and we have to release your order. Please try again.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1012, N'Login', N'LoginPwdNotExistSy', N'Password not matching!Use forgot pasword!')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1013, N'MyAccount', N'MyAccountFnameRequiredUI', N'Please Enter First name.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1015, N'MyAccount', N'MyAccountPasswordValidationUI', N'Invalid Password.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1018, N'MyAccount', N'MyAccountDateValidationUI', N'Invalid Date.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1019, N'MyAccount', N'MyAccountZipValidationUI', N'Invalid Zip Code.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1020, N'MyAccount', N'MyAccountImagesizeValidationUI', N'Image More than 2 MB not allowed.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1021, N'MyAccount', N'MyAccountImageCountValidationUI', N'Only 1 image allowed.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1022, N'MyAccount', N'MyAccountEmailmatchValidationSy', N'Email and email verification doesn''t match.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1023, N'MyAccount', N'MyAccountPwdmatchValidationSy', N'New password and confirm new password doesn''t match.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1024, N'MyAccount', N'MyAccountPwdValidationSys', N'Invalid current password.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1025, N'MyAccount', N'MyAccountEmailAlreadyExistSY', N'Email already exist in our system choose different email')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1026, N'ForgotPassword', N'FrgtpwdEmailValidationUI', N'Invalid Email .')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1027, N'ForgotPassword', N'FrgtpwdEmailValidationSy', N'Email Not Found in our Database')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1028, N'ForgotPassword', N'ForgotPwdSuccessInitSY', N'Please check your email for password set link')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1029, N'ResetPassword', N'PwdResetPwdValidationUI', N'Invalid Password.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1030, N'ResetPassword', N'PwdResetPwdValidationSys', N'Password and confirm password doesn''t match')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1031, N'ResetPassword', N'PwdResetSuccessInitSY', N'Password reset successfully.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1032, N'CreateEvent', N'CreateEventTitileUI', N'Please enter the name of event.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1033, N'CreateEvent', N'CreateEventOrganizerUI', N'Please enter organizer name.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1034, N'CreateEvent', N'CreateEventtypeUI', N'Please select type of event.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1035, N'CreateEvent', N'CreateEventCategoryUI', N'Please select category of event')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1036, N'CreateEvent', N'CreateEventHighlightFieldsUI', N'Please fix the highlighted fields')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1037, N'CreateEvent', N'CreateEventvariabledescUI', N'Please enter description for variable charges')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1038, N'CreateEvent', N'CreateEventPwdUI', N'Please enter password for view event')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1039, N'CreateEvent', N'CreateEventCompareDateUI', N'Start Date can not be greater than End Date')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1040, N'CreateEvent', N'CreateEventInvalidTimeUI', N'Invalid time format')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1041, N'CreateEvent', N'CreateEventInvalidDateUI', N'Invalid date format')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1042, N'CreateEvent', N'CreateEventValidatevenueUI', N'Please enter valid venue name')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1043, N'CreateEvent', N'CreateEventurlexistUI', N'Event Url already exists.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1044, N'Messages', N'MessageReqUI', N'All Fields are required')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1045, N'Messages', N'MessageSuccSY', N'All Messages are saved successfully')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1046, N'MyAccount', N'MyAccountImagestypeValidationUI', N'Only files with extension jpg,.png,.jpeg are allowed to be uploaded.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1047, N'CreateEvent', N'CreateEventImageTypeUI', N'Only files with extension jpg,.png,.jpeg are allowed to be uploaded.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1048, N'CreateEvent', N'CreateEventImageSizeUI', N'Image More than 2 MB not allowed.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1049, N'CreateEvent', N'CreateEventImagecountUI', N'Only 5 image allowed.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1050, N'CreateEvent', N'CreateEventOrganizercountUI', N'Limit Exceeded, System Allow Only 25 Orgnizer.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1051, N'CreateEvent', N'CreateEventProbleminApllUI', N'Sorry there is some problem.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1052, N'CreateEvent', N'CreateEventEnternumberUI', N'Please enter number')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1053, N'CreateEvent', N'CreateEventDeleteUI', N'Are you sure you want to delete it?')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1054, N'CMSUsers', N'UsersOrgMerStatusSucc', N'Record updated Successfully.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1055, N'CreateEvent', N'CreateEventsavedsucc', N'Congratulations, your event is on Eventcombo!')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1056, N'CMSUsers', N'UsersDeleteUI', N'Are you sure you want to delete it ?')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1058, N'EventConfirmation', N'Evtconfirmsucc', N'Congratulation Your Event is Live now')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1059, N'ViewEvent', N'ViewEventExpiredSy', N'Your Event has Ended')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1060, N'ViewEvent', N'ViewEventNoQtyUI', N'Please Select Atleast one qty')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1061, N'TicketPayment', N'TPayValidateEmailUI', N'Invalid Email')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1062, N'TicketPayment', N'TPayValidateConfirmMatchEmailUI', N'Confirm Email Doesnot match Email')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1063, N'TicketPayment', N'TPayValidateconfirmPwdMatchUI', N'Confirm Password Doesnot match Password')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1064, N'TicketPayment', N'TPayCardValidationUI', N'Invalid Card Number')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1065, N'TicketPayment', N'TPayValidateBillingAddressUI', N'Please Fill the Billing Address Details')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1070, N'TicketPayment', N'TPayValidateShippingAddressUI', N'Please Fill the Shipping Address Details')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1071, N'TicketPayment', N'TPayValidateEmailSy', N'Email already exist,Please use Sign in')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1072, N'TicketPayment', N'TPayValidatebearernameUI', N'Please enter valid Name')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1073, N'TicketPayment', N'TPayValidatebeareremailUI', N'Please enter valid Email')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1074, N'TicketPayment', N'TPayValidatePwdUI', N'Invalid Password')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1075, N'TicketPayment', N'TPayFnameValidateUI', N'Please Enter Name')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1076, N'CreateEvent', N'CreateEventQtyValidateUI', N'Ticket Qty cannot be less than equal to zero')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1077, N'Email', N'EmailSuccessSy', N'Template Saved Successfully')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1080, N'ViewEvent', N'OverSelling', N'One of your selected ticket has been already sold.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1081, N'ViewEvent', N'EventNotLive', N'You cannot purchase ticket as this event is not live.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1082, N'ViewEvent', N'VoteOnce', N'You can only vote once.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1083, N'ViewEvent', N'ThanksForVote', N'Thank you for voting!')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1084, N'ViewEvent', N'eventSaved', N'Your Event have been saved!')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1085, N'ViewEvent', N'eventnotsaved', N'Your Event have not been saved!')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1086, N'CreateEvent', N'CreateEventSubDescUI', N'Please Enter Variable Sub Description')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1087, N'CreateEvent', N'CreateEventSubQtyUI', N'Variable Sub qty can''t be zero')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1088, N'CMSUsers', N'NoEvents', N'User has not created any event')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1089, N'CreateEvent', N'PriceNotzeroUI', N'Invalid Price')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1090, N'CreateEvent', N'NoTicketUI', N'Select atleast one ticket')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1091, N'CreateEvent', N'Validdate', N'Please add valid date and time')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1092, N'EventType', N'EventtypeDeleteUI', N'Are you sure you want to delete it ?')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1093, N'EventCatogary', N'EventCatDeleteUi', N'Are you sure you want to delete it ?')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1094, N'CreateEvent', N'CreateeventUpdated', N'Your Event is successfully updated!!')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1095, N'MyAccount', N'AccEmailPwdchangesys', N'Your Email and Password has been saved. ')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1096, N'TicketPayment', N'TpayMaxTicketReciepts', N'Maximum limit to add ticket recepients:')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1097, N'ManageEvent', N'MEPublisheventSucc', N'Congratulations! Your event is live now.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1098, N'ManageEvent', N'MEDeleteEvent', N'Are you sure you want to delete this event? This cannot be undone')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1099, N'ManageEvent', N'MEunPublish', N'Are you sure you want to unpublish event?')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1100, N'ManageEvent', N'MECancel', N'Are you sure you want to cancel the event?')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1101, N'ManageEvent', N'MEnterUrlUI', N'Url cannot be empty!!')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1102, N'PaymentInfo', N'PICompanynameUI', N'Please Enter Company name')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1103, N'PaymentInfo', N'PInameUI', N'Please Enter Name')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1104, N'PaymentInfo', N'PIAddressUI', N'Please Enter Address Details')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1105, N'PaymentInfo', N'PIBankNameUI', N'Please enter bank name')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1106, N'PaymentInfo', N'PIRoutingnoUI', N'Please enter routing number')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1107, N'PaymentInfo', N'PIAccountinfoUI', N'Please Enter Account info')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1108, N'PaymentInfo', N'PIPayeeUI', N'Please Enter Payee name')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1109, N'PaymentInfo', N'PIAccountinfocompUI', N'Account and Re account number not same')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1110, N'FeeSetting', N'FSRequiredUI', N'Please Fill the required fields')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1111, N'FeeSetting', N'FSSuccess', N'Saved Successfully')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1113, N'OrganizerMaster', N'OrgNameUi', N'Please enter name')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1114, N'OrganizerMaster', N'OrgEmailUi', N'Please enter email')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1115, N'OrganizerMaster', N'OrgvalidEmailUi', N'Please enter valid email')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1116, N'OrganizerMaster', N'OrgvalidZipUi', N'Please enter valid zip/pin code')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1117, N'OrganizerMaster', N'OrgsaveSucc', N'Congratulation!!Organizer saved successfully')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1118, N'OrganizerMaster', N'OrgsaveEdit', N'Congratulation!!Organizer updated successfully')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1119, N'OrganizerMaster', N'OrgDelete', N'Organiser deleted successfully')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1120, N'OrganizerMaster', N'OrgDeleteMessage', N'Do you want to delete the organizer!')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1121, N'OrganizerMaster', N'OrgImagesizeValidationUI', N'Image More than 2 MB not allowed.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1122, N'OrganizerMaster', N'OrgImageCountValidationUI', N'Only 1 image allowed.')
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1123, N'OrganizerMaster', N'OrgImagestypeValidationUI', N'Only files with extension jpg,.png,.jpeg are allowed to be uploaded.')
SET IDENTITY_INSERT [dbo].[Messages] OFF
SET IDENTITY_INSERT [dbo].[Order_Detail_T] ON 

INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (2, N'T000000002', N'800f9add-e483-4c68-b7ec-528c742e5407', CAST(65.87 AS Numeric(18, 2)), CAST(43.65 AS Numeric(18, 2)), N'3', CAST(22.22 AS Numeric(18, 2)), 0, CAST(N'2016-03-07 07:38:11.127' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (3, N'T000000003', N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'', CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-03-14 14:17:12.117' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (4, N'T000000004', N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', CAST(24.98 AS Numeric(18, 2)), CAST(24.98 AS Numeric(18, 2)), N'', CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-03-14 14:29:44.680' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (5, N'T000000005', N'800f9add-e483-4c68-b7ec-528c742e5407', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'', CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-03-15 09:37:47.667' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (6, N'T000000006', N'800f9add-e483-4c68-b7ec-528c742e5407', CAST(11.49 AS Numeric(18, 2)), CAST(11.49 AS Numeric(18, 2)), N'', CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-03-15 09:39:13.120' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (7, N'T000000007', N'800f9add-e483-4c68-b7ec-528c742e5407', CAST(11.49 AS Numeric(18, 2)), CAST(11.49 AS Numeric(18, 2)), NULL, CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-03-15 09:49:11.013' AS DateTime), N'EC-1G470730BX012680V', N'3V4P29FJC94V6', N'5CA21525RM876410B')
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (8, N'T000000008', N'800f9add-e483-4c68-b7ec-528c742e5407', CAST(11.49 AS Numeric(18, 2)), CAST(11.49 AS Numeric(18, 2)), NULL, CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-03-15 09:55:28.170' AS DateTime), N'EC-7GA14507XW8344005', N'3V4P29FJC94V6', N'06710421A3092841S')
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (9, N'T000000009', N'800f9add-e483-4c68-b7ec-528c742e5407', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'', CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-03-15 09:56:01.013' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (10, N'T000000010', N'800f9add-e483-4c68-b7ec-528c742e5407', CAST(11.49 AS Numeric(18, 2)), CAST(11.49 AS Numeric(18, 2)), N'', CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-03-15 09:57:06.137' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (11, N'T000000011', N'800f9add-e483-4c68-b7ec-528c742e5407', CAST(11.49 AS Numeric(18, 2)), CAST(11.49 AS Numeric(18, 2)), N'', CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-03-15 09:58:31.793' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Order_Detail_T] ([O_Id], [O_Order_Id], [O_User_Id], [O_TotalAmount], [O_OrderAmount], [O_VariableId], [O_VariableAmount], [O_PromoCodeId], [O_OrderDateTime], [O_PayPal_TokenId], [O_PayPal_PayerId], [O_PayPal_TrancId]) VALUES (12, N'T000000012', N'909e0dae-2f54-4fe6-84da-c1a810ee7ab7', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'', CAST(0.00 AS Numeric(18, 2)), 0, CAST(N'2016-03-15 11:53:17.703' AS DateTime), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Order_Detail_T] OFF
SET IDENTITY_INSERT [dbo].[Organizer_Master] ON 

INSERT [dbo].[Organizer_Master] ([Orgnizer_Id], [Orgnizer_Name], [Organizer_Desc], [Organizer_FBLink], [Organizer_Twitter], [Organizer_Linkedin], [UserId], [Organizer_Image], [Organizer_Address1], [Organizer_Address2], [Organizer_City], [Organizer_State], [Organizer_Zipcode], [Organizer_CountryId], [Organizer_Email], [Organizer_Phoneno], [Organizer_Websiteurl], [Organizer_Status], [contenttype], [Imagepath]) VALUES (1, N'ShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetavShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShweta', N' ShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetavShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetavShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetavShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetavShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetavShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShweta ShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetavShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShweta  ShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetavShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShweta ShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetavShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShweta  ShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetavShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShweta ShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetavShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShweta  ShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetavShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShweta ShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetavShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShweta ShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetavShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShweta ', N'ShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetavShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShweta', N'ShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetavShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShweta', N'ShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetavShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShweta', N'800f9add-e483-4c68-b7ec-528c742e5407', N'OrgImage_37b02240.Jpeg', N'ShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetavShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetavShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaSh', N'ShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetavShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShweta', N'ShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetavShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShweta', N'ShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetavShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShweta', N'Shwet', 1, N'Shweta@gmail.com', N'3333333', N'ShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetavShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShwetaShweta', N'A', N'image/jpeg', N'/Images/Organizer/Organizer_Images/OrgImage_37b02240.Jpeg')
INSERT [dbo].[Organizer_Master] ([Orgnizer_Id], [Orgnizer_Name], [Organizer_Desc], [Organizer_FBLink], [Organizer_Twitter], [Organizer_Linkedin], [UserId], [Organizer_Image], [Organizer_Address1], [Organizer_Address2], [Organizer_City], [Organizer_State], [Organizer_Zipcode], [Organizer_CountryId], [Organizer_Email], [Organizer_Phoneno], [Organizer_Websiteurl], [Organizer_Status], [contenttype], [Imagepath]) VALUES (9, N'no', N'no', NULL, NULL, NULL, N'800f9add-e483-4c68-b7ec-528c742e5407', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'A', NULL, NULL)
INSERT [dbo].[Organizer_Master] ([Orgnizer_Id], [Orgnizer_Name], [Organizer_Desc], [Organizer_FBLink], [Organizer_Twitter], [Organizer_Linkedin], [UserId], [Organizer_Image], [Organizer_Address1], [Organizer_Address2], [Organizer_City], [Organizer_State], [Organizer_Zipcode], [Organizer_CountryId], [Organizer_Email], [Organizer_Phoneno], [Organizer_Websiteurl], [Organizer_Status], [contenttype], [Imagepath]) VALUES (10, N'PiyushGupta', N'%3Cp%3E%3Cbr%3E%3C/p%3E', NULL, NULL, NULL, N'0cb998d7-c1e1-4019-8381-45132347d9af', N'', N'Sector-3', N'Noida', N'Columbia', N'Maryland', N'21045', 1, N'piyush.gupta@kiwitech.com', N'123-456-7890', N'http://eventcombonew-qa.kiwireader.com/', N'A', N'', N'')
INSERT [dbo].[Organizer_Master] ([Orgnizer_Id], [Orgnizer_Name], [Organizer_Desc], [Organizer_FBLink], [Organizer_Twitter], [Organizer_Linkedin], [UserId], [Organizer_Image], [Organizer_Address1], [Organizer_Address2], [Organizer_City], [Organizer_State], [Organizer_Zipcode], [Organizer_CountryId], [Organizer_Email], [Organizer_Phoneno], [Organizer_Websiteurl], [Organizer_Status], [contenttype], [Imagepath]) VALUES (11, N'Piyush', N'%3Cp%3E%3Cbr%3E%3C/p%3E', NULL, NULL, NULL, N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', N'OrgImage_d1ce2f96.Jpeg', N'Noida', N'Sector-3', N'Columbia', N'Maryland', N'21045', 1, N'guptapiyush21@gmail.com', N'123-456-4567', N'http://eventcombonew-qa.kiwireader.com/', N'A', N'image/jpeg', N'/Images/Organizer/Organizer_Images/OrgImage_d1ce2f96.Jpeg')
INSERT [dbo].[Organizer_Master] ([Orgnizer_Id], [Orgnizer_Name], [Organizer_Desc], [Organizer_FBLink], [Organizer_Twitter], [Organizer_Linkedin], [UserId], [Organizer_Image], [Organizer_Address1], [Organizer_Address2], [Organizer_City], [Organizer_State], [Organizer_Zipcode], [Organizer_CountryId], [Organizer_Email], [Organizer_Phoneno], [Organizer_Websiteurl], [Organizer_Status], [contenttype], [Imagepath]) VALUES (12, N'Piyush123', N'Desc', NULL, NULL, NULL, N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'A', NULL, NULL)
INSERT [dbo].[Organizer_Master] ([Orgnizer_Id], [Orgnizer_Name], [Organizer_Desc], [Organizer_FBLink], [Organizer_Twitter], [Organizer_Linkedin], [UserId], [Organizer_Image], [Organizer_Address1], [Organizer_Address2], [Organizer_City], [Organizer_State], [Organizer_Zipcode], [Organizer_CountryId], [Organizer_Email], [Organizer_Phoneno], [Organizer_Websiteurl], [Organizer_Status], [contenttype], [Imagepath]) VALUES (13, N'hun', N'asdasd', NULL, NULL, NULL, N'800f9add-e483-4c68-b7ec-528c742e5407', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'A', NULL, NULL)
INSERT [dbo].[Organizer_Master] ([Orgnizer_Id], [Orgnizer_Name], [Organizer_Desc], [Organizer_FBLink], [Organizer_Twitter], [Organizer_Linkedin], [UserId], [Organizer_Image], [Organizer_Address1], [Organizer_Address2], [Organizer_City], [Organizer_State], [Organizer_Zipcode], [Organizer_CountryId], [Organizer_Email], [Organizer_Phoneno], [Organizer_Websiteurl], [Organizer_Status], [contenttype], [Imagepath]) VALUES (14, N'Piyush', N'Test Engineer', N'https://www.facebook.com/piyush.gupta.39904', NULL, NULL, N'909e0dae-2f54-4fe6-84da-c1a810ee7ab7', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'A', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Organizer_Master] OFF
SET IDENTITY_INSERT [dbo].[Permission_Detail] ON 

INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (1, N'Event', N'APP')
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (2, N'Deal', N'APP')
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (3, N'Users', N'CMS')
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (4, N'Events', N'CMS')
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (5, N'Tickets', N'CMS')
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (6, N'EMails', N'CMS')
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (7, N'Messages', N'CMS')
SET IDENTITY_INSERT [dbo].[Permission_Detail] OFF
SET IDENTITY_INSERT [dbo].[Profile] ON 

INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10139, N'800f9add-e483-4c68-b7ec-528c742e5407', N'shweta', N'sindhu', N'just.shweta29@gmail.com', NULL, NULL, N'Louisville', N'Kentucky', N'sdf', 1, N'222-222-2222', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'India', N'National Capital Territory of Delhi', N'New Delhi')
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10140, N'3fd06a6a-9b9c-46a4-84db-e2f5f5168ad6', NULL, NULL, N'krk@outlook.in', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'India', N'National Capital Territory of Delhi', N'New Delhi')
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10141, N'57af65d8-f739-4e09-a39e-f7b0ae2715f0', NULL, NULL, N'shweta.sindhu123567@kiwitech.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'', N'', N'')
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10142, N'766ab684-8ea6-4ff7-b09c-1a99cec69e22', NULL, NULL, N'apavlov@gmail.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'United States', N'Vermont', N'Winooski')
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10143, N'9288c7f0-e234-4489-971d-e8d029ededcc', NULL, NULL, N'kesarwanirohit@rocketmail.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'', N'', N'')
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10144, N'df32bef3-022e-4c62-a695-dfa21892321f', NULL, NULL, N'rohit.kesarwani@kiwitech.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'India', N'', N'')
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10145, N'a06cd8b0-1d3e-4dfe-92d4-0d4bc62a5076', NULL, NULL, N'sarooshgull@gmail.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'India', N'National Capital Territory of Delhi', N'New Delhi')
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10146, N'2ecda86e-1d74-4cc4-b05b-97dba9111ac0', NULL, NULL, N'saroosh@eventcombo.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'India', N'National Capital Territory of Delhi', N'New Delhi')
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10147, N'a60e2f31-b772-428e-9bf5-2c5e6dcd56d7', NULL, NULL, N'editor@eventcombo.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'India', N'National Capital Territory of Delhi', N'New Delhi')
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10148, N'909e0dae-2f54-4fe6-84da-c1a810ee7ab7', N'Piyush', N'GUpta', N'piyush.gupta@kiwitech.com', NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'India', N'', N'')
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10149, N'8dee8fd1-a0e0-403f-9c19-f2e271f0444c', NULL, NULL, N'pintu.sah@kiwitech.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'', N'', N'')
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10150, N'd60e166b-0713-475c-8ca4-9d749aed86f3', NULL, NULL, N'kannan.k@kiwitech.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'India', N'National Capital Territory of Delhi', N'New Delhi')
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10151, N'250d1f3f-6bac-463d-9ae6-53bd8dda317b', NULL, NULL, N'shweta.sindu@kiwitech.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'', N'', N'')
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10152, N'ab798d10-5c11-4c4e-b206-6da5ac9045f1', NULL, NULL, N'navrit.singh@kiwitech.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'India', N'National Capital Territory of Delhi', N'New Delhi')
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10153, N'17f0863a-156f-4d34-bea3-fd247d0c7d62', NULL, NULL, N'saurabh.bhardwaj@kiwitech.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'', N'', N'')
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10154, N'0aa92a22-0b7a-4055-bc2b-8ff72a92adf1', NULL, NULL, N'mayank.srivastava@kiwitech.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'India', N'National Capital Territory of Delhi', N'New Delhi')
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10155, N'0c2935d4-cfaf-4792-8569-a07d624b762f', NULL, NULL, N'fun@ickld.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'United States', N'New Jersey', N'Hackensack')
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10156, N'0cb998d7-c1e1-4019-8381-45132347d9af', N'Piyush', N'Gupta', N'piyush.gupta123@kiwitech.com', N'Noida', N'Sector-76', N'Noida', N'Uttar Pradesh', N'201301', 1, NULL, N'858-783-1233', NULL, NULL, NULL, NULL, NULL, N'', NULL, NULL, NULL, NULL, NULL, NULL, N'', N'Male', N'21-8-1989', NULL, NULL, N'Y', NULL, N'', N'', N'')
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10157, N'8dcc551e-dd63-4130-a138-21ed5ce3ecaa', NULL, NULL, N'piyush.gupta12@kiwitech.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'', N'', N'')
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10158, N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', N'Piyush', N'Gupta', N'guptapiyush21@gmail.com', N'Sector-3', N'Noida', NULL, NULL, N'21045', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1_ProfImage6.Jpeg', NULL, NULL, NULL, NULL, NULL, NULL, N'image/jpeg', N'Male', N'21-8-1989', NULL, NULL, N'Y', NULL, N'India', N'National Capital Territory of Delhi', N'New Delhi')
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10159, N'd8757720-1948-4fe5-8d55-0aa7530a82f1', NULL, NULL, N'mckeonc@seas.upenn.edu', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'', N'', N'')
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus], [SendCur_EventDetail], [Ipcountry], [IpState], [Ipcity]) VALUES (10160, N'e25f3b4b-4c09-43df-82a0-c72054417035', NULL, NULL, N'mayank.srivastav@kiwitech.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', NULL, N'India', N'', N'')
SET IDENTITY_INSERT [dbo].[Profile] OFF
SET IDENTITY_INSERT [dbo].[Publish_Event_Detail] ON 

INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (4, 10055, N'70', 0, 4, N'2,3', N'Mar 07, 2016', N'7:00pm', N'7:00pm')
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (6, 10064, N'79', 0, 13, N'5', N'Mar 14, 2016', N'7:00pm', N'7:00pm')
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (7, 10063, N'80', 0, 14, N'4', N'Mar 14, 2016', N'7:00pm', N'7:00pm')
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (8, 10065, N'81', 0, 15, N'6,7,8', N'Mar 14, 2016', N'7:00pm', N'7:00pm')
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (9, 10066, N'82,83', 0, 16, N'9', N'Mar 14, 2016', N'7:00am', N'11:00pm')
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (10, 10067, N'84,85', 0, 17, N'10', N'Mar 14, 2016', N'6:00am', N'7:30pm')
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (11, 10068, N'86,87', 0, 18, N'11,12,13', N'Mar 14, 2016', N'6:00am', N'7:30pm')
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (12, 10069, N'88', 0, 19, N'14', N'Mar 15, 2016', N'7:00pm', N'7:00pm')
INSERT [dbo].[Publish_Event_Detail] ([PE_Id], [PE_Event_Id], [PE_Address_Ids], [PE_MultipleVenue_id], [PE_SingleVenue_Id], [PE_Tickets_Ids], [PE_Scheduled_Date], [PE_Start_Time], [PE_End_Time]) VALUES (13, 10070, N'89', 0, 20, N'15,16,17', N'Mar 15, 2016', N'7:00pm', N'7:00pm')
SET IDENTITY_INSERT [dbo].[Publish_Event_Detail] OFF
SET IDENTITY_INSERT [dbo].[ShippingAddress] ON 

INSERT [dbo].[ShippingAddress] ([ShippingId], [UserId], [OrderId], [Guid], [Fname], [Lname], [Phone_Number], [Address1], [Address2], [City], [State], [Zip], [Country]) VALUES (1, N'800f9add-e483-4c68-b7ec-528c742e5407                                                                                            ', N'T000000002', N'0f5ba609-752b-4de4-b2c1-e3a69d0e2bc1                                                                                            ', N'sd', N'sad', N'222-222-2222', N'sdd', N'sfsdf', N'Louisville', N'Kentucky', N'sdf', N'1')
SET IDENTITY_INSERT [dbo].[ShippingAddress] OFF
SET IDENTITY_INSERT [dbo].[Status] ON 

INSERT [dbo].[Status] ([StatusID], [StatusName]) VALUES (1, N'Active')
INSERT [dbo].[Status] ([StatusID], [StatusName]) VALUES (2, N'Not Active')
INSERT [dbo].[Status] ([StatusID], [StatusName]) VALUES (3, N'Pending')
SET IDENTITY_INSERT [dbo].[Status] OFF
SET IDENTITY_INSERT [dbo].[Ticket] ON 

INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice], [T_Customize], [T_Ecpercent], [T_EcAmount]) VALUES (2, 10055, N'free', 23, NULL, 1, NULL, NULL, NULL, N'0', N'1', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, NULL, NULL, N'0', N'0', NULL, NULL, NULL, N'0', CAST(5.00 AS Decimal(9, 2)), CAST(0.99 AS Decimal(9, 2)))
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice], [T_Customize], [T_Ecpercent], [T_EcAmount]) VALUES (3, 10055, N'paid', 45, CAST(44.44 AS Decimal(9, 2)), 2, NULL, NULL, NULL, N'0', N'1', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 1, CAST(3.21 AS Decimal(9, 2)), CAST(3.21 AS Decimal(9, 2)), N'0', N'0', NULL, CAST(4.00 AS Decimal(9, 2)), CAST(47.65 AS Decimal(9, 2)), N'0', CAST(5.00 AS Decimal(9, 2)), CAST(0.99 AS Decimal(9, 2)))
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice], [T_Customize], [T_Ecpercent], [T_EcAmount]) VALUES (4, 10063, N'free', 20, NULL, 1, NULL, NULL, NULL, N'0', N'1', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, NULL, NULL, N'0', N'0', NULL, NULL, NULL, N'0', CAST(5.00 AS Decimal(9, 2)), CAST(0.99 AS Decimal(9, 2)))
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice], [T_Customize], [T_Ecpercent], [T_EcAmount]) VALUES (5, 10064, N'free', 20, NULL, 1, NULL, NULL, NULL, N'0', NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, NULL, NULL, N'0', N'0', NULL, NULL, NULL, N'0', CAST(5.00 AS Decimal(9, 2)), CAST(0.99 AS Decimal(9, 2)))
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice], [T_Customize], [T_Ecpercent], [T_EcAmount]) VALUES (6, 10065, N'Paid', 10, CAST(10.00 AS Decimal(9, 2)), 2, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(1.49 AS Decimal(9, 2)), CAST(1.49 AS Decimal(9, 2)), N'0', N'0', NULL, NULL, CAST(11.49 AS Decimal(9, 2)), N'0', NULL, NULL)
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice], [T_Customize], [T_Ecpercent], [T_EcAmount]) VALUES (7, 10065, N'Free', 10, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 1, NULL, NULL, N'0', N'0', NULL, NULL, NULL, N'0', NULL, NULL)
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice], [T_Customize], [T_Ecpercent], [T_EcAmount]) VALUES (8, 10065, N'Donate', 10, NULL, 3, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 2, NULL, NULL, N'0', N'0', NULL, NULL, NULL, N'0', NULL, NULL)
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice], [T_Customize], [T_Ecpercent], [T_EcAmount]) VALUES (9, 10066, N'Free', 10, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, NULL, NULL, N'0', N'0', NULL, NULL, NULL, N'0', NULL, NULL)
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice], [T_Customize], [T_Ecpercent], [T_EcAmount]) VALUES (10, 10067, N'paid', 10, CAST(10.00 AS Decimal(9, 2)), 2, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(1.49 AS Decimal(9, 2)), CAST(1.49 AS Decimal(9, 2)), N'0', N'0', NULL, NULL, CAST(11.49 AS Decimal(9, 2)), N'0', NULL, NULL)
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice], [T_Customize], [T_Ecpercent], [T_EcAmount]) VALUES (11, 10068, N'paid', 10, CAST(10.00 AS Decimal(9, 2)), 2, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, CAST(1.49 AS Decimal(9, 2)), CAST(1.49 AS Decimal(9, 2)), N'0', N'0', NULL, NULL, CAST(11.49 AS Decimal(9, 2)), N'0', CAST(5.00 AS Decimal(9, 2)), CAST(0.99 AS Decimal(9, 2)))
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice], [T_Customize], [T_Ecpercent], [T_EcAmount]) VALUES (12, 10068, N'Free', 10, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, N'7:00pm', NULL, N'7:00pm', N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 1, CAST(1.49 AS Decimal(9, 2)), CAST(1.49 AS Decimal(9, 2)), N'0', N'0', NULL, NULL, CAST(0.00 AS Decimal(9, 2)), N'0', CAST(5.00 AS Decimal(9, 2)), CAST(0.99 AS Decimal(9, 2)))
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice], [T_Customize], [T_Ecpercent], [T_EcAmount]) VALUES (13, 10068, N'Donate', 10, NULL, 3, NULL, NULL, NULL, N'0', N'0', NULL, N'7:00pm', NULL, N'7:00pm', N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 2, CAST(0.00 AS Decimal(9, 2)), CAST(0.00 AS Decimal(9, 2)), N'0', N'0', NULL, NULL, CAST(0.00 AS Decimal(9, 2)), N'0', CAST(5.00 AS Decimal(9, 2)), CAST(0.99 AS Decimal(9, 2)))
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice], [T_Customize], [T_Ecpercent], [T_EcAmount]) VALUES (14, 10069, N'free', 34, NULL, 1, NULL, NULL, NULL, N'0', NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, NULL, NULL, N'0', N'0', NULL, NULL, NULL, N'0', NULL, NULL)
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice], [T_Customize], [T_Ecpercent], [T_EcAmount]) VALUES (15, 10070, N'Free', 10, NULL, 1, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 0, NULL, NULL, N'0', N'0', NULL, NULL, NULL, N'0', NULL, NULL)
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice], [T_Customize], [T_Ecpercent], [T_EcAmount]) VALUES (16, 10070, N'Paid', 10, CAST(10.00 AS Decimal(9, 2)), 2, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 1, CAST(1.49 AS Decimal(9, 2)), CAST(1.49 AS Decimal(9, 2)), N'0', N'0', NULL, NULL, CAST(11.49 AS Decimal(9, 2)), N'0', NULL, NULL)
INSERT [dbo].[Ticket] ([T_Id], [E_Id], [T_name], [Qty_Available], [Price], [TicketTypeID], [T_Sold], [Registration_Recorded], [T_Desc], [Show_T_Desc], [Fees_Type], [Sale_Start_Date], [Sale_Start_Time], [Sale_End_Date], [Sale_End_Time], [Hide_Ticket], [Auto_Hide_Sche], [Hide_Untill_Date], [Hide_Untill_Time], [Hide_After_Date], [Hide_After_Time], [Min_T_Qty], [Max_T_Qty], [T_Disable], [T_Mark_SoldOut], [T_Sold_Qty], [T_order], [EC_Fee], [Customer_Fee], [T_Displayremaining], [T_AutoSechduleType], [Additional_Fee], [T_Discount], [TotalPrice], [T_Customize], [T_Ecpercent], [T_EcAmount]) VALUES (17, 10070, N'Donate', 10, NULL, 3, NULL, NULL, NULL, N'1', N'0', CAST(N'2016-03-17' AS Date), NULL, CAST(N'2016-03-17' AS Date), NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, 2, NULL, NULL, N'0', N'0', NULL, NULL, NULL, N'0', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Ticket] OFF
SET IDENTITY_INSERT [dbo].[Ticket_Locked_Detail] ON 

INSERT [dbo].[Ticket_Locked_Detail] ([TLD_Id], [TLD_User_Id], [TLD_Locked_Qty], [TLD_Event_Id], [TLD_TQD_Id], [Locktime], [TLD_GUID], [TLD_Donate], [TicketAmount]) VALUES (6, N'800f9add-e483-4c68-b7ec-528c742e5407', 1, 10055, 5, CAST(N'2016-03-07 07:39:03.547' AS DateTime), N'd4b2cf67-5906-4adf-8f2b-14eb72f73c66', CAST(0 AS Decimal(18, 0)), CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[Ticket_Locked_Detail] ([TLD_Id], [TLD_User_Id], [TLD_Locked_Qty], [TLD_Event_Id], [TLD_TQD_Id], [Locktime], [TLD_GUID], [TLD_Donate], [TicketAmount]) VALUES (7, N'800f9add-e483-4c68-b7ec-528c742e5407', 1, 10055, 6, CAST(N'2016-03-07 07:39:03.547' AS DateTime), N'd4b2cf67-5906-4adf-8f2b-14eb72f73c66', CAST(0 AS Decimal(18, 0)), CAST(43.65 AS Decimal(18, 2)))
INSERT [dbo].[Ticket_Locked_Detail] ([TLD_Id], [TLD_User_Id], [TLD_Locked_Qty], [TLD_Event_Id], [TLD_TQD_Id], [Locktime], [TLD_GUID], [TLD_Donate], [TicketAmount]) VALUES (14, N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', 2, 10067, 15, CAST(N'2016-03-14 14:20:58.757' AS DateTime), N'76dfb39b-df56-45e1-8105-b22e4821cdd0', CAST(0 AS Decimal(18, 0)), CAST(22.98 AS Decimal(18, 2)))
INSERT [dbo].[Ticket_Locked_Detail] ([TLD_Id], [TLD_User_Id], [TLD_Locked_Qty], [TLD_Event_Id], [TLD_TQD_Id], [Locktime], [TLD_GUID], [TLD_Donate], [TicketAmount]) VALUES (15, N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', 2, 10067, 16, CAST(N'2016-03-14 14:20:58.757' AS DateTime), N'76dfb39b-df56-45e1-8105-b22e4821cdd0', CAST(0 AS Decimal(18, 0)), CAST(22.98 AS Decimal(18, 2)))
INSERT [dbo].[Ticket_Locked_Detail] ([TLD_Id], [TLD_User_Id], [TLD_Locked_Qty], [TLD_Event_Id], [TLD_TQD_Id], [Locktime], [TLD_GUID], [TLD_Donate], [TicketAmount]) VALUES (24, N'', 1, 10068, 17, CAST(N'2016-03-14 16:24:35.717' AS DateTime), N'95b28b8e-63f0-4f21-afb0-0ba518c1bcc3', CAST(0 AS Decimal(18, 0)), CAST(11.49 AS Decimal(18, 2)))
INSERT [dbo].[Ticket_Locked_Detail] ([TLD_Id], [TLD_User_Id], [TLD_Locked_Qty], [TLD_Event_Id], [TLD_TQD_Id], [Locktime], [TLD_GUID], [TLD_Donate], [TicketAmount]) VALUES (27, N'800f9add-e483-4c68-b7ec-528c742e5407', 1, 10068, 17, CAST(N'2016-03-15 09:40:28.013' AS DateTime), N'd55a1d04-2319-4038-b3ef-35e6f9572912', CAST(0 AS Decimal(18, 0)), CAST(11.49 AS Decimal(18, 2)))
INSERT [dbo].[Ticket_Locked_Detail] ([TLD_Id], [TLD_User_Id], [TLD_Locked_Qty], [TLD_Event_Id], [TLD_TQD_Id], [Locktime], [TLD_GUID], [TLD_Donate], [TicketAmount]) VALUES (29, N'', 1, 10068, 17, CAST(N'2016-03-15 09:51:28.403' AS DateTime), N'c89a73ab-0c95-4521-a31a-d885519f4562', CAST(0 AS Decimal(18, 0)), CAST(11.49 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Ticket_Locked_Detail] OFF
SET IDENTITY_INSERT [dbo].[Ticket_Purchased_Detail] ON 

INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID], [TPD_Ec_Fee]) VALUES (2, N'800f9add-e483-4c68-b7ec-528c742e5407', N'T000000002', 1, 5, 10055, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'0f5ba609-752b-4de4-b2c1-e3a69d0e2bc1', CAST(0.00 AS Numeric(18, 2)))
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID], [TPD_Ec_Fee]) VALUES (3, N'800f9add-e483-4c68-b7ec-528c742e5407', N'T000000002', 1, 6, 10055, CAST(43.65 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'0f5ba609-752b-4de4-b2c1-e3a69d0e2bc1', CAST(3.21 AS Numeric(18, 2)))
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID], [TPD_Ec_Fee]) VALUES (6, N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', N'T000000004', 1, 17, 10068, CAST(11.49 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'2ba28e9d-72d3-45c9-bdb9-80be326ec1ea', CAST(1.49 AS Numeric(18, 2)))
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID], [TPD_Ec_Fee]) VALUES (7, N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', N'T000000004', 1, 18, 10068, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'2ba28e9d-72d3-45c9-bdb9-80be326ec1ea', CAST(1.49 AS Numeric(18, 2)))
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID], [TPD_Ec_Fee]) VALUES (8, N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', N'T000000004', 1, 19, 10068, CAST(0.00 AS Numeric(9, 2)), CAST(1 AS Decimal(18, 0)), N'2ba28e9d-72d3-45c9-bdb9-80be326ec1ea', CAST(0.00 AS Numeric(18, 2)))
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID], [TPD_Ec_Fee]) VALUES (9, N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', N'T000000004', 1, 20, 10068, CAST(11.49 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'2ba28e9d-72d3-45c9-bdb9-80be326ec1ea', CAST(1.49 AS Numeric(18, 2)))
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID], [TPD_Ec_Fee]) VALUES (10, N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', N'T000000004', 1, 21, 10068, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'2ba28e9d-72d3-45c9-bdb9-80be326ec1ea', CAST(1.49 AS Numeric(18, 2)))
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID], [TPD_Ec_Fee]) VALUES (11, N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', N'T000000004', 1, 22, 10068, CAST(0.00 AS Numeric(9, 2)), CAST(1 AS Decimal(18, 0)), N'2ba28e9d-72d3-45c9-bdb9-80be326ec1ea', CAST(0.00 AS Numeric(18, 2)))
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID], [TPD_Ec_Fee]) VALUES (12, N'800f9add-e483-4c68-b7ec-528c742e5407', N'T000000005', 3, 18, 10068, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'8e7f7bf3-65ae-4ca1-bab6-803e798b6e88', CAST(1.49 AS Numeric(18, 2)))
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID], [TPD_Ec_Fee]) VALUES (13, N'800f9add-e483-4c68-b7ec-528c742e5407', N'T000000006', 1, 17, 10068, CAST(11.49 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'd28739d8-76a6-4fa1-87dd-6133baee50af', CAST(1.49 AS Numeric(18, 2)))
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID], [TPD_Ec_Fee]) VALUES (14, N'800f9add-e483-4c68-b7ec-528c742e5407', N'T000000007', 1, 17, 10068, CAST(11.49 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'643eeb09-c7a2-4c66-8c93-f45bafe29783', CAST(1.49 AS Numeric(18, 2)))
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID], [TPD_Ec_Fee]) VALUES (15, N'800f9add-e483-4c68-b7ec-528c742e5407', N'T000000008', 1, 17, 10068, CAST(11.49 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'08ea9f31-9d02-4717-b652-a132e0bfb574', CAST(1.49 AS Numeric(18, 2)))
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID], [TPD_Ec_Fee]) VALUES (16, N'800f9add-e483-4c68-b7ec-528c742e5407', N'T000000009', 1, 18, 10068, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'a871d984-a623-4fc3-ace9-a4d37da50198', CAST(1.49 AS Numeric(18, 2)))
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID], [TPD_Ec_Fee]) VALUES (17, N'800f9add-e483-4c68-b7ec-528c742e5407', N'T000000010', 1, 17, 10068, CAST(11.49 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'cc72712c-98b9-41be-ba57-068321ed6def', CAST(1.49 AS Numeric(18, 2)))
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID], [TPD_Ec_Fee]) VALUES (18, N'800f9add-e483-4c68-b7ec-528c742e5407', N'T000000011', 1, 17, 10068, CAST(11.49 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'6a549230-e3a6-4d76-a3eb-dde758f5b14c', CAST(1.49 AS Numeric(18, 2)))
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID], [TPD_Ec_Fee]) VALUES (19, N'909e0dae-2f54-4fe6-84da-c1a810ee7ab7', N'T000000012', 2, 13, 10066, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'a328fd61-a240-4631-81a5-1422bdb0f9dc', CAST(0.00 AS Numeric(18, 2)))
INSERT [dbo].[Ticket_Purchased_Detail] ([TPD_Id], [TPD_User_Id], [TPD_Order_Id], [TPD_Purchased_Qty], [TPD_TQD_Id], [TPD_Event_Id], [TPD_Amount], [TPD_Donate], [TPD_GUID], [TPD_Ec_Fee]) VALUES (20, N'909e0dae-2f54-4fe6-84da-c1a810ee7ab7', N'T000000012', 3, 14, 10066, CAST(0.00 AS Numeric(9, 2)), CAST(0 AS Decimal(18, 0)), N'a328fd61-a240-4631-81a5-1422bdb0f9dc', CAST(0.00 AS Numeric(18, 2)))
SET IDENTITY_INSERT [dbo].[Ticket_Purchased_Detail] OFF
SET IDENTITY_INSERT [dbo].[Ticket_Quantity_Detail] ON 

INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (5, 4, 10055, 2, 70, 23, 22, N'Mar 07, 2016', N'7:00pm')
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (6, 4, 10055, 3, 70, 45, 44, N'Mar 07, 2016', N'7:00pm')
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (8, 6, 10064, 5, 79, 20, 20, N'Mar 14, 2016', N'7:00pm')
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (9, 7, 10063, 4, 80, 20, 20, N'Mar 14, 2016', N'7:00pm')
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (10, 8, 10065, 6, 81, 10, 10, N'Mar 14, 2016', N'7:00pm')
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (11, 8, 10065, 7, 81, 10, 10, N'Mar 14, 2016', N'7:00pm')
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (12, 8, 10065, 8, 81, 10, 10, N'Mar 14, 2016', N'7:00pm')
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (13, 9, 10066, 9, 82, 10, 8, N'Mar 14, 2016', N'7:00am')
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (14, 9, 10066, 9, 83, 10, 7, N'Mar 14, 2016', N'7:00am')
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (15, 10, 10067, 10, 84, 10, 10, N'Mar 14, 2016', N'6:00am')
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (16, 10, 10067, 10, 85, 10, 10, N'Mar 14, 2016', N'6:00am')
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (17, 11, 10068, 11, 86, 10, 4, N'Mar 14, 2016', N'6:00am')
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (18, 11, 10068, 12, 86, 10, 5, N'Mar 14, 2016', N'6:00am')
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (19, 11, 10068, 13, 86, 10, 9, N'Mar 14, 2016', N'6:00am')
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (20, 11, 10068, 11, 87, 10, 9, N'Mar 14, 2016', N'6:00am')
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (21, 11, 10068, 12, 87, 10, 9, N'Mar 14, 2016', N'6:00am')
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (22, 11, 10068, 13, 87, 10, 9, N'Mar 14, 2016', N'6:00am')
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (23, 12, 10069, 14, 88, 34, 34, N'Mar 15, 2016', N'7:00pm')
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (24, 13, 10070, 15, 89, 10, 10, N'Mar 15, 2016', N'7:00pm')
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (25, 13, 10070, 16, 89, 10, 10, N'Mar 15, 2016', N'7:00pm')
INSERT [dbo].[Ticket_Quantity_Detail] ([TQD_Id], [TQD_PE_Id], [TQD_Event_Id], [TQD_Ticket_Id], [TQD_AddressId], [TQD_Quantity], [TQD_Remaining_Quantity], [TQD_StartDate], [TQD_StartTime]) VALUES (26, 13, 10070, 17, 89, 10, 10, N'Mar 15, 2016', N'7:00pm')
SET IDENTITY_INSERT [dbo].[Ticket_Quantity_Detail] OFF
SET IDENTITY_INSERT [dbo].[TicketBearer] ON 

INSERT [dbo].[TicketBearer] ([TicketbearerId], [UserId], [OrderId], [Guid], [Name], [Email]) VALUES (1, N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1                                                                                            ', N'T000000004', N'2ba28e9d-72d3-45c9-bdb9-80be326ec1ea                                                                                            ', N'mayank                                                                                                                          ', N'mayank.srivastava@kiwitech.com                                                                                                                                                                                                                                  ')
INSERT [dbo].[TicketBearer] ([TicketbearerId], [UserId], [OrderId], [Guid], [Name], [Email]) VALUES (2, N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1                                                                                            ', N'T000000004', N'2ba28e9d-72d3-45c9-bdb9-80be326ec1ea                                                                                            ', N'Rohit                                                                                                                           ', N'rohit.sharma@kiwitech.com                                                                                                                                                                                                                                       ')
SET IDENTITY_INSERT [dbo].[TicketBearer] OFF
INSERT [dbo].[TicketOrderDetail] ([T_TQD_Id], [T_Guid], [T_Id], [T_Order_Id]) VALUES (18, N'8e7f7bf3-65ae-4ca1-bab6-803e798b6e88', N' 11100315336', N'T000000005')
INSERT [dbo].[TicketOrderDetail] ([T_TQD_Id], [T_Guid], [T_Id], [T_Order_Id]) VALUES (22, N'2ba28e9d-72d3-45c9-bdb9-80be326ec1ea', N' 28775984727', N'T000000004')
INSERT [dbo].[TicketOrderDetail] ([T_TQD_Id], [T_Guid], [T_Id], [T_Order_Id]) VALUES (18, N'8e7f7bf3-65ae-4ca1-bab6-803e798b6e88', N' 41183489903', N'T000000005')
INSERT [dbo].[TicketOrderDetail] ([T_TQD_Id], [T_Guid], [T_Id], [T_Order_Id]) VALUES (20, N'2ba28e9d-72d3-45c9-bdb9-80be326ec1ea', N'************', N'T000000004')
INSERT [dbo].[TicketOrderDetail] ([T_TQD_Id], [T_Guid], [T_Id], [T_Order_Id]) VALUES (17, N'643eeb09-c7a2-4c66-8c93-f45bafe29783', N'164933196254', N'T000000007')
INSERT [dbo].[TicketOrderDetail] ([T_TQD_Id], [T_Guid], [T_Id], [T_Order_Id]) VALUES (6, N'0f5ba609-752b-4de4-b2c1-e3a69d0e2bc1', N'176029232847', N'T000000002')
INSERT [dbo].[TicketOrderDetail] ([T_TQD_Id], [T_Guid], [T_Id], [T_Order_Id]) VALUES (18, N'8e7f7bf3-65ae-4ca1-bab6-803e798b6e88', N'186144210510', N'T000000005')
INSERT [dbo].[TicketOrderDetail] ([T_TQD_Id], [T_Guid], [T_Id], [T_Order_Id]) VALUES (18, N'a871d984-a623-4fc3-ace9-a4d37da50198', N'196301170896', N'T000000009')
INSERT [dbo].[TicketOrderDetail] ([T_TQD_Id], [T_Guid], [T_Id], [T_Order_Id]) VALUES (14, N'a328fd61-a240-4631-81a5-1422bdb0f9dc', N'210091955780', N'T000000012')
INSERT [dbo].[TicketOrderDetail] ([T_TQD_Id], [T_Guid], [T_Id], [T_Order_Id]) VALUES (17, N'2ba28e9d-72d3-45c9-bdb9-80be326ec1ea', N'317544056259', N'T000000004')
INSERT [dbo].[TicketOrderDetail] ([T_TQD_Id], [T_Guid], [T_Id], [T_Order_Id]) VALUES (17, N'cc72712c-98b9-41be-ba57-068321ed6def', N'425575191482', N'T000000010')
INSERT [dbo].[TicketOrderDetail] ([T_TQD_Id], [T_Guid], [T_Id], [T_Order_Id]) VALUES (18, N'2ba28e9d-72d3-45c9-bdb9-80be326ec1ea', N'427604380616', N'T000000004')
INSERT [dbo].[TicketOrderDetail] ([T_TQD_Id], [T_Guid], [T_Id], [T_Order_Id]) VALUES (5, N'0f5ba609-752b-4de4-b2c1-e3a69d0e2bc1', N'483564874251', N'T000000002')
INSERT [dbo].[TicketOrderDetail] ([T_TQD_Id], [T_Guid], [T_Id], [T_Order_Id]) VALUES (14, N'a328fd61-a240-4631-81a5-1422bdb0f9dc', N'546444405446', N'T000000012')
INSERT [dbo].[TicketOrderDetail] ([T_TQD_Id], [T_Guid], [T_Id], [T_Order_Id]) VALUES (19, N'2ba28e9d-72d3-45c9-bdb9-80be326ec1ea', N'560161680468', N'T000000004')
INSERT [dbo].[TicketOrderDetail] ([T_TQD_Id], [T_Guid], [T_Id], [T_Order_Id]) VALUES (17, N'd28739d8-76a6-4fa1-87dd-6133baee50af', N'569986497648', N'T000000006')
INSERT [dbo].[TicketOrderDetail] ([T_TQD_Id], [T_Guid], [T_Id], [T_Order_Id]) VALUES (17, N'08ea9f31-9d02-4717-b652-a132e0bfb574', N'611966705733', N'T000000008')
INSERT [dbo].[TicketOrderDetail] ([T_TQD_Id], [T_Guid], [T_Id], [T_Order_Id]) VALUES (14, N'a328fd61-a240-4631-81a5-1422bdb0f9dc', N'621214521070', N'T000000012')
INSERT [dbo].[TicketOrderDetail] ([T_TQD_Id], [T_Guid], [T_Id], [T_Order_Id]) VALUES (21, N'2ba28e9d-72d3-45c9-bdb9-80be326ec1ea', N'621770320256', N'T000000004')
INSERT [dbo].[TicketOrderDetail] ([T_TQD_Id], [T_Guid], [T_Id], [T_Order_Id]) VALUES (13, N'a328fd61-a240-4631-81a5-1422bdb0f9dc', N'633921970172', N'T000000012')
INSERT [dbo].[TicketOrderDetail] ([T_TQD_Id], [T_Guid], [T_Id], [T_Order_Id]) VALUES (13, N'a328fd61-a240-4631-81a5-1422bdb0f9dc', N'645087222882', N'T000000012')
INSERT [dbo].[TicketOrderDetail] ([T_TQD_Id], [T_Guid], [T_Id], [T_Order_Id]) VALUES (17, N'6a549230-e3a6-4d76-a3eb-dde758f5b14c', N'799665899034', N'T000000011')
SET IDENTITY_INSERT [dbo].[TicketType] ON 

INSERT [dbo].[TicketType] ([TicketTypeID], [TicketType]) VALUES (1, N'Free')
INSERT [dbo].[TicketType] ([TicketTypeID], [TicketType]) VALUES (2, N'Paid')
INSERT [dbo].[TicketType] ([TicketTypeID], [TicketType]) VALUES (3, N'Donation')
SET IDENTITY_INSERT [dbo].[TicketType] OFF
SET IDENTITY_INSERT [dbo].[TimeZoneDetail] ON 

INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+04:30) Kabul', N'Afghanistan Standard Time', 1)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-09:00) Alaska', N'Alaskan Standard Time', 2)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+03:00) Kuwait, Riyadh', N'Arab Standard Time', 3)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+04:00) Abu Dhabi, Muscat', N'Arabian Standard Time', 4)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+03:00) Baghdad', N'Arabic Standard Time', 5)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-03:00) Buenos Aires', N'Argentina Standard Time', 6)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-04:00) Atlantic Time (Canada)', N'Atlantic Standard Time', 7)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+09:30) Darwin', N'AUS Central Standard Time', 8)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+10:00) Canberra, Melbourne, Sydney', N'AUS Eastern Standard Time', 9)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+04:00) Baku', N'Azerbaijan Standard Time', 10)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-01:00) Azores', N'Azores Standard Time', 11)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+06:00) Dhaka', N'Bangladesh Standard Time', 12)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-06:00) Saskatchewan', N'Canada Central Standard Time', 13)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-01:00) Cape Verde Is.', N'Cape Verde Standard Time', 14)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+04:00) Yerevan', N'Caucasus Standard Time', 15)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+09:30) Adelaide', N'Cen. Australia Standard Time', 16)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-06:00) Central America', N'Central America Standard Time', 17)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+06:00) Astana', N'Central Asia Standard Time', 18)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-04:00) Cuiaba', N'Central Brazilian Standard Time', 19)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague', N'Central Europe Standard Time', 20)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+01:00) Sarajevo, Skopje, Warsaw, Zagreb', N'Central European Standard Time', 21)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+11:00) Solomon Is., New Caledonia', N'Central Pacific Standard Time', 22)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-06:00) Central Time (US & Canada)', N'Central Standard Time', 23)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-06:00) Guadalajara, Mexico City, Monterrey', N'Central Standard Time (Mexico)', 24)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+08:00) Beijing, Chongqing, Hong Kong, Urumqi', N'China Standard Time', 25)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-12:00) International Date Line West', N'Dateline Standard Time', 26)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+03:00) Nairobi', N'E. Africa Standard Time', 27)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+10:00) Brisbane', N'E. Australia Standard Time', 28)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+02:00) Minsk', N'E. Europe Standard Time', 29)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-03:00) Brasilia', N'E. South America Standard Time', 30)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-05:00) Eastern Time (US & Canada)', N'Eastern Standard Time', 31)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+02:00) Cairo', N'Egypt Standard Time', 32)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+05:00) Ekaterinburg', N'Ekaterinburg Standard Time', 33)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+12:00) Fiji', N'Fiji Standard Time', 34)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+02:00) Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius', N'FLE Standard Time', 35)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+04:00) Tbilisi', N'Georgian Standard Time', 36)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT) Dublin, Edinburgh, Lisbon, London', N'GMT Standard Time', 37)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-03:00) Greenland', N'Greenland Standard Time', 38)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT) Monrovia, Reykjavik', N'Greenwich Standard Time', 39)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+02:00) Athens, Bucharest, Istanbul', N'GTB Standard Time', 40)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-10:00) Hawaii', N'Hawaiian Standard Time', 41)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+05:30) Chennai, Kolkata, Mumbai, New Delhi', N'India Standard Time', 42)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+03:30) Tehran', N'Iran Standard Time', 43)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+02:00) Jerusalem', N'Israel Standard Time', 44)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+02:00) Amman', N'Jordan Standard Time', 45)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+12:00) Petropavlovsk-Kamchatsky - Old', N'Kamchatka Standard Time', 46)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+09:00) Seoul', N'Korea Standard Time', 47)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+11:00) Magadan', N'Magadan Standard Time', 48)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+04:00) Port Louis', N'Mauritius Standard Time', 49)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-02:00) Mid-Atlantic', N'Mid-Atlantic Standard Time', 50)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+02:00) Beirut', N'Middle East Standard Time', 51)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-03:00) Montevideo', N'Montevideo Standard Time', 52)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT) Casablanca', N'Morocco Standard Time', 53)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-07:00) Mountain Time (US & Canada)', N'Mountain Standard Time', 54)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-07:00) Chihuahua, La Paz, Mazatlan', N'Mountain Standard Time (Mexico)', 55)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+06:30) Yangon (Rangoon)', N'Myanmar Standard Time', 56)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+06:00) Novosibirsk', N'N. Central Asia Standard Time', 57)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+01:00) Windhoek', N'Namibia Standard Time', 58)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+05:45) Kathmandu', N'Nepal Standard Time', 59)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+12:00) Auckland, Wellington', N'New Zealand Standard Time', 60)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-03:30) Newfoundland', N'Newfoundland Standard Time', 61)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+08:00) Irkutsk', N'North Asia East Standard Time', 62)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+07:00) Krasnoyarsk', N'North Asia Standard Time', 63)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-04:00) Santiago', N'Pacific SA Standard Time', 64)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-08:00) Pacific Time (US & Canada)', N'Pacific Standard Time', 65)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-08:00) Baja California', N'Pacific Standard Time (Mexico)', 66)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+05:00) Islamabad, Karachi', N'Pakistan Standard Time', 67)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-04:00) Asuncion', N'Paraguay Standard Time', 68)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+01:00) Brussels, Copenhagen, Madrid, Paris', N'Romance Standard Time', 69)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+03:00) Moscow, St. Petersburg, Volgograd', N'Russian Standard Time', 70)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-03:00) Cayenne, Fortaleza', N'SA Eastern Standard Time', 71)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-05:00) Bogota, Lima, Quito', N'SA Pacific Standard Time', 72)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-04:00) Georgetown, La Paz, Manaus, San Juan', N'SA Western Standard Time', 73)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-11:00) Samoa', N'Samoa Standard Time', 74)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+07:00) Bangkok, Hanoi, Jakarta', N'SE Asia Standard Time', 75)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+08:00) Kuala Lumpur, Singapore', N'Singapore Standard Time', 76)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+02:00) Harare, Pretoria', N'South Africa Standard Time', 77)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+05:30) Sri Jayawardenepura', N'Sri Lanka Standard Time', 78)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+02:00) Damascus', N'Syria Standard Time', 79)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+08:00) Taipei', N'Taipei Standard Time', 80)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+10:00) Hobart', N'Tasmania Standard Time', 81)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+09:00) Osaka, Sapporo, Tokyo', N'Tokyo Standard Time', 82)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+13:00) Nuku''alofa', N'Tonga Standard Time', 83)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+08:00) Ulaanbaatar', N'Ulaanbaatar Standard Time', 84)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-05:00) Indiana (East)', N'US Eastern Standard Time', 85)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-07:00) Arizona', N'US Mountain Standard Time', 86)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT) Coordinated Universal Time', N'UTC', 87)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+12:00) Coordinated Universal Time+12', N'UTC+12', 88)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-02:00) Coordinated Universal Time-02', N'UTC-02', 89)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-11:00) Coordinated Universal Time-11', N'UTC-11', 90)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT-04:30) Caracas', N'Venezuela Standard Time', 91)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+10:00) Vladivostok', N'Vladivostok Standard Time', 92)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+08:00) Perth', N'W. Australia Standard Time', 93)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+01:00) West Central Africa', N'W. Central Africa Standard Time', 94)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna', N'W. Europe Standard Time', 95)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+05:00) Tashkent', N'West Asia Standard Time', 96)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+10:00) Guam, Port Moresby', N'West Pacific Standard Time', 97)
INSERT [dbo].[TimeZoneDetail] ([TimeZone_Name], [TimeZone], [TimeZone_Id]) VALUES (N'(GMT+09:00) Yakutsk', N'Yakutsk Standard Time', 98)
SET IDENTITY_INSERT [dbo].[TimeZoneDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[User_Permission_Detail] ON 

INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2491, N'800f9add-e483-4c68-b7ec-528c742e5407', 1, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2492, N'800f9add-e483-4c68-b7ec-528c742e5407', 2, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2493, N'3fd06a6a-9b9c-46a4-84db-e2f5f5168ad6', 1, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2494, N'3fd06a6a-9b9c-46a4-84db-e2f5f5168ad6', 2, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2495, N'57af65d8-f739-4e09-a39e-f7b0ae2715f0', 1, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2496, N'57af65d8-f739-4e09-a39e-f7b0ae2715f0', 2, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2497, N'766ab684-8ea6-4ff7-b09c-1a99cec69e22', 1, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2498, N'766ab684-8ea6-4ff7-b09c-1a99cec69e22', 2, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2499, N'9288c7f0-e234-4489-971d-e8d029ededcc', 1, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2500, N'9288c7f0-e234-4489-971d-e8d029ededcc', 2, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2501, N'df32bef3-022e-4c62-a695-dfa21892321f', 1, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2502, N'df32bef3-022e-4c62-a695-dfa21892321f', 2, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2503, N'a06cd8b0-1d3e-4dfe-92d4-0d4bc62a5076', 1, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2504, N'a06cd8b0-1d3e-4dfe-92d4-0d4bc62a5076', 2, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2505, N'2ecda86e-1d74-4cc4-b05b-97dba9111ac0', 1, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2506, N'2ecda86e-1d74-4cc4-b05b-97dba9111ac0', 2, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2507, N'a60e2f31-b772-428e-9bf5-2c5e6dcd56d7', 1, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2508, N'a60e2f31-b772-428e-9bf5-2c5e6dcd56d7', 2, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2509, N'909e0dae-2f54-4fe6-84da-c1a810ee7ab7', 1, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2510, N'909e0dae-2f54-4fe6-84da-c1a810ee7ab7', 2, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2511, N'8dee8fd1-a0e0-403f-9c19-f2e271f0444c', 1, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2512, N'8dee8fd1-a0e0-403f-9c19-f2e271f0444c', 2, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2513, N'd60e166b-0713-475c-8ca4-9d749aed86f3', 1, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2514, N'd60e166b-0713-475c-8ca4-9d749aed86f3', 2, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2515, N'250d1f3f-6bac-463d-9ae6-53bd8dda317b', 1, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2516, N'250d1f3f-6bac-463d-9ae6-53bd8dda317b', 2, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2517, N'ab798d10-5c11-4c4e-b206-6da5ac9045f1', 1, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2518, N'ab798d10-5c11-4c4e-b206-6da5ac9045f1', 2, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2519, N'17f0863a-156f-4d34-bea3-fd247d0c7d62', 1, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2520, N'17f0863a-156f-4d34-bea3-fd247d0c7d62', 2, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2521, N'0aa92a22-0b7a-4055-bc2b-8ff72a92adf1', 1, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2522, N'0aa92a22-0b7a-4055-bc2b-8ff72a92adf1', 2, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2523, N'0c2935d4-cfaf-4792-8569-a07d624b762f', 1, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2524, N'0c2935d4-cfaf-4792-8569-a07d624b762f', 2, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2525, N'0cb998d7-c1e1-4019-8381-45132347d9af', 1, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2526, N'0cb998d7-c1e1-4019-8381-45132347d9af', 2, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2529, N'8dcc551e-dd63-4130-a138-21ed5ce3ecaa', 1, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2530, N'8dcc551e-dd63-4130-a138-21ed5ce3ecaa', 2, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2531, N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', 1, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2532, N'fad0e8c6-a3f8-4a9b-9c87-ad51cec99ec1', 2, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2533, N'd8757720-1948-4fe5-8d55-0aa7530a82f1', 1, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2534, N'd8757720-1948-4fe5-8d55-0aa7530a82f1', 2, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2535, N'e25f3b4b-4c09-43df-82a0-c72054417035', 1, NULL)
INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (2536, N'e25f3b4b-4c09-43df-82a0-c72054417035', 2, NULL)
SET IDENTITY_INSERT [dbo].[User_Permission_Detail] OFF
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Address_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Address]'))
ALTER TABLE [dbo].[Address]  WITH NOCHECK ADD  CONSTRAINT [FK_Address_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([EventID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Address_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Address]'))
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_Event]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]'))
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]'))
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]'))
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]'))
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_Orgnizer_Detail_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event_Orgnizer_Detail]'))
ALTER TABLE [dbo].[Event_Orgnizer_Detail]  WITH NOCHECK ADD  CONSTRAINT [FK_Event_Orgnizer_Detail_Event] FOREIGN KEY([Orgnizer_Event_Id])
REFERENCES [dbo].[Event] ([EventID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_Orgnizer_Detail_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event_Orgnizer_Detail]'))
ALTER TABLE [dbo].[Event_Orgnizer_Detail] CHECK CONSTRAINT [FK_Event_Orgnizer_Detail_Event]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_Orgnizer_Detail_Organizer_Master]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event_Orgnizer_Detail]'))
ALTER TABLE [dbo].[Event_Orgnizer_Detail]  WITH CHECK ADD  CONSTRAINT [FK_Event_Orgnizer_Detail_Organizer_Master] FOREIGN KEY([OrganizerMaster_Id])
REFERENCES [dbo].[Organizer_Master] ([Orgnizer_Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_Orgnizer_Detail_Organizer_Master]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event_Orgnizer_Detail]'))
ALTER TABLE [dbo].[Event_Orgnizer_Detail] CHECK CONSTRAINT [FK_Event_Orgnizer_Detail_Organizer_Master]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_VariableDesc_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event_VariableDesc]'))
ALTER TABLE [dbo].[Event_VariableDesc]  WITH NOCHECK ADD  CONSTRAINT [FK_Event_VariableDesc_Event] FOREIGN KEY([Event_Id])
REFERENCES [dbo].[Event] ([EventID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Event_VariableDesc_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Event_VariableDesc]'))
ALTER TABLE [dbo].[Event_VariableDesc] CHECK CONSTRAINT [FK_Event_VariableDesc_Event]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EventImage_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[EventImage]'))
ALTER TABLE [dbo].[EventImage]  WITH NOCHECK ADD  CONSTRAINT [FK_EventImage_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([EventID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EventImage_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[EventImage]'))
ALTER TABLE [dbo].[EventImage] CHECK CONSTRAINT [FK_EventImage_Event]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__EventSubC__Event__3F7150CD]') AND parent_object_id = OBJECT_ID(N'[dbo].[EventSubCategory]'))
ALTER TABLE [dbo].[EventSubCategory]  WITH CHECK ADD FOREIGN KEY([EventCategoryID])
REFERENCES [dbo].[EventCategory] ([EventCategoryID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EventVenue_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[EventVenue]'))
ALTER TABLE [dbo].[EventVenue]  WITH NOCHECK ADD  CONSTRAINT [FK_EventVenue_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([EventID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EventVenue_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[EventVenue]'))
ALTER TABLE [dbo].[EventVenue] CHECK CONSTRAINT [FK_EventVenue_Event]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EventVenue_EventVenue]') AND parent_object_id = OBJECT_ID(N'[dbo].[EventVenue]'))
ALTER TABLE [dbo].[EventVenue]  WITH CHECK ADD  CONSTRAINT [FK_EventVenue_EventVenue] FOREIGN KEY([EventVenueID])
REFERENCES [dbo].[EventVenue] ([EventVenueID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EventVenue_EventVenue]') AND parent_object_id = OBJECT_ID(N'[dbo].[EventVenue]'))
ALTER TABLE [dbo].[EventVenue] CHECK CONSTRAINT [FK_EventVenue_EventVenue]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MultipleEvent_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[MultipleEvent]'))
ALTER TABLE [dbo].[MultipleEvent]  WITH NOCHECK ADD  CONSTRAINT [FK_MultipleEvent_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([EventID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MultipleEvent_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[MultipleEvent]'))
ALTER TABLE [dbo].[MultipleEvent] CHECK CONSTRAINT [FK_MultipleEvent_Event]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Profile_Profile]') AND parent_object_id = OBJECT_ID(N'[dbo].[Profile]'))
ALTER TABLE [dbo].[Profile]  WITH CHECK ADD  CONSTRAINT [FK_Profile_Profile] FOREIGN KEY([UserID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Profile_Profile]') AND parent_object_id = OBJECT_ID(N'[dbo].[Profile]'))
ALTER TABLE [dbo].[Profile] CHECK CONSTRAINT [FK_Profile_Profile]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Ticket_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Ticket]'))
ALTER TABLE [dbo].[Ticket]  WITH NOCHECK ADD  CONSTRAINT [FK_Ticket_Event] FOREIGN KEY([E_Id])
REFERENCES [dbo].[Event] ([EventID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Ticket_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Ticket]'))
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Event]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Ticket_Quantity_Info_Ticket]') AND parent_object_id = OBJECT_ID(N'[dbo].[Ticket_Quantity_Detail]'))
ALTER TABLE [dbo].[Ticket_Quantity_Detail]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Quantity_Info_Ticket] FOREIGN KEY([TQD_Ticket_Id])
REFERENCES [dbo].[Ticket] ([T_Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Ticket_Quantity_Info_Ticket]') AND parent_object_id = OBJECT_ID(N'[dbo].[Ticket_Quantity_Detail]'))
ALTER TABLE [dbo].[Ticket_Quantity_Detail] CHECK CONSTRAINT [FK_Ticket_Quantity_Info_Ticket]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__TicketDel__Deliv__461E4E5C]') AND parent_object_id = OBJECT_ID(N'[dbo].[TicketDeliveryMethod]'))
ALTER TABLE [dbo].[TicketDeliveryMethod]  WITH CHECK ADD FOREIGN KEY([DeliveryMethodID])
REFERENCES [dbo].[DeliveryMethod] ([DeliveryMethodID])
GO
/****** Object:  StoredProcedure [dbo].[GetEventDateList]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetEventDateList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetEventDateList] AS' 
END
GO
--[GetEventDateList] 294
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[GetEventDateList]
	@EventId bigint
AS
BEGIN
	DECLARE @StartDate varchar(50)
	DECLARE @EndDate varchar(50)
	DECLARE @StartTime varchar(50)
	DECLARE @EndTime varchar(50)
	Declare @Dayofweek varchar(50)
    DECLARE @Freq VARCHAR(50)
	DECLARE @Singleevent int
	SET @Singleevent =0
    DECLARE @Days INT;
	DECLARE @cnt INT = 0;
    declare @DateFrom Date
    declare @DateTo Date

	SELECT @Singleevent=count(*) from EventVenue WHERE EventID = @EventId
	print @Singleevent 
	DECLARE @tblDates TABLE (Dayofweek varchar(50),startdate date,Time varchar(50),type varchar(50))


	if(@Singleevent>0)
	BEGIN

		SELECT @StartDate = EventStartDate ,@StartTime=EventStartTime
		FROM EventVenue WHERE EventID = @EventId
		SELECT  @Dayofweek=datename(dw,@StartDate)
	
	INSERT INTO @tblDates VALUES
		(@Dayofweek,@StartDate,@StartTime,'Single')	
	End
	else 
	begin
	select @Freq=Frequency from MultipleEvent  WHERE EventID = @EventId
	print @Freq
	If (@Freq = 'Daily')
	begin

		SELECT @Days = DateDiff(dd,StartingFrom ,StartingTo),
			@StartDate = StartingFrom,@StartTime=StartTime
			FROM MultipleEvent WHERE EventID = @EventId
			Set @DateFrom = convert(date,@StartDate)
               Set @DateTo = convert(date,@EndDate)
			WHILE @cnt <= @Days
            BEGIN
             SELECT  @Dayofweek=datename(dw,@DateFrom)
           INSERT INTO @tblDates VALUES
				(@Dayofweek,@DateFrom,@StartTime,'Daily')
						
				SET @DateFrom = DATEADD(dd,1,@DateFrom) 
				

				set @cnt=@cnt+1;
              END
		
	end
	Else If (@Freq = 'Weekly')
      Begin
	 DECLARE @Weekly varchar(50)
	    Declare @WeekDay int
	 Declare @DayID int
	  
	   SELECT @Weekly = WeeklyDay FROM MultipleEvent WHERE EventID = @EventId
	   	SELECT @Days = DateDiff(dd,StartingFrom ,StartingTo),@StartDate = StartingFrom,@StartTime=StartTime,@EndDate=StartingTo
			FROM MultipleEvent WHERE EventID = @EventId
			   Set @DateFrom = convert(date,@StartDate)
               Set @DateTo = convert(date,@EndDate)
			 Declare @tblWeek Table(Id INT,DayId INT) 
               insert into @tblWeek 
               select * From [dbo].func_Split(@Weekly,',') 
		  WHILE (@DateFrom<=@DateTo)
            
            BEGIN
			 SET @WeekDay = dbo.ReturnDayOfWeek(@DateFrom) 
			 
			 Select @DayID = count(*) from @tblWeek where DayId= @WeekDay

			 if (@DayID >0)
			 begin
			  SELECT  @Dayofweek=datename(dw,@DateFrom)
           INSERT INTO @tblDates VALUES
				(@Dayofweek,@DateFrom,@StartTime,'weekly')
	
				 
			 end
			SET @DateFrom = DATEADD(dd,1,@DateFrom) 
			
			end

   End
   Else If(@Freq = 'Monthly')
   Begin
   Declare @Monthlyday varchar(50)
     Declare @Monthlyweek varchar(50)
	   Declare @MonthlyweekDays varchar(50)
	
	 Declare @day varchar(50)
   	SELECT @Days = DateDiff(dd,StartingFrom ,StartingTo),
			@StartDate = StartingFrom,@StartTime=StartTime,@Monthlyday=MonthlyDay,@Monthlyweek=MonthlyWeek,
			@MonthlyweekDays=MonthlyWeekDays,@EndDate=StartingTo
			FROM MultipleEvent WHERE EventID = @EventId
			
 Set @DateFrom = convert(date,@StartDate)
 
               Set @DateTo = convert(date,@EndDate)
			   
if(isnull(@Monthlyday,'')!='')
begin 

 WHILE (@DateFrom<=@DateTo)
 begin 


set @day=DATEPART(dd,@DateFrom) 

if(@day=@Monthlyday)
begin 
  SELECT  @Dayofweek=datename(dw,@DateFrom)
     INSERT INTO @tblDates VALUES
				(@Dayofweek,@DateFrom,@StartTime,'Monthly')
	
end
SET @DateFrom = DATEADD(dd,1,@DateFrom) 
 end
end
if( isnull(@Monthlyweek,'')!='')
begin

Declare @monthday as int

SELECT @monthday = CASE @Monthlyweek WHEN 'First' THEN 1 WHEN 'Second' THEN 2 WHEN 'Third' THEN 3 WHEN 'Four' THEN 4 END

Declare @Noofweek varchar(50)
set @cnt=1
SELECT @Noofweek= DATEDIFF(week, @DateFrom, @DateTo)

WHILE @cnt <= @Noofweek
begin
print  convert(varchar,convert(varchar,@Noofweek)+'g'+convert(varchar,@monthday)+'g'+convert(varchar,@cnt))
if(@cnt=@monthday)
begin

Declare @newstartdate as date
Declare @newenddate as date
set @newstartdate=@StartDate
print @newstartdate
set @newenddate=DATEADD(dd,7,@newstartdate) 
               insert into @tblWeek 
               select * From [dbo].func_Split(@MonthlyweekDays,',') 


			     WHILE (@newstartdate<=@newenddate)
            
            BEGIN
			 SET @WeekDay = dbo.ReturnDayOfWeek(@newstartdate) 
			
			 Select @DayID = count(*) from @tblWeek where DayId= @WeekDay
			
			 if (@DayID >0)
			 begin
			  SELECT  @Dayofweek=datename(dw,@newstartdate)
           INSERT INTO @tblDates VALUES
				(@Dayofweek,@newstartdate,@StartTime,'Monthly')
	
				 
			 end
			SET @newstartdate = DATEADD(dd,1,@newstartdate) 
			
			end

end
SET @StartDate = DATEADD(dd,7,@StartDate) 
set @cnt=@cnt+1
 end
end
   End
	end

select Dayofweek ,convert(varchar(30),startdate,107) as Datefrom ,Time ,type from @tblDates
END



GO
/****** Object:  StoredProcedure [dbo].[GetEventListing]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetEventListing]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetEventListing] AS' 
END
GO





--exec [GetEventListing] 

ALTER PROC [dbo].[GetEventListing]
(
	@EventTitle varchar(1200) =null,
	@EventType varchar(50) = null,
	@EventCat varchar(50) = null,
	@EventSubCat varchar(50) = null,
	@EventFeature varchar(10) = null,
	@EventStartdate varchar(10)=null,
	@EventTicket varchar(10)=null

)
AS 
BEGIN
	Declare @EventT int
	Declare @EventC int
	Declare @EventSC int
	Declare @EventF int
	Declare @strsql varchar(max)
	
	if(@EventStartdate=0 and @EventTicket=2)
	begin
		select EV.UserID, EV.EventID as EventID , EventTitle,ET.EventType AS EventType,EC.EventCategory as EventCategory , ESC.EventSubCategory as EventSubCategory ,
		(CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
			( convert(varchar, convert(Date,EVen.EventStartDate),106) +' ' +  convert(varchar,convert(time,EVen.EventStartTime),100)  + '-' +
			convert(varchar, convert(Date,EVen.EventEndDate),106) + ' ' + convert(varchar,convert(time,EVen.EventEndTime),100)) 
		ELSE
			( convert(varchar,convert(Date,ME.StartingFrom) ,106) + ' ' +  convert(varchar,ME.StartTime)  + '-' +
			CONVERT(VARCHAR,convert(Date,Me.StartingTo),106 ) + '-' + CONVERT(VARCHAR,Me.EndTime)) 
		END ) as EventTiming ,ME.StartingFrom as	StartingFrom ,ISNULL(Feature,0) as Feature,
		(select top 1 a.Orgnizer_Name from Organizer_Master a join  Event_Orgnizer_Detail EOD on a.Orgnizer_Id=eod.OrganizerMaster_Id where  EOD.Orgnizer_Event_Id=Ev.EventID and EOD.DefaultOrg='Y' ) 
		as  Orgnizer_Name,
		(select top 1 AD.ConsolidateAddress from Address AD where  AD.EventId= EV.EventID ) as EventAddress,
		(select top 1 AD.VenueName from Address AD where  AD.EventId= EV.EventID ) as Venuename,
		anr.Email,
		(SELECT   convert(varchar,isnull( sum(TQD_Remaining_Quantity),0)) FROM Ticket_Quantity_Detail where TQD_Event_Id=Ev.EventID) 
		as TicketDetail,
		isnull((Select sum(TPD_Purchased_Qty) from Ticket_Purchased_Detail  where TPD_Event_Id=Ev.EventID),0) as Purchasedqty

		 from 
		 
		 ((((((Event Ev LEFT JOIN EventType ET
	ON Ev.EventTypeID = Et.EventTypeID ) LEFT JOIN EventCategory EC on EV.EventCategoryID =  EC.EventCategoryID)
	LEFT JOIN  EventSubCategory ESC on EV.EventSubCategoryID = ESC.EventSubCategoryID)
	LEFT JOIN EventVenue EVen
	ON Ev.EventID = EVen.EventID)
	LEFT JOIN MultipleEvent ME on EV.EventID = ME.EventID)
	Left join AspNetUsers ANR on Ev.UserID=ANR.Id )
	WHERE Ev.EventTitle like '%' + Coalesce(@EventTitle,Ev.EventTitle) + '%'
	AND isnull(Ev.EventTypeID,'')  = CASE WHEN @EventType >0 Then @EventType Else isnull(Ev.EventTypeID,'') END
	AND isnull(Ev.EventCategoryID,0)  = CASE WHEN @EventCat >0 Then @EventCat Else isnull(Ev.EventCategoryID,0) END
	AND isnull(Ev.EventSubCategoryID,0)  = CASE WHEN @EventSubCat >0 Then @EventSubCat  Else isnull(Ev.EventSubCategoryID,0) END
	AND ISNULL(Ev.Feature,0) = CASE WHEN @EventFeature >0 Then @EventFeature  Else ISNULL(Ev.Feature,0) END 
	AND (SELECT   isnull(sum(TQD_Remaining_Quantity),0)
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) >0 
order by (CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		(convert(datetime,convert(varchar, convert(datetime,EVen.EventEndDate),106))  + convert(datetime,convert(varchar,convert(time,EVen.EventEndTime),100)) )
	ELSE
		( convert(datetime,CONVERT(VARCHAR,convert(datetime,Me.StartingTo),106 )) + convert(datetime,CONVERT(VARCHAR,Me.EndTime)))
	END ) desc
	end
	if(@EventStartdate=0 and @EventTicket=1)
	begin
	select EV.UserID, EV.EventID as EventID , EventTitle,ET.EventType AS EventType,EC.EventCategory as EventCategory , ESC.EventSubCategory as EventSubCategory ,
	(CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		( convert(varchar, convert(datetime,EVen.EventStartDate),106) +' ' +  convert(varchar,convert(datetime,EVen.EventStartTime),100)  + '-' +
		convert(varchar, convert(datetime,EVen.EventEndDate),106) + ' ' + convert(varchar,convert(datetime,EVen.EventEndTime),100)) 
	ELSE
		( convert(varchar,convert(datetime,ME.StartingFrom) ,106) + ' ' +  convert(varchar,convert(datetime,ME.StartTime))  + '-' +
		CONVERT(VARCHAR,convert(datetime,Me.StartingTo),106 ) + '-' + CONVERT(VARCHAR,convert(datetime,Me.EndTime))) 
	END ) as EventTiming ,ME.StartingFrom as	StartingFrom ,ISNULL(Feature,0) as Feature,
(select top 1 a.Orgnizer_Name from Organizer_Master a join  Event_Orgnizer_Detail EOD on a.Orgnizer_Id=eod.OrganizerMaster_Id where  EOD.Orgnizer_Event_Id=Ev.EventID and EOD.DefaultOrg='Y' ) 
as  Orgnizer_Name,
(select top 1 AD.ConsolidateAddress from Address AD where  AD.EventId= EV.EventID ) as EventAddress,
(select top 1 AD.VenueName from Address AD where  AD.EventId= EV.EventID ) as Venuename,
anr.Email,
(SELECT   convert(varchar,isnull( sum(TQD_Remaining_Quantity),0)) 
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) as TicketDetail,
isnull((Select sum(TPD_Purchased_Qty) from Ticket_Purchased_Detail  where TPD_Event_Id=Ev.EventID),0) as Purchasedqty
 from ((((((Event Ev LEFT JOIN EventType ET
	ON Ev.EventTypeID = Et.EventTypeID ) LEFT JOIN EventCategory EC on EV.EventCategoryID =  EC.EventCategoryID)
	LEFT JOIN  EventSubCategory ESC on EV.EventSubCategoryID = ESC.EventSubCategoryID)
	LEFT JOIN EventVenue EVen
	ON Ev.EventID = EVen.EventID)
	LEFT JOIN MultipleEvent ME on EV.EventID = ME.EventID)
	Left join AspNetUsers ANR on Ev.UserID=ANR.Id )
	WHERE Ev.EventTitle like '%' + Coalesce(@EventTitle,Ev.EventTitle) + '%'
	AND isnull(Ev.EventTypeID,'')  = CASE WHEN @EventType >0 Then @EventType Else isnull(Ev.EventTypeID,'') END
	AND isnull(Ev.EventCategoryID,0)  = CASE WHEN @EventCat >0 Then @EventCat Else isnull(Ev.EventCategoryID,0) END
	AND isnull(Ev.EventSubCategoryID,0)  = CASE WHEN @EventSubCat >0 Then @EventSubCat  Else isnull(Ev.EventSubCategoryID,0) END
	AND ISNULL(Ev.Feature,0) = CASE WHEN @EventFeature >0 Then @EventFeature  Else ISNULL(Ev.Feature,0) END 
	AND (SELECT   isnull( sum(TQD_Remaining_Quantity),0)
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) <=0
order by (CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		(convert(datetime,convert(varchar, convert(datetime,EVen.EventEndDate),106))  + convert(datetime,convert(varchar,convert(time,EVen.EventEndTime),100)) )
	ELSE
		( convert(datetime,CONVERT(VARCHAR,convert(datetime,Me.StartingTo),106 )) + convert(datetime,CONVERT(VARCHAR,Me.EndTime)))
	END ) desc
	end
	if(@EventStartdate=0 and @EventTicket=0)
	begin
	select EV.UserID, EV.EventID as EventID , EventTitle,ET.EventType AS EventType,EC.EventCategory as EventCategory , ESC.EventSubCategory as EventSubCategory ,
	(CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		( convert(varchar, convert(datetime,EVen.EventStartDate),106) +' ' +  convert(varchar,convert(datetime,EVen.EventStartTime),100)  + '-' +
		convert(varchar, convert(datetime,EVen.EventEndDate),106) + ' ' + convert(varchar,convert(datetime,EVen.EventEndTime),100)) 
	ELSE
		( convert(varchar,convert(datetime,ME.StartingFrom) ,106) + ' ' +  convert(varchar,convert(datetime,ME.StartTime))  + '-' +
		CONVERT(VARCHAR,convert(datetime,Me.StartingTo),106 ) + '-' + CONVERT(VARCHAR,convert(datetime,Me.EndTime))) 
	END ) as EventTiming ,ME.StartingFrom as	StartingFrom ,ISNULL(Feature,0) as Feature,
(select top 1 a.Orgnizer_Name from Organizer_Master a join  Event_Orgnizer_Detail EOD on a.Orgnizer_Id=eod.OrganizerMaster_Id where  EOD.Orgnizer_Event_Id=Ev.EventID and EOD.DefaultOrg='Y' ) 
as  Orgnizer_Name,
(select top 1 AD.ConsolidateAddress from Address AD where  AD.EventId= EV.EventID ) as EventAddress,
(select top 1 AD.VenueName from Address AD where  AD.EventId= EV.EventID ) as Venuename,
anr.Email,
(SELECT   convert(varchar,isnull( sum(TQD_Remaining_Quantity),0)) 
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) as TicketDetail,
isnull((Select sum(TPD_Purchased_Qty) from Ticket_Purchased_Detail  where TPD_Event_Id=Ev.EventID),0) as Purchasedqty
 from ((((((Event Ev LEFT JOIN EventType ET
	ON Ev.EventTypeID = Et.EventTypeID ) LEFT JOIN EventCategory EC on EV.EventCategoryID =  EC.EventCategoryID)
	LEFT JOIN  EventSubCategory ESC on EV.EventSubCategoryID = ESC.EventSubCategoryID)
	LEFT JOIN EventVenue EVen
	ON Ev.EventID = EVen.EventID)
	LEFT JOIN MultipleEvent ME on EV.EventID = ME.EventID)
	Left join AspNetUsers ANR on Ev.UserID=ANR.Id )
	WHERE Ev.EventTitle like '%' + Coalesce(@EventTitle,Ev.EventTitle) + '%'
	AND isnull(Ev.EventTypeID,'')  = CASE WHEN @EventType >0 Then @EventType Else isnull(Ev.EventTypeID,'') END
	AND isnull(Ev.EventCategoryID,0)  = CASE WHEN @EventCat >0 Then @EventCat Else isnull(Ev.EventCategoryID,0) END
	AND isnull(Ev.EventSubCategoryID,0)  = CASE WHEN @EventSubCat >0 Then @EventSubCat  Else isnull(Ev.EventSubCategoryID,0) END
	AND ISNULL(Ev.Feature,0) = CASE WHEN @EventFeature >0 Then @EventFeature  Else ISNULL(Ev.Feature,0) END 
	order by (CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		(convert(datetime,convert(varchar, convert(datetime,EVen.EventEndDate),106))  + convert(datetime,convert(varchar,convert(time,EVen.EventEndTime),100)) )
	ELSE
		( convert(datetime,CONVERT(VARCHAR,convert(datetime,Me.StartingTo),106 )) + convert(datetime,CONVERT(VARCHAR,Me.EndTime)))
	END ) desc
	
	end
 if(@EventStartdate=1 and  @EventTicket=0 )
	begin 
	select EV.UserID,  EV.EventID as EventID , EventTitle,ET.EventType AS EventType,EC.EventCategory as EventCategory , ESC.EventSubCategory as EventSubCategory ,
	(CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		( convert(varchar, convert(datetime,EVen.EventStartDate),106) +' ' +  convert(varchar,convert(datetime,EVen.EventStartTime),100)  + '-' +
		convert(varchar, convert(datetime,EVen.EventEndDate),106) + ' ' + convert(varchar,convert(datetime,EVen.EventEndTime),100)) 
	ELSE
		( convert(varchar,convert(datetime,ME.StartingFrom) ,106) + ' ' +  convert(varchar,convert(datetime,ME.StartTime))  + '-' +
		CONVERT(VARCHAR,convert(datetime,Me.StartingTo),106 ) + '-' + CONVERT(VARCHAR,convert(datetime,Me.EndTime))) 
	END ) as EventTiming ,ME.StartingFrom as	StartingFrom ,ISNULL(Feature,0) as Feature,
(select top 1 a.Orgnizer_Name from Organizer_Master a join  Event_Orgnizer_Detail EOD on a.Orgnizer_Id=eod.OrganizerMaster_Id where  EOD.Orgnizer_Event_Id=Ev.EventID and EOD.DefaultOrg='Y' ) 
as  Orgnizer_Name,
(select top 1 AD.ConsolidateAddress from Address AD where  AD.EventId= EV.EventID ) as EventAddress,
(select top 1 AD.VenueName from Address AD where  AD.EventId= EV.EventID ) as Venuename,
anr.Email,
(SELECT   convert(varchar,isnull( sum(TQD_Remaining_Quantity),0)) 
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) as TicketDetail,
isnull((Select sum(TPD_Purchased_Qty) from Ticket_Purchased_Detail  where TPD_Event_Id=Ev.EventID),0) as Purchasedqty
 from ((((((Event Ev LEFT JOIN EventType ET
	ON Ev.EventTypeID = Et.EventTypeID ) LEFT JOIN EventCategory EC on EV.EventCategoryID =  EC.EventCategoryID)
	LEFT JOIN  EventSubCategory ESC on EV.EventSubCategoryID = ESC.EventSubCategoryID)
	LEFT JOIN EventVenue EVen
	ON Ev.EventID = EVen.EventID)
	LEFT JOIN MultipleEvent ME on EV.EventID = ME.EventID)
	Left join AspNetUsers ANR on Ev.UserID=ANR.Id )
	WHERE Ev.EventTitle like '%' + Coalesce(@EventTitle,Ev.EventTitle) + '%'
	AND isnull(Ev.EventTypeID,'')  = CASE WHEN @EventType >0 Then @EventType Else isnull(Ev.EventTypeID,'') END
	AND isnull(Ev.EventCategoryID,0)  = CASE WHEN @EventCat >0 Then @EventCat Else isnull(Ev.EventCategoryID,0) END
	AND isnull(Ev.EventSubCategoryID,0)  = CASE WHEN @EventSubCat >0 Then @EventSubCat  Else isnull(Ev.EventSubCategoryID,0) END
	AND ISNULL(Ev.Feature,0) = CASE WHEN @EventFeature >0 Then @EventFeature  Else ISNULL(Ev.Feature,0) END 
	AND (CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		(convert(datetime,convert(varchar, convert(datetime,EVen.EventEndDate),106))  + convert(datetime,convert(varchar,convert(time,EVen.EventEndTime),100)) )
	ELSE
		( convert(datetime,CONVERT(VARCHAR,convert(datetime,Me.StartingTo),106 )) + convert(datetime,CONVERT(VARCHAR,Me.EndTime)))
	END )>=GETDATE()
	order by (CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		(convert(datetime,convert(varchar, convert(datetime,EVen.EventEndDate),106))  + convert(datetime,convert(varchar,convert(time,EVen.EventEndTime),100)) )
	ELSE
		( convert(datetime,CONVERT(VARCHAR,convert(datetime,Me.StartingTo),106 )) + convert(datetime,CONVERT(VARCHAR,Me.EndTime)))
	END ) desc

	end
 if(@EventStartdate=2 and @EventTicket=0)
	begin 
	select EV.UserID, EV.EventID as EventID , EventTitle,ET.EventType AS EventType,EC.EventCategory as EventCategory , ESC.EventSubCategory as EventSubCategory ,
	(CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		( convert(varchar, convert(datetime,EVen.EventStartDate),106) +' ' +  convert(varchar,convert(datetime,EVen.EventStartTime),100)  + '-' +
		convert(varchar, convert(datetime,EVen.EventEndDate),106) + ' ' + convert(varchar,convert(datetime,EVen.EventEndTime),100)) 
	ELSE
		( convert(varchar,convert(datetime,ME.StartingFrom) ,106) + ' ' +  convert(varchar,convert(datetime,ME.StartTime))  + '-' +
		CONVERT(VARCHAR,convert(datetime,Me.StartingTo),106 ) + '-' + CONVERT(VARCHAR,convert(datetime,Me.EndTime))) 
	END ) as EventTiming ,ME.StartingFrom as	StartingFrom ,ISNULL(Feature,0) as Feature,
(select top 1 a.Orgnizer_Name from Organizer_Master a join  Event_Orgnizer_Detail EOD on a.Orgnizer_Id=eod.OrganizerMaster_Id where  EOD.Orgnizer_Event_Id=Ev.EventID and EOD.DefaultOrg='Y' ) 
as  Orgnizer_Name,
(select top 1 AD.ConsolidateAddress from Address AD where  AD.EventId= EV.EventID ) as EventAddress,
(select top 1 AD.VenueName from Address AD where  AD.EventId= EV.EventID ) as Venuename,
anr.Email,
(SELECT   convert(varchar,isnull( sum(TQD_Remaining_Quantity),0)) 
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) as TicketDetail,
isnull((Select sum(TPD_Purchased_Qty) from Ticket_Purchased_Detail  where TPD_Event_Id=Ev.EventID),0) as Purchasedqty
 from ((((((Event Ev LEFT JOIN EventType ET
	ON Ev.EventTypeID = Et.EventTypeID ) LEFT JOIN EventCategory EC on EV.EventCategoryID =  EC.EventCategoryID)
	LEFT JOIN  EventSubCategory ESC on EV.EventSubCategoryID = ESC.EventSubCategoryID)
	LEFT JOIN EventVenue EVen
	ON Ev.EventID = EVen.EventID)
	LEFT JOIN MultipleEvent ME on EV.EventID = ME.EventID)
	Left join AspNetUsers ANR on Ev.UserID=ANR.Id )
	WHERE Ev.EventTitle like '%' + Coalesce(@EventTitle,Ev.EventTitle) + '%'
	AND isnull(Ev.EventTypeID,'')  = CASE WHEN @EventType >0 Then @EventType Else isnull(Ev.EventTypeID,'') END
	AND isnull(Ev.EventCategoryID,0)  = CASE WHEN @EventCat >0 Then @EventCat Else isnull(Ev.EventCategoryID,0) END
	AND isnull(Ev.EventSubCategoryID,0)  = CASE WHEN @EventSubCat >0 Then @EventSubCat  Else isnull(Ev.EventSubCategoryID,0) END
	AND ISNULL(Ev.Feature,0) = CASE WHEN @EventFeature >0 Then @EventFeature  Else ISNULL(Ev.Feature,0) END 
	AND (CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		(convert(datetime,convert(varchar, convert(datetime,EVen.EventEndDate),106))  + convert(datetime,convert(varchar,convert(time,EVen.EventEndTime),100)) )
	ELSE
		( convert(datetime,CONVERT(VARCHAR,convert(datetime,Me.StartingTo),106 )) + convert(datetime,CONVERT(VARCHAR,Me.EndTime)))
	END )<GETDATE()
	order by (CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		(convert(datetime,convert(varchar, convert(datetime,EVen.EventEndDate),106))  + convert(datetime,convert(varchar,convert(time,EVen.EventEndTime),100)) )
	ELSE
		( convert(datetime,CONVERT(VARCHAR,convert(datetime,Me.StartingTo),106 )) + convert(datetime,CONVERT(VARCHAR,Me.EndTime)))
	END ) desc

	end

if(@EventStartdate=1 and @EventTicket=1)
	begin
		select EV.UserID,  EV.EventID as EventID , EventTitle,ET.EventType AS EventType,EC.EventCategory as EventCategory , ESC.EventSubCategory as EventSubCategory ,
	(CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		( convert(varchar, convert(datetime,EVen.EventStartDate),106) +' ' +  convert(varchar,convert(datetime,EVen.EventStartTime),100)  + '-' +
		convert(varchar, convert(datetime,EVen.EventEndDate),106) + ' ' + convert(varchar,convert(datetime,EVen.EventEndTime),100)) 
	ELSE
		( convert(varchar,convert(datetime,ME.StartingFrom) ,106) + ' ' +  convert(varchar,convert(datetime,ME.StartTime))  + '-' +
		CONVERT(VARCHAR,convert(datetime,Me.StartingTo),106 ) + '-' + CONVERT(VARCHAR,convert(datetime,Me.EndTime))) 
	END ) as EventTiming ,ME.StartingFrom as	StartingFrom ,ISNULL(Feature,0) as Feature,
(select top 1 a.Orgnizer_Name from Organizer_Master a join  Event_Orgnizer_Detail EOD on a.Orgnizer_Id=eod.OrganizerMaster_Id where  EOD.Orgnizer_Event_Id=Ev.EventID and EOD.DefaultOrg='Y' ) 
as  Orgnizer_Name,
(select top 1 AD.ConsolidateAddress from Address AD where  AD.EventId= EV.EventID ) as EventAddress,
(select top 1 AD.VenueName from Address AD where  AD.EventId= EV.EventID ) as Venuename,
anr.Email,
(SELECT    convert(varchar,isnull( sum(TQD_Remaining_Quantity),0)) 
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) as TicketDetail,
isnull((Select sum(TPD_Purchased_Qty) from Ticket_Purchased_Detail  where TPD_Event_Id=Ev.EventID),0) as Purchasedqty
 from ((((((Event Ev LEFT JOIN EventType ET
	ON Ev.EventTypeID = Et.EventTypeID ) LEFT JOIN EventCategory EC on EV.EventCategoryID =  EC.EventCategoryID)
	LEFT JOIN  EventSubCategory ESC on EV.EventSubCategoryID = ESC.EventSubCategoryID)
	LEFT JOIN EventVenue EVen
	ON Ev.EventID = EVen.EventID)
	LEFT JOIN MultipleEvent ME on EV.EventID = ME.EventID)
	Left join AspNetUsers ANR on Ev.UserID=ANR.Id )
	WHERE Ev.EventTitle like '%' + Coalesce(@EventTitle,Ev.EventTitle) + '%'
	AND isnull(Ev.EventTypeID,'')  = CASE WHEN @EventType >0 Then @EventType Else isnull(Ev.EventTypeID,'') END
	AND isnull(Ev.EventCategoryID,0)  = CASE WHEN @EventCat >0 Then @EventCat Else isnull(Ev.EventCategoryID,0) END
	AND isnull(Ev.EventSubCategoryID,0)  = CASE WHEN @EventSubCat >0 Then @EventSubCat  Else isnull(Ev.EventSubCategoryID,0) END
	AND ISNULL(Ev.Feature,0) = CASE WHEN @EventFeature >0 Then @EventFeature  Else ISNULL(Ev.Feature,0) END 
	AND (CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		(convert(datetime,convert(varchar, convert(datetime,EVen.EventEndDate),106))  + convert(datetime,convert(varchar,convert(time,EVen.EventEndTime),100)) )
	ELSE
		( convert(datetime,CONVERT(VARCHAR,convert(datetime,Me.StartingTo),106 )) + convert(datetime,CONVERT(VARCHAR,Me.EndTime)))
	END )>=GETDATE()
	AND (SELECT    isnull( sum(TQD_Remaining_Quantity),0)
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) <='No'
order by (CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		(convert(datetime,convert(varchar, convert(datetime,EVen.EventEndDate),106))  + convert(datetime,convert(varchar,convert(time,EVen.EventEndTime),100)) )
	ELSE
		( convert(datetime,CONVERT(VARCHAR,convert(datetime,Me.StartingTo),106 )) + convert(datetime,CONVERT(VARCHAR,Me.EndTime)))
	END ) desc
	end
 if(@EventStartdate=1 and @EventTicket=2)
	begin
		select EV.UserID,  EV.EventID as EventID , EventTitle,ET.EventType AS EventType,EC.EventCategory as EventCategory , ESC.EventSubCategory as EventSubCategory ,
	(CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		( convert(varchar, convert(datetime,EVen.EventStartDate),106) +' ' +  convert(varchar,convert(datetime,EVen.EventStartTime),100)  + '-' +
		convert(varchar, convert(datetime,EVen.EventEndDate),106) + ' ' + convert(varchar,convert(datetime,EVen.EventEndTime),100)) 
	ELSE
		( convert(varchar,convert(datetime,ME.StartingFrom) ,106) + ' ' +  convert(varchar,convert(datetime,ME.StartTime))  + '-' +
		CONVERT(VARCHAR,convert(datetime,Me.StartingTo),106 ) + '-' + CONVERT(VARCHAR,convert(datetime,Me.EndTime))) 
	END ) as EventTiming ,ME.StartingFrom as	StartingFrom ,ISNULL(Feature,0) as Feature,
(select top 1 a.Orgnizer_Name from Organizer_Master a join  Event_Orgnizer_Detail EOD on a.Orgnizer_Id=eod.OrganizerMaster_Id where  EOD.Orgnizer_Event_Id=Ev.EventID and EOD.DefaultOrg='Y' ) 
as  Orgnizer_Name,
(select top 1 AD.ConsolidateAddress from Address AD where  AD.EventId= EV.EventID ) as EventAddress,
(select top 1 AD.VenueName from Address AD where  AD.EventId= EV.EventID ) as Venuename,
anr.Email,
(SELECT    convert(varchar,isnull( sum(TQD_Remaining_Quantity),0)) 
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) as TicketDetail,
isnull((Select sum(TPD_Purchased_Qty) from Ticket_Purchased_Detail  where TPD_Event_Id=Ev.EventID),0) as Purchasedqty
 from ((((((Event Ev LEFT JOIN EventType ET
	ON Ev.EventTypeID = Et.EventTypeID ) LEFT JOIN EventCategory EC on EV.EventCategoryID =  EC.EventCategoryID)
	LEFT JOIN  EventSubCategory ESC on EV.EventSubCategoryID = ESC.EventSubCategoryID)
	LEFT JOIN EventVenue EVen
	ON Ev.EventID = EVen.EventID)
	LEFT JOIN MultipleEvent ME on EV.EventID = ME.EventID)
	Left join AspNetUsers ANR on Ev.UserID=ANR.Id )
	WHERE Ev.EventTitle like '%' + Coalesce(@EventTitle,Ev.EventTitle) + '%'
	AND isnull(Ev.EventTypeID,'')  = CASE WHEN @EventType >0 Then @EventType Else isnull(Ev.EventTypeID,'') END
	AND isnull(Ev.EventCategoryID,0)  = CASE WHEN @EventCat >0 Then @EventCat Else isnull(Ev.EventCategoryID,0) END
	AND isnull(Ev.EventSubCategoryID,0)  = CASE WHEN @EventSubCat >0 Then @EventSubCat  Else isnull(Ev.EventSubCategoryID,0) END
	AND ISNULL(Ev.Feature,0) = CASE WHEN @EventFeature >0 Then @EventFeature  Else ISNULL(Ev.Feature,0) END 
	AND (CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		(convert(datetime,convert(varchar, convert(datetime,EVen.EventEndDate),106))  + convert(datetime,convert(varchar,convert(time,EVen.EventEndTime),100)) )
	ELSE
		( convert(datetime,CONVERT(VARCHAR,convert(datetime,Me.StartingTo),106 )) + convert(datetime,CONVERT(VARCHAR,Me.EndTime)))
	END )>=GETDATE()
	AND (SELECT   isnull( sum(TQD_Remaining_Quantity),0)
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) >0
order by (CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		(convert(datetime,convert(varchar, convert(datetime,EVen.EventEndDate),106))  + convert(datetime,convert(varchar,convert(time,EVen.EventEndTime),100)) )
	ELSE
		( convert(datetime,CONVERT(VARCHAR,convert(datetime,Me.StartingTo),106 )) + convert(datetime,CONVERT(VARCHAR,Me.EndTime)))
	END ) desc
	end
  
   if(@EventStartdate=2 and @EventTicket=1)
	begin
		select EV.UserID, EV.EventID as EventID , EventTitle,ET.EventType AS EventType,EC.EventCategory as EventCategory , ESC.EventSubCategory as EventSubCategory ,
	(CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		( convert(varchar, convert(datetime,EVen.EventStartDate),106) +' ' +  convert(varchar,convert(datetime,EVen.EventStartTime),100)  + '-' +
		convert(varchar, convert(datetime,EVen.EventEndDate),106) + ' ' + convert(varchar,convert(datetime,EVen.EventEndTime),100)) 
	ELSE
		( convert(varchar,convert(datetime,ME.StartingFrom) ,106) + ' ' +  convert(varchar,convert(datetime,ME.StartTime))  + '-' +
		CONVERT(VARCHAR,convert(datetime,Me.StartingTo),106 ) + '-' + CONVERT(VARCHAR,convert(datetime,Me.EndTime))) 
	END ) as EventTiming ,ME.StartingFrom as	StartingFrom ,ISNULL(Feature,0) as Feature,
(select top 1 a.Orgnizer_Name from Organizer_Master a join  Event_Orgnizer_Detail EOD on a.Orgnizer_Id=eod.OrganizerMaster_Id where  EOD.Orgnizer_Event_Id=Ev.EventID and EOD.DefaultOrg='Y' ) 
as  Orgnizer_Name,
(select top 1 AD.ConsolidateAddress from Address AD where  AD.EventId= EV.EventID ) as EventAddress,
(select top 1 AD.VenueName from Address AD where  AD.EventId= EV.EventID ) as Venuename,
anr.Email,
(SELECT    convert(varchar,isnull( sum(TQD_Remaining_Quantity),0)) 
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) as TicketDetail,
isnull((Select sum(TPD_Purchased_Qty) from Ticket_Purchased_Detail  where TPD_Event_Id=Ev.EventID),0) as Purchasedqty
 from ((((((Event Ev LEFT JOIN EventType ET
	ON Ev.EventTypeID = Et.EventTypeID ) LEFT JOIN EventCategory EC on EV.EventCategoryID =  EC.EventCategoryID)
	LEFT JOIN  EventSubCategory ESC on EV.EventSubCategoryID = ESC.EventSubCategoryID)
	LEFT JOIN EventVenue EVen
	ON Ev.EventID = EVen.EventID)
	LEFT JOIN MultipleEvent ME on EV.EventID = ME.EventID)
	Left join AspNetUsers ANR on Ev.UserID=ANR.Id )
	WHERE Ev.EventTitle like '%' + Coalesce(@EventTitle,Ev.EventTitle) + '%'
	AND isnull(Ev.EventTypeID,'')  = CASE WHEN @EventType >0 Then @EventType Else isnull(Ev.EventTypeID,'') END
	AND isnull(Ev.EventCategoryID,0)  = CASE WHEN @EventCat >0 Then @EventCat Else isnull(Ev.EventCategoryID,0) END
	AND isnull(Ev.EventSubCategoryID,0)  = CASE WHEN @EventSubCat >0 Then @EventSubCat  Else isnull(Ev.EventSubCategoryID,0) END
	AND ISNULL(Ev.Feature,0) = CASE WHEN @EventFeature >0 Then @EventFeature  Else ISNULL(Ev.Feature,0) END 
	AND (CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		(convert(datetime,convert(varchar, convert(datetime,EVen.EventEndDate),106))  + convert(datetime,convert(varchar,convert(time,EVen.EventEndTime),100)) )
	ELSE
		( convert(datetime,CONVERT(VARCHAR,convert(datetime,Me.StartingTo),106 )) + convert(datetime,CONVERT(VARCHAR,Me.EndTime)))
	END )<GETDATE()
	AND (SELECT   isnull( sum(TQD_Remaining_Quantity),0)
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) <=0
order by (CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		(convert(datetime,convert(varchar, convert(datetime,EVen.EventEndDate),106))  + convert(datetime,convert(varchar,convert(time,EVen.EventEndTime),100)) )
	ELSE
		( convert(datetime,CONVERT(VARCHAR,convert(datetime,Me.StartingTo),106 )) + convert(datetime,CONVERT(VARCHAR,Me.EndTime)))
	END ) desc
	end

 if(@EventStartdate=2 and @EventTicket=2)
	begin
		select EV.UserID, EV.EventID as EventID , EventTitle,ET.EventType AS EventType,EC.EventCategory as EventCategory , ESC.EventSubCategory as EventSubCategory ,
	(CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		( convert(varchar, convert(datetime,EVen.EventStartDate),106) +' ' +  convert(varchar,convert(datetime,EVen.EventStartTime),100)  + '-' +
		convert(varchar, convert(datetime,EVen.EventEndDate),106) + ' ' + convert(varchar,convert(datetime,EVen.EventEndTime),100)) 
	ELSE
		( convert(varchar,convert(datetime,ME.StartingFrom) ,106) + ' ' +  convert(varchar,convert(datetime,ME.StartTime))  + '-' +
		CONVERT(VARCHAR,convert(datetime,Me.StartingTo),106 ) + '-' + CONVERT(VARCHAR,convert(datetime,Me.EndTime))) 
	END ) as EventTiming ,ME.StartingFrom as	StartingFrom ,ISNULL(Feature,0) as Feature,
(select top 1 a.Orgnizer_Name from Organizer_Master a join  Event_Orgnizer_Detail EOD on a.Orgnizer_Id=eod.OrganizerMaster_Id where  EOD.Orgnizer_Event_Id=Ev.EventID and EOD.DefaultOrg='Y' ) 
as  Orgnizer_Name,
(select top 1 AD.ConsolidateAddress from Address AD where  AD.EventId= EV.EventID ) as EventAddress,
(select top 1 AD.VenueName from Address AD where  AD.EventId= EV.EventID ) as Venuename,
anr.Email,
(SELECT    convert(varchar,isnull( sum(TQD_Remaining_Quantity),0)) 
   
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) as TicketDetail,
isnull((Select sum(TPD_Purchased_Qty) from Ticket_Purchased_Detail  where TPD_Event_Id=Ev.EventID),0) as Purchasedqty
 from ((((((Event Ev LEFT JOIN EventType ET
	ON Ev.EventTypeID = Et.EventTypeID ) LEFT JOIN EventCategory EC on EV.EventCategoryID =  EC.EventCategoryID)
	LEFT JOIN  EventSubCategory ESC on EV.EventSubCategoryID = ESC.EventSubCategoryID)
	LEFT JOIN EventVenue EVen
	ON Ev.EventID = EVen.EventID)
	LEFT JOIN MultipleEvent ME on EV.EventID = ME.EventID)
	Left join AspNetUsers ANR on Ev.UserID=ANR.Id )
	WHERE Ev.EventTitle like '%' + Coalesce(@EventTitle,Ev.EventTitle) + '%'
	AND isnull(Ev.EventTypeID,'')  = CASE WHEN @EventType >0 Then @EventType Else isnull(Ev.EventTypeID,'') END
	AND isnull(Ev.EventCategoryID,0)  = CASE WHEN @EventCat >0 Then @EventCat Else isnull(Ev.EventCategoryID,0) END
	AND isnull(Ev.EventSubCategoryID,0)  = CASE WHEN @EventSubCat >0 Then @EventSubCat  Else isnull(Ev.EventSubCategoryID,0) END
	AND ISNULL(Ev.Feature,0) = CASE WHEN @EventFeature >0 Then @EventFeature  Else ISNULL(Ev.Feature,0) END 
	AND (CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		(convert(datetime,convert(varchar, convert(datetime,EVen.EventEndDate),106))  + convert(datetime,convert(varchar,convert(time,EVen.EventEndTime),100)) )
	ELSE
		( convert(datetime,CONVERT(VARCHAR,convert(datetime,Me.StartingTo),106 )) + convert(datetime,CONVERT(VARCHAR,Me.EndTime)))
	END )<GETDATE()
	AND (SELECT   isnull( sum(TQD_Remaining_Quantity),0)
FROM Ticket_Quantity_Detail
where TQD_Event_Id=Ev.EventID ) >0
order by (CASE WHEN ISNULL(EVen.EventID,0) != 0 THEN
		(convert(datetime,convert(varchar, convert(datetime,EVen.EventEndDate),106))  + convert(datetime,convert(varchar,convert(time,EVen.EventEndTime),100)) )
	ELSE
		( convert(datetime,CONVERT(VARCHAR,convert(datetime,Me.StartingTo),106 )) + convert(datetime,CONVERT(VARCHAR,Me.EndTime)))
	END ) desc
	end
  
	
END








GO
/****** Object:  StoredProcedure [dbo].[GetEventsListByStatus]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetEventsListByStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetEventsListByStatus] AS' 
END
GO



-- GetEventsListByStatus '','LIVE','e583e85c-bf2f-43e4-9d56-5a20c73796be'
ALTER Procedure [dbo].[GetEventsListByStatus]

@EventTitle nvarchar(200),

@EventStatus varchar(20),
@UserID nvarchar(100)

as

BEGIN

IF UPPER(@EventStatus)='LIVE'
BEGIN
	With GrpEvent as (
		SELECT Max(EventId) as EV,Parent_EventID FROM EVENT WHERE ISNULL(Parent_EventID,0) >0 and UserID =@UserID GROUP BY 
		Parent_EventID
		UNION 
		SELECT EventId as EV,Parent_EventID FROM EVENT WHERE ISNULL(Parent_EventID,0) <= 0 AND UserID =@UserID And EventID 
		NOT IN 
		(SELECT Parent_EventID FROM EVENT)
	)

	Select * From 
	(SELECT Name,Email,Date_Time,TicketType,TicketPurchased,OrderId,EventID,EventTitle,EventDate,
	TotalTicket,TicketSold,EndTime as EventTime,EventCancel
	from (Select distinct  (p.FirstName+'  '+p.LastName) as Name,P.Email as EMail, a.UserID, a.EventID,
	'' as Date_Time,'' as TicketType,CONVERT(bigint, 0) as TicketPurchased ,'' as OrderId,LEFT(a.EventTitle,200) as EventTitle,
	isnull(b.StartingFrom,'')  as EventDate,
	isnull(b.StartingTo,'') as EventEnddate,
	(select ISNULL(SUM(TQD_Quantity),0) from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TotalTicket,
	(select ISNULL(SUM(TQD_Quantity)- SUM(TQD_Remaining_Quantity),0)   from Ticket_Quantity_Detail tq where
	 tq.TQD_Event_Id=a.EventID) as TicketSold,
	a.EventStatus,a.EventCancel,isnull(b.StartTime,'') as EndTime,isnull(b.EndTime,'') as EventendTime
	from event  a inner join MultipleEvent b on a.EventID =b.EventID 
	inner join Profile p on a.UserID=p.UserID 
	UNION 

	Select distinct (p.FirstName+'  '+p.LastName) as Name,P.Email as EMail, a.UserID, a.EventID,
	'' as Date_Time,'' as TicketType,CONVERT(bigint, 0) as TicketPurchased ,'' as OrderId, LEFT(a.EventTitle,200) as EventTitle,
	 isnull(b.EventStartDate,'')  as EventDate,isnull(b.EventEndDate,'') as EventEnddate,
	
	(select ISNULL(SUM(TQD_Quantity),0) from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TotalTicket,
	(select ISNULL(SUM(TQD_Quantity)- SUM(TQD_Remaining_Quantity),0)   from Ticket_Quantity_Detail tq where 
	tq.TQD_Event_Id=a.EventID) as TicketSold,
	a.EventStatus,a.EventCancel,
	isnull(b.EventStartTime,'') as EndTime,isnull(b.EventEndTime,'') as EventendTime
	 from event  a inner join EventVenue b on a.EventID =b.EventID 
	 inner join Profile p on a.UserID=p.UserID 

	UNION 
		 
	Select distinct (p.FirstName+'  '+p.LastName) as Name ,P.Email as EMail,a.UserID,a.EventID, 
	'' as Date_Time,'' as TicketType,CONVERT(bigint, 0) as TicketPurchased ,'' as OrderId,LEFT(a.EventTitle,200) as EventTitle,
		''  as EventDate,'' as EventEnddate,

	(select ISNULL(SUM(TQD_Quantity),0) from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TotalTicket,
	(select ISNULL(SUM(TQD_Quantity)- SUM(TQD_Remaining_Quantity),0)   from Ticket_Quantity_Detail tq where
	 tq.TQD_Event_Id=a.EventID) as TicketSold,
	a.EventStatus,a.EventCancel,
	'' as EndTime,'' as EventendTime
	 from event  a  
	 inner join Profile p on a.UserID=p.UserID 
	  where 
	(select count(*) from EventVenue b where b.EventID=a.EventID )<=0
	and (select count(*) from MultipleEvent b where b.EventID=a.EventID )<=0    ) as xEven

	 where xEven.EventStatus='Live' and isnull(xEven.EventCancel,'')!='Y' and 
	 (convert(datetime,(xEven.EventEnddate))+ convert(datetime,xEven.EventendTime))>=GETDATE()  and xEven.UserID=@UserID

	 and  EventTitle like '%'+ISNULL(@EventTitle,'')+'%' 
	 ) as LiveEventList where EventID in (Select Ev from GrpEvent) order by convert(datetime,EventDate) asc

	


END
ELSE IF UPPER(@EventStatus)='SAVE'

	
	select Name,Email,Date_Time,TicketType,TicketPurchased,OrderId,EventID,EventTitle,EventDate,
	TotalTicket,TicketSold,EndTime as EventTime,EventCancel
	 from  ( Select (p.FirstName+'  '+p.LastName) as Name,P.Email as EMail, a.UserID, a.EventID,
	'' as Date_Time,'' as TicketType,CONVERT(bigint, 0) as TicketPurchased ,'' as OrderId,LEFT(a.EventTitle,200) as EventTitle,
	 isnull(b.StartingFrom,'')  as EventDate,
 isnull(b.StartingTo,'') as EventEnddate,
(select ISNULL(SUM(TQD_Quantity),0) from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TotalTicket,
(select ISNULL(SUM(TQD_Quantity)- SUM(TQD_Remaining_Quantity),0)   from Ticket_Quantity_Detail tq where 
tq.TQD_Event_Id=a.EventID) as TicketSold,
a.EventStatus,a.EventCancel,isnull(b.StartTime,'') as EndTime,isnull(b.EndTime,'') as EventendTime
 from event  a inner join MultipleEvent b on a.EventID =b.EventID 
 inner join Profile p on a.UserID=p.UserID 
	union 

	Select (p.FirstName+'  '+p.LastName) as Name,P.Email as EMail, a.UserID, a.EventID,
	'' as Date_Time,'' as TicketType,CONVERT(bigint, 0) as TicketPurchased ,'' as OrderId, LEFT(a.EventTitle,200) as EventTitle,
	 isnull(b.EventStartDate,'')  as EventDate,isnull(b.EventEndDate,'') as EventEnddate,
	
(select ISNULL(SUM(TQD_Quantity),0) from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TotalTicket,
(select ISNULL(SUM(TQD_Quantity)- SUM(TQD_Remaining_Quantity),0)   from Ticket_Quantity_Detail tq where 
tq.TQD_Event_Id=a.EventID) as TicketSold,
a.EventStatus,a.EventCancel,
isnull(b.EventStartTime,'') as EndTime,isnull(b.EventEndTime,'') as EventendTime
 from event  a inner join EventVenue b on a.EventID =b.EventID 
 inner join Profile p on a.UserID=p.UserID 
	union 
		 
	 	Select  (p.FirstName+'  '+p.LastName) as Name ,P.Email as EMail,a.UserID,a.EventID, 
	'' as Date_Time,'' as TicketType,CONVERT(bigint, 0) as TicketPurchased ,'' as OrderId,LEFT(a.EventTitle,200) as EventTitle,
		''  as EventDate,'' as EventEnddate,

(select ISNULL(SUM(TQD_Quantity),0) from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TotalTicket,
(select ISNULL(SUM(TQD_Quantity)- SUM(TQD_Remaining_Quantity),0)   from Ticket_Quantity_Detail tq where 
tq.TQD_Event_Id=a.EventID) as TicketSold,
a.EventStatus,a.EventCancel,
'' as EndTime,'' as EventendTime
 from event  a  
 inner join Profile p on a.UserID=p.UserID 
  where 
(select count(*) from EventVenue b where b.EventID=a.EventID )<=0
	 and (select count(*) from MultipleEvent b where b.EventID=a.EventID )<=0    ) as xEven

	 where xEven.EventStatus='Save' and isnull(xEven.EventCancel,'')!='Y' and 
	 (convert(datetime,(xEven.EventEnddate))+ convert(datetime,xEven.EventendTime))>=GETDATE()  and xEven.UserID=@UserID

	 and  EventTitle like '%'+ISNULL(@EventTitle,'')+'%' 
	 order by convert(datetime,xeven.EventDate) asc

	

ELSE IF UPPER(@EventStatus)='PAST'

	
	select Name,Email,Date_Time,TicketType,TicketPurchased,OrderId,EventID,EventTitle,EventDate,
	TotalTicket,TicketSold,EndTime as EventTime,EventCancel
	 from  ( Select (p.FirstName+'  '+p.LastName) as Name,P.Email as EMail, a.UserID, a.EventID,
	'' as Date_Time,'' as TicketType,CONVERT(bigint, 0) as TicketPurchased ,'' as OrderId,LEFT(a.EventTitle,200) as EventTitle,
	 isnull(b.StartingFrom,'')  as EventDate,
 isnull(b.StartingTo,'') as EventEnddate,
(select ISNULL(SUM(TQD_Quantity),0) from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TotalTicket,
(select ISNULL(SUM(TQD_Quantity)- SUM(TQD_Remaining_Quantity),0)   from Ticket_Quantity_Detail tq where
 tq.TQD_Event_Id=a.EventID) as TicketSold,
a.EventStatus,a.EventCancel,isnull(b.StartTime,'') as EndTime,isnull(b.EndTime,'') as EventendTime
 from event  a inner join MultipleEvent b on a.EventID =b.EventID 
 inner join Profile p on a.UserID=p.UserID  
	union 

	Select (p.FirstName+'  '+p.LastName) as Name,P.Email as EMail, a.UserID, a.EventID,
	'' as Date_Time,'' as TicketType,CONVERT(bigint, 0) as TicketPurchased ,'' as OrderId, LEFT(a.EventTitle,200) as EventTitle,
	 isnull(b.EventStartDate,'')  as EventDate,isnull(b.EventEndDate,'') as EventEnddate,
	
(select ISNULL(SUM(TQD_Quantity),0) from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TotalTicket,
(select ISNULL(SUM(TQD_Quantity)- SUM(TQD_Remaining_Quantity),0)   from Ticket_Quantity_Detail tq where 
tq.TQD_Event_Id=a.EventID) as TicketSold,
a.EventStatus,a.EventCancel,
isnull(b.EventStartTime,'') as EndTime,isnull(b.EventEndTime,'') as EventendTime
 from event  a inner join EventVenue b on a.EventID =b.EventID 
 inner join Profile p on a.UserID=p.UserID   
	union 
		 
	 	Select  (p.FirstName+'  '+p.LastName) as Name ,P.Email as EMail,a.UserID,a.EventID, 
	'' as Date_Time,'' as TicketType,CONVERT(bigint, 0) as TicketPurchased ,'' as OrderId,LEFT(a.EventTitle,200) as EventTitle,
		''  as EventDate,'' as EventEnddate,

(select ISNULL(SUM(TQD_Quantity),0) from Ticket_Quantity_Detail tq where tq.TQD_Event_Id=a.EventID) as TotalTicket,
(select ISNULL(SUM(TQD_Quantity)- SUM(TQD_Remaining_Quantity),0)   from Ticket_Quantity_Detail tq where 
tq.TQD_Event_Id=a.EventID) as TicketSold,
a.EventStatus,a.EventCancel,
'' as EndTime,'' as EventendTime
 from event  a  
 inner join Profile p on a.UserID=p.UserID 
  where 
(select count(*) from EventVenue b where b.EventID=a.EventID )<=0
	 and (select count(*) from MultipleEvent b where b.EventID=a.EventID )<=0   
	 
	 
	 
	 
	    ) as xEven

	 where ( (convert(datetime,(xEven.EventEnddate))+ convert(datetime,xEven.EventendTime))<GETDATE() or isnull(xEven.EventCancel,'')='Y') and xEven.UserID=@UserID 

	 and  EventTitle like '%'+ISNULL(@EventTitle,'')+'%' 
	 order by convert(datetime,xeven.EventDate) asc

	


ELSE IF UPPER(@EventStatus)='GUEST'

	select (PR.FirstName+' '+PR.LastName) as Name,Pr.Email, LEFT(ev.EventTitle,200) as EventTitle,
	(evn.EventStartDate+' '+evn.EventStartTime) as Date_Time,

	tt.TicketType, SUM(TPD_Purchased_Qty) as TicketPurchased,TPD_Order_Id as OrderId,ev.EventID,'' as EventDate,
	'' as EventTime ,'N' as EventCancel,CONVERT(bigint, 0) as TotalTicket,CONVERT(bigint, 0) as TicketSold

	from Ticket_Purchased_Detail TPD inner join Profile Pr 

	on TPD.TPD_User_Id=Pr.UserID

	LEFT OUTER JOIN Event ev on TPD.TPD_Event_Id=ev.EventID

	LEFT OUTER JOIN EventVenue evn on TPD.TPD_Event_Id=evn.EventID

	LEFT OUTER JOIN Ticket_Quantity_Detail tqd on TPD.TPD_TQD_Id=tqd.TQD_Id

	LEFT OUTER JOIN Ticket t on tqd.TQD_Ticket_Id=t.T_Id

	LEFT OUTER JOIN TicketType tt on t.TicketTypeID=tt.TicketTypeID

	where ev.UserID=@UserID AND (pr.FirstName like '%'+ISNULL(@EventTitle,'') +'%' or 
	ev.EventTitle like '%'+ISNULL(@EventTitle,'') +'%' or  pr.LastName like '%'+ISNULL(@EventTitle,'') +'%' )

	group by TPD.TPD_User_Id,(PR.FirstName+' '+PR.LastName),Pr.Email,EventTitle,TPD_Order_Id,
	(evn.EventStartDate+' '+evn.EventStartTime),tt.TicketType,ev.EventID





END





GO
/****** Object:  StoredProcedure [dbo].[GetLantLong]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetLantLong]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetLantLong] AS' 
END
GO






--exec [GetEventListing] 

ALTER PROC [dbo].[GetLantLong]
(
	@Lat varchar(50),
	@Long varchar(50)
)
AS 
BEGIN
	DECLARE @Result varchar(Max) 
	Set @Result =''
	SELECT @Result = @Result + ',' + convert(varchar(max),EventId) FROM 
	(SELECT dbo.distance(@Lat, @Long, Latitude, Longitude) discoverdistance,* FROM ADDRESS) as Temp
	WHERE isnull(Temp.discoverdistance,0) <=20 and  isnull(Temp.discoverdistance,0) > 0
	SELECT SUBSTRING(@Result,2,Len(@Result))

END

--28.6139, 77.2090







GO
/****** Object:  StoredProcedure [dbo].[GetOrganizerEventid]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetOrganizerEventid]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetOrganizerEventid] AS' 
END
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[GetOrganizerEventid] 
	@Masterid bigint 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from [Event_Orgnizer_Detail]  where Orgnizer_Event_Id in   (select a.Orgnizer_Event_Id as eventid from [dbo].[Event_Orgnizer_Detail] a inner join event b on a.Orgnizer_Event_Id=b.EventID
where b.Parent_EventID=0
and a.Orgnizer_Event_Id not in (select distinct Parent_EventID from event  where EventID in (select Orgnizer_Event_Id from [Event_Orgnizer_Detail] ) and Parent_EventID!=0)
union
select Max(Orgnizer_Event_Id) as eventid from Event_Orgnizer_Detail a inner join event b on a.Orgnizer_Event_Id=b.EventID
where a.Orgnizer_Event_Id  in (select distinct eventid from event  where Parent_EventID in (select Orgnizer_Event_Id from [Event_Orgnizer_Detail] ) and Parent_EventID!=0)
group by b.Parent_EventID
union
select Max(Orgnizer_Event_Id) as eventid from Event_Orgnizer_Detail a inner join event b on a.Orgnizer_Event_Id=b.EventID
where a.Orgnizer_Event_Id  in (select distinct eventid from event  where Parent_EventID not in (select Orgnizer_Event_Id from [Event_Orgnizer_Detail] ) and Parent_EventID!=0)
group by b.Parent_EventID)   and OrganizerMaster_Id=@Masterid
END


GO
/****** Object:  StoredProcedure [dbo].[GetSelectedTicketListing]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSelectedTicketListing]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetSelectedTicketListing] AS' 
END
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
				SET @Html = CONCAT(@Html,'<div class="divTableCell text-right">Quantity</div>')
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
			
			SET @Html = CONCAT(@Html,'<div class="col-sm-12 col-xs-12 no_pad divticketaddres">',@Addess,'</div>')
			
			SET @Html = CONCAT(@Html,'<div class="divTable">')
			SET @Html = CONCAT(@Html,'<div class="divTablehead">')
				SET @Html = CONCAT(@Html,'<div class="divTableCell">Ticket Type</div>')
				SET @Html = CONCAT(@Html,'<div class="divTableCell text-right">', 'Price','</div>')
				SET @Html =	CONCAT(@Html,'<div class="divTableCell text-right">Fee</div>')
				SET @Html = CONCAT(@Html,'<div class="divTableCell text-right">Quantity</div>')
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
SET @Html = CONCAT(@Html,'<p class="ticketsubtotal text-right">', 'Promo Code ','<input type="text" class="form-control evnt_inp_cont table_promo_input"  placeholder="" id="txtPromoCode','" value="" />','</p>') 
SET @Html = CONCAT(@Html,'<p class="ticketsubtotal text-right">','<b>', 'Grand Total : $ ', '<span id="spGrdTotal">',CONVERT(NUMERIC(18,2),@GrandTotal) ,'</span></b>','</p>')

SET @Html = CONCAT(@Html,'<input id="hdIds" type="hidden" value=', @QtyIds ,' />')
SET @Html = CONCAT(@Html,'<input id="hdOrderTotal" type="hidden" value=', @RunningTotal ,' />')
SET @Html = CONCAT(@Html,'<input id="hdGrandTotal" type="hidden" value=', @GrandTotal ,' />')
SET @Html = CONCAT(@Html,'<input id="hdPromoId" type="hidden" value=', '0' ,' />')
SET @Html = CONCAT(@Html,'<input id="hdVarChanges" type="hidden" value=', @VariableChanges ,' />')
SET @Html = CONCAT(@Html,'<input id="hdVarId" type="hidden" value="', @VariableIds ,'" />')
SET @Html = CONCAT(@Html,'<input id="hidQty" type="hidden" value=', @PaidTicketQty ,' />')
SET @Html = CONCAT(@Html,'</div></div>')
Select @Html as Ticket


END































GO
/****** Object:  StoredProcedure [dbo].[GetSetUserRole]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSetUserRole]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetSetUserRole] AS' 
END
GO

ALTER proc [dbo].[GetSetUserRole](
	@user_Id nvarchar(500),
	@GETSET varchar(3),
	@Role_Id varchar(50)
)
AS 
BEGIN
	Declare @RId varchar(50)
	SET @RId =''
	IF (UPPER(@GETSET) = 'GET')
	BEGIN
		SELECT @RID =RoleId FROM AspNetUserRoles WHERE UserId = @user_Id 
	END
	ELSE IF (UPPER(@GETSET) = 'SET')
	BEGIN
		SELECT @RID = Isnull(RoleId,'') FROM AspNetUserRoles WHERE USERID = @user_Id 
		IF (@RID ='')
		BEGIN
			DELETE from AspNetUserRoles where USERID = @user_Id
			INSERT INTO AspNetUserRoles (UserId,RoleId) VALUES(@user_Id,@Role_Id)
		END
		ELSE
			UPDATE AspNetUserRoles SET RoleId = @Role_Id WHERE UserId= @user_Id 
	END
	Select @RId

END



GO
/****** Object:  StoredProcedure [dbo].[GetTicketListing]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTicketListing]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetTicketListing] AS' 
END
GO



ALTER PROC [dbo].[GetTicketListing]
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

WHILE (@RCnt>0)
BEGIN
	SELECT @PublishId = P_Id FROM @tblPublish WHERE Id= @RInd
	SELECT @StartDate = (ISNULL(PE_Scheduled_Date,'') + ', ' + ISNULL(PE_Start_Time,'')), @AddIdss=isnull(PE_Address_Ids,'')
	FROM Publish_Event_Detail WHERE PE_Id = @PublishId
	


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
			SET @Html = CONCAT(@Html,'<div class="col-sm-12 col-xs-12 no_pad divticketdate">',@StartDate,'</div>')

			SET @Html = CONCAT(@Html,'<div class="divTable">')
				SET @Html = CONCAT(@Html,'<div class="divTablehead">')
					SET @Html = CONCAT(@Html,'<div class="divTableCell">Ticket Type</div>')
					SET @Html = CONCAT(@Html,'<div class="divTableCell text-right">Sales End</div>')
					SET @Html = CONCAT(@Html,'<div class="divTableCell text-right">','Price','</div>')
					SET @Html =	CONCAT(@Html,'<div class="divTableCell text-right">Fee</div>')
					SET @Html = CONCAT(@Html,'<div class="divTableCell text-right">Quantity</div>')
				SET @Html = CONCAT(@Html,'</div>')
			SET @Html = CONCAT(@Html,'</div>')
		END

	WHILE (@RowCount>0)
	BEGIN		

		SELECT @TQTY_Id = QTY_Id FROM @tblMain WHERE Id= @RowIndex
		SELECT @AddId = TQD_AddressId, @TicId = TQD_Ticket_Id 
		FROM Ticket_Quantity_Detail WHERE TQD_ID = @TQTY_Id

		IF (@QtyIds='') SET @QtyIds = CONCAT(@QtyIds,@TQTY_Id)  ELSE SET @QtyIds = CONCAT(@QtyIds,',',@TQTY_Id) 
		
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
			SET @Html = CONCAT(@Html,'<div class="col-sm-12 col-xs-12 no_pad divticketdate">',@StartDate,'</div>')
	
			SET @NextAddId = @AddId 
			SELECT @Addess = ConsolidateAddress FROM ADDRESS WHERE AddressID = @AddId
			SET @Html = CONCAT(@Html,'<div class="col-sm-12 col-xs-12 no_pad divticketaddres">',@Addess,'</div>')
			SET @Html = CONCAT(@Html,'<div class="divTable">')
				SET @Html = CONCAT(@Html,'<div class="divTablehead">')
					SET @Html = CONCAT(@Html,'<div class="divTableCell">Ticket Type</div>')
					SET @Html = CONCAT(@Html,'<div class="divTableCell text-right">Sales End</div>')
					SET @Html = CONCAT(@Html,'<div class="divTableCell text-right">','Price','</div>')
					SET @Html =	CONCAT(@Html,'<div class="divTableCell text-right">Fee</div>')
					SET @Html = CONCAT(@Html,'<div class="divTableCell text-right">Quantity</div>')
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
						SET @Html = CONCAT(@Html,'<div class="midtablecell text-right">','<div style="width:60%;float:right;" class="col-sm-10 no_pad"><span style="font-weight: normal; float: left; margin-right: 7px; margin-top:3px">$ </span><input type="text" style="width:80%;" class="form-control evnt_inp_cont table_input" oncopy="return false" onpaste="return false" oncut="return false" onkeydown="return checknumeric(event);" onchange="calculateTickTotal();" placeholder=" Enter Donation" autocomplete="false" name="d" value="" id="txtd_',@TQTY_Id,'"  /></div>','</div>')
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
SET @Html = CONCAT(@Html,'<input id="hdIds" type="hidden" value=', @QtyIds ,' />')

Select @Html as Ticket


END




























GO
/****** Object:  StoredProcedure [dbo].[PublishEvent]    Script Date: 3/15/2016 12:07:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PublishEvent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PublishEvent] AS' 
END
GO






-- Navrit,  27/Oct/2015, 

--[PublishEvent 52,''] -- Single
--[PublishEvent 79,''] -- Daily
--PublishEvent 80,'' -- Weekly
--PublishEvent 81,'' -- MOnthly --Week wise
--PublishEvent 82,'' -- MOnthly --Single Day

ALTER PROC [dbo].[PublishEvent]
(
	@EventId bigint,
	@UserId nvarchar(256)
)
AS 
BEGIN
--Also need to consider Online Event
	Declare @RowCount int
	Declare @RowIndex int
	Declare @RowId int
	DECLARE @ESch BIGINT
	
	DECLARE @StartDate varchar(50)
	DECLARE @EndDate varchar(50)
	DECLARE @StartTime varchar(50)
	DECLARE @EndTime varchar(50)


	DECLARE @Ticketids varchar(MAX)
	DECLARE @Addressids varchar(MAX)




	SET @Ticketids = ''
	SET @Addressids =''
-- Order by was not working, need to work with temp table	
	
	DECLARE @Tbl TABLE (T_Id bigint,odId int)
	INSERT INTO @Tbl
	SELECT T_Id,T_order  FROM Ticket WHERE E_id = @EventId

	SELECT @Ticketids = @Ticketids + ',' + CONVERT(VARCHAR(MAX),T_Id) FROM @Tbl order by odId

	--SELECT @Ticketids = @Ticketids + ',' + CONVERT(VARCHAR(MAX),T_Id) FROM Ticket  WHERE E_id = @EventId 
	IF (@Ticketids != '')
		SET  @Ticketids = RIGHT(@Ticketids,Len(@Ticketids)-1)

	SELECT @Addressids = @Addressids + ',' + CONVERT(VARCHAR(Max),AddressID) from [Address]  where [Address].EventId  = @EventId
	IF (@Addressids !='')
		SET  @Addressids = Right(@Addressids,Len(@Addressids)-1)

	DECLARE @SingleDate int
	SET @SingleDate =0
	SELECT @SingleDate=EventVenueID FROM EventVenue WHERE EventID = @EventId
	DECLARE @tblMerge TABLE (Id int identity,EventId bigint,TicketsIDs varchar(Max),AddressIds varchar(Max),MutipleVId bigint,EventVId bigint, StartDate varchar(50), EndDate varchar(50), StartTime varchar(50), EndTime varchar(50))
	IF (@SingleDate>0)
	BEGIN
		SELECT @StartDate = CONVERT(VARCHAR,DATEADD(dd,0,EventStartDate),107),@EndDate=CONVERT(VARCHAR,DATEADD(dd,0,EventEndDate),107), @StartTime=EventStartTime,@EndTime=EventEndTime  
		FROM EventVenue WHERE EventID = @EventId

		INSERT INTO @tblMerge VALUES
		(@EventId,@Ticketids,@Addressids,0,@SingleDate,@StartDate,@EndDate,@StartTime,@EndTime)			

	END
	ELSE
	BEGIN
		
		DECLARE @Freq VARCHAR(50)
		DECLARE @MultipleId BIGINT

		SELECT @Freq = ISNULL(Frequency,'') FROM MultipleEvent WHERE EventID = @EventId
		-- 174 

		If (@Freq = 'Daily' or @Freq = 'Custom')
		BEGIN
			DECLARE @Days INT
			Set @Days =0
			SELECT @Days = DateDiff(dd,StartingFrom ,StartingTo), @MultipleId = MultipleEventID ,
			@StartDate = StartingFrom,@EndDate=StartingTo, @StartTime=StartTime,@EndTime=EndTime 
			FROM MultipleEvent WHERE EventID = @EventId
			SET @StartDate = CONVERT(VARCHAR,DATEADD(dd,0,@StartDate),107)
			WHILE (@Days>=0)
			BEGIN
				INSERT INTO @tblMerge VALUES (@EventId,@Ticketids,@Addressids,@MultipleId,0,@StartDate,'',@StartTime,@EndTime)			
				SET @StartDate = CONVERT(VARCHAR,DATEADD(dd,1,@StartDate),107)
				SET @Days = @Days-1
			END
		END
		Else If (@Freq = 'Weekly')
		BEGIN

			DECLARE @Weekly VARCHAR(50)
			SELECT @Weekly = WeeklyDay,@MultipleId = MultipleEventID ,
			@StartDate = StartingFrom,@EndDate=StartingTo, @StartTime=StartTime,@EndTime=EndTime 
			FROM MultipleEvent WHERE EventID = @EventId
			DECLARE @DateFrom DATE
			DECLARE @DateTo DATE
			
			SET @DateFrom = CONVERT(DATE,@StartDate)
			SET @DateTo = CONVERT(DATE,@EndDate)

			DECLARE @WeekDay VARCHAR(20)
			DECLARE @tblWeek TABLE(Id INT,DayId INT) 
			INSERT INTO @tblWeek SELECT * FROM [dbo].func_Split(@Weekly,',')  
			
			
			DECLARE @DayID INT
			DECLARE @RDayID INT
			SET @RDayID=0
			SET @DayID =0
			WHILE (@DateFrom<=@DateTo)
			BEGIN
				SET @DayID = dbo.ReturnDayOfWeek(@DateFrom) 
				SELECT @RDayID = Count(*) FROM @tblWeek WHERE DayId= @DayID
				if (@RDayID >0)
				BEGIN
					INSERT INTO @tblMerge VALUES
					(@EventId,@Ticketids,@Addressids,@MultipleId,0,convert(varchar,@DateFrom,107),'',@StartTime,@EndTime)		
				END
				SET @DateFrom =   DATEADD(dd,1,@DateFrom)
			
			END
		END
		Else If (@Freq = 'Monthly')
		BEGIN
			Declare @cnt int
			Declare @Dayofweek varchar(20)
			Declare @Monthlyday varchar(50)
			Declare @Monthlyweek varchar(50)
			Declare @MonthlyweekDays varchar(50)
			Declare @day varchar(50)
			SELECT @Days = DateDiff(dd,StartingFrom ,StartingTo),@MultipleId = MultipleEventID,
			@StartDate = StartingFrom,@StartTime=StartTime,@EndTime = EndTime ,@Monthlyday=MonthlyDay,@Monthlyweek=MonthlyWeek,
			@MonthlyweekDays=MonthlyWeekDays,@EndDate=StartingTo
			FROM MultipleEvent WHERE EventID = @EventId
   			Set @DateFrom = CONVERT(DATE,@StartDate)
            Set @DateTo = CONVERT(DATE,@EndDate)

			   
			IF(ISNULL(@Monthlyday,'')!='')
			BEGIN 
				WHILE (@StartDate<=@DateTo)
				BEGIN 
					SET @day=DATEPART(dd,@StartDate) 
					if(@day=@Monthlyday)
					BEGIN 
					  SELECT  @Dayofweek=datename(dw,@StartDate)
					  INSERT INTO @tblMerge VALUES
						(@EventId,@Ticketids,@Addressids,@MultipleId,0,CONVERT(VARCHAR,@StartDate,107),'',@StartTime,@EndTime)
					END
					SET @StartDate = DATEADD(dd,1,@StartDate) 
				END
			END
			ELSE IF(ISNULL(@Monthlyweek,'')!='')
			BEGIN
				DECLARE @monthday as int
				SELECT @monthday = CASE @Monthlyweek WHEN 'First' THEN 1 WHEN 'Second' THEN 2 WHEN 'Third' THEN 3 WHEN 'Four' THEN 4 END
				DECLARE @Noofweek VARCHAR(50)
				SET @cnt=1
				SELECT @Noofweek= DATEDIFF(week, @DateFrom, @DateTo)

				WHILE @cnt <= @Noofweek
				BEGIN
					if(@cnt=@monthday)
					BEGIN
						DECLARE @newstartdate AS VARCHAR(50)
						DECLARE @newenddate AS VARCHAR(50)
						SET @newstartdate=@StartDate
						SET @newenddate=DATEADD(dd,7,@newstartdate) 
						INSERT INTO @tblWeek SELECT * FROM [dbo].func_Split(@MonthlyweekDays,',') 
						WHILE (@newstartdate<=@newenddate)
						BEGIN
							SET @WeekDay = dbo.ReturnDayOfWeek(@newstartdate) 
							Select @DayID = count(*) from @tblWeek where DayId= @WeekDay
							IF (@DayID >0)
							BEGIN
								INSERT INTO @tblMerge VALUES
								(@EventId,@Ticketids,@Addressids,@MultipleId,0,CONVERT(VARCHAR,@newstartdate,107),'',@StartTime,@EndTime)
							END
							SET @newstartdate = DATEADD(dd,1,@newstartdate) 
						END
					END
					SET @StartDate = DATEADD(dd,7,@StartDate) 
					SET @cnt=@cnt+1
				END
			END
   	
		END
	END

--	select * From @tblMerge

	INSERT INTO [dbo].[Publish_Event_Detail]
           ([PE_Event_Id]
           ,[PE_Address_Ids]
		   ,[PE_Tickets_Ids]
           ,[PE_MultipleVenue_id]
           ,[PE_SingleVenue_Id]
           ,[PE_Scheduled_Date]
           ,[PE_Start_Time]
           ,[PE_End_Time])
	SELECT EventId,AddressIds,TicketsIDs,MutipleVId,EventVId,StartDate,StartTime,EndTime FROM @tblMerge

	--select * From [Publish_Event_Detail]
	DECLARE @tblFinal TABLE (Id INT IDENTITY,Publish_Id BIGINT, EventId BIGINT,TicketsIDs VARCHAR(MAX),AddressIds VARCHAR(MAX),StartDate VARCHAR(50), StartTime VARCHAR(50), EndTime VARCHAR(50))

	INSERT INTO @tblFinal
	SELECT PE_Id,PE_Event_Id,PE_Tickets_Ids,PE_Address_Ids,PE_Scheduled_Date,PE_Start_Time,PE_End_Time FROM Publish_Event_Detail WHERE PE_Event_Id = @EventId
	SET @RowCount = @@ROWCOUNT
	SET @RowIndex =1
	--select * From @tblFinal

	Declare @PublishId bigint
	Declare @Addresses varchar(500)
	Declare @Ticketss varchar(500)
	Declare @AddRowCot Int
	Declare @AddRowInd Int
	
	Declare @TicRowCot Int
	Declare @TicRowInd Int

	Declare @Addid int
	Declare @Tickid int
	Declare @TQuantity int
	Declare @tblMVenue table (Id int,Element int)
	Declare @tblMTicket table (Id int,Element int)
	Set @PublishId=0
	Set @StartDate=''
	Set @StartTime=''
	WHILE (@RowCount >0)
	BEGIN
		SELECT @StartDate=StartDate,@PublishId=Publish_Id,@Addresses=isnull(AddressIds,''),@Ticketss=TicketsIDs,@StartTime=StartTime FROM @tblFinal WHERE Id=@RowIndex
		INSERT INTO @tblMVenue SELECT * FROM [dbo].func_Split(@Addresses,',') 
		SET @AddRowCot = @@ROWCOUNT
		SET @AddRowInd =1
		If (@Addresses != '')
		BEGIN
			WHILE (@AddRowCot>0)
			BEGIN
				SELECT @Addid = Element FROM @tblMVenue WHERE Id = @AddRowInd
				INSERT INTO @tblMTicket SELECT * FROM [dbo].func_Split(@Ticketss,',') 
				SET @TicRowCot = @@ROWCOUNT
				SET @TicRowInd =1
				WHILE (@TicRowCot>0)
				BEGIN
					SELECT @Tickid = Element FROM @tblMTicket WHERE Id = @TicRowInd
					SELECT @TQuantity = Qty_Available FROM Ticket WHERE T_Id = @Tickid

					INSERT Ticket_Quantity_Detail ([TQD_PE_Id],[TQD_Event_Id],[TQD_Ticket_Id],[TQD_AddressId],[TQD_Quantity],[TQD_Remaining_Quantity],TQD_StartDate,TQD_StartTime)
					VALUES(@PublishId,@EventId,@Tickid,@Addid,@TQuantity,@TQuantity,@StartDate,@StartTime)
					SET @TicRowInd = @TicRowInd +1
					SET @TicRowCot = @TicRowCot -1
				END
				SET @AddRowInd = @AddRowInd +1
				SET @AddRowCot = @AddRowCot -1
			END
		END
		ELSE 
		BEGIN
				INSERT INTO @tblMTicket SELECT * FROM [dbo].func_Split(@Ticketss,',') 
				SET @TicRowCot = @@ROWCOUNT
				SET @TicRowInd =1
				WHILE (@TicRowCot>0)
				BEGIN
					SELECT @Tickid = Element FROM @tblMTicket WHERE Id = @TicRowInd
					SELECT @TQuantity = Qty_Available FROM Ticket WHERE T_Id = @Tickid

					INSERT Ticket_Quantity_Detail ([TQD_PE_Id],[TQD_Event_Id],[TQD_Ticket_Id],[TQD_AddressId],[TQD_Quantity],[TQD_Remaining_Quantity],TQD_StartDate,TQD_StartTime)
					VALUES(@PublishId,@EventId,@Tickid,0,@TQuantity,@TQuantity,@StartDate,@StartTime)
					SET @TicRowInd = @TicRowInd +1
					SET @TicRowCot = @TicRowCot -1
				END

		END
		SET @RowIndex = @RowIndex+1
		SET @RowCount = @RowCount-1
	END

	SELECT * FROM Ticket_Quantity_Detail WHERE TQD_Event_Id = @EventId

	--truncate table Ticket_Quantity_Detail truncate table Publish_Event_Detail
	--
END












GO
