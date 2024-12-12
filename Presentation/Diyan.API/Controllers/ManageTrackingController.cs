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

            if (parameters.Id > 0 && parameters.PaymentReceived_Or_LCReceivedDetails != null)
            {
                parameters.PLR_PaymentOrLCReceived = parameters.PaymentReceived_Or_LCReceivedDetails.PaymentOrLCReceived;
                parameters.PLR_IsPaymentOrLCClosed = parameters.PaymentReceived_Or_LCReceivedDetails.PaymentOrLCClosed;
            }

            // Booking Issued Image
            if (parameters.Id > 0 && !string.IsNullOrWhiteSpace(parameters.BI_Image_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.BI_Image_Base64, "\\Uploads\\ManageTracking\\", parameters.BI_OriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.BI_Image = vUploadFile;
                }
            }

            // Final BL Issued Image
            if (parameters.Id > 0 && !string.IsNullOrWhiteSpace(parameters.FBI_FinalBIImage_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.FBI_FinalBIImage_Base64, "\\Uploads\\ManageTracking\\", parameters.FBI_FinalBIOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.FBI_FinalBIImage = vUploadFile;
                }
            }

            // Payment Received Image
            if (parameters.Id > 0 && !string.IsNullOrWhiteSpace(parameters.PR_Image_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.PR_Image_Base64, "\\Uploads\\ManageTracking\\", parameters.PR_OriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.PR_Image = vUploadFile;
                }
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

                var vResultPurchaseOrderObj = await _manageTrackingRepository.GetPurchaseOrderById(result);


                #region PI Issued

                if (parameters.Id > 0 && vResultPurchaseOrderObj.PO_IsPOStatusClosed == true)
                {
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
                }

                #endregion

                #region Payment Received Or LC Received

                if (parameters.Id > 0 && vResultPurchaseOrderObj.PO_IsPOStatusClosed == true && vResultPurchaseOrderObj.PII_IsClosed == true && vResultPurchaseOrderObj.PIC_IsConfirmed == true)
                {

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
                                    PaymentReceivedId = itemList.PaymentReceivedId,
                                    POAmount = itemList.POAmount,
                                    RemainingAmount = itemList.RemainingAmount,
                                    TotalReceivedAmount = itemList.TotalReceivedAmount,
                                    BankReferenceNumber = itemList.BankReferenceNumber,
                                    InvoiceGenerateDate = itemList.InvoiceGenerateDate,
                                    PINumber = itemList.PINumber,
                                };

                                int resultPaymentReceived = await _manageTrackingRepository.SavePurchaseOrderPaymentReceived(vPaymentReceivedObj);

                                // Image Save
                                if (resultPaymentReceived > 0)
                                {
                                    foreach (var vimgItem in itemList.PurchaseOrderPaymentReceivedImagesList)
                                    {
                                        string vImageName = "";
                                        // Image Upload
                                        if (!string.IsNullOrWhiteSpace(vimgItem.Image_Base64))
                                        {
                                            var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(vimgItem.Image_Base64, "\\Uploads\\ManageTracking\\", vimgItem.ImageOriginalFileName);

                                            if (!string.IsNullOrWhiteSpace(vUploadFile))
                                            {
                                                vImageName = vUploadFile;
                                            }
                                        }

                                        var vPOPaymentReceivedImagesObj = new PurchaseOrderPaymentReceivedImages_Request()
                                        {
                                            Id = vimgItem.Id,
                                            PurchaseOrderPaymentReceivedId = resultPaymentReceived,
                                            ImageFileName = vImageName,
                                            ImageOriginalFileName = vimgItem.ImageOriginalFileName,
                                        };

                                        int resultPaymentReceivedImage = await _manageTrackingRepository.SavePurchaseOrderPaymentReceivedImages(vPOPaymentReceivedImagesObj);
                                    }
                                }
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
                                    LCNumber = itemList.LCNumber,
                                    POAmount = itemList.POAmount,
                                    PINumber = itemList.PINumber,
                                    ImageName = itemList.ImageName,
                                    OriginalFileName = itemList.OriginalFileName,
                                };

                                int resultPaymentReceived = await _manageTrackingRepository.SavePurchaseOrderLCReceived(vPaymentReceivedObj);
                            }
                        }
                    }
                }

                #endregion

                #region Containers UnderLoading Images

                if (parameters.Id > 0 && vResultPurchaseOrderObj.PLR_IsPaymentOrLCClosed == true && parameters.ContainersUnderLoadingList.Count > 0)
                {
                    foreach (var vParentItem in parameters.ContainersUnderLoadingList)
                    {
                        var vContainersUnderLoading_RequestObj = new ContainersUnderLoading_Request()
                        {
                            Id = vParentItem.Id,
                            PurchaseOrderId = result,
                            ContainerCount = vParentItem.ContainerCount,
                        };

                        int resultvPIIssued = await _manageTrackingRepository.SaveContainersUnderLoading(vContainersUnderLoading_RequestObj);

                        // Image Save
                        if (resultvPIIssued > 0)
                        {
                            foreach (var vimgItem in vParentItem.ContainersUnderLoadingImagesList)
                            {
                                string vImageName = "";
                                // PO Upload
                                if (!string.IsNullOrWhiteSpace(vimgItem.ContainerOriginalImage_Base64))
                                {
                                    var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(vimgItem.ContainerOriginalImage_Base64, "\\Uploads\\ManageTracking\\", vimgItem.ContainerOriginalFileName);

                                    if (!string.IsNullOrWhiteSpace(vUploadFile))
                                    {
                                        vImageName = vUploadFile;
                                    }
                                }

                                var vPIIssuedObj = new ContainersUnderLoadingImages_Request()
                                {
                                    Id = vimgItem.Id,
                                    ContainersUnderLoadingId = resultvPIIssued,
                                    ContainerImage = vImageName,
                                    ContainerOriginalFileName = vimgItem.ContainerOriginalFileName,
                                };

                                int resultContainer = await _manageTrackingRepository.SaveContainersUnderLoadingImages(vPIIssuedObj);
                            }
                        }
                    }
                }

                #endregion

                #region Invoice

                if (parameters.Id > 0 && vResultPurchaseOrderObj.CUL_IsContainersUnderLoadingClose == true)
                {
                    foreach (var vPIIssuedItem in parameters.InvoiceList)
                    {
                        // PO Upload
                        if (parameters! != null && !string.IsNullOrWhiteSpace(vPIIssuedItem.Invoice_Base64))
                        {
                            var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(vPIIssuedItem.Invoice_Base64, "\\Uploads\\ManageTracking\\", vPIIssuedItem.InvoiceOriginalFileName);

                            if (!string.IsNullOrWhiteSpace(vUploadFile))
                            {
                                vPIIssuedItem.InvoiceImage = vUploadFile;
                            }
                        }

                        var vInvoiceObj = new Invoice_Request()
                        {
                            Id = vPIIssuedItem.Id,
                            PurchaseOrderId = result,
                            InvoiceGeneratedDate= vPIIssuedItem.InvoiceGeneratedDate,
                            InvoiceNumber = vPIIssuedItem.InvoiceNumber,
                            InvoiceAmount = vPIIssuedItem.InvoiceAmount,
                            Quantity = vPIIssuedItem.Quantity,
                            InvoiceImage = vPIIssuedItem.InvoiceImage,
                            InvoiceOriginalFileName = vPIIssuedItem.InvoiceOriginalFileName,
                        };

                        int resultInvoice = await _manageTrackingRepository.SaveInvoice(vInvoiceObj);
                    }
                }

                #endregion

                #region BI Draft

                // Image Save
                foreach (var vimgItem in parameters.BIDraftIssuedImagesList)
                {
                    string vImageName = "";
                    // PO Upload
                    if (!string.IsNullOrWhiteSpace(vimgItem.Image_Base64))
                    {
                        var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(vimgItem.Image_Base64, "\\Uploads\\ManageTracking\\", vimgItem.OriginalFileName);

                        if (!string.IsNullOrWhiteSpace(vUploadFile))
                        {
                            vImageName = vUploadFile;
                        }
                    }

                    var vPIIssuedObj = new BIDraftIssuedImages_Request()
                    {
                        Id = vimgItem.Id,
                        PurchaseOrderId = result,
                        ImageName = vImageName,
                        OriginalFileName = vimgItem.OriginalFileName,
                    };

                    int resultContainer = await _manageTrackingRepository.SaveBIDraftIssuedImages(vPIIssuedObj);
                }

                #endregion
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetPurchaseOrderList(PurchaseOrderSearch_Request parameters)
        {
            IEnumerable<PurchaseOrderList_Response> lstUsers = await _manageTrackingRepository.GetPurchaseOrderList(parameters);
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

                    var piIssuedSearch = new PIIssued_Search()
                    {
                        PurchaseOrderId = vResultObj.Id,
                        CountryId = 0,
                        CustomerId = 0,
                        PINumber = "",
                        StatusId = 0
                    };

                    var vPIIssuedObj = await _manageTrackingRepository.GetPIIssuedList(piIssuedSearch);
                    foreach (var item in vPIIssuedObj)
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
                            PIImage = item.PIImage,
                            PIOriginalFileName = item.PIOriginalFileName,
                            PIImageURL = item.PIImageURL,
                            StatusId = item.StatusId,
                            StatusName = item.StatusName,

                            CreatedBy = item.CreatedBy,
                            CreatedDate = item.CreatedDate,
                            CreatorName = item.CreatorName,

                            ModifiedBy = item.ModifiedBy,
                            ModifiedDate = item.ModifiedDate,
                            ModifierName = item.ModifierName
                        };

                        #region PI Issued Log

                        var PIIssuedLog_Search = new PIIssuedLog_Search()
                        {
                            PIIssuedId = item.Id,
                        };

                        var vPIIssuedLogObjList = await _manageTrackingRepository.GetPIIssuedLogListById(PIIssuedLog_Search);
                        foreach (var itemLog in vPIIssuedLogObjList)
                        {
                            var vPIIssuedLog = new PIIssuedLog_Response()
                            {
                                PIIssuedId = itemLog.PIIssuedId,
                                Remarks = itemLog.Remarks,
                                StatusId = itemLog.StatusId,
                                StatusName = itemLog.StatusName,
                                CreatedBy = itemLog.CreatedBy,
                                CreatedDate = itemLog.CreatedDate,
                                CreatorName = itemLog.CreatorName,
                            };

                            vPIIssued.PIIssuedLogList.Add(vPIIssuedLog);
                        }

                        #endregion

                        vResultObj.PIIssuedList.Add(vPIIssued);
                    }

                    #endregion

                    #region PI Confirmation

                    foreach (var item in vPIIssuedObj.Where(x => x.StatusId == 2).ToList())
                    {
                        var PIIssuedLog_Search = new PIIssuedLog_Search() { PIIssuedId = item.Id };
                        var vPIIssuedLogObjList = _manageTrackingRepository.GetPIIssuedLogListById(PIIssuedLog_Search).Result.FirstOrDefault();

                        var vPIConfirmation = new PIConfirmation_Response()
                        {
                            Id = item.Id,
                            PIIssueDate = item.PIIssueDate,
                            PINumber = item.PINumber,
                            PIImage = item.PIImage,
                            PIOriginalFileName = item.PIOriginalFileName,
                            PIImageURL = item.PIImageURL,
                            Remark = vPIIssuedLogObjList != null ? vPIIssuedLogObjList.Remarks : string.Empty,
                            StatusId = item.StatusId,
                            StatusName = item.StatusName,

                            CreatedBy = item.CreatedBy,
                            CreatedDate = item.CreatedDate,
                            CreatorName = item.CreatorName,

                            ModifiedBy = item.ModifiedBy,
                            ModifiedDate = item.ModifiedDate,
                            ModifierName = item.ModifierName
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
                            var vPO_PaymentReceived_Response = new PO_PaymentReceived_Response();

                            vPO_PaymentReceived_Response.Id = item.Id;
                            vPO_PaymentReceived_Response.PurchaseOrderId = item.PurchaseOrderId;
                            vPO_PaymentReceived_Response.InvoiceNumber = item.InvoiceNumber;
                            vPO_PaymentReceived_Response.PaymentTermsId = item.PaymentTermsId;
                            vPO_PaymentReceived_Response.PaymentTerms = item.PaymentTerms;
                            vPO_PaymentReceived_Response.PaymentReceivedDate = item.PaymentReceivedDate;
                            vPO_PaymentReceived_Response.CurrencyTypeId = item.CurrencyTypeId;
                            vPO_PaymentReceived_Response.CurrencyType = item.CurrencyType;
                            vPO_PaymentReceived_Response.Amount = item.Amount;
                            vPO_PaymentReceived_Response.PaymentReceivedId = item.PaymentReceivedId;
                            vPO_PaymentReceived_Response.PaymentReceived = item.PaymentReceived;
                            vPO_PaymentReceived_Response.POAmount = item.POAmount;
                            vPO_PaymentReceived_Response.RemainingAmount = item.RemainingAmount;
                            vPO_PaymentReceived_Response.TotalReceivedAmount = item.TotalReceivedAmount;
                            vPO_PaymentReceived_Response.BankReferenceNumber = item.BankReferenceNumber;
                            vPO_PaymentReceived_Response.InvoiceGenerateDate = item.InvoiceGenerateDate;
                            vPO_PaymentReceived_Response.PINumber = item.PINumber;

                            vPO_PaymentReceived_Response.CreatedBy = item.CreatedBy;
                            vPO_PaymentReceived_Response.CreatedDate = item.CreatedDate;
                            vPO_PaymentReceived_Response.CreatorName = item.CreatorName;

                            vPO_PaymentReceived_Response.ModifiedBy = item.ModifiedBy;
                            vPO_PaymentReceived_Response.ModifiedDate = item.ModifiedDate;
                            vPO_PaymentReceived_Response.ModifierName = item.ModifierName;

                            var vPurchaseOrderPaymentReceivedImage_Search = new PurchaseOrderPaymentReceivedImage_Search()
                            {
                                PurchaseOrderPaymentReceivedId = item.Id,
                            };

                            var vPurchaseOrderPaymentReceivedImageistObj = await _manageTrackingRepository.GetPurchaseOrderPaymentReceivedImagesById(vPurchaseOrderPaymentReceivedImage_Search);
                            foreach (var itemImg in vPurchaseOrderPaymentReceivedImageistObj)
                            {
                                var vPurchaseOrderPaymentReceivedImages_Response = new PurchaseOrderPaymentReceivedImages_Response()
                                {
                                    Id = itemImg.Id,
                                    PurchaseOrderPaymentReceivedId = item.Id,
                                    ImageFileName = itemImg.ImageFileName,
                                    ImageOriginalFileName = itemImg.ImageOriginalFileName,
                                    ImageURL = itemImg.ImageURL,
                                };
                                vPO_PaymentReceived_Response.PurchaseOrderPaymentReceivedImagesList.Add(vPurchaseOrderPaymentReceivedImages_Response);
                            }

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
                                LCNumber = item.LCNumber,
                                POAmount = item.POAmount,
                                PINumber = item.PINumber,
                                ImageName = item.ImageName,
                                OriginalFileName = item.OriginalFileName,
                                ImageURL = item.ImageURL,

                                CreatedBy = item.CreatedBy,
                                CreatedDate = item.CreatedDate,
                                CreatorName = item.CreatorName,

                                ModifiedBy = item.ModifiedBy,
                                ModifiedDate = item.ModifiedDate,
                                ModifierName = item.ModifierName
                            };

                            vResultObj.PaymentReceived_Or_LCReceivedDetail.LCReceivedDetail.Add(vPO_LCReceived_Response);
                        }
                    }


                    #endregion

                    #region Containers UnderLoading Images

                    var vContainersUnderLoading_Search = new ContainersUnderLoading_Search()
                    {
                        PurchaseOrderId = vResultObj.Id,
                    };

                    var vContainersUnderLoadingListObj = await _manageTrackingRepository.GetContainersUnderLoadingById(vContainersUnderLoading_Search);
                    foreach (var item in vContainersUnderLoadingListObj)
                    {
                        var vContainersUnderLoading_ResponseObj = new ContainersUnderLoading_Response();

                        vContainersUnderLoading_ResponseObj.Id = item.Id;
                        vContainersUnderLoading_ResponseObj.PurchaseOrderId = item.PurchaseOrderId;
                        vContainersUnderLoading_ResponseObj.ContainerCount = item.ContainerCount;

                        vContainersUnderLoading_ResponseObj.CreatedBy = item.CreatedBy;
                        vContainersUnderLoading_ResponseObj.CreatedDate = item.CreatedDate;
                        vContainersUnderLoading_ResponseObj.CreatorName = item.CreatorName;

                        vContainersUnderLoading_ResponseObj.ModifiedBy = item.ModifiedBy;
                        vContainersUnderLoading_ResponseObj.ModifiedDate = item.ModifiedDate;
                        vContainersUnderLoading_ResponseObj.ModifierName = item.ModifierName;


                        var vContainersUnderLoadingImage_Search = new ContainersUnderLoadingImages_Search()
                        {
                            ContainersUnderLoadingId = item.Id,
                        };

                        var vContainersUnderLoadingImageListObj = await _manageTrackingRepository.GetContainersUnderLoadingImagesById(vContainersUnderLoadingImage_Search);
                        foreach (var itemImg in vContainersUnderLoadingImageListObj)
                        {
                            var vContainersUnderLoadingImages_Response = new ContainersUnderLoadingImages_Response()
                            {
                                Id = itemImg.Id,
                                ContainersUnderLoadingId = item.Id,
                                ContainerImage = itemImg.ContainerImage,
                                ContainerOriginalFileName = itemImg.ContainerOriginalFileName,
                                ContainerImageURL = itemImg.ContainerImageURL,
                            };
                            vContainersUnderLoading_ResponseObj.ContainersUnderLoadingImagesList.Add(vContainersUnderLoadingImages_Response);
                        }

                        vResultObj.ContainersUnderLoadingList.Add(vContainersUnderLoading_ResponseObj);
                    }

                    #endregion

                    #region Invoice

                    var vInvoice_Search = new Invoice_Search() { PurchaseOrderId = vResultObj.Id };
                    var vInvoiceObjList = await _manageTrackingRepository.GetInvoiceList(vInvoice_Search);

                    foreach (var itemLog in vInvoiceObjList)
                    {
                        var vPIIssuedLog = new Invoice_Response()
                        {
                            Id = itemLog.Id,
                            PurchaseOrderId = itemLog.PurchaseOrderId,
                            InvoiceGeneratedDate = itemLog.InvoiceGeneratedDate,
                            InvoiceNumber = itemLog.InvoiceNumber,
                            InvoiceAmount = itemLog.InvoiceAmount,
                            Quantity = itemLog.Quantity,
                            InvoiceImage = itemLog.InvoiceImage,
                            InvoiceOriginalFileName = itemLog.InvoiceOriginalFileName,
                            InvoiceImageURL = itemLog.InvoiceImageURL,
                            CreatedBy = itemLog.CreatedBy,
                            CreatedDate = itemLog.CreatedDate,
                            CreatorName = itemLog.CreatorName,
                        };

                        vResultObj.InvoiceList.Add(vPIIssuedLog);
                    }

                    #endregion

                    #region BI Draft

                    var vBIDraftIssuedImages_Search = new BIDraftIssuedImages_Search() { PurchaseOrderId = vResultObj.Id };
                    var vBIDraftIssuedImagesObjList = await _manageTrackingRepository.GetBIDraftIssuedImagesById(vBIDraftIssuedImages_Search);

                    foreach (var itemLog in vBIDraftIssuedImagesObjList)
                    {
                        var vPIIssuedLog = new BIDraftIssuedImages_Response()
                        {
                            Id = itemLog.Id,
                            PurchaseOrderId = itemLog.PurchaseOrderId,
                            ImageName = itemLog.ImageName,
                            OriginalFileName = itemLog.OriginalFileName,
                            ImageURL = itemLog.ImageURL,
                        };

                        vResultObj.BIDraftIssuedImagesList.Add(vPIIssuedLog);
                    }

                    // Remark Log
                    var vBIDraftIssuedRemarks_Search = new BIDraftIssuedImages_Search() { PurchaseOrderId = vResultObj.Id };
                    var vBIDraftIssuedRemarksObjList = await _manageTrackingRepository.GetBIDraftIssuedRemarkLogById(vBIDraftIssuedRemarks_Search);

                    foreach (var itemLog in vBIDraftIssuedRemarksObjList)
                    {
                        var vPIIssuedLog = new BIDraftIssuedRemarkLog_Response()
                        {
                            Id = itemLog.Id,
                            PurchaseOrderId = itemLog.PurchaseOrderId,
                            Remarks = itemLog.Remarks,
                            CreatedBy = itemLog.CreatedBy,
                            CreatorName = itemLog.CreatorName,
                            CreatedDate = itemLog.CreatedDate,
                        };

                        vResultObj.BIDraftIssuedRemarkLogList.Add(vPIIssuedLog);
                    }

                    // Comments Log
                    var vBIDraftIssuedComments_Search = new BIDraftIssuedImages_Search() { PurchaseOrderId = vResultObj.Id };
                    var vBIDraftIssuedCommentsObjList = await _manageTrackingRepository.GetBIDraftIssuedCommentLogById(vBIDraftIssuedComments_Search);

                    foreach (var itemLog in vBIDraftIssuedCommentsObjList)
                    {
                        var vPIIssuedLog = new BIDraftIssuedCommentsLog_Response()
                        {
                            Id = itemLog.Id,
                            PurchaseOrderId = itemLog.PurchaseOrderId,
                            Comments = itemLog.Comments,
                            CreatedBy = itemLog.CreatedBy,
                            CreatorName = itemLog.CreatorName,
                            CreatedDate = itemLog.CreatedDate,
                        };

                        vResultObj.BIDraftIssuedCommentLogList.Add(vPIIssuedLog);
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
        public async Task<ResponseModel> DeletePIIssued(int Id)
        {
            int result = await _manageTrackingRepository.DeletePIIssued(Id);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record details deleted sucessfully";
            }
            return _response;
        }
        #endregion

        #region Payment Received Or LC Received

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> DeletePaymentReceived(int Id)
        {
            int result = await _manageTrackingRepository.DeletePaymentReceived(Id);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record details deleted sucessfully";
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> DeleteLCReceived(int Id)
        {
            int result = await _manageTrackingRepository.DeleteLCReceived(Id);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record details deleted sucessfully";
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> DeletePurchaseOrderPaymentReceivedImages(int Id)
        {
            int result = await _manageTrackingRepository.DeletePurchaseOrderPaymentReceivedImages(Id);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record details deleted sucessfully";
            }
            return _response;
        }

        #endregion

        #region Containers Under Loading

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> DeleteContainersUnderLoading(int Id)
        {
            int result = await _manageTrackingRepository.DeleteContainersUnderLoading(Id);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record details deleted sucessfully";
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> DeleteContainersUnderLoadingImages(int Id)
        {
            int result = await _manageTrackingRepository.DeleteContainersUnderLoadingImages(Id);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record details deleted sucessfully";
            }
            return _response;
        }

        #endregion

        #region Invoice

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> DeleteInvoice(int Id)
        {
            int result = await _manageTrackingRepository.DeleteInvoice(Id);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record details deleted sucessfully";
            }
            return _response;
        }

        #endregion

        #region BI Draft

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> DeleteBIDraftIssuedImages(int Id)
        {
            int result = await _manageTrackingRepository.DeleteBIDraftIssuedImages(Id);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record details deleted sucessfully";
            }
            return _response;
        }

        #endregion


        #region PI Issue

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetPINumberForSelectList()
        {
            IEnumerable<SelectListResponse> lstUsers = await _manageTrackingRepository.GetPINumberForSelectList();
            _response.Data = lstUsers.ToList();
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ValidatePINumber(string PINumber = "")
        {
            var objList = await _manageTrackingRepository.ValidatePINumber(PINumber);
            if (objList.ToList().Count > 0)
            {
                _response.Id = -1;
                _response.IsSuccess = false;
                _response.Message = "The PI Number is already Exist";
            }

            return _response;
        }

        //[Route("[action]")]
        //[HttpPost]
        //public async Task<ResponseModel> GetPIIssueList(PIIssued_Search parameters)
        //{
        //    IEnumerable<PIIssued_Response> lstUsers = await _manageTrackingRepository.GetPIIssuedList(parameters);
        //    _response.Data = lstUsers.ToList();
        //    _response.Total = parameters.Total;
        //    return _response;
        //}

        #endregion

        //#region Payment Received Or LC Received

        //[Route("[action]")]
        //[HttpPost]
        //public async Task<ResponseModel> GetPaymentReceived_Or_LCReceivedList(PO_PaymentReceived_Or_LCReceived_Search parameters)
        //{
        //    IEnumerable<PO_PaymentReceived_Or_LCReceivedList_Response> lstUsers = await _manageTrackingRepository.GetPaymentReceived_Or_LCReceivedList(parameters);
        //    _response.Data = lstUsers.ToList();
        //    _response.Total = parameters.Total;
        //    return _response;
        //}

        ////[Route("[action]")]
        ////[HttpPost]
        ////public async Task<ResponseModel> GetPaymentReceivedList(PO_PaymentReceived_Or_LCReceived_Search parameters)
        ////{
        ////    IEnumerable<PO_PaymentReceived_Response> lstUsers = await _manageTrackingRepository.GetPaymentReceivedList(parameters);
        ////    _response.Data = lstUsers.ToList();
        ////    _response.Total = parameters.Total;
        ////    return _response;
        ////}

        ////[Route("[action]")]
        ////[HttpPost]
        ////public async Task<ResponseModel> GetLCReceivedList(PO_PaymentReceived_Or_LCReceived_Search parameters)
        ////{
        ////    IEnumerable<PO_LCReceived_Response> lstUsers = await _manageTrackingRepository.GetLCReceivedList(parameters);
        ////    _response.Data = lstUsers.ToList();
        ////    _response.Total = parameters.Total;
        ////    return _response;
        ////}

        //#endregion
    }
}
