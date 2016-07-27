/*
--select * from CreditCardInfo --Yes
select * from CreditCardInfoTemp -- No
select * from EventAdditional; -- No
select * from EventAdditionalCharges -- Maybe
select * from EventAdditionalData -- No
	select * from EventCategory --Yes
select * from EventClosingTime -- Maybe
select * from EventCorpCampaign --No
select * from EventMembers -- Maybe Invitations
select * from eventNotify -- No
select * from EventOptions -- No
	select * from Events -- Yes
	select * from EventSubCategory -- Yes
	select * from EventSubType -- No, only types
	select * from EventTicketPrice -- Yes
	select * from EventTimes -- Yes
	select * from EventTypes -- Yes
	select * from EventVariableChargeCat --Yes but
	select * from EventVariableChargeOptions -- Yes, but
select * from [Order]
select * from order_sitting_details -- No
	select * from RawUserDB -- Yes
select * from temp_sitting_cart -- no
select * from TicketOrderInfo -- Yes
select * from TicketShippingAddresses -- Yes
select * from TicketTypes -- Discuss
select * from UserIPs -- yes
	select * from UserDB -- Yes
select * from v_ticketFees --yes
select * from v_ticketInformation --maybe
select * from v_ticketNumberPurchased -- yes

------------------------
--EventType mapping not found

*/

alter table EC2.dbo.AspNetUsers add OldUserID int null;
GO

/* Migrating users */
-- Users and Password
insert into EC2.dbo.AspNetUsers 
(id, OldUserID, Email, EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,LoginStatus)
select lower(newid()), UserID, UserEmail, 0, '', '', null, 0, 0, null,1,0,UserName,'Y'
from amsuj786n3x.dbo.UserDB
GO

SET IDENTITY_INSERT EC2.dbo.Country ON
insert into EC2.dbo.Country(CountryID, Country)
select CountryID, CountryName from amsuj786n3x.dbo.Country;
SET IDENTITY_INSERT EC2.dbo.Country OFF
GO
--select * from  EventCombo.dbo.Profile;
--select * from amsuj786n3x.dbo.UserDB

--Migrating profile

insert into EC2.dbo.Profile 
(UserID,FirstName,LastName,Email,StreetAddressLine1,StreetAddressLine2,City,State,Zip,CountryID,MainPhone,Gender,DateofBirth,AddedOn,UpdateOn)
-- SecondPhone,WebsiteURL,TwitterURL,FacebookURL,GooglePlusURL,LinkedInURL,UserProfileImage,OrganizerName,SecondaryEmail,OrganizerDescription,WorkPhone,ContentType,Organiser,Merchant,UserStatus,SendCur_EventDetail,Ipcountry,IpState,Ipcity)
select ac.Id,UserFirstName,UserLastName,UserEmail,UserAddress,UserAddress2,UserCity,StateName,UserZip,u.CountryID,UserPhone,
case 
	when UserGender='F' then 'Female' 
	when UserGender='M' then 'Male'
	else null 
end as UserGender ,CONVERT(VARCHAR(24),UserBirthDate,105),MemberDate,TimeUpdated
-- UserName,,UserInit,,OtherLoc,GetsNewsletter,UserAutoLogin,RoleID,UserConfirmed,UserActive,UserLastLogin,UserReferrer,AdminPage,CodeID,Premium,Subscriber,ConfirmationCode,AdminShopPage,AdminServicePage,DateTime
from amsuj786n3x.dbo.UserDB
u inner join amsuj786n3x.dbo.State s on u.StateID=s.stateID
inner join EC2.dbo.AspNetUsers ac on u.userID=ac.OldUserID
GO

--select * from EC2.dbo.Profile 

--select * from amsuj786n3x.dbo.Country;



SET IDENTITY_INSERT EC2.dbo.EventCategory ON;
insert into EC2.dbo.EventCategory (EventCategoryID, EventCategory)
select EventCategoryID, Description from amsuj786n3x.dbo.EventCategory;
SET IDENTITY_INSERT EC2.dbo.EventCategory OFF;
GO

--select * from amsuj786n3x.dbo.EventSubCategory -- Yes
--select * from EC2.dbo.EventSubCategory -- Yes

