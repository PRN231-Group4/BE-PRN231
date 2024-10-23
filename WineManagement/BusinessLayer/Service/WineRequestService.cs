using AutoMapper;
using BusinessLayer.Modal.Request;
using BusinessLayer.Service.Interface;
using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.Repository.Interface;
using DataLayer.UnitOfWork;
using Microsoft.EntityFrameworkCore;
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
        private readonly IWineCheckRepository _wineCheckRepository;

        private readonly ISupplierRepository _supplierRepository;
        private readonly IWineRepository _wineRepository;


        private readonly IMapper _mapper;


        public WineRequestService(IWineCheckRepository wineCheckRepository,ISupplierRepository supplierRepository ,IWineRequestRepository wineRequestRepo, IMapper mapper, IWineRepository wineRepository)
        {
            _wineRequestRepo = wineRequestRepo;
            _mapper = mapper;
            _supplierRepository = supplierRepository;
            _wineRepository = wineRepository;
            _wineCheckRepository = wineCheckRepository;
        }

        public async Task<WineRequestCRUDDTO> Create(WineRequestCRUDDTO data)
        {
            try
            {
                // Ánh xạ từ DTO sang entity WineRequest
                var wineRequest = _mapper.Map<WineRequest>(data);

                // Tạo WineRequest
                var wineRequestCreate = await _wineRequestRepo.Create(wineRequest);

                // Tạo WineCheck từ WineRequest vừa tạo
                var wineCheckDto = new WineCheckDTO
                {
                    RequestId = wineRequest.RequestId,
                    CheckDate = wineRequest.RequestDate,
                    Description = wineRequest.Description,
                    ImageUrl = "",
                    Status = "Pending", // hoặc bất kỳ trạng thái nào bạn muốn
                    InspectorId = wineRequest.ManagerId,
                    Quantity = wineRequest.Quantity,
                    WineId = wineRequest.WineId
                };

                // Tạo WineCheck
                var wineCheck = _mapper.Map<WineCheck>(wineCheckDto);
                var wineCheckCreate = await _wineCheckRepository.Create(wineCheck);

                // Ánh xạ lại kết quả thành DTO
                var result = _mapper.Map<WineRequestCRUDDTO>(wineRequestCreate);

                return result;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu một trong các bước không thành công
                throw new Exception("Lỗi khi tạo WineRequest và WineCheck: " + ex.Message);
            }
        }



        public async Task<bool> Delete(int id)
        {
            try
            {
                // Lấy WineRequest theo id
                var wineRequest = await _wineRequestRepo.GetById(id);
                if (wineRequest == null)
                {
                    throw new Exception($"Data {id} does not exist");
                }

                // Lấy danh sách WineCheck liên quan tới WineRequest
                var wineChecks = await _wineCheckRepository.GetByRequest(id);

                // Xóa từng WineCheck
                foreach (var wineCheck in wineChecks)
                {
                    await _wineCheckRepository.Delete(wineCheck);
                }

                // Xóa WineRequest
                await _wineRequestRepo.Delete(wineRequest);

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
