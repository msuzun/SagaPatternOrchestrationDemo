using Microsoft.AspNetCore.Mvc;
using SagaPatternOrchestrationDemo.App;
using SagaPatternOrchestrationDemo.Model;

namespace SagaPatternOrchestrationDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderSagaOrchestrator _orderSagaOrchestrator;
        public OrderController(OrderSagaOrchestrator orderSagaOrchestrator)
        {
            _orderSagaOrchestrator = orderSagaOrchestrator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody]OrderRequest order)
        {
            var result = await _orderSagaOrchestrator.ProcessOrderAsync(order);
            return result ? Ok("Order proccessed successfully") : BadRequest("Order processing failed");
        }
    }
}
