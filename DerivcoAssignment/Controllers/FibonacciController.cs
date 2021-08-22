using AutoMapper;
using DerivcoAssignment.Core;
using DerivcoAssignment.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DerivcoAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FibonacciController : ControllerBase
    {
        private readonly IFibonacciGenerator _fibonacciGenerator;
        private readonly IMapper _mapper;

        public FibonacciController(IFibonacciGenerator fibonacciGenerator, IMapper mapper)
        {
            _fibonacciGenerator = fibonacciGenerator ?? throw new ArgumentNullException(nameof(fibonacciGenerator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        [ProducesResponseType(typeof(FibonacciResponseViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> GenerateAsync(FibonacciRequestViewModel request)
        {
            var numbers = await _fibonacciGenerator.GenerateFibonacci(request.FirstIndex, request.LastIndex, request.UseCache, request.TimeLimit, request.MemoryLimit);

            var viewModel = _mapper.Map<FibonacciResponseViewModel>(numbers);

            return Ok(viewModel);
        }
    }
}
