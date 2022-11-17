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


namespace DDR_PROJECTAPIS.Features.Items.ItemsCommand
{
    public class DeleteItem
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
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


                var item = await _itemsContext.Items.FindAsync(request.Id);
                if (item == null)
                {
                    return Unit.Value;
                }
                _itemsContext.Remove(item);
                await _itemsContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
