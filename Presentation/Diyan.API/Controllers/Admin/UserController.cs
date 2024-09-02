using Diyan.Application.Enums;
using Diyan.Application.Helpers;
using Diyan.Application.Interfaces;
using Diyan.Application.Models;
using Diyan.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Diyan.API.Middlewares;
using OfficeOpenXml.Style;
using OfficeOpenXml;

namespace Diyan.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IBranchRepository _branchRepository;
        private IFileManager _fileManager;

        public UserController(IUserRepository userRepository, ICompanyRepository companyRepository, IBranchRepository branchRepository, IFileManager fileManager)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _branchRepository = branchRepository;
            _fileManager = fileManager;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region User 

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveUser(User_Request parameters)
        {
            #region User Restriction 

            int vCompanyNoofUserAdd = 0;
            int vBranchNoofUserAdd = 0;

            int totalCompanyRegisteredUser = 0;
            //int totalBranchRegisteredUser = 0;

            if (parameters.Id == 0)
            {
                var baseSearch = new BaseSearchEntity();
                var vUser = await _userRepository.GetUserList(baseSearch);

                #region Company Wise User Check

                if (parameters.CompanyId > 0)
                {
                    var vCompany = await _companyRepository.GetCompanyById(parameters.CompanyId);
                    if (vCompany != null)
                    {
                        vCompanyNoofUserAdd = vCompany.NoofUserAdd ?? 0;
                    }
                }

                if (parameters.CompanyId > 0 && parameters.BranchList.Count == 0)
                {
                    //get total company user
                    totalCompanyRegisteredUser = vUser.Where(x => x.IsActive == true && x.CompanyId == parameters.CompanyId).Count();

                    // Total Company User check with register user
                    if (totalCompanyRegisteredUser >= vCompanyNoofUserAdd)
                    {
                        _response.IsSuccess = false;
                        _response.Message = "You are not allowed to create user more then " + vCompanyNoofUserAdd + ", Please contact your administrator to access this feature!";
                        
                        return _response;
                    }
                }

                #endregion

                #region Company and Branch Wise User Check

                List<string> strBranchList = new List<string>();

                if (parameters.CompanyId > 0 && parameters.BranchList.Count > 0)
                {
                    foreach (var vBranchitem in parameters.BranchList)
                    {
                        var vBranchMappingObj = await _branchRepository.GetBranchMappingByEmployeeId(0, Convert.ToInt32(vBranchitem.BranchId));

                        var vBranchObj = await _branchRepository.GetBranchById(Convert.ToInt32(vBranchitem.BranchId));

                        if (vBranchMappingObj.Count() >= vCompanyNoofUserAdd)
                        {
                            strBranchList.Add(vBranchObj != null ? vBranchObj.BranchName : string.Empty);
                        }
                    }

                    if (strBranchList.Count > 0)
                    {
                        string sbranchListCommaseparated = string.Join(", ", strBranchList);

                        _response.IsSuccess = false;
                        _response.Message = "You are not allowed to create user more then " + vCompanyNoofUserAdd + " for branch " + sbranchListCommaseparated + ", Please contact your administrator to access this feature!";
                        
                        return _response;
                    }
                }

                #endregion
            }

            #endregion

            // Aadhar Card Upload
            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.AadharImage_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.AadharImage_Base64, "\\Uploads\\Employee\\", parameters.AadharOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.AadharImage = vUploadFile;
                }
            }

            // Pan Card Upload
            if (parameters != null && !string.IsNullOrWhiteSpace(parameters.PanCardImage_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.PanCardImage_Base64, "\\Uploads\\Employee\\", parameters.PanCardOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.PanCardImage = vUploadFile;
                }
            }

            // Profile Upload
            if (parameters != null && !string.IsNullOrWhiteSpace(parameters.ProfileImage_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.ProfileImage_Base64, "\\Uploads\\Employee\\", parameters.ProfileOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.ProfileImage = vUploadFile;
                }
            }

            // Other Proof Upload
            if (parameters != null && !string.IsNullOrWhiteSpace(parameters.OtherProofImage_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.OtherProofImage_Base64, "\\Uploads\\Employee\\", parameters.OtherProofOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.OtherProofImage = vUploadFile;
                }
            }

            int result = await _userRepository.SaveUser(parameters);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.ReocrdExists)
            {
                _response.Message = "Record is already exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record details saved sucessfully";

                #region // Add/Update Branch Mapping

                // Delete Old mapping of employee

                var vBracnMapDELETEObj = new BranchMapping_Request()
                {
                    Action = "DELETE",
                    UserId = result,
                    BranchId = 0
                };
                int resultBranchMappingDELETE = await _branchRepository.SaveBranchMapping(vBracnMapDELETEObj);


                // Add new mapping of employee
                foreach (var vBranchitem in parameters.BranchList)
                {
                    var vBracnMapObj = new BranchMapping_Request()
                    {
                        Action = "INSERT",
                        UserId = result,
                        BranchId = vBranchitem.BranchId
                    };

                    int resultBranchMapping = await _branchRepository.SaveBranchMapping(vBracnMapObj);
                }

                // Add new mapping of employee
                if (result > 0)
                {
                    foreach (var items in parameters.UserOtherDetailsList)
                    {
                        var vUserOtherDetails = new UserOtherDetails_Request()
                        {
                            Id = items.Id,
                            EmployeeId = result,
                            PastCompanyName = items.PastCompanyName,
                            TotalExp = items.TotalExp,
                            Remark = items.Remark
                        };

                        int resultUserOtherDetails = await _userRepository.SaveUserOtherDetails(vUserOtherDetails);
                    }
                }

                #endregion
            }
            return _response;
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetUserList(BaseSearchEntity parameters)
        {
            IEnumerable<User_Response> lstUsers = await _userRepository.GetUserList(parameters);
            _response.Data = lstUsers.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetUserById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _userRepository.GetUserById(Id);

                if (vResultObj != null)
                {
                    var vBranchMappingObj = await _branchRepository.GetBranchMappingByEmployeeId(vResultObj.Id, 0);

                    foreach (var item in vBranchMappingObj)
                    {
                        var vBranchObj = await _branchRepository.GetBranchById(Convert.ToInt32(item.BranchId));
                        var vBrMapResOnj = new BranchMapping_Response()
                        {
                            Id = item.Id,
                            UserId = vResultObj.Id,
                            BranchId = item.BranchId,
                            BranchName = vBranchObj != null ? vBranchObj.BranchName : string.Empty,
                        };

                        vResultObj.BranchList.Add(vBrMapResOnj);
                    }

                    //get user other details
                    var vUserOtherDetailsObj = await _userRepository.GetUserOtherDetailsByEmployeeId(vResultObj.Id);
                    foreach (var item in vUserOtherDetailsObj)
                    {
                        var vUserOtherDetailsResObj = new UserOtherDetails_Response()
                        {
                            Id = item.Id,
                            EmployeeId = vResultObj.Id,
                            PastCompanyName = item.PastCompanyName,
                            TotalExp = item.TotalExp,
                            Remark = item.Remark
                        };

                        vResultObj.UserOtherDetailsList.Add(vUserOtherDetailsResObj);
                    }
                }
                _response.Data = vResultObj;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportUserData(bool IsActive = true)
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var request = new BaseSearchEntity();
            request.IsActive = IsActive;

            IEnumerable<User_Response> lstSizeObj = await _userRepository.GetUserList(request);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("Employee");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "User Code";
                    WorkSheet1.Cells[1, 2].Value = "User Name";
                    WorkSheet1.Cells[1, 3].Value = "EmailId";
                    WorkSheet1.Cells[1, 4].Value = "Mobile";
                    WorkSheet1.Cells[1, 5].Value = "Company";
                    WorkSheet1.Cells[1, 6].Value = "Branch";
                    WorkSheet1.Cells[1, 7].Value = "Department";
                    WorkSheet1.Cells[1, 8].Value = "Role";
                    WorkSheet1.Cells[1, 9].Value = "ReportingTo";
                    WorkSheet1.Cells[1, 10].Value = "DateOfBirth";
                    WorkSheet1.Cells[1, 11].Value = "Date Of Joining";
                    WorkSheet1.Cells[1, 12].Value = "Blood Group";
                    WorkSheet1.Cells[1, 13].Value = "Gender";
                    WorkSheet1.Cells[1, 14].Value = "Marital Status";
                    WorkSheet1.Cells[1, 15].Value = "Company Number";
                    WorkSheet1.Cells[1, 16].Value = "Permanent Address";
                    WorkSheet1.Cells[1, 17].Value = "Country";
                    WorkSheet1.Cells[1, 18].Value = "State";
                    WorkSheet1.Cells[1, 19].Value = "District";
                    WorkSheet1.Cells[1, 20].Value = "Pincode";
                    WorkSheet1.Cells[1, 21].Value = "Temporary Address";
                    WorkSheet1.Cells[1, 22].Value = "Country";
                    WorkSheet1.Cells[1, 23].Value = "State";
                    WorkSheet1.Cells[1, 24].Value = "District";
                    WorkSheet1.Cells[1, 25].Value = "Pincode";
                    WorkSheet1.Cells[1, 26].Value = "Emergency Name";
                    WorkSheet1.Cells[1, 27].Value = "Emergency Contact";
                    WorkSheet1.Cells[1, 28].Value = "Emergency Relation";
                    WorkSheet1.Cells[1, 29].Value = "Aadhar Number";
                    WorkSheet1.Cells[1, 30].Value = "Pan Number";
                    WorkSheet1.Cells[1, 31].Value = "Other Proof";
                    WorkSheet1.Cells[1, 32].Value = "IsMobileUser";
                    WorkSheet1.Cells[1, 33].Value = "IsWebUser";
                    WorkSheet1.Cells[1, 34].Value = "IsActive";

                    recordIndex = 2;

                    foreach (var items in lstSizeObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.UserCode;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.UserName;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.EmailId;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.MobileNumber;

                        WorkSheet1.Cells[recordIndex, 5].Value = items.CompanyName;
                        WorkSheet1.Cells[recordIndex, 6].Value = items.BranchName;
                        WorkSheet1.Cells[recordIndex, 7].Value = items.DepartmentName;
                        WorkSheet1.Cells[recordIndex, 8].Value = items.RoleName;
                        WorkSheet1.Cells[recordIndex, 9].Value = items.ReportingToName;

                        WorkSheet1.Cells[recordIndex, 10].Value = items.DateOfBirth.HasValue ? items.DateOfBirth.Value.ToString("dd/MM/yyyy") : string.Empty;
                        WorkSheet1.Cells[recordIndex, 11].Value = items.DateOfJoining.HasValue ? items.DateOfJoining.Value.ToString("dd/MM/yyyy") : string.Empty;

                        WorkSheet1.Cells[recordIndex, 12].Value = items.BloodGroup;
                        WorkSheet1.Cells[recordIndex, 13].Value = items.GenderName;
                        WorkSheet1.Cells[recordIndex, 14].Value = items.MaritalStatus;
                        WorkSheet1.Cells[recordIndex, 15].Value = items.CompanyNumber;

                        WorkSheet1.Cells[recordIndex, 16].Value = items.AddressLine;
                        WorkSheet1.Cells[recordIndex, 17].Value = items.CountryName;
                        WorkSheet1.Cells[recordIndex, 18].Value = items.StateName;
                        WorkSheet1.Cells[recordIndex, 19].Value = items.DistrictName;
                        WorkSheet1.Cells[recordIndex, 20].Value = items.Pincode;

                        WorkSheet1.Cells[recordIndex, 21].Value = items.AddressLine;
                        WorkSheet1.Cells[recordIndex, 22].Value = items.CountryName;
                        WorkSheet1.Cells[recordIndex, 23].Value = items.StateName;
                        WorkSheet1.Cells[recordIndex, 24].Value = items.DistrictName;
                        WorkSheet1.Cells[recordIndex, 25].Value = items.Pincode;

                        WorkSheet1.Cells[recordIndex, 26].Value = items.EmergencyName;
                        WorkSheet1.Cells[recordIndex, 27].Value = items.EmergencyContactNumber;
                        WorkSheet1.Cells[recordIndex, 28].Value = items.EmergencyRelation;

                        WorkSheet1.Cells[recordIndex, 29].Value = items.AadharNumber;
                        WorkSheet1.Cells[recordIndex, 30].Value = items.PanNumber;
                        WorkSheet1.Cells[recordIndex, 31].Value = items.OtherProof;

                        WorkSheet1.Cells[recordIndex, 32].Value = items.IsMobileUser;
                        WorkSheet1.Cells[recordIndex, 33].Value = items.IsWebUser;
                        WorkSheet1.Cells[recordIndex, 34].Value = items.IsActive == true ? "Active" : "Inactive";

                        recordIndex += 1;
                    }

                    WorkSheet1.Columns.AutoFit();

                    excelExportData.SaveAs(msExportDataFile);
                    msExportDataFile.Position = 0;
                    result = msExportDataFile.ToArray();
                }
            }

            if (result != null)
            {
                _response.Data = result;
                _response.IsSuccess = true;
                _response.Message = "Exported successfully";
            }

            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportUserHistoryData(bool IsActive = false)
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var request = new BaseSearchEntity();
            request.IsActive = IsActive;

            IEnumerable<User_Response> lstSizeObj = await _userRepository.GetUserList(request);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("Employee");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "User Code";
                    WorkSheet1.Cells[1, 2].Value = "User Name";
                    WorkSheet1.Cells[1, 3].Value = "EmailId";
                    WorkSheet1.Cells[1, 4].Value = "Mobile";
                    WorkSheet1.Cells[1, 5].Value = "Company";
                    WorkSheet1.Cells[1, 6].Value = "Branch";
                    WorkSheet1.Cells[1, 7].Value = "Department";
                    WorkSheet1.Cells[1, 8].Value = "Role";
                    WorkSheet1.Cells[1, 9].Value = "ReportingTo";
                    WorkSheet1.Cells[1, 10].Value = "DateOfBirth";
                    WorkSheet1.Cells[1, 11].Value = "Date Of Joining";
                    WorkSheet1.Cells[1, 12].Value = "Blood Group";
                    WorkSheet1.Cells[1, 13].Value = "Gender";
                    WorkSheet1.Cells[1, 14].Value = "Marital Status";
                    WorkSheet1.Cells[1, 15].Value = "Company Number";
                    WorkSheet1.Cells[1, 16].Value = "Permanent Address";
                    WorkSheet1.Cells[1, 17].Value = "Country";
                    WorkSheet1.Cells[1, 18].Value = "State";
                    WorkSheet1.Cells[1, 19].Value = "District";
                    WorkSheet1.Cells[1, 20].Value = "Pincode";
                    WorkSheet1.Cells[1, 21].Value = "Temporary Address";
                    WorkSheet1.Cells[1, 22].Value = "Country";
                    WorkSheet1.Cells[1, 23].Value = "State";
                    WorkSheet1.Cells[1, 24].Value = "District";
                    WorkSheet1.Cells[1, 25].Value = "Pincode";
                    WorkSheet1.Cells[1, 26].Value = "Emergency Name";
                    WorkSheet1.Cells[1, 27].Value = "Emergency Contact";
                    WorkSheet1.Cells[1, 28].Value = "Emergency Relation";
                    WorkSheet1.Cells[1, 29].Value = "Aadhar Number";
                    WorkSheet1.Cells[1, 30].Value = "Pan Number";
                    WorkSheet1.Cells[1, 31].Value = "Other Proof";
                    WorkSheet1.Cells[1, 32].Value = "IsMobileUser";
                    WorkSheet1.Cells[1, 33].Value = "IsWebUser";
                    WorkSheet1.Cells[1, 34].Value = "IsActive";

                    recordIndex = 2;

                    foreach (var items in lstSizeObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.UserCode;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.UserName;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.EmailId;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.MobileNumber;

                        WorkSheet1.Cells[recordIndex, 5].Value = items.CompanyName;
                        WorkSheet1.Cells[recordIndex, 6].Value = items.BranchName;
                        WorkSheet1.Cells[recordIndex, 7].Value = items.DepartmentName;
                        WorkSheet1.Cells[recordIndex, 8].Value = items.RoleName;
                        WorkSheet1.Cells[recordIndex, 9].Value = items.ReportingToName;

                        WorkSheet1.Cells[recordIndex, 10].Value = items.DateOfBirth.HasValue ? items.DateOfBirth.Value.ToString("dd/MM/yyyy") : string.Empty;
                        WorkSheet1.Cells[recordIndex, 11].Value = items.DateOfJoining.HasValue ? items.DateOfJoining.Value.ToString("dd/MM/yyyy") : string.Empty;

                        WorkSheet1.Cells[recordIndex, 12].Value = items.BloodGroup;
                        WorkSheet1.Cells[recordIndex, 13].Value = items.GenderName;
                        WorkSheet1.Cells[recordIndex, 14].Value = items.MaritalStatus;
                        WorkSheet1.Cells[recordIndex, 15].Value = items.CompanyNumber;

                        WorkSheet1.Cells[recordIndex, 16].Value = items.AddressLine;
                        WorkSheet1.Cells[recordIndex, 17].Value = items.CountryName;
                        WorkSheet1.Cells[recordIndex, 18].Value = items.StateName;
                        WorkSheet1.Cells[recordIndex, 19].Value = items.DistrictName;
                        WorkSheet1.Cells[recordIndex, 20].Value = items.Pincode;

                        WorkSheet1.Cells[recordIndex, 21].Value = items.AddressLine;
                        WorkSheet1.Cells[recordIndex, 22].Value = items.CountryName;
                        WorkSheet1.Cells[recordIndex, 23].Value = items.StateName;
                        WorkSheet1.Cells[recordIndex, 24].Value = items.DistrictName;
                        WorkSheet1.Cells[recordIndex, 25].Value = items.Pincode;

                        WorkSheet1.Cells[recordIndex, 26].Value = items.EmergencyName;
                        WorkSheet1.Cells[recordIndex, 27].Value = items.EmergencyContactNumber;
                        WorkSheet1.Cells[recordIndex, 28].Value = items.EmergencyRelation;

                        WorkSheet1.Cells[recordIndex, 29].Value = items.AadharNumber;
                        WorkSheet1.Cells[recordIndex, 30].Value = items.PanNumber;
                        WorkSheet1.Cells[recordIndex, 31].Value = items.OtherProof;

                        WorkSheet1.Cells[recordIndex, 32].Value = items.IsMobileUser;
                        WorkSheet1.Cells[recordIndex, 33].Value = items.IsWebUser;
                        WorkSheet1.Cells[recordIndex, 34].Value = items.IsActive == true ? "Active" : "Inactive";

                        recordIndex += 1;
                    }

                    WorkSheet1.Columns.AutoFit();

                    excelExportData.SaveAs(msExportDataFile);
                    msExportDataFile.Position = 0;
                    result = msExportDataFile.ToArray();
                }
            }

            if (result != null)
            {
                _response.Data = result;
                _response.IsSuccess = true;
                _response.Message = "Exported successfully";
            }

            return _response;
        }

        #endregion

        #region User Other Details

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveUserOtherDetails(UserOtherDetails_Request parameters)
        {
            int result = await _userRepository.SaveUserOtherDetails(parameters);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.ReocrdExists)
            {
                _response.Message = "Record is already exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record details saved sucessfully";
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetUserOtherDetailsById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _userRepository.GetUserOtherDetailsById(Id);

                _response.Data = vResultObj;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> DeleteUserOtherDetails(int Id)
        {
            int result = await _userRepository.DeleteUserOtherDetails(Id);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record details deleted sucessfully";
            }
            return _response;
        }

        #endregion
    }
}
