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
    public class CustomerModel
    {
    }

    public class Customer_Request : BaseEntity
    {
        public Customer_Request()
        {
            ContactDetailsList = new List<ContactDetails_Request>();
            BillingDetailsList = new List<Billing_ShippingDetails_Request>();
            ShippingDetailsList = new List<Billing_ShippingDetails_Request>();
            //LoginCredntials = new List<LoginCredentials_Request>();
        }

        [DefaultValue("Cust\\Lead")]
        public string? ModuleType { get; set; }
        public string? CustomerName { get; set; }
        public string? MobileNo { get; set; }
        public int? ParentCustomerId { get; set; }
        public string? LandlineNumber { get; set; }
        public int? CustomerTypeId { get; set; }
        public string? EmailId { get; set; }
        public int? CountryId { get; set; }
        public string? ContactName { get; set; }
        public int? LeadStatusId { get; set; }
        public bool? IsActive { get; set; }
        public List<ContactDetails_Request>? ContactDetailsList { get; set; }
        public List<Billing_ShippingDetails_Request>? BillingDetailsList { get; set; }
        public List<Billing_ShippingDetails_Request>? ShippingDetailsList { get; set; }
        //public List<LoginCredentials_Request>? LoginCredntials { get; set; }
    }

    public class ContactDetails_Request : BaseEntity
    {
        public int? CustomerId { get; set; }
        public string? ContactPerson { get; set; }
        public string? MobileNo { get; set; }
        [JsonIgnore]
        public string? AlternetMobileNo { get; set; }
        public string? EmailId { get; set; }
        [JsonIgnore]
        public string? AlternetEmailId { get; set; }
        [JsonIgnore]
        public string? Designation { get; set; }
        public bool? IsActive { get; set; }
    }


    public class Billing_ShippingDetails_Request : BaseEntity
    {
        public int? CustomerId { get; set; }
        public string? StreetName { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public string? PostalZipCodde { get; set; }
        public bool? IsActive { get; set; }
    }

    public class LoginCredentials_Request : BaseEntity
    {
        public int? CustomerId { get; set; }
        public string? ContactName { get; set; }
        public string? Username { get; set; }
        public string? Passwords { get; set; }
        public bool? IsActive { get; set; }
    }

    public class Customer_Response : BaseResponseEntity
    {
        //public Customer_Response()
        //{
        //    ContactDetailsList = new List<ContactDetails_Response>();
        //    BillingDetailsList = new List<Billing_ShippingDetails_Response>();
        //    ShippingDetailsList = new List<Billing_ShippingDetails_Response>();
        //    LoginCredntials = new List<LoginCredentials_Response>();
        //}

        public string? CustomerName { get; set; }
        public string? MobileNo { get; set; }
        public int? ParentCustomerId { get; set; }
        public string? ParentCustomer { get; set; }
        public string? LandlineNumber { get; set; }
        public int? CustomerTypeId { get; set; }
        public string? CustomerType { get; set; }
        public string? EmailId { get; set; }
        public string? Passwords { get; set; }
        public int? CountryId { get; set; }
        public string? CountryName { get; set; }
        public string? CountryCode { get; set; }
        public string? ContactName { get; set; }
        public int? LeadStatusId { get; set; }
        public string? LeadStatus { get; set; }
        public bool? IsActive { get; set; }

        //public List<ContactDetails_Response>? ContactDetailsList { get; set; }
        //public List<Billing_ShippingDetails_Response>? BillingDetailsList { get; set; }
        //public List<Billing_ShippingDetails_Response>? ShippingDetailsList { get; set; }
        //public List<LoginCredentials_Response>? LoginCredntials { get; set; }
    }

    public class ContactDetails_Response : BaseEntity
    {
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? ContactPerson { get; set; }
        public string? MobileNo { get; set; }
        [JsonIgnore]
        public string? AlternetMobileNo { get; set; }
        public string? EmailId { get; set; }
        [JsonIgnore]
        public string? AlternetEmailId { get; set; }
        [JsonIgnore]
        public string? Designation { get; set; }
        public bool? IsActive { get; set; }
    }

    public class CustomerSearch_Request : BaseSearchEntity
    {
        [DefaultValue(0)]
        public int CountryId { get; set; }
        [DefaultValue(0)]
        public int CustomerId { get; set; }
        [DefaultValue(0)]
        public int LeadStatusId { get; set; }
        [DefaultValue(0)]
        public int ParentCustomerId { get; set; }
    }

    public class Search_Request : BaseSearchEntity
    {
        [DefaultValue(0)]
        public int CustomerId { get; set; }
    }

    public class Billing_ShippingDetails_Response : BaseEntity
    {
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? StreetName { get; set; }
        public int? CountryId { get; set; }
        public string? CountryName { get; set; }
        public int? StateId { get; set; }
        public string? StateName { get; set; }
        public string? PostalZipCodde { get; set; }
        public bool? IsActive { get; set; }
    }

    public class LoginCredentials_Response : BaseEntity
    {
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? ContactName { get; set; }
        public string? Username { get; set; }
        public string? Passwords { get; set; }
        public bool? IsActive { get; set; }
    }

    public class CustomerApproveNReject_Request
    {
        public int? CustomerId { get; set; }
        public int? LeadStatusId { get; set; }
    }
}

