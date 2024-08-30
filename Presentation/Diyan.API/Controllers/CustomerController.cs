using Diyan.API.CustomAttributes;
using Diyan.Application.Enums;
using Diyan.Application.Helpers;
using Diyan.Application.Interfaces;
using Diyan.Application.Models;
using Diyan.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Collections.Generic;

namespace Diyan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region Customer 

        [Route("[action]")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseModel> SaveCustomer(Customer_Request parameters)
        {
            bool bIsCustomerDeleted = false;
            int result = await _customerRepository.SaveCustomer(parameters);

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
            else if (result == -3)
            {
                _response.Message = "Mobile Number is exists";
            }
            else if (result == -4)
            {
                _response.Message = "Email is exists";
            }
            else
            {
                _response.Message = "Record details saved sucessfully";

                if (result > 0 && parameters.ModuleType == "Cust")
                {
                    string strContactErrorMsg = "";
                    string strLoginErrorMsg = "";

                    // Add data into Contact details
                    foreach (var items in parameters.ContactDetailsList)
                    {
                        var vContactDetails = new ContactDetails_Request()
                        {
                            Id = items.Id,
                            CustomerId = result,
                            ContactPerson = items.ContactPerson,
                            MobileNo = items.MobileNo,
                            EmailId = items.EmailId,
                            IsActive = items.IsActive,
                        };

                        int resultContactDetails = await _customerRepository.SaveContactDetails(vContactDetails);

                        if (resultContactDetails == (int)SaveOperationEnums.NoRecordExists)
                        {
                            strContactErrorMsg = "No record exists in Contact detail";
                        }
                        else if (resultContactDetails == (int)SaveOperationEnums.ReocrdExists)
                        {
                            strContactErrorMsg = "Record is already exists in Contact detail";
                        }
                        else if (resultContactDetails == (int)SaveOperationEnums.NoResult)
                        {
                            strContactErrorMsg = "Something went wrong in Contact detail, please try again";
                        }
                        else if (resultContactDetails == -3)
                        {
                            strContactErrorMsg = "Mobile Number is exists in Contact detail";
                        }

                        if (!string.IsNullOrWhiteSpace(strContactErrorMsg))
                        {
                            _response.Message = strContactErrorMsg;

                            int resultDeleteCustomer = await _customerRepository.DeleteCustomer(result);
                            if (resultDeleteCustomer > 0)
                            {
                                bIsCustomerDeleted = true;
                            }
                        }
                    }

                    if (bIsCustomerDeleted == false)
                    {
                        // Add data into Billing details
                        foreach (var items in parameters.BillingDetailsList)
                        {
                            var vBillingDetails = new Billing_ShippingDetails_Request()
                            {
                                Id = items.Id,
                                CustomerId = result,
                                StreetName = items.StreetName,
                                CountryId = items.CountryId,
                                StateId = items.StateId,
                                PostalZipCodde = items.PostalZipCodde,
                                IsActive = items.IsActive,
                            };

                            int resultBillingDetails = await _customerRepository.SaveBillingDetails(vBillingDetails);
                        }

                        // Add data into Shipping details
                        foreach (var items in parameters.ShippingDetailsList)
                        {
                            var vShippingDetails = new Billing_ShippingDetails_Request()
                            {
                                Id = items.Id,
                                CustomerId = result,
                                StreetName = items.StreetName,
                                CountryId = items.CountryId,
                                StateId = items.StateId,
                                PostalZipCodde = items.PostalZipCodde,
                                IsActive = items.IsActive,
                            };

                            int resultShippingDetails = await _customerRepository.SaveShippingDetails(vShippingDetails);
                        }

                        //// Add data into Login Credentials details
                        //foreach (var items in parameters.LoginCredntials)
                        //{
                        //    var vLoginCredentials = new LoginCredentials_Request()
                        //    {
                        //        Id = items.Id,
                        //        CustomerId = result,
                        //        Username = items.Username,
                        //        Passwords = items.Passwords,
                        //        IsActive = items.IsActive,
                        //    };

                        //    int resultLoginCredentials = await _customerRepository.SaveLoginCredentials(vLoginCredentials);
                        //}
                    }
                }
            }

            if (bIsCustomerDeleted == true)
            {
                _response.Id = 0;
            }

            return _response;
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetCustomerList(CustomerSearch_Request parameters)
        {
            IEnumerable<Customer_Response> lstCustomers = await _customerRepository.GetCustomerList(parameters);
            _response.Data = lstCustomers.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetCustomerById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _customerRepository.GetCustomerById(Id);

                //if (vResultObj != null)
                //{
                //    //get Contact details
                //    var vSearch_Request = new Search_Request()
                //    {
                //        CustomerId = Convert.ToInt32(vResultObj.Id),
                //    };

                //    var vContactDetailsListObj = await _customerRepository.GetContactDetailsList(vSearch_Request);
                //    foreach (var item in vContactDetailsListObj)
                //    {
                //        var vContactDetailsObj = new ContactDetails_Response()
                //        {
                //            Id = item.Id,
                //            CustomerId = vResultObj.Id,
                //            ContactPerson = item.ContactPerson,
                //            MobileNo = item.MobileNo,
                //            EmailId = item.EmailId,
                //            IsActive = item.IsActive,
                //        };

                //        vResultObj.ContactDetailsList.Add(vContactDetailsObj);
                //    }

                //    //get Billing details
                //    var vBillingDetailsListObj = await _customerRepository.GetBillingDetailsList(vSearch_Request);
                //    foreach (var item in vBillingDetailsListObj)
                //    {
                //        var vBillingDetailsObj = new Billing_ShippingDetails_Response()
                //        {
                //            Id = item.Id,
                //            CustomerId = vResultObj.Id,
                //            StreetName = item.StreetName,
                //            CountryId = item.CountryId,
                //            CountryName = item.CountryName,
                //            StateId = item.StateId,
                //            StateName = item.StateName,
                //            PostalZipCodde = item.PostalZipCodde,
                //            IsActive = item.IsActive,
                //        };

                //        vResultObj.BillingDetailsList.Add(vBillingDetailsObj);
                //    }

                //    //get Shipping details
                //    var vShippingDetailsListObj = await _customerRepository.GetShippingDetailsList(vSearch_Request);
                //    foreach (var item in vShippingDetailsListObj)
                //    {
                //        var vShippingDetailsObj = new Billing_ShippingDetails_Response()
                //        {
                //            Id = item.Id,
                //            CustomerId = vResultObj.Id,
                //            StreetName = item.StreetName,
                //            CountryId = item.CountryId,
                //            CountryName = item.CountryName,
                //            StateId = item.StateId,
                //            StateName = item.StateName,
                //            PostalZipCodde = item.PostalZipCodde,
                //            IsActive = item.IsActive,
                //        };

                //        vResultObj.ShippingDetailsList.Add(vShippingDetailsObj);
                //    }

                //    //get Login Credentials
                //    var vLoginCredentialsListObj = await _customerRepository.GetLoginCredentialsList(vSearch_Request);
                //    foreach (var item in vLoginCredentialsListObj)
                //    {
                //        var vLoginCredentialsObj = new LoginCredentials_Response()
                //        {
                //            Id = item.Id,
                //            CustomerId = vResultObj.Id,
                //            CustomerName = item.CustomerName,
                //            Username = item.Username,
                //            Passwords = item.Passwords,
                //            IsActive = item.IsActive,
                //        };

                //        vResultObj.LoginCredntials.Add(vLoginCredentialsObj);
                //    }
                //}
                _response.Data = vResultObj;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> CustomerApproveNReject(CustomerApproveNReject_Request parameters)
        {
            int result = await _customerRepository.CustomerApproveNReject(parameters);

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
                if (parameters.LeadStatusId == 2)
                {
                    _response.Message = "Lead accepted successfully.";
                }
                else if (parameters.LeadStatusId == 3)
                {
                    _response.Message = "Lead rejected successfully .";
                }
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportCustomer()
        {
            _response.IsSuccess = false;
            byte[] result;

            var parameters = new CustomerSearch_Request();
            IEnumerable<Customer_Response> lstCustomerListObj = await _customerRepository.GetCustomerList(parameters);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    int recordIndex;
                    ExcelWorksheet WorkSheet1 = excelExportData.Workbook.Worksheets.Add("Customer");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Customer Name";
                    WorkSheet1.Cells[1, 2].Value = "Parent Customer";
                    WorkSheet1.Cells[1, 3].Value = "Mobile Number";
                    WorkSheet1.Cells[1, 4].Value = "LandLine Number";
                    WorkSheet1.Cells[1, 5].Value = "Customer Type";
                    WorkSheet1.Cells[1, 6].Value = "Email";
                    WorkSheet1.Cells[1, 7].Value = "Country Name";
                    WorkSheet1.Cells[1, 8].Value = "Country Code";
                    WorkSheet1.Cells[1, 9].Value = "Contact Name";
                    WorkSheet1.Cells[1, 10].Value = "IsActive";


                    recordIndex = 2;
                    foreach (var items in lstCustomerListObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.CustomerName;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.ParentCustomer;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.MobileNo;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.LandlineNumber;
                        WorkSheet1.Cells[recordIndex, 5].Value = items.CustomerType;
                        WorkSheet1.Cells[recordIndex, 6].Value = items.EmailId;
                        WorkSheet1.Cells[recordIndex, 7].Value = items.CountryName;
                        WorkSheet1.Cells[recordIndex, 8].Value = items.CountryCode;
                        WorkSheet1.Cells[recordIndex, 9].Value = items.ContactName;
                        WorkSheet1.Cells[recordIndex, 10].Value = items.IsActive == true ? "Active" : "Inactive";

                        recordIndex += 1;
                    }

                    WorkSheet1.Columns.AutoFit();


                    // Contact
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("Contact");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Customer Name";
                    WorkSheet1.Cells[1, 2].Value = "Contact Person";
                    WorkSheet1.Cells[1, 3].Value = "Mobile Number";
                    WorkSheet1.Cells[1, 4].Value = "Email";

                    recordIndex = 2;
                    foreach (var items in lstCustomerListObj.ToList().Distinct())
                    {
                        var vContactDetail_Search = new Search_Request()
                        {
                            CustomerId = items.Id,
                            SearchText = ""
                        };

                        var lstContactListObj = await _customerRepository.GetContactDetailsList(vContactDetail_Search);
                        foreach (var itemContact in lstContactListObj)
                        {
                            WorkSheet1.Cells[recordIndex, 1].Value = items.CustomerName;
                            WorkSheet1.Cells[recordIndex, 2].Value = itemContact.ContactPerson;
                            WorkSheet1.Cells[recordIndex, 3].Value = itemContact.MobileNo;
                            WorkSheet1.Cells[recordIndex, 4].Value = itemContact.EmailId;

                            recordIndex += 1;
                        }
                    }
                    WorkSheet1.Columns.AutoFit();


                    // Billing
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("Billing");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Customer Name";
                    WorkSheet1.Cells[1, 2].Value = "Street Name";
                    WorkSheet1.Cells[1, 3].Value = "Country Name";

                    recordIndex = 2;
                    foreach (var items in lstCustomerListObj)
                    {
                        var vBillingSearch = new Search_Request()
                        {
                            CustomerId = items.Id,
                            SearchText = ""
                        };

                        var lstAddressListObj = await _customerRepository.GetBillingDetailsList(vBillingSearch);
                        foreach (var itemAddress in lstAddressListObj)
                        {
                            WorkSheet1.Cells[recordIndex, 1].Value = items.CustomerName;
                            WorkSheet1.Cells[recordIndex, 2].Value = itemAddress.StreetName;
                            WorkSheet1.Cells[recordIndex, 3].Value = itemAddress.CountryName;

                            recordIndex += 1;
                        }
                    }
                    WorkSheet1.Columns.AutoFit();

                    // Shipping
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("Shipping");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Customer Name";
                    WorkSheet1.Cells[1, 2].Value = "Street Name";
                    WorkSheet1.Cells[1, 3].Value = "Country Name";

                    recordIndex = 2;
                    foreach (var items in lstCustomerListObj)
                    {
                        var vShippingSearch = new Search_Request()
                        {
                            CustomerId = items.Id,
                            SearchText = ""
                        };

                        var lstAddressListObj = await _customerRepository.GetShippingDetailsList(vShippingSearch);
                        foreach (var itemAddress in lstAddressListObj)
                        {
                            WorkSheet1.Cells[recordIndex, 1].Value = items.CustomerName;
                            WorkSheet1.Cells[recordIndex, 2].Value = itemAddress.StreetName;
                            WorkSheet1.Cells[recordIndex, 3].Value = itemAddress.CountryName;

                            recordIndex += 1;
                        }
                    }
                    WorkSheet1.Columns.AutoFit();

                    // Shipping
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("Login");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Customer Name";
                    WorkSheet1.Cells[1, 2].Value = "Contact Name";
                    WorkSheet1.Cells[1, 3].Value = "Mobile";
                    WorkSheet1.Cells[1, 4].Value = "IsActive";

                    recordIndex = 2;
                    foreach (var items in lstCustomerListObj)
                    {
                        var vLoginSearch = new Search_Request()
                        {
                            CustomerId = items.Id,
                            SearchText = ""
                        };

                        var lstAddressListObj = await _customerRepository.GetLoginCredentialsList(vLoginSearch);
                        foreach (var itemAddress in lstAddressListObj)
                        {
                            WorkSheet1.Cells[recordIndex, 1].Value = items.CustomerName;
                            WorkSheet1.Cells[recordIndex, 2].Value = itemAddress.ContactName;
                            WorkSheet1.Cells[recordIndex, 3].Value = itemAddress.Username;
                            WorkSheet1.Cells[recordIndex, 4].Value = items.IsActive == true ? "Active" : "Inactive";

                            recordIndex += 1;
                        }
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

        #region Contact Details 

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveContactDetails(ContactDetails_Request parameters)
        {
            int result = await _customerRepository.SaveContactDetails(parameters);

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
            else if (result == -3)
            {
                _response.Message = "Mobile Number is exists";
            }
            else
            {
                _response.Message = "Record details saved sucessfully";
            }

            return _response;
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetContactDetailsList(Search_Request parameters)
        {
            IEnumerable<ContactDetails_Response> lstCustomers = await _customerRepository.GetContactDetailsList(parameters);
            _response.Data = lstCustomers.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetContactDetailsById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _customerRepository.GetContactDetailsById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region Billing Details 

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveBillingDetails(Billing_ShippingDetails_Request parameters)
        {
            int result = await _customerRepository.SaveBillingDetails(parameters);

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
        public async Task<ResponseModel> GetBillingDetailsList(Search_Request parameters)
        {
            IEnumerable<Billing_ShippingDetails_Response> lstCustomers = await _customerRepository.GetBillingDetailsList(parameters);
            _response.Data = lstCustomers.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetBillingDetailsById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _customerRepository.GetBillingDetailsById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region Shipping Details 

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveShippingDetails(Billing_ShippingDetails_Request parameters)
        {
            int result = await _customerRepository.SaveShippingDetails(parameters);

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
        public async Task<ResponseModel> GetShippingDetailsList(Search_Request parameters)
        {
            IEnumerable<Billing_ShippingDetails_Response> lstCustomers = await _customerRepository.GetShippingDetailsList(parameters);
            _response.Data = lstCustomers.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetShippingDetailsById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _customerRepository.GetShippingDetailsById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region Login Credentials 

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveLoginCredentials(LoginCredentials_Request parameters)
        {
            int result = await _customerRepository.SaveLoginCredentials(parameters);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.ReocrdExists)
            {
                _response.Message = "Mobile Number is exists";
                //_response.Message = "Record is already exists";
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
        public async Task<ResponseModel> GetLoginCredentialsList(Search_Request parameters)
        {
            IEnumerable<LoginCredentials_Response> lstCustomers = await _customerRepository.GetLoginCredentialsList(parameters);
            _response.Data = lstCustomers.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetLoginCredentialsById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _customerRepository.GetLoginCredentialsById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion
    }
}
