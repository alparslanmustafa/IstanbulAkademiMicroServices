using AkademiECommerce.Shared.Dtos;
using ECommerce.Services.Catalog.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Services.Catalog.Services
{
    public interface ICategoryService
    {
        Task<ResponseDTO<List<CategoryDto>>> GetAllAsync();
        Task<ResponseDTO<CategoryDto>> CreateAsync(CategoryDto categoryDto);
        Task<ResponseDTO<CategoryDto>> GetByIdAsync(string id);
        Task<ResponseDTO<CategoryDto>> DeleteAsync(string id);
        Task<ResponseDTO<CategoryDto>> UpdateAsync(string id);
    }
}
