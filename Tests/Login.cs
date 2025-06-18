using Microsoft.Playwright;
using Allure.NUnit.Attributes;
using Pages;
using static Microsoft.Playwright.Assertions;
using System.Text.RegularExpressions;
using Allure.Commons;
using Allure.NUnit;

namespace Test
{
    [TestFixture]
    public class Login : BaseTest
    {
        [Test, Order(1)]
        [AllureTag("Smoke")]
        [AllureOwner("Nikitha")]
        [AllureLabel("displayName", "Verify successful login and logout")]

        public static async Task Loginpage()
        {
            var loginPage = new LoginPage(page!);
            var logout = new LogoutPage(page!);
            await loginPage.NavigateToLoginPageAsync(EnvReader.Url);
            await page!.SetViewportSizeAsync(1530, 850);
            await Expect(page).ToHaveURLAsync(new Regex(".*login.*"));
            await loginPage.LoginAsync(EnvReader.Username, EnvReader.Password);
            await Expect(page).ToHaveURLAsync(new Regex("dev.urbuddi.com"));
            Assert.That(await logout.isLogoutButtonDisplayed(), Is.EqualTo("Logout"), "Value is not changed");
        }
    }
}
