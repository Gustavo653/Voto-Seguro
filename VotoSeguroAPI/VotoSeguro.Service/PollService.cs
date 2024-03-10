using VotoSeguro.Domain;
using VotoSeguro.DTO.Base;
using VotoSeguro.Infrastructure.Repository;
using VotoSeguro.Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Serilog;
using VotoSeguro.DTO;

namespace VotoSeguro.Service
{
    public class PollService(IPollRepository pollRepository) : IPollService
    {
        private readonly IPollRepository _pollRepository = pollRepository;

        public async Task<ResponseDTO> Create(PollDTO pollDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var poll = new Poll
                {
                    Description = pollDTO.Description,
                    Status = Domain.Enum.PollStatus.Active,
                    Title = pollDTO.Title,
                    EndDate = pollDTO.EndDate,
                    StartDate = pollDTO.StartDate,
                };
                await _pollRepository.InsertAsync(poll);
                await _pollRepository.SaveChangesAsync();
                Log.Information("Enquete persistida id: {id}", poll.Id);

                responseDTO.Object = poll;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public Task<ResponseDTO> Update(Guid id, PollDTO pollDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDTO> Remove(Guid id)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var poll = await _pollRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (poll == null)
                {
                    responseDTO.SetBadInput($"A enquete com id: {id} não existe!");
                    return responseDTO;
                }

                _pollRepository.Delete(poll);
                await _pollRepository.SaveChangesAsync();
                Log.Information("Enquete removida id: {id}", poll.Id);

                responseDTO.Object = poll;
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
                responseDTO.Object = await _pollRepository.GetEntities().ToListAsync();
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }
    }
}