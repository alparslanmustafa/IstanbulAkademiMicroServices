using AkademiECommerce.Shared.Dtos;
using AutoMapper;
using AutoMapper.Internal.Mappers;
using ECommerce.Services.Order.Application.Dtos;
using ECommerce.Services.Order.Application.Mapping;
using ECommerce.Services.Order.Application.Queries;
using ECommerce.Services.Order.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Services.Order.Application.Handlers
{
    public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, ResponseDTO<List<OrderDto>>>
    {
        private readonly OrderDbContext _orderDbContext;

        public GetOrdersByUserIdQueryHandler(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;

        }



        public async Task<ResponseDTO<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderDbContext.Orders.Include(x => x.OrderItems).Where(y => y.BuyerID == request.UserId).ToListAsync();
            if (!orders.Any())
            {
                return ResponseDTO<List<OrderDto>>.Success(new List<OrderDto>(), 200);
            }



            var ordersDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);



            return ResponseDTO<List<OrderDto>>.Success(ordersDto, 200);
        }
    }
}
