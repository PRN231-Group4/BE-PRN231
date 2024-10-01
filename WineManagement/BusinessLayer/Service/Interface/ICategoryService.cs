using BusinessLayer.Modal.Request;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service.Interface
{
    public interface ICategoryService
    {

        public Task<List<CategoryDTO>> GetAll();
        public Task<CategoryDTO> Create(CategoryDTO data);
        Task<bool> Update(int id, CategoryDTO data);
        Task<bool> Delete(int id);
        Task<Category> GetById(int id);
    }
}
