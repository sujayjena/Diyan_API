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
        public string? PO_DeliveryTerms{ get; set; }
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
}
