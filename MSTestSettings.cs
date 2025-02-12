using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace PlaywrightTests
{
    [TestClass]
    public class MSTestSettings
    {
        protected static IPlaywright _playwright;
        protected static IBrowser _browser;
        protected static IBrowserContext _context;

        [AssemblyInitialize]
        public static async Task AssemblyInitialize(TestContext context)
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false, 
                SlowMo = 100
            });

            _context = await _browser.NewContextAsync();
        }

        [AssemblyCleanup]
        public static async Task AssemblyCleanup()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }
    }
}
