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
    public class PurchaseOrderSearch_Request : BaseSearchEntity
    {
        [DefaultValue(0)]
        public int CountryId { get; set; }

        [DefaultValue(0)]
        public int CustomerId { get; set; }

        [DefaultValue(0)]
        public int StatusId { get; set; }

        [DefaultValue(0)]
        public int TrakingStatusId { get; set; }

        public string? TrakingNumber { get; set; }

        public string? PINumber { get; set; }

        [DefaultValue(0)]
        public int IsPIConfirmation { get; set; }

        [DefaultValue(0)]
        public int IsPaymentOrLCReceived { get; set; }

        [DefaultValue(0)]
        public int IsCommissionAgent { get; set; }

        [DefaultValue("All")]
        public string FilterType { get; set; }
    }

    public class PurchaseOrder_Request : BaseEntity
    {
        public PurchaseOrder_Request()
        {
            PIIssuedList = new List<PIIssued_Request>();
            ContainersUnderLoadingList = new List<ContainersUnderLoading_Request>();
            InvoiceList = new List<Invoice_Request>();
            BIDraftIssuedImagesList = new List<BIDraftIssuedImages_Request>();
        }
        public int? CustomerId { get; set; }
        public int? PO_PortDischargeId { get; set; }
        [DefaultValue(false)]
        public bool? PO_IsPOReceived { get; set; }
        public string? PO_PONumber { get; set; }
        public int? PO_PaymentTermsId { get; set; }
        public decimal? PO_Quantity { get; set; }
        public int? PO_PaymentType { get; set; }
        public int? PO_CurrencyTypeId { get; set; }
        public decimal? PO_CurrencyValue { get; set; }
        public decimal? PO_Amount { get; set; }
        public string? PO_PaperTypeId { get; set; }
        public string? PO_BrandId { get; set; }
        public string? PO_TypeOfPackagingId { get; set; }
        public int? PO_DeliveryTermsId { get; set; }
        [DefaultValue(false)]
        public bool? PO_IsPOStatusClosed { get; set; }
        public string? PO_POImage { get; set; }
        public string? PO_POOriginalFileName { get; set; }
        public string? PO_Image_Base64 { get; set; }
        public decimal? PO_CommissionPerTon { get; set; }

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

        public string? BID_BIDraftRemark { get; set; }
        public string? BID_BIDraftComment { get; set; }

        [DefaultValue(false)]
        public bool? BID_IsBIDraftIssueClose { get; set; }
        [DefaultValue(null)]
        public DateTime? BID_BIDraftIssueClosedDate { get; set; }

        [DefaultValue(false)]
        public bool? BID_IsConfirmed { get; set; }


        [DefaultValue(false)]
        public bool? FBI_IsFinalBIDraftIssueClose { get; set; }
        [DefaultValue(null)]
        public DateTime? FBI_FinalBIDraftIssueClosedDate { get; set; }

        [JsonIgnore]
        public string? FBI_FinalBIImage { get; set; }
        public string? FBI_FinalBIOriginalFileName { get; set; }
        public string? FBI_FinalBIImage_Base64 { get; set; }


        [DefaultValue(false)]
        public bool? FAP_IsFinalAmountToPayClose { get; set; }

        [DefaultValue(null)]
        public DateTime? FAP_FinalAmountToPayClosedDate { get; set; }

        [DefaultValue("")]
        public string? FAP_FinalAmountToPay { get; set; }


        [DefaultValue(false)]
        public bool? PR_IsPaymentReceived { get; set; }
        [DefaultValue(null)]
        public DateTime? PR_PaymentReceivedDate { get; set; }

        public string? PR_FinalAmount { get; set; }
        public string? PR_BankReferenceNumber { get; set; }

        [DefaultValue("")]
        public string? PR_AmountDue { get; set; }
        public decimal? PR_BalanceAmount { get; set; }

        [JsonIgnore]
        public string? PR_Image { get; set; }
        public string? PR_OriginalFileName { get; set; }
        public string? PR_Image_Base64 { get; set; }


        [DefaultValue(false)]
        public bool? DDS_IsDocumentSendDHL_Submitted { get; set; }
        [DefaultValue(null)]
        public DateTime? DDS_DocumentSendDHL_SubmittedDate { get; set; }
        public string? DDS_AWBNumber { get; set; }


        [DefaultValue(false)]
        public bool? POC_IsPOClosed { get; set; }
        [DefaultValue(null)]
        public DateTime? POC_POClosedDate { get; set; }

        public decimal? MC_TotalQty { get; set; }
        public decimal? MC_TotalPaymentReceived { get; set; }
        public decimal? MC_CommissionPerTon { get; set; }
        public decimal? MC_TotalCommission { get; set; }

        [DefaultValue(null)]
        public DateTime? MC_CommissionClosedDate { get; set; }

        [DefaultValue(false)]
        public bool? MC_IsCommissionClosed { get; set; }


        [DefaultValue(false)]
        public bool? OC_IsOrderCompleteClosed { get; set; }
        [DefaultValue(null)]
        public DateTime? OC_OrderCompleteDate { get; set; }

        public List<PIIssued_Request>? PIIssuedList { get; set; }

        public PO_PaymentReceived_Or_LCReceived_Request? PaymentReceived_Or_LCReceivedDetails { get; set; }

        public List<ContainersUnderLoading_Request>? ContainersUnderLoadingList { get; set; }

        public List<Invoice_Request>? InvoiceList { get; set; }

        public List<BIDraftIssuedImages_Request>? BIDraftIssuedImagesList { get; set; }
    }

    public class PurchaseOrderList_Response : BaseResponseEntity
    {
        public string? TrackingNumber { get; set; }
        public string? PINumber { get; set; }
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
        public decimal? PO_Quantity { get; set; }
        public int? PO_PaymentType { get; set; }
        public int? PO_CurrencyTypeId { get; set; }
        public string? PO_CurrencyType { get; set; }
        public decimal? PO_CurrencyValue { get; set; }
        public decimal? PO_Amount { get; set; }
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
        public decimal? PO_CommissionPerTon { get; set; }

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


        [DefaultValue(false)]
        public bool? BID_IsBIDraftIssueClose { get; set; }

        [DefaultValue(false)]
        public bool? BID_IsConfirmed { get; set; }


        [DefaultValue(false)]
        public bool? FBI_IsFinalBIDraftIssueClose { get; set; }

        [DefaultValue(false)]
        public bool? FAP_IsFinalAmountToPayClose { get; set; }

        [DefaultValue(false)]
        public bool? PR_IsPaymentReceived { get; set; }

        [DefaultValue(false)]
        public bool? DDS_IsDocumentSendDHL_Submitted { get; set; }

        [DefaultValue(false)]
        public bool? POC_IsPOClosed { get; set; }

        [DefaultValue(false)]
        public bool? MC_IsCommissionClosed { get; set; }

        [DefaultValue(false)]
        public bool? OC_IsOrderCompleteClosed { get; set; }
        [DefaultValue(null)]
        public DateTime? OC_OrderCompleteDate { get; set; }
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
            BIDraftIssuedImagesList = new List<BIDraftIssuedImages_Response>();
            BIDraftIssuedRemarkLogList = new List<BIDraftIssuedRemarkLog_Response>();
            BIDraftIssuedCommentLogList = new List<BIDraftIssuedCommentsLog_Response>();
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
        public decimal? PO_Quantity { get; set; }
        public int? PO_PaymentType { get; set; }
        public int? PO_CurrencyTypeId { get; set; }
        public string? PO_CurrencyType { get; set; }
        public decimal? PO_CurrencyValue { get; set; }
        public decimal? PO_Amount { get; set; }
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
        public decimal? PO_CommissionPerTon { get; set; }

        [DefaultValue(false)]
        public bool? IsActive { get; set; }
        public bool? PO_IsPOStatusClosed { get; set; }
        [DefaultValue(null)]
        public DateTime? PO_StatusClosedDate { get; set; }


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

        [DefaultValue(null)]
        public DateTime? OA_OrderAcceptedClosedDate { get; set; }



        [DefaultValue(false)]
        public bool? OUP_IsOrderUnderProcess { get; set; }

        [DefaultValue(null)]
        public DateTime? OUP_OrderUnderProcessDate { get; set; }

        [DefaultValue(null)]
        public DateTime? OUP_OrderUnderProcessClosedDate { get; set; }



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

        [DefaultValue(null)]
        public DateTime? CUL_ContainersUnderLoadingClosedDateTime { get; set; }


        [DefaultValue(false)]
        public bool? IN_IsInvoiceGenerateClose { get; set; }

        [DefaultValue(null)]
        public DateTime? IN_InvoiceGenerateClosedDate { get; set; }

        [DefaultValue(null)]
        public DateTime? IN_InvoiceGenerateClosedDateTime { get; set; }


        public string? BID_BIDraftComment { get; set; }

        [DefaultValue(false)]
        public bool? BID_IsBIDraftIssueClose { get; set; }

        [DefaultValue(null)]
        public DateTime? BID_BIDraftIssueClosedDate { get; set; }

        [DefaultValue(null)]
        public DateTime? BID_BIDraftIssueClosedDateTime { get; set; }

        [DefaultValue(false)]
        public bool? BID_IsConfirmed { get; set; }


        [DefaultValue(false)]
        public bool? FBI_IsFinalBIDraftIssueClose { get; set; }

        [DefaultValue(null)]
        public DateTime? FBI_FinalBIDraftIssueClosedDate { get; set; }

        [DefaultValue(null)]
        public DateTime? FBI_FinalBIDraftIssueClosedDateTime { get; set; }

        public string? FBI_FinalBIImage { get; set; }
        public string? FBI_FinalBIOriginalFileName { get; set; }
        public string? FBI_FinalBIImageURL { get; set; }


        [DefaultValue(false)]
        public bool? FAP_IsFinalAmountToPayClose { get; set; }

        [DefaultValue(null)]
        public DateTime? FAP_FinalAmountToPayClosedDate { get; set; }

        [DefaultValue(null)]
        public DateTime? FAP_FinalAmountToPayClosedDateTime { get; set; }
        public string? FAP_FinalAmountToPay { get; set; }


        [DefaultValue(false)]
        public bool? PR_IsPaymentReceived { get; set; }

        [DefaultValue(null)]
        public DateTime? PR_PaymentReceivedDate { get; set; }

        [DefaultValue(null)]
        public DateTime? PR_PaymentReceivedClosedDate { get; set; }

        public string? PR_FinalAmount { get; set; }
        public string? PR_BankReferenceNumber { get; set; }
        public string? PR_AmountDue { get; set; }
        public decimal? PR_BalanceAmount { get; set; }

        public string? PR_Image { get; set; }
        public string? PR_OriginalFileName { get; set; }
        public string? PR_ImageURL { get; set; }


        [DefaultValue(false)]
        public bool? DDS_IsDocumentSendDHL_Submitted { get; set; }

        [DefaultValue(null)]
        public DateTime? DDS_DocumentSendDHL_SubmittedDate { get; set; }

        [DefaultValue(null)]
        public DateTime? DDS_DocumentSendDHL_SubmittedClosedDate { get; set; }
        public string? DDS_AWBNumber { get; set; }


        [DefaultValue(false)]
        public bool? POC_IsPOClosed { get; set; }

        [DefaultValue(null)]
        public DateTime? POC_POClosedDate { get; set; }

        [DefaultValue(null)]
        public DateTime? POC_POClosedDateTime { get; set; }

        public decimal? MC_TotalQty { get; set; }
        public decimal? MC_TotalPaymentReceived { get; set; }
        public decimal? MC_CommissionPerTon { get; set; }
        public decimal? MC_TotalCommission { get; set; }

        [DefaultValue(null)]
        public DateTime? MC_CommissionClosedDate { get; set; }

        [DefaultValue(null)]
        public DateTime? MC_CommissionClosedDateTime { get; set; }

        [DefaultValue(false)]
        public bool? MC_IsCommissionClosed { get; set; }


        [DefaultValue(false)]
        public bool? OC_IsOrderCompleteClosed { get; set; }

        [DefaultValue(null)]
        public DateTime? OC_OrderCompleteDate { get; set; }

        [DefaultValue(null)]
        public DateTime? OC_OrderCompleteClosedDate { get; set; }


        public List<PIIssued_Response>? PIIssuedList { get; set; }

        public List<PIConfirmation_Response>? PIConfirmationList { get; set; }

        public PO_PaymentReceived_Or_LCReceived_Resonse? PaymentReceived_Or_LCReceivedDetail { get; set; }

        public List<ContainersUnderLoading_Response>? ContainersUnderLoadingList { get; set; }

        public List<Invoice_Response>? InvoiceList { get; set; }

        public List<BIDraftIssuedImages_Response>? BIDraftIssuedImagesList { get; set; }
        public List<BIDraftIssuedRemarkLog_Response>? BIDraftIssuedRemarkLogList { get; set; }
        public List<BIDraftIssuedCommentsLog_Response>? BIDraftIssuedCommentLogList { get; set; }
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

    public class ValidatePINumber_Response
    {
        public int? Id { get; set; }
        public string? PINumber { get; set; }
    }

    public class PINumberForSelect_Search
    {
        [DefaultValue(0)]
        public int? StatusId { get; set; }

        [DefaultValue(0)]
        public int? CustomerId { get; set; }
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
        public PO_PaymentReceived_Request()
        {
            PurchaseOrderPaymentReceivedImagesList = new List<PurchaseOrderPaymentReceivedImages_Request>();
        }

        [JsonIgnore]
        public int? PurchaseOrderId { get; set; }
        public string? InvoiceNumber { get; set; }
        public int? PaymentTermsId { get; set; }
        public DateTime? PaymentReceivedDate { get; set; }
        public int? CurrencyTypeId { get; set; }
        public double? Amount { get; set; }
        public int? PaymentReceivedId { get; set; }
        public decimal? POAmount { get; set; }
        public decimal? RemainingAmount { get; set; }
        public decimal? TotalReceivedAmount { get; set; }
        public string? BankReferenceNumber { get; set; }
        public DateTime? InvoiceGenerateDate { get; set; }
        public string? PINumber { get; set; }
        public DateTime? PIIssuedDate { get; set; }

        [DefaultValue(0)]
        public int? BankId { get; set; }

        [DefaultValue(0)]
        public decimal? BankCommission { get; set; }

        [DefaultValue("Payment_Received")]
        [JsonIgnore]
        public string? ModuleType { get; set; }

        public List<PurchaseOrderPaymentReceivedImages_Request> PurchaseOrderPaymentReceivedImagesList { get; set; }
    }

    public class PO_LCReceived_Request : BaseEntity
    {
        [JsonIgnore]
        public int? PurchaseOrderId { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string? LCNumber { get; set; }
        public decimal? POAmount { get; set; }
        public string? PINumber { get; set; }
        public DateTime? PIIssuedDate { get; set; }
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
        public PO_PaymentReceived_Response()
        {
            PurchaseOrderPaymentReceivedImagesList = new List<PurchaseOrderPaymentReceivedImages_Response>();
        }

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
        public decimal? POAmount { get; set; }
        public decimal? RemainingAmount { get; set; }
        public decimal? TotalReceivedAmount { get; set; }
        public string? BankReferenceNumber { get; set; }
        public DateTime? InvoiceGenerateDate { get; set; }
        public string? PINumber { get; set; }
        public DateTime? PIIssuedDate { get; set; }
        public int? BankId { get; set; }
        public string? BankName { get; set; }
        public decimal? BankCommission { get; set; }
        public List<PurchaseOrderPaymentReceivedImages_Response> PurchaseOrderPaymentReceivedImagesList { get; set; }
    }

    public class PO_LCReceived_Response : BaseResponseEntity
    {
        [JsonIgnore]
        public int? PurchaseOrderId { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string? LCNumber { get; set; }
        public decimal? POAmount { get; set; }
        public string? PINumber { get; set; }
        public DateTime? PIIssuedDate { get; set; }
        public string? ImageName { get; set; }
        public string? OriginalFileName { get; set; }
        public string? ImageURL { get; set; }
    }

    public class PurchaseOrderPaymentReceivedImages_Request : BaseEntity
    {
        [JsonIgnore]
        public int? PurchaseOrderPaymentReceivedId { get; set; }
        public string? ImageFileName { get; set; }
        public string? ImageOriginalFileName { get; set; }
        public string? Image_Base64 { get; set; }
    }

    public class PurchaseOrderPaymentReceivedImage_Search
    {
        [DefaultValue(0)]
        public int? PurchaseOrderPaymentReceivedId { get; set; }
    }

    public class PurchaseOrderPaymentReceivedImages_Response : BaseEntity
    {
        public int? PurchaseOrderPaymentReceivedId { get; set; }
        public string? ImageFileName { get; set; }
        public string? ImageOriginalFileName { get; set; }
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
        public decimal? POAmount { get; set; }
        public decimal? RemainingAmount { get; set; }
        public decimal? TotalReceivedAmount { get; set; }
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
        public string? ContainerCount { get; set; }

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
        public string? ContainerCount { get; set; }

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

    public class Invoice_Search : BaseSearchEntity
    {
        [DefaultValue(0)]
        public int? PurchaseOrderId { get; set; }
    }

    public class Invoice_Request : BaseEntity
    {
        [JsonIgnore]
        public int? PurchaseOrderId { get; set; }
        public DateTime? InvoiceGeneratedDate { get; set; }
        public string? InvoiceNumber { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public decimal? Quantity { get; set; }

        [DefaultValue(0)]
        public decimal? Freight { get; set; }

        [DefaultValue(0)]
        public int? PortId { get; set; }

        [DefaultValue("")]
        public string? SBNo { get; set; }
        public DateTime? SBDate { get; set; }
        public DateTime? LeoDate { get; set; }

        [DefaultValue(0)]
        public decimal? ExchangeRate { get; set; }

        [DefaultValue(0)]
        public decimal? TotalFOBValue { get; set; }

        [DefaultValue(false)]
        public bool? BRCInBank { get; set; }

        [DefaultValue(false)]
        public bool? BRCInDGFT { get; set; }

        [DefaultValue(0)]
        public decimal? FOBPerct { get; set; }

        [DefaultValue(0)]
        public decimal? DBKValue { get; set; }

        [DefaultValue(false)]
        public bool? DBKReceived { get; set; }

        [DefaultValue(0)]
        public decimal? IGSTAmount { get; set; }

        [DefaultValue(false)]
        public bool? IGSTReceived { get; set; }

        [DefaultValue(0)]
        public decimal? CommissionMentionInSBill { get; set; }

        [DefaultValue(0)]
        public decimal? UtilizedAmount { get; set; }

        [DefaultValue(0)]
        public decimal? UnUtilizedAmount { get; set; }

        [DefaultValue("")]
        public string? CommissionRemark { get; set; }

        [DefaultValue(false)]
        public bool? IsCommissionClosed { get; set; }

        [DefaultValue(0)]
        public int? Containers { get; set; }

        [DefaultValue(0)]
        public int? ReuseOrFreshId { get; set; }

        [DefaultValue(0)]
        public int? TransporterId { get; set; }

        [DefaultValue(0)]
        public decimal? Rate { get; set; }

        [DefaultValue(0)]
        public decimal? LandFreight { get; set; }

        [DefaultValue("")]
        public string? TransporterInvoice { get; set; }

        [DefaultValue(0)]
        public int? ForwarderId { get; set; }

        [DefaultValue("")]
        public string? ForwarderInvoice { get; set; }

        [DefaultValue(0)]
        public decimal? SeaFreight { get; set; }

        [DefaultValue(0)]
        public int? CHAId { get; set; }

        [DefaultValue("")]
        public string? ChaInvoice { get; set; }

        [DefaultValue(0)]
        public decimal? Clearing { get; set; }

        [DefaultValue(0)]
        public decimal? CurrentExchangeRate { get; set; }

        [DefaultValue(0)]
        public decimal? DrawBackPerct { get; set; }

        [DefaultValue(0)]
        public decimal? DrawBack_RodTep { get; set; }

        [DefaultValue(0)]
        public decimal? InvoiceAmountInINR { get; set; }

        [DefaultValue(0)]
        public decimal? NetSellRate { get; set; }

        public string? InvoiceImage { get; set; }
        public string? InvoiceOriginalFileName { get; set; }
        public string? Invoice_Base64 { get; set; }

        [JsonIgnore]
        [DefaultValue("Invoice")]
        public string? ModuleType { get; set; }
    }

    public class Invoice_Response : BaseResponseEntity
    {
        [JsonIgnore]
        public int? PurchaseOrderId { get; set; }
        public DateTime? InvoiceGeneratedDate { get; set; }
        public string? InvoiceNumber { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public decimal? Quantity { get; set; }

        public decimal? Freight { get; set; }
        public int? PortId { get; set; }
        public string? PortName { get; set; }
        public string? PortCode { get; set; }
        public string? SBNo { get; set; }
        public DateTime? SBDate { get; set; }
        public DateTime? LeoDate { get; set; }
        public decimal? ExchangeRate { get; set; }
        public decimal? TotalFOBValue { get; set; }
        public bool? BRCInBank { get; set; }
        public bool? BRCInDGFT { get; set; }
        public decimal? FOBPerct { get; set; }
        public decimal? DBKValue { get; set; }
        public bool? DBKReceived { get; set; }
        public decimal? IGSTAmount { get; set; }
        public bool? IGSTReceived { get; set; }
        public decimal? CommissionMentionInSBill { get; set; }
        public decimal? UtilizedAmount { get; set; }
        public decimal? UnUtilizedAmount { get; set; }
        public string? CommissionRemark { get; set; }
        public bool? IsCommissionClosed { get; set; }  
        public int? Containers { get; set; }
        public int? ReuseOrFreshId { get; set; }
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
        public decimal? CurrentExchangeRate { get; set; }
        public decimal? DrawBackPerct { get; set; }
        public decimal? DrawBack_RodTep { get; set; }
        public decimal? InvoiceAmountInINR { get; set; }
        public decimal? NetSellRate { get; set; }

        public string? InvoiceImage { get; set; }
        public string? InvoiceOriginalFileName { get; set; }
        public string? InvoiceImageURL { get; set; }
    }

    #endregion

    #region BI Draft

    public class BIDraftIssuedImages_Search
    {
        [DefaultValue(0)]
        public int? PurchaseOrderId { get; set; }
    }

    public class BIDraftIssuedImages_Request : BaseEntity
    {
        public int? Id { get; set; }

        [JsonIgnore]
        public int? PurchaseOrderId { get; set; }
        public string? ImageName { get; set; }
        public string? OriginalFileName { get; set; }
        public string? Image_Base64 { get; set; }
        public string? Remark { get; set; }
        public int? StatusId { get; set; }
    }

    public class BIDraftIssuedImages_Response : BaseEntity
    {
        public BIDraftIssuedImages_Response()
        {
            BIDraftIssuedLogList = new List<BIDraftIssuedLog_Response>();
        }

        public int? PurchaseOrderId { get; set; }
        public string? ImageName { get; set; }
        public string? OriginalFileName { get; set; }
        public string ImageURL { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public List<BIDraftIssuedLog_Response>? BIDraftIssuedLogList { get; set; }
    }

    public class BIDraftIssuedRemarkLog_Response : BaseResponseEntity
    {
        [JsonIgnore]
        public int? PurchaseOrderId { get; set; }

        public string Remarks { get; set; }
    }

    public class BIDraftIssuedCommentsLog_Response : BaseResponseEntity
    {
        [JsonIgnore]
        public int? PurchaseOrderId { get; set; }

        public string Comments { get; set; }
    }

    public class BIDraftIssuedLog_Search
    {
        [DefaultValue(0)]
        public int? BIDraftIssuedId { get; set; }
    }

    public class BIDraftIssuedLog_Response
    {
        public int? BIDraftIssuedId { get; set; }
        public string? Remarks { get; set; }
        public int? StatusId { get; set; }
        public string? StatusName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string? CreatorName { get; set; }
    }

    #endregion
}
