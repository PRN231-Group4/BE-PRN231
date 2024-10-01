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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> Create(CategoryDTO data)
        {
            try
            {
                var map = _mapper.Map<Category>(data);
                var dataCreate = await _categoryRepository.Create(map);
                var resutl = _mapper.Map<CategoryDTO>(dataCreate);
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
                var data = await _categoryRepository.GetById(id);
                if (data == null)
                {
                    throw new Exception($"Data {id} does not exist");
                }

                await _categoryRepository.Delete(data);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CategoryDTO>> GetAll()
        {
            try
            {

                var data = await _categoryRepository.GetAll();
                var map = _mapper.Map<List<CategoryDTO>>(data);
                return map;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Category> GetById(int id)
        {
            var data = await _categoryRepository.GetById(id);
            return data;
        }


        public async Task<bool> Update(int id, CategoryDTO data)
        {
            try
            {
                var cate = await _categoryRepository.GetById(id);
                if (data == null)
                {
                    return false;
                }

                _mapper.Map(data, cate);
                await _categoryRepository.Update(cate);
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
