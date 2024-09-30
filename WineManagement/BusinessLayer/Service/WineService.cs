using AutoMapper;
using BusinessLayer.Modal.Request;
using BusinessLayer.Service.Interface;
using DataLayer.Models;
using DataLayer.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class WineService : IWineService
    {
        private readonly IWineRepository _wineRepo;
        private readonly IMapper _mapper;

        public WineService(IWineRepository wineRepo, IMapper mapper)
        {
            _wineRepo = wineRepo;
            _mapper = mapper;
        }

        public async Task<WineDTO> Create(WineDTO data)
        {
            try
            {
                var map = _mapper.Map<Wine>(data);
                var dataCreate = await _wineRepo.Create(map);
                var resutl = _mapper.Map<WineDTO>(dataCreate);
                return resutl;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var data = await _wineRepo.GetById(id);
                if (data == null)
                {
                    throw new Exception($"Data {id} does not exist");
                }

                await _wineRepo.Delete(data);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<WineDTO>> GetAll()
        {
            try
            {

                var data = await _wineRepo.GetAll();
                var map = _mapper.Map<List<WineDTO>>(data);
                return map;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Wine> GetById(int id)
        {
            var data = await _wineRepo.GetById(id);
            return data;
        }

        public async Task<Wine> GetByName(string data)
        {
            var dataa = await _wineRepo.GetByName(data);
            return dataa;
        }

        public async Task<bool> Update(int id, WineDTO data)
        {
            try
            {
                var data1 = await _wineRepo.GetById(id);
                if (data == null)
                {
                    return false;
                }

                _mapper.Map(data, data1 );
                await _wineRepo.Update(data1);
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Fail to update info {ex.Message}");
                return false;
            }
        }
    }
}