SET IDENTITY_INSERT EC2.dbo.EventSubCategory ON;
INSERT INTO EC2.dbo.EventSubCategory(EventSubCategoryID, EventCategoryID, EventSubCategory)
select SubCatID,CatID,SubCatName from amsuj786n3x.dbo.EventSubCategory
SET IDENTITY_INSERT EC2.dbo.EventSubCategory OFF;
GO
-- select * from amsuj786n3x.dbo.EventTypes -- Yes
--select * from EC2.dbo.EventType -- Yes
/*
delete from EC2.dbo.EventType;
SET IDENTITY_INSERT EC2.dbo.EventType ON;
insert into EC2.dbo.EventType (EventTypeID, EventType, EventHide)
select TypeID, Name, 'N' from amsuj786n3x.dbo.EventTypes;
SET IDENTITY_INSERT EC2.dbo.EventType OFF;
*/

SET IDENTITY_INSERT EC2.dbo.EventType ON;
insert into EC2.dbo.EventType (EventTypeID, EventType, EventHide)
select EventSubTypeID, Description, 'N' from amsuj786n3x.dbo.EventSubType;
SET IDENTITY_INSERT EC2.dbo.EventType OFF;
GO


-- select * from amsuj786n3x.dbo.Events -- Yes
-- select * from EC2.dbo.Event

-- select * from EC2.dbo.EventSubCategory where EventCategoryID=13 -- Yes
-- select * from EC2.dbo.EventCategory -- Yes
insert into EC2.dbo.EventSubCategory (EventCategoryID, EventSubCategory)
values (13, 'Other');
GO

SET IDENTITY_INSERT EC2.dbo.Event ON;
INSERT INTO EC2.dbo.Event (EventID, EventTypeID, EventCategoryID, EventSubCategoryID, UserID, EventTitle, 
	DisplayStartTime, DisplayEndTime, DisplayTimeZone, EventDescription, EventPrivacy, EventStatus, TimeZone, 
	AddressStatus, CreateDate, ModifyDate, Parent_EventID)
select EventID, EventSubTypeID, isnull(EventCategoryID,13), isnull(EventSubCatID,165),u.Id,EventTitle,
   'Y', 'Y', 'Y', EventAdmissionDetails, case when IsPublic=1 then 'Public' else 'Private' end,  case when eventstatus='A' then 'Live' else 'Save' end, 85,
   'Single', EventSubmitDate, EventApprovedDate, 0
     from amsuj786n3x.dbo.Events e inner join EC2.dbo.AspNetUsers u
	 on e.UserID=u.OldUserID;
SET IDENTITY_INSERT EC2.dbo.Event OFF;
GO

--select distinct userid from EC2.dbo.Event

--select * from amsuj786n3x.dbo.Events

INSERT INTO EC2.dbo.Address (Name, Address1, Address2, City, State, Zip, CountryID, 
	VenueName, UserID, EventID, ConsolidateAddress, Latitude, Longitude)
select '', EventTempAddress1, EventTempAddress2, EventTempCity, isnull(s.StateName,''), EventTempZip, isnull(e.CountryID,1),
	VenueName, u.id, EventID, EventTempAddress1 + ',' + EventTempAddress2 + ',' + EventTempCity + ',' + EventTempZip + ',' + isnull(s.StateName,'') + ',' + isnull(c.CountryName,''), null, null
   from amsuj786n3x.dbo.Events e inner join EC2.dbo.AspNetUsers u
	 on e.UserID=u.OldUserID left outer join amsuj786n3x.dbo.StateTab s
	 on e.StateID=s.StateID left outer join amsuj786n3x.dbo.Country c
	 on e.CountryID=c.CountryID
GO

--select * from EC2.dbo.Address


--select * from amsuj786n3x.dbo.EventTimes
--select * from EC2.dbo.EventVenue
INSERT INTO EC2.dbo.EventVenue (EventID, AddressID, EventStartDate, EventEndDate, EventStartTime, EventEndTime, E_Startdate, E_Enddate)
select S.EventID, 0, CONVERT(varchar,S.EventDateTime,101), CONVERT(varchar,isnull(E.EventDateTime,DATEADD(day, 1, S.EventDateTime)),101), 
	lower(LTRIM(RIGHT(CONVERT(VARCHAR(20), S.EventDateTime, 100), 7))) , lower(LTRIM(RIGHT(CONVERT(VARCHAR(20), isnull(E.EventDateTime,DATEADD(day, 1, S.EventDateTime)), 100), 7))), S.EventDateTime, E.EventDateTime from
