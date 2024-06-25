using Diyan.Application.Models;
using Diyan.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diyan.Application.Interfaces
{
    public interface IManageTrackingRepository
    {
        Task<int> SavePurchaseOrderTracking(PurchaseOrderTracking_Request parameters);

        Task<IEnumerable<PurchaseOrderTracking_Response>> GetPurchaseOrderTrackingList(PurchaseOrderTrackingSearch_Request parameters);

        Task<PurchaseOrderTracking_Response?> GetPurchaseOrderTrackingById(int Id);

        Task<int> SavePIIssued(PIIssued_Request parameters);
        Task<IEnumerable<PIIssued_Response>> GetPIIssuedList(PIIssuedSearch_Request parameters);
    }
}
