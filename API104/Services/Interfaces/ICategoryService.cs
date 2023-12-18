﻿using API104.DTOs.Category;

namespace API104.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ICollection<GetCategoryDto>> GetAllAsync(int page, int take);
        Task<GetCategoryDto> GetAsync(int id);
        Task CreateAsync(CreateCategoryDto categoryDto);
        Task UpdateAsync(int id, UpdateCategoryDto categoryDto);
        Task DeleteAsync(int id);
    }
}