(select * from amsuj786n3x.dbo.EventTimes  
where EventTimeType='S' ) S left outer join (Select * from amsuj786n3x.dbo.EventTimes where EventTimeType='E' )  E
 on S.EventID=E.EventID
where S.EventID  in 
(select EventID from EC2.dbo.Event)

GO

--select * from EC2.dbo.Ticket where eventTicketPriceID is null
--select * from EventCombo.dbo.Ticket
select * from amsuj786n3x.dbo.EventTicketPrice; -- Yes


INSERT INTO EC2.dbo.Ticket (E_Id, T_name, Qty_Available, Price, TicketTypeID, T_Sold, Registration_Recorded,
T_Desc,Show_T_DEsc, Fees_Type, Sale_Start_Date, Sale_Start_Time, Sale_End_Date, Sale_End_Time, Hide_Ticket,
Auto_Hide_Sche, Hide_Untill_Date, Hide_Untill_Time, Hide_After_Date, Hide_After_Time, Min_T_Qty, Max_T_Qty,
T_Disable, T_Mark_SoldOut, T_Sold_Qty, T_order, EC_Fee, Customer_Fee, T_Displayremaining, T_AutoSechduleType,
Additional_Fee, T_Discount, TotalPrice, T_Customize, T_Ecpercent, T_EcAmount)
Select e.eventID, left(t.ticketType,255), numTickets, convert(decimal, ticketPrice), 2, 0, null, t.ticketType,0, 1,
Null,Null,Null,Null,0,0,null,null,null,null,Min_Per_Order,Max_Per_Order,0,0,0,0,convert(decimal, e.Desi_fees),convert(decimal, Customer_fee),0,0,
null, convert(decimal, Discount_Amount), convert(decimal, ticketPrice+Customer_Fee),0,0,0  from amsuj786n3x.dbo.EventTicketPrice e 
inner join amsuj786n3x.dbo.TicketTypes t on e.ticketTypeID=t.ticketTypeID
inner join EC2.dbo.Event v on e.eventId=v.eventID
where e.ticketPrice<9999999
and e.customer_fee<9999999
and e.Discount_Amount<9999999
GO


-------------------------------
select * from EC2.dbo.Ticket t inner join amsuj786n3x.dbo.EventTicketPrice o 
on t.E_Id=o.eventID and t.TicketTypeID=o.ticketTypeID

select * from amsuj786n3x.dbo.EventTicketPrice o inner join amsuj786n3x.dbo.TicketTypes y on o.ticketTypeID=y.ticketTypeID
inner join EC2.dbo.Ticket t on o.eventID=t.E_Id and t.T_name=left(y.ticketType,255)

Alter table EC2.dbo.Ticket add eventTicketPriceID int null;

update t set eventTicketPriceID=o.eventTicketPriceID
 from amsuj786n3x.dbo.EventTicketPrice o inner join amsuj786n3x.dbo.TicketTypes y on o.ticketTypeID=y.ticketTypeID
inner join EC2.dbo.Ticket t on o.eventID=t.E_Id and t.T_name=left(y.ticketType,255)
-------------------------------


--Fees_Type ?

--select * from amsuj786n3x.dbo.EventTicketPrice where Customer_Fee>99999999




--select * from EventCombo.dbo.Event_VariableDesc


INSERT INTO EC2.dbo.Event_VariableDesc (Event_ID, VariableDesc, Price)
select c.eventID, EVCC_Name, EVCO_Amount from amsuj786n3x.dbo.EventVariableChargeCat c inner join  amsuj786n3x.dbo.EventVariableChargeOptions o on c.EVCC_Id=o.EVCC_Id
inner join EC2.dbo.Event e on c.eventId=e.eventId
GO





--select * from EC2.dbo.Organizer_Master

