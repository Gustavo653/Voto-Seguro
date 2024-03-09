using VotoSeguro.Domain;
using VotoSeguro.DTO.Base;
using VotoSeguro.Infrastructure.Repository;
using VotoSeguro.Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace VotoSeguro.Service
{
    public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        public async Task<ResponseDTO> Create(BasicDTO basicDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var categoryExists = await _categoryRepository.GetEntities().AnyAsync(c => c.Name == basicDTO.Name);
                if (categoryExists)
                {
                    responseDTO.SetBadInput($"A categoria {basicDTO.Name} já existe!");
                    return responseDTO;
                }
                var category = new Category
                {
                    Name = basicDTO.Name,
                };
                category.SetCreatedAt();
                await _categoryRepository.InsertAsync(category);
                await _categoryRepository.SaveChangesAsync();
                Log.Information("Categoria persistida id: {id}", category.Id);

                responseDTO.Object = category;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> Update(Guid id, BasicDTO basicDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var category = await _categoryRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (category == null)
                {
                    responseDTO.SetBadInput($"A categoria {basicDTO.Name} não existe!");
                    return responseDTO;
                }
                category.Name = basicDTO.Name;
                category.SetUpdatedAt();
                await _categoryRepository.SaveChangesAsync();
                Log.Information("Categoria persistida id: {id}", category.Id);

                responseDTO.Object = category;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> Remove(Guid id)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var category = await _categoryRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (category == null)
                {
                    responseDTO.SetBadInput($"A categoria com id: {id} não existe!");
                    return responseDTO;
                }

                _categoryRepository.Delete(category);
                await _categoryRepository.SaveChangesAsync();
                Log.Information("Categoria removida id: {id}", category.Id);

                responseDTO.Object = category;
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
                responseDTO.Object = await _categoryRepository.GetEntities().ToListAsync();
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }
    }
}