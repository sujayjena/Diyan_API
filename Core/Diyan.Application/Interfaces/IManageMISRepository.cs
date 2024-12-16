using Diyan.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diyan.Application.Interfaces
{
    public interface IManageMISRepository
    {
        Task<IEnumerable<MIS_PITrackingList_Response>> GetMIS_PITrackingList(MIS_PITracking_Search parameters);
        Task<IEnumerable<MIS_AdvanceAmountList_Response>> GetMIS_AdvanceAmountList(MIS_Search parameters);
        Task<IEnumerable<MIS_FinalInvoicePaymentList_Response>> GetMIS_FinalInvoicePaymentList(MIS_Search parameters);
    }
}
