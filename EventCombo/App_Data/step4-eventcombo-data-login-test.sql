
update EC2.dbo.Aspnetusers
set Email='kannan.k@kiwitech.com',
UserName='kannan.k@kiwitech.com' --
where Email='gillharmeet@yahoo.com'

/*
--select *  from event
update EC2.dbo.event set Parent_EventID=0;
--select * from Address;
update EC2.dbo.Address set Latitude=28.57, Longitude=77.32
where AddressID<50;

select * from Event where EventID=7578
select * from Address where EventId=7578
update EC2.dbo.Address set Latitude=28.57, Longitude=77.32
where EventId=7578

--drop table amsuj786n3x.dbo.zipcodetmp
--drop table amsuj786n3x.dbo.statecitytmp
--select * from amsuj786n3x.dbo.zipcodetmp c inner join EC2.dbo.Address a
--on c.zip=a.Zip
*/

update a 
set Latitude=c.latitude,
Longitude=c.Longitude
 from amsuj786n3x.dbo.zipcodetmp c inner join EC2.dbo.Address a
on c.zip=a.Zip


update [EC2].[dbo].[EventVenue] set E_Enddate=DATEADD (HOUR ,4, E_Startdate ) where E_Enddate is null
update ev set ev.EventStatus = 'live' FROM Event ev LEFT Join MultipleEvent me on ev.EventID = me.EventID
update ev set ev.EventStatus ='live' FROM Event ev LEFT Join EventVenue me on ev.EventID = me.EventID


update [EC2].[dbo].AspNetUserRoles set RoleId=1
where userid in (select id from [EC2].[dbo].AspNetUsers where Email='saroosh@eventcombo.com');

update [EC2].[dbo].AspNetUserRoles set RoleId=1
where userid in (select id from [EC2].[dbo].AspNetUsers where Email='kannan.k@kiwitech.com');

update [EC2].[dbo].Profile set UserStatus='Y'
where userid in (select id from [EC2].[dbo].AspNetUsers where Email='kannan.k@kiwitech.com');

update [EC2].[dbo].Profile set UserStatus='Y'
where userid in (select id from [EC2].[dbo].AspNetUsers where Email='saroosh@eventcombo.com');