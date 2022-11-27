using DDR_PROJECTAPIS.Data;
using DDR_PROJECTAPIS.Models;
using MediatR;

namespace DDR_PROJECTAPIS.Features.BorrowedItems.BorrowedItemsCommand
{
    public class UpdateBorrowedItemById
    {
        public class Command : IRequest<Tuple<Guid, string>>
        {
            public Guid ItemId { get; set; }
            public string StudentId { get; set; }
            public int QuantityBorrowed { get; set; }
            public string TimeBorrowed { get; set; }
            public string TimeToBeReturned { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command, Tuple<Guid, string>>
        {
            private readonly ItemsContext _itemscontext;
            public CommandHandler(ItemsContext itemscontext)
            {
                _itemscontext = itemscontext;
            }
            public async Task<Tuple<Guid,string>> Handle(Command command, CancellationToken cancellationToken)
            {
                var existingItem = _itemscontext.BorrowedItems.Where(a => a.ItemId == command.ItemId && a.StudentId == command.StudentId).FirstOrDefault();

                if (existingItem == null)
                {
                    return default;
                }
                else
                {
                    int quantityDifference = command.QuantityBorrowed - existingItem.QuantityBorrowed;
                    var _item = _itemscontext.Items.Where(a => a.Id == command.ItemId).FirstOrDefault();
                    _item.Quantity -= quantityDifference;
                    //if (_item.Quantity >= quantityDifference)
                    //{
                    //    _item.Quantity -= quantityDifference;
                    //}
                    existingItem.QuantityBorrowed = command.QuantityBorrowed;
                    existingItem.TimeBorrowed = command.TimeBorrowed;
                    existingItem.TimeToBeReturned = command.TimeToBeReturned;

                    await _itemscontext.SaveChangesAsync(cancellationToken);

                    return Tuple.Create(existingItem.ItemId, existingItem.StudentId);
                    //return existingItem.ItemId;
                }
            }
        }
    }
}
