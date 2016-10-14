/****** Object:  View [dbo].[TicketAttendee_View]    Script Date: 14-Oct-16 3:27:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[TicketAttendee_View]
AS
SELECT        
		TicketAttendee.TicketAttendeeId, 
		TicketAttendee.TicketBearerId, 
		TicketBearer.UserId, 
		TicketBearer.OrderId, 
		TicketBearer.Guid, 
		TicketBearer.Name, 
		TicketBearer.Email, 
		TicketAttendee.PurchasedTicketId, 
		Ticket_Purchased_Detail.TPD_User_Id, 
		Ticket_Purchased_Detail.TPD_Order_Id, 
		Ticket_Purchased_Detail.TPD_Purchased_Qty, 
		Ticket_Purchased_Detail.TPD_TQD_Id, 
		Ticket_Purchased_Detail.TPD_Event_Id, 
		Ticket_Purchased_Detail.TPD_Amount, 
		Ticket_Purchased_Detail.TPD_Donate, 
		Ticket_Purchased_Detail.TPD_GUID, 
		Ticket_Purchased_Detail.TPD_EC_Fee, 
		Ticket_Purchased_Detail.TPD_PromoCodeID, 
		Ticket_Purchased_Detail.TPD_PromoCodeAmount, 
		Ticket_Purchased_Detail.Customer_Fee, 
		TicketAttendee.Quantity,
		Ticket.T_name

FROM  TicketAttendee 
INNER JOIN TicketBearer ON TicketAttendee.TicketBearerId = TicketBearer.TicketbearerId 
INNER JOIN Ticket_Purchased_Detail ON TicketAttendee.PurchasedTicketId = Ticket_Purchased_Detail.TPD_Id
INNER JOIN Ticket_Quantity_Detail ON Ticket_Quantity_Detail.TQD_Id = Ticket_Purchased_Detail.TPD_TQD_Id
INNER JOIN Ticket ON Ticket.T_Id = Ticket_Quantity_Detail.TQD_Ticket_Id



GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[51] 4[11] 2[21] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "TicketAttendee"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 224
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TicketBearer"
            Begin Extent = 
               Top = 0
               Left = 613
               Bottom = 130
               Right = 783
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "Ticket_Purchased_Detail"
            Begin Extent = 
               Top = 140
               Left = 470
               Bottom = 270
               Right = 694
            End
            DisplayFlags = 280
            TopColumn = 9
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TicketAttendee_View'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TicketAttendee_View'
GO


