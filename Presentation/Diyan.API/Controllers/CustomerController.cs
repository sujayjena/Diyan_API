using Diyan.API.CustomAttributes;
using Diyan.Application.Enums;
using Diyan.Application.Helpers;
using Diyan.Application.Interfaces;
using Diyan.Application.Models;
using Diyan.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            else
            {
                _response.Message = "Record details saved sucessfully";

               
                if (result > 0)
                {
                    // Add data into Contact details
                    foreach (var items in parameters.ContactDetailsList)
                    {
                        var vContactDetails = new ContactDetails_Request()
                        {
                            Id = items.Id,
                            CustomerId = result,
                            ContactPerson = items.ContactPerson,
                            MobileNo = items.MobileNo,
                            AlternetMobileNo = items.AlternetMobileNo,
                            EmailId = items.EmailId,
                            AlternetEmailId = items.AlternetEmailId,
                            Designation = items.Designation,
                            IsActive = items.IsActive,
                        };

                        int resultContactDetails = await _customerRepository.SaveContactDetails(vContactDetails);
                    }

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

                    // Add data into Login Credentials details
                    foreach (var items in parameters.LoginCredntials)
                    {
                        var vLoginCredentials = new LoginCredentials_Request()
                        {
                            Id = items.Id,
                            CustomerId = result,
                            Username = items.Username,
                            Passwords = items.Passwords,
                            IsActive = items.IsActive,
                        };

                        int resultLoginCredentials = await _customerRepository.SaveLoginCredentials(vLoginCredentials);
                    }
                }
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

                if (vResultObj != null)
                {
                    //get Contact details
                    var vSearch_Request = new Search_Request()
                    {
                        CustomerId = Convert.ToInt32(vResultObj.Id),
                    };

                    var vContactDetailsListObj = await _customerRepository.GetContactDetailsList(vSearch_Request);
                    foreach (var item in vContactDetailsListObj)
                    {
                        var vContactDetailsObj = new ContactDetails_Response()
                        {
                            Id = item.Id,
                            CustomerId = vResultObj.Id,
                            ContactPerson = item.ContactPerson,
                            MobileNo = item.MobileNo,
                            AlternetMobileNo = item.AlternetMobileNo,
                            EmailId = item.EmailId,
                            AlternetEmailId = item.AlternetEmailId,
                            Designation = item.Designation,
                            IsActive = item.IsActive,
                        };

                        vResultObj.ContactDetailsList.Add(vContactDetailsObj);
                    }

                    //get Billing details
                    var vBillingDetailsListObj = await _customerRepository.GetBillingDetailsList(vSearch_Request);
                    foreach (var item in vBillingDetailsListObj)
                    {
                        var vBillingDetailsObj = new Billing_ShippingDetails_Response()
                        {
                            Id = item.Id,
                            CustomerId = vResultObj.Id,
                            StreetName = item.StreetName,
                            CountryId = item.CountryId,
                            CountryName = item.CountryName,
                            StateId = item.StateId,
                            StateName = item.StateName,
                            PostalZipCodde = item.PostalZipCodde,
                            IsActive = item.IsActive,
                        };

                        vResultObj.BillingDetailsList.Add(vBillingDetailsObj);
                    }

                    //get Shipping details
                    var vShippingDetailsListObj = await _customerRepository.GetShippingDetailsList(vSearch_Request);
                    foreach (var item in vShippingDetailsListObj)
                    {
                        var vShippingDetailsObj = new Billing_ShippingDetails_Response()
                        {
                            Id = item.Id,
                            CustomerId = vResultObj.Id,
                            StreetName = item.StreetName,
                            CountryId = item.CountryId,
                            CountryName = item.CountryName,
                            StateId = item.StateId,
                            StateName = item.StateName,
                            PostalZipCodde = item.PostalZipCodde,
                            IsActive = item.IsActive,
                        };

                        vResultObj.ShippingDetailsList.Add(vShippingDetailsObj);
                    }

                    //get Login Credentials
                    var vLoginCredentialsListObj = await _customerRepository.GetLoginCredentialsList(vSearch_Request);
                    foreach (var item in vLoginCredentialsListObj)
                    {
                        var vLoginCredentialsObj = new LoginCredentials_Response()
                        {
                            Id = item.Id,
                            CustomerId = vResultObj.Id,
                            CustomerName = item.CustomerName,
                            Username = item.Username,
                            Passwords = item.Passwords,
                            IsActive = item.IsActive,
                        };

                        vResultObj.LoginCredntials.Add(vLoginCredentialsObj);
                    }
                }
                _response.Data = vResultObj;
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
