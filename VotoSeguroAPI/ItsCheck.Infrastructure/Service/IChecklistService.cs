using ItsCheck.DTO;
using ItsCheck.DTO.Base;
using ItsCheck.Infrastructure.Base;

namespace ItsCheck.Infrastructure.Service
{
    public interface IChecklistService : IBaseService<ChecklistDTO>
    {
        Task<ResponseDTO> GetById(int id);
    }
}