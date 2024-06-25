using Diyan.Application.Models;
using Diyan.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diyan.Application.Interfaces
{
    public interface ICustomerRepository
    {
        #region Customer 

        Task<int> SaveCustomer(Customer_Request parameters);

        Task<IEnumerable<Customer_Response>> GetCustomerList(CustomerSearch_Request parameters);

        Task<Customer_Response?> GetCustomerById(long Id);

        #endregion

        #region Contact Details 

        Task<int> SaveContactDetails(ContactDetails_Request parameters);

        Task<IEnumerable<ContactDetails_Response>> GetContactDetailsList(Search_Request parameters);

        Task<ContactDetails_Response?> GetContactDetailsById(long Id);

        #endregion

        #region Billing Details 

        Task<int> SaveBillingDetails(Billing_ShippingDetails_Request parameters);

        Task<IEnumerable<Billing_ShippingDetails_Response>> GetBillingDetailsList(Search_Request parameters);

        Task<Billing_ShippingDetails_Response?> GetBillingDetailsById(long Id);

        #endregion

        #region Shipping Details 

        Task<int> SaveShippingDetails(Billing_ShippingDetails_Request parameters);

        Task<IEnumerable<Billing_ShippingDetails_Response>> GetShippingDetailsList(Search_Request parameters);

        Task<Billing_ShippingDetails_Response?> GetShippingDetailsById(long Id);

        #endregion

        #region Login Credentials 

        Task<int> SaveLoginCredentials(LoginCredentials_Request parameters);

        Task<IEnumerable<LoginCredentials_Response>> GetLoginCredentialsList(Search_Request parameters);

        Task<LoginCredentials_Response?> GetLoginCredentialsById(long Id);

        #endregion
    }
}
