﻿using API104.DTOs.Category;
using Microsoft.EntityFrameworkCore;

namespace API104.Services.Implementations
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(CreateCategoryDto createCategoryDto)
        {
            Category category = new Category
            {
                Name = createCategoryDto.Name
            };
            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();
        }

        public async Task<ICollection<GetCategoryDto>> GetAllAsync(int page, int take)
        {
            ICollection<Category> categories = await _repository.GetAllAsync(skip:(page-1)*take,take:take,IsTracking:false).ToListAsync();

            ICollection<GetCategoryDto> getCategoryDtos =new List<GetCategoryDto>();

            foreach (Category category in categories)
            {
                getCategoryDtos.Add(new GetCategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                });
            }
            return getCategoryDtos;
        }

        public async Task<GetCategoryDto> GetAsync(int id)
        {
            Category category = await _repository.GetByIdAsync(id);

            if (category is null) throw new Exception("Not found");

            return new GetCategoryDto()
            {
                Id = category.Id,
                Name = category.Name,
            };
        }

        public async Task UpdateAsync(int id, UpdateCategoryDto updateCategoryDto)
        {
            Category category = await _repository.GetByIdAsync(id);

            if (category is null) throw new Exception("Not found");

            category.Name = updateCategoryDto.Name;

            _repository.Update(category);
            await _repository.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Category category = await _repository.GetByIdAsync(id);

            if (category is null) throw new Exception("Not found");

            _repository.Delete(category);
            await _repository.SaveChangesAsync();
        }
    }
}
