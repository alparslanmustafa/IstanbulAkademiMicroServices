using AkademiECommerce.Shared.Dtos;
using ECommerce.Services.Order.Application.Commands;
using ECommerce.Services.Order.Application.Dtos;
using ECommerce.Services.Order.Domain.OrderAggregate;
using ECommerce.Services.Order.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Services.Order.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ResponseDTO<CreatedOrderDto>>
    {
        private readonly OrderDbContext _orderDbContext;

        public CreateOrderCommandHandler(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<ResponseDTO<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var address = new Address(request.Address.City,request.Address.District,request.Address.Street,request.Address.ZipCode);
            Domain.OrderAggregate.Order neworder = new Domain.OrderAggregate.Order(request.BuyerId,address);
            request.Items.ForEach(x =>
            {
                neworder.AddOrderItem(x.ProductId, x.ProductName, x.PictureURL, x.Price);
            });
            await _orderDbContext.Orders.AddAsync(neworder);
            await _orderDbContext.SaveChangesAsync();
            return ResponseDTO<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId = neworder.Id }, 200);
        }
    }
}
