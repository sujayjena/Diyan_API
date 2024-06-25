using Diyan.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diyan.Application.Interfaces
{
    public interface ILoginRepository
    {
        Task<MobileAppLoginSessionData> ValidateMobileAppLogin(MobileAppLoginRequestModel parameters);
        Task<MobileAppLoginSessionData?> GetCustomerDetailsByToken(string token);


        Task<UsersLoginSessionData?> ValidateUserLoginByEmail(LoginByMobileNumberRequestModel parameters);

        Task SaveUserLoginHistory(UserLoginHistorySaveParameters parameters);

        Task<UsersLoginSessionData?> GetProfileDetailsByToken(string token);
    }
}
