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
    
        public class RoleRepository : IRoleRepository
        {
            private readonly WineManagementSystemContext _context;

            public RoleRepository(WineManagementSystemContext context)
            {
                _context = context;
            }
            public async Task<Role> Create(Role data)
            {
                _context.Add(data);
                await _context.SaveChangesAsync();
                return data;
            }

            public async Task<bool> Delete(Role data)
            {
                _context.Remove(data);
                await _context.SaveChangesAsync();
                return true;
            }

            public async Task<List<Role>> GetAll()
            {
                var data = await _context.Roles.ToListAsync();
                return data;
            }

            public async Task<Role> GetById(int id)
            {
                var data = await _context.Roles.SingleOrDefaultAsync(x => x.RoleId.Equals(id));
                return data;
            }

            public async Task<Role> Update(Role data)
            {
                _context.Update(data);
                await _context.SaveChangesAsync();
                return data;
            }
        }
    }

