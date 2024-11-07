using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.Interface
{
    public interface IReportRepository
    {
        public Task<List<Report>> GetALL();
        public Task<Report> Create(Report order);
        public Task<bool> Delete(Report order);

        public Task<List<Report>> GetCandleByAccountId(int id);

        public Task<Report> GetCateById(int id);
    }
}
