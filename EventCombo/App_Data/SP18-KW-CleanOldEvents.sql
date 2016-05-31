 Begin try
	BEGIN TRAN	
	 Declare @EventCTE Table(eventid bigint)
	 Insert into @EventCTE SELECT eventId FROM EventVenue WHERE CONVERT(DATE,E_Startdate) <= '04/01/2014'
	 DELETE From Ticket_Quantity_Detail where TQD_Event_Id in (select eventid From @EventCTE)  

	 DELETE From Ticket where E_Id in (select eventid From @EventCTE) 
  
	 DELETE From Address where EventId in (select eventid From @EventCTE)

	 Delete from Event_Orgnizer_Detail  where Orgnizer_Event_Id  in (select eventid From @EventCTE)
 
	 Delete from Event_VariableDesc  where Event_Id  in (select eventid From @EventCTE)

	 DELETE From EventVenue where EventId in (select eventid From @EventCTE)
  
	 DELETE From Event where EventID in (select eventid From @EventCTE)
	COMMIT TRAN
END try
  Begin catch
	RollBack Tran
 END catch