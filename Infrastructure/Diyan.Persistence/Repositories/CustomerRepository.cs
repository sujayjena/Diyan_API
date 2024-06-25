using Dapper;
using Diyan.Application.Helpers;
using Diyan.Application.Interfaces;
using Diyan.Application.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diyan.Persistence.Repositories
{
    public class CustomerRepository : GenericRepository, ICustomerRepository
    {
        private IConfiguration _configuration;

        public CustomerRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        #region Customer 

        public async Task<int> SaveCustomer(Customer_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@CustomerName", parameters.CustomerName);
            queryParameters.Add("@MobileNo", parameters.MobileNo);
            queryParameters.Add("@ParentCustomerId", parameters.ParentCustomerId);
            queryParameters.Add("@LandlineNumber", parameters.LandlineNumber);
            queryParameters.Add("@CustomerTypeId", parameters.CustomerTypeId);
            queryParameters.Add("@EmailId", parameters.EmailId);
            queryParameters.Add("@CountryId", parameters.CountryId);
            queryParameters.Add("@ContactName", parameters.ContactName);
            queryParameters.Add("@LeadStatusId", parameters.LeadStatusId);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveCustomer", queryParameters);
        }

        public async Task<IEnumerable<Customer_Response>> GetCustomerList(CustomerSearch_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@CustomerId", parameters.CustomerId);
            queryParameters.Add("@LeadStatusId", parameters.LeadStatusId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<Customer_Response>("GetCustomerList", queryParameters);

            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<Customer_Response?> GetCustomerById(long Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<Customer_Response>("GetCustomerById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Contact Details 

        public async Task<int> SaveContactDetails(ContactDetails_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@CustomerId", parameters.CustomerId);
            queryParameters.Add("@ContactPerson", parameters.ContactPerson);
            queryParameters.Add("@MobileNo", parameters.MobileNo);
            queryParameters.Add("@AlternetMobileNo", parameters.AlternetMobileNo);
            queryParameters.Add("@EmailId", parameters.EmailId);
            queryParameters.Add("@AlternetEmailId", parameters.AlternetEmailId);
            queryParameters.Add("@Designation", parameters.Designation);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveContactDetails", queryParameters);
        }

        public async Task<IEnumerable<ContactDetails_Response>> GetContactDetailsList(Search_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@CustomerId", parameters.CustomerId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<ContactDetails_Response>("GetContactDetailsList", queryParameters);

            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<ContactDetails_Response?> GetContactDetailsById(long Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<ContactDetails_Response>("GetContactDetailsById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Billing Details 

        public async Task<int> SaveBillingDetails(Billing_ShippingDetails_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@CustomerId", parameters.CustomerId);
            queryParameters.Add("@StreetName", parameters.StreetName);
            queryParameters.Add("@CountryId", parameters.CountryId);
            queryParameters.Add("@StateId", parameters.StateId);
            queryParameters.Add("@PostalZipCodde", parameters.PostalZipCodde);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveBillingDetails", queryParameters);
        }

        public async Task<IEnumerable<Billing_ShippingDetails_Response>> GetBillingDetailsList(Search_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@CustomerId", parameters.CustomerId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<Billing_ShippingDetails_Response>("GetBillingDetailsList", queryParameters);

            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<Billing_ShippingDetails_Response?> GetBillingDetailsById(long Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<Billing_ShippingDetails_Response>("GetBillingDetailsById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Shipping Details 

        public async Task<int> SaveShippingDetails(Billing_ShippingDetails_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@CustomerId", parameters.CustomerId);
            queryParameters.Add("@StreetName", parameters.StreetName);
            queryParameters.Add("@CountryId", parameters.CountryId);
            queryParameters.Add("@StateId", parameters.StateId);
            queryParameters.Add("@PostalZipCodde", parameters.PostalZipCodde);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveShippingDetails", queryParameters);
        }

        public async Task<IEnumerable<Billing_ShippingDetails_Response>> GetShippingDetailsList(Search_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@CustomerId", parameters.CustomerId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<Billing_ShippingDetails_Response>("GetShippingDetailsList", queryParameters);

            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<Billing_ShippingDetails_Response?> GetShippingDetailsById(long Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<Billing_ShippingDetails_Response>("GetShippingDetailsById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Login Credentials 

        public async Task<int> SaveLoginCredentials(LoginCredentials_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@CustomerId", parameters.CustomerId);
            queryParameters.Add("@Username", parameters.Username);
            queryParameters.Add("@Passwords", parameters.Passwords);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveLoginCredentials", queryParameters);
        }

        public async Task<IEnumerable<LoginCredentials_Response>> GetLoginCredentialsList(Search_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@CustomerId", parameters.CustomerId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<LoginCredentials_Response>("GetLoginCredentialsList", queryParameters);

            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<LoginCredentials_Response?> GetLoginCredentialsById(long Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<LoginCredentials_Response>("GetLoginCredentialsById", queryParameters)).FirstOrDefault();
        }

        #endregion

    }
}