alter table EC2.dbo.Organizer_Master add eventId bigint null;
GO
INSERT INTO EC2.dbo.Organizer_Master (Orgnizer_Name,  Organizer_Desc, UserID, Organizer_Email, Organizer_Phoneno, eventID)
select left(EventContactNAme,300), null, u.id, left(e.EventContactEmail,100), left(EventContactPhone,20), e.eventID  from amsuj786n3x.dbo.Events e inner join  EC2.dbo.Event c1 on e.EventID=c1.EventID
inner join EC2.dbo.AspNetUSers u on e.userid=u.olduserid
 -- Yes
GO
--Select * from EC2.dbo.Organizer_Master;
INSERT INTO EC2.dbo.Event_Orgnizer_Detail (Orgnizer_Event_Id, userID, DefaultOrg, OrganizerMaster_Id)
Select EventId, UserID, 'Y', Orgnizer_Id from EC2.dbo.Organizer_Master;
GO
--select * from EC2.dbo.Event_Orgnizer_Detail;

alter table EC2.dbo.Organizer_Master drop column eventId;
GO
/*
select O_Id, O_Order_Id, O_User_ID, O_TotalAmount, O_OrderAmount, O_VariableID, O_VariableAmount,
O_PromoCodeId, O_OrderDateTime, O_First_Name, O_Last_Name, O_Email, O_Card_TransHash, O_Card_TransID from EventCombo.dbo.Order_Detail_T

select * from amsuj786n3x.dbo.[Order]
*/

-- Orders start



SET IDENTITY_INSERT EC2.dbo.Order_Detail_T ON;
;with cte  (order_id, a, id, order_amount, sub_order_amount, b,c,d,orderDate,firstname,lastname,Email,authorizationCode,transaction_id,rw ) 
as 
(select o.order_id, '' as a,  u.Id,order_amount, sub_order_amount,null as b,null as c,null as d,orderDate,p.firstname, p.lastname, u.Email, cc.authorizationCode, cc.transaction_id
,ROW_NUMBER() OVER
     (
         PARTITION BY o.order_id
         ORDER BY cc.transaction_id DESC
     ) AS rw
   from  amsuj786n3x.dbo.[Order] o
inner join Ec2.dbo.aspnetusers u on o.member_id=u.olduserid inner join ec2.dbo.[profile] p on u.Id=p.userid
inner join amsuj786n3x.dbo.CreditCardInfo cc on o.order_id=cc.order_id where cc.responsecode=1
--and o.order_id=525
)
INSERT INTO EC2.dbo.Order_Detail_T (O_Id, O_Order_Id, O_User_ID, O_TotalAmount, O_OrderAmount, O_VariableID, O_VariableAmount,
O_PromoCodeId, O_OrderDateTime, O_First_Name, O_Last_Name, O_Email, O_Card_TransHash, O_Card_TransID)
select order_id, a, id, order_amount, sub_order_amount, b,c,d,orderDate,firstname,lastname,Email,authorizationCode,transaction_id from cte where rw=1;
GO
SET IDENTITY_INSERT EC2.dbo.Order_Detail_T OFF;
GO

UPDATE EC2.dbo.Order_Detail_T set O_Order_Id='T' + RIGHT('00000000' + convert(varchar,O_Id), 9)
WHERE O_Order_Id='';
GO

SELECT distinct TQD_ID, t.eventTicketPriceID into #tmp2 from EC2.dbo.Ticket_Quantity_Detail tqd inner join EC2.dbo.Ticket t on tqd.TQD_Ticket_ID=t.t_ID
 inner join amsuj786n3x.dbo.TicketOrderInfo a on t.eventTicketPriceID=a.eventTicketPriceID


INSERT INTO EC2.dbo.Ticket_Purchased_Detail (TPD_User_Id, TPD_Order_Id, TPD_Purchased_Qty, TPD_TQD_Id, TPD_Amount, TPD_Donate,
TPD_GUID, TPD_EC_Fee, TPD_PromoCodeID, TPD_PromoCodeAmount)
SELECT u.Id, order_id, 1, tqd.TQD_ID, priceWithChargesAndDiscounts, 0, '',0,0,0 from amsuj786n3x.dbo.TicketOrderInfo t inner join #tmp2 tqd on t.eventTicketPriceID=tqd.eventTicketPriceID 
inner join EC2.dbo.AspNetUsers u on u.OldUserID=t.user_id

