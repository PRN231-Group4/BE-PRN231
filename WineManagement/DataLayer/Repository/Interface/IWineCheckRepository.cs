using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.Interface
{
 
        public interface IWineCheckRepository
        {
            public Task<List<WineCheck>> GetAll();
            public Task<WineCheck> Create(WineCheck data);
            public Task<WineCheck> Update(WineCheck data);
            public Task<bool> Delete(WineCheck data);
            public Task<List<WineCheck>> GetByRequest(int id);
            public Task<WineCheck> GetById(int id);
        public Task<List<WineCheck>> GetByReqId(int id);


    }
}
