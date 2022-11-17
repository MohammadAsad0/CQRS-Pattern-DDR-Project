using DDR_PROJECTAPIS.Data;
using DDR_PROJECTAPIS.Models;
using MediatR;

namespace DDR_PROJECTAPIS.Features.Items.ItemsCommand
{
    public class UpdateItemById
    {

        public class Command : IRequest<Guid>
        {
            public Guid Id { get; set; }
            public string ItemName { get; set; }

            public int Quantity { get; set; }

            public int Price { get; set; }


        }

        public class CommandHandler : IRequestHandler<Command, Guid>
        {
            private readonly ItemsContext _itemscontext;
            public CommandHandler(ItemsContext itemscontext)
            {
                _itemscontext = itemscontext;
            }
            public async Task<Guid> Handle(Command command, CancellationToken cancellationToken)
            {
                var existingItem = _itemscontext.Items.Where(a => a.Id == command.Id).FirstOrDefault();

                if (existingItem == null)
                {
                    return default;
                }
                else
                {
                    existingItem.ItemName = command.ItemName;
                    existingItem.Price = command.Price;
                    existingItem.Quantity = command.Quantity;

                    await _itemscontext.SaveChangesAsync(cancellationToken);

                    return existingItem.Id;
                }
            }
        }
    }
}
