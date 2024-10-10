using BusinessLayer.Modal.Request;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service.Interface
{
    public interface ISupplierService
    {
        public Task<List<SupplierDTO>> GetAll();
        public Task<SupplierDTO> Create(SupplierDTO data);
        Task<bool> Update(int id, SupplierDTO data);
        Task<bool> Delete(int id);
        Task<Supplier> GetById(int id);
    }
}
