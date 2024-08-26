﻿using Diyan.Domain.Entities;
using Diyan.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diyan.Application.Models
{
    public class ProfileModel
    {
    }

    #region Department

    public class Department_Request : BaseEntity
    {
        public string?  DepartmentName { get; set; }

        public bool?  IsActive { get; set; }
    }

    public class Department_Response : BaseResponseEntity
    {
        public string?  DepartmentName { get; set; }

        public bool?  IsActive { get; set; }
    }

    #endregion

    #region Role

    public class Role_Request : BaseEntity
    {
        public string?  RoleName { get; set; }

        public int? DepartmentId { get; set; }

        public bool?  IsActive { get; set; }
    }

    public class Role_Response : BaseResponseEntity
    {
        public string?  RoleName { get; set; }
        public int? DepartmentId { get; set; }
        public string?  DepartmentName { get; set; }

        public bool?  IsActive { get; set; }
    }

    public class RoleSearch_Request : BaseSearchEntity
    {
        [DefaultValue(0)]
        public int? DepartmentId { get; set; }
    }

    #endregion

    #region RoleHierarchy

    public class RoleHierarchy_Request : BaseEntity
    {
        [Required]
        public int? RoleId { get; set; }

        public int? ReportingTo { get; set; }

        public bool?  IsActive { get; set; }
    }

    public class RoleHierarchy_Response : BaseResponseEntity
    {
        public int? RoleId { get; set; }

        public string?  RoleName { get; set; }

        public int? ReportingTo { get; set; }

        public string?  ReportingToName { get; set; }

        public bool?  IsActive { get; set; }
    }

    #endregion

    #region Reporting To 

    public class SelectListResponse
    {
        public long Value { get; set; }
        public string?  Text { get; set; }
    }
    public class ReportingToEmpListParameters
    {
        public long RoleId { get; set; }
        public long? RegionId { get; set; }
    }
    public partial class EmployeesListByReportingTo_Response
    {
        public int? Id { get; set; }
        public string? EmployeeName { get; set; }
        public int? CompanyId { get; set; }
        public string? BranchId { get; set; }
        public int? UserId { get; set; }
    }

    #endregion
}
