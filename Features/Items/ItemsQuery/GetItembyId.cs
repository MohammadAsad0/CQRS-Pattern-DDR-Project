using DDR_PROJECTAPIS.Data;
using DDR_PROJECTAPIS.Models;
using MediatR;

namespace DDR_PROJECTAPIS.Features.Items.ItemsQuery
{
    public class GetItembyId
    {
        public class Query : IRequest<Item>
        {
            public Guid Id { get; set; }
        }


        public class QueryHandler : IRequestHandler<Query, Item>
        {
            private readonly ItemsContext _itemsContext;

            public QueryHandler(ItemsContext itemsContext)
            {
                _itemsContext = itemsContext;
            }

            public async Task<Item> Handle(Query request, CancellationToken cancellation)
            {
                return await _itemsContext.Items.FindAsync(request.Id);
            }
        }


    }
}