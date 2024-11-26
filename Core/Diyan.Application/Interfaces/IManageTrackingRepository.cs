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

        #region PI Issued

        Task<int> SavePIIssued(PIIssued_Request parameters);
        Task<IEnumerable<PIIssued_Response>> GetPIIssuedList(PIIssued_Search parameters);

        Task<IEnumerable<PIIssuedLog_Response>> GetPIIssuedLogListById(PIIssuedLog_Search parameters);

        Task<int> DeletePIIssued(int Id);

        Task<IEnumerable<SelectListResponse>> GetPINumberForSelectList();

        #endregion

        #region Payment Received Or LC Received

        Task<IEnumerable<PO_PaymentReceived_Or_LCReceivedList_Response>> GetPaymentReceived_Or_LCReceivedList(PO_PaymentReceived_Or_LCReceived_Search parameters);

        Task<int> SavePurchaseOrderPaymentReceived(PO_PaymentReceived_Request parameters);

        Task<IEnumerable<PO_PaymentReceived_Response>> GetPaymentReceivedList(PO_PaymentReceived_Or_LCReceived_Search parameters);

        Task<int> SavePurchaseOrderLCReceived(PO_LCReceived_Request parameters);

        Task<IEnumerable<PO_LCReceived_Response>> GetLCReceivedList(PO_PaymentReceived_Or_LCReceived_Search parameters);

        Task<int> DeletePaymentReceived(int Id);

        Task<int> DeleteLCReceived(int Id);

        Task<int> SavePurchaseOrderPaymentReceivedImages(PurchaseOrderPaymentReceivedImages_Request parameters);
        Task<IEnumerable<PurchaseOrderPaymentReceivedImages_Response>> GetPurchaseOrderPaymentReceivedImagesById(PurchaseOrderPaymentReceivedImage_Search parameters);

        Task<int> DeletePurchaseOrderPaymentReceivedImages(int Id);

        #endregion

        #region Containers Under Loading

        Task<int> SaveContainersUnderLoading(ContainersUnderLoading_Request parameters);

        Task<int> SaveContainersUnderLoadingImages(ContainersUnderLoadingImages_Request parameters);

        Task<IEnumerable<ContainersUnderLoading_Response>> GetContainersUnderLoadingById(ContainersUnderLoading_Search parameters);

        Task<IEnumerable<ContainersUnderLoadingImages_Response>> GetContainersUnderLoadingImagesById(ContainersUnderLoadingImages_Search parameters);

        Task<int> DeleteContainersUnderLoading(int Id);

        Task<int> DeleteContainersUnderLoadingImages(int Id);

        #endregion

        #region Invoice

        Task<int> SaveInvoice(Invoice_Request parameters);

        Task<IEnumerable<Invoice_Response>> GetInvoiceList(Invoice_Search parameters);

        Task<int> DeleteInvoice(int Id);

        #endregion

        #region BI Draft

        Task<int> SaveBIDraftIssuedImages(BIDraftIssuedImages_Request parameters);

        Task<IEnumerable<BIDraftIssuedImages_Response>> GetBIDraftIssuedImagesById(BIDraftIssuedImages_Search parameters);

        Task<int> DeleteBIDraftIssuedImages(int Id);

        Task<IEnumerable<BIDraftIssuedRemarkLog_Response>> GetBIDraftIssuedRemarkLogById(BIDraftIssuedImages_Search parameters);

        Task<IEnumerable<BIDraftIssuedCommentsLog_Response>> GetBIDraftIssuedCommentLogById(BIDraftIssuedImages_Search parameters);

        #endregion
    }
}
