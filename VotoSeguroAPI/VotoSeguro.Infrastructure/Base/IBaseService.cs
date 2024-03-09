using VotoSeguro.DTO.Base;

namespace VotoSeguro.Infrastructure.Base
{
    public interface IBaseService<T>
    {
        Task<ResponseDTO> Create(T objectDTO);
        Task<ResponseDTO> Update(int id, T objectDTO);
        Task<ResponseDTO> Remove(int id);
        Task<ResponseDTO> GetList();
    }
}
