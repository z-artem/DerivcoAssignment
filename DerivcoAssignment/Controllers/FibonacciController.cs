using DerivcoAssignment.Core;
using DerivcoAssignment.Web.Helpers;
using DerivcoAssignment.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace DerivcoAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FibonacciController : ControllerBase
    {
        private readonly IFibonacciGenerator _fibonacciGenerator;

        public FibonacciController(IFibonacciGenerator fibonacciGenerator)
        {
            _fibonacciGenerator = fibonacciGenerator ?? throw new ArgumentNullException(nameof(fibonacciGenerator));
        }

        [HttpPost]
        [ProducesResponseType(typeof(FibonacciResponseViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> GenerateAsync(FibonacciRequestViewModel request)
        {
            var numbers = await _fibonacciGenerator.GenerateFibonacci(request.FirstIndex, request.LastIndex, request.TimeLimit, request.MemoryLimit);

            var viewModel = new FibonacciResponseViewModel
            {
                FibonacciNumbers = JsonConvert.SerializeObject(numbers, new FibonacciResponseConverter()),
                Status = Core.Enums.GenerationResult.Ok
            };

            return Ok(viewModel);
        }
    }
}
