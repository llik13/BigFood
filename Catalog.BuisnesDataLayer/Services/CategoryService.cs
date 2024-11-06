using AutoMapper;
using Catalog.BuisnesDataLayer.DTO.Request;
using Catalog.BuisnesDataLayer.DTO.Response;
using Catalog.BuisnesDataLayer.Interfaces;
using Catalog.DataAccessLayer.Entities;
using Catalog.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.BuisnesDataLayer.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CategoryResponseDTO>> GetCategoriesAsync()
        {
            var categories = await unitOfWork.CategoryRepository.GetAllAsync();
            var categoriesDTOs = mapper.Map<IEnumerable<CategoryResponseDTO>>(categories);
            return categoriesDTOs;
        }

        public async Task<CategoryResponseDTO> GetCategoryByIdAsync(int id)
        {
            var categories = await unitOfWork.CategoryRepository.GetByIdAsync(id);
            var categoriesDTO = mapper.Map<CategoryResponseDTO>(categories);
            return categoriesDTO;
        }
        public async Task DeleteCategoryAsync(int id)
        {
            await unitOfWork.CategoryRepository.DeleteAsync(id);
            await unitOfWork.CompleteAsync();

        }
        public async Task ChangeCategoryAsync(CategoryRequestDTO categoryDTO)
        {
            var category = mapper.Map<Category>(categoryDTO);
            await unitOfWork.CategoryRepository.ReplaceAsync(category);
            await unitOfWork.CompleteAsync();
        }
        public async Task AddCategoryAsync(CategoryRequestDTO categoryDTO)
        {
            var category = mapper.Map<Category>(categoryDTO);
            await unitOfWork.CategoryRepository.AddAsync(category);
            await unitOfWork.CompleteAsync();
        }
    }
}
