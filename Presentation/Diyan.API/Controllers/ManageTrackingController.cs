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


        #region Purchase Order

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SavePurchaseOrder(PurchaseOrder_Request parameters)
        {
            // PO Upload
            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.PO_Image_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.PO_Image_Base64, "\\Uploads\\ManageTracking\\", parameters.PO_POOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.PO_POImage = vUploadFile;
                }
            }

            if (parameters.PaymentReceived_Or_LCReceivedDetails != null)
            {
                parameters.PLR_PaymentOrLCReceived = parameters.PaymentReceived_Or_LCReceivedDetails.PaymentOrLCReceived;
                parameters.PLR_PaymentOrLCClosed = parameters.PaymentReceived_Or_LCReceivedDetails.PaymentOrLCClosed;
            }

            int result = await _manageTrackingRepository.SavePurchaseOrder(parameters);

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

                #region PI Issued

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
                        Id = vPIIssuedItem.Id,
                        PurchaseOrderId = result,
                        PIIssueDate = vPIIssuedItem.PIIssueDate,
                        PINumber = vPIIssuedItem.PINumber,
                        PIImage = vPIIssuedItem.PIImage,
                        PIOriginalFileName = vPIIssuedItem.PIOriginalFileName,
                        IsActive = vPIIssuedItem.IsActive,
                        Remark = vPIIssuedItem.Remark,
                        StatusId = vPIIssuedItem.StatusId,

                    };

                    int resultvPIIssued = await _manageTrackingRepository.SavePIIssued(vPIIssuedObj);
                }

                #endregion

                #region Payment Received Or LC Received

                if (parameters.PaymentReceived_Or_LCReceivedDetails != null)
                {
                    if (parameters.PaymentReceived_Or_LCReceivedDetails.PaymentOrLCReceived == 1)
                    {
                        foreach (var itemList in parameters.PaymentReceived_Or_LCReceivedDetails.PaymentReceivedDetail)
                        {
                            var vPaymentReceivedObj = new PO_PaymentReceived_Request()
                            {
                                Id = itemList.Id,
                                PurchaseOrderId = result,
                                InvoiceNumber = itemList.InvoiceNumber,
                                PaymentTermsId = itemList.PaymentTermsId,
                                PaymentReceivedDate = itemList.PaymentReceivedDate,
                                CurrencyTypeId = itemList.CurrencyTypeId,
                                Amount = itemList.Amount,
                                PaymentReceivedId = itemList.PaymentReceivedId
                            };

                            int resultPaymentReceived = await _manageTrackingRepository.SavePurchaseOrderPaymentReceived(vPaymentReceivedObj);
                        }
                    }
                    else if (parameters.PaymentReceived_Or_LCReceivedDetails.PaymentOrLCReceived == 2)
                    {
                        foreach (var itemList in parameters.PaymentReceived_Or_LCReceivedDetails.LCReceivedDetail)
                        {
                            // PO Upload
                            if (!string.IsNullOrWhiteSpace(itemList.Image_Base64))
                            {
                                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(itemList.Image_Base64, "\\Uploads\\ManageTracking\\", itemList.OriginalFileName);

                                if (!string.IsNullOrWhiteSpace(vUploadFile))
                                {
                                    itemList.ImageName = vUploadFile;
                                }
                            }

                            var vPaymentReceivedObj = new PO_LCReceived_Request()
                            {
                                Id = itemList.Id,
                                PurchaseOrderId = result,
                                ReceivedDate = itemList.ReceivedDate,
                                ImageName = itemList.ImageName,
                                OriginalFileName = itemList.OriginalFileName,
                            };

                            int resultPaymentReceived = await _manageTrackingRepository.SavePurchaseOrderLCReceived(vPaymentReceivedObj);
                        }
                    }
                }



                #endregion
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetPurchaseOrderList(PurchaseOrderSearch_Request parameters)
        {
            IEnumerable<PurchaseOrder_Response> lstUsers = await _manageTrackingRepository.GetPurchaseOrderList(parameters);
            _response.Data = lstUsers.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetPurchaseOrderById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _manageTrackingRepository.GetPurchaseOrderById(Id);

                if (vResultObj != null)
                {
                    #region PI Issue

                    var piIssuedSearch = new PIIssuedSearch_Request()
                    {
                        PurchaseOrderId = vResultObj.Id,
                        CountryId = 0,
                        CustomerId = 0,
                        PINumber = "",
                        StatusId = 0
                    };

                    var vPIIssuedObj = await _manageTrackingRepository.GetPIIssuedList(piIssuedSearch);
                    foreach (var item in vPIIssuedObj.Where(x => x.StatusId == 1 || x.StatusId == 3).ToList())
                    {
                        var vPIIssued = new PIIssued_Response()
                        {
                            Id = item.Id,
                            PurchaseOrderId = item.PurchaseOrderId,
                            CountryId = item.CountryId,
                            CountryName = item.CountryName,
                            CustomerId = item.CustomerId,
                            PO_PONumber = item.PINumber,

                            PIIssueDate = item.PIIssueDate,
                            PINumber = item.PINumber,
                            PIOriginalFileName = item.PIOriginalFileName,
                            PIImage = item.PIImage,
                            PIImageURL = item.PIImageURL,
                            Remark = item.Remark,
                            StatusId = item.StatusId,
                            StatusName = item.StatusName,
                        };

                        vResultObj.PIIssuedList.Add(vPIIssued);
                    }

                    #endregion

                    #region PI Confirmation

                    foreach (var item in vPIIssuedObj.Where(x => x.StatusId == 2).ToList())
                    {
                        var vPIConfirmation = new PIConfirmation_Response()
                        {
                            Id = item.Id,
                            PIIssueDate = item.PIIssueDate,
                            PINumber = item.PINumber,
                            Remark = item.Remark,
                            StatusId = item.StatusId,
                            StatusName = item.StatusName,
                        };

                        vResultObj.PIConfirmationList.Add(vPIConfirmation);
                    }

                    #endregion

                    #region Payment Received Or LC Received

                    var vPO_PaymentReceived_Or_LCReceived_Search = new PO_PaymentReceived_Or_LCReceived_Search()
                    {
                        PurchaseOrderId = vResultObj.Id,
                    };

                    if (vResultObj.PLR_PaymentOrLCReceived != null && vResultObj.PLR_PaymentOrLCReceived == 1)
                    {
                        var lstPaymentReceived_Or_LCReceived = await _manageTrackingRepository.GetPaymentReceivedList(vPO_PaymentReceived_Or_LCReceived_Search);
                        foreach (var item in lstPaymentReceived_Or_LCReceived)
                        {
                            var vPO_PaymentReceived_Response = new PO_PaymentReceived_Response()
                            {
                                Id = item.Id,
                                PurchaseOrderId = item.PurchaseOrderId,

                                InvoiceNumber = item.InvoiceNumber,
                                PaymentTermsId = item.PaymentTermsId,
                                PaymentTerms = item.PaymentTerms,
                                PaymentReceivedDate = item.PaymentReceivedDate,
                                CurrencyTypeId = item.CurrencyTypeId,
                                CurrencyType = item.CurrencyType,
                                Amount = item.Amount,
                                PaymentReceivedId = item.PaymentReceivedId,
                                PaymentReceived = item.PaymentReceived,
                            };

                            vResultObj.PaymentReceived_Or_LCReceivedDetail.PaymentReceivedDetail.Add(vPO_PaymentReceived_Response);
                        }
                    }

                    if (vResultObj.PLR_PaymentOrLCReceived != null && vResultObj.PLR_PaymentOrLCReceived == 2)
                    {
                        var lstPaymentReceived_Or_LCReceived = await _manageTrackingRepository.GetLCReceivedList(vPO_PaymentReceived_Or_LCReceived_Search);
                        foreach (var item in lstPaymentReceived_Or_LCReceived)
                        {
                            var vPO_LCReceived_Response = new PO_LCReceived_Response()
                            {
                                Id = item.Id,
                                PurchaseOrderId = item.PurchaseOrderId,

                                ReceivedDate = item.ReceivedDate,
                                ImageName = item.ImageName,
                                OriginalFileName = item.OriginalFileName,
                                ImageURL = item.ImageURL,
                            };

                            vResultObj.PaymentReceived_Or_LCReceivedDetail.LCReceivedDetail.Add(vPO_LCReceived_Response);
                        }
                    }


                    #endregion
                }
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region PI Issue

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetPIIssueList(PIIssuedSearch_Request parameters)
        {
            IEnumerable<PIIssued_Response> lstUsers = await _manageTrackingRepository.GetPIIssuedList(parameters);
            _response.Data = lstUsers.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        #endregion

        #region Payment Received Or LC Received

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetPaymentReceived_Or_LCReceivedList(PO_PaymentReceived_Or_LCReceived_Search parameters)
        {
            IEnumerable<PO_PaymentReceived_Or_LCReceivedList_Response> lstUsers = await _manageTrackingRepository.GetPaymentReceived_Or_LCReceivedList(parameters);
            _response.Data = lstUsers.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        //[Route("[action]")]
        //[HttpPost]
        //public async Task<ResponseModel> GetPaymentReceivedList(PO_PaymentReceived_Or_LCReceived_Search parameters)
        //{
        //    IEnumerable<PO_PaymentReceived_Response> lstUsers = await _manageTrackingRepository.GetPaymentReceivedList(parameters);
        //    _response.Data = lstUsers.ToList();
        //    _response.Total = parameters.Total;
        //    return _response;
        //}

        //[Route("[action]")]
        //[HttpPost]
        //public async Task<ResponseModel> GetLCReceivedList(PO_PaymentReceived_Or_LCReceived_Search parameters)
        //{
        //    IEnumerable<PO_LCReceived_Response> lstUsers = await _manageTrackingRepository.GetLCReceivedList(parameters);
        //    _response.Data = lstUsers.ToList();
        //    _response.Total = parameters.Total;
        //    return _response;
        //}

        #endregion
    }
}
