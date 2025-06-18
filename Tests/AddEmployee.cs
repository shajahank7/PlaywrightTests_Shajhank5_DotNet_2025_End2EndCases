using Microsoft.Playwright;
using Allure.NUnit.Attributes;
using Pages;
using static Microsoft.Playwright.Assertions;
using System.Text.RegularExpressions;
using Allure.NUnit;

namespace Test
{
    [TestFixture]
    public class AddEmployee : BaseTest
    {
        // private IPage? page;

        [Test, Order(3)]
        [AllureTag("Smoke")]
        [AllureSubSuite("Add Employee test case")]
        [AllureLabel("displayName", "Verify Add employee")]
        public async Task AddEmployeeUI()
        {
            var loginPage = new LoginPage(page!);
            var logout = new LogoutPage(page!);
            var addEmployee = new AddEmployeePage(page!);

            Console.WriteLine("Add employee test case");

            await loginPage.NavigateToLoginPageAsync(EnvReader.Url);
            await Expect(page).ToHaveURLAsync(new Regex("login"));
            await loginPage.NavigateToLoginPageAsync(EnvReader.Url);
            await page!.SetViewportSizeAsync(1530, 850);
            await loginPage.LoginAsync(EnvReader.Username, EnvReader.Password);
            await addEmployee.NavigateToAddEmployeeFormAsync();
            await addEmployee.FillBasicEmployeeInfoAsync();
            await addEmployee.FillJobInfoAsync(addEmployee.password);

            await addEmployee.FillContactDetailsAsync("O+");
            await addEmployee.SelectAdditionalDetailsAsync();
            await addEmployee.HandleCertificatesAsync();
            await addEmployee.SubmitEmployeeFormAsync();
            await addEmployee.searchEmployee();
            Assert.That(await addEmployee.IsEmployeeDisplayed(), "Employee not created successfully");
            Console.WriteLine("email id" + addEmployee.GetEmailID());
            Assert.That(await addEmployee.GetEmailID(), Is.EqualTo(addEmployee.Email), "Emails Ids are not matching");
            Console.WriteLine("email id for writing in excel" + await addEmployee.GetEmailID());
            Console.WriteLine("After writing into excel sheet");
            await addEmployee.LogoutAsync();
        }

    }
}