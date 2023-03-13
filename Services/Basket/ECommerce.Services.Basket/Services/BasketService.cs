using AkademiECommerce.Shared.Dtos;
using ECommerce.Services.Basket.Dtos;
using Microsoft.OpenApi.Expressions;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Services.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<ResponseDTO<bool>> Delete(string Userid)
        {
            var status=await _redisService.GetDb().KeyDeleteAsync(Userid);
            return status ? ResponseDTO<bool>.Success(204) : ResponseDTO<bool>.Fail("Sepet bulunamadı.", 404);
        }

        public async Task<ResponseDTO<BasketDto>> GetBasket(string Userid)
        {
            var existBasket = await _redisService.GetDb().StringGetAsync(Userid);
            if (string.IsNullOrEmpty(existBasket))
            {
                return ResponseDTO<BasketDto>.Fail("Sepet bulunamadı.", 404);
            }
            return ResponseDTO<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existBasket), 200);
        }

        public async Task<ResponseDTO<bool>> SaveorUpdate(BasketDto basket)
        {
            var status = await _redisService.GetDb().StringSetAsync(basket.UserId, JsonSerializer.Serialize(basket));
            return status ? ResponseDTO<bool>.Success(204) : ResponseDTO<bool>.Fail("Bir hata oluştu.", 500);
        }

    }
}
