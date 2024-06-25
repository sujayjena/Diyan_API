using Diyan.Application.Enums;
using Diyan.Application.Helpers;
using Diyan.Application.Interfaces;
using Diyan.Application.Models;
using Diyan.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Diyan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageTrackingController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IManageTrackingRepository _manageTrackingRepository;
        private IFileManager _fileManager;

        public ManageTrackingController(IManageTrackingRepository manageTrackingRepository, IFileManager fileManager)
        {
            _manageTrackingRepository = manageTrackingRepository;
            _fileManager = fileManager;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SavePurchaseOrderTracking(PurchaseOrderTracking_Request parameters)
        {
            // PO Upload
            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.POImage_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.POImage_Base64, "\\Uploads\\ManageTracking\\", parameters.POOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.POImage = vUploadFile;
                }
            }

            int result = await _manageTrackingRepository.SavePurchaseOrderTracking(parameters);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.ReocrdExists)
            {
                _response.Message = "Record is already exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record details saved sucessfully";

                // PI Issued
                foreach (var vPIIssuedItem in parameters.PIIssuedList)
                {
                    // PO Upload
                    if (parameters! != null && !string.IsNullOrWhiteSpace(vPIIssuedItem.PIImage_Base64))
                    {
                        var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(vPIIssuedItem.PIImage_Base64, "\\Uploads\\ManageTracking\\", vPIIssuedItem.PIOriginalFileName);

                        if (!string.IsNullOrWhiteSpace(vUploadFile))
                        {
                            vPIIssuedItem.PIImage = vUploadFile;
                        }
                    }

                    var vPIIssuedObj = new PIIssued_Request()
                    {
                        POTrackingId = result,
                        PIIssueDate = vPIIssuedItem.PIIssueDate,
                        PINumber = vPIIssuedItem.PINumber,
                        PIImage = vPIIssuedItem.PIImage,
                        PIOriginalFileName = vPIIssuedItem.PIOriginalFileName,
                        IsActive = vPIIssuedItem.IsActive
                    };

                    int resultvPIIssued = await _manageTrackingRepository.SavePIIssued(vPIIssuedObj);
                }
            }
            return _response;
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetPurchaseOrderTrackingList(PurchaseOrderTrackingSearch_Request parameters)
        {
            IEnumerable<PurchaseOrderTracking_Response> lstUsers = await _manageTrackingRepository.GetPurchaseOrderTrackingList(parameters);
            _response.Data = lstUsers.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetPurchaseOrderTrackingById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _manageTrackingRepository.GetPurchaseOrderTrackingById(Id);

                if (vResultObj != null)
                {
                    var piIssuedSearch = new PIIssuedSearch_Request()
                    {
                        POTrackingId = vResultObj.Id
                    };

                    var vPIIssuedObj = await _manageTrackingRepository.GetPIIssuedList(piIssuedSearch);

                    foreach (var item in vPIIssuedObj)
                    {
                        var vPIIssued = new PIIssued_Response()
                        {
                            Id = item.Id,
                            POTrackingId = item.POTrackingId,
                            PIIssueDate = item.PIIssueDate,
                            PINumber = item.PINumber,
                            PIOriginalFileName = item.PIOriginalFileName,
                            PIImage = item.PIImage,
                            PIImageURL = item.PIImageURL,
                            IsActive = item.IsActive,
                            CreatedDate = item.CreatedDate,
                            CreatedBy = item.CreatedBy,
                            CreatorName = item.CreatorName,
                            ModifiedDate = item.ModifiedDate,
                            ModifiedBy = item.ModifiedBy,
                            ModifierName = item.ModifierName,
                        };

                        vResultObj.PIIssuedList.Add(vPIIssued);
                    }
                }
                _response.Data = vResultObj;
            }
            return _response;
        }

    }
}
