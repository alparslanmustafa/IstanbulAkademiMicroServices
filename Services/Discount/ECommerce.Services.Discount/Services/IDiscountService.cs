using AkademiECommerce.Shared.Dtos;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Services.Discount.Services
{
    public interface IDiscountService
    {
        Task<ResponseDTO<List<Models.Discount>>> GetAll();
        Task<ResponseDTO<Models.Discount>> GetById(int id);
        Task<ResponseDTO<NoContent>> Insert(Models.Discount discount);
        Task<ResponseDTO<NoContent>> Update(Models.Discount discount);
        Task<ResponseDTO<NoContent>> Delete(int id);
        Task<ResponseDTO<Models.Discount>> GetByCodeUser(string code, string id);
    }
}
