using DDR_PROJECTAPIS.Data;
using DDR_PROJECTAPIS.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace DDR_PROJECTAPIS.Features.BorrowedItems.BorrowedItemsQuery
{
    public class GetBorrowedItembyStudentId
    {
        public class Query : IRequest<IEnumerable<object>>
        {
            public string StudentId { get; set; }
        }
        public class QueryHandler : IRequestHandler<Query, IEnumerable<object>>
        {
            private readonly ItemsContext _itemsContext;

            public QueryHandler(ItemsContext itemsContext)
            {
                _itemsContext = itemsContext;
            }

            public async Task<IEnumerable<object>> Handle(Query request, CancellationToken cancellation)
            {
                var innerJoin = from s in _itemsContext.Items
                                join st in _itemsContext.BorrowedItems
                                on s.Id equals st.ItemId
                                join ab in _itemsContext.Students
                                on st.StudentId equals ab.StudentId
                                where st.StudentId == request.StudentId
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
                //return (await _itemsContext.BorrowedItems.ToListAsync(cancellation)).Where(a => a.StudentId == request.StudentId);
            }
        }


    }
}