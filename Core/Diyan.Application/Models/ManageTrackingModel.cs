﻿using Diyan.Domain.Entities;
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
    public class PurchaseOrderSearch_Request : BaseSearchEntity
    {
        public int? CustomerId { get; set; }
        public int? CountryId { get; set; }

        [DefaultValue("")]
        public string? PONumber { get; set; }
    }

    public class PurchaseOrder_Request : BaseEntity
    {
        public PurchaseOrder_Request()
        {
            PIIssuedList = new List<PIIssued_Request>();
        }
        public int? CustomerId { get; set; }
        public int? PO_PortDischargeId { get; set; }
        [DefaultValue(false)]
        public bool? PO_IsPOReceived { get; set; }
        public string PO_PONumber { get; set; }
        public int? PO_PaymentTermsId { get; set; }
        public int? PO_Quantity { get; set; }
        public int? PO_CurrencyTypeId { get; set; }
        public decimal? PO_CurrencyValue { get; set; }
        public string PO_PaperTypeId { get; set; }
        public string PO_BrandId { get; set; }
        public string PO_TypeOfPackagingId { get; set; }
        public int? PO_DeliveryTermsId { get; set; }
        [DefaultValue(false)]
        public bool? PO_IsPOStatusClosed { get; set; }
        public string PO_POImage { get; set; }
        public string PO_POOriginalFileName { get; set; }
        public string? PO_Image_Base64 { get; set; }
        [DefaultValue(false)]
        public bool? IsActive { get; set; }

        [DefaultValue(false)]
        public bool? PII_IsClosed { get; set; }

        [JsonIgnore]
        [DefaultValue(0)]
        public int? PLR_PaymentOrLCReceived { get; set; }

        [JsonIgnore]
        [DefaultValue(false)]
        public bool? PLR_PaymentOrLCClosed { get; set; }

        public List<PIIssued_Request>? PIIssuedList { get; set; }

        public PO_PaymentReceived_Or_LCReceived_Request? PaymentReceived_Or_LCReceivedDetails { get; set; }
    }

    public class PurchaseOrder_Response : BaseResponseEntity
    {
        public PurchaseOrder_Response()
        {
            PIIssuedList = new List<PIIssued_Response>();
            PIConfirmationList = new List<PIConfirmation_Response>();
            PaymentReceived_Or_LCReceivedDetail = new PO_PaymentReceived_Or_LCReceived_Resonse();
        }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? ParentCustomer { get; set; }
        public int? CountryId { get; set; }
        public string? CountryName { get; set; }
        public int? PO_PortDischargeId { get; set; }
        public string PO_PortDischarge { get; set; }
        [DefaultValue(false)]
        public bool? PO_IsPOReceived { get; set; }
        public string PO_PONumber { get; set; }
        public int? PO_PaymentTermsId { get; set; }
        public string PO_PaymentTerms { get; set; }
        public int? PO_Quantity { get; set; }
        public int? PO_CurrencyTypeId { get; set; }
        public string PO_CurrencyType { get; set; }
        public decimal? PO_CurrencyValue { get; set; }
        public string PO_PaperTypeId { get; set; }
        public string PO_PaperType { get; set; }
        public string PO_BrandId { get; set; }
        public string PO_Brand { get; set; }
        public string PO_TypeOfPackagingId { get; set; }
        public string PO_TypeOfPackaging { get; set; }
        public int? PO_DeliveryTermsId { get; set; }
        public string PO_DeliveryTerms { get; set; }
        [DefaultValue(false)]
        public bool? PO_IsPOStatusClosed { get; set; }
        public string PO_OriginalFileName { get; set; }
        public string PO_Image { get; set; }
        public string PO_ImageURL { get; set; }

        [DefaultValue(false)]
        public bool? IsActive { get; set; }

        [DefaultValue(false)]
        public bool? PII_IsClosed { get; set; }

        [DefaultValue(0)]
        public int? PLR_PaymentOrLCReceived { get; set; }

        [DefaultValue(false)]
        public bool? PLR_PaymentOrLCClosed { get; set; }

        public List<PIIssued_Response>? PIIssuedList { get; set; }

        public List<PIConfirmation_Response>? PIConfirmationList { get; set; }

        public PO_PaymentReceived_Or_LCReceived_Resonse? PaymentReceived_Or_LCReceivedDetail { get; set; }
    }


    #region PI Issued

    public class PIIssuedSearch_Request : BaseSearchEntity
    {
        [DefaultValue(0)]
        public int? PurchaseOrderId { get; set; }
        [DefaultValue(0)]
        public int? CountryId { get; set; }
        [DefaultValue(0)]
        public int? CustomerId { get; set; }
        [DefaultValue("")]
        public string? PINumber { get; set; }
        [DefaultValue(0)]
        public int? StatusId { get; set; }
    }

    public class PIIssued_Request : BaseEntity
    {
        [JsonIgnore]
        public int? PurchaseOrderId { get; set; }
        public DateTime? PIIssueDate { get; set; }
        public string? PINumber { get; set; }
        public string? PIImage { get; set; }
        public string? PIImage_Base64 { get; set; }
        public string? PIOriginalFileName { get; set; }
        public string Remark { get; set; }
        public int? StatusId { get; set; }

        [JsonIgnore]
        [DefaultValue(true)]
        public bool? IsActive { get; set; }
    }

    public class PIIssued_Response
    {
        public int? Id { get; set; }

        public int? PurchaseOrderId { get; set; }
        public string PO_PONumber { get; set; }
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? PIIssueDate { get; set; }
        public string PINumber { get; set; }
        public string PIOriginalFileName { get; set; }
        public string PIImage { get; set; }
        public string PIImageURL { get; set; }
        public string Remark { get; set; }
        public int? StatusId { get; set; }
        public string StatusName { get; set; }
    }

    #endregion

    #region PI Confirmation

    public class PIConfirmation_Response
    {
        public int? Id { get; set; }
        public DateTime? PIIssueDate { get; set; }
        public string PINumber { get; set; }
        public string Remark { get; set; }
        public int? StatusId { get; set; }
        public string StatusName { get; set; }
    }

    #endregion

    #region Payment Received and LC Received

    public class PO_PaymentReceived_Or_LCReceived_Search : BaseSearchEntity
    {
        [DefaultValue(0)]
        public int? PurchaseOrderId { get; set; }
        [DefaultValue(0)]
        public int? CountryId { get; set; }
        [DefaultValue(0)]
        public int? CustomerId { get; set; }
        [DefaultValue("")]
        public string? PINumber { get; set; }
        [DefaultValue("")]
        public string? InvoiceNumber { get; set; }
        [DefaultValue(0)]
        public int? StatusId { get; set; }
    }

    #region PaymentReceived_Or_LCReceived Inner Request and Response

    public class PO_PaymentReceived_Or_LCReceived_Request 
    {
        public PO_PaymentReceived_Or_LCReceived_Request()
        {
            PaymentReceivedDetail = new List<PO_PaymentReceived_Request>();
            LCReceivedDetail = new List<PO_LCReceived_Request>();
        }

        [DefaultValue(0)]
        public int? PaymentOrLCReceived { get; set; }

        [DefaultValue(false)]
        public bool? PaymentOrLCClosed { get; set; }

        public List<PO_PaymentReceived_Request>? PaymentReceivedDetail { get; set; }

        public List<PO_LCReceived_Request>? LCReceivedDetail { get; set; }
    }

    public class PO_PaymentReceived_Request : BaseEntity
    {
        [JsonIgnore]
        public int? PurchaseOrderId { get; set; }
        public string? InvoiceNumber { get; set; }
        public int? PaymentTermsId { get; set; }
        public DateTime? PaymentReceivedDate { get; set; }
        public int? CurrencyTypeId { get; set; }
        public double? Amount { get; set; }
        public int? PaymentReceivedId { get; set; }
    }

    public class PO_LCReceived_Request : BaseEntity
    {
        [JsonIgnore]
        public int? PurchaseOrderId { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string ImageName { get; set; }
        public string OriginalFileName { get; set; }
        public string? Image_Base64 { get; set; }
    }


    public class PO_PaymentReceived_Or_LCReceived_Resonse
    {
        public PO_PaymentReceived_Or_LCReceived_Resonse()
        {
            PaymentReceivedDetail = new List<PO_PaymentReceived_Response>();
            LCReceivedDetail = new List<PO_LCReceived_Response>();
        }

        public List<PO_PaymentReceived_Response>? PaymentReceivedDetail { get; set; }

        public List<PO_LCReceived_Response>? LCReceivedDetail { get; set; }
    }

    public class PO_PaymentReceived_Response : BaseResponseEntity
    {
        [JsonIgnore]
        public int? PurchaseOrderId { get; set; }
        public string InvoiceNumber { get; set; }
        public int? PaymentTermsId { get; set; }
        public string PaymentTerms { get; set; }
        public DateTime? PaymentReceivedDate { get; set; }
        public int? CurrencyTypeId { get; set; }
        public string CurrencyType { get; set; }
        public decimal? Amount { get; set; }
        public int? PaymentReceivedId { get; set; }
        public string PaymentReceived { get; set; }
    }

    public class PO_LCReceived_Response : BaseResponseEntity
    {
        [JsonIgnore]
        public int? PurchaseOrderId { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string ImageName { get; set; }
        public string OriginalFileName { get; set; }
        public string ImageURL { get; set; }
    }

    #endregion


    public class PO_PaymentReceived_Or_LCReceivedList_Response : BaseResponseEntity
    {
        public int? PurchaseOrderId { get; set; }
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
        public int? CustomerId { get; set; }
        public string IsPaymentReceived { get; set; }
        public string IsLCReceived { get; set; }
        public bool? PLR_PaymentOrLCClosed { get; set; }
        public string InvoiceNumber { get; set; }
        public int? PaymentTermsId { get; set; }
        public string PaymentTerms { get; set; }
        public DateTime? PaymentReceivedDate { get; set; }
        public int? CurrencyTypeId { get; set; }
        public string CurrencyType { get; set; }
        public decimal? Amount { get; set; }
        public int? PaymentReceivedId { get; set; }
        public string PaymentReceived { get; set; }
        public int? LCReceivedDate { get; set; }
        public string ImageName { get; set; }
        public string OriginalFileName { get; set; }
        public string ImageURL { get; set; }
    }


    #endregion
}
