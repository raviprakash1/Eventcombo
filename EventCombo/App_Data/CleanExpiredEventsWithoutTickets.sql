BEGIN TRY
	BEGIN TRAN	
    Declare @EventCTE Table(eventid bigint)
    
    INSERT INTO @EventCTE 
    SELECT eventId 
    FROM Event e
    WHERE LOWER(RTRIM(LTRIM(e.EventStatus))) = 'expired' 
      AND (e.EventID NOT IN (SELECT TPD_Event_Id FROM Ticket_Purchased_Detail)
            OR e.EventID  NOT IN (SELECT E_Id FROM Ticket))
    DELETE FROM Ticket_Purchased_Detail WHERE TPD_Event_Id in (SELECT eventid FROM @EventCTE)  
    DELETE FROM Ticket_Quantity_Detail WHERE TQD_Event_Id in (SELECT eventid FROM @EventCTE)  
    DELETE FROM Ticket WHERE E_Id in (SELECT eventid FROM @EventCTE) 
    DELETE FROM Address WHERE EventId in (SELECT eventid FROM @EventCTE)
    DELETE FROM Event_Orgnizer_Detail  WHERE Orgnizer_Event_Id  in (SELECT eventid FROM @EventCTE)
    DELETE FROM Event_VariableDesc  WHERE Event_Id  in (SELECT eventid FROM @EventCTE)
    DELETE FROM EventVenue WHERE EventId in (SELECT eventid FROM @EventCTE)
    DELETE FROM EventFavourite WHERE eventId in (SELECT eventid FROM @EventCTE)
    DELETE FROM EventImage WHERE EventID in (SELECT eventid FROM @EventCTE)
    DELETE FROM Events_Hit WHERE EventHit_EventId in (SELECT eventid FROM @EventCTE)
    DELETE FROM Publish_Event_Detail WHERE PE_Event_Id in (SELECT eventid FROM @EventCTE)
    DELETE FROM Event WHERE EventID in (SELECT eventid FROM @EventCTE)
    DELETE FROM Order_Detail_T WHERE NOT EXISTS (SELECT TPD_Id FROM Ticket_Purchased_Detail WHERE TPD_Order_Id = Order_Detail_T.O_Order_Id)

    COMMIT TRAN
END TRY
BEGIN CATCH
	ROLLBACK TRAN
END CATCH
