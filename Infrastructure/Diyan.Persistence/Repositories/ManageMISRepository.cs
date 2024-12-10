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
    public class ManageMISRepository : GenericRepository, IManageMISRepository
    {
        private IConfiguration _configuration;

        public ManageMISRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<MIS_PITrackingList_Response>> GetMIS_PITrackingList(MIS_PITracking_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@FromDate", parameters.FromDate);
            queryParameters.Add("@ToDate", parameters.ToDate);
            queryParameters.Add("@PaperTypeId", parameters.PaperTypeId);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<MIS_PITrackingList_Response>("GetMIS_PITrackingList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }
    }
}
