using AutoMapper;
using eshop.apiservices.Cache;
using eshop.core.DTO.Request;
using eshop.core.Entities;
using eshop.core.Interfaces.Repositories;
using eshop.core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eshop.apiservices.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderCached _order;
        private readonly IProductRepository _product;
        private readonly IMapper _mapper;

        public OrderController(IOrderCached order, IProductRepository product, IMapper mapper)
        {
            _order = order;
            _product = product;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Policy = "Customer")]
        public async Task<IActionResult> AddOrder([FromBody] OrderInfoRequest orderInfo)
        {
            if (!ModelState.IsValid || orderInfo.Items.Count < 1) return BadRequest();
            var productList = new List<ProductViewModel>();
            for (int i = 0; i < orderInfo.Items.Count; i++)
            {
                var p = await _product.GetProductAsync(orderInfo.Items[i].ProductId);
                if (p == null) return BadRequest();
                p.Quantity = orderInfo.Items[i].Quantity;
                productList.Add(p);
            }
            var orderItems = _mapper.Map<List<OrderItems>>(productList);
            var result = await _order.AddOrder(orderInfo.CustomerId, orderItems);
            if (result == null) return BadRequest();
            orderItems.ForEach(oi => oi.Order_Id = result.Id);
            result.OrderItems = _mapper.Map<List<OrderItemsViewModel>>(orderItems);
            return Ok(result);
        }

        [HttpGet("customer/{id}")]
        public async Task<IActionResult> GetAllCustomerOrders(int id)
        {
            var result = await _order.GetAllCustomerOrders(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _order.GetAllOrders();
            return Ok(result);
        }
    }
}
