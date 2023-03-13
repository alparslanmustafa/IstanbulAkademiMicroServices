using AkademiECommerce.Shared.Dtos;
using ECommerce.Services.Order.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Order.Application.Queries
{
    public class GetOrdersByUserIdQuery:IRequest<ResponseDTO<List<OrderDto>>>
    {
        public string UserId { get; set; }
    }
}
