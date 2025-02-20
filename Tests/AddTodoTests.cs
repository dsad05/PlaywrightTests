using Microsoft.Playwright;
using PlaywrightTests.Pages;
using PlaywrightTests.Helpers;

namespace PlaywrightTests.Tests
{
    [TestClass]
    public class AddTodoTests
    {
        private IPlaywright? _playwright;
        private IBrowser? _browser;
        private IPage? _page;
        private TodoPage? _todoPage;

        [TestInitialize]
        public async Task TestSetup()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 100
            });

            var context = await _browser.NewContextAsync();
            _page = await context.NewPageAsync();

            _todoPage = new TodoPage(_page);
            await _todoPage.InitializePage();
            Assert.IsTrue(await _todoPage.IsTodoListEmpty());
        }

        public static IEnumerable<object[]> TodoNamesData
        {
            get
            {
                return
                [
                    ["NormalTask"],
                    ["Special@Chars!"],
                    ["<script>alert('XSS')</script>"],
                    ["'; DROP TABLE Todos; --"],
                    ["任务_中文测试"]
                ];
            }
        }

        [TestMethod]
        [DynamicData(nameof(TodoNamesData))]
        public async Task Should_Add_Todo_GeneratedData(string todoName)
        {
            string generatedTodoName = TestDataGenerator.GenerateUniqueTodoName(todoName);
            Console.WriteLine(generatedTodoName);
            await _todoPage!.AddTodo(generatedTodoName);
            Assert.IsTrue(await _todoPage.IsTodoVisible(generatedTodoName));
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            await _browser?.CloseAsync();
            _playwright?.Dispose();
        }
    }
}