using DDR_PROJECTAPIS.Data;
using DDR_PROJECTAPIS.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DDR_PROJECTAPIS.Features.BorrowedItems.BorrowedItemsQuery
{
    public class GetBorrowedItems
    {
        public class Query : IRequest<IEnumerable<object>>
        { }

        public class QueryHandler : IRequestHandler<Query, IEnumerable<object>>
        {
            private readonly ItemsContext _context;
            public QueryHandler(ItemsContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<object>> Handle(Query request, CancellationToken cancellationToken)
            {
                var innerJoin = from s in _context.Items
                                join st in _context.BorrowedItems
                                on s.Id equals st.ItemId
                                join ab in _context.Students
                                on st.StudentId equals ab.StudentId
                                select new
                                {
                                    ItemId = s.Id,
                                    StudentId = ab.StudentId,
                                    ItemName = s.ItemName,
                                    StudentName = ab.StudentName,
                                    QuantityBorrowed = st.QuantityBorrowed,
                                    TimeBorrowed = st.TimeBorrowed,
                                    Timetobereturned = st.TimeToBeReturned

                                };
                return innerJoin;

                //List<object> BorrowedItems = (List<object>)(await _context.BorrowedItems.ToListAsync(cancellationToken)).Cast<object>();
                //BorrowedItems.Concat();
                ////return await _context.BorrowedItems.ToListAsync(cancellationToken);
                //return BorrowedItems;
            }

        }
    }
}
