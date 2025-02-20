using Microsoft.Playwright.MSTest;
using Microsoft.Playwright;
using PlaywrightTests.Pages;
using PlaywrightTests.Helpers;

namespace PlaywrightTests.Tests
{
    [TestClass]
    public class CRUDTests : PageTest
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

        [TestMethod]
        public async Task Should_Add_Valid_Todo()
        {
            string todoName = TestDataGenerator.GenerateUniqueTodoName();
            await TodoPage.AddTodo(todoName);
            Assert.IsTrue(await TodoPage.IsTodoVisible(todoName));
        }

        [TestMethod]
        public async Task Should_Complete_Todo()
        {
            string todoName = TestDataGenerator.GenerateUniqueTodoName();
            await TodoPage.AddTodo(todoName);
            await TodoPage.CompleteTodo(todoName);
            Assert.IsTrue(await TodoPage.IsTodoCompleted(todoName));
        }

        [TestMethod]
        public async Task Should_Clear_Completed_Todo() 
        {
            string todoName = TestDataGenerator.GenerateUniqueTodoName();
            await TodoPage.AddTodo(todoName);
            await TodoPage.CompleteTodo(todoName);
            Assert.IsTrue(await TodoPage.IsTodoCompleted(todoName));
            await TodoPage.ClearCompleted();
            Assert.IsTrue(await TodoPage.IsTodoListEmpty());
        }

        [TestMethod]
        public async Task Should_Delete_Todo(){
            string todoName = TestDataGenerator.GenerateUniqueTodoName();
            await TodoPage.AddTodo(todoName);
            await TodoPage.DeleteTodo(todoName);
            Assert.IsTrue(await TodoPage.IsTodoListEmpty());            
        }
    }
}
