using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Mapper.Abstraction
{
    public interface IMapper<in T, out T0>
    {
        T0 MapFrom(T input);
    }
}
