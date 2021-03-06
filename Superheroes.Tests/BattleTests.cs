using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Threading.Tasks;
using FluentAssertions;
using System.Net;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Superheroes.Controllers;
using Superheroes.Services;

namespace Superheroes.Tests
{
    public class BattleTests
    {
        [Test]
        public async Task CanGetHeros()
        {
            var charactersProvider = new FakeCharactersProvider();

            var startup = new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureServices(x => { x.AddSingleton<ICharactersProvider>(charactersProvider); });
            var testServer = new TestServer(startup);
            var client = testServer.CreateClient();

            charactersProvider.FakeResponse(new CharactersResponse
            {
                Items = new[]
                {
                    new CharacterResponse
                    {
                        Name = "Batman",
                        Score = 8.3,
                        Type = "hero"
                    },
                    new CharacterResponse
                    {
                        Name = "Joker",
                        Score = 8.2,
                        Type = "villain"
                    }
                }
            });

            var response = await client.GetAsync("battle?hero=Batman&villain=Joker");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseJson = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<JObject>(responseJson);

            responseObject.Value<string>("name").Should().Be("Batman");
        }

        [Test]
        public async Task ShouldReturnJokerWhenBatmanFightsJoker()
        {
            var fakeBattleService = A.Fake<IBattleService>();

            var batman = new CharacterResponse()
            {
                Name = "Batman",
                Score = 8.6,
                Type = "hero",
                Weakness = "Joker"
            };

            var joker = new CharacterResponse()
            {
                Name = "Joker",
                Score = 8.5,
                Type = "villain"
            };

            A.CallTo(() => fakeBattleService.Battle("Batman", "Joker"))
                .Returns(joker);

            var sut = new BattleController(fakeBattleService);

            var result = await sut.Get("Batman", "Joker");

            result.Should()
                .BeOfType<OkObjectResult>()
                .Which
                .Value
                .Should()
                .Be(joker);
        }
    }
}
    
