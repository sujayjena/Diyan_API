using Diyan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diyan.Application.Models
{
    public class TerritoryModel
    {
    }

    #region Country

    public class Country_Request : BaseEntity
    {
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public bool? IsActive { get; set; }
    }

    public class Country_Response : BaseResponseEntity
    {
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public bool? IsActive { get; set; }
    }

    #endregion

    #region State

    public class State_Request : BaseEntity
    {
        public string StateName { get; set; }

        public bool? IsActive { get; set; }
    }

    public class State_Response : BaseResponseEntity
    {
        public string StateName { get; set; }

        public bool? IsActive { get; set; }
    }

    #endregion

    #region District

    public class District_Request : BaseEntity
    {
        public string DistrictName { get; set; }

        public bool? IsActive { get; set; }
    }

    public class District_Response : BaseResponseEntity
    {
        public string DistrictName { get; set; }

        public bool? IsActive { get; set; }
    }

    #endregion

    #region Territories

    public class Territories_Request : BaseEntity
    {
        public int? CountryId { get; set; }

        public int? StateId { get; set; }

        public int? DistrictId { get; set; }

        public bool? IsActive { get; set; }
    }

    public class Territories_Response : BaseResponseEntity
    {
        public int? CountryId { get; set; }

        public string CountryName { get; set; }

        public int? StateId { get; set; }

        public string StateName { get; set; }

        public int? DistrictId { get; set; }

        public string DistrictName { get; set; }

        public bool? IsActive { get; set; }
    }

    #endregion
}
