namespace PlaywrightTests.Helpers;

public static class TestDataGenerator
{
    public static string GenerateUniqueTodoName(string taskName = "Task")
    {

        return $"{taskName}_{DateTime.Now:s}";
        
    }
}
