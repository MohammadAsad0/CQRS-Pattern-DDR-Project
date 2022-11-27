using DDR_PROJECTAPIS.Data;
using DDR_PROJECTAPIS.Models;
using MediatR;
using System.Diagnostics.Eventing.Reader;

namespace DDR_PROJECTAPIS.Features.BorrowedItems.BorrowedItemsCommand
{
    public class AddNewBorrowedItem
    {
        public class Command : IRequest<Guid>
        {
            public Guid ItemId { get; set; }
            public string StudentId { get; set; }
            public int QuantityBorrowed { get; set; }
            public string TimeBorrowed { get; set; }
            public string TimeToBeReturned { get; set; }
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
                var _item = _itemsContext.Items.Where(a => a.Id == request.ItemId).FirstOrDefault();
                if (_item == null)
                {
                    return default;
                }
                else
                {
                    _item.Quantity -= request.QuantityBorrowed;
                    var entity = new BorrowedItem
                    {
                        ItemId = request.ItemId,
                        StudentId = request.StudentId,
                        QuantityBorrowed = request.QuantityBorrowed,
                        TimeBorrowed = request.TimeBorrowed,
                        TimeToBeReturned = request.TimeToBeReturned
                    };

                    await _itemsContext.AddAsync(entity, cancellationToken);
                    await _itemsContext.SaveChangesAsync();

                    return entity.ItemId;
                }
            }
        }
    }
}
