using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace Superheroes.Tests
{
    public class HerosTests
    {
        [Test]
        public void CanGetHeros()
        {
            var startup = new WebHostBuilder()
                            .UseStartup<Startup>();
            var testServer = new TestServer(startup);
            var client = testServer.CreateClient();


        }
    }
}
