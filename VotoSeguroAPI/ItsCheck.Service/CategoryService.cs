using ItsCheck.Domain;
using ItsCheck.DTO.Base;
using ItsCheck.Infrastructure.Repository;
using ItsCheck.Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ItsCheck.Service
{
    public class CategoryService(ICategoryRepository categoryRepository,
                                 IChecklistItemRepository checklistItemRepository) : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IChecklistItemRepository _checklistItemRepository = checklistItemRepository;

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

        public async Task<ResponseDTO> Update(int id, BasicDTO basicDTO)
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

        public async Task<ResponseDTO> Remove(int id)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var checkListItemExists = await _checklistItemRepository.GetEntities().AnyAsync(c => c.Category.Id == id);
                if (checkListItemExists)
                {
                    responseDTO.SetBadInput("Não é possível apagar a categoria, já existe um item de checklist vinculado!");
                    return responseDTO;
                }

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