using System.Diagnostics;

namespace AppLibrary.Tests.EfRepositoryTests;

[SetUpFixture]
public class EfRepositoryTestSetup
{
    [OneTimeTearDown]
    public void RunAfterAllTests()
    {
        // Don't leave LocalDB process running (fixes test runner warning)
        // See https://resharper-support.jetbrains.com/hc/en-us/community/posts/360006736719/comments/360002383960
        using var process = Process.Start("sqllocaldb", "stop MSSQLLocalDB");
        process.WaitForExit();
    }
}
