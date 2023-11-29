
using LifxLibrary;
using System.IO;

namespace UnitTestLifxLibrary
{
    [TestClass]
    public class UnitTest1
    {
        public string? lifxToken = Environment.GetEnvironmentVariable("LIFXKEY");

        
        [TestMethod]
        public void TestMethodToggle()
        {
            LifxBulbs bulb = new LifxBulbs(lifxToken, "Bedroom");

            void ToggleAction ()=> bulb.LightToggle(101);

            Assert.ThrowsException<ArgumentOutOfRangeException>(ToggleAction);
        }


        [TestMethod]
        public async Task TestMethodToggleAsync()
        {
            LifxBulbs bulb = new LifxBulbs(lifxToken, "Bedroom");
            
            async Task ToggleActionAsync ()=> await bulb.LightToggleAsync(-4);

            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(ToggleActionAsync);
        }


        [TestMethod]
        public async Task TestMethodSweepToggle()
        {

            LifxBulbs bulb = new LifxBulbs(lifxToken, "Bedroom");

            void SweepToggleAction() => bulb.SweepToggle(3);

            Assert.ThrowsException<ArgumentOutOfRangeException>(SweepToggleAction);
        }
    }
}