using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Mapper.Abstraction;
using DatingApp.API.Models;
using DatingApp.API.Query;
using DatingApp.API.Query.QueryHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DatingApp.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IQueryHandler<ValueQuery, IReadOnlyCollection<Values>> _queryHandler;
        private readonly IMapper<Values, ValuesForUser> _mapper;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger, 
            IQueryHandler<ValueQuery, IReadOnlyCollection<Values>> queryHandler, 
            IMapper<Values, ValuesForUser> mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _queryHandler = queryHandler ?? throw new ArgumentNullException(nameof(queryHandler));
            _mapper = mapper;
        }


        // Use Async code so that tread is not blocked..
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Calling without Id");
            var val =  _queryHandler.Handle(new ValueQuery(1));
            return Ok(val.Select(x => _mapper.MapFrom(x)));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _logger.LogInformation($"Calling with Id: {id}");
            var val = _queryHandler.Handle(new ValueQuery(id));
            return Ok(val.Select(x => _mapper.MapFrom(x)));
        }
    }
}
