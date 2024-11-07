using BusinessLayer.Modal.Request;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service.Interface
{
    public interface IReportService
    {
        public Task<List<Report>> GetALL();
        public Task<ReportRequest> Create(ReportRequest order);
        public Task<bool> Delete(int order);

        public Task<List<Report>> GetCandleByCategoryId(int id);
    }
}
