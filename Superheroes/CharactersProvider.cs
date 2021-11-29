using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Superheroes
{
    internal class CharactersProvider : ICharactersProvider
    {
        private const string CharactersUri = "https://jsonkeeper.com/b/X95Y";
        readonly HttpClient _client = new HttpClient();
        

        public async Task<CharactersResponse> GetCharacters()
        {
            var response = await _client.GetAsync(CharactersUri);

            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CharactersResponse>(responseJson);
        }
    }
}