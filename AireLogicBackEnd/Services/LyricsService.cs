using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AireLogicBackEnd
{
    public class LyricsService
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly string url = "https://api.lyrics.ovh/v1/";

        public string GetLyrics(string artist, string title)
        {
            Task<string> task = Task.Run(async () => await GetLyricsFromApi(artist, title));

            dynamic json = JsonConvert.DeserializeObject(task.Result);
            return json["lyrics"];
        }

        public int GetNumberOfLyrics(string artist, string title)
        {
            string lyrics = GetLyrics(artist, title);

            if (lyrics == null)
                return 0;

            return lyrics.Split(' ').Length;
        }

        private async Task<string> GetLyricsFromApi(string artist, string title)
        {
            // Api won't take forward slashes, even encoded. Seems to work without them.
            artist = artist.Replace("/", " ");
            title = title.Replace("/", " ");

            string artistEnc = HttpUtility.UrlEncode(artist);
            string titleEnc = HttpUtility.UrlEncode(title);

            var response = await httpClient.GetAsync(url + artistEnc + '/' + titleEnc);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
