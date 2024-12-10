using Diyan.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diyan.Application.Interfaces
{
    public interface IManageMISRepository
    {
        Task<IEnumerable<MIS_PITrackingList_Response>> GetMIS_PITrackingList(MIS_PITracking_Search parameters);
    }
}
