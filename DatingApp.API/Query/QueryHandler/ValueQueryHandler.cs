using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Models;
using DatingApp.API.Query.Model;

namespace DatingApp.API.Query.QueryHandler
{
    public class ValueQueryHandler : IQueryHandler<ValueQuery, IReadOnlyCollection<Values>>
    {
        private readonly DataContext _context;

        public ValueQueryHandler(DataContext context)
        {
            _context = context;
        }

        public IReadOnlyCollection<Values> Handle(ValueQuery query)
        {
            return _context.Values?.Where(x => x.Id > query.Id).ToList();
        }
    }
}
