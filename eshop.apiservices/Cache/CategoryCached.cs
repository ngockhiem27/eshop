using eshop.core.Entities;
using eshop.core.Interfaces.Repositories;
using eshop.core.ViewModels;
using eshop.infrastructure.RedisCache;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eshop.apiservices.Cache
{
    public class CategoryCached : BaseCached, ICategoryCached
    {
        private readonly ICategoryRepository _categoryRepository;
        private const string KEY_PREFIX = "Category.";

        public CategoryCached(ICategoryRepository categoryRepository, ICache cache) : base(cache)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
        {
            var cachedKey = GetKey(KEY_PREFIX, nameof(GetAllCategoryAsync));
            return await VerifyCache(cachedKey, () => _categoryRepository.GetAllCategoryAsync());
        }

        public async Task<List<ProductCategoryViewModel>> GetAllCategoryWithProductAsync()
        {
            var cachedKey = GetKey(KEY_PREFIX, nameof(GetAllCategoryWithProductAsync));
            return await VerifyCache(cachedKey, () => _categoryRepository.GetAllCategoryWithProductAsync());
        }

        public async Task<CategoryViewModel> GetCategoryAsync(int id)
        {
            var cachedKey = GetKey(KEY_PREFIX, nameof(GetCategoryAsync), id);
            return await VerifyCache(cachedKey, () => _categoryRepository.GetCategoryAsync(id));
        }

        public async Task<CategoryViewModel> AddCategoryAsync(Category category)
        {
            await ClearGetAllKeysAsync();
            return await _categoryRepository.AddCategoryAsync(category);
        }

        public async Task<int> DeleteCategoryAsync(int id)
        {
            await ClearGetAllKeysAsync();
            await ClearGetIdKeyAsync(id);
            return await _categoryRepository.DeleteCategoryAsync(id);
        }

        public async Task<CategoryViewModel> UpdateCategoryAsync(int id, Category category)
        {
            await ClearGetAllKeysAsync();
            await ClearGetIdKeyAsync(id);
            return await _categoryRepository.UpdateCategoryAsync(id, category);
        }

        private async Task ClearGetAllKeysAsync()
        {
            var cachedKeyAll1 = GetKey(KEY_PREFIX, nameof(GetAllCategoryAsync));
            var cachedKeyAll2 = GetKey(KEY_PREFIX, nameof(GetAllCategoryWithProductAsync));
            await _cache.RemoveAsync(cachedKeyAll1);
            await _cache.RemoveAsync(cachedKeyAll2);
        }

        private async Task ClearGetIdKeyAsync(int id)
        {
            var cachedKey = GetKey(KEY_PREFIX, nameof(GetCategoryAsync), id);
            await _cache.RemoveAsync(cachedKey);
        }
    }
}
