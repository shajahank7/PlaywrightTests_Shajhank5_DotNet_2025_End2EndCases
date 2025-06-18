using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

namespace Pages
{
    public class ApproveExtradaypage
    {
        private readonly IPage page;

        // Constructor
        public ApproveExtradaypage(IPage page)
        {
            this.page = page;
        }

        // Locators
        private ILocator ReimbursmentButton => page.Locator("xpath=//p[text()='Reimbursement']").First;
        private ILocator RequestsButton => page.Locator("xpath=//button[text()='Requests']");
        private ILocator ApproveButton => page.Locator("xpath=//button[text()='Approve']").First;

        // Methods
        public async Task NavigateToApplyWork()
        {
            await Task.Delay(2000);
            await ReimbursmentButton.ClickAsync();
        }
        public async Task ApproveRequest()
        {
            await RequestsButton.ClickAsync();
            await ApproveButton.ClickAsync();
        }
    }
}
