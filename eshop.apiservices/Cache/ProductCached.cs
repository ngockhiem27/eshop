using eshop.core.Entities;
using eshop.core.Interfaces.Repositories;
using eshop.core.ViewModels;
using eshop.infrastructure.RedisCache;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eshop.apiservices.Cache
{
    public class ProductCached : BaseCached, IProductCached
    {
        private readonly IProductRepository _productRepository;
        private const string KEY_PREFIX = "Product.";

        public ProductCached(IRedisCache cache, IProductRepository productRepository) : base(cache)
        {
            _productRepository = productRepository;
        }

        private async Task ClearGetAllProductKey()
        {
            var cachedKey1 = GetKey(KEY_PREFIX, nameof(GetAllProductAsync));
            var cachedKey2 = GetKey(KEY_PREFIX, nameof(GetAllProductWithCategoryAsync));
            await _cache.RemoveAsync(cachedKey1);
            await _cache.RemoveAsync(cachedKey2);
        }

        private async Task ClearGetProductIdKey(int id)
        {
            var cachedKey = GetKey(KEY_PREFIX, nameof(GetProductAsync), id);
            await _cache.RemoveAsync(cachedKey);
        }

        public async Task<List<ProductViewModel>> GetAllProductAsync()
        {
            var cachedKey = GetKey(KEY_PREFIX, nameof(GetAllProductAsync));
            return await VerifyCache(cachedKey, () => _productRepository.GetAllProductAsync());
        }

        public async Task<List<ProductCategoryViewModel>> GetAllProductWithCategoryAsync()
        {
            var cachedKey = GetKey(KEY_PREFIX, nameof(GetAllProductWithCategoryAsync));
            return await VerifyCache(cachedKey, () => _productRepository.GetAllProductWithCategoryAsync());
        }

        public async Task<ProductViewModel> GetProductAsync(int id)
        {
            var cachedKey = GetKey(KEY_PREFIX, nameof(GetProductAsync), id);
            return await VerifyCache(cachedKey, () => _productRepository.GetProductAsync(id));
        }

        public async Task<ProductViewModel> AddProductAsync(Product p, List<CategoryViewModel> c)
        {
            await ClearGetAllProductKey();
            return await _productRepository.AddProductAsync(p, c);
        }

        public async Task<int> DeleteProductAsync(int id)
        {
            await ClearGetAllProductKey();
            await ClearGetProductIdKey(id);
            return await _productRepository.DeleteProductAsync(id);
        }

        public async Task<ProductViewModel> UpdateProductAsync(int id, Product p, List<CategoryViewModel> categories)
        {
            await ClearGetAllProductKey();
            await ClearGetProductIdKey(id);
            return await _productRepository.UpdateProductAsync(id, p, categories);
        }

        private async Task ClearGetProductCategoryKey(int id)
        {
            var cachedKey = GetKey(KEY_PREFIX, nameof(GetProductCategoryAsync), id);
            await _cache.RemoveAsync(cachedKey);
        }

        public async Task<List<int>> GetProductCategoryAsync(int id)
        {
            var cachedKey = GetKey(KEY_PREFIX, nameof(GetProductCategoryAsync), id);
            return await VerifyCache(cachedKey, () => _productRepository.GetProductCategoryAsync(id));
        }

        public async Task<int> AddProductCategoryAsync(int productId, int categoryId)
        {
            await ClearGetProductCategoryKey(productId);
            return await _productRepository.AddProductCategoryAsync(productId, categoryId);
        }

        public async Task<int> DeleteProductCategoryAsync(int productId, int categoryId)
        {
            await ClearGetProductCategoryKey(productId);
            return await _productRepository.DeleteProductCategoryAsync(productId, categoryId);
        }

        private async Task ClearGetProductImageAsync(int id)
        {
            var cachedKey = GetKey(KEY_PREFIX, nameof(GetProductImage), id);
            await _cache.RemoveAsync(cachedKey);
        }

        public async Task<List<ImageViewModel>> GetProductImage(int productId)
        {
            var cachedKey = GetKey(KEY_PREFIX, nameof(GetProductImage), productId);
            return await VerifyCache(cachedKey, () => _productRepository.GetProductImage(productId));
        }

        public async Task<ImageViewModel> AddProductImage(int productId, string filePath)
        {
            await ClearGetProductImageAsync(productId);
            return await _productRepository.AddProductImage(productId, filePath);
        }

        private async Task ClearGetImageAsync(int id)
        {
            var cachedKey = GetKey(KEY_PREFIX, nameof(GetImage), id);
            await _cache.RemoveAsync(cachedKey);
        }

        public async Task<ImageViewModel> GetImage(int imageId)
        {
            var cachedKey = GetKey(KEY_PREFIX, nameof(GetImage), imageId);
            return await VerifyCache(cachedKey, () => _productRepository.GetImage(imageId));
        }

        public async Task<int> DeleteProductImage(int productId, int imgId)
        {
            await ClearGetImageAsync(imgId);
            await ClearGetProductImageAsync(productId);
            return await _productRepository.DeleteProductImage(productId, imgId);
        }
    }
}
