using NickX.PasswordSafe.Models.Main;
using NickX.PasswordSafe.WebAPI.Domain.Communication;
using NickX.PasswordSafe.WebAPI.Domain.Repositories;
using NickX.PasswordSafe.WebAPI.Domain.Services.Interfaces;
using NickX.PasswordSafe.WebAPI.Domain.UnitOfWorkManagement;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NickX.PasswordSafe.WebAPI.Domain.Services.Classes
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            return await _categoryRepository.ListAsync();
        }

        public async Task<CategoryResponse> SaveAsync(Category category)
        {
            try
            {
                await _categoryRepository.AddAsync(category);
                await _unitOfWork.CommitChangesAsync();

                return new CategoryResponse(category);
            }
            catch (Exception ex)
            {
                return new CategoryResponse($"An error occured while trying to save the category: {ex.Message}");
            }
        }

        public async Task<CategoryResponse> UpdateAsync(int id, Category category)
        {
            var existingCat = await _categoryRepository.FindByIdAsync(id);

            if (existingCat == null)
                return new CategoryResponse("Category not found!");

            existingCat.Name = category.Name;

            try
            {
                _categoryRepository.Update(existingCat);
                await _unitOfWork.CommitChangesAsync();
                return new CategoryResponse(existingCat);
            }
            catch (Exception ex)
            {
                return new CategoryResponse($"An error occurred while trying to update the category: {ex.Message}");
            }
        }

        public async Task<CategoryResponse> DeleteAsync(int id)
        {
            var existingCat = await _categoryRepository.FindByIdAsync(id);

            if (existingCat == null)
                return new CategoryResponse("Category not found!");

            try
            {
                _categoryRepository.Remove(existingCat);
                await _unitOfWork.CommitChangesAsync();

                return new CategoryResponse(existingCat);
            }
            catch (Exception ex)
            {
                return new CategoryResponse($"An error occured while trying to delete the category: {ex.Message}");
            }
        }
    }
}
