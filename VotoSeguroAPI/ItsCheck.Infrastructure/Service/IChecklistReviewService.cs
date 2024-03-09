using ItsCheck.DTO;
using ItsCheck.DTO.Base;
using ItsCheck.Infrastructure.Base;

namespace ItsCheck.Infrastructure.Service
{
    public interface IChecklistReviewService : IBaseService<ChecklistReviewDTO>
    {
        Task<ResponseDTO> GetList(int? takeLast);
    }
}