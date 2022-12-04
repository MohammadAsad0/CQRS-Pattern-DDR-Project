using DDR_PROJECTAPIS.Data;
using DDR_PROJECTAPIS.Models;
using MediatR;

namespace DDR_PROJECTAPIS.Features.Fines.Fine_Querry
{
    public class GetFineByStudentId
    {
        public class Query : IRequest<object>
        {
            public string StudentId { get; set; }
        }
        public class QueryHandler : IRequestHandler<Query, object>
        {
            private readonly ItemsContext _itemsContext;

            public QueryHandler(ItemsContext itemsContext)
            {
                _itemsContext = itemsContext;
            }

            public async Task<object> Handle(Query request, CancellationToken cancellation)
            {
                foreach (var item in _itemsContext.Fines)
                {
                    _itemsContext.Fines.Remove(item);
                }
                _itemsContext.SaveChanges();

                DateTime now = DateTime.Now;
                var dateTime = DateTime.Now;

                var query = from da in _itemsContext.BorrowedItems
                            join st in _itemsContext.Items
                            on da.ItemId equals st.Id
                            join ab in _itemsContext.Students
                            on da.StudentId equals ab.StudentId

                            select new
                            {
                                FinedAmount = (st.Price * da.QuantityBorrowed) / 10,
                                ItemName = st.ItemName,
                                StudentId = ab.StudentId,
                                ItemId = st.Id,
                                StudentName = ab.StudentName,
                                Condition = DateTime.Compare(Convert.ToDateTime(da.TimeToBeReturned), DateTime.Now) < 0


                            };
                List<Fine> listobj = new List<Fine>();
                string zyx = "";
                foreach (var abc in query)
                {
                    if (abc.Condition == true)
                    {




                        Fine objfine = new Fine();
                        objfine.ItemId = abc.ItemId;
                        objfine.StudentId = abc.StudentId;
                        objfine.FineAmount = abc.FinedAmount;
                        objfine.ReturnedTime = "";
                        listobj.Add(objfine);
                        await _itemsContext.Fines.AddAsync(objfine);



                    }


                }
                await _itemsContext.SaveChangesAsync();

                var innerJoin = from s in _itemsContext.Items // outer sequence
                                join st in _itemsContext.Fines //inner sequence 
                                on s.Id equals st.ItemId
                                join ab in _itemsContext.Students
                                on st.StudentId equals ab.StudentId
                                where st.StudentId == ab.StudentId
                                // key selector 
                                select new
                                { // result selector
                                    ItemId = s.Id,
                                    StudentId = ab.StudentId,
                                    ItemName = s.ItemName,
                                    StudentName = ab.StudentName,
                                    FinedAmount = st.FineAmount,
                                    TimeBorrowed = st.ReturnedTime
                                };

                return innerJoin;


                //return await _itemsContext.Fines.FindAsync(request.StudentId);
            }
        }
    }
}
