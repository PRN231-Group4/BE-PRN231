using BusinessLayer.Modal.Request;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service.Interface
{
    public interface IRoleService
    {
        public Task<List<RoleDTO>> GetAll();
        public Task<RoleDTO> Create(RoleDTO data);
        Task<bool> Update(int id, RoleDTO data);
        Task<bool> Delete(int id);
        Task<Role> GetById(int id);
    }
}
