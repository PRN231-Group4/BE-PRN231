using AutoMapper;
using BusinessLayer.Modal.Request;
using BusinessLayer.Service.Interface;
using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class WineCheckService : IWineCheckService
    {
        private readonly IWineCheckRepository _wineCheckRepository;
        private readonly IWineRepository _wineRepository;
        private readonly IMapper _mapper;

        public WineCheckService(IWineCheckRepository wineCheckRepository, IMapper mapper, IWineRepository wineRepository)
        {
            _wineRepository = wineRepository;
            _wineCheckRepository = wineCheckRepository;
            _mapper = mapper;
        }

        public async Task<WineCheckDTO> Create(WineCheckDTO data)
        {
            try
            {
                var map = _mapper.Map<WineCheck>(data);
                var dataCreate = await _wineCheckRepository.Create(map);
                var resutl = _mapper.Map<WineCheckDTO>(dataCreate);
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
                var data = await _wineCheckRepository.GetById(id);
                if (data == null)
                {
                    throw new Exception($"Data {id} does not exist");
                }

                await _wineCheckRepository.Delete(data);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<WineCheckDTO>> GetAll()
        {
            try
            {

                var data = await _wineCheckRepository.GetAll();
                var map = _mapper.Map<List<WineCheckDTO>>(data);
                foreach (var wine in map)
                {
                    // Giả sử bạn có phương thức GetById trong repository
                    var wineDetails = await _wineRepository.GetById((int)wine.WineId);

                    // Kiểm tra wineDetails có khác null không và gán wineName
                    if (wineDetails != null)
                    {
                        wine.wineName = wineDetails.Name; // Gán tên rượu từ thông tin chi tiết
                    }
                    else
                    {
                        wine.wineName = "Unknown wineName"; // Gán giá trị mặc định nếu không tìm thấy
                    }
                }
                return map;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<WineCheck> GetById(int id)
        {
            var data = await _wineCheckRepository.GetById(id);
            return data;
        }


        public async Task<bool> Update(int id, WineCheckDTO data)
        {
            try
            {
                var check = await _wineCheckRepository.GetById(id);
                if (data == null)
                {
                    return false;
                }

                _mapper.Map(data, check);
                await _wineCheckRepository.Update(check);
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
