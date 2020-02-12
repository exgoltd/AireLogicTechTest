using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AireLogicBackEnd
{
    public class ArtistService
    {
        private static readonly string artistUrl = "https://musicbrainz.org/ws/2/artist/?query=";
        private static readonly string songUrl = "https://musicbrainz.org/ws/2/artist/";
        private static readonly HttpClient httpClient = new HttpClient();


        public List<Recordings> GetRecordings(string artist)
        {
            string artistId = GetArtistId(artist);

            Task<string> songsTask = Task.Run(async () => await SearchSongsInApi(artistId));

            var root = JObject.Parse(songsTask.Result);
            return root["recordings"].ToObject<List<Recordings>>();
        }

        public string GetArtistId(string artist)
        {
            

            Task<string> artistTask = Task.Run(async () => await SearchArtistInApi(artist));

            dynamic json = JsonConvert.DeserializeObject(artistTask.Result);
            return json["artists"][0]["id"];

        }

        private async Task<string> SearchArtistInApi(string artist)
        {
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Tech-Test");
            var response = await httpClient.GetAsync(artistUrl + artist + "&fmt=json");

            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> SearchSongsInApi(string artistId)
        {
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Tech-Test");
            var response = await httpClient.GetAsync(songUrl + artistId + "?inc=recordings&fmt=json");

            return await response.Content.ReadAsStringAsync();
        }
    }
}
