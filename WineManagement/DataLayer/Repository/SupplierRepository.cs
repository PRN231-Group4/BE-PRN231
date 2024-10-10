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
    public class SupplierRepository : ISupplierRepository
    {
        private readonly WineManagementSystemContext _context;

        public SupplierRepository(WineManagementSystemContext context)
        {
            _context = context;
        }
        public async Task<Supplier> Create(Supplier data)
        {
            _context.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> Delete(Supplier data)
        {
            _context.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Supplier>> GetAll()
        {
            var data = await _context.Suppliers.ToListAsync();
            return data;
        }

        public async Task<Supplier> GetById(int id)
        {
            var data = await _context.Suppliers.SingleOrDefaultAsync(x => x.SupplierId.Equals(id));
            return data;
        }

        public async Task<Supplier> Update(Supplier data)
        {
            _context.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }
    }
}
