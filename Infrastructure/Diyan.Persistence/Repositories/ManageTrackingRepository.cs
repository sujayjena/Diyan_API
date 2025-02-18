﻿using Dapper;
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
            queryParameters.Add("@PO_PaymentType", parameters.PO_PaymentType);
            queryParameters.Add("@PO_CurrencyTypeId", parameters.PO_CurrencyTypeId);
            queryParameters.Add("@PO_CurrencyValue", parameters.PO_CurrencyValue);
            queryParameters.Add("@PO_Amount", parameters.PO_Amount);
            queryParameters.Add("@PO_PaperTypeId", parameters.PO_PaperTypeId);
            queryParameters.Add("@PO_BrandId", parameters.PO_BrandId);
            queryParameters.Add("@PO_TypeOfPackagingId", parameters.PO_TypeOfPackagingId);
            queryParameters.Add("@PO_DeliveryTermsId", parameters.PO_DeliveryTermsId);
            queryParameters.Add("@PO_IsPOStatusClosed", parameters.PO_IsPOStatusClosed);
            queryParameters.Add("@PO_Image", parameters.PO_POImage);
            queryParameters.Add("@PO_OriginalFileName", parameters.PO_POOriginalFileName);
            queryParameters.Add("@PO_CommissionPerTon", parameters.PO_CommissionPerTon);
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
            queryParameters.Add("@BI_ETD", parameters.BI_ETD);
            queryParameters.Add("@BI_ETA", parameters.BI_ETA);
            queryParameters.Add("@BI_Image", parameters.BI_Image);
            queryParameters.Add("@BI_OriginalFileName", parameters.BI_OriginalFileName);

            queryParameters.Add("@CUL_IsContainersUnderLoadingClose", parameters.CUL_IsContainersUnderLoadingClose);
            queryParameters.Add("@CUL_ContainersUnderLoadingClosedDate", parameters.CUL_ContainersUnderLoadingClosedDate);

            queryParameters.Add("@IN_IsInvoiceGenerateClose", parameters.IN_IsInvoiceGenerateClose);
            queryParameters.Add("@IN_InvoiceGenerateClosedDate", parameters.IN_InvoiceGenerateClosedDate);

            queryParameters.Add("@BID_BIDraftRemark", parameters.BID_BIDraftRemark);
            queryParameters.Add("@BID_BIDraftComment", parameters.BID_BIDraftComment);
            queryParameters.Add("@BID_IsBIDraftIssueClose", parameters.BID_IsBIDraftIssueClose);
            queryParameters.Add("@BID_BIDraftIssueClosedDate", parameters.BID_BIDraftIssueClosedDate);
            queryParameters.Add("@BID_IsConfirmed", parameters.BID_IsConfirmed);

            queryParameters.Add("@FBI_IsFinalBIDraftIssueClose", parameters.FBI_IsFinalBIDraftIssueClose);
            queryParameters.Add("@FBI_FinalBIDraftIssueClosedDate", parameters.FBI_FinalBIDraftIssueClosedDate);
            queryParameters.Add("@FBI_FinalBIImage", parameters.FBI_FinalBIImage);
            queryParameters.Add("@FBI_FinalBIOriginalFileName", parameters.FBI_FinalBIOriginalFileName);

            queryParameters.Add("@FAP_IsFinalAmountToPayClose", parameters.FAP_IsFinalAmountToPayClose);
            queryParameters.Add("@FAP_FinalAmountToPayClosedDate", parameters.FAP_FinalAmountToPayClosedDate);
            queryParameters.Add("@FAP_FinalAmountToPay", parameters.FAP_FinalAmountToPay);

            queryParameters.Add("@PR_IsPaymentReceived", parameters.PR_IsPaymentReceived);
            queryParameters.Add("@PR_PaymentReceivedDate", parameters.PR_PaymentReceivedDate);
            queryParameters.Add("@PR_FinalAmount", parameters.PR_FinalAmount);
            queryParameters.Add("@PR_BankReferenceNumber", parameters.PR_BankReferenceNumber);
            queryParameters.Add("@PR_AmountDue", parameters.PR_AmountDue);
            queryParameters.Add("@PR_BalanceAmount", parameters.PR_BalanceAmount);
            queryParameters.Add("@PR_OriginalFileName", parameters.PR_OriginalFileName);
            queryParameters.Add("@PR_Image", parameters.PR_Image);

            queryParameters.Add("@DDS_IsDocumentSendDHL_Submitted", parameters.DDS_IsDocumentSendDHL_Submitted);
            queryParameters.Add("@DDS_DocumentSendDHL_SubmittedDate", parameters.DDS_DocumentSendDHL_SubmittedDate);
            queryParameters.Add("@DDS_AWBNumber", parameters.DDS_AWBNumber);
            queryParameters.Add("@POC_IsPOClosed", parameters.POC_IsPOClosed);
            queryParameters.Add("@POC_POClosedDate", parameters.POC_POClosedDate);

            queryParameters.Add("@MC_TotalQty", parameters.MC_TotalQty);
            queryParameters.Add("@MC_TotalPaymentReceived", parameters.MC_TotalPaymentReceived);
            queryParameters.Add("@MC_CommissionPerTon", parameters.MC_CommissionPerTon);
            queryParameters.Add("@MC_TotalCommission", parameters.MC_TotalCommission);
            queryParameters.Add("@MC_CommissionClosedDate", parameters.MC_CommissionClosedDate);
            queryParameters.Add("@MC_IsCommissionClosed", parameters.MC_IsCommissionClosed);

            queryParameters.Add("@OC_IsOrderCompleteClosed", parameters.OC_IsOrderCompleteClosed);
            queryParameters.Add("@OC_OrderCompleteDate", parameters.OC_OrderCompleteDate);

            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SavePurchaseOrder", queryParameters);
        }

        public async Task<IEnumerable<PurchaseOrderList_Response>> GetPurchaseOrderList(PurchaseOrderSearch_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@CountryId", parameters.CountryId);
            queryParameters.Add("@CustomerId", parameters.CustomerId);
            queryParameters.Add("@StatusId", parameters.StatusId);
            queryParameters.Add("@TrakingStatusId", parameters.TrakingStatusId);
            queryParameters.Add("@TrakingNumber", parameters.TrakingNumber);
            queryParameters.Add("@PINumber", parameters.PINumber);
            queryParameters.Add("@IsPIConfirmation", parameters.IsPIConfirmation);
            queryParameters.Add("@IsPaymentOrLCReceived", parameters.IsPaymentOrLCReceived);
            queryParameters.Add("@IsCommissionAgent", parameters.IsCommissionAgent);
            queryParameters.Add("@FilterType", parameters.FilterType);
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

        public async Task<int> DeletePIIssued(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return await SaveByStoredProcedure<int>("DeletePIIssued", queryParameters);
        }

        public async Task<IEnumerable<SelectListResponse>> GetPINumberForSelectList(PINumberForSelect_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@StatusId", parameters.StatusId);
            queryParameters.Add("@CustomerId", parameters.CustomerId);
            var result = await ListByStoredProcedure<SelectListResponse>("GetPINumberForSelectList", queryParameters);

            return result;
        }

        public async Task<IEnumerable<ValidatePINumber_Response>> ValidatePINumber(string PINumber)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@PINumber", PINumber); ;

            var result = await ListByStoredProcedure<ValidatePINumber_Response>("ValidatePINumber", queryParameters);
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
            queryParameters.Add("@POAmount", parameters.POAmount);
            queryParameters.Add("@RemainingAmount", parameters.RemainingAmount);
            queryParameters.Add("@TotalReceivedAmount", parameters.TotalReceivedAmount);
            queryParameters.Add("@BankReferenceNumber", parameters.BankReferenceNumber);
            queryParameters.Add("@InvoiceGenerateDate", parameters.InvoiceGenerateDate);
            queryParameters.Add("@PINumber", parameters.PINumber);
            queryParameters.Add("@PIIssuedDate", parameters.PIIssuedDate);
            queryParameters.Add("@BankId", parameters.BankId);
            queryParameters.Add("@BankCommission", parameters.BankCommission);
            queryParameters.Add("@ModuleType", parameters.ModuleType);

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

        public async Task<PO_PaymentReceived_Response?> GetPurchaseOrderPaymentById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<PO_PaymentReceived_Response>("GetPurchaseOrderPaymentById", queryParameters)).FirstOrDefault();
        }

        public async Task<int> SavePurchaseOrderLCReceived(PO_LCReceived_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@PurchaseOrderId", parameters.PurchaseOrderId);
            queryParameters.Add("@ReceivedDate", parameters.ReceivedDate);
            queryParameters.Add("@LCNumber", parameters.LCNumber);
            queryParameters.Add("@POAmount", parameters.POAmount);
            queryParameters.Add("@PINumber", parameters.PINumber);
            queryParameters.Add("@PIIssuedDate", parameters.PIIssuedDate);
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

        public async Task<int> DeletePaymentReceived(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return await SaveByStoredProcedure<int>("DeletePaymentReceived", queryParameters);
        }

        public async Task<int> DeleteLCReceived(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return await SaveByStoredProcedure<int>("DeleteLCReceived", queryParameters);
        }

        public async Task<int> SavePurchaseOrderPaymentReceivedImages(PurchaseOrderPaymentReceivedImages_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@PurchaseOrderPaymentReceivedId", parameters.PurchaseOrderPaymentReceivedId);
            queryParameters.Add("@ImageFileName", parameters.ImageFileName);
            queryParameters.Add("@ImageOriginalFileName", parameters.ImageOriginalFileName);

            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SavePurchaseOrderPaymentReceivedImages", queryParameters);
        }

        public async Task<IEnumerable<PurchaseOrderPaymentReceivedImages_Response>> GetPurchaseOrderPaymentReceivedImagesById(PurchaseOrderPaymentReceivedImage_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.PurchaseOrderPaymentReceivedId);

            var result = await ListByStoredProcedure<PurchaseOrderPaymentReceivedImages_Response>("GetPurchaseOrderPaymentReceivedImagesById", queryParameters);

            return result;
        }

        public async Task<int> DeletePurchaseOrderPaymentReceivedImages(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return await SaveByStoredProcedure<int>("DeletePurchaseOrderPaymentReceivedImages", queryParameters);
        }

        #endregion

        #region Containers Under Loading

        public async Task<int> SaveContainersUnderLoading(ContainersUnderLoading_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@PurchaseOrderId", parameters.PurchaseOrderId);
            queryParameters.Add("@ContainerCount", parameters.ContainerCount);
            queryParameters.Add("@IsPartialShipment", parameters.IsPartialShipment);

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

      

        public async Task<int> DeleteContainersUnderLoading(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return await SaveByStoredProcedure<int>("DeleteContainersUnderLoading", queryParameters);
        }

        public async Task<int> DeleteContainersUnderLoadingImages(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return await SaveByStoredProcedure<int>("DeleteContainersUnderLoadingImages", queryParameters);
        }

        #endregion

        #region Invoice

        public async Task<int> SaveInvoice(Invoice_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@PurchaseOrderId", parameters.PurchaseOrderId);
            queryParameters.Add("@InvoiceGeneratedDate", parameters.InvoiceGeneratedDate);
            queryParameters.Add("@InvoiceNumber", parameters.InvoiceNumber);
            queryParameters.Add("@InvoiceAmount", parameters.InvoiceAmount);
            queryParameters.Add("@Quantity", parameters.Quantity);

            queryParameters.Add("@Freight", parameters.Freight);
            queryParameters.Add("@PortId", parameters.PortId);
            queryParameters.Add("@SBNo", parameters.SBNo);
            queryParameters.Add("@SBDate", parameters.SBDate);
            queryParameters.Add("@LeoDate", parameters.LeoDate);
            queryParameters.Add("@ExchangeRate", parameters.ExchangeRate);
            queryParameters.Add("@TotalFOBValue", parameters.TotalFOBValue);
            queryParameters.Add("@BRCInBank", parameters.BRCInBank);
            queryParameters.Add("@BRCInDGFT", parameters.BRCInDGFT);
            queryParameters.Add("@FOBPerct", parameters.FOBPerct);
            queryParameters.Add("@DBKValue", parameters.DBKValue);
            queryParameters.Add("@DBKReceived", parameters.DBKReceived);
            queryParameters.Add("@IGSTAmount", parameters.IGSTAmount);
            queryParameters.Add("@IGSTReceived", parameters.IGSTReceived);
            queryParameters.Add("@CommissionMentionInSBill", parameters.CommissionMentionInSBill);
            queryParameters.Add("@UtilizedAmount", parameters.UtilizedAmount);
            queryParameters.Add("@UnUtilizedAmount", parameters.UnUtilizedAmount);
            queryParameters.Add("@CommissionRemark", parameters.CommissionRemark);
            queryParameters.Add("@IsCommissionClosed", parameters.IsCommissionClosed);
            queryParameters.Add("@Containers", parameters.Containers);
            queryParameters.Add("@ReuseOrFreshId", parameters.ReuseOrFreshId);
            queryParameters.Add("@TransporterId", parameters.TransporterId);
            queryParameters.Add("@Rate", parameters.Rate);
            queryParameters.Add("@LandFreight", parameters.LandFreight);
            queryParameters.Add("@TransporterInvoice", parameters.TransporterInvoice);
            queryParameters.Add("@ForwarderId", parameters.ForwarderId);
            queryParameters.Add("@ForwarderInvoice", parameters.ForwarderInvoice);
            queryParameters.Add("@SeaFreight", parameters.SeaFreight);
            queryParameters.Add("@CHAId", parameters.CHAId);
            queryParameters.Add("@ChaInvoice", parameters.ChaInvoice);
            queryParameters.Add("@Clearing", parameters.Clearing);
            queryParameters.Add("@CurrentExchangeRate", parameters.CurrentExchangeRate);
            queryParameters.Add("@DrawBackPerct", parameters.DrawBackPerct);
            queryParameters.Add("@DrawBack_RodTep", parameters.DrawBack_RodTep);
            queryParameters.Add("@InvoiceAmountInINR", parameters.InvoiceAmountInINR);
            queryParameters.Add("@NetSellRate", parameters.NetSellRate);

            queryParameters.Add("@InvoiceImage", parameters.InvoiceImage);
            queryParameters.Add("@InvoiceOriginalFileName", parameters.InvoiceOriginalFileName);

            queryParameters.Add("@ModuleType", parameters.ModuleType);

            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveInvoice", queryParameters);
        }

        public async Task<IEnumerable<Invoice_Response>> GetInvoiceList(Invoice_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@PurchaseOrderId", parameters.PurchaseOrderId);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<Invoice_Response>("GetInvoiceList", queryParameters);
            return result;
        }

        public async Task<IEnumerable<Invoice_Response>> GetInvoiceById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);

            var result = await ListByStoredProcedure<Invoice_Response>("GetInvoiceById", queryParameters);
            return result;
        }

        public async Task<int> DeleteInvoice(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return await SaveByStoredProcedure<int>("DeleteInvoice", queryParameters);
        }
        #endregion

        #region BI Draft

        public async Task<int> SaveBIDraftIssuedImages(BIDraftIssuedImages_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@PurchaseOrderId", parameters.PurchaseOrderId);
            queryParameters.Add("@ImageName", parameters.ImageName);
            queryParameters.Add("@OriginalFileName", parameters.OriginalFileName);
            queryParameters.Add("@Remark", parameters.Remark);
            queryParameters.Add("@StatusId", parameters.StatusId);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveBIDraftIssuedImages", queryParameters);
        }

        public async Task<IEnumerable<BIDraftIssuedImages_Response>> GetBIDraftIssuedImagesById(BIDraftIssuedImages_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@PurchaseOrderId", parameters.PurchaseOrderId);

            var result = await ListByStoredProcedure<BIDraftIssuedImages_Response>("GetBIDraftIssuedImagesById", queryParameters);

            return result;
        }

        public async Task<int> DeleteBIDraftIssuedImages(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return await SaveByStoredProcedure<int>("DeleteBIDraftIssuedImages", queryParameters);
        }

        public async Task<IEnumerable<BIDraftIssuedRemarkLog_Response>> GetBIDraftIssuedRemarkLogById(BIDraftIssuedImages_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@PurchaseOrderId", parameters.PurchaseOrderId);

            var result = await ListByStoredProcedure<BIDraftIssuedRemarkLog_Response>("GetBIDraftIssuedRemarksLogById", queryParameters);

            return result;
        }

        public async Task<IEnumerable<BIDraftIssuedCommentsLog_Response>> GetBIDraftIssuedCommentLogById(BIDraftIssuedImages_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@PurchaseOrderId", parameters.PurchaseOrderId);

            var result = await ListByStoredProcedure<BIDraftIssuedCommentsLog_Response>("GetBIDraftIssuedCommentsLogById", queryParameters);

            return result;
        }

        public async Task<IEnumerable<BIDraftIssuedLog_Response>> GetBIDraftIssuedLogListById(BIDraftIssuedLog_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@BIDraftIssuedId", parameters.BIDraftIssuedId);

            var result = await ListByStoredProcedure<BIDraftIssuedLog_Response>("GetBIDraftIssuedLogListById", queryParameters);

            return result;
        }

        #endregion

    }
}
