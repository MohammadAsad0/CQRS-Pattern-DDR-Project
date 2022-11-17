using DDR_PROJECTAPIS.Data;
using DDR_PROJECTAPIS.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DDR_PROJECTAPIS.Features.Items.ItemsQuery
{
    public class GetItems
    {
        public class Query : IRequest<IEnumerable<Item>>
        { }

        public class QueryHandler : IRequestHandler<Query, IEnumerable<Item>>
        {
            private readonly ItemsContext _context;
            public QueryHandler(ItemsContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Item>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Items.ToListAsync(cancellationToken);
            }

        }
    }
}
