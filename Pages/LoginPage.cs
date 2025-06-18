using Microsoft.Playwright;
using System;
using System.Threading.Tasks;


namespace Pages
{
    public class LoginPage
    {
        public readonly IPage page;
        public readonly ILocator userEmailInput;
        public readonly ILocator userPasswordInput;
        public readonly ILocator loginButton;
        public readonly ILocator logoutButton;
        public readonly ILocator alertButton;

        public LoginPage(IPage page)
        {
            this.page = page;

            userEmailInput = page.Locator("input[id='userEmail']");
            userPasswordInput = page.Locator("input[id='userPassword']");
            loginButton = page.Locator("//button[text()='Login']");
            logoutButton = page.Locator("//p[text()='Logout']");
            alertButton = page.Locator($"//button[normalize-space()='Yes']");
        }

        public async Task NavigateToLoginPageAsync(string url)
        {
            Console.WriteLine("Step: Navigate to Login Page");
            await page.GotoAsync(url);
        }

        public async Task LoginAsync(string email, string password)
        {
            Console.WriteLine("Step: Logging in");
            await userEmailInput.FillAsync(email);
            Console.WriteLine("Email id " + email);
            await userPasswordInput.FillAsync(password);
            Console.WriteLine("Password " + password);
            await loginButton.ClickAsync();
        }

        public async Task LogoutAsync()
        {
            Console.WriteLine("Step: Logging out");
            await logoutButton.ClickAsync();
            Thread.Sleep(3000);
            await alertButton.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible
            });
            await alertButton.ClickAsync();
        }


    }
}
