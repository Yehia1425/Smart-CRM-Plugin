using CRM.Core.Contracts;
using CRM.Core.Entities.UserEntity;
using CRM.Services.Abstraction.Services;
using CRM.Shared.DTOs.User;
using CRM.Shared.Respones;
using CRM.Shared.SharedEnums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services.Servcies
{
    public class UserServices:IUserServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserServices> _logger;

        public UserServices(IUnitOfWork unitOfWork,ILogger<UserServices> logger)
        {
            _unitOfWork = unitOfWork;
           _logger = logger;
        }

        public async Task<GenericeResponse<int>> CreateUserAsync(CreateUserDTO userDTO)
        {
            var response = new GenericeResponse<int>();

            try
            {
                if (userDTO == null)
                {
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Message = "Invalid data";
                    return response;
                }


                var user = new User
                {
                    Name = userDTO.Name,
                    Email = userDTO.Email,
                    CreatedAt = DateTime.Now,
                };

                await _unitOfWork.GetRepository<User, int>().AddAsync(user);

                var result = await _unitOfWork.SaveChangesAsync() > 0;

                if (result)
                {
                    response.StatusCode = StatusCodes.Status200OK;
                    response.Message = "User created successfully";
                    response.Data = user.Id;
                }
                else
                {
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                    response.Message = "Failed to create user";
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateUser");

                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = "Unexpected error";

                return response;
            }
        }

        public async Task<GenericeResponse<bool>> DeleteUserAsync(int id)
        {
            var response = new GenericeResponse<bool>();

            try
            {
                var repo = _unitOfWork.GetRepository<User, int>();
                var user = await repo.GetByIdAsync(id);

                if (user == null)
                {
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.Message = "User not found";
                    return response;
                }

                repo.Delete(user);

                var result = await _unitOfWork.SaveChangesAsync() > 0;

                if (result)
                {
                    response.StatusCode = StatusCodes.Status200OK;
                    response.Message = "User deleted";
                    response.Data = true;
                }
                else
                {
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                    response.Message = "Failed to delete user";
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteUser");

                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = "Unexpected error";

                return response;
            }
        }

        public async Task<GenericeResponse<IEnumerable<UserDTO>>> GetAllUserAsync()
        {
            var response = new GenericeResponse<IEnumerable<UserDTO>>();

            try
            {
                var users = await _unitOfWork
                    .GetRepository<User, int>()
                    .GetAllAsync();

                if (users == null || !users.Any())
                {
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.Message = "No users found";
                    return response;
                }

                var data = users.Select(u => new UserDTO
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.Role.ToString(),
                    CreatedAt = u.CreatedAt // Convert to local time if needed
                });

                response.StatusCode = StatusCodes.Status200OK;
                response.Message = "Success";
                response.Data = data;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllUsers");

                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = "Unexpected error";

                return response;
            }
        }

        public async Task<GenericeResponse<UserDTO>> GetUserByIdAsync(int id)
        {
            var response = new GenericeResponse<UserDTO>();

            try
            {
                var user = await _unitOfWork
                    .GetRepository<User, int>()
                    .GetByIdAsync(id);

                if (user == null)
                {
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.Message = "User not found";
                    return response;
                }

                response.Data = new UserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    CreatedAt = DateTime.Now
                };

                response.StatusCode = StatusCodes.Status200OK;
                response.Message = "Success";

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetUserById");

                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = "Unexpected error";

                return response;
            }
        }

        public async Task<GenericeResponse<bool>> UpdateUserAsync(UpdateUserDTO userDTO)
        {
            var response = new GenericeResponse<bool>();

            try
            {
                var repo = _unitOfWork.GetRepository<User, int>();
                var user = await repo.GetByIdAsync(userDTO.Id);

                if (user == null)
                {
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.Message = "User not found";
                    return response;
                }

                user.Name = userDTO.Name;
                user.Email = userDTO.Email;
                user.CreatedAt= DateTime.Now;

                repo.Update(user);

                var result = await _unitOfWork.SaveChangesAsync() > 0;

                if (result)
                {
                    response.StatusCode = StatusCodes.Status200OK;
                    response.Message = "Updated successfully";
                    response.Data = true;
                }
                else
                {
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                    response.Message = "Failed to update";
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateUser");

                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = "Unexpected error";

                return response;
            }
        }
    }
    
}
