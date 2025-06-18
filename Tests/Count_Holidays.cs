using Microsoft.Playwright;
using DotNetEnv;
using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualBasic;
using static Microsoft.Playwright.Assertions;
using System.Text.RegularExpressions;
using Pages;
using Allure.NUnit.Attributes;
using Allure.NUnit;

namespace Test
{
    [TestFixture]
    public class Count_Holidays : BaseTest
    {
        [Test, Order(2)]
        [AllureTag("Smoke")]
        [AllureOwner("Nikitha")]
        [AllureLabel("displayName", "Verify Holidays Count Display")]
        public async Task Holidays()
        {
            using var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();

            var loginPage = new LoginPage(page);
            var holidayPage = new HolidayPage(page);
            // var holiday = new Holidays(page); // Uncomment if needed
            await loginPage.NavigateToLoginPageAsync(EnvReader.Url);
            await page!.SetViewportSizeAsync(1530, 850);
            await Expect(page).ToHaveURLAsync(new Regex(".*login.*"));
            await loginPage.LoginAsync(EnvReader.Username, EnvReader.Password);
            await Expect(page).ToHaveURLAsync(new Regex("dev.urbuddi.com"));
            await holidayPage.WaitForHolidaysToLoadAsync();
            await holidayPage.GetHolidayCountAsync();
            await holidayPage.GetHolidayTextsAsync();
            await holidayPage.PrintHolidaysToConsoleAsync();
        }
    }
}
