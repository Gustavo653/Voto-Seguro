using ItsCheck.Domain;
using ItsCheck.DTO;
using ItsCheck.DTO.Base;
using ItsCheck.Infrastructure.Repository;
using ItsCheck.Infrastructure.Service;
using ItsCheck.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ItsCheck.Service
{
    public class ChecklistService(IChecklistRepository checklistRepository,
                                  ICategoryRepository categoryRepository,
                                  IItemRepository itemRepository,
                                  IHttpContextAccessor httpContextAccessor) : IChecklistService
    {
        private readonly IChecklistRepository _checklistRepository = checklistRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IItemRepository _itemRepository = itemRepository;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext!.Session;

        public async Task<ResponseDTO> Remove(int id)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var checklist = await _checklistRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (checklist == null)
                {
                    responseDTO.SetBadInput($"O checklist com id: {id} não existe!");
                    return responseDTO;
                }

                _checklistRepository.Delete(checklist);
                await _checklistRepository.SaveChangesAsync();
                Log.Information("Checklist removido id: {id}", checklist.Id);

                responseDTO.Object = checklist;
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
                var checklists = await _checklistRepository.GetEntities()
                    .Include(x => x.ChecklistItems)!.ThenInclude(x => x.Item)
                    .Include(x => x.ChecklistItems)!.ThenInclude(x => x.ChecklistSubItems)!.ThenInclude(x => x.Item)
                    .Include(x => x.ChecklistItems)!.ThenInclude(x => x.Category)
                    .ToListAsync();

                var jsonData = checklists.Select(checklist => new
                {
                    id = checklist.Id,
                    createdAt = checklist.CreatedAt,
                    updatedAt = checklist.UpdatedAt,
                    name = checklist.Name,
                    requireFullReview = checklist.RequireFullReview,
                    categories = checklist.ChecklistItems?
                            .Where(x => x.ParentChecklistItemId == null)
                            .Select(item => new
                            {
                                id = item.Category.Id,
                                name = item.Category.Name,
                                items = new List<object>
                                {
                                    new
                                    {
                                        id = item.Item.Id,
                                        name = item.Item.Name,
                                        amountRequired = item.RequiredQuantity,
                                        subItems = item.ChecklistSubItems != null && item.ChecklistSubItems.Count != 0 ? item.ChecklistSubItems.Select(x=>new
                                        {
                                            id = x.Item.Id,
                                            name = x.Item.Name,
                                            amountRequired = x.RequiredQuantity
                                        }) : []
                                    }
                                }
                            })
                }).GroupBy(checklist => new { checklist.id, checklist.name, checklist.createdAt, checklist.updatedAt, checklist.requireFullReview })
                    .Select(groupedChecklist => new
                    {
                        groupedChecklist.Key.id,
                        groupedChecklist.Key.createdAt,
                        groupedChecklist.Key.updatedAt,
                        groupedChecklist.Key.requireFullReview,
                        groupedChecklist.Key.name,
                        categories = groupedChecklist
                            .SelectMany(checklist => checklist.categories!)
                            .GroupBy(category => new { category.id, category.name })
                            .Select(groupedCategory => new
                            {
                                groupedCategory.Key.id,
                                groupedCategory.Key.name,
                                items = groupedCategory.SelectMany(category => category.items)
                            })
                    });

                responseDTO.Object = jsonData;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }

        public async Task<ResponseDTO> Create(ChecklistDTO checklistDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var checklistExists = await _checklistRepository.GetEntities().AnyAsync(c => c.Name == checklistDTO.Name);
                if (checklistExists)
                {
                    responseDTO.SetBadInput($"O checklist {checklistDTO.Name} já existe!");
                    return responseDTO;
                }

                var checklist = new Checklist
                {
                    Name = checklistDTO.Name,
                    RequireFullReview = checklistDTO.RequireFullReview,
                    ChecklistItems = []
                };

                await ProcessChecklistItems(checklistDTO, checklist, responseDTO);

                checklist.SetCreatedAt();
                await _checklistRepository.InsertAsync(checklist);

                if (responseDTO.Code == 200)
                {
                    await _checklistRepository.SaveChangesAsync();
                    Log.Information("Checklist persistido id: {id}", checklist.Id);
                }

                responseDTO.Object = checklistDTO;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }

        private async Task ProcessChecklistItems(ChecklistDTO checklistDTO, Checklist checklist, ResponseDTO responseDTO)
        {
            foreach (var categoryDTO in checklistDTO.Categories)
            {
                Log.Information("Processando categoria: {id} Quantidade Itens: {qtd}", categoryDTO.Id, categoryDTO.Items.Count());
                var category = await _categoryRepository.GetTrackedEntities()
                                                        .FirstOrDefaultAsync(x => x.Id == categoryDTO.Id);

                if (category == null)
                {
                    responseDTO.SetBadInput($"A categoria {categoryDTO.Id} não existe");
                    return;
                }

                foreach (var itemDTO in categoryDTO.Items)
                {
                    Log.Information("Processando item: {id}", itemDTO.Id);
                    var item = await _itemRepository.GetTrackedEntities()
                                                    .FirstOrDefaultAsync(x => x.Id == itemDTO.Id) ??
                                                    throw new Exception();

                    if (item == null)
                    {
                        responseDTO.SetBadInput($"O item {itemDTO.Id} não existe");
                        return;
                    }

                    var checklistItem = new ChecklistItem()
                    {
                        Category = category,
                        Checklist = checklist,
                        Item = item,
                        RequiredQuantity = itemDTO.AmountRequired,
                        TenantId = Convert.ToInt32(_session.GetString(Consts.ClaimTenantId))
                    };
                    checklistItem.SetCreatedAt();
                    checklist.ChecklistItems?.Add(checklistItem);
                    Log.Information("Item {id} processado", itemDTO.Id);

                    if (itemDTO.SubItems != null && itemDTO.SubItems.Count != 0)
                    {
                        foreach (var subItemDTO in itemDTO.SubItems)
                        {
                            Log.Information("Processando subItem: {id}", subItemDTO.Id);
                            var subItem = await _itemRepository.GetTrackedEntities()
                                                    .FirstOrDefaultAsync(x => x.Id == subItemDTO.Id);

                            if (subItem == null)
                            {
                                responseDTO.SetBadInput($"O item {subItemDTO.Id} não existe");
                                return;
                            }

                            var subChecklistItem = new ChecklistItem()
                            {
                                Category = category,
                                Checklist = checklist,
                                Item = subItem,
                                ParentChecklistItem = checklistItem,
                                RequiredQuantity = subItemDTO.AmountRequired,
                                TenantId = Convert.ToInt32(_session.GetString(Consts.ClaimTenantId))
                            };
                            subChecklistItem.SetCreatedAt();
                            checklist.ChecklistItems?.Add(subChecklistItem);
                            Log.Information("Subitem {id} processado", subItemDTO.Id);
                        }
                    }
                }
            }
        }

        public async Task<ResponseDTO> Update(int id, ChecklistDTO checklistDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var checklist = await _checklistRepository.GetTrackedEntities()
                                                          .Include(x => x.ChecklistItems)
                                                          .FirstOrDefaultAsync(c => c.Id == id);
                if (checklist == null)
                {
                    responseDTO.SetBadInput($"O checklist {checklistDTO.Name} não existe!");
                    return responseDTO;
                }

                checklist.Name = checklistDTO.Name;
                checklist.RequireFullReview = checklistDTO.RequireFullReview;
                checklist.ChecklistItems?.RemoveAll(_ => true);

                await ProcessChecklistItems(checklistDTO, checklist, responseDTO);

                checklist.SetUpdatedAt();

                if (responseDTO.Code == 200)
                {
                    await _checklistRepository.SaveChangesAsync();
                    Log.Information("Checklist persistido id: {id}", checklist.Id);
                }

                responseDTO.Object = checklist;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }

        public async Task<ResponseDTO> GetById(int id)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var checklist = await _checklistRepository.GetEntities()
                    .Include(x => x.ChecklistItems)!.ThenInclude(x => x.Item)
                    .Include(x => x.ChecklistItems)!.ThenInclude(x => x.Category)
                    .FirstOrDefaultAsync(x => x.Id == id);

                var jsonData = new
                {
                    id = checklist!.Id,
                    checklist.CreatedAt,
                    checklist.UpdatedAt,
                    name = checklist.Name,
                    requireFullReview = checklist.RequireFullReview,
                    categories = checklist.ChecklistItems?
                        .Where(x => x.ParentChecklistItemId == null)
                        .Select(item => new
                        {
                            id = item.Category.Id,
                            name = item.Category.Name,
                            items = new List<object>
                            {
                                new
                                {
                                    id = item.Item.Id,
                                    name = item.Item.Name,
                                    amountRequired = item.RequiredQuantity,
                                    subItems = item.ChecklistSubItems != null && item.ChecklistSubItems.Count != 0 ? item.ChecklistSubItems.Select(x=>new
                                    {
                                        id = x.Item.Id,
                                        name = x.Item.Name,
                                        amountRequired = x.RequiredQuantity
                                    }) : []
                                }
                            }
                        })
                        .GroupBy(category => new { category.id, category.name })
                        .Select(groupedCategory => new
                        {
                            groupedCategory.Key.id,
                            groupedCategory.Key.name,
                            items = groupedCategory.SelectMany(category => category.items)
                        })
                };

                responseDTO.Object = jsonData;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }
    }
}
