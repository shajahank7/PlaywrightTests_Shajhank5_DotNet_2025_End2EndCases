using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

namespace Pages
{
    public class ApplyExtradaypage
    {
        private readonly IPage page;

        public ApplyExtradaypage(IPage page)
        {
            this.page = page;
        }

        // Locators
        private ILocator ReimbursmentButton => page.Locator("xpath=//p[text()='Reimbursement']").First;
        private ILocator AllHistoryButton => page.Locator("xpath=//button[text()='All History']");
        private ILocator ApplyExtraDayButton => page.Locator("xpath=//button[text()='Apply Extra Work']");
        private ILocator SelectDateInput => page.Locator("xpath=//input[@name='date']");
        private ILocator SelectHoursInput => page.Locator("xpath=//input[@name='hours']");
        private ILocator SelectLeadSelect => page.Locator("xpath=//select[@name='lead']");
        private ILocator CancelButton => page.Locator("xpath=//button[text()='Cancel']");
        private ILocator SubmitButton => page.Locator("xpath=//button[text()='Submit']");


        // Methods
        public async Task NavigateToApplyWork()
        {
            await Task.Delay(2000);
            await ReimbursmentButton.ClickAsync();
            await ApplyExtraDayButton.ClickAsync();
        }

        public async Task SelectDateAsync()
        {
            string previousDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            await SelectDateInput.FillAsync(previousDate);

        }

        public async Task SelectHoursAsync()
        {
            await SelectHoursInput.FillAsync("8");
        }

        public async Task SelectLeadAsync()
        {
            await page.WaitForSelectorAsync("xpath=//select[@name='lead']");
            await SelectLeadSelect.SelectOptionAsync(new SelectOptionValue { Value = "shajahant389@gmail.com" });
        }

        public async Task SubmitAsync()
        {
            await SubmitButton.ClickAsync();
        }

    }
}
