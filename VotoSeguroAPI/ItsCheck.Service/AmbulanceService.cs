using ItsCheck.Domain;
using ItsCheck.DTO;
using ItsCheck.DTO.Base;
using ItsCheck.Infrastructure.Repository;
using ItsCheck.Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ItsCheck.Service
{
    public class AmbulanceService(IAmbulanceRepository ambulanceRepository) : IAmbulanceService
    {
        private readonly IAmbulanceRepository _ambulanceRepository = ambulanceRepository;

        public async Task<ResponseDTO> Create(AmbulanceDTO ambulanceDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var ambulanceExists = await _ambulanceRepository.GetEntities().AnyAsync(c => c.Number == ambulanceDTO.Number);
                if (ambulanceExists)
                {
                    responseDTO.SetBadInput($"A ambulância {ambulanceDTO.Number} já existe!");
                    return responseDTO;
                }

                var ambulance = new Ambulance
                {
                    Number = ambulanceDTO.Number,
                    LicensePlate = ambulanceDTO.LicensePlate,
                };
                ambulance.SetCreatedAt();
                await _ambulanceRepository.InsertAsync(ambulance);

                await _ambulanceRepository.SaveChangesAsync();
                Log.Information("Ambulância persistida id: {id}", ambulance.Id);

                responseDTO.Object = ambulance;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> Update(int id, AmbulanceDTO ambulanceDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var ambulance = await _ambulanceRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (ambulance == null)
                {
                    responseDTO.SetBadInput($"A ambulância {ambulanceDTO.Number} não existe!");
                    return responseDTO;
                }

                ambulance.Number = ambulanceDTO.Number;
                ambulance.LicensePlate = ambulanceDTO.LicensePlate;
                ambulance.SetUpdatedAt();

                await _ambulanceRepository.SaveChangesAsync();
                Log.Information("Ambulância persistida id: {id}", ambulance.Id);

                responseDTO.Object = ambulance;
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
                var ambulance = await _ambulanceRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (ambulance == null)
                {
                    responseDTO.SetBadInput($"A ambulância com id: {id} não existe!");
                    return responseDTO;
                }
                _ambulanceRepository.Delete(ambulance);
                await _ambulanceRepository.SaveChangesAsync();
                Log.Information("Ambulância removida id: {id}", ambulance.Id);

                responseDTO.Object = ambulance;
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
                responseDTO.Object = await _ambulanceRepository.GetEntities().ToListAsync();
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }
    }
}
