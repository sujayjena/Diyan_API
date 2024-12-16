using Diyan.Application.Helpers;
using Diyan.Application.Interfaces;
using Diyan.Application.Models;
using Diyan.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Globalization;

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
    }
}
