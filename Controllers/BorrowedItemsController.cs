
using DDR_PROJECTAPIS.Features.Items.ItemsCommand;
using DDR_PROJECTAPIS.Features.Items.ItemsQuery;
using DDR_PROJECTAPIS.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDR_PROJECTAPIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowedItemsController : Controller
    {
        private readonly IMediator _mediator;

        public BorrowedItemsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IEnumerable<Item>> GetItems() => await _mediator.Send(new GetItems.Query());

        [HttpGet("{id}")]
        public async Task<Item> GetItem(Guid id) => await _mediator.Send(new GetItembyId.Query { Id = id });

        [HttpPost]
        public async Task<ActionResult> CreateItem([FromBody] AddNewItem.Command command)
        {
            var createdItemId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetItem), new { id = createdItemId }, null);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteItem(Guid id)
        {
            await _mediator.Send(new DeleteItem.Command { Id = id });
            return NoContent();

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemById(Guid id, UpdateItemById.Command command)
        {

            return Ok(await _mediator.Send(command));
        }
    }
}
