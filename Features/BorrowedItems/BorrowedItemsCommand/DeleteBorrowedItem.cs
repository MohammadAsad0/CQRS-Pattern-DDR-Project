using DDR_PROJECTAPIS.Data;
using MediatR;

//using FluentValidation;
//using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
//using WebApiCqrs.Data;

namespace DDR_PROJECTAPIS.Features.BorrowedItems.BorrowedItemsCommand
{
    public class DeleteBorrowedItem
    {
        public class Command : IRequest
        {
            public Guid ItemId { get; set; }
            public string StudentId { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command, Unit>
        {
            private readonly ItemsContext _itemsContext;

            public CommandHandler(ItemsContext itemsContext)
            {
                _itemsContext = itemsContext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var borrowedItem = _itemsContext.BorrowedItems.Where(a => a.ItemId == request.ItemId && a.StudentId == request.StudentId).FirstOrDefault();
                if (borrowedItem == null)
                {
                    return Unit.Value;
                }
                var item = _itemsContext.Items.Where(a => a.Id == request.ItemId).FirstOrDefault();
                item.Quantity += borrowedItem.QuantityBorrowed;

                _itemsContext.Remove(borrowedItem);

                await _itemsContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
