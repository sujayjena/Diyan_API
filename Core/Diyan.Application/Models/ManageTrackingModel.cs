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
            ContainersUnderLoadingList = new List<ContainersUnderLoading_Request>();
            InvoiceList = new List<Invoice_Request>();
        }
        public int? CustomerId { get; set; }
        public int? PO_PortDischargeId { get; set; }
        [DefaultValue(false)]
        public bool? PO_IsPOReceived { get; set; }
        public string? PO_PONumber { get; set; }
        public int? PO_PaymentTermsId { get; set; }
        public int? PO_Quantity { get; set; }
        public int? PO_CurrencyTypeId { get; set; }
        public decimal? PO_CurrencyValue { get; set; }
        public string? PO_PaperTypeId { get; set; }
        public string? PO_BrandId { get; set; }
        public string? PO_TypeOfPackagingId { get; set; }
        public int? PO_DeliveryTermsId { get; set; }
        [DefaultValue(false)]
        public bool? PO_IsPOStatusClosed { get; set; }
        public string? PO_POImage { get; set; }
        public string? PO_POOriginalFileName { get; set; }
        public string? PO_Image_Base64 { get; set; }
        [DefaultValue(false)]
        public bool? IsActive { get; set; }


        [DefaultValue(false)]
        public bool? PII_IsClosed { get; set; }

        [DefaultValue(false)]
        public bool? PIC_IsConfirmed { get; set; }

        [JsonIgnore]
        [DefaultValue(0)]
        public int? PLR_PaymentOrLCReceived { get; set; }

        [JsonIgnore]
        [DefaultValue(false)]
        public bool? PLR_IsPaymentOrLCClosed { get; set; }



        [DefaultValue(false)]
        public bool? OA_IsOrderAccepted { get; set; }

        [DefaultValue(null)]
        public DateTime? OA_OrderAcceptedDate { get; set; }


        [DefaultValue(false)]
        public bool? OUP_IsOrderUnderProcess { get; set; }

        [DefaultValue(null)]
        public DateTime? OUP_OrderUnderProcessDate { get; set; }


        [DefaultValue(false)]
        public bool? BI_IsBookingIssueAccepted { get; set; }

        [DefaultValue(null)]
        public DateTime? BI_BookingIssueAcceptedDate { get; set; }

        [DefaultValue(null)]
        public DateTime? BI_ETD { get; set; }

        [DefaultValue(null)]
        public DateTime? BI_ETA { get; set; }

        [JsonIgnore]
        public string? BI_Image { get; set; }
        public string? BI_OriginalFileName { get; set; }
        public string? BI_Image_Base64 { get; set; }


        [DefaultValue(false)]
        public bool? CUL_IsContainersUnderLoadingClose { get; set; }

        [DefaultValue(null)]
        public DateTime? CUL_ContainersUnderLoadingClosedDate { get; set; }


        [DefaultValue(false)]
        public bool? IN_IsInvoiceGenerateClose { get; set; }

        [DefaultValue(null)]
        public DateTime? IN_InvoiceGenerateClosedDate { get; set; }


        public List<PIIssued_Request>? PIIssuedList { get; set; }

        public PO_PaymentReceived_Or_LCReceived_Request? PaymentReceived_Or_LCReceivedDetails { get; set; }

        public List<ContainersUnderLoading_Request>? ContainersUnderLoadingList { get; set; }

        public List<Invoice_Request>? InvoiceList { get; set; }
    }

    public class PurchaseOrderList_Response : BaseResponseEntity
    {
        public string? TrackingNumber { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? ParentCustomer { get; set; }
        public int? CountryId { get; set; }
        public string? CountryName { get; set; }
        public int? PO_PortDischargeId { get; set; }
        public string? PO_PortDischarge { get; set; }
        [DefaultValue(false)]
        public bool? PO_IsPOReceived { get; set; }
        public string? PO_PONumber { get; set; }
        public int? PO_PaymentTermsId { get; set; }
        public string? PO_PaymentTerms { get; set; }
        public int? PO_Quantity { get; set; }
        public int? PO_CurrencyTypeId { get; set; }
        public string? PO_CurrencyType { get; set; }
        public decimal? PO_CurrencyValue { get; set; }
        public string? PO_PaperTypeId { get; set; }
        public string? PO_PaperType { get; set; }
        public string? PO_BrandId { get; set; }
        public string? PO_Brand { get; set; }
        public string? PO_TypeOfPackagingId { get; set; }
        public string? PO_TypeOfPackaging { get; set; }
        public int? PO_DeliveryTermsId { get; set; }
        public string? PO_DeliveryTerms { get; set; }
        [DefaultValue(false)]
        public bool? PO_IsPOStatusClosed { get; set; }
        public string? PO_OriginalFileName { get; set; }
        public string? PO_Image { get; set; }
        public string? PO_ImageURL { get; set; }

        [DefaultValue(false)]
        public bool? IsActive { get; set; }
        public string? POCurrentStatus { get; set; }


        [DefaultValue(false)]
        public bool? PII_IsClosed { get; set; }

        [DefaultValue(false)]
        public bool? PIC_IsConfirmed { get; set; }

        [DefaultValue(false)]
        public bool? PLR_IsPaymentOrLCClosed { get; set; }

        [DefaultValue(false)]
        public bool? OA_IsOrderAccepted { get; set; }

        [DefaultValue(false)]
        public bool? OUP_IsOrderUnderProcess { get; set; }

        [DefaultValue(false)]
        public bool? BI_IsBookingIssueAccepted { get; set; }

        [DefaultValue(false)]
        public bool? CUL_IsContainersUnderLoadingClose { get; set; }

        [DefaultValue(false)]
        public bool? IN_IsInvoiceGenerateClose { get; set; }

    }

    public class PurchaseOrderDetail_Response : BaseResponseEntity
    {
        public PurchaseOrderDetail_Response()
        {
            PIIssuedList = new List<PIIssued_Response>();
            PIConfirmationList = new List<PIConfirmation_Response>();
            PaymentReceived_Or_LCReceivedDetail = new PO_PaymentReceived_Or_LCReceived_Resonse();
            ContainersUnderLoadingList = new List<ContainersUnderLoading_Response>();
            InvoiceList = new List<Invoice_Response>();
        }

        public string? TrackingNumber { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? ParentCustomer { get; set; }
        public int? CountryId { get; set; }
        public string? CountryName { get; set; }
        public int? PO_PortDischargeId { get; set; }
        public string? PO_PortDischarge { get; set; }
        [DefaultValue(false)]
        public bool? PO_IsPOReceived { get; set; }
        public string? PO_PONumber { get; set; }
        public int? PO_PaymentTermsId { get; set; }
        public string? PO_PaymentTerms { get; set; }
        public int? PO_Quantity { get; set; }
        public int? PO_CurrencyTypeId { get; set; }
        public string? PO_CurrencyType { get; set; }
        public decimal? PO_CurrencyValue { get; set; }
        public string? PO_PaperTypeId { get; set; }
        public string? PO_PaperType { get; set; }
        public string? PO_BrandId { get; set; }
        public string? PO_Brand { get; set; }
        public string? PO_TypeOfPackagingId { get; set; }
        public string? PO_TypeOfPackaging { get; set; }
        public int? PO_DeliveryTermsId { get; set; }
        public string? PO_DeliveryTerms { get; set; }
        [DefaultValue(false)]
        public string? PO_OriginalFileName { get; set; }
        public string? PO_Image { get; set; }
        public string? PO_ImageURL { get; set; }
        [DefaultValue(false)]
        public bool? IsActive { get; set; }
        public bool? PO_IsPOStatusClosed { get; set; }



        [DefaultValue(false)]
        public bool? PII_IsClosed { get; set; }

        [DefaultValue(null)]
        public DateTime? PII_CloseDate { get; set; }


        [DefaultValue(false)]
        public bool? PIC_IsConfirmed { get; set; }

        [DefaultValue(null)]
        public DateTime? PIC_CloseDate { get; set; }



        [DefaultValue(0)]
        public int? PLR_PaymentOrLCReceived { get; set; }

        [DefaultValue(null)]
        public DateTime? PLR_PaymentOrLCReceivedDate { get; set; }

        [DefaultValue(false)]
        public bool? PLR_IsPaymentOrLCClosed { get; set; }

        [DefaultValue(null)]
        public DateTime? PLR_PaymentOrLCClosedDate { get; set; }



        [DefaultValue(false)]
        public bool? OA_IsOrderAccepted { get; set; }

        [DefaultValue(null)]
        public DateTime? OA_OrderAcceptedDate { get; set; }



        [DefaultValue(false)]
        public bool? OUP_IsOrderUnderProcess { get; set; }

        [DefaultValue(null)]
        public DateTime? OUP_OrderUnderProcessDate { get; set; }



        [DefaultValue(false)]
        public bool? BI_IsBookingIssueAccepted { get; set; }

        [DefaultValue(null)]
        public DateTime? BI_BookingIssueAcceptedDate { get; set; }

        [DefaultValue(null)]
        public DateTime? BI_ETD { get; set; }

        [DefaultValue(null)]
        public DateTime? BI_ETA { get; set; }

        public string? BI_OriginalFileName { get; set; }
        public string? BI_Image { get; set; }
        public string? BI_ImageURL { get; set; }



        [DefaultValue(false)]
        public bool? CUL_IsContainersUnderLoadingClose { get; set; }

        [DefaultValue(null)]
        public DateTime? CUL_ContainersUnderLoadingClosedDate { get; set; }


        [DefaultValue(false)]
        public bool? IN_IsInvoiceGenerateClose { get; set; }

        [DefaultValue(null)]
        public DateTime? IN_InvoiceGenerateClosedDate { get; set; }


        public List<PIIssued_Response>? PIIssuedList { get; set; }

        public List<PIConfirmation_Response>? PIConfirmationList { get; set; }

        public PO_PaymentReceived_Or_LCReceived_Resonse? PaymentReceived_Or_LCReceivedDetail { get; set; }

        public List<ContainersUnderLoading_Response>? ContainersUnderLoadingList { get; set; }

        public List<Invoice_Response>? InvoiceList { get; set; }
    }


    #region PI Issued

    public class PIIssued_Search : BaseSearchEntity
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

    public class PIIssuedLog_Search
    {
        [DefaultValue(0)]
        public int? PIIssuedId { get; set; }
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
        public string? Remark { get; set; }
        public int? StatusId { get; set; }

        [JsonIgnore]
        [DefaultValue(true)]
        public bool? IsActive { get; set; }
    }

    public class PIIssued_Response : BaseResponseEntity
    {
        public PIIssued_Response()
        {
            PIIssuedLogList = new List<PIIssuedLog_Response>();
        }

        public int? Id { get; set; }
        public int? PurchaseOrderId { get; set; }
        public string? PO_PONumber { get; set; }
        public int? CountryId { get; set; }
        public string? CountryName { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? PIIssueDate { get; set; }
        public string? PINumber { get; set; }
        public string? PIImage { get; set; }
        public string? PIOriginalFileName { get; set; }
        public string? PIImageURL { get; set; }
        public int? StatusId { get; set; }
        public string? StatusName { get; set; }

        public List<PIIssuedLog_Response>? PIIssuedLogList { get; set; }
    }

    public class PIIssuedLog_Response
    {
        public int? PIIssuedId { get; set; }
        public string? Remarks { get; set; }
        public int? StatusId { get; set; }
        public string? StatusName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string? CreatorName { get; set; }
    }

    #endregion

    #region PI Confirmation

    public class PIConfirmation_Response : BaseResponseEntity
    {
        public int? Id { get; set; }
        public DateTime? PIIssueDate { get; set; }
        public string? PINumber { get; set; }
        public string? PIImage { get; set; }
        public string? PIOriginalFileName { get; set; }
        public string? PIImageURL { get; set; }
        public string? Remark { get; set; }
        public int? StatusId { get; set; }
        public string? StatusName { get; set; }
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
        public string? LCNumber { get; set; }
        public string? ImageName { get; set; }
        public string? OriginalFileName { get; set; }
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
        public string? InvoiceNumber { get; set; }
        public int? PaymentTermsId { get; set; }
        public string? PaymentTerms { get; set; }
        public DateTime? PaymentReceivedDate { get; set; }
        public int? CurrencyTypeId { get; set; }
        public string? CurrencyType { get; set; }
        public decimal? Amount { get; set; }
        public int? PaymentReceivedId { get; set; }
        public string? PaymentReceived { get; set; }
    }

    public class PO_LCReceived_Response : BaseResponseEntity
    {
        [JsonIgnore]
        public int? PurchaseOrderId { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string? LCNumber { get; set; }
        public string? ImageName { get; set; }
        public string? OriginalFileName { get; set; }
        public string? ImageURL { get; set; }
    }

    #endregion


    public class PO_PaymentReceived_Or_LCReceivedList_Response : BaseResponseEntity
    {
        public int? PurchaseOrderId { get; set; }
        public int? CountryId { get; set; }
        public string? CountryName { get; set; }
        public int? CustomerId { get; set; }
        public string? IsPaymentReceived { get; set; }
        public string? IsLCReceived { get; set; }
        public bool? PLR_PaymentOrLCClosed { get; set; }
        public string? InvoiceNumber { get; set; }
        public int? PaymentTermsId { get; set; }
        public string? PaymentTerms { get; set; }
        public DateTime? PaymentReceivedDate { get; set; }
        public int? CurrencyTypeId { get; set; }
        public string? CurrencyType { get; set; }
        public decimal? Amount { get; set; }
        public int? PaymentReceivedId { get; set; }
        public string? PaymentReceived { get; set; }
        public int? LCReceivedDate { get; set; }
        public string? ImageName { get; set; }
        public string? OriginalFileName { get; set; }
        public string? ImageURL { get; set; }
    }


    #endregion

    #region Containers UnderLoading Images

    public class ContainersUnderLoading_Search
    {
        [DefaultValue(0)]
        public int? PurchaseOrderId { get; set; }
    }

    public class ContainersUnderLoadingImages_Search
    {
        [DefaultValue(0)]
        public int? ContainersUnderLoadingId { get; set; }
    }

    public class ContainersUnderLoading_Request : BaseEntity
    {
        public ContainersUnderLoading_Request()
        {
            ContainersUnderLoadingImagesList = new List<ContainersUnderLoadingImages_Request>();
        }

        [JsonIgnore]
        public int? PurchaseOrderId { get; set; }
        public int? ContainerCount { get; set; }

        public List<ContainersUnderLoadingImages_Request>? ContainersUnderLoadingImagesList { get; set; }
    }

    public class ContainersUnderLoadingImages_Request : BaseEntity
    {
        [JsonIgnore]
        public int? ContainersUnderLoadingId { get; set; }
        public string ContainerImage { get; set; }
        public string ContainerOriginalFileName { get; set; }
        public string? ContainerOriginalImage_Base64 { get; set; }
    }

    public class ContainersUnderLoading_Response : BaseResponseEntity
    {
        public ContainersUnderLoading_Response()
        {
            ContainersUnderLoadingImagesList = new List<ContainersUnderLoadingImages_Response>();
        }

        [JsonIgnore]
        public int? PurchaseOrderId { get; set; }
        public int? ContainerCount { get; set; }

        public List<ContainersUnderLoadingImages_Response>? ContainersUnderLoadingImagesList { get; set; }
    }

    public class ContainersUnderLoadingImages_Response : BaseEntity
    {
        public int? ContainersUnderLoadingId { get; set; }
        public string ContainerImage { get; set; }
        public string ContainerOriginalFileName { get; set; }
        public string ContainerImageURL { get; set; }
    }

    #endregion

    #region Invoice

    public class Invoice_Search
    {
        [DefaultValue(0)]
        public int? PurchaseOrderId { get; set; }
    }

    public class Invoice_Request : BaseEntity
    {
        [JsonIgnore]
        public int? PurchaseOrderId { get; set; }

        public string? InvoiceNumber { get; set; }
        public string? InvoiceImage { get; set; }
        public string? InvoiceOriginalFileName { get; set; }
        public string? Invoice_Base64 { get; set; }
    }

    public class Invoice_Response : BaseResponseEntity
    {
        [JsonIgnore]
        public int? PurchaseOrderId { get; set; }
        public int? InvoiceNumber { get; set; }
        public string InvoiceImage { get; set; }
        public string InvoiceOriginalFileName { get; set; }
        public string InvoiceImageURL { get; set; }
    }

    #endregion
}
