using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class TicketSaleViewModel
    {
        public long EventId { get; set; }
        public decimal VarChargesAmount { get; set; }
        public decimal PromoCodeAmount { get; set; }
        public decimal Refunded { get; set; }
        public decimal Discount { get; set; }
        public decimal EventComboFee { get; set; }
        public decimal MerchantFee { get; set; }
        private List<TicketSales> _ticketSales = new List<TicketSales>();
        public List<TicketSales> TicketSales
        {
            get { return _ticketSales; }
            set { _ticketSales = value; }
        }
    }

    public class TicketSales
    {
        public long TicketId { get; set; }
        public string TicketName { get; set; }
        public long TicketTypeId { get; set; }
        public string TicketTypeName { get; set; }
        public decimal PricePerTicket { get; set; }
        public long Quantity { get; set; }
        public decimal PricePaid { get; set; }
        public decimal PriceNet { get; set; }
    }

    public class OrderSummaryViewModel
    {
        public long OId { get; set; }
        public string OrderId { get; set; }
        public long EventId { get; set; }
        public long Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal PriceNet { get; set; }
        public decimal Discount { get; set; }
        public decimal Fee { get; set; }
        public decimal EventComboFee { get; set; }
        public decimal MerchantFee { get; set; }
        public decimal CustomerFee { get; set; }
        public decimal VarChargesAmount { get; set; }
        public decimal Refunded { get; set; }
        public decimal Cancelled { get; set; }
        public decimal PromoCodeAmount { get; set; }
        public DateTime Date { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsRefunded { get; set; }
        public bool IsManualOrder { get; set; }
    }

    public class EventSummaryViewModel
    {
        public long EventId { get; set; }
        public long OrderQuantity { get; set; }
        public long TicketQuantity { get; set; }
        public decimal Price { get; set; }
        public decimal PriceNet { get; set; }
        public decimal Fee { get; set; }
        public decimal CustomerFee { get; set; }
        public decimal VarChargesAmount { get; set; }
        public decimal Refunded { get; set; }
        public decimal Cancelled { get; set; }
    }
}