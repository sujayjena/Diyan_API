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
    public class MIS_PITracking_Search : BasePaninationEntity
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? PaperTypeId { get; set; }
    }

    public class MIS_Search
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
}