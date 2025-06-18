
using Microsoft.Playwright;
using Pages;
using System;
using System.Threading.Tasks;
using DotNetEnv;
using NUnit.Framework;
using Utilities;
using Allure.NUnit.Attributes;
using Allure.NUnit;

namespace Test
{
    [TestFixture]
    public class ApplyLeave : BaseTest
    {

        [Test, Order(4)]
        [AllureTag("Smoke")]
        [AllureSubSuite("Apply leave test case")]
        [AllureLabel("displayName", "Apply leave")]
        public async Task Apply()
        {
            var loginPage = new LoginPage(page!);
            var applyLeave = new LeavePage(page!);
            var addEmployeePage = new AddEmployeePage(page!);
            Console.WriteLine("Apply leave global test data" + GlobalData.TestData["email"]);
            Console.WriteLine("Apply leave test case");
            Console.WriteLine($"Navigating to URL: {EnvReader.Url}");
            await loginPage.NavigateToLoginPageAsync(EnvReader.Url);
            await page!.SetViewportSizeAsync(1530, 850);
            await loginPage.LoginAsync(GlobalData.TestData["email"], "Shajahan@123");
            await applyLeave.ClickYourProfile();
            await applyLeave.ClickOthersButton();
            await applyLeave.SelectLeadinOthersTab();
            await applyLeave.ClickSubmitButton();
            await applyLeave.NavigateToLeaveApplicationAsync();
            Assert.That(await applyLeave.IsLeaveManagementHeaderDisplayed(), "Leave management header is not displayed");
            await applyLeave.ClickApplyLeaveButton();
            await applyLeave.ApplyLeaveAsync();
            await applyLeave.SubmitLeaveAsyn();
            Assert.That(await applyLeave.IsApplyTextDisplayed(), Is.True, "Apply text is not displayed");
            Assert.That(await applyLeave.GetActualStartDate(), Is.EqualTo(applyLeave.ExpectedStartDate()), "Start date are not matching");
            Assert.That(await applyLeave.GetActualEndtDate(), Is.EqualTo(applyLeave.ExpectedToDate()), "End date are not matching");
            Assert.That(await applyLeave.GetActualNumberOfDays(), Is.EqualTo(await applyLeave.ExpectedNumberOfDays()), "Number of days are not matching");
            Console.WriteLine("Completeion of apply leave test case");
        }
    }
}


