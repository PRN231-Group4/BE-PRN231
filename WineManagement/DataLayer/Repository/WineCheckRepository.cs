using Azure.Core;
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

    public class WineCheckRepository : IWineCheckRepository
    {
        private readonly WineManagementSystemContext _context;

        public WineCheckRepository(WineManagementSystemContext context)
        {
            _context = context;
        }
        public async Task<WineCheck> Create(WineCheck data)
        {
            _context.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> Delete(WineCheck data)
        {
            _context.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<WineCheck>> GetAll()
        {
            var data = await _context.WineChecks.ToListAsync();
            return data;
        }

        public async Task<WineCheck> GetById(int id)
        {
            var data = await _context.WineChecks.SingleOrDefaultAsync(x => x.CheckId.Equals(id));
            return data;
        }

        public async Task<List<WineCheck>> GetByRequest(int id)
        {
            var data = await _context.WineChecks.Where(w => w.RequestId == id).ToListAsync();
            return data;
        }

        public async Task<WineCheck> Update(WineCheck data)
        {
            _context.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }
    }

}
