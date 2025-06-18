using Allure.Commons;
using Microsoft.Playwright;

public class ReuseableClass
{
    protected static IPage? page;
    public async Task<bool> IsElementVisibleAsync(IPage page, ILocator locator)
    {
        return await locator.IsVisibleAsync();
    }

    public async Task<string> GetInnerText(IPage page, ILocator locator)
    {
        return await locator.InnerTextAsync();
    }

    public async Task WaitUntilElementIsVisibleAsync(ILocator locator)
    {
        await locator.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 5000
        });
    }

    public async Task CaptureAndAttachScreenshot(string name = "Failure Screenshot")
    {
        if (page == null) return;

        string screenshotPath = Path.Combine(
            TestContext.CurrentContext.WorkDirectory,
            $"screenshot_{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png"
        );

        await page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = screenshotPath,
            FullPage = true
        });

        if (File.Exists(screenshotPath))
        {
            byte[] screenshotBytes = await File.ReadAllBytesAsync(screenshotPath);
            AllureLifecycle.Instance.AddAttachment(name, "image/png", screenshotBytes);
        }
    }

}