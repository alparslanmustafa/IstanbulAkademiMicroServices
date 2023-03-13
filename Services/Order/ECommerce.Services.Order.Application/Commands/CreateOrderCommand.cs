using AkademiECommerce.Shared.Dtos;
using ECommerce.Services.Order.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Order.Application.Commands
{
    public class CreateOrderCommand:IRequest<ResponseDTO<CreatedOrderDto>>
    {
        public string BuyerId { get; set; }
        public List<OrderItemDto> Items { get; set; }
        public AddressDto Address { get; set; }
    }
}
