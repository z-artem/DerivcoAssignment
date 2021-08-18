using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.Json;

namespace DerivcoAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FibonacciController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get(uint firstIndex, uint lastIndex, bool useCache, uint timeLimit, uint memoryLimit)
        {
            List<BigInteger> fibNumbers = new List<BigInteger>();
            BigInteger currentNumber = 1;
            BigInteger previousNumber = 0;

            for (int i = 0; i < lastIndex; i++)
            {
                BigInteger temp = currentNumber;
                currentNumber += previousNumber;
                previousNumber = temp;

                if (i >= firstIndex)
                {
                    fibNumbers.Add(currentNumber);
                }
            }

            var jsonResult = JsonSerializer.Serialize(fibNumbers.Select(x => x.ToString()));
            return Ok(jsonResult);
        }
    }
}
