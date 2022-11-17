using DDR_PROJECTAPIS.Data;
using DDR_PROJECTAPIS.Models;
using MediatR;
using System.Diagnostics.Eventing.Reader;

namespace DDR_PROJECTAPIS.Features.Items.ItemsCommand
{
    public class AddNewItem
    {
        public class Command : IRequest<Guid>
        {
            public string ItemName { get; set; }
            public int Price { get; set; }

            public int Quantity { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command, Guid>
        {
            private readonly ItemsContext _itemsContext;
            public CommandHandler(ItemsContext itemsContext)
            {
                _itemsContext = itemsContext;
            }

            public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = new Item
                {
                    ItemName = request.ItemName,
                    Quantity = request.Quantity,
                    Price = request.Price
                };

                await _itemsContext.AddAsync(entity, cancellationToken);
                await _itemsContext.SaveChangesAsync();

                return entity.Id;
            }
        }
    }
}
