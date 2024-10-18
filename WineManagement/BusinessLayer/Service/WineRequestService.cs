using AutoMapper;
using BusinessLayer.Modal.Request;
using BusinessLayer.Service.Interface;
using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.Repository.Interface;
using DataLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class WineRequestService : IWineRequestService
    {
        private readonly IWineRequestRepository _wineRequestRepo;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IWineRepository _wineRepository;


        private readonly IMapper _mapper;


        public WineRequestService(ISupplierRepository supplierRepository ,IWineRequestRepository wineRequestRepo, IMapper mapper, IWineRepository wineRepository)
        {
            _wineRequestRepo = wineRequestRepo;
            _mapper = mapper;
            _supplierRepository = supplierRepository;
            _wineRepository = wineRepository;
        }

        public async Task<WineRequestCRUDDTO> Create(WineRequestCRUDDTO data)
        {
            try
            {
                var map = _mapper.Map<WineRequest>(data);
                var dataCreate = await _wineRequestRepo.Create(map);
                var resutl = _mapper.Map<WineRequestCRUDDTO>(dataCreate);
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
                var data = await _wineRequestRepo.GetById(id);
                if (data == null)
                {
                    throw new Exception($"Data {id} does not exist");
                }

                await _wineRequestRepo.Delete(data);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<WineRequestDTO>> GetAll()
        {
            try
            {
                // Lấy tất cả WineRequests từ repository (bất đồng bộ)
                var data = await _wineRequestRepo.GetAll();

                // Ánh xạ từ WineRequest sang WineRequestDTO
                var map = _mapper.Map<List<WineRequestDTO>>(data);

                // Xử lý lấy tên của Supplier, Manager và Wine cho từng WineRequestDTO
                foreach (var wineRequestDto in map)
                {
                    // Lấy Supplier bằng SupplierId (bất đồng bộ)
                    var supplier = await _supplierRepository.GetById(wineRequestDto.SupplierId);
                    wineRequestDto.SupplierName = supplier != null ? supplier.Name : "Unknown Supplier";

                    // Lấy Manager bằng ManagerId (bất đồng bộ)
                    if (wineRequestDto.ManagerId.HasValue)
                    {
                        var manager = await _wineRequestRepo.GetAccountById(wineRequestDto.ManagerId.Value);
                        wineRequestDto.ManagerName = manager != null ? manager.Username : "Unknown Manager";
                    }
                    else
                    {
                        wineRequestDto.ManagerName = "No Manager Assigned";
                    }

                    // Lấy Wine bằng WineId (bất đồng bộ)
                    if (wineRequestDto.WineId.HasValue)
                    {
                        var wine = await _wineRepository.GetById(wineRequestDto.WineId.Value);
                        wineRequestDto.Wine = wine != null ? wine.Name : "Unknown Wine";
                    }
                    else
                    {
                        wineRequestDto.Wine = "No Wine Assigned";
                    }
                }

                return map;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Account>> GetAllStaffAccounts()
        {
            var data = await _wineRequestRepo.GetAllStaffAccounts();
            return data;
        }

        public async Task<WineRequest> GetById(int id)
        {
            var data = await _wineRequestRepo.GetById(id);
            return data;
        }

    
        public async Task<bool> Update(int id, WineRequestCRUDDTO data)
        {
            try
            {
                var data1 = await _wineRequestRepo.GetById(id);
                if (data == null)
                {
                    return false;
                }

                _mapper.Map(data, data1);
                await _wineRequestRepo.Update(data1);
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
