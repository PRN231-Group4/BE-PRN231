using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.Interface
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetAll();
        public Task<Category> Create(Category data);
        public Task<Category> Update(Category data);
        public Task<bool> Delete(Category data);

        public Task<Category> GetById(int id);
    }
}
