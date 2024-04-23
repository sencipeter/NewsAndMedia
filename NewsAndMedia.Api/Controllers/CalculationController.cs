﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NewsAndMedia.Core.Interfaces;
using NewsAndMedia.Model.Request;
using NewsAndMedia.Model.Response;

namespace NewsAndMedia.Api.Controllers
{
    [Route("calculation")]
    [ApiController]
    public class CalculationController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ICalculationService _calculationService;
        public CalculationController(ICalculationService calculationService,
            IMemoryCache memoryCache) 
        {
            _calculationService = calculationService;
            _memoryCache = memoryCache;
        }

        [HttpPost("{key}")]
        public IActionResult Get([FromRoute]int? key, [FromBody] CalculationRequest? request)
        {
            try
            {
                if (!key.HasValue)
                    throw new ArgumentNullException(nameof(key));
                if (request is null)
                    throw new ArgumentNullException(nameof(request));                
                                
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(15));

                if (!_memoryCache.TryGetValue(key, out decimal cacheValue))
                {
                    cacheValue = 2;                    
                }
                
                var computedValue = _calculationService.Calculate(request.Input.Value/cacheValue);
                
                _memoryCache.Set(key, computedValue, cacheEntryOptions);

                var response = new CalculationResponse 
                {
                    ComputedValue = computedValue,
                    InputValue = request.Input.Value,
                    PreviousValue = cacheValue
                };
                return Ok(response);
            }
            catch(ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                #if DEBUG
                return BadRequest(ex.Message);
                #endif

                return BadRequest("Internal Error");
            }            
        }
    }
}
