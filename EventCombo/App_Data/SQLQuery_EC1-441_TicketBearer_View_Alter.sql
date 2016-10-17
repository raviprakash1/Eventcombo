/****** Object:  View [dbo].[TicketBearer_View]    Script Date: 13-Oct-16 11:13:10 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER VIEW [dbo].[TicketBearer_View]
AS

SELECT 

	tb.TicketbearerId,  
	tb.OrderId, 
	tb.UserId, 
	tb.Guid, 
	tb.Name, 
	tb.Email

FROM TicketBearer tb


GO


