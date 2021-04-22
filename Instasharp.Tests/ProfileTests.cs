using System.Threading.Tasks;
using Xunit;
using Instasharp.Exceptions;

namespace Instasharp.Tests
{
    public class ProfileTests
    {
        private InstagramClient _instagramClient = new();

        [Theory]
        [InlineData("hey_its_curtis")] // Private profile
        [InlineData("therock")] // Public profile
        [InlineData("https://www.instagram.com/hey_its_curtis/")] // Url
        public async ValueTask I_can_get_metadata_from_valid_profiles(string usernameOrUrl)
        {
            var profile = await _instagramClient.GetProfileMetadataAsync(usernameOrUrl);

            Assert.NotNull(profile);
        }

        [Theory]
        [InlineData("d99a0sd5sf3sf5sud2as")] // Profile does not exist
        [InlineData("d99a0 d5sf3s 5sud2as")] // Profile does not exist, and contains whitespaces
        [InlineData("https://www.instagram.com/d99a0sd5sf3sf5sud2as/")] // Url to a non-existent profile
        public async ValueTask Non_existent_profiles_throw_exceptions(string usernameOrUrl)
        {
            await Assert.ThrowsAsync<ProfileNotFoundException>(async () =>
            {
                var profile = await _instagramClient.GetProfileMetadataAsync(usernameOrUrl);
            });
        }
    }
}
