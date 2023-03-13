using AkademiECommerce.Shared.Dtos;
using ECommerce.Services.Basket.Dtos;
using System.Threading.Tasks;

namespace ECommerce.Services.Basket.Services
{
    public interface IBasketService
    {
        Task<ResponseDTO<BasketDto>> GetBasket(string Userid);
        Task<ResponseDTO<bool>> SaveorUpdate(BasketDto basket);
        Task<ResponseDTO<bool>> Delete(string Userid);
    }
}
