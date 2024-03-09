using ItsCheck.Domain;
using ItsCheck.DTO.Base;
using ItsCheck.Infrastructure.Repository;
using ItsCheck.Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ItsCheck.Service
{
    public class ItemService(IItemRepository itemRepository,
                             IChecklistReplacedItemRepository ChecklistReplacedItemRepository,
                             IChecklistItemRepository checklistItemRepository) : IItemService
    {
        private readonly IItemRepository _itemRepository = itemRepository;
        private readonly IChecklistReplacedItemRepository _ChecklistReplacedItemRepository = ChecklistReplacedItemRepository;
        private readonly IChecklistItemRepository _checklistItemRepository = checklistItemRepository;

        public async Task<ResponseDTO> Create(BasicDTO basicDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var itemExists = await _itemRepository.GetEntities().AnyAsync(c => c.Name == basicDTO.Name);
                if (itemExists)
                {
                    responseDTO.SetBadInput($"O item {basicDTO.Name} já existe!");
                    return responseDTO;
                }

                var item = new Item
                {
                    Name = basicDTO.Name,
                };
                item.SetCreatedAt();
                await _itemRepository.InsertAsync(item);
                await _itemRepository.SaveChangesAsync();
                Log.Information("Item persistido id: {id}", item.Id);

                responseDTO.Object = item;
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
                var item = await _itemRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (item == null)
                {
                    responseDTO.SetBadInput($"O item {basicDTO.Name} não existe!");
                    return responseDTO;
                }

                item.Name = basicDTO.Name;
                item.SetUpdatedAt();
                await _itemRepository.SaveChangesAsync();
                Log.Information("Item persistido id: {id}", item.Id);

                responseDTO.Object = item;
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
                var ChecklistReplacedItemExists = await _ChecklistReplacedItemRepository.GetEntities().AnyAsync(c => c.ChecklistItem.Item.Id == id);
                if (ChecklistReplacedItemExists)
                {
                    responseDTO.SetBadInput("Não é possível apagar o item, já existe uma reposição vinculada!");
                    return responseDTO;
                }

                var checkListItemExists = await _checklistItemRepository.GetEntities().AnyAsync(c => c.Item.Id == id);
                if (ChecklistReplacedItemExists)
                {
                    responseDTO.SetBadInput("Não é possível apagar o item, já existe um item de checklist vinculado!");
                    return responseDTO;
                }

                var item = await _itemRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (item == null)
                {
                    responseDTO.SetBadInput($"O item com id: {id} não existe!");
                    return responseDTO;
                }

                _itemRepository.Delete(item);
                await _itemRepository.SaveChangesAsync();
                Log.Information("Item removido id: {id}", item.Id);

                responseDTO.Object = item;
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
                responseDTO.Object = await _itemRepository.GetEntities().ToListAsync();
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }
    }
}