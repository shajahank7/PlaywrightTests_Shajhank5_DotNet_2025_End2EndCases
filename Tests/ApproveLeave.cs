
using Microsoft.Playwright;
using Pages;
using static Microsoft.Playwright.Assertions;
using System.Text.RegularExpressions;
using System;
using System.Threading.Tasks;
using DotNetEnv;
using NUnit.Framework;
using Allure.NUnit.Attributes;
using Allure.NUnit;


namespace Test
{
    [TestFixture]
    public class ApproveLeave : BaseTest
    {

        [Test, Order(5)]
        [AllureTag("Smoke")]
        [AllureOwner("Nikitha")]
        [AllureSubSuite("Approve leave test case")]
        public async Task Approve()
        {
            var loginPage = new LoginPage(page!);
            var applyLeave = new LeavePage(page!);
            var approveLeave = new ApproveLeavePage(page!);
            Console.WriteLine("Approve leave test case");
            Console.WriteLine($"Navigating to URL: {EnvReader.Url}");
            await loginPage.NavigateToLoginPageAsync(EnvReader.Url);
            await page!.SetViewportSizeAsync(1530, 850);
            await loginPage.LoginAsync(EnvReader.ReportingMail, EnvReader.ReportingPassword);
            await applyLeave.NavigateToLeaveApplicationAsync();
            await Expect(page).ToHaveURLAsync(new Regex(".*leave_management.*"));
            Console.WriteLine("After clicking on leave managemnet in Approve leave");
            Assert.That(await applyLeave.IsLeaveManagementHeaderDisplayed(), "Leave management header is not displayed");
            await approveLeave.ClickRequestsTab();
            await approveLeave.ClickApproveButton();
            Assert.That(await approveLeave.IsApprovedTextDisplayed(), "Approved text is not displayed");
            Console.WriteLine("Completeion of approve leave test case");
        }
    }

}


