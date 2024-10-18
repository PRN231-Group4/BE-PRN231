using DataLayer.Models;
using DataLayer.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class WineRequestRepository : IWineRequestRepository
    {
        private readonly WineManagementSystemContext _context;

        public WineRequestRepository(WineManagementSystemContext context)
        {
            _context = context;
        }
        public async Task<WineRequest> Create(WineRequest data)
        {
            _context.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> Delete(WineRequest data)
        {
            _context.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Account> GetAccountById(int id)
        {
            var data = await _context.Accounts.SingleOrDefaultAsync(x => x.AccountId.Equals(id));
            return data;
        }

        public async Task<List<WineRequest>> GetAll()
        {
            var data = await _context.WineRequests.ToListAsync();
            return data;
        }

        public async Task<List<Account>> GetAllStaffAccounts()
        {
            var data = await _context.Accounts
                                     .Where(account => account.Role.Name.Equals("Staff"))
                                     .ToListAsync();
            return data;
        }

        public async Task<WineRequest> GetById(int id)
        {
            var data = await _context.WineRequests.SingleOrDefaultAsync(x => x.RequestId.Equals(id));
            return data;
        }

        public async Task<WineRequest> Update(WineRequest data)
        {
            _context.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }
    }
}
