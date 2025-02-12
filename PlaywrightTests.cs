using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace PlaywrightTests
{
    [TestClass]
    public class PlaywrightTest
    {
        private static IPlaywright _playwright;
        private static IBrowser _browser;

        [ClassInitialize]
        public static async Task Setup(TestContext context)
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions 
            { 
                Headless = false
            });
        }

        [TestMethod]
        public async Task TestGoogleSearch()
        {
            var page = await _browser.NewPageAsync();
            await page.GotoAsync("https://www.google.com");
            var title = await page.TitleAsync();
            Assert.IsTrue(title.Contains("Google"));
        }

        [ClassCleanup]
        public static async Task Cleanup()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }
    }
}
