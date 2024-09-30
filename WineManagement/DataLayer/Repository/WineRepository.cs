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
    public class WineRepository : IWineRepository
    {
        private readonly WineManagementSystemContext _context;

        public WineRepository(WineManagementSystemContext context)
        {
            _context = context;
        }
        public async Task<Wine> Create(Wine data)
        {
            _context.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> Delete(Wine data)
        {
            _context.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Wine>> GetAll()
        {
            var data = await _context.Wines.ToListAsync();
            return data;
        }

        public async Task<Wine> GetById(int id)
        {
            var data = await _context.Wines.SingleOrDefaultAsync(x => x.WineId.Equals(id));
            return data;
        }

        public async Task<Wine> GetByName(string name)
        {
            var data = await _context.Wines.SingleOrDefaultAsync(x => x.Name.Equals(name));
            return data;
        }

        public async Task<Wine> Update(Wine data)
        {
            _context.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }
    }
}
