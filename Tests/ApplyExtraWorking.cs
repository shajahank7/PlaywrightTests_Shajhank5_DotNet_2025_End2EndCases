using Pages;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Allure.NUnit.Attributes;
using Utilities;

namespace Test
{
    [TestFixture]
    public class ApplyExtraWorking : BaseTest
    {

        [Test, Order(6)]
        [AllureTag("Smoke")]
        [AllureSubSuite("Apply extra working day")]
        [AllureLabel("displayName", "Apply extra working day")]
        public async Task ApplyExtraDay1()
        {
            var loginPage = new LoginPage(page!);
            var applyExtraDay = new ApplyExtradaypage(page!);
            Console.WriteLine("Apply extra working day test case");
            Console.WriteLine($"Navigating to URL: {EnvReader.Url}");
            await loginPage.NavigateToLoginPageAsync(EnvReader.Url);
            await page!.SetViewportSizeAsync(1530, 850);
            Console.WriteLine("Email id in Apply extra working day test case" + GlobalData.TestData["email"]);
            await loginPage.LoginAsync(GlobalData.TestData["email"], "Shajahan@123");
            await applyExtraDay.NavigateToApplyWork();
            await applyExtraDay.SelectDateAsync();
            await applyExtraDay.SelectHoursAsync();
            await applyExtraDay.SelectLeadAsync();
            await applyExtraDay.SubmitAsync();
            Console.WriteLine("Apply extra working day completed");
        }
    }
}
