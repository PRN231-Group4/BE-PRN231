using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.Interface
{
    public interface IWineRequestRepository
    {
        public Task<List<WineRequest>> GetAll();
        public Task<WineRequest> Create(WineRequest data);
        public Task<WineRequest> Update(WineRequest data);
        public Task<bool> Delete(WineRequest data);
        public Task<Account> GetAccountById(int id);

        public Task<WineRequest> GetById(int id);

        public Task<List<Account>> GetAllStaffAccounts();
       
    }
}
