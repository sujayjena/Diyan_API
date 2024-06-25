using Diyan.Domain.Entities;
using Diyan.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diyan.Application.Models
{
    public class ManageTrackingModel
    {
    }

    public class PurchaseOrderTracking_Request : BaseEntity
    {
        public PurchaseOrderTracking_Request()
        {
            PIIssuedList = new List<PIIssued_Request>();
        }
        public int CustomerId { get; set; }
        public int PortDischargeId { get; set; }
        public bool IsPOReceived { get; set; }
        public string PONumber { get; set; }
        public int PaymentTermsId { get; set; }
        public int Quantity { get; set; }
        public int CurrencyTypeId { get; set; }
        public decimal CurrencyValue { get; set; }
        public int PaperTypeId { get; set; }
        public int BrandId { get; set; }
        public int TypeOfPackagingId { get; set; }
        public int DeliveryTermsId { get; set; }
        public bool IsPOStatusClosed { get; set; }
        public bool IsPIClosed { get; set; }
        public bool IsPIConfirmed { get; set; }
        public string POImage { get; set; }
        public string POImage_Base64 { get; set; }
        public string? POOriginalFileName { get; set; }
        public bool? IsActive { get; set; }

        public List<PIIssued_Request>? PIIssuedList { get; set; }
    }

    public class PurchaseOrderTrackingSearch_Request : BaseSearchEntity
    {
        public int? CustomerId { get; set; }
        public int? CountryId { get; set; }

        [DefaultValue("")]
        public string PONumber { get; set; }
    }

    public class PurchaseOrderTracking_Response : BaseResponseEntity
    {
        public PurchaseOrderTracking_Response()
        {
            PIIssuedList = new List<PIIssued_Response>();
        }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ParentCustomer { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int PortDischargeId { get; set; }
        public string PortDischarge { get; set; }
        public bool IsPOReceived { get; set; }
        public string PONumber { get; set; }
        public int PaymentTermsId { get; set; }
        public string PaymentTerms { get; set; }
        public int Quantity { get; set; }
        public int CurrencyTypeId { get; set; }
        public string CurrencyType { get; set; }
        public decimal CurrencyValue { get; set; }
        public int PaperTypeId { get; set; }
        public string PaperType { get; set; }
        public int BrandId { get; set; }
        public string Brand { get; set; }
        public int TypeOfPackagingId { get; set; }
        public string TypeOfPackaging { get; set; }
        public int DeliveryTermsId { get; set; }
        public string DeliveryTerms { get; set; }
        public bool IsPOStatusClosed { get; set; }
        public bool IsPIClosed { get; set; }
        public bool IsPIConfirmed { get; set; }
        public string POImage { get; set; }
        public string? POOriginalFileName { get; set; }
        public string POImageURL { get; set; }
        public bool? IsActive { get; set; }

        public List<PIIssued_Response>? PIIssuedList { get; set; }
    }

    public class PIIssued_Request : BaseEntity
    {
        public int POTrackingId { get; set; }
        public DateTime PIIssueDate { get; set; }
        public string PINumber { get; set; }
        public string PIImage { get; set; }
        public string PIImage_Base64 { get; set; }
        public string? PIOriginalFileName { get; set; }
        public bool? IsActive { get; set; }
    }

    public class PIIssuedSearch_Request : BaseSearchEntity
    {
        public int? POTrackingId { get; set; }
    }

    public class PIIssued_Response : BaseResponseEntity
    {
        public int POTrackingId { get; set; }
        public DateTime PIIssueDate { get; set; }
        public string PINumber { get; set; }
        public string? PIOriginalFileName { get; set; }
        public string PIImage { get; set; }
        public string PIImageURL { get; set; }
        public bool? IsActive { get; set; }
    }
}
