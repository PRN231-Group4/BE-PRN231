using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.Interface
{
    public interface ISupplierRepository
    {

        public Task<List<Supplier>> GetAll();
        public Task<Supplier> Create(Supplier data);
        public Task<Supplier> Update(Supplier data);
        public Task<bool> Delete(Supplier data);

        public Task<Supplier> GetById(int id);

    }
}
