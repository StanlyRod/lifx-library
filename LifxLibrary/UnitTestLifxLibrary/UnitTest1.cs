
using LifxLibrary;
using System.IO;

namespace UnitTestLifxLibrary
{
    [TestClass]
    public class UnitTest1
    {
        public string? lifxToken = Environment.GetEnvironmentVariable("LIFXKEY");


        [TestMethod]
        public async Task TestMethodToggleAsync()
        {
            LifxBulbs bulb = new LifxBulbs(lifxToken, "Bedroom");
            
            async Task ToggleActionAsync ()=> await bulb.LightToggleAsync(-4);

            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(ToggleActionAsync);
        }

    }
}