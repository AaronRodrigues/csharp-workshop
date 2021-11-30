using System;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Superheroes.Controllers;

namespace Superheroes.Tests
{
    public class BattleServiceTests
    {
        [Test]
        public void ShouldExistAndNotThrow()
        {
            Action constructor = () => new BattleService(A.Fake<ICharactersProvider>());
            
            constructor.Should().NotThrow();
        }

        [Test]
        public void ShouldThrowIfCharacterProviderIsNull()
        {
            Action constructor = () => new BattleService(null);

            constructor.Should()
                .Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'charactersProvider')");
        }
        
        [Test]
        public async Task ShouldReturnJokerWhenBatmanFightsJoker()
        {
            //Arrange
            var fakeCharacterProvider = A.Fake<ICharactersProvider>();

            var batman = new CharacterResponse()
            {
                Name = "Batman",
                Score = 8.3,
                Type = "hero",
                Weakness = "Joker"
            };

            var joker = new CharacterResponse()
            {
                Name = "Joker",
                Score = 8.2,
                Type = "villain"
            };

            A.CallTo(() => fakeCharacterProvider.GetCharacters()).Returns(new CharactersResponse(){Items = new []{batman, joker}})
                ;

            var sut = new BattleService(fakeCharacterProvider);

            //Act
            var result = await sut.Battle("Batman", "Joker");
            
            //Assert
            result
                .Should()
                .Be(joker);
        }
    }
}