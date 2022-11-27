using DDR_PROJECTAPIS.Features.BorrowedItems.BorrowedItemsCommand;
using DDR_PROJECTAPIS.Features.BorrowedItems.BorrowedItemsQuery;
using DDR_PROJECTAPIS.Features.Items.ItemsCommand;
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
        public async Task<IEnumerable<object>> GetBorrowedItems() => await _mediator.Send(new GetBorrowedItems.Query());

        [HttpGet("{id}")]
        public async Task<IEnumerable<object>> GetBorrowedItembyStudentId(string id) => await _mediator.Send(new GetBorrowedItembyStudentId.Query { StudentId = id });

        [HttpPost]
        public async Task<ActionResult> CreateBorrowedItem([FromBody] AddNewBorrowedItem.Command command)
        {
            var createdBorrowedItemId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetBorrowedItems), new { id = createdBorrowedItemId }, null);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBorrowedItem(Guid ItemId, string StudentId)
        {
            await _mediator.Send(new DeleteBorrowedItem.Command { ItemId = ItemId, StudentId = StudentId });
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBorrowedItemByItemId(Guid ItemId, string StudentId, UpdateBorrowedItemById.Command command)
        {
            command.ItemId = ItemId;
            command.StudentId = StudentId;
            return Ok(await _mediator.Send(command));
        }
    }
}
