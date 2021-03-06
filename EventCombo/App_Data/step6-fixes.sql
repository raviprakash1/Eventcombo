DELETE
FROM Order_Detail_T
WHERE O_Id in 
(
select o.order_id
from  amsuj786n3x.dbo.[Order] o
inner join aspnetusers u on o.member_id=u.olduserid 
inner join [profile] p on u.Id=p.userid
inner join amsuj786n3x.dbo.CreditCardInfo cc on o.order_id=cc.order_id where cc.responsecode=1
)
GO

SET IDENTITY_INSERT Order_Detail_T ON;
with cte  (order_id, a, id, order_amount, sub_order_amount, b,c,d,orderDate,firstname,lastname,Email,authorizationCode,transaction_id,rw ) 
as 
(select o.order_id, '' as a,  u.Id,order_amount, sub_order_amount,null as b,null as c,null as d,orderDate,p.firstname, p.lastname, u.Email, cc.authorizationCode, cc.transaction_id
,ROW_NUMBER() OVER
     (
         PARTITION BY o.order_id
         ORDER BY cc.transaction_id DESC
     ) AS rw
   from  amsuj786n3x.dbo.[Order] o
inner join aspnetusers u on o.member_id=u.olduserid 
inner join [profile] p on u.Id=p.userid
inner join amsuj786n3x.dbo.CreditCardInfo cc on o.order_id=cc.order_id where cc.responsecode=1
)
INSERT INTO Order_Detail_T (O_Id, O_Order_Id, O_User_ID, O_TotalAmount, O_OrderAmount, O_VariableID, O_VariableAmount,
O_PromoCodeId, O_OrderDateTime, O_First_Name, O_Last_Name, O_Email, O_Card_TransHash, O_Card_TransID)
select order_id, a, id, order_amount, sub_order_amount, b,c,d,orderDate,firstname,lastname,Email,authorizationCode,transaction_id from cte where rw=1;
SET IDENTITY_INSERT Order_Detail_T OFF;
GO

UPDATE Order_Detail_T set O_Order_Id='T' + RIGHT('00000000' + convert(varchar,O_Id), 9)
WHERE O_Order_Id='';
GO

delete from User_Permission_Detail  where UP_Permission_Id in (1,2)

insert into User_Permission_Detail 
(Up_User_Id,Up_Permission_Id) 
select Id,1  From AspNetUsers

insert into User_Permission_Detail 
(Up_User_Id,Up_Permission_Id) 
select Id,2  From AspNetUsers


Update Profile set Organiser ='Y'
GO

Alter table Ticket add eventTicketPriceID int null;
GO

DELETE FROM Publish_Event_Detail
WHERE PE_ID IN
(
  SELECT ped.PE_Id
  FROM Publish_Event_Detail ped
  LEFT JOIN MultipleEvent me ON me.MultipleEventID = ped.PE_MultipleVenue_id
  LEFT JOIN EventVenue ev1 ON ev1.EventVenueID = ped.PE_SingleVenue_Id
  LEFT JOIN EventVenue ev2 ON ev2.EventVenueID = ped.PE_MultipleVenue_id
  WHERE me.MultipleEventID is null AND ISNULL(ev1.EventVenueID, 0) = 0 AND ISNULL(ev2.EventVenueID , 0) = 0
)

UPDATE ped SET ped.PE_SingleVenue_Id = ped.PE_MultipleVenue_id, ped.PE_MultipleVenue_id = null
FROM Publish_Event_Detail ped
LEFT JOIN MultipleEvent me ON me.MultipleEventID = ped.PE_MultipleVenue_id
LEFT JOIN EventVenue ev1 ON ev1.EventVenueID = ped.PE_SingleVenue_Id
LEFT JOIN EventVenue ev2 ON ev2.EventVenueID = ped.PE_MultipleVenue_id
WHERE me.MultipleEventID is null AND ISNULL(ev1.EventVenueID, 0) = 0 AND ISNULL(ev2.EventVenueID , 0) <> 0

UPDATE Ticket SET eventTicketPriceID = at.eventTicketPriceID
FROM
(
	SELECT ROW_NUMBER() OVER (PARTITION BY t.T_name, t.E_Id, t.Price ORDER BY t.T_Id) ord, t.*
	FROM Ticket t
) et
INNER JOIN
(
	SELECT LEFT(tt.ticketType, 255) ticketType, etp.eventID, etp.eventTicketPriceID, 
		etp.numTickets, convert(decimal, etp.ticketPrice) ticketPrice,
		ROW_NUMBER() OVER(PARTITION BY LEFT(tt.ticketType, 255), etp.eventID, convert(decimal, etp.ticketPrice) ORDER BY etp.eventTicketPriceID)  ord
	FROM amsuj786n3x.dbo.EventTicketPrice etp 
	LEFT JOIN amsuj786n3x.dbo.TicketTypes tt ON etp.ticketTypeID = tt.ticketTypeID
) at ON et.T_name=at.ticketType AND at.eventID=et.E_Id
	AND et.Price = at.ticketPrice AND et.ord = at.ord
WHERE et.T_Id = Ticket.T_Id

GO


select orderid, newid() oguid into #guidTemp from 
(select distinct 
 'T' + RIGHT('00000000' + convert(varchar,oi.order_id), 9) orderid
 from amsuj786n3x.dbo.TicketOrderInfo toi inner join Ticket et 
on toi.eventTicketPriceID=et.eventTicketPriceID inner join  amsuj786n3x.dbo.OrderInfo oi
on toi.eventTicketPriceID=oi.product_id and toi.order_id=oi.order_id inner join amsuj786n3x.dbo.[Order] o
on toi.order_id=o.order_id inner join Ticket_Quantity_Detail tqd
on tqd.TQD_Ticket_ID=et.t_ID inner join AspNetUsers u on u.OldUserID=toi.user_id
where
o.dateAdded>'2014-04-01' and complete=1) x;
GO



INSERT INTO Ticket_Purchased_Detail (TPD_User_Id, TPD_Order_Id, TPD_Purchased_Qty, TPD_Event_ID, TPD_TQD_Id, TPD_Amount, TPD_Donate,
TPD_GUID, TPD_EC_Fee, TPD_PromoCodeID, TPD_PromoCodeAmount)
select 
u.Id, 'T' + RIGHT('00000000' + convert(varchar,oi.order_id), 9)
, oi.quantity,toi.event_Id, tqd.TQD_ID, priceWithChargesAndDiscounts, 0, g.oguid,0,0,0
 from amsuj786n3x.dbo.TicketOrderInfo toi inner join Ticket et 
on toi.eventTicketPriceID=et.eventTicketPriceID inner join  amsuj786n3x.dbo.OrderInfo oi
on toi.eventTicketPriceID=oi.product_id and toi.order_id=oi.order_id inner join amsuj786n3x.dbo.[Order] o
on toi.order_id=o.order_id inner join Ticket_Quantity_Detail tqd
on tqd.TQD_Ticket_ID=et.t_ID inner join AspNetUsers u 
on u.OldUserID=toi.user_id inner join #guidTemp g on 'T' + RIGHT('00000000' + convert(varchar,oi.order_id), 9)=g.orderid
where o.dateAdded>'2014-04-01' and complete=1;
GO

drop table #guidTemp;
GO