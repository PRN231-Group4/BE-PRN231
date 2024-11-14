using DataLayer.Models;
using DataLayer.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly WineManagementSystemContext _context;

        public ReportRepository(WineManagementSystemContext context)
        {
            _context = context;
        }
        public async Task<Report> Create(Report data)
        {
            _context.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> Delete(Report data)
        {
            _context.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Report>> GetALL()
        {
            var data = await _context.Reports.ToListAsync();
            return data;
        }
        public async Task<List<Report>> GetCandleByAccountId(int id)
        {
            var candles = await _context.Reports
                           .Where(c => c.AccountId == id)
                           .ToListAsync();

            return candles;
        }

        public async Task<Report> GetCateById(int id)
        {
            var data = await _context.Reports.SingleOrDefaultAsync(x => x.ReportId.Equals(id));
            return data;
        }
    }
}
