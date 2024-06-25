using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diyan.Application.Models;
using Diyan.Persistence.Repositories;

namespace Diyan.Application.Interfaces
{
    public interface IAdminMasterRepository
    {
        #region Gender
        Task<int> SaveGender(Gender_Request parameters);

        Task<IEnumerable<Gender_Response>> GetGenderList(BaseSearchEntity parameters);

        Task<Gender_Response?> GetGenderById(int Id);
        #endregion

        #region UOM
        Task<int> SaveUOM(UOM_Request parameters);

        Task<IEnumerable<UOM_Response>> GetUOMList(BaseSearchEntity parameters);

        Task<UOM_Response?> GetUOMById(int Id);
        #endregion

        #region Marital Status
        Task<int> SaveMaritalStatus(MaritalStatus_Request parameters);

        Task<IEnumerable<MaritalStatus_Response>> GetMaritalStatusList(BaseSearchEntity parameters);

        Task<MaritalStatus_Response?> GetMaritalStatusById(int Id);
        #endregion

        #region Blood Group
        Task<int> SaveBloodGroup(BloodGroup_Request parameters);

        Task<IEnumerable<BloodGroup_Response>> GetBloodGroupList(BaseSearchEntity parameters);

        Task<BloodGroup_Response?> GetBloodGroupById(int Id);

        #endregion

        #region Customer Type
        Task<int> SaveCustomerType(CustomerType_Request parameters);

        Task<IEnumerable<CustomerType_Response>> GetCustomerTypeList(BaseSearchEntity parameters);

        Task<CustomerType_Response?> GetCustomerTypeById(int Id);

        #endregion

        #region Port Discharge
        Task<int> SavePortDischarge(PortDischarge_Request parameters);

        Task<IEnumerable<PortDischarge_Response>> GetPortDischargeList(BaseSearchEntity parameters);

        Task<PortDischarge_Response?> GetPortDischargeById(int Id);

        #endregion

        #region Paper Type
        Task<int> SavePaperType(PaperType_Request parameters);

        Task<IEnumerable<PaperType_Response>> GetPaperTypeList(BaseSearchEntity parameters);

        Task<PaperType_Response?> GetPaperTypeById(int Id);

        #endregion

        #region Brand
        Task<int> SaveBrand(Brand_Request parameters);

        Task<IEnumerable<Brand_Response>> GetBrandList(BaseSearchEntity parameters);

        Task<Brand_Response?> GetBrandById(int Id);

        #endregion

        #region Type of Packaging
        Task<int> SaveTypeOfPackaging(TypeOfPackaging_Request parameters);

        Task<IEnumerable<TypeOfPackaging_Response>> GetTypeOfPackagingList(BaseSearchEntity parameters);

        Task<TypeOfPackaging_Response?> GetTypeOfPackagingById(int Id);

        #endregion

        #region Packaging Type
        Task<int> SavePackagingType(PackagingType_Request parameters);

        Task<IEnumerable<PackagingType_Response>> GetPackagingTypeList(BaseSearchEntity parameters);

        Task<PackagingType_Response?> GetPackagingTypeById(int Id);

        #endregion

        #region Container Type
        Task<int> SaveContainerType(ContainerType_Request parameters);

        Task<IEnumerable<ContainerType_Response>> GetContainerTypeList(BaseSearchEntity parameters);

        Task<ContainerType_Response?> GetContainerTypeById(int Id);

        #endregion

        #region Currency Type
        Task<int> SaveCurrencyType(CurrencyType_Request parameters);

        Task<IEnumerable<CurrencyType_Response>> GetCurrencyTypeList(BaseSearchEntity parameters);

        Task<CurrencyType_Response?> GetCurrencyTypeById(int Id);

        #endregion

        #region Delivery Terms
        Task<int> SaveDeliveryTerms(DeliveryTerms_Request parameters);

        Task<IEnumerable<DeliveryTerms_Response>> GetDeliveryTermsList(BaseSearchEntity parameters);

        Task<DeliveryTerms_Response?> GetDeliveryTermsById(int Id);

        #endregion

        #region Payment Terms
        Task<int> SavePaymentTerms(PaymentTerms_Request parameters);

        Task<IEnumerable<PaymentTerms_Response>> GetPaymentTermsList(BaseSearchEntity parameters);

        Task<PaymentTerms_Response?> GetPaymentTermsById(int Id);

        #endregion

        #region Production Status
        Task<int> SaveProductionStatus(ProductionStatus_Request parameters);

        Task<IEnumerable<ProductionStatus_Response>> GetProductionStatusList(BaseSearchEntity parameters);

        Task<ProductionStatus_Response?> GetProductionStatusById(int Id);

        #endregion

        #region BRC
        Task<int> SaveBRC(BRC_Request parameters);

        Task<IEnumerable<BRC_Response>> GetBRCList(BaseSearchEntity parameters);

        Task<BRC_Response?> GetBRCById(int Id);

        #endregion

        #region Forwarding
        Task<int> SaveForwarding(Forwarding_Request parameters);

        Task<IEnumerable<Forwarding_Response>> GetForwardingList(BaseSearchEntity parameters);

        Task<Forwarding_Response?> GetForwardingById(int Id);

        #endregion

        #region Tracking Status
        Task<int> SaveTrackingStatus(TrackingStatus_Request parameters);

        Task<IEnumerable<TrackingStatus_Response>> GetTrackingStatusList(BaseSearchEntity parameters);

        Task<TrackingStatus_Response?> GetTrackingStatusById(int Id);

        #endregion
    }
}
