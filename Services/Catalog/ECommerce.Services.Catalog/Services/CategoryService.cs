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
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IDatabaseSettings databaseSettings, IMapper mapper)
        {
            var client=new MongoClient(databaseSettings.ConnectionString);
            var database=client.GetDatabase(databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<ResponseDTO<CategoryDto>> CreateAsync(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryCollection.InsertOneAsync(category);
            return ResponseDTO<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }

        public Task<ResponseDTO<CategoryDto>> DeleteAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ResponseDTO<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryCollection.Find(category => true).ToListAsync();
            return ResponseDTO<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
        }

        public async Task<ResponseDTO<CategoryDto>> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find<Category>(x => x.Id == id).FirstOrDefaultAsync();
            if (category == null)
            {
                return ResponseDTO<CategoryDto>.Fail("Kategori Bulunamadı", 404);
            }
            else
            {
                return ResponseDTO<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
            }
        }

        public Task<ResponseDTO<CategoryDto>> UpdateAsync(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
