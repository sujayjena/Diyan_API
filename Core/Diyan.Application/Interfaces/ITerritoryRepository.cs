using Diyan.Application.Models;
using Diyan.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diyan.Application.Interfaces
{
    public interface ITerritoryRepository
    {
        #region Country 

        Task<int> SaveCountry(Country_Request parameters);

        Task<IEnumerable<Country_Response>> GetCountryList(BaseSearchEntity parameters);

        Task<Country_Response?> GetCountryById(long Id);

        Task<IEnumerable<CountryDataValidationErrors>> ImportCountry(List<ImportedCountry> parameters);

        #endregion

        #region State 

        Task<int> SaveState(State_Request parameters);

        Task<IEnumerable<State_Response>> GetStateList(BaseSearchEntity parameters);

        Task<State_Response?> GetStateById(long Id);

        Task<IEnumerable<StateDataValidationErrors>> ImportState(List<ImportedState> parameters);

        #endregion

        #region District 

        Task<int> SaveDistrict(District_Request parameters);

        Task<IEnumerable<District_Response>> GetDistrictList(BaseSearchEntity parameters);

        Task<District_Response?> GetDistrictById(long Id);

        Task<IEnumerable<DistrictDataValidationErrors>> ImportDistrict(List<ImportedDistrict> parameters);

        #endregion

        #region Territories 

        Task<int> SaveTerritories(Territories_Request parameters);

        Task<IEnumerable<Territories_Response>> GetTerritoriesList(BaseSearchEntity parameters);

        Task<Territories_Response?> GetTerritoriesById(long Id);

        #endregion
    }
}
