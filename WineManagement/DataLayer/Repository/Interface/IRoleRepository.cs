using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.Interface
{
    public interface IRoleRepository
    {
        public Task<List<Role>> GetAll();
        public Task<Role> Create(Role data);
        public Task<Role> Update(Role data);
        public Task<bool> Delete(Role data);

        public Task<Role> GetById(int id);
    }
}
