using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Mapper.Abstraction;
using DatingApp.API.Models;

namespace DatingApp.API.Mapper.Implementation
{
    public class TestMapper : IMapper<Values, ValuesForUser>
    {
        public ValuesForUser MapFrom(Values input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            return new ValuesForUser
            {
                Id = input.Id,
                Name = input.Name,
                Description = DateTime.Now.ToString()
            };

        }
    }
}
