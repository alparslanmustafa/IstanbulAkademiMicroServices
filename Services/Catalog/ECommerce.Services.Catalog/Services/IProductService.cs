using AkademiECommerce.Shared.Dtos;
using ECommerce.Services.Catalog.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Services.Catalog.Services
{
    public interface IProductService
    {
        Task<ResponseDTO<List<ProductDto>>> GetAllAsync();
        Task<ResponseDTO<ProductDto>> CreateAsync(ProductCreateDto productCreateDto);
        Task<ResponseDTO<ProductDto>> GetByIdAsync(string id);
        Task<ResponseDTO<NoContent>> DeleteAsync(string id);
        Task<ResponseDTO<NoContent>> UpdateAsync(ProductUpdateDto productUpdateDto);
    }
}
