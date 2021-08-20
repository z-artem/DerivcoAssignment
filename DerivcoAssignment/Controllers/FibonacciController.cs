using DerivcoAssignment.Core;
using DerivcoAssignment.Web.Helpers;
using DerivcoAssignment.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        [ProducesResponseType(typeof(FibonacciResultViewModel), StatusCodes.Status200OK)]
        public ActionResult Get(uint firstIndex, uint lastIndex, bool useCache, uint timeLimit, uint memoryLimit)
        {
            var numbers = fibonacciGenerator.GenerateFibonacci(firstIndex, lastIndex);

            var viewModel = new FibonacciResultViewModel
            {
                FibonacciNumbers = JsonConvert.SerializeObject(numbers, new FibonacciResultConverter()),
                Status = Core.Enums.GenerationResult.Ok
            };

            return Ok(viewModel);
        }
    }
}
