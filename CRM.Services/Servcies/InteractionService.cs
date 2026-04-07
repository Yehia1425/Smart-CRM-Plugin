using CRM.Core.Contracts;
using CRM.Core.Entities.InteractionEntity;
using CRM.Services.Abstraction.Services;
using CRM.Shared.DTOs.Interaction;
using CRM.Shared.Respones;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services.Servcies
{

        public class InteractionService : IInteractionService
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<InteractionService> _logger;

            public InteractionService(IUnitOfWork unitOfWork, ILogger<InteractionService> logger)
            {
                _unitOfWork = unitOfWork;
                _logger = logger;
            }

            public async Task<GenericeResponse<IEnumerable<InteractionDTO>>> GetAllAsync()
            {
                var response = new GenericeResponse<IEnumerable<InteractionDTO>>();

                try
                {
                    var repo = _unitOfWork.GetRepository<Interaction, int>();
                    var data = await repo.GetAllAsync();

                    if (data == null || !data.Any())
                    {
                        response.StatusCode = 404;
                        response.Message = "No interactions found";
                        return response;
                    }

                    response.StatusCode = 200;
                    response.Message = "Interactions retrieved successfully";
                    response.Data = data.Select(MapToDto);

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

            public async Task<GenericeResponse<InteractionDTO>> GetByIdAsync(int id)
            {
                var response = new GenericeResponse<InteractionDTO>();

                try
                {
                    var repo = _unitOfWork.GetRepository<Interaction, int>();
                    var entity = await repo.GetByIdAsync(id);

                    if (entity == null)
                    {
                        response.StatusCode = 404;
                        response.Message = "Interaction not found";
                        return response;
                    }

                    response.StatusCode = 200;
                    response.Message = "Interaction retrieved successfully";
                    response.Data = MapToDto(entity);

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

            public async Task<GenericeResponse<int>> AddAsync(CreateInteractionDTO dto)
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

                    var repo = _unitOfWork.GetRepository<Interaction, int>();

                    var entity = MapCreate(dto);

                    await repo.AddAsync(entity);

                    var result = await _unitOfWork.SaveChangesAsync() > 0;

                    if (result)
                    {
                        response.StatusCode = 201;
                        response.Message = "Interaction created successfully";
                        response.Data = entity.Id;
                    }
                    else
                    {
                        response.StatusCode = 500;
                        response.Message = "Failed to create interaction";
                    }

                    return response;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in AddAsync");
                    response.StatusCode = 500;
                    response.Message = "Internal server error";
                    return response;
                }
            }

           
            public async Task<GenericeResponse<bool>> UpdateAsync(UpdateInteractionDTO dto)
            {
                var response = new GenericeResponse<bool>();

                try
                {
                    var repo = _unitOfWork.GetRepository<Interaction, int>();
                    var entity = await repo.GetByIdAsync(dto.Id);

                    if (entity == null)
                    {
                        response.StatusCode = 404;
                        response.Message = "Interaction not found";
                        return response;
                    }

                    // manual update
                 
                    entity.Notes = dto.Notes;
                    entity.CustomerId = dto.CustomerId;

                    repo.Update(entity);

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

            public async Task<GenericeResponse<bool>> DeleteAsync(int id)
            {
                var response = new GenericeResponse<bool>();

                try
                {
                    var repo = _unitOfWork.GetRepository<Interaction, int>();
                    var entity = await repo.GetByIdAsync(id);

                    if (entity == null)
                    {
                        response.StatusCode = 404;
                        response.Message = "Interaction not found";
                        return response;
                    }

                    repo.Delete(entity);

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

            public async Task<GenericeResponse<IEnumerable<InteractionDTO>>> GetByCustomerIdAsync(int customerId)
            {
                var response = new GenericeResponse<IEnumerable<InteractionDTO>>();

                try
                {
                    var repo = _unitOfWork.GetRepository<Interaction, int>();
                    var data = await repo.GetAllAsync();

                    var filtered = data.Where(x => x.CustomerId == customerId);

                    if (!filtered.Any())
                    {
                        response.StatusCode = 404;
                        response.Message = "No interactions found for this customer";
                        return response;
                    }

                    response.StatusCode = 200;
                    response.Message = "Data retrieved successfully";
                    response.Data = filtered.Select(MapToDto);

                    return response;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in GetByCustomerIdAsync");
                    response.StatusCode = 500;
                    response.Message = "Internal server error";
                    return response;
                }
            }


            private InteractionDTO MapToDto(Interaction entity)
            {
                return new InteractionDTO
                {
                    Id = entity.Id,
                    Notes = entity.Notes,
                    Date = entity.Date,
                    CustomerId = entity.CustomerId,
                    CustomerName = entity.Customer?.Name!
                };
            }

            private Interaction MapCreate(CreateInteractionDTO dto)
            {
                return new Interaction
                {
                    Notes = dto.Notes,
                    CustomerId = dto.CustomerId,
                    Date = DateTime.Now
                };
            }
        }
    }

