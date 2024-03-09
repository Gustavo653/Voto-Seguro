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
    public class ChecklistReviewService(IChecklistReviewRepository checklistReviewRepository,
                                        IChecklistRepository checklistRepository,
                                        ICategoryRepository categoryRepository,
                                        IChecklistItemRepository checklistItemRepository,
                                        IUserRepository userRepository,
                                        IHttpContextAccessor httpContextAccessor,
                                        IAmbulanceRepository ambulanceRepository) : IChecklistReviewService
    {
        private readonly IChecklistReviewRepository _checklistReviewRepository = checklistReviewRepository;
        private readonly IChecklistRepository _checklistRepository = checklistRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IChecklistItemRepository _checklistItemRepository = checklistItemRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IAmbulanceRepository _ambulanceRepository = ambulanceRepository;
        private ISession _session => _httpContextAccessor.HttpContext!.Session;

        public async Task<ResponseDTO> Create(ChecklistReviewDTO checklistReviewDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                checklistReviewDTO.IdUser = Convert.ToInt32(_session.GetString(Consts.ClaimUserId));

                var checklist = await _checklistRepository.GetTrackedEntities().FirstOrDefaultAsync(x => x.Id == checklistReviewDTO.IdChecklist);
                if (checklist == null)
                {
                    responseDTO.SetBadInput($"O checklist {checklistReviewDTO.IdChecklist} não existe!");
                    return responseDTO;
                }

                if (checklist.RequireFullReview && checklistReviewDTO.Type != Domain.Enum.ReviewType.Full)
                {
                    responseDTO.SetBadInput("O checklist deve ser completo!");
                    return responseDTO;
                }

                var user = await _userRepository.GetTrackedEntities().FirstOrDefaultAsync(x => x.Id == checklistReviewDTO.IdUser);
                if (user == null)
                {
                    responseDTO.SetBadInput($"O usuário {checklistReviewDTO.IdUser} não existe!");
                    return responseDTO;
                }

                var ambulance = await _ambulanceRepository.GetTrackedEntities().FirstOrDefaultAsync(x => x.Id == checklistReviewDTO.IdAmbulance);
                if (ambulance == null)
                {
                    responseDTO.SetBadInput($"A ambulância {checklistReviewDTO.IdAmbulance} não existe!");
                    return responseDTO;
                }

                var checklistReview = new ChecklistReview
                {
                    Type = checklistReviewDTO.Type,
                    Observation = checklistReviewDTO.Observation,
                    Checklist = checklist,
                    Ambulance = ambulance,
                    User = user,
                };
                checklistReview.SetCreatedAt();

                await _checklistReviewRepository.InsertAsync(checklistReview);

                await ProcessChecklistReviewItems(checklistReviewDTO, checklistReview, responseDTO);

                if (responseDTO.Code == 200)
                {
                    await _checklistReviewRepository.SaveChangesAsync();
                    Log.Information("ChecklistReview persistido id: {id}", checklistReview.Id);
                }

                responseDTO.Object = checklistReviewDTO;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }

        private static void ValidateCategoryExists(ChecklistReviewDTO checklistReviewDTO, int categoryId, ResponseDTO responseDTO)
        {
            if (!checklistReviewDTO.Categories.Any(x => x.Id == categoryId))
            {
                responseDTO.SetBadInput($"A categoria {categoryId} deve ser conferida");
                return;
            }
        }

        private static void ValidateItemExists(CategoryReviewDTO category, int itemId, ResponseDTO responseDTO)
        {
            if (!category.Items.Any(i => i.Id == itemId))
            {
                responseDTO.SetBadInput($"O item {itemId} deve ser conferido");
                return;
            }
        }

        private static void ValidateSubItemsExist(ItemReviewDTO itemDTO, List<int>? subItemIds, ResponseDTO responseDTO)
        {
            if (subItemIds != null && subItemIds.Count != 0 && (itemDTO.SubItems == null || itemDTO.SubItems.Count == 0))
            {
                responseDTO.SetBadInput("O checklist selecionado possui subitens");
                return;
            }

            if (subItemIds != null)
            {
                foreach (var subItemId in subItemIds)
                {
                    if (!itemDTO.SubItems!.Any(i => i.Id == subItemId))
                    {
                        responseDTO.SetBadInput($"O item {subItemId} deve ser conferido");
                        return;
                    }
                }
            }
        }


        private async Task ProcessChecklistReviewItems(ChecklistReviewDTO checklistReviewDTO, ChecklistReview checklistReview, ResponseDTO responseDTO)
        {
            if (checklistReviewDTO.Type == Domain.Enum.ReviewType.Full)
            {
                var checklist = await _checklistItemRepository.GetEntities()
                                                             .Where(x => x.ChecklistId == checklistReviewDTO.IdChecklist && x.ParentChecklistItem == null)
                                                             .Select(x => new
                                                             {
                                                                 x.CategoryId,
                                                                 x.ItemId,
                                                                 SubItemIds = x.ChecklistSubItems != null && x.ChecklistSubItems.Count > 0 ?
                                                                                x.ChecklistSubItems.Select(c => c.ItemId).ToList() : null,
                                                             })
                                                             .ToListAsync();

                foreach (var item in checklist)
                {
                    ValidateCategoryExists(checklistReviewDTO, item.CategoryId, responseDTO);
                    if (responseDTO.Code != 200) return;
                    Log.Information("Categorias validadas");

                    var categoryReviewDTO = checklistReviewDTO.Categories.First(x => x.Id == item.CategoryId);

                    ValidateItemExists(categoryReviewDTO, item.ItemId, responseDTO);
                    if (responseDTO.Code != 200) return;
                    Log.Information("Itens validados");

                    var itemReviewDTO = categoryReviewDTO.Items.First(i => i.Id == item.ItemId);

                    ValidateSubItemsExist(itemReviewDTO, item.SubItemIds, responseDTO);
                    if (responseDTO.Code != 200) return;
                    Log.Information("Subitens validados");
                }
            }

            foreach (var categoryReviewDTO in checklistReviewDTO.Categories)
            {
                Log.Information("Processando categoria: {id} Quantidade Itens: {qtd}", categoryReviewDTO.Id, categoryReviewDTO.Items.Count());
                var category = await _categoryRepository.GetTrackedEntities()
                                                        .FirstOrDefaultAsync(x => x.Id == categoryReviewDTO.Id);
                if (category == null)
                {
                    responseDTO.SetBadInput($"A categoria {categoryReviewDTO.Id} não existe");
                    return;
                }

                foreach (var itemReviewDTO in categoryReviewDTO.Items)
                {
                    Log.Information("Processando item: {id}", itemReviewDTO.Id);
                    var checklistItem = await _checklistItemRepository.GetTrackedEntities()
                                                             .Include(x => x.ChecklistReplacedItems)
                                                             .Include(x => x.Item)
                                                             .FirstOrDefaultAsync(x => x.Item.Id == itemReviewDTO.Id && x.Category.Id == category.Id && x.ParentChecklistItemId == null);

                    if (checklistItem == null)
                    {
                        responseDTO.SetBadInput($"O item {itemReviewDTO.Id} não existe");
                        return;
                    }

                    var checklistReplacedItem = new ChecklistReplacedItem()
                    {
                        ReplacedQuantity = itemReviewDTO.ReplacedQuantity,
                        ReplenishmentQuantity = itemReviewDTO.ReplenishmentQuantity,
                        RequiredQuantity = itemReviewDTO.RequiredQuantity,
                        ChecklistItem = checklistItem,
                        ChecklistReview = checklistReview,
                        TenantId = Convert.ToInt32(_session.GetString(Consts.ClaimTenantId))
                    };
                    checklistReplacedItem.SetCreatedAt();
                    checklistItem.ChecklistReplacedItems?.Add(checklistReplacedItem);
                    Log.Information("Item {id} processado", itemReviewDTO.Id);

                    if (itemReviewDTO.SubItems != null && itemReviewDTO.SubItems.Count != 0)
                    {
                        foreach (var subItemReviewDTO in itemReviewDTO.SubItems)
                        {
                            Log.Information("Processando subItem: {id}", subItemReviewDTO.Id);
                            var subChecklistItem = await _checklistItemRepository.GetTrackedEntities()
                                         .Include(x => x.ChecklistReplacedItems)
                                         .Include(x => x.Item)
                                         .FirstOrDefaultAsync(x => x.Item.Id == subItemReviewDTO.Id && x.Category.Id == category.Id);

                            if (subChecklistItem == null)
                            {
                                responseDTO.SetBadInput($"O item {subItemReviewDTO.Id} não existe");
                                return;
                            }

                            var subChecklistReplacedItem = new ChecklistReplacedItem()
                            {
                                ReplacedQuantity = subItemReviewDTO.ReplacedQuantity,
                                ReplenishmentQuantity = subItemReviewDTO.ReplenishmentQuantity,
                                RequiredQuantity = subItemReviewDTO.RequiredQuantity,
                                ChecklistItem = subChecklistItem,
                                ChecklistReview = checklistReview,
                                TenantId = Convert.ToInt32(_session.GetString(Consts.ClaimTenantId))
                            };
                            subChecklistReplacedItem.SetCreatedAt();
                            subChecklistItem.ChecklistReplacedItems?.Add(subChecklistReplacedItem);
                            Log.Information("Subitem {id} processado", subItemReviewDTO.Id);
                        }
                    }
                }
            }
        }

        public async Task<ResponseDTO> Update(int id, ChecklistReviewDTO checklistReviewDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                checklistReviewDTO.IdUser = Convert.ToInt32(_session.GetString(Consts.ClaimUserId));
                var checklistReview = await _checklistReviewRepository.GetTrackedEntities()
                                                                      .Include(x => x.ChecklistReplacedItems)
                                                                      .FirstOrDefaultAsync(c => c.Id == id);
                if (checklistReview == null)
                {
                    responseDTO.SetBadInput($"A conferência do checklist {id} não existe!");
                    return responseDTO;
                }

                var checklist = await _checklistRepository.GetTrackedEntities()
                                                          .FirstOrDefaultAsync(x => x.Id == checklistReviewDTO.IdChecklist);
                if (checklist == null)
                {
                    responseDTO.SetBadInput($"O checklist {checklistReviewDTO.IdChecklist} não existe!");
                    return responseDTO;
                }

                if (checklist.RequireFullReview && checklistReviewDTO.Type != Domain.Enum.ReviewType.Full)
                {
                    responseDTO.SetBadInput("O checklist deve ser completo!");
                    return responseDTO;
                }

                checklistReview.Observation = checklistReviewDTO.Observation;
                checklistReview.Checklist = checklist;

                checklistReview.ChecklistReplacedItems?.RemoveAll(_ => true);

                await ProcessChecklistReviewItems(checklistReviewDTO, checklistReview, responseDTO);

                checklistReview.SetUpdatedAt();

                if (responseDTO.Code == 200)
                {
                    await _checklistReviewRepository.SaveChangesAsync();
                    Log.Information("ChecklistReview persistido id: {id}", checklistReview.Id);
                }

                responseDTO.Object = checklistReview;
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
                var checklistReview = await _checklistReviewRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (checklistReview == null)
                {
                    responseDTO.SetBadInput($"O checklist com id: {id} não existe!");
                    return responseDTO;
                }

                _checklistReviewRepository.Delete(checklistReview);
                await _checklistReviewRepository.SaveChangesAsync();
                Log.Information("ChecklistReview removido id: {id}", checklistReview.Id);

                responseDTO.Object = checklistReview;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }

        public async Task<ResponseDTO> GetList(int? takeLast)
        {
            ResponseDTO responseDTO = new();
            try
            {
                responseDTO.Object = await _checklistReviewRepository.GetEntities()
                                                                     .Select(x => new
                                                                     {
                                                                         x.Id,
                                                                         x.Type,
                                                                         x.CreatedAt,
                                                                         x.UpdatedAt,
                                                                         x.Ambulance,
                                                                         x.Observation,
                                                                         User = x.User.Name,
                                                                         x.Checklist,
                                                                         ChecklistReviews = x.ChecklistReplacedItems != null &&
                                                                                            x.ChecklistReplacedItems.Count != 0 ?
                                                                                            x.ChecklistReplacedItems.Where(x => x.ChecklistItem.ParentChecklistItemId == null)
                                                                                                                    .Select(x => new
                                                                                                                    {
                                                                                                                        category = x.ChecklistItem.Category.Name,
                                                                                                                        item = x.ChecklistItem.Item.Name,
                                                                                                                        requiredQuantity = x.RequiredQuantity,
                                                                                                                        replacedQuantity = x.ReplacedQuantity,
                                                                                                                        replenishmentQuantity = x.ReplenishmentQuantity,
                                                                                                                        subItems = x.ChecklistItem.ChecklistSubItems != null &&
                                                                                                                                   x.ChecklistItem.ChecklistSubItems.Count != 0 ?
                                                                                                                                   x.ChecklistItem.ChecklistSubItems.Select(x => new
                                                                                                                                   {
                                                                                                                                       category = x.Category.Name,
                                                                                                                                       item = x.Item.Name,
                                                                                                                                       requiredQuantity = x.ChecklistReplacedItems != null &&
                                                                                                                                                        x.ChecklistReplacedItems.Count != 0 ?
                                                                                                                                                        x.ChecklistReplacedItems.FirstOrDefault()!.RequiredQuantity : int.MinValue,
                                                                                                                                       replacedQuantity = x.ChecklistReplacedItems != null &&
                                                                                                                                                        x.ChecklistReplacedItems.Count != 0 ?
                                                                                                                                                        x.ChecklistReplacedItems.FirstOrDefault()!.ReplacedQuantity : int.MinValue,
                                                                                                                                       replenishmentQuantity = x.ChecklistReplacedItems != null &&
                                                                                                                                                        x.ChecklistReplacedItems.Count != 0 ?
                                                                                                                                                        x.ChecklistReplacedItems.FirstOrDefault()!.ReplenishmentQuantity : int.MinValue,
                                                                                                                                   }) : null
                                                                                                                    }) : null
                                                                     })
                                                                     .OrderByDescending(x => x.Id)
                                                                     .Take(takeLast ?? 1000)
                                                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }

            return responseDTO;
        }

        public Task<ResponseDTO> GetList()
        {
            throw new NotImplementedException();
        }
    }
}