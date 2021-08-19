using DerivcoAssignment.Core;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DerivcoAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FibonacciController : ControllerBase
    {
        private readonly IFibonacciGenerator fibonacciGenerator;

        public FibonacciController(IFibonacciGenerator fibonacciGenerator)
        {
            this.fibonacciGenerator = fibonacciGenerator ?? throw new ArgumentNullException(nameof(fibonacciGenerator));
        }

        [HttpGet]
        public ActionResult Get(uint firstIndex, uint lastIndex, bool useCache, uint timeLimit, uint memoryLimit)
        {
            var jsonResult = fibonacciGenerator.GenerateFibonacci(firstIndex, lastIndex);

            return Ok(jsonResult);
        }
    }
}
