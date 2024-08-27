using Diyan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diyan.Persistence.Repositories;

namespace Diyan.Application.Models
{
    public class CompanySearch_Request : BaseSearchEntity
    {
        [DefaultValue(0)]
        public int CompanyId { get; set; }
    }
    public class CompanyAMC_Search
    {
        [DefaultValue(0)]
        public int CompanyId { get; set; }
    }
    public class Company_Request : BaseEntity
    {
        public string? CompanyName { get; set; }
        public int? CompanyTypeId { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? TaxNumber { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public int? DistrictId { get; set; }
        public string? Pincode { get; set; }
        public string? GSTNumber { get; set; }
        public string? PANNumber { get; set; }
        public string? CompanyLogoFileName { get; set; }
        public string? CompanyLogo_Base64 { get; set; }
        public int? NoofUserAdd { get; set; }
        public int? NoofBranchAdd { get; set; }
        public int? AmcMonth { get; set; }
        public DateTime? AmcStartDate { get; set; }
        public DateTime? AmcEndDate { get; set; }
        public bool? IsActive { get; set; }
    }

    public class Company_Response : BaseResponseEntity
    {
        public string? CompanyName { get; set; }
        public int? CompanyTypeId { get; set; }
        public string? CompanyType { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? TaxNumber { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public int? CountryId { get; set; }
        public string? CountryName { get; set; }
        public int? StateId { get; set; }
        public string? StateName { get; set; }
        public int? DistrictId { get; set; }
        public string? DistrictName { get; set; }
        public string? Pincode { get; set; }
        public string? GSTNumber { get; set; }
        public string? PANNumber { get; set; }
        public string? CompanyLogo { get; set; }
        public string? CompanyLogoURL { get; set; }
        public int? NoofUserAdd { get; set; }
        public int? NoofBranchAdd { get; set; }
        public int? AmcMonth { get; set; }
        public DateTime? AmcStartDate { get; set; }
        public DateTime? AmcEndDate { get; set; }
        public DateTime? AmcLastEmailDate { get; set; }
        public int? TotalAmcDays { get; set; }
        public int? TotalAmcRemainingDays { get; set; }
        public bool? IsActive { get; set; }
    }
    public class CompanyAMCRminderEmail_Request : BaseEntity
    {
        public int? CompanyId { get; set; }
        public string AMCYear { get; set; }
        public string AMCStartDate_EndDate_LastEmailDate { get; set; }

        [DefaultValue(0)]
        public int? AMCRemainingDays { get; set; }

        [DefaultValue(0)]
        public int? AMCReminderCount { get; set; }

        [DefaultValue(false)]
        public bool? AMCPreorPostExpire { get; set; }
        public DateTime? AmcEndDate { get; set; }
        public DateTime? AmcLastEmailDate { get; set; }
    }
}
