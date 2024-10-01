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
    public class WineBatchService : IWineBatchService
    {
        private readonly IWineBatchRepository _wineBatchRepo;
        private readonly IMapper _mapper;

        public WineBatchService(IWineBatchRepository wineBatchRepo, IMapper mapper)
        {
            _wineBatchRepo = wineBatchRepo;
            _mapper = mapper;
        }

        public async Task<WineBatchDTO> Create(WineBatchDTO data)
        {
            try
            {
                var map = _mapper.Map<WineBatch>(data);
                var dataCreate = await _wineBatchRepo.Create(map);
                var resutl = _mapper.Map<WineBatchDTO>(dataCreate);
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
                var data = await _wineBatchRepo.GetById(id);
                if (data == null)
                {
                    throw new Exception($"Data {id} does not exist");
                }

                await _wineBatchRepo.Delete(data);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<WineBatchDTO>> GetAll()
        {
            try
            {

                var data = await _wineBatchRepo.GetAll();
                var map = _mapper.Map<List<WineBatchDTO>>(data);
                return map;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<WineBatch> GetById(int id)
        {
            var data = await _wineBatchRepo.GetById(id);
            return data;
        }

 
        public async Task<bool> Update(int id, WineBatchDTO data)
        {
            try
            {
                var wineBatch = await _wineBatchRepo.GetById(id);
                if (data == null)
                {
                    return false;
                }

                _mapper.Map(data, wineBatch);
                await _wineBatchRepo.Update(wineBatch);
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
