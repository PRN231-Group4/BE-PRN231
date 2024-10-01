using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.Interface
{
    public interface IWineBatchRepository
    {
        public Task<List<WineBatch>> GetAll();
        public Task<WineBatch> Create(WineBatch data);
        public Task<WineBatch> Update(WineBatch data);
        public Task<bool> Delete(WineBatch data);

        public Task<WineBatch> GetById(int id);
    }
}
