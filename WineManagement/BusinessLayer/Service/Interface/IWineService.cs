﻿using BusinessLayer.Modal.Request;
using BusinessLayer.Modal.Response;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service.Interface
{
    public interface IWineService
    {
        public Task<List<WineDTO>> GetAll();
        public Task<WineDTORespond> Create(WineDTORespond data);
        Task<bool> Update(int id, WineDTORespond data);
        Task<bool> Delete(int id);
        Task<Wine> GetByName(string data);
        Task<Wine> GetById(int id);
    }
}
