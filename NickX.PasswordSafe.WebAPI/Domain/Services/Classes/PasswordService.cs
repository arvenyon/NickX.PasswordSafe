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
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordRepository _passwordRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PasswordService(IPasswordRepository passwordRepository, IUnitOfWork unitOfWork)
        {
            _passwordRepository = passwordRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Password>> ListAsync()
        {
            return await _passwordRepository.ListAsync();
        }

        public async Task<PasswordResponse> SaveAsync(Password password)
        {
            try
            {
                await _passwordRepository.AddAsync(password);
                await _unitOfWork.CommitChangesAsync();

                return new PasswordResponse(password);
            }
            catch (Exception ex)
            {
                return new PasswordResponse($"An error occured while trying to save the password: {ex.Message}");
            }
        }

        public async Task<PasswordResponse> UpdateAsync(int id, Password password)
        {
            var curPass = await _passwordRepository.FindByIdAsync(id);

            if (curPass == null)
                return new PasswordResponse("Password not found!");

            curPass.DisplayName = password.DisplayName;
            curPass.Description = password.Description;
            curPass.Username = password.Username;
            curPass.Key = password.Key;
            curPass.Url = password.Url;
            curPass.Domain = password.Domain;
            curPass.Category = password.Category;

            try
            {
                _passwordRepository.Update(curPass);
                await _unitOfWork.CommitChangesAsync();
                return new PasswordResponse(curPass);
            }
            catch (Exception ex)
            {
                return new PasswordResponse($"An error occured while trying to update the category: " + ex.Message);
            }
        }

        public async Task<PasswordResponse> DeleteAsync(int id)
        {
            var curPass = await _passwordRepository.FindByIdAsync(id);

            if (curPass == null)
                return new PasswordResponse("Password not found!");

            try
            {
                _passwordRepository.Remove(curPass);
                await _unitOfWork.CommitChangesAsync();
                return new PasswordResponse(curPass);
            }
            catch (Exception ex)
            {
                return new PasswordResponse($"An error occured while trying to delete the passdword: " + ex.Message);
            }
        }
    }
}
