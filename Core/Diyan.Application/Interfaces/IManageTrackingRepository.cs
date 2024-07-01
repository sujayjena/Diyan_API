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
        Task<int> SavePurchaseOrder(PurchaseOrder_Request parameters);
        Task<IEnumerable<PurchaseOrder_Response>> GetPurchaseOrderList(PurchaseOrderSearch_Request parameters);
        Task<PurchaseOrder_Response?> GetPurchaseOrderById(int Id);

        Task<int> SavePIIssued(PIIssued_Request parameters);
        Task<IEnumerable<PIIssued_Response>> GetPIIssuedList(PIIssuedSearch_Request parameters);
    }
}
