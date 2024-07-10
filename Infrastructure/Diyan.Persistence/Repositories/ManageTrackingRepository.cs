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
    public class ManageTrackingRepository : GenericRepository, IManageTrackingRepository
    {
        private IConfiguration _configuration;

        public ManageTrackingRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        #region Purchase Order

        public async Task<int> SavePurchaseOrder(PurchaseOrder_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@CustomerId", parameters.CustomerId);
            queryParameters.Add("@PO_PortDischargeId", parameters.PO_PortDischargeId);
            queryParameters.Add("@PO_IsPOReceived", parameters.PO_IsPOReceived);
            queryParameters.Add("@PO_PONumber", parameters.PO_PONumber);
            queryParameters.Add("@PO_PaymentTermsId", parameters.PO_PaymentTermsId);
            queryParameters.Add("@PO_Quantity", parameters.PO_Quantity);
            queryParameters.Add("@PO_CurrencyTypeId", parameters.PO_CurrencyTypeId);
            queryParameters.Add("@PO_CurrencyValue", parameters.PO_CurrencyValue);
            queryParameters.Add("@PO_PaperTypeId", parameters.PO_PaperTypeId);
            queryParameters.Add("@PO_BrandId", parameters.PO_BrandId);
            queryParameters.Add("@PO_TypeOfPackagingId", parameters.PO_TypeOfPackagingId);
            queryParameters.Add("@PO_DeliveryTermsId", parameters.PO_DeliveryTermsId);
            queryParameters.Add("@PO_IsPOStatusClosed", parameters.PO_IsPOStatusClosed);
            queryParameters.Add("@PO_Image", parameters.PO_POImage);
            queryParameters.Add("@PO_OriginalFileName", parameters.PO_POOriginalFileName);
            queryParameters.Add("@IsActive", parameters.IsActive);

            queryParameters.Add("@PII_IsClosed", parameters.PII_IsClosed);
            queryParameters.Add("@PIC_IsConfirmed", parameters.PIC_IsConfirmed);

            queryParameters.Add("@PLR_PaymentOrLCReceived", parameters.PLR_PaymentOrLCReceived);
            queryParameters.Add("@PLR_IsPaymentOrLCClosed", parameters.PLR_IsPaymentOrLCClosed);

            queryParameters.Add("@OA_IsOrderAccepted", parameters.OA_IsOrderAccepted);
            queryParameters.Add("@OA_OrderAcceptedDate", parameters.OA_OrderAcceptedDate)
                ;
            queryParameters.Add("@OUP_IsOrderUnderProcess", parameters.OUP_IsOrderUnderProcess);
            queryParameters.Add("@OUP_OrderUnderProcessDate", parameters.OUP_OrderUnderProcessDate);

            queryParameters.Add("@BI_IsBookingIssueAccepted", parameters.BI_IsBookingIssueAccepted);
            queryParameters.Add("@BI_BookingIssueAcceptedDate", parameters.BI_BookingIssueAcceptedDate);

            queryParameters.Add("@CUL_IsContainersUnderLoadingClose", parameters.CUL_IsContainersUnderLoadingClose);
            queryParameters.Add("@CUL_ContainersUnderLoadingClosedDate", parameters.CUL_ContainersUnderLoadingClosedDate);

            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SavePurchaseOrder", queryParameters);
        }

        public async Task<IEnumerable<PurchaseOrderList_Response>> GetPurchaseOrderList(PurchaseOrderSearch_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@CustomerId", parameters.CustomerId);
            queryParameters.Add("@CountryId", parameters.CountryId);
            queryParameters.Add("@PONumber", parameters.PONumber);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<PurchaseOrderList_Response>("GetPurchaseOrderList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<PurchaseOrderDetail_Response?> GetPurchaseOrderById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<PurchaseOrderDetail_Response>("GetPurchaseOrderById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region PI Issue

        public async Task<int> SavePIIssued(PIIssued_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@PurchaseOrderId", parameters.PurchaseOrderId);
            queryParameters.Add("@PIIssueDate", parameters.PIIssueDate);
            queryParameters.Add("@PINumber", parameters.PINumber);
            queryParameters.Add("@PIImage", parameters.PIImage);
            queryParameters.Add("@PIOriginalFileName", parameters.PIOriginalFileName);
            queryParameters.Add("@Remark", parameters.Remark);
            queryParameters.Add("@StatusId", parameters.StatusId);

            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SavePIIssued", queryParameters);
        }

        public async Task<IEnumerable<PIIssued_Response>> GetPIIssuedList(PIIssued_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@PurchaseOrderId", parameters.PurchaseOrderId);
            queryParameters.Add("@CountryId", parameters.CountryId);
            queryParameters.Add("@CustomerId", parameters.CustomerId);
            queryParameters.Add("@PINumber", parameters.PINumber);
            queryParameters.Add("@StatusId", parameters.StatusId);

            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<PIIssued_Response>("GetPIIssuedList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<IEnumerable<PIIssuedLog_Response>> GetPIIssuedLogListById(PIIssuedLog_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@PIIssuedId", parameters.PIIssuedId);

            var result = await ListByStoredProcedure<PIIssuedLog_Response>("GetPIIssuedLogListById", queryParameters);

            return result;
        }

        #endregion

        #region Payment Received Or LC Received

        public async Task<IEnumerable<PO_PaymentReceived_Or_LCReceivedList_Response>> GetPaymentReceived_Or_LCReceivedList(PO_PaymentReceived_Or_LCReceived_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@PurchaseOrderId", parameters.PurchaseOrderId);
            queryParameters.Add("@CountryId", parameters.CountryId);
            queryParameters.Add("@CustomerId", parameters.CustomerId);
            queryParameters.Add("@InvoiceNumber", parameters.InvoiceNumber);
            queryParameters.Add("@StatusId", parameters.StatusId);

            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<PO_PaymentReceived_Or_LCReceivedList_Response>("GetPaymentOrLCReceivedList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<int> SavePurchaseOrderPaymentReceived(PO_PaymentReceived_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@PurchaseOrderId", parameters.PurchaseOrderId);
            queryParameters.Add("@InvoiceNumber", parameters.InvoiceNumber);
            queryParameters.Add("@PaymentTermsId", parameters.PaymentTermsId);
            queryParameters.Add("@PaymentReceivedDate", parameters.PaymentReceivedDate);
            queryParameters.Add("@CurrencyTypeId", parameters.CurrencyTypeId);
            queryParameters.Add("@Amount", parameters.Amount);
            queryParameters.Add("@PaymentReceivedId", parameters.PaymentReceivedId);

            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SavePurchaseOrderPaymentReceived", queryParameters);
        }

        public async Task<IEnumerable<PO_PaymentReceived_Response>> GetPaymentReceivedList(PO_PaymentReceived_Or_LCReceived_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@PurchaseOrderId", parameters.PurchaseOrderId);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<PO_PaymentReceived_Response>("GetPurchaseOrderPaymentList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<int> SavePurchaseOrderLCReceived(PO_LCReceived_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@PurchaseOrderId", parameters.PurchaseOrderId);
            queryParameters.Add("@ReceivedDate", parameters.ReceivedDate);
            queryParameters.Add("@ImageName", parameters.ImageName);
            queryParameters.Add("@OriginalFileName", parameters.OriginalFileName);

            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SavePurchaseOrderLCReceived", queryParameters);
        }

        public async Task<IEnumerable<PO_LCReceived_Response>> GetLCReceivedList(PO_PaymentReceived_Or_LCReceived_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@PurchaseOrderId", parameters.PurchaseOrderId);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<PO_LCReceived_Response>("GetLCReceivedList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        #endregion

        #region Containers Under Loading

        public async Task<int> SaveContainersUnderLoading(ContainersUnderLoading_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@PurchaseOrderId", parameters.PurchaseOrderId);
            queryParameters.Add("@ContainerCount", parameters.ContainerCount);

            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveContainersUnderLoading", queryParameters);
        }

        public async Task<int> SaveContainersUnderLoadingImages(ContainersUnderLoadingImages_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@ContainersUnderLoadingId", parameters.ContainersUnderLoadingId);
            queryParameters.Add("@ContainerImage", parameters.ContainerImage);
            queryParameters.Add("@ContainerOriginalFileName", parameters.ContainerOriginalFileName);

            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveContainersUnderLoadingImages", queryParameters);
        }


        public async Task<IEnumerable<ContainersUnderLoading_Response>> GetContainersUnderLoadingById(ContainersUnderLoading_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.PurchaseOrderId);

            var result = await ListByStoredProcedure<ContainersUnderLoading_Response>("GetContainersUnderLoadingById", queryParameters);

            return result;
        }

        public async Task<IEnumerable<ContainersUnderLoadingImages_Response>> GetContainersUnderLoadingImagesById(ContainersUnderLoadingImages_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.ContainersUnderLoadingId);

            var result = await ListByStoredProcedure<ContainersUnderLoadingImages_Response>("GetContainersUnderLoadingImagesById", queryParameters);

            return result;
        }

        #endregion
    }
}
