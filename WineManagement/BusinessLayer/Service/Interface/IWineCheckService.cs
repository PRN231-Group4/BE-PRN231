using BusinessLayer.Modal.Request;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service.Interface
{
    
        public interface IWineCheckService
        {
            public Task<List<WineCheckDTO>> GetAll();
            public Task<WineCheckDTO> Create(WineCheckDTO data);
            Task<bool> Update(int id, WineCheckDTO data);
            Task<bool> Delete(int id);
            Task<WineCheck> GetById(int id);
        }
    
}
