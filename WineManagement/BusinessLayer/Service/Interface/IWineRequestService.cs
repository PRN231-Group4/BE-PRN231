using BusinessLayer.Modal.Request;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service.Interface
{
    public interface IWineRequestService
    {
        public Task<List<WineRequestDTO>> GetAll();
        public Task<WineRequestCRUDDTO> Create(WineRequestCRUDDTO data);
        Task<bool> Update(int id, WineRequestCRUDDTO data);
        Task<bool> Delete(int id);
        Task<WineRequest> GetById(int id);

        Task<List<Account>> GetAllStaffAccounts();

    }
}
