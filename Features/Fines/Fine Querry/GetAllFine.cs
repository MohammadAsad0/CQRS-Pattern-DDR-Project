using DDR_PROJECTAPIS.Data;
using DDR_PROJECTAPIS.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace DDR_PROJECTAPIS.Features.Fines.Fine_Querry
{
    public class GetAllFine
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

                foreach (var item in _context.Fines)
                {
                    _context.Fines.Remove(item);
                }
                _context.SaveChanges();

                DateTime now = DateTime.Now;
                var dateTime = DateTime.Now;

                var query = from da in _context.BorrowedItems
                            join st in _context.Items
                            on da.ItemId equals st.Id
                            join ab in _context.Students
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
                        await _context.Fines.AddAsync(objfine);



                    }


                }
                await _context.SaveChangesAsync();

                
                    var result = (from s in _context.Items // outer sequence
                                  join st in _context.Fines //inner sequence 
                                  on s.Id equals st.ItemId
                                  join ab in _context.Students
                                  on st.StudentId equals ab.StudentId
                                  // key selector 
                                  select new
                                  { // result selector
                                      ItemId = s.Id,
                                      StudentId = ab.StudentId,
                                      ItemName = s.ItemName,
                                      StudentName = ab.StudentName,
                                      FinedAmount = st.FineAmount,
                                      // TimeBorrowed = st.ReturnedTime
                                  }).ToList();
                    //string json = Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
                    return result;

                

                //                return Ok(innerJoin);
                //string json = Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
                
                
            }

        }
    }
}
