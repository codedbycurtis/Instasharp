using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Instasharp.Search;
using Instasharp.Profiles;

namespace Instasharp.Demo
{
    class Program
    {
        private const string _sessionId = ""; // Insert your own here if necessary
        private static readonly InstagramClient _instagramClient = new(_sessionId);

        static void Main()
        {
            Console.Title = "Instasharp Demo";
            MainMenu();
        }

        static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Instasharp Demo");
            Console.WriteLine("---------------");
            Console.WriteLine("[1] Get Profile Metadata");
            Console.WriteLine("[2] Search For Profiles\n");
            Console.Write("Selection: ");

            if (!int.TryParse(Console.ReadLine(), out int selection))
            {
                MainMenu();
            }

            switch (selection)
            {
                case 1:
                    ProfileInterface();
                    break;

                case 2:
                    SearchInterface();
                    break;

                default:
                    MainMenu();
                    break;
            }
        }

        static void ProfileInterface()
        {
            Console.Clear();
            Console.Write("Enter a valid username to retrieve the account's metadata: ");
            var searchQuery = Console.ReadLine();

            if (string.IsNullOrEmpty(searchQuery))
            {
                ProfileInterface();
            }

            Task.Run(async () =>
            {
                var profile = await GetProfileMetadataAsync(searchQuery!);
                Console.WriteLine($"Showing information for {profile.Handle}'s profile...");
                Console.WriteLine("-----------------------------------------------------");
                Console.WriteLine(profile.ToString());
            });
        }

        static void SearchInterface()
        {
            Console.Clear();
            Console.Write("Enter a valid username to search for: ");
            var searchQuery = Console.ReadLine();

            Console.Write("Search result limit (0 for none): ");
            if (!int.TryParse(Console.ReadLine(), out int count))
            {
                SearchInterface();
            }

            if (string.IsNullOrEmpty(searchQuery))
            {
                SearchInterface();
            }

            Console.WriteLine($"Showing results for {searchQuery}...");
            Console.WriteLine("------------------------------------");

            Task.Run(async () =>
            {
                IReadOnlyList<ProfileSearchResult> results;

                if (count == 0)
                {
                    results = await SearchForProfilesAsync(searchQuery!);
                }
                else
                {
                    results = await SearchForProfilesAsync(searchQuery!, count);
                }

                foreach (var result in results)
                {
                    Console.WriteLine($"{result}\n");
                }
            });

            Console.ReadLine();
        }

        static async ValueTask<IReadOnlyList<ProfileSearchResult>> SearchForProfilesAsync(string searchQuery)
            => await _instagramClient.SearchForProfilesAsync(searchQuery).CollectAsync();

        static async ValueTask<IReadOnlyList<ProfileSearchResult>> SearchForProfilesAsync(string searchQuery, int count)
            => await _instagramClient.SearchForProfilesAsync(searchQuery).CollectAsync(count);

        static async ValueTask<Profile> GetProfileMetadataAsync(string searchQuery)
            => await _instagramClient.GetProfileMetadataAsync(searchQuery);
    }
}
