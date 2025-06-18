using Microsoft.Playwright;
using System.Data.Common;
using System.Threading.Tasks;
using Utilities;

namespace Pages
{
    public class ApproveLeavePage
    {
        private readonly IPage page;

        private ILocator ApproveButton => page.Locator("//button[text()='Approve']").Nth(0);
        private ILocator RequestsButton => page.Locator("//button[text()='Requests']");
        private ILocator ApprovedText => page.Locator("//*[text()='Leave Approved']");
        private ILocator HorizontalScrollBar => page.Locator("//*[contains(@class,'horizontal-scroll') and@ref='eViewport']");
        ReuseableClass reuseableClass = new ReuseableClass();

        public ApproveLeavePage(IPage page)
        {
            this.page = page;
        }

        public async Task ClickRequestsTab()
        {
            await Task.Delay(2000);
            await reuseableClass.WaitUntilElementIsVisibleAsync(RequestsButton);
            await RequestsButton.ScrollIntoViewIfNeededAsync();
            await RequestsButton.ClickAsync();
        }

        public async Task ClickApproveButton()
        {
            Console.WriteLine("Inside approve leave button started");
            await HorizontalScrollBar.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });

            // Scroll the container horizontally
            await page.EvalOnSelectorAsync("//*[contains(@class,'horizontal-scroll') and@ref='eViewport']", "el => el.scrollLeft = el.scrollWidth");

            // Wait for button visibility
            await ApproveButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });

            // Ensure it's in viewport
            await ApproveButton.ScrollIntoViewIfNeededAsync();

            // Click it
            await ApproveButton.ClickAsync();

            Console.WriteLine("Inside approve leave button completed");
        }


        public async Task<bool> IsApprovedTextDisplayed()
        {
            await Task.Delay(2000);
            return await reuseableClass.IsElementVisibleAsync(page, ApprovedText);
        }

    }
}