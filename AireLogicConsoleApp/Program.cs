using AireLogicBackEnd;
using System;

namespace AireLogicConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new BackEndService();

            while (true)
            {
                Console.WriteLine("Please select an option with the corresponding key below:");
                Console.WriteLine("1 - Get a single artists average lyric count");
                Console.WriteLine("2 - Compare two artists average lyric counts");
                var option = Console.ReadLine();

                switch(option)
                {
                    case "1":
                        GetSingleArtist(service);
                        break;

                    case "2":
                        CompareArtists(service);
                        break;

                    default:
                        Console.WriteLine("Option not recognised");
                        break;
                }


                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        public static void GetSingleArtist(BackEndService service)
        {
            Console.WriteLine("Enter Artist name:");
            string artist = Console.ReadLine();

            Console.WriteLine("Average lyrics length:");
            double average = service.GetAverageLyricCount(artist);

            if (double.IsNaN(average))
                Console.WriteLine("No lyrics found for artist");
            else
                Console.WriteLine(average);
        }

        public static void CompareArtists(BackEndService service)
        {
            Console.WriteLine("Enter First Artists name:");
            string artist1 = Console.ReadLine();

            Console.WriteLine("Enter Second Artists name:");
            string artist2 = Console.ReadLine();

            double artist1Average = service.GetAverageLyricCount(artist1);
            if (double.IsNaN(artist1Average))
                artist1Average = 0;

            double artist2Average = service.GetAverageLyricCount(artist2);
            if (double.IsNaN(artist2Average))
                artist2Average = 0;

            double highest = Math.Max(artist1Average, artist2Average);
            double lowest = Math.Min(artist1Average, artist2Average);

            double difference = highest - lowest;

            Console.WriteLine($"{artist1} has an average lyric count of {artist1Average}");
            Console.WriteLine($"{artist2} has an average lyric count of {artist2Average}");
            Console.WriteLine($"With a difference of {difference} words.");
        }
    }
}
