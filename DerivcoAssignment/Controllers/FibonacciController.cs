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

        [HttpPost]
        [ProducesResponseType(typeof(FibonacciResponseViewModel), StatusCodes.Status200OK)]
        public ActionResult Generate(FibonacciRequestViewModel request)
        {
            var numbers = fibonacciGenerator.GenerateFibonacci(request.FirstIndex, request.LastIndex);

            var viewModel = new FibonacciResponseViewModel
            {
                FibonacciNumbers = JsonConvert.SerializeObject(numbers, new FibonacciResponseConverter()),
                Status = Core.Enums.GenerationResult.Ok
            };

            return Ok(viewModel);
        }
    }
}
