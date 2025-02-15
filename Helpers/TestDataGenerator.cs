namespace TODO_MVC.Helpers;

public static class TestDataGenerator
{
    public static string GenerateUniqueTodoName()
    {

        return $"Task_{DateTime.Now:s}";
        
    }
}
