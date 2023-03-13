using AkademiECommerce.Shared.Dtos;
using AutoMapper;
using ECommerce.Services.Catalog.Dtos;
using ECommerce.Services.Catalog.Models;
using ECommerce.Services.Catalog.Settings;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Services.Catalog.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Category> _categoryCollection;
        public ProductService(IDatabaseSettings databaseSettings, IMapper mapper)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }
        public async Task<ResponseDTO<ProductDto>> CreateAsync(ProductCreateDto productCreateDto)
        {
            var product = _mapper.Map<Product>(productCreateDto);
            await _productCollection.InsertOneAsync(product);
            return ResponseDTO<ProductDto>.Success(_mapper.Map<ProductDto>(product), 200);
        }

        public async Task<ResponseDTO<NoContent>> DeleteAsync(string id)
        {
            var result = await _productCollection.DeleteOneAsync(x => x.Id == id);
            if (result.DeletedCount > 0)
            {
                return ResponseDTO<NoContent>.Success(204);
            }
            else
            {
                return ResponseDTO<NoContent>.Fail("Ürün Bulunamadı", 404);
            }
        }

        public async Task<ResponseDTO<List<ProductDto>>> GetAllAsync()
        {
            var products = await _productCollection.Find(product => true).ToListAsync();
            return ResponseDTO<List<ProductDto>>.Success(_mapper.Map<List<ProductDto>>(products), 200);
        }

        public async Task<ResponseDTO<ProductDto>> GetByIdAsync(string id)
        {
            var product = await _productCollection.Find<Product>(x => x.Id == id).FirstOrDefaultAsync();
            if (product == null)
            {
                return ResponseDTO<ProductDto>.Fail("Ürün bulunamadı", 404);
            }
            else
            {
                return ResponseDTO<ProductDto>.Success(_mapper.Map<ProductDto>(product), 200);
            }
        }

        public async Task<ResponseDTO<NoContent>> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            var updatedProduct = _mapper.Map<Product>(productUpdateDto);
            var result = await _productCollection.FindOneAndReplaceAsync(x => x.Id == productUpdateDto.Id, updatedProduct);
            if (result == null)
            {
                return ResponseDTO<NoContent>.Fail("Ürün Bulunamadı", 404);
            }
            else
            {
                return ResponseDTO<NoContent>.Success(204);
            }
        }
    }
}
