using System.Threading.Tasks;

namespace Superheroes.Services
{
    public interface IBattleService
    {
        public Task<CharacterResponse> Battle(string hero, string villain);
    }
}