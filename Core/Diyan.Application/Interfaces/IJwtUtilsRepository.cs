using Diyan.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diyan.Application.Interfaces
{
    public interface IJwtUtilsRepository
    {
        public (string, DateTime) GenerateJwtToken_Customer(MobileAppLoginSessionData parameters);
        Task<MobileAppLoginSessionData?> ValidateJwtToken_Customer(string token);

        public (string, DateTime) GenerateJwtToken(UsersLoginSessionData parameters);
        Task<UsersLoginSessionData?> ValidateJwtToken(string token);
    }
}
