using Microsoft.Playwright.MSTest;
using Microsoft.Playwright;
using PlaywrightTests.Pages;
using PlaywrightTests.Helpers;
using System.Web;




namespace PlaywrightTests.AddTodoTests
{
    [TestClass]
    public class AddTodoTests : PageTest
    {
        private TodoPage? _todoPage;
        private TodoPage TodoPage => _todoPage ?? throw new InvalidOperationException("TestSetup() was not run!");

        [TestInitialize]
        public async Task TestSetup()
        {
            _todoPage = new TodoPage(Page);
            await _todoPage.InitializePage();
            Assert.IsTrue(await TodoPage.IsTodoListEmpty());
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
            await TodoPage.AddTodo(todoName);
            
            string todoListHtml = await TodoPage.TodoList.InnerHTMLAsync();
            Console.WriteLine($"Todo List HTML: {todoListHtml}");

            Assert.IsTrue(await TodoPage.IsTodoVisible(todoName));

            Console.WriteLine(generatedTodoName);
        }
    }
}