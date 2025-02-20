using Microsoft.Playwright;

namespace PlaywrightTests.Pages
{
    public class TodoPage
    {
        private const string BASE_URL = "https://todomvc.com/examples/react/dist/";
        private readonly IPage _page;
        private readonly ILocator _input;
        private readonly ILocator _todoList;

        public TodoPage(IPage page)
        {
            _page = page;
            _input = _page.GetByTestId("text-input");
            _todoList = _page.GetByTestId("todo-list");
        }

        public static string Url => BASE_URL;
        public ILocator Input => _input;
        public ILocator TodoList => _todoList;

        public async Task InitializePage()
        {
            await _page.GotoAsync(BASE_URL);
        }

        public async Task AddTodo(string task)
        {
            await _input.FillAsync(task);
            await _input.PressAsync("Enter");
        }

        public async Task<bool> IsTodoVisible(string task)
        {
            var items = await _todoList.InnerTextAsync();
            return items.Contains(task);
        }

        public async Task CompleteTodo(string task)
        {

            var todoToCheck = _todoList.GetByTestId("todo-item").Filter(new() { HasText = task });
            var checkbox = todoToCheck.GetByTestId("todo-item-toggle");
            await checkbox.CheckAsync();
        }
        public async Task<bool> IsTodoCompleted(string task)
        {
            var checkedTodo = _todoList.GetByTestId("todo-item").Filter(new() { HasText = task });
            var checkbox = checkedTodo.GetByTestId("todo-item-toggle");
            //TODO: completed task should be striketrough
            return await checkbox.IsCheckedAsync();
        }

        public async Task ShowCompleted()
        {
            await _page.GetByRole(AriaRole.Link, new() { Name = "Completed" }).ClickAsync();
        }

        public async Task ShowAll()
        {
            await _page.GetByRole(AriaRole.Link, new() { Name = "All" }).ClickAsync();
        }

        public async Task ShowActive()
        {
            await _page.GetByRole(AriaRole.Link, new() { Name = "Active" }).ClickAsync();
        }

        public async Task ClearCompleted()
        {
            await _page.GetByRole(AriaRole.Button, new() { Name = "Clear completed" }).ClickAsync();
        }

        public async Task<bool> IsTodoListEmpty()
        {
            var items = _todoList.GetByRole(AriaRole.Listitem);
            int count = await items.CountAsync();
            return count.Equals(0);
        }

        public async Task DeleteTodo(string task)
        {
            var todoToDelete = _todoList.GetByTestId("todo-item").Filter(new() { HasText = task });
            await todoToDelete.HoverAsync();
            var delete = todoToDelete.GetByTestId("todo-item-button");
            await delete.ClickAsync();
        }

        public async Task EditTodo(string task)
        {
            var todoToEdit = _todoList.GetByTestId("todo-item").Filter(new() { HasText = task });
            await todoToEdit.DblClickAsync();
            //TODO: fill //TODO: save

        }

    }
}