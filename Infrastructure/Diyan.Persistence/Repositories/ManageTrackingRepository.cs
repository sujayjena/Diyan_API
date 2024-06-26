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

        public async Task<int> SavePurchaseOrder(PurchaseOrder_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@CustomerId", parameters.CustomerId);
            queryParameters.Add("@PortDischargeId", parameters.PortDischargeId);
            queryParameters.Add("@IsPOReceived", parameters.IsPOReceived);
            queryParameters.Add("@PONumber", parameters.PONumber);
            queryParameters.Add("@PaymentTermsId", parameters.PaymentTermsId);
            queryParameters.Add("@Quantity", parameters.Quantity);
            queryParameters.Add("@CurrencyTypeId", parameters.CurrencyTypeId);
            queryParameters.Add("@CurrencyValue", parameters.CurrencyValue);
            queryParameters.Add("@PaperTypeId", parameters.PaperTypeId);
            queryParameters.Add("@BrandId", parameters.BrandId);
            queryParameters.Add("@TypeOfPackagingId", parameters.TypeOfPackagingId);
            queryParameters.Add("@DeliveryTermsId", parameters.DeliveryTermsId);
            queryParameters.Add("@IsPOStatusClosed", parameters.IsPOStatusClosed);
            queryParameters.Add("@IsPIClosed", parameters.IsPIClosed);
            queryParameters.Add("@IsPIConfirmed", parameters.IsPIConfirmed);
            queryParameters.Add("@POImage", parameters.POImage);
            queryParameters.Add("@POOriginalFileName", parameters.POOriginalFileName);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SavePurchaseOrder", queryParameters);
        }

        public async Task<IEnumerable<PurchaseOrder_Response>> GetPurchaseOrderList(PurchaseOrderSearch_Request parameters)
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

            var result = await ListByStoredProcedure<PurchaseOrder_Response>("GetPurchaseOrderList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<PurchaseOrder_Response?> GetPurchaseOrderById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<PurchaseOrder_Response>("GetPurchaseOrderById", queryParameters)).FirstOrDefault();
        }



        public async Task<int> SavePIIssued(PIIssued_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@PurchaseOrderId", parameters.PurchaseOrderId);
            queryParameters.Add("@PIIssueDate", parameters.PIIssueDate);
            queryParameters.Add("@PINumber", parameters.PINumber);
            queryParameters.Add("@PIImage", parameters.PIImage);
            queryParameters.Add("@PIOriginalFileName", parameters.PIOriginalFileName);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SavePIIssued", queryParameters);
        }

        public async Task<IEnumerable<PIIssued_Response>> GetPIIssuedList(PIIssuedSearch_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@PurchaseOrderId", parameters.PurchaseOrderId);
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
    }
}
