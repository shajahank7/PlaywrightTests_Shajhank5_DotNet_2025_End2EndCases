using Allure.Commons;
using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Test
{
    public class BaseTest
    {
        public required IPlaywright playwright;
        public required IBrowser browser;
        public required IBrowserContext context;
        protected static IPage? page;

        [SetUp]
        public async Task SetUpAsync()
        {
            playwright = await Playwright.CreateAsync();
            browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            context = await browser.NewContextAsync();
            page = await context.NewPageAsync();
        }

        [TearDown]
        public void TearDownAsync()
        {
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;

            if (testStatus == NUnit.Framework.Interfaces.TestStatus.Failed && page != null)
            {
                try
                {
                    string screenshotPath = Path.Combine(
                        TestContext.CurrentContext.WorkDirectory,
                        $"screenshot_{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png"
                    );

                    page.ScreenshotAsync(new PageScreenshotOptions
                    {
                        Path = screenshotPath,
                        FullPage = true
                    }).GetAwaiter().GetResult();




                    if (File.Exists(screenshotPath))
                    {
                        Console.WriteLine("this is inside the file exists screenshot");
                        Console.WriteLine("this is path for screenshot path" + screenshotPath);
                        string path = Path.Combine(screenshotPath);
                        Console.WriteLine("Path is " + path);


                        var screenshotBytes = File.ReadAllBytes(path);

                        AllureLifecycle.Instance.AddAttachment("Failure Screenshot", "image/png", screenshotBytes, ".png");

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ Screenshot attach failed: " + ex.Message);
                }
            }
            context.CloseAsync().GetAwaiter().GetResult();
            browser.CloseAsync().GetAwaiter().GetResult();
            playwright.Dispose();
        }
    }
}
