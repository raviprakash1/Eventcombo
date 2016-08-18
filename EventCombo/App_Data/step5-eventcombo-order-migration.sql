/*
Assuming that you've restored your V1 database as amsuj786n3x and
you V2 database is EC2 and both databases are avilable on same server

*/


Alter table EC2.dbo.Ticket add eventTicketPriceID int null;
GO

update t set eventTicketPriceID=o.eventTicketPriceID
 from amsuj786n3x.dbo.EventTicketPrice o inner join amsuj786n3x.dbo.TicketTypes y on o.ticketTypeID=y.ticketTypeID
inner join EC2.dbo.Ticket t on o.eventID=t.E_Id and t.T_name=left(y.ticketType,255);
GO

UPDATE EC2.dbo.Order_Detail_T set O_Order_Id='T' + RIGHT('00000000' + convert(varchar,O_Id), 9)
WHERE O_Order_Id='';
GO


select orderid, newid() oguid into #guidTemp from 
(select distinct 
 'T' + RIGHT('00000000' + convert(varchar,oi.order_id), 9) orderid
 from amsuj786n3x.dbo.TicketOrderInfo toi inner join EC2.dbo.Ticket et 
on toi.eventTicketPriceID=et.eventTicketPriceID inner join  amsuj786n3x.dbo.OrderInfo oi
on toi.eventTicketPriceID=oi.product_id and toi.order_id=oi.order_id inner join amsuj786n3x.dbo.[Order] o
on toi.order_id=o.order_id inner join EC2.dbo.Ticket_Quantity_Detail tqd
on tqd.TQD_Ticket_ID=et.t_ID inner join EC2.dbo.AspNetUsers u on u.OldUserID=toi.user_id
where -- toi.order_id=95434 and 
o.dateAdded>'2014-04-01' and complete=1) x;
GO



INSERT INTO EC2.dbo.Ticket_Purchased_Detail (TPD_User_Id, TPD_Order_Id, TPD_Purchased_Qty, TPD_Event_ID, TPD_TQD_Id, TPD_Amount, TPD_Donate,
TPD_GUID, TPD_EC_Fee, TPD_PromoCodeID, TPD_PromoCodeAmount)
select 
u.Id, 'T' + RIGHT('00000000' + convert(varchar,oi.order_id), 9)
, oi.quantity,toi.event_Id, tqd.TQD_ID, priceWithChargesAndDiscounts, 0, g.oguid,0,0,0
 from amsuj786n3x.dbo.TicketOrderInfo toi inner join EC2.dbo.Ticket et 
on toi.eventTicketPriceID=et.eventTicketPriceID inner join  amsuj786n3x.dbo.OrderInfo oi
on toi.eventTicketPriceID=oi.product_id and toi.order_id=oi.order_id inner join amsuj786n3x.dbo.[Order] o
on toi.order_id=o.order_id inner join EC2.dbo.Ticket_Quantity_Detail tqd
on tqd.TQD_Ticket_ID=et.t_ID inner join EC2.dbo.AspNetUsers u 
on u.OldUserID=toi.user_id inner join #guidTemp g on 'T' + RIGHT('00000000' + convert(varchar,oi.order_id), 9)=g.orderid
where o.dateAdded>'2014-04-01' and complete=1;
GO

drop table #guidTemp;
GO
