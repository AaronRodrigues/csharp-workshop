using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Superheroes.Services;

namespace Superheroes.Controllers
{
    [Route("battle")]
    public class BattleController : Controller
    {
        private readonly IBattleService _battleService;

        public BattleController(IBattleService battleService)
        {
            _battleService = battleService;
        }

        public async Task<IActionResult> Get(string hero, string villain)
        {
            // call BattleService 
            // return Ok(winner)
            var winner = await _battleService.Battle(hero, villain);
            return Ok(winner);
        }
    }
}