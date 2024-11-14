using AutoMapper;
using BusinessLayer.Modal.Request;
using BusinessLayer.Service.Interface;
using DataLayer.Enum;
using DataLayer.Models;
using DataLayer.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class WineExportService : IWineExportService
    {
        private readonly IWineRequestRepository _wineRequestRepo;
        private readonly IWineCheckRepository _wineCheckRepository;

        private readonly ISupplierRepository _supplierRepository;
        private readonly IWineRepository _wineRepository;


        private readonly IMapper _mapper;

        public WineExportService(IWineRequestRepository wineRequestRepo,
            IWineCheckRepository wineCheckRepository,
            ISupplierRepository supplierRepository,
            IWineRepository wineRepository,
            IMapper mapper)
        {
            _wineRequestRepo = wineRequestRepo;
            _wineCheckRepository = wineCheckRepository;
            _supplierRepository = supplierRepository;
            _wineRepository = wineRepository;
            _mapper = mapper;
        }
        public async Task<WineRequestCRUDDTO> Create(WineRequestCRUDDTO data)
        {
            if (data == null || data.WineItems == null || !data.WineItems.Any())
            {
                throw new ArgumentException("Dữ liệu yêu cầu không hợp lệ.");
            }

            try
            {
                var wineRequest = _mapper.Map<WineRequest>(data);
                var wineRequestCreate = await _wineRequestRepo.Create(wineRequest);

                foreach (var item in data.WineItems)
                {
                    var existingWine = await _wineRepository.GetById(item.WineId);
                    if (existingWine == null)
                    {
                        throw new Exception($"Wine với ID = {item.WineId} không tồn tại.");
                    }

                    var wineCheckDto = new WineCheckDTO
                    {
                        RequestId = wineRequestCreate.RequestId,
                        CheckDate = wineRequestCreate.RequestDate,
                        Description = wineRequestCreate.Description,
                        ImageUrl = "",
                        Status = "Pending",
                        InspectorId = wineRequestCreate.ManagerId,
                        Quantity = item.Quantity,
                        WineId = item.WineId,
                        wineName = existingWine.Name // Nếu cần thêm tên rượu
                    };

                    var wineCheck = _mapper.Map<WineCheck>(wineCheckDto);
                    await _wineCheckRepository.Create(wineCheck); // Tạo WineCheck
                }

                var result = _mapper.Map<WineRequestCRUDDTO>(wineRequestCreate);
                return result;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Lỗi cơ sở dữ liệu khi tạo WineRequest và WineCheck: " + dbEx.InnerException?.Message);
            }
            catch (Exception ex)
            {
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
        public async Task<List<WineRequest>> GetByUserId(int id)
        {
            var data = await _wineRequestRepo.GetByUserId(id);
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

        public async Task<bool> UpdateStatusCheckedExport(int id, WineRequestCRUDDTO data)
        {
            try
            {
                var currentData = await _wineRequestRepo.GetById(id);
                if (currentData == null || data == null)
                {
                    return false;
                }
                currentData.Status = WineExportStatus.Checked.ToString();
                await _wineRequestRepo.Update(currentData);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fail to update info {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateStatusFailedExport(int id, WineRequestCRUDDTO data)
        {
            try
            {
                var currentData = await _wineRequestRepo.GetById(id);
                if (currentData == null || data == null)
                {
                    return false;
                }
                currentData.Status = WineExportStatus.FailedExport.ToString();
                await _wineRequestRepo.Update(currentData);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fail to update info {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateStatusSuccessExport(int id, WineRequestCRUDDTO data)
        {
            try
            {
                var currentData = await _wineRequestRepo.GetById(id);
                if (currentData == null || data == null)
                {
                    return false;
                }
                currentData.Status = WineExportStatus.SuccessExport.ToString();
                await _wineRequestRepo.Update(currentData);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fail to update info {ex.Message}");
                return false;
            }
        }
    }
}
