using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.Interface
{
    public interface IWineRepository
    {
        public Task<List<Wine>> GetAll();
        public Task<Wine> Create(Wine data);
        public Task<Wine> Update(Wine data);
        public Task<bool> Delete(Wine data);

        public Task<Wine> GetById(int id);
        public Task<Wine> GetByName(string name);
    }
}
