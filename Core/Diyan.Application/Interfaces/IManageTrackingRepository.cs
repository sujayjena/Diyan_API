using Diyan.Application.Models;
using Diyan.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Diyan.Application.Interfaces
{
    public interface IManageTrackingRepository
    {
        #region Purchase Order

        Task<int> SavePurchaseOrder(PurchaseOrder_Request parameters);
        Task<IEnumerable<PurchaseOrderList_Response>> GetPurchaseOrderList(PurchaseOrderSearch_Request parameters);
        Task<PurchaseOrderDetail_Response?> GetPurchaseOrderById(int Id);

        #endregion

        #region PI Issue

        Task<int> SavePIIssued(PIIssued_Request parameters);
        Task<IEnumerable<PIIssued_Response>> GetPIIssuedList(PIIssued_Search parameters);

        Task<IEnumerable<PIIssuedLog_Response>> GetPIIssuedLogListById(PIIssuedLog_Search parameters);

        #endregion

        #region Payment Received Or LC Received

        Task<IEnumerable<PO_PaymentReceived_Or_LCReceivedList_Response>> GetPaymentReceived_Or_LCReceivedList(PO_PaymentReceived_Or_LCReceived_Search parameters);

        Task<int> SavePurchaseOrderPaymentReceived(PO_PaymentReceived_Request parameters);

        Task<IEnumerable<PO_PaymentReceived_Response>> GetPaymentReceivedList(PO_PaymentReceived_Or_LCReceived_Search parameters);

        Task<int> SavePurchaseOrderLCReceived(PO_LCReceived_Request parameters);

        Task<IEnumerable<PO_LCReceived_Response>> GetLCReceivedList(PO_PaymentReceived_Or_LCReceived_Search parameters);

        #endregion

        #region Containers Under Loading

        Task<int> SaveContainersUnderLoading(ContainersUnderLoading_Request parameters);

        Task<int> SaveContainersUnderLoadingImages(ContainersUnderLoadingImages_Request parameters);

        Task<IEnumerable<ContainersUnderLoading_Response>> GetContainersUnderLoadingById(ContainersUnderLoading_Search parameters);

        Task<IEnumerable<ContainersUnderLoadingImages_Response>> GetContainersUnderLoadingImagesById(ContainersUnderLoadingImages_Search parameters);

        #endregion
    }
}
