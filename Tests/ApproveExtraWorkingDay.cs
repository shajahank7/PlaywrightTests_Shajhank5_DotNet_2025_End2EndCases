using Pages;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Allure.NUnit.Attributes;

namespace Test
{
    [TestFixture]
    public class ApproveExtraWorkingDay : BaseTest
    {
        [Test, Order(7)]
        [AllureTag("Smoke")]
        [AllureSubSuite("Approve extra working day")]
        [AllureLabel("displayName", "Approve extra working day")]
        public async Task ApproveExtraWorking()
        {
            var loginPage = new LoginPage(page!);
            var approveExtraWorking = new ApproveExtradaypage(page!);
            Console.WriteLine("Approve extra working day test case");

            Console.WriteLine($"Navigating to URL: {EnvReader.Url}");
            await loginPage.NavigateToLoginPageAsync(EnvReader.Url);
            await page!.SetViewportSizeAsync(1530, 850);
            await loginPage.LoginAsync(EnvReader.ReportingMail, EnvReader.ReportingPassword);

            await approveExtraWorking.NavigateToApplyWork();
            await approveExtraWorking.ApproveRequest();

            Console.WriteLine("Approve extra working day test case completed");
        }
    }
}