--SELECT * from amsuj786n3x.dbo.TicketOrderInfo t inner join #tmp2 tqd on t.eventTicketPriceID=tqd.eventTicketPriceID 


SELECT * from EC2.dbo.AspNetUsers

/*
SELECT * from EC2.dbo.Ticket;

SELECT * from EC2.dbo.Ticket_Purchased_Detail;

SELECT * from EC2.dbo.Ticket_Quantity_Detail;

SELECT * from amsuj786n3x.dbo.TicketOrderInfo  where order_id=413 ;

SELECT * from amsuj786n3x.dbo.OrderInfo where order_id=413 ;
SELECT * from amsuj786n3x.dbo.[Order] where order_id=413 ;

SELECT * from EC2.dbo.Order_Detail_T

SELECT * from amsuj786n3x.dbo.OrderInfo where category_id=0 ;

SELECT category_id, count(*) cn from amsuj786n3x.dbo.OrderInfo 
group by category_id
order by cn desc


where category_id=0 ;

select * from amsuj786n3x.dbo.Products where product_id=12630 ;

SELECT product_id, count(*) cn from amsuj786n3x.dbo.OrderInfo 
group by product_id
order by cn desc

where category_id=0 ;

*/










drop table #tmp;
GO
select E_Id, a.AddressID,v.EventVenueID,0 as PE_SingleVenue_Id, t.T_ID, convert(varchar, convert(datetime,v.EventStartDate), 107) as PE_Scheduled_Date, v.EventStartTime, v.EventEndTime  
into #tmp
from EC2.dbo.Ticket t inner join EC2.dbo.Address a 
on a.EventId=t.E_Id inner join EC2.dbo.EventVenue v on a.eventId=v.EventID
group by E_Id, a.AddressID,v.EventVenueID, t.T_ID, convert(varchar, convert(datetime,v.EventStartDate), 107), v.EventStartTime, v.EventEndTime
GO

INSERT INTO EC2.dbo.Publish_Event_Detail (PE_Tickets_Ids, PE_Event_Id, PE_Address_Ids, PE_MultipleVenue_id, PE_SingleVenue_Id,  PE_Scheduled_Date,
PE_Start_Time, PE_End_Time)
select T_ID=left(STUFF((SELECT ',' + convert(varchar,T_ID)
           FROM #tmp b 
           WHERE b.E_Id = a.E_Id 
          FOR XML PATH('')), 1, 2, ''),100),
 E_Id, AddressID, EventVenueID, PE_SingleVenue_Id, PE_Scheduled_Date, EventStartTime, EventEndTime from #tmp a
group by E_Id, AddressID, EventVenueID, PE_SingleVenue_Id, PE_Scheduled_Date, EventStartTime, EventEndTime
GO

--select * from EC2.dbo.Ticket where E_Id=5833

--select * from EC2.[dbo].[Publish_Event_Detail] --where pe_event_id=7578

--select * from [dbo].[Ticket_Quantity_Detail] where TQD_Event_Id=5833

INSERT INTO EC2.dbo.Ticket_Quantity_Detail (TQD_PE_Id, TQD_Event_Id, TQD_Ticket_Id, TQD_AddressId, TQD_Quantity, TQD_Remaining_Quantity,
TQD_StartDate, TQD_StartTime)
select ped.PE_Id, ped.PE_Event_Id, t.T_Id, ped.PE_Address_Ids, t.Qty_Available, 0, ped.PE_Scheduled_Date, ped.PE_Start_Time  
from EC2.dbo.Publish_Event_Detail ped 
inner join EC2.dbo.Ticket t on t.E_Id=ped.PE_Event_Id


/*
select *
from EC2.dbo.Publish_Event_Detail ped 
inner join EC2.dbo.Ticket t on t.E_Id=ped.PE_Event_Id

select distinct E_ID from EC2.dbo.Ticket 



*/



GO

/*
SELECT * from EC2.dbo.Ticket;

SELECT * from EC2.dbo.Ticket_Purchased_Detail;


SELECT * from amsuj786n3x.dbo.TicketOrderInfo 

SELECT * from EC2.dbo.Order_Detail_T

*/


