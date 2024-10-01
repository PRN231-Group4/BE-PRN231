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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly WineManagementSystemContext _context;

        public CategoryRepository(WineManagementSystemContext context)
        {
            _context = context;
        }
        public async Task<Category> Create(Category data)
        {
            _context.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> Delete(Category data)
        {
            _context.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Category>> GetAll()
        {
            var data = await _context.Categories.ToListAsync();
            return data;
        }

        public async Task<Category> GetById(int id)
        {
            var data = await _context.Categories.SingleOrDefaultAsync(x => x.CategoryId.Equals(id));
            return data;
        }

        public async Task<Category> Update(Category data)
        {
            _context.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }
    }
}
