using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diyan.Application.Helpers;
using Diyan.Application.Interfaces;
using Diyan.Application.Models;

namespace Diyan.Persistence.Repositories
{
    public class AdminMasterRepository : GenericRepository, IAdminMasterRepository
    {
        private IConfiguration _configuration;

        public AdminMasterRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        #region Gender
        public async Task<int> SaveGender(Gender_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@GenderName", parameters.GenderName.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveGender", queryParameters);
        }

        public async Task<IEnumerable<Gender_Response>> GetGenderList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<Gender_Response>("GetGenderList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<Gender_Response?> GetGenderById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<Gender_Response>("GetGenderById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region UOM
        public async Task<int> SaveUOM(UOM_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@UOMName", parameters.UOMName.SanitizeValue());
            queryParameters.Add("@UOMDesc", parameters.UOMDesc.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveUOM", queryParameters);
        }

        public async Task<IEnumerable<UOM_Response>> GetUOMList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<UOM_Response>("GetUOMList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<UOM_Response?> GetUOMById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<UOM_Response>("GetUOMById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Marital Status
        public async Task<int> SaveMaritalStatus(MaritalStatus_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@MaritalStatus", parameters.MaritalStatus);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveMaritalStatus", queryParameters);
        }

        public async Task<IEnumerable<MaritalStatus_Response>> GetMaritalStatusList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<MaritalStatus_Response>("GetMaritalStatusList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<MaritalStatus_Response?> GetMaritalStatusById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<MaritalStatus_Response>("GetMaritalStatusById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Blood Group
        public async Task<int> SaveBloodGroup(BloodGroup_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@BloodGroup", parameters.BloodGroup);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveBloodGroup", queryParameters);
        }

        public async Task<IEnumerable<BloodGroup_Response>> GetBloodGroupList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<BloodGroup_Response>("GetBloodGroupList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<BloodGroup_Response?> GetBloodGroupById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<BloodGroup_Response>("GetBloodGroupById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Customer Type
        public async Task<int> SaveCustomerType(CustomerType_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@CustomerType", parameters.CustomerType);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveCustomerType", queryParameters);
        }

        public async Task<IEnumerable<CustomerType_Response>> GetCustomerTypeList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<CustomerType_Response>("GetCustomerTypeList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<CustomerType_Response?> GetCustomerTypeById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<CustomerType_Response>("GetCustomerTypeById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Port Discharge
        public async Task<int> SavePortDischarge(PortDischarge_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@PortCode", parameters.PortCode);
            queryParameters.Add("@PortDischarge", parameters.PortDischarge);
            queryParameters.Add("@CountryId", parameters.CountryId);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SavePortDischarge", queryParameters);
        }

        public async Task<IEnumerable<PortDischarge_Response>> GetPortDischargeList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<PortDischarge_Response>("GetPortDischargeList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<PortDischarge_Response?> GetPortDischargeById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<PortDischarge_Response>("GetPortDischargeById", queryParameters)).FirstOrDefault();
        }
        #endregion

        #region Paper Type
        public async Task<int> SavePaperType(PaperType_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@PaperType", parameters.PaperType);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SavePaperType", queryParameters);
        }

        public async Task<IEnumerable<PaperType_Response>> GetPaperTypeList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<PaperType_Response>("GetPaperTypeList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<PaperType_Response?> GetPaperTypeById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<PaperType_Response>("GetPaperTypeById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Brand
        public async Task<int> SaveBrand(Brand_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@PaperTypeId", parameters.PaperTypeId);
            queryParameters.Add("@Brand", parameters.Brand);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveBrand", queryParameters);
        }

        public async Task<IEnumerable<Brand_Response>> GetBrandList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<Brand_Response>("GetBrandList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<Brand_Response?> GetBrandById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<Brand_Response>("GetBrandById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Type of Packaging
        public async Task<int> SaveTypeOfPackaging(TypeOfPackaging_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@TypeOfPackaging", parameters.TypeOfPackaging);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveTypeOfPackaging", queryParameters);
        }

        public async Task<IEnumerable<TypeOfPackaging_Response>> GetTypeOfPackagingList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<TypeOfPackaging_Response>("GetTypeOfPackagingList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<TypeOfPackaging_Response?> GetTypeOfPackagingById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<TypeOfPackaging_Response>("GetTypeOfPackagingById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Packaging Type
        public async Task<int> SavePackagingType(PackagingType_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@PackagingType", parameters.PackagingType);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SavePackagingType", queryParameters);
        }

        public async Task<IEnumerable<PackagingType_Response>> GetPackagingTypeList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<PackagingType_Response>("GetPackagingTypeList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<PackagingType_Response?> GetPackagingTypeById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<PackagingType_Response>("GetPackagingTypeById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Container Type
        public async Task<int> SaveContainerType(ContainerType_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@ContainerType", parameters.ContainerType);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveContainerType", queryParameters);
        }

        public async Task<IEnumerable<ContainerType_Response>> GetContainerTypeList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<ContainerType_Response>("GetContainerTypeList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<ContainerType_Response?> GetContainerTypeById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<ContainerType_Response>("GetContainerTypeById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Currency Type
        public async Task<int> SaveCurrencyType(CurrencyType_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@CurrencyType", parameters.CurrencyType);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveCurrencyType", queryParameters);
        }

        public async Task<IEnumerable<CurrencyType_Response>> GetCurrencyTypeList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<CurrencyType_Response>("GetCurrencyTypeList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<CurrencyType_Response?> GetCurrencyTypeById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<CurrencyType_Response>("GetCurrencyTypeById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Delivery Terms
        public async Task<int> SaveDeliveryTerms(DeliveryTerms_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@DeliveryTerms", parameters.DeliveryTerms);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveDeliveryTerms", queryParameters);
        }

        public async Task<IEnumerable<DeliveryTerms_Response>> GetDeliveryTermsList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<DeliveryTerms_Response>("GetDeliveryTermsList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<DeliveryTerms_Response?> GetDeliveryTermsById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<DeliveryTerms_Response>("GetDeliveryTermsById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Payment Terms
        public async Task<int> SavePaymentTerms(PaymentTerms_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@PaymentTerms", parameters.PaymentTerms);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SavePaymentTerms", queryParameters);
        }

        public async Task<IEnumerable<PaymentTerms_Response>> GetPaymentTermsList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<PaymentTerms_Response>("GetPaymentTermsList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<PaymentTerms_Response?> GetPaymentTermsById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<PaymentTerms_Response>("GetPaymentTermsById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Production Status
        public async Task<int> SaveProductionStatus(ProductionStatus_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@ProductionStatus", parameters.ProductionStatus);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveProductionStatus", queryParameters);
        }

        public async Task<IEnumerable<ProductionStatus_Response>> GetProductionStatusList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<ProductionStatus_Response>("GetProductionStatusList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<ProductionStatus_Response?> GetProductionStatusById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<ProductionStatus_Response>("GetProductionStatusById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region BRC
        public async Task<int> SaveBRC(BRC_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@BRC", parameters.BRC);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveBRC", queryParameters);
        }

        public async Task<IEnumerable<BRC_Response>> GetBRCList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<BRC_Response>("GetBRCList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<BRC_Response?> GetBRCById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<BRC_Response>("GetBRCById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Forwarding
        public async Task<int> SaveForwarding(Forwarding_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@Forwarding", parameters.Forwarding);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveForwarding", queryParameters);
        }

        public async Task<IEnumerable<Forwarding_Response>> GetForwardingList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<Forwarding_Response>("GetForwardingList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<Forwarding_Response?> GetForwardingById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<Forwarding_Response>("GetForwardingById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Tracking Status
        public async Task<int> SaveTrackingStatus(TrackingStatus_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@TrackingStatus", parameters.TrackingStatus);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveTrackingStatus", queryParameters);
        }

        public async Task<IEnumerable<TrackingStatus_Response>> GetTrackingStatusList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<TrackingStatus_Response>("GetTrackingStatusList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<TrackingStatus_Response?> GetTrackingStatusById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<TrackingStatus_Response>("GetTrackingStatusById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Payment Received
        public async Task<int> SavePaymentReceived(PaymentReceived_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@PaymentReceived", parameters.PaymentReceived);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SavePaymentReceived", queryParameters);
        }

        public async Task<IEnumerable<PaymentReceived_Response>> GetPaymentReceivedList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<PaymentReceived_Response>("GetPaymentReceivedList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<PaymentReceived_Response?> GetPaymentReceivedById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<PaymentReceived_Response>("GetPaymentReceivedById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Transporter
        public async Task<int> SaveTransporter(Transporter_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@Transporter", parameters.Transporter);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveTransporter", queryParameters);
        }

        public async Task<IEnumerable<Transporter_Response>> GetTransporterList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<Transporter_Response>("GetTransporterList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<Transporter_Response?> GetTransporterById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<Transporter_Response>("GetTransporterById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Forwarder
        public async Task<int> SaveForwarder(Forwarder_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@Forwarder", parameters.Forwarder);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveForwarder", queryParameters);
        }

        public async Task<IEnumerable<Forwarder_Response>> GetForwarderList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<Forwarder_Response>("GetForwarderList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<Forwarder_Response?> GetForwarderById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<Forwarder_Response>("GetForwarderById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region CHA
        public async Task<int> SaveCHA(CHA_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@CHA", parameters.CHA);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveCHA", queryParameters);
        }

        public async Task<IEnumerable<CHA_Response>> GetCHAList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<CHA_Response>("GetCHAList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<CHA_Response?> GetCHAById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<CHA_Response>("GetCHAById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Bank
        public async Task<int> SaveBank(Bank_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@BankName", parameters.BankName.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveBank", queryParameters);
        }

        public async Task<IEnumerable<Bank_Response>> GetBankList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<Bank_Response>("GetBankList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<Bank_Response?> GetBankById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<Bank_Response>("GetBankById", queryParameters)).FirstOrDefault();
        }

        #endregion

        #region Port
        public async Task<int> SavePort(Port_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@PortName", parameters.PortName.SanitizeValue());
            queryParameters.Add("@PortCode", parameters.PortCode.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SavePort", queryParameters);
        }

        public async Task<IEnumerable<Port_Response>> GetPortList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<Port_Response>("GetPortList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<Port_Response?> GetPortById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<Port_Response>("GetPortById", queryParameters)).FirstOrDefault();
        }

        #endregion
    }
}
