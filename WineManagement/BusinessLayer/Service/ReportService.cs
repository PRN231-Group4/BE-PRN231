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
    public class ReportService : IReportService
    {
        private readonly IReportRepository _service;
        private readonly IMapper _mapper;

        public ReportService(IReportRepository service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<ReportRequest> Create(ReportRequest order)
        {
            try
            {
                var map = _mapper.Map<Report>(order);
                var createCandle = await _service.Create(map);
                var resutl = _mapper.Map<ReportRequest>(createCandle);
                return resutl;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(int order)
        {
            try
            {
                var candle = await _service.GetCateById(order);
                if (candle == null)
                {
                    throw new Exception($"Report {order} does not exist");
                }

                await _service.Delete(candle);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Report>> GetALL()
        {
            try
            {
                var data = await _service.GetALL();
                if (data == null)
                {
                    throw new Exception($"No data !");
                }
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Report>> GetCandleByCategoryId(int id)
        {
            try
            {
                var data = await _service.GetCandleByAccountId(id);
                if (data == null)
                {
                    throw new Exception($"No data !");
                }
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
