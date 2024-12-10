using Diyan.Application.Helpers;
using Diyan.Application.Interfaces;
using Diyan.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Diyan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageMISController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IManageMISRepository _manageMISRepository;

        public ManageMISController(IManageMISRepository manageMISRepository)
        {
            _manageMISRepository = manageMISRepository;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetMIS_PITrackingList(MIS_PITracking_Search parameters)
        {
            IEnumerable<MIS_PITrackingList_Response> lstUsers = await _manageMISRepository.GetMIS_PITrackingList(parameters);
            _response.Data = lstUsers.ToList();
            _response.Total = parameters.Total;
            return _response;
        }
    }
}
