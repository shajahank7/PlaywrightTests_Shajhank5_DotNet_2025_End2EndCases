// using Allure.Commons;
// using Microsoft.Playwright;

// namespace Test
// {
//     public class AttcachScreenshots
//     {
//         [Test]
//         public static async Task CaptureAndAttachScreenshot(IPage page, string name = "Failure Screenshot")
//         {
//             Console.WriteLine("this is the method for screenshot attaching");
//             string screenshotPath = Path.Combine(
//                 TestContext.CurrentContext.WorkDirectory,
//                 $"screenshot_{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png"
//             );

//             await page.ScreenshotAsync(new PageScreenshotOptions
//             {
//                 Path = screenshotPath,
//                 FullPage = true
//             });

//             if (File.Exists(screenshotPath))
//             {
//                 Console.WriteLine("this is the before method for file exists");
//                 byte[] screenshotBytes = await File.ReadAllBytesAsync(screenshotPath);
//                 AllureLifecycle.Instance.AddAttachment(name, "image/png", screenshotBytes);
//                 Console.WriteLine("this is the after method for file exists");
//             }
//             Console.WriteLine("this is the method comepletion for screenshot attaching");
//         }
//     }
// }