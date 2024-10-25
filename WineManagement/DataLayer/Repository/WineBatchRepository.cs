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
    public class WineBatchRepository : IWineBatchRepository
    {
        private readonly WineManagementSystemContext _context;

        public WineBatchRepository(WineManagementSystemContext context)
        {
            _context = context;
        }
        public async Task<WineBatch> Create(WineBatch data)
        {
            _context.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> Delete(WineBatch data)
        {
            _context.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<WineBatch>> GetAll()
        {
            var data = await _context.WineBatches.ToListAsync();
            return data;
        }

        public async Task<WineBatch> GetById(int id)
        {
            var data = await _context.WineBatches.SingleOrDefaultAsync(x => x.BatchId.Equals(id));
            return data;
        }

        public async Task<WineBatch> Update(WineBatch data)
        {
            _context.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }
    }
}
