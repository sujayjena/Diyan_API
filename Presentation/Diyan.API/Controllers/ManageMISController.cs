using Diyan.Application.Helpers;
using Diyan.Application.Interfaces;
using Diyan.Application.Models;
using Diyan.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Globalization;
using Diyan.Application.Enums;

namespace Diyan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageMISController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IManageMISRepository _manageMISRepository;
        private readonly IManageTrackingRepository _manageTrackingRepository;

        public ManageMISController(IManageMISRepository manageMISRepository, IManageTrackingRepository manageTrackingRepository)
        {
            _manageMISRepository = manageMISRepository;
            _manageTrackingRepository = manageTrackingRepository;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region MIS PI Tracking

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetMIS_PITrackingList(MIS_PITracking_Search parameters)
        {
            IEnumerable<MIS_PITrackingList_Response> lstUsers = await _manageMISRepository.GetMIS_PITrackingList(parameters);
            _response.Data = lstUsers.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportMIS_PITrackingData(MIS_PITracking_Search parameters)
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            IEnumerable<MIS_PITrackingList_Response> lstSizeObj = await _manageMISRepository.GetMIS_PITrackingList(parameters);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("MIS_PI_Tracking");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "PI Number";
                    WorkSheet1.Cells[1, 2].Value = "Customer Name";
                    WorkSheet1.Cells[1, 3].Value = "Port Discharge";
                    WorkSheet1.Cells[1, 4].Value = "Shipment Schedule";
                    WorkSheet1.Cells[1, 5].Value = "Payment Term";
                    WorkSheet1.Cells[1, 6].Value = "Brand";
                    WorkSheet1.Cells[1, 7].Value = "Packing";
                    WorkSheet1.Cells[1, 8].Value = "Quantity";
                    WorkSheet1.Cells[1, 9].Value = "Commission";
                    WorkSheet1.Cells[1, 10].Value = "Paper Type";
                    WorkSheet1.Cells[1, 11].Value = "Created Date";
                    WorkSheet1.Cells[1, 12].Value = "Created By";
                   
                    recordIndex = 2;

                    foreach (var items in lstSizeObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.PINumber;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.CustomerName;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.PortDischarge;
                        WorkSheet1.Cells[recordIndex, 4].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.ShipmentSchedule;

                        WorkSheet1.Cells[recordIndex, 5].Value = items.PaymentTerms;
                        WorkSheet1.Cells[recordIndex, 6].Value = items.Brand;
                        WorkSheet1.Cells[recordIndex, 7].Value = items.TypeOfPackaging;
                        WorkSheet1.Cells[recordIndex, 8].Value = items.PO_Quantity;
                        WorkSheet1.Cells[recordIndex, 9].Value = items.PO_CommissionPerTon;
                        WorkSheet1.Cells[recordIndex, 10].Value = items.PaperType;
                        WorkSheet1.Cells[recordIndex, 11].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 11].Value = items.CreatedDate;
                        WorkSheet1.Cells[recordIndex, 12].Value = items.CreatorName;

                        recordIndex += 1;
                    }

                    WorkSheet1.Columns.AutoFit();

                    excelExportData.SaveAs(msExportDataFile);
                    msExportDataFile.Position = 0;
                    result = msExportDataFile.ToArray();
                }
            }

            if (result != null)
            {
                _response.Data = result;
                _response.IsSuccess = true;
                _response.Message = "Exported successfully";
            }

            return _response;
        }

        #endregion

        #region MIS Advance Amount

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetMIS_AdvanceAmountList(MIS_Search parameters)
        {
            IEnumerable<MIS_AdvanceAmountList_Response> lstUsers = await _manageMISRepository.GetMIS_AdvanceAmountList(parameters);
            _response.Data = lstUsers.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> UpdateMIS_AdvanceAmount(PO_PaymentReceived_Request parameters)
        {
            parameters.ModuleType = "MIS_AdvanceAmount";

            int result = await _manageTrackingRepository.SavePurchaseOrderPaymentReceived(parameters);

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
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportMIS_AdvanceAmountData(MIS_Search parameters)
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            IEnumerable<MIS_AdvanceAmountList_Response> lstSizeObj = await _manageMISRepository.GetMIS_AdvanceAmountList(parameters);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("MIS_AdvanceAmount");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "PI Number";
                    WorkSheet1.Cells[1, 2].Value = "Consignee";
                    WorkSheet1.Cells[1, 3].Value = "Payment Term";
                    WorkSheet1.Cells[1, 4].Value = "Bank";
                    WorkSheet1.Cells[1, 5].Value = "PO Amount";
                    WorkSheet1.Cells[1, 6].Value = "Advance Received";
                    WorkSheet1.Cells[1, 7].Value = "Bank Commission";
                    WorkSheet1.Cells[1, 8].Value = "Bank Reference Number";
                    WorkSheet1.Cells[1, 9].Value = "Date";
                    WorkSheet1.Cells[1, 10].Value = "Created Date";
                    WorkSheet1.Cells[1, 11].Value = "Created By";

                    recordIndex = 2;

                    foreach (var items in lstSizeObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.PINumber;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.CustomerName;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.PaymentTerms;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.Bank;
                        WorkSheet1.Cells[recordIndex, 5].Value = items.PO_Amount;
                        WorkSheet1.Cells[recordIndex, 6].Value = items.TotalReceivedAmount;
                        WorkSheet1.Cells[recordIndex, 7].Value = items.BankCommission;
                        WorkSheet1.Cells[recordIndex, 8].Value = items.BankReferenceNumber;
                        WorkSheet1.Cells[recordIndex, 9].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 9].Value = items.PaymentReceivedDate;
                        WorkSheet1.Cells[recordIndex, 10].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 10].Value = items.CreatedDate;
                        WorkSheet1.Cells[recordIndex, 11].Value = items.CreatorName;

                        if (string.IsNullOrEmpty(items.Bank) || items.BankCommission == 0)
                        {
                            WorkSheet1.Row(recordIndex).Style.Fill.PatternType = ExcelFillStyle.Solid;
                            WorkSheet1.Row(recordIndex).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Pink);
                        }

                        recordIndex += 1;
                    }

                    WorkSheet1.Columns.AutoFit();

                    excelExportData.SaveAs(msExportDataFile);
                    msExportDataFile.Position = 0;
                    result = msExportDataFile.ToArray();
                }
            }

            if (result != null)
            {
                _response.Data = result;
                _response.IsSuccess = true;
                _response.Message = "Exported successfully";
            }

            return _response;
        }

        #endregion

        #region MIS Final Invoice & Payment Record

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetMIS_FinalInvoicePaymentList(MIS_Search parameters)
        {
            IEnumerable<MIS_FinalInvoicePaymentList_Response> lstUsers = await _manageMISRepository.GetMIS_FinalInvoicePaymentList(parameters);
            _response.Data = lstUsers.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> UpdateMIS_FinalInvoicePayment(Invoice_Request parameters)
        {
            parameters.ModuleType = "MIS_FinalInvoicePayment";

            int result = await _manageTrackingRepository.SaveInvoice(parameters);

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
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportMIS_FinalInvoicePaymentData(MIS_Search parameters)
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            IEnumerable<MIS_FinalInvoicePaymentList_Response> lstSizeObj = await _manageMISRepository.GetMIS_FinalInvoicePaymentList(parameters);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("MIS_FinalInvoicePayment");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Invoice Number";
                    WorkSheet1.Cells[1, 2].Value = "Invoice Date";
                    WorkSheet1.Cells[1, 3].Value = "PI No.";
                    WorkSheet1.Cells[1, 4].Value = "Consignee";
                    WorkSheet1.Cells[1, 5].Value = "Notify Party";
                    WorkSheet1.Cells[1, 6].Value = "Port of Discharge";
                    WorkSheet1.Cells[1, 7].Value = "Destination Country";
                    WorkSheet1.Cells[1, 8].Value = "Paper Type";
                    WorkSheet1.Cells[1, 9].Value = "Type of Packing";
                    WorkSheet1.Cells[1, 10].Value = "Brand";
                    WorkSheet1.Cells[1, 11].Value = "Delivery Term";
                    WorkSheet1.Cells[1, 12].Value = "Final Qty.";
                    WorkSheet1.Cells[1, 13].Value = "CIF/CFR Value";
                    WorkSheet1.Cells[1, 14].Value = "FOB Value";
                    WorkSheet1.Cells[1, 15].Value = "Freight";
                    WorkSheet1.Cells[1, 16].Value = "Payment Term";
                    WorkSheet1.Cells[1, 17].Value = "Remitted Amount";
                    WorkSheet1.Cells[1, 18].Value = "Date";
                    WorkSheet1.Cells[1, 19].Value = "Port Code";
                    WorkSheet1.Cells[1, 20].Value = "SB No";
                    WorkSheet1.Cells[1, 21].Value = "SB Date";
                    WorkSheet1.Cells[1, 22].Value = "Created Date";
                    WorkSheet1.Cells[1, 23].Value = "Created By";

                    recordIndex = 2;

                    foreach (var items in lstSizeObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.InvoiceNumber;
                        WorkSheet1.Cells[recordIndex, 2].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.InvoiceDate;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.PINumber;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.ConsigneeName;
                        WorkSheet1.Cells[recordIndex, 5].Value = items.NotifyPartyName;
                        WorkSheet1.Cells[recordIndex, 6].Value = items.PortDischarge;
                        WorkSheet1.Cells[recordIndex, 7].Value = items.CountryName;
                        WorkSheet1.Cells[recordIndex, 8].Value = items.PaperType;
                        WorkSheet1.Cells[recordIndex, 9].Value = items.TypeOfPackaging;
                        WorkSheet1.Cells[recordIndex, 10].Value = items.Brand;
                        WorkSheet1.Cells[recordIndex, 11].Value = items.DeliveryTerms;
                        WorkSheet1.Cells[recordIndex, 12].Value = items.Quantity;
                        WorkSheet1.Cells[recordIndex, 13].Value = items.InvoiceAmount;
                        WorkSheet1.Cells[recordIndex, 14].Value = items.FOBValue;
                        WorkSheet1.Cells[recordIndex, 15].Value = items.Freight;
                        WorkSheet1.Cells[recordIndex, 16].Value = items.PaymentTerms;
                        WorkSheet1.Cells[recordIndex, 17].Value = items.TotalReceivedAmount;
                        WorkSheet1.Cells[recordIndex, 18].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 18].Value = items.PaymentReceivedDate;
                        WorkSheet1.Cells[recordIndex, 19].Value = items.PortCode;
                        WorkSheet1.Cells[recordIndex, 20].Value = items.SBNo;
                        WorkSheet1.Cells[recordIndex, 21].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 21].Value = items.SBDate;
                        WorkSheet1.Cells[recordIndex, 22].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 22].Value = items.CreatedDate;
                        WorkSheet1.Cells[recordIndex, 23].Value = items.CreatorName;

                        if (string.IsNullOrEmpty(items.PortCode) || string.IsNullOrEmpty(items.SBNo) || items.Freight == 0 || items.SBDate == null)
                        {
                            WorkSheet1.Row(recordIndex).Style.Fill.PatternType = ExcelFillStyle.Solid;
                            WorkSheet1.Row(recordIndex).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Pink);
                        }

                        recordIndex += 1;
                    }

                    WorkSheet1.Columns.AutoFit();

                    excelExportData.SaveAs(msExportDataFile);
                    msExportDataFile.Position = 0;
                    result = msExportDataFile.ToArray();
                }
            }

            if (result != null)
            {
                _response.Data = result;
                _response.IsSuccess = true;
                _response.Message = "Exported successfully";
            }

            return _response;
        }

        #endregion

        #region MIS SHIPPING Bill Record

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetMIS_SBillList(MIS_Search parameters)
        {
            IEnumerable<MIS_SBillList_Response> lstUsers = await _manageMISRepository.GetMIS_SBillList(parameters);
            _response.Data = lstUsers.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> UpdateMIS_SBill(Invoice_Request parameters)
        {
            parameters.ModuleType = "MIS_SBill";

            int result = await _manageTrackingRepository.SaveInvoice(parameters);

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
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportMIS_SBillListData(MIS_Search parameters)
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            IEnumerable<MIS_SBillList_Response> lstSizeObj = await _manageMISRepository.GetMIS_SBillList(parameters);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("MIS_SBill");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Invoice Number";
                    WorkSheet1.Cells[1, 2].Value = "Invoice Date";
                    WorkSheet1.Cells[1, 3].Value = "SB No.";
                    WorkSheet1.Cells[1, 4].Value = "SB Date";
                    WorkSheet1.Cells[1, 5].Value = "Leo Date";
                    WorkSheet1.Cells[1, 6].Value = "Port Code";
                    WorkSheet1.Cells[1, 7].Value = "Quantity";
                    WorkSheet1.Cells[1, 8].Value = "CIF/CFR Value";
                    WorkSheet1.Cells[1, 9].Value = "Freight";
                    WorkSheet1.Cells[1, 10].Value = "FOB Value";
                    WorkSheet1.Cells[1, 11].Value = "Exchange Rate";
                    WorkSheet1.Cells[1, 12].Value = "FOB Value(INR)";
                    WorkSheet1.Cells[1, 13].Value = "BRC In Bank";
                    WorkSheet1.Cells[1, 14].Value = "BRC In DGFT";
                    WorkSheet1.Cells[1, 15].Value = "DBK Value";
                    WorkSheet1.Cells[1, 16].Value = "DBK Received";
                    WorkSheet1.Cells[1, 17].Value = "IGST Amount";
                    WorkSheet1.Cells[1, 18].Value = "IGST Received";
                    WorkSheet1.Cells[1, 19].Value = "Created Date";
                    WorkSheet1.Cells[1, 20].Value = "Created By";

                    recordIndex = 2;

                    foreach (var items in lstSizeObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.InvoiceNumber;
                        WorkSheet1.Cells[recordIndex, 2].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.InvoiceDate;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.SBNo;
                        WorkSheet1.Cells[recordIndex, 4].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.SBDate;
                        WorkSheet1.Cells[recordIndex, 5].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 5].Value = items.LeoDate;
                        WorkSheet1.Cells[recordIndex, 6].Value = items.PortCode;
                        WorkSheet1.Cells[recordIndex, 7].Value = items.Quantity;
                        WorkSheet1.Cells[recordIndex, 8].Value = items.InvoiceAmount;
                        WorkSheet1.Cells[recordIndex, 9].Value = items.Freight;
                        WorkSheet1.Cells[recordIndex, 10].Value = items.FOBValue;
                        WorkSheet1.Cells[recordIndex, 11].Value = items.ExchangeRate;
                        WorkSheet1.Cells[recordIndex, 12].Value = items.TotalFOBValue;
                        WorkSheet1.Cells[recordIndex, 13].Value = items.BRCInBank;
                        WorkSheet1.Cells[recordIndex, 14].Value = items.BRCInDGFT;
                        WorkSheet1.Cells[recordIndex, 15].Value = items.DBKValue;
                        WorkSheet1.Cells[recordIndex, 16].Value = items.DBKReceived;
                        WorkSheet1.Cells[recordIndex, 17].Value = items.IGSTAmount;
                        WorkSheet1.Cells[recordIndex, 18].Value = items.IGSTReceived;
                        WorkSheet1.Cells[recordIndex, 19].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 19].Value = items.CreatedDate;
                        WorkSheet1.Cells[recordIndex, 20].Value = items.CreatorName;

                        if (string.IsNullOrEmpty(items.PortCode) || string.IsNullOrEmpty(items.SBNo) || items.Freight == 0 || items.SBDate == null || items.LeoDate == null 
                            || items.ExchangeRate == 0 || items.BRCInBank == false || items.BRCInDGFT == false || items.DBKValue == 0 || items.DBKReceived == false 
                            || items.IGSTAmount == 0 || items.IGSTReceived == false)
                        {
                            WorkSheet1.Row(recordIndex).Style.Fill.PatternType = ExcelFillStyle.Solid;
                            WorkSheet1.Row(recordIndex).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Pink);
                        }

                        recordIndex += 1;
                    }

                    WorkSheet1.Columns.AutoFit();

                    excelExportData.SaveAs(msExportDataFile);
                    msExportDataFile.Position = 0;
                    result = msExportDataFile.ToArray();
                }
            }

            if (result != null)
            {
                _response.Data = result;
                _response.IsSuccess = true;
                _response.Message = "Exported successfully";
            }

            return _response;
        }

        #endregion

        #region MIS Commission

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetMIS_CommissionList(MIS_Search parameters)
        {
            IEnumerable<MIS_CommissionList_Response> lstUsers = await _manageMISRepository.GetMIS_CommissionList(parameters);
            _response.Data = lstUsers.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> UpdateMIS_Commission(Invoice_Request parameters)
        {
            parameters.ModuleType = "MIS_Commission";

            int result = await _manageTrackingRepository.SaveInvoice(parameters);

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
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportMIS_CommissionListData(MIS_Search parameters)
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            IEnumerable<MIS_CommissionList_Response> lstSizeObj = await _manageMISRepository.GetMIS_CommissionList(parameters);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("MIS_Commission");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Invoice Number";
                    WorkSheet1.Cells[1, 2].Value = "Invoice Date";
                    WorkSheet1.Cells[1, 3].Value = "SB No.";
                    WorkSheet1.Cells[1, 4].Value = "SB Date";
                    WorkSheet1.Cells[1, 5].Value = "Exchange Rate";
                    WorkSheet1.Cells[1, 6].Value = "PI No.";
                    WorkSheet1.Cells[1, 7].Value = "Consignee";
                    WorkSheet1.Cells[1, 8].Value = "Notify Party";
                    WorkSheet1.Cells[1, 9].Value = "Destination Country";
                    WorkSheet1.Cells[1, 10].Value = "Quantity";
                    WorkSheet1.Cells[1, 11].Value = "Invoice Amount";
                    WorkSheet1.Cells[1, 12].Value = "Commission / MT";
                    WorkSheet1.Cells[1, 13].Value = "Total Commission";
                    WorkSheet1.Cells[1, 14].Value = "Commission Mentioned In S.Bill";
                    WorkSheet1.Cells[1, 15].Value = "Utilized Amount";
                    WorkSheet1.Cells[1, 16].Value = "UnUtilized Amount";
                    WorkSheet1.Cells[1, 17].Value = "Created Date";
                    WorkSheet1.Cells[1, 18].Value = "Created By";

                    recordIndex = 2;

                    foreach (var items in lstSizeObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.InvoiceNumber;
                        WorkSheet1.Cells[recordIndex, 2].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.InvoiceDate;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.SBNo;
                        WorkSheet1.Cells[recordIndex, 4].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.SBDate;
                        WorkSheet1.Cells[recordIndex, 5].Value = items.ExchangeRate;
                        WorkSheet1.Cells[recordIndex, 6].Value = items.PINumber;
                        WorkSheet1.Cells[recordIndex, 7].Value = items.ConsigneeName;
                        WorkSheet1.Cells[recordIndex, 8].Value = items.NotifyPartyName;
                        WorkSheet1.Cells[recordIndex, 9].Value = items.CountryName;
                        WorkSheet1.Cells[recordIndex, 10].Value = items.Quantity;
                        WorkSheet1.Cells[recordIndex, 11].Value = items.InvoiceAmount;
                        WorkSheet1.Cells[recordIndex, 12].Value = items.PO_CommissionPerTon;
                        WorkSheet1.Cells[recordIndex, 13].Value = items.TotalCommission;
                        WorkSheet1.Cells[recordIndex, 14].Value = items.CommissionMentionInSBill;
                        WorkSheet1.Cells[recordIndex, 15].Value = items.UtilizedAmount;
                        WorkSheet1.Cells[recordIndex, 16].Value = items.UnUtilizedAmount;
                        WorkSheet1.Cells[recordIndex, 17].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 17].Value = items.CreatedDate;
                        WorkSheet1.Cells[recordIndex, 18].Value = items.CreatorName;

                        if (string.IsNullOrEmpty(items.SBNo) || items.SBDate == null || items.ExchangeRate == 0 || items.CommissionMentionInSBill == 0 || items.UtilizedAmount == 0 || items.UnUtilizedAmount == 0)
                        {
                            WorkSheet1.Row(recordIndex).Style.Fill.PatternType = ExcelFillStyle.Solid;
                            WorkSheet1.Row(recordIndex).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Pink);
                        }

                        recordIndex += 1;
                    }

                    WorkSheet1.Columns.AutoFit();

                    excelExportData.SaveAs(msExportDataFile);
                    msExportDataFile.Position = 0;
                    result = msExportDataFile.ToArray();
                }
            }

            if (result != null)
            {
                _response.Data = result;
                _response.IsSuccess = true;
                _response.Message = "Exported successfully";
            }

            return _response;
        }

        #endregion

        #region MIS Sell Rate

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetMIS_SellRateList(MIS_Search parameters)
        {
            IEnumerable<MIS_SellRateList_Response> lstUsers = await _manageMISRepository.GetMIS_SellRateList(parameters);
            _response.Data = lstUsers.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> UpdateMIS_SellRate(Invoice_Request parameters)
        {
            parameters.ModuleType = "MIS_SellRate";

            int result = await _manageTrackingRepository.SaveInvoice(parameters);

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
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportMIS_SellRateListData(MIS_Search parameters)
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            IEnumerable<MIS_SellRateList_Response> lstSizeObj = await _manageMISRepository.GetMIS_SellRateList(parameters);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("MIS_SellRate");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Invoice Number";
                    WorkSheet1.Cells[1, 2].Value = "Invoice Date";
                    WorkSheet1.Cells[1, 3].Value = "Quantity";
                    WorkSheet1.Cells[1, 4].Value = "Final Invoice Amount";
                    WorkSheet1.Cells[1, 5].Value = "Containers";
                    WorkSheet1.Cells[1, 6].Value = "Reuse / Fresh.";
                    WorkSheet1.Cells[1, 7].Value = "Transporter";
                    WorkSheet1.Cells[1, 8].Value = "Rate";
                    WorkSheet1.Cells[1, 9].Value = "Land Freight";
                    WorkSheet1.Cells[1, 10].Value = "Transporter Invoice";
                    WorkSheet1.Cells[1, 11].Value = "Forwarder";
                    WorkSheet1.Cells[1, 12].Value = "Forwarder Invoice";
                    WorkSheet1.Cells[1, 13].Value = "Sea Freight";
                    WorkSheet1.Cells[1, 14].Value = "Cha";
                    WorkSheet1.Cells[1, 15].Value = "Cha Invoice";
                    WorkSheet1.Cells[1, 16].Value = "Clearing";
                    WorkSheet1.Cells[1, 17].Value = "Commission / MT";
                    WorkSheet1.Cells[1, 18].Value = "Total Commission";
                    WorkSheet1.Cells[1, 19].Value = "Exchange Rate";
                    WorkSheet1.Cells[1, 20].Value = "Commission In (INR)";
                    WorkSheet1.Cells[1, 21].Value = "SB No.";
                    WorkSheet1.Cells[1, 22].Value = "SB Date";
                    WorkSheet1.Cells[1, 23].Value = "FOB Value";
                    WorkSheet1.Cells[1, 24].Value = "Current Exchange Rate";
                    WorkSheet1.Cells[1, 25].Value = "DrawBack + RodTep";
                    WorkSheet1.Cells[1, 26].Value = "InvoiceAmount In (INR)";
                    WorkSheet1.Cells[1, 27].Value = "NetSellRate";
                    WorkSheet1.Cells[1, 28].Value = "Created Date";
                    WorkSheet1.Cells[1, 29].Value = "Created By";

                    recordIndex = 2;

                    foreach (var items in lstSizeObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.InvoiceNumber;
                        WorkSheet1.Cells[recordIndex, 2].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.InvoiceDate;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.Quantity;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.FinalInvoiceAmount;
                        WorkSheet1.Cells[recordIndex, 5].Value = items.Containers;
                        WorkSheet1.Cells[recordIndex, 6].Value = items.Reuse_Fresh;
                        WorkSheet1.Cells[recordIndex, 7].Value = items.Transporter;
                        WorkSheet1.Cells[recordIndex, 8].Value = items.Rate;
                        WorkSheet1.Cells[recordIndex, 9].Value = items.LandFreight;
                        WorkSheet1.Cells[recordIndex, 10].Value = items.TransporterInvoice;
                        WorkSheet1.Cells[recordIndex, 11].Value = items.Forwarder;
                        WorkSheet1.Cells[recordIndex, 12].Value = items.ForwarderInvoice;
                        WorkSheet1.Cells[recordIndex, 13].Value = items.SeaFreight;
                        WorkSheet1.Cells[recordIndex, 14].Value = items.CHA;
                        WorkSheet1.Cells[recordIndex, 15].Value = items.ChaInvoice;
                        WorkSheet1.Cells[recordIndex, 16].Value = items.Clearing;
                        WorkSheet1.Cells[recordIndex, 17].Value = items.PO_CommissionPerTon;
                        WorkSheet1.Cells[recordIndex, 18].Value = items.TotalCommission;
                        WorkSheet1.Cells[recordIndex, 19].Value = items.ExchangeRate;
                        WorkSheet1.Cells[recordIndex, 20].Value = items.CommissionInINR;
                        WorkSheet1.Cells[recordIndex, 21].Value = items.SBNo;
                        WorkSheet1.Cells[recordIndex, 22].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 22].Value = items.SBDate;
                        WorkSheet1.Cells[recordIndex, 23].Value = items.FOBValue;
                        WorkSheet1.Cells[recordIndex, 24].Value = items.CurrentExchangeRate;
                        WorkSheet1.Cells[recordIndex, 25].Value = items.DrawBack_RodTep;
                        WorkSheet1.Cells[recordIndex, 26].Value = items.InvoiceAmountInINR;
                        WorkSheet1.Cells[recordIndex, 27].Value = items.NetSellRate;
                        WorkSheet1.Cells[recordIndex, 28].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 28].Value = items.CreatedDate;
                        WorkSheet1.Cells[recordIndex, 29].Value = items.CreatorName;

                        if (items.Containers == 0 || string.IsNullOrEmpty(items.Reuse_Fresh) || string.IsNullOrEmpty(items.SBNo) || items.SBDate == null 
                            || items.ExchangeRate == 0 || items.Rate == 0 || items.LandFreight == 0 || string.IsNullOrEmpty(items.TransporterInvoice) 
                            || string.IsNullOrEmpty(items.ForwarderInvoice) || items.SeaFreight == 0 || string.IsNullOrEmpty(items.ChaInvoice) 
                            || items.Clearing==0 || items.CurrentExchangeRate == 0 || items.DrawBack_RodTep == 0 || items.InvoiceAmountInINR == 0 
                            || items.NetSellRate == 0 || string.IsNullOrEmpty(items.Transporter) || string.IsNullOrEmpty(items.Forwarder) 
                            || string.IsNullOrEmpty(items.CHA))
                        {
                            WorkSheet1.Row(recordIndex).Style.Fill.PatternType = ExcelFillStyle.Solid;
                            WorkSheet1.Row(recordIndex).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Pink);
                        }

                        recordIndex += 1;
                    }

                    WorkSheet1.Columns.AutoFit();

                    excelExportData.SaveAs(msExportDataFile);
                    msExportDataFile.Position = 0;
                    result = msExportDataFile.ToArray();
                }
            }

            if (result != null)
            {
                _response.Data = result;
                _response.IsSuccess = true;
                _response.Message = "Exported successfully";
            }

            return _response;
        }

        #endregion
    }
}
