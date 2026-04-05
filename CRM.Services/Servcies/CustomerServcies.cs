using CRM.Core.Contracts;
using CRM.Core.Entities.CustomersEntity;
using CRM.Shared.DTOs.Customer;
using CRM.Shared.Respones;
using CRM.Shared.SharedEnums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services.Servcies
{
    public class CustomerServcies:ICustomerServcies
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public CustomerServcies(IUnitOfWork unitOfWork,ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<GenericeResponse<int>> CreateAsync(CreateCustomerDto dto)
        {
            var response = new GenericeResponse<int>();

            try
            {
                if (dto == null)
                {
                    response.StatusCode = 400;
                    response.Message = "Invalid data";
                    return response;
                }

                var repo = _unitOfWork.GetRepository<Customer, int>();

                var customer = new Customer
                {
                    Name = dto.Name,
                    Phone = dto.Phone,
                    Email = dto.Email,
                    AssignedToId = dto.AssignedToId
                };

                await repo.AddAsync(customer);

                var result = await _unitOfWork.SaveChangesAsync() > 0;

                if (result)
                {
                    response.StatusCode = 201;
                    response.Message = "Customer created successfully";
                    response.Data = customer.Id;
                }
                else
                {
                    response.StatusCode = 500;
                    response.Message = "Failed to create customer";
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateAsync");
                response.StatusCode = 500;
                response.Message = "Internal server error";
                return response;
            }
        }

        public async Task<GenericeResponse<bool>> DeleteAsync(int id)
        {
            var response = new GenericeResponse<bool>();

            try
            {
                var repo = _unitOfWork.GetRepository<Customer, int>();
                var customer = await repo.GetByIdAsync(id);

                if (customer == null)
                {
                    response.StatusCode = 404;
                    response.Message = "Customer not found";
                    return response;
                }

                repo.Delete(customer);

                var result = await _unitOfWork.SaveChangesAsync() > 0;

                response.StatusCode = result ? 200 : 500;
                response.Message = result ? "Deleted successfully" : "Delete failed";
                response.Data = result;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteAsync");
                response.StatusCode = 500;
                response.Message = "Internal server error";
                return response;
            }
        }

        public async Task<GenericeResponse<IEnumerable<CustomerDto>>> GetAllAsync()
        {
            var response = new GenericeResponse<IEnumerable<CustomerDto>>();

            try
            {
                var repo = _unitOfWork.GetRepository<Customer, int>();
                var customers = await repo.GetAllAsync();

                if (customers == null || !customers.Any())
                {
                    response.StatusCode = 404;
                    response.Message = "No customers found";
                    return response;
                }

                response.StatusCode = 200;
                response.Message = "Customers retrieved successfully";
                response.Data = customers.Select(c => new CustomerDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Phone = c.Phone,
                    Email = c.Email,
                    AssignedToId = c.AssignedToId,
                    CreatedAt = c.CreatedAt
                });

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync");
                response.StatusCode = 500;
                response.Message = "Internal server error";
                return response;
            }
        }

        public async Task<GenericeResponse<CustomerDto>> GetByIdAsync(int id)
        {
            var response = new GenericeResponse<CustomerDto>();

            try
            {
                var repo = _unitOfWork.GetRepository<Customer, int>();
                var customer = await repo.GetByIdAsync(id);

                if (customer == null)
                {
                    response.StatusCode = 404;
                    response.Message = "Customer not found";
                    return response;
                }

                response.StatusCode = 200;
                response.Message = "Customer retrieved successfully";
                response.Data = new CustomerDto
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    Email = customer.Email,
                    AssignedToId = customer.AssignedToId,
                    CreatedAt = customer.CreatedAt
                };

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetByIdAsync");
                response.StatusCode = 500;
                response.Message = "Internal server error";
                return response;
            }
        }

        public async Task<GenericeResponse<bool>> UpdateAsync(UpdateCustomerDto dto)
        {
            var response = new GenericeResponse<bool>();

            try
            {
                var repo = _unitOfWork.GetRepository<Customer, int>();
                var customer = await repo.GetByIdAsync(dto.Id);

                if (customer == null)
                {
                    response.StatusCode = 404;
                    response.Message = "Customer not found";
                    return response;
                }

                customer.Name = dto.Name;
                customer.Phone = dto.Phone;
                customer.Email = dto.Email;
                customer.AssignedToId = dto.AssignedToId;

                repo.Update(customer);

                var result = await _unitOfWork.SaveChangesAsync() > 0;

                response.StatusCode = result ? 200 : 500;
                response.Message = result ? "Updated successfully" : "Update failed";
                response.Data = result;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync");
                response.StatusCode = 500;
                response.Message = "Internal server error";
                return response;
            }
        }
    }
}
