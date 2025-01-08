using Diyan.Domain.Entities;
using Diyan.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Diyan.Application.Models
{
    public class MIS_PITracking_Search : BasePaninationEntity
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? PaperTypeId { get; set; }
    }

    public class MIS_Search : BasePaninationEntity
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class MIS_PITrackingList_Response : BaseResponseEntity
    {
        public string? PINumber { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int? PO_PortDischargeId { get; set; }
        public string? PortDischarge { get; set; }
        public DateTime? ShipmentSchedule { get; set; }
        public int? PO_PaymentTermsId { get; set; }
        public string? PaymentTerms { get; set; }
        public string? PO_BrandId { get; set; }
        public string? Brand { get; set; }
        public string? PO_TypeOfPackagingId { get; set; }
        public string? TypeOfPackaging { get; set; }
        public int? PO_Quantity { get; set; }
        public decimal? PO_CommissionPerTon { get; set; }
        public string? PaperTypeId { get; set; }
        public string? PaperType { get; set; }
    }

    public class MIS_AdvanceAmountList_Response : BaseResponseEntity
    {
        public string? PINumber { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int? PO_PaymentTermsId { get; set; }
        public string? PaymentTerms { get; set; }
        public string? Bank { get; set; }
        public decimal? PO_Amount { get; set; }
        public decimal? TotalReceivedAmount { get; set; }
        public decimal? BankCommission { get; set; }
        public string? BankReferenceNumber { get; set; }
        public DateTime? PaymentReceivedDate { get; set; }
    }

    public class MIS_FinalInvoicePaymentList_Response : BaseResponseEntity
    {
        public string? InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string? PINumber { get; set; }
        public int? ConsigneeId { get; set; }
        public string? ConsigneeName { get; set; }
        public int? NotifyPartyId { get; set; }
        public string? NotifyPartyName { get; set; }
        public int? PO_PortDischargeId { get; set; }
        public string? PortDischarge { get; set; }
        public int? CountryId { get; set; }
        public string? CountryName { get; set; }
        public string? PO_PaperTypeId { get; set; }
        public string? PaperType { get; set; }
        public string? PO_TypeOfPackagingId { get; set; }
        public string? TypeOfPackaging { get; set; }
        public string? PO_BrandId { get; set; }
        public string? Brand { get; set; }
        public int? PO_DeliveryTermsId { get; set; }
        public string? DeliveryTerms{ get; set; }
        public int? Quantity { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public decimal? Freight { get; set; }
        public decimal? FOBValue { get; set; }
        public int? PO_PaymentTermsId { get; set; }
        public string? PaymentTerms { get; set; }
        public decimal? TotalReceivedAmount { get; set; }
        public DateTime? PaymentReceivedDate { get; set; }
        public string? PortCode { get; set; }
        public string? SBNo { get; set; }
        public DateTime? SBDate { get; set; }
    }

    public class MIS_SBillList_Response : BaseResponseEntity
    {
        public string? InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string? SBNo { get; set; }
        public DateTime? SBDate { get; set; }
        public DateTime? LeoDate { get; set; }
        public string? PortCode { get; set; }
        public int? Quantity { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public decimal? Freight { get; set; }
        public decimal? FOBValue { get; set; }
        public decimal? ExchangeRate { get; set; }
        public decimal? TotalFOBValue { get; set; }
        public bool? BRCInBank { get; set; }
        public bool? BRCInDGFT { get; set; }
        public decimal? FOBPerct { get; set; }
        public decimal? DBKValue { get; set; }
        public bool? DBKReceived { get; set; }
        public decimal? IGSTAmount { get; set; }
        public bool? IGSTReceived { get; set; }
    }

    public class MIS_CommissionList_Response : BaseResponseEntity
    {
        public string? InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string? SBNo { get; set; }
        public DateTime? SBDate { get; set; }
        public decimal? ExchangeRate { get; set; }
        public string? PINumber { get; set; }
        public int? ConsigneeId { get; set; }
        public string? ConsigneeName { get; set; }
        public int? NotifyPartyId { get; set; }
        public string? NotifyPartyName { get; set; }
        public int? CountryId { get; set; }
        public string? CountryName { get; set; }
        public int? Quantity { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public decimal? PO_CommissionPerTon { get; set; }
        public decimal? TotalCommission { get; set; }
        public decimal? CommissionMentionInSBill { get; set; }
        public decimal? UtilizedAmount { get; set; }
        public decimal? UnUtilizedAmount { get; set; }
    }

    public class MIS_SellRateList_Response : BaseResponseEntity
    {
        public string? InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public int? Quantity { get; set; }
        public decimal? FinalInvoiceAmount { get; set; }
        public int? Containers { get; set; }
        public string? Reuse_Fresh { get; set; }
        public int? TransporterId { get; set; }
        public string? Transporter { get; set; }
        public decimal? Rate { get; set; }
        public decimal? LandFreight { get; set; }
        public string? TransporterInvoice { get; set; }
        public int? ForwarderId { get; set; }
        public string? Forwarder { get; set; }
        public string? ForwarderInvoice { get; set; }
        public decimal? SeaFreight { get; set; }
        public int? CHAId { get; set; }
        public string? CHA { get; set; }
        public string? ChaInvoice { get; set; }
        public decimal? Clearing { get; set; }
        public decimal? PO_CommissionPerTon { get; set; }
        public decimal? TotalCommission { get; set; }
        public decimal? ExchangeRate { get; set; }
        public decimal? CommissionInINR { get; set; }
        public string? SBNo { get; set; }
        public DateTime? SBDate { get; set; }
        public decimal? FOBValue { get; set; }
        public decimal? CurrentExchangeRate { get; set; }
        public decimal? DrawBack_RodTep { get; set; }
        public decimal? InvoiceAmountInINR { get; set; }
        public decimal? NetSellRate { get; set; }
    }
}
