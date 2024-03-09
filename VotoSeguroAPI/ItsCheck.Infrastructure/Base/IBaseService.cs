using ItsCheck.DTO.Base;

namespace ItsCheck.Infrastructure.Base
{
    public interface IBaseService<T>
    {
        Task<ResponseDTO> Create(T objectDTO);
        Task<ResponseDTO> Update(int id, T objectDTO);
        Task<ResponseDTO> Remove(int id);
        Task<ResponseDTO> GetList();
    }
}
