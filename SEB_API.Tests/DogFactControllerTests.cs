using Microsoft.Extensions.Caching.Memory;
using SEB_API;
using SEB_API.Controllers;
using System.Threading.Tasks;
using FakeItEasy;
using Xunit;

namespace SEB_API_TEST
{
    public class DogFactControllerTests
    {
        [Fact]
        public async Task GetDogFact_Returns_A_Dog_Fact_When_FactNumber_Is_Provided()
        {
            //Setup
            var factNumber = 1;
            var cache = A.Fake<IMemoryCache>();

            var fakeDogFact = A.Fake<DogFact>();
            fakeDogFact.fact = "";

            var httpDogFactFactoryService = A.Fake<IHttpDogFactFactoryService>();
            A.CallTo(() => httpDogFactFactoryService.ExecuteAsync(factNumber)).Returns(Task.FromResult(fakeDogFact));
            var controller = new DogFactController(cache, httpDogFactFactoryService);

            //Act
            var actionResult = await controller.GetDogFact();

            //Assert
            var value = actionResult.Value;
            Assert.NotNull(value.fact);
        }
    }
}