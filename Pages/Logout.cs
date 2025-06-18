using Microsoft.Playwright;
using System.Threading.Tasks;

namespace Pages
{
    public class LogoutPage
    {
        private readonly IPage page;
        public readonly ILocator logoutButton;
        public readonly ILocator alertButton;

        public LogoutPage(IPage page)
        {
            this.page = page;
            logoutButton = page.Locator("//p[text()='Logout']");
            alertButton = page.Locator("//button[normalize-space()='Yes']");
        }

        public async Task PerformLogout()
        {


            await logoutButton.ClickAsync();
            Console.WriteLine("Step: Logging out");
            await alertButton.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible
            });
            await alertButton.ClickAsync();
        }

        public async Task<String?> isLogoutButtonDisplayed()
        {
            return await logoutButton.TextContentAsync();
        }
    }
}