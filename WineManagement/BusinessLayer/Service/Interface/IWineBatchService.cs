using BusinessLayer.Modal.Request;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service.Interface
{
    public interface IWineBatchService
    {
        public Task<List<WineBatchDTO>> GetAll();
        public Task<WineBatchDTO> Create(WineBatchDTO data);
        Task<bool> Update(int id, WineBatchDTO data);
        Task<bool> Delete(int id);
        Task<WineBatch> GetById(int id);
    }
}
