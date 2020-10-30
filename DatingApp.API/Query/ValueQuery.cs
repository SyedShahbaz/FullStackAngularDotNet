using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Models;
using DatingApp.API.Query.Abstraction;
using DatingApp.API.Query.Model;
using DatingApp.API.Query.QueryHandler;

namespace DatingApp.API.Query
{
    public class ValueQuery : IQuery<IReadOnlyCollection<Values>>
    {
        public int Id { get; set; }

        public ValueQuery(int id)
        {
           Id = id;
        }
    }
}
