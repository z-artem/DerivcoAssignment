using AutoMapper;
using DerivcoAssignment.Controllers;
using DerivcoAssignment.Core;
using DerivcoAssignment.Core.Dtos;
using DerivcoAssignment.Tests.Common;
using DerivcoAssignment.Tests.Common.Helpers;
using DerivcoAssignment.Web.Mapping;
using DerivcoAssignment.Web.ViewModels;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using Xunit;

namespace DerivcoAssignment.Web.Tests
{
    public class FibonacciControllerTest : TestBase<FibonacciController>
    {
        private readonly IMapper _mapper;
        private readonly IFibonacciGenerator _fibonacciGenerator;

        public FibonacciControllerTest()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GeneratorProfile>();
            });

            _mapper = new Mapper(mapperConfig);
            _fibonacciGenerator = Substitute.For<IFibonacciGenerator>();

            TestTarget = new FibonacciController(_fibonacciGenerator, _mapper);
        }

        [Fact]
        public override void Constructor_ArgumentIsNull_Throws()
        {
            // act & assert
            AssertHelper.ThrowsArgumentNullException(Tuple.Create(_fibonacciGenerator, _mapper), (t) => new FibonacciController(t.Item1, t.Item2));
        }

        [Fact]
        public async Task GenerateAsync_ArgumentsAreOk_CallsGenerator()
        {
            // arrange
            var request = new FibonacciRequestViewModel
            {
                FirstIndex = 101,
                LastIndex = 201,
                MemoryLimit = 301,
                TimeLimit = 401,
                UseCache = true
            };

            var expectedResult = new FibonacciResultDto
            {
                Status = Core.Enums.GenerationResult.Ok,
                FibonacciNumbers = new List<BigInteger> { 901, 902, 903, 904, 905 }
            };

            _fibonacciGenerator.GenerateFibonacci(0, 0, false, 0, 0).ReturnsForAnyArgs(expectedResult);

            // act
            var actualResult = await TestTarget.GenerateAsync(request);

            // assert
            await _fibonacciGenerator.Received(1).GenerateFibonacci(request.FirstIndex, request.LastIndex, request.UseCache, request.TimeLimit, request.MemoryLimit);

            actualResult.Should().NotBeNull();
            actualResult.Value.Should().BeOfType(typeof(FibonacciResponseViewModel));

            var response = actualResult.Value as FibonacciResponseViewModel;
            response.Status.Should().Be(expectedResult.Status);
            response.FibonacciNumbers.Should().Be("[901,902,903,904,905]");
        }
    }
}
