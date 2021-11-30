using System;
using System.Threading.Tasks;
using Superheroes.Controllers;
using Superheroes.Models;

namespace Superheroes.Services
{
    public class BattleService : IBattleService
    {
        private readonly ICharactersProvider _charactersProvider;

        public BattleService(ICharactersProvider charactersProvider)
        {
            _charactersProvider = charactersProvider ?? throw new ArgumentNullException(nameof(charactersProvider));
        }

        public async Task<CharacterResponse> Battle(string hero, string villain)
        {
            var battle = new Battle();
            var characters = await _charactersProvider.GetCharacters();
            
            foreach(var character in characters.Items)
            {
                if(character.Name == hero)
                {
                    battle.Hero = character;
                }
                if(character.Name == villain)
                {
                    battle.Villain = character;
                }
            }

            if (battle.Hero.Weakness == battle.Villain.Name)
            {
                battle.Hero.Score = battle.Hero.Score - 1;
            }
            
            if(battle.Hero.Score > battle.Villain.Score)
            {
                return battle.Hero;
            }

            return battle.Villain;
        }
    }
}