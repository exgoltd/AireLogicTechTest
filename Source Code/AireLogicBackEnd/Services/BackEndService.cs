using System.Collections.Generic;
using System.Linq;

namespace AireLogicBackEnd
{
    public class BackEndService
    {
        public double GetAverageLyricCount(string artist)
        {
            var lyricService = new LyricsService();
            var artistService = new ArtistService();

            List<Recordings> recordings = artistService.GetRecordings(artist);

            List<int> lyricNumbers = new List<int>();

            foreach (var recording in recordings)
            {
                var numberOfLyrics = lyricService.GetNumberOfLyrics(artist, recording.Title);

                if (numberOfLyrics != 0)
                    lyricNumbers.Add(numberOfLyrics);
            }

            double average = lyricNumbers.Count > 0 ? lyricNumbers.Average() : double.NaN;

            return average;
        }
    }
}
