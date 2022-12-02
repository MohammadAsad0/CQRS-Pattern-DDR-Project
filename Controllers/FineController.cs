//using DDR_PROJECTAPIS.Features.Fine.Fine_Querry;
using DDR_PROJECTAPIS.Features.Fines.Fine_Querry;
using DDR_PROJECTAPIS.Features.Items.ItemsQuery;
using DDR_PROJECTAPIS.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDR_PROJECTAPIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FineController : Controller
    {
        private readonly IMediator _mediator;

        public FineController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IEnumerable<object>> GetAllFine() => await _mediator.Send(new GetAllFine.Query());
        
        [HttpGet("{id}")]
        public async Task<object> GetFineByStudentId(string id) => await _mediator.Send(new GetFineByStudentId.Query { StudentId = id });
    }
}
