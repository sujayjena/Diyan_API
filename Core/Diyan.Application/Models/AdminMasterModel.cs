﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diyan.Domain.Entities;

namespace Diyan.Application.Models
{
    public class AdminMasterModel
    {
    }

    #region Gender

    public class Gender_Request : BaseEntity
    {
        [DefaultValue("")]
        public string?  GenderName { get; set; }

        public bool?  IsActive { get; set; }
    }

    public class Gender_Response : BaseResponseEntity
    {
        public string?  GenderName { get; set; }

        public bool?  IsActive { get; set; }
    }

    #endregion

    #region UOM
    public class UOM_Request : BaseEntity
    {
        [DefaultValue("")]
        public string?  UOMName { get; set; }

        [DefaultValue("")]
        public string?  UOMDesc { get; set; }

        public bool?  IsActive { get; set; }
    }

    public class UOM_Response : BaseResponseEntity
    {
        public string?  UOMName { get; set; }
        public string?  UOMDesc { get; set; }

        public bool?  IsActive { get; set; }
    }

    #endregion

    #region Marital Status
    public class MaritalStatus_Request : BaseEntity
    {
        [DefaultValue("")]
        public string?  MaritalStatus { get; set; }

        public bool?  IsActive { get; set; }
    }

    public class MaritalStatus_Response : BaseResponseEntity
    {
        public string?  MaritalStatus { get; set; }

        public bool?  IsActive { get; set; }
    }

    #endregion

    #region Blood Group
    public class BloodGroup_Request : BaseEntity
    {
        [DefaultValue("")]
        public string?  BloodGroup { get; set; }

        public bool?  IsActive { get; set; }
    }

    public class BloodGroup_Response : BaseResponseEntity
    {
        public string?  BloodGroup { get; set; }

        public bool?  IsActive { get; set; }
    }
    #endregion

    #region Customer Type
    public class CustomerType_Request : BaseEntity
    {
        [DefaultValue("")]
        public string?  CustomerType { get; set; }

        public bool?  IsActive { get; set; }
    }

    public class CustomerType_Response : BaseResponseEntity
    {
        public string?  CustomerType { get; set; }

        public bool?  IsActive { get; set; }
    }
    #endregion

    #region Port Discharge
    public class PortDischarge_Request : BaseEntity
    {
        [DefaultValue("")]
        public string? PortCode { get; set; }

        [DefaultValue("")]
        public string? PortDischarge { get; set; }
        public int? CountryId { get; set; }

        public bool?  IsActive { get; set; }
    }

    public class PortDischarge_Response : BaseResponseEntity
    {
        public string? PortCode { get; set; }
        public string? PortDischarge { get; set; }
        public int? CountryId { get; set; }
        public string? CountryName { get; set; }
        public bool?  IsActive { get; set; }
    }
    #endregion

    #region Paper Type
    public class PaperType_Request : BaseEntity
    {
        [DefaultValue("")]
        public string? PaperType { get; set; }

        public bool?  IsActive { get; set; }
    }

    public class PaperType_Response : BaseResponseEntity
    {
        public string? PaperType { get; set; }
        public bool?  IsActive { get; set; }
    }
    #endregion

    #region Brand
    public class Brand_Request : BaseEntity
    {
        public int? PaperTypeId { get; set; }

        [DefaultValue("")]
        public string? Brand { get; set; }
        public bool?  IsActive { get; set; }
    }

    public class Brand_Response : BaseResponseEntity
    {
        public int? PaperTypeId { get; set; }
        public string? PaperType { get; set; }
        public string? Brand { get; set; }
        public bool?  IsActive { get; set; }
    }
    #endregion

    #region Type of Packaging
    public class TypeOfPackaging_Request : BaseEntity
    {
        [DefaultValue("")]
        public string? TypeOfPackaging { get; set; }
        public bool?  IsActive { get; set; }
    }

    public class TypeOfPackaging_Response : BaseResponseEntity
    {
        public string? TypeOfPackaging { get; set; }
        public bool?  IsActive { get; set; }
    }
    #endregion

    #region Packaging Type
    public class PackagingType_Request : BaseEntity
    {
        [DefaultValue("")]
        public string? PackagingType { get; set; }

        public bool?  IsActive { get; set; }
    }

    public class PackagingType_Response : BaseResponseEntity
    {
        public string? PackagingType { get; set; }
        public bool?  IsActive { get; set; }
    }
    #endregion

    #region Container Type
    public class ContainerType_Request : BaseEntity
    {
        [DefaultValue("")]
        public string? ContainerType { get; set; }

        public bool?  IsActive { get; set; }
    }

    public class ContainerType_Response : BaseResponseEntity
    {
        public string? ContainerType { get; set; }
        public bool?  IsActive { get; set; }
    }
    #endregion

    #region Currency Type
    public class CurrencyType_Request : BaseEntity
    {
        [DefaultValue("")]
        public string? CurrencyType { get; set; }

        public bool?  IsActive { get; set; }
    }

    public class CurrencyType_Response : BaseResponseEntity
    {
        public string? CurrencyType { get; set; }
        public bool?  IsActive { get; set; }
    }
    #endregion

    #region Delivery Terms
    public class DeliveryTerms_Request : BaseEntity
    {
        [DefaultValue("")]
        public string? DeliveryTerms { get; set; }

        public bool?  IsActive { get; set; }
    }

    public class DeliveryTerms_Response : BaseResponseEntity
    {
        public string? DeliveryTerms { get; set; }
        public bool?  IsActive { get; set; }
    }
    #endregion

    #region Payment Terms
    public class PaymentTerms_Request : BaseEntity
    {
        [DefaultValue("")]
        public string? PaymentTerms { get; set; }

        public bool?  IsActive { get; set; }
    }

    public class PaymentTerms_Response : BaseResponseEntity
    {
        public string? PaymentTerms { get; set; }
        public bool?  IsActive { get; set; }
    }
    #endregion

    #region Production Status
    public class ProductionStatus_Request : BaseEntity
    {
        [DefaultValue("")]
        public string? ProductionStatus { get; set; }

        public bool?  IsActive { get; set; }
    }

    public class ProductionStatus_Response : BaseResponseEntity
    {
        public string? ProductionStatus { get; set; }
        public bool?  IsActive { get; set; }
    }
    #endregion

    #region BRC
    public class BRC_Request : BaseEntity
    {
        [DefaultValue("")]
        public string? BRC { get; set; }

        public bool?  IsActive { get; set; }
    }

    public class BRC_Response : BaseResponseEntity
    {
        public string? BRC { get; set; }
        public bool?  IsActive { get; set; }
    }
    #endregion

    #region Forwarding
    public class Forwarding_Request : BaseEntity
    {
        [DefaultValue("")]
        public string? Forwarding { get; set; }

        public bool?  IsActive { get; set; }
    }

    public class Forwarding_Response : BaseResponseEntity
    {
        public string? Forwarding { get; set; }
        public bool?  IsActive { get; set; }
    }
    #endregion

    #region Tracking Status
    public class TrackingStatus_Request : BaseEntity
    {
        [DefaultValue("")]
        public string? TrackingStatus { get; set; }

        public bool?  IsActive { get; set; }
    }

    public class TrackingStatus_Response : BaseResponseEntity
    {
        public string? TrackingStatus { get; set; }
        public bool?  IsActive { get; set; }
    }
    #endregion
}
