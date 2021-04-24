using System.Threading.Tasks;
using Xunit;
using Instasharp.Search;

namespace Instasharp.Tests
{
    public class QueryTests
    {
        private readonly InstagramClient _instagramClient = new();

        [Fact]
        public async Task I_can_search_for_profiles_and_return_a_list()
        {
            var query = "hey_its_curtis";
            var results = await _instagramClient.GetProfilesAsync(query).CollateAsync();

            Assert.True(results.Count >= 1);
        }
    }
}
