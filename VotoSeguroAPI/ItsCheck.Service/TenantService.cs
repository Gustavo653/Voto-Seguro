using ItsCheck.Domain;
using ItsCheck.DTO.Base;
using ItsCheck.Infrastructure.Repository;
using ItsCheck.Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ItsCheck.Service
{
    public class TenantService(ITenantRepository tenantRepository) : ITenantService
    {
        private readonly ITenantRepository _tenantRepository = tenantRepository;

        public async Task<ResponseDTO> Create(BasicDTO nameDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var tenant = new Tenant
                {
                    Name = nameDTO.Name,
                };

                tenant.SetCreatedAt();
                await _tenantRepository.InsertAsync(tenant);
                await _tenantRepository.SaveChangesAsync();
                Log.Information("Tenant persistido id: {id}", tenant.Id);

                responseDTO.Object = tenant;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> Update(int id, BasicDTO nameDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var tenant = await _tenantRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (tenant == null)
                {
                    responseDTO.SetBadInput($"O tenant com id: {id} não existe!");
                    return responseDTO;
                }
                tenant.Name = nameDTO.Name;
                tenant.SetUpdatedAt();
                await _tenantRepository.SaveChangesAsync();
                Log.Information("Tenant persistido id: {id}", tenant.Id);

                responseDTO.Object = tenant;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> Remove(int id)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var tenant = await _tenantRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (tenant == null)
                {
                    responseDTO.SetBadInput($"O tenant com id: {id} não existe!");
                    return responseDTO;
                }
                _tenantRepository.Delete(tenant);
                await _tenantRepository.SaveChangesAsync();
                Log.Information("Tenant removido id: {id}", tenant.Id);

                responseDTO.Object = tenant;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> GetList()
        {
            ResponseDTO responseDTO = new();
            try
            {
                responseDTO.Object = await _tenantRepository.GetEntities().ToListAsync();
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }
    }
}