using AkademiECommerce.Shared.Dtos;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbconnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbconnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<ResponseDTO<NoContent>> Delete(int id)
        {
            var status = await _dbconnection.ExecuteAsync("Delete from Discount where id=@Id", new { Id = id });
            return status > 0 ? ResponseDTO<NoContent>.Success(204) : ResponseDTO<NoContent>.Fail("Silinecek değer bulunamadı", 404);
        }

        public async Task<ResponseDTO<List<Models.Discount>>> GetAll()
        {
            var discount = await _dbconnection.QueryAsync<Models.Discount>("Select * from discount");
            return ResponseDTO<List<Models.Discount>>.Success(discount.ToList(), 200);

        }

        public async Task<ResponseDTO<Models.Discount>> GetByCodeUser(string code, string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ResponseDTO<Models.Discount>> GetById(int id)
        {
            var discount = (await _dbconnection.QueryAsync<Models.Discount>("Select * from discount where id=@Id", new { Id = id })).SingleOrDefault();
            if (discount == null)
            {
                return ResponseDTO<Models.Discount>.Fail("Değer bulunamadı", 404);
            }
            return ResponseDTO<Models.Discount>.Success(discount, 200);
        }

        public async Task<ResponseDTO<NoContent>> Insert(Models.Discount discount)
        {
            var status = await _dbconnection.ExecuteAsync("Insert into discount (UserId,Rate,Code) values (@UserId, @Rate, @Code)", discount);
            return status > 0 ? ResponseDTO<NoContent>.Success(204) : ResponseDTO<NoContent>.Fail("Bir hata oluştu", 500);
        }

        public async Task<ResponseDTO<NoContent>> Update(Models.Discount discount)
        {
            var status = await _dbconnection.ExecuteAsync("Update Discount set userid=@UserId, rate=@Rate, code=@Code where id=@Id",
                new
                {
                    Id=discount.ID,
                    UserId=discount.UserID,
                    Rate=discount.Rate,
                    Code=discount.Code
                });
            return status > 0 ? ResponseDTO<NoContent>.Success(204) : ResponseDTO<NoContent>.Fail("Bir hata oluştu", 500);
        }
    }
}
