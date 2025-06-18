using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.VisualBasic;
using Utilities;
using System.IO;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace Pages
{
    public class AddEmployeePage
    {
        private readonly IPage page;
        private readonly Dictionary<string, ILocator> locators;
        private readonly string jsonFilePath = "C:\\Users\\Admin\\Desktop\\playwrightdemo\\automation\\c#\\PlaywrightTests\\Utilities\\testdata.json";
        public string Email = FakeData.Email.ToLower();
        public string EmployeeId = FakeData.RandomId;
        public string password = "Shajahan@123";

        ReuseableClass reuseableClass = new ReuseableClass();
        public AddEmployeePage(IPage page)
        {
            this.page = page;

            locators = new Dictionary<string, ILocator>
            {
                ["employeesTab"] = page.Locator("text='Employees'"),
                ["addEmployeeBtn"] = page.Locator("text='Add Employee'"),
                ["firstName"] = page.Locator("input[name='firstName']"),
                ["lastName"] = page.Locator("input[name='lastName']"),
                ["id"] = page.Locator("input[name='id']"),
                ["email"] = page.Locator("input[name='email']"),
                ["role"] = page.Locator("select[id='role']"),
                ["role_option"] = page.Locator("//option[@value='Employee']"),
                ["Qualification"] = page.Locator("#qualifications"),
                ["password"] = page.Locator("input[name='password']"),
                ["dob"] = page.Locator("input[name='dob']"),
                ["joiningDate"] = page.Locator("input[name='joiningDate']"),
                ["qualifications"] = page.Locator("select[id='qualifications']"),
                ["department"] = page.Locator("input[name='department']"),
                ["gender"] = page.Locator("select[id='gender']"),
                ["mobileNumber"] = page.Locator("input[name='mobileNumber']"),
                ["bloodGroup"] = page.Locator("select[id='bloodGroup']"),
                ["designation"] = page.Locator("input[name='designation']"),
                ["salary"] = page.Locator("input[name='salary']"),
                ["location"] = page.Locator("input[name='location']"),
                ["reportingTo"] = page.Locator("//*[@id='reportingTo']"),
                ["certificatesTab"] = page.Locator("text='Certificates'"),
                ["certificates"] = page.Locator("//*[@class='dropdown-btn']"),
                ["certificateCheckbox"] = page.Locator("//input[@name='Degree']"),
                ["addButton"] = page.Locator("//button[@type='submit' and text()='Add']"),
                ["empid"] = page.Locator("//input[@i    d='ag-1435-input']"),
                ["check"] = page.Locator("//input[@id='ag-447-input']"),
                ["savedsuccessfully"] = page.Locator("//*[text()='Saved Successfully']"),
                ["logoutButton"] = page.Locator("//p[text()='Logout']"),
                ["searchEmployee"] = page.Locator("//*[@aria-label='EMP ID Filter Input']"),
                ["emailText"] = page.Locator("//*[contains(@aria-label,'Press SPACE')]//*[@col-id='email']")
                // alertMessage locator needs testData, create dynamically in method below
            };
        }

        public async Task NavigateToAddEmployeeFormAsync()
        {
            await locators["employeesTab"].ClickAsync();
            await locators["addEmployeeBtn"].ClickAsync();
        }

        public async Task FillBasicEmployeeInfoAsync()
        {
            await locators["firstName"].WaitForAsync();
            await locators["firstName"].FillAsync(FakeData.FirstName);
            Console.WriteLine("First name in add employee page:" + FakeData.FirstName);
            await locators["lastName"].WaitForAsync();
            await locators["lastName"].FillAsync(FakeData.LastName);
            await locators["id"].WaitForAsync();
            await locators["id"].FillAsync(EmployeeId);
            await locators["dob"].WaitForAsync();

            await locators["dob"].FillAsync(FakeData.DOB);
        }

        public async Task FillJobInfoAsync(string password)
        {
            await locators["role"].ClickAsync();
            await locators["role"].SelectOptionAsync(new SelectOptionValue { Value = "Employee" });

            await locators["password"].WaitForAsync();
            await locators["password"].FillAsync(password);
            await locators["joiningDate"].WaitForAsync();
            await locators["joiningDate"].FillAsync(FakeData.DOJ);
            await locators["Qualification"].ClickAsync();
            await locators["Qualification"].SelectOptionAsync(new SelectOptionValue { Value = "B.Tech" });
            await locators["designation"].WaitForAsync();
            await locators["designation"].FillAsync(FakeData.RandomDesignation);
            Console.WriteLine("Designation in add employee page:" + FakeData.RandomDesignation);

        }



        public async Task FillContactDetailsAsync(string bloodGroup)
        {
            await locators["email"].FillAsync(Email);
            GlobalData.TestData["email"] = Email;
            Console.WriteLine("fillcontcat" + GlobalData.TestData["email"]);
            await page.WaitForTimeoutAsync(1000);
            Console.WriteLine("Email in add employee page:" + Email);
            await locators["mobileNumber"].FillAsync(FakeData.Phone);
            await page.WaitForTimeoutAsync(1000);

            await locators["department"].FillAsync(FakeData.RandomDepartment);
            await locators["gender"].SelectOptionAsync(new SelectOptionValue { Value = "Male" });

            await locators["bloodGroup"].ClickAsync();
            await locators["bloodGroup"].SelectOptionAsync(new SelectOptionValue { Value = "AB+" });

            await locators["location"].FillAsync(FakeData.Location);
        }

        public async Task SelectAdditionalDetailsAsync()
        {
            await locators["reportingTo"].ClickAsync();
            await locators["reportingTo"].WaitForAsync();
            await page.WaitForTimeoutAsync(2000);
            var options = await locators["reportingTo"].Locator("option").AllAsync();
            await locators["reportingTo"].SelectOptionAsync(new SelectOptionValue { Value = "shajahant389@gmail.com" });
            Console.WriteLine("reporting to emial method");
            await locators["salary"].ClickAsync();
            await locators["salary"].FillAsync(FakeData.Salary.ToString());

        }

        public async Task HandleCertificatesAsync()
        {
            await locators["certificates"].ClickAsync();
            await locators["certificateCheckbox"].WaitForAsync();
            await locators["certificateCheckbox"].ScrollIntoViewIfNeededAsync();
            await locators["certificateCheckbox"].ClickAsync();
        }

        public async Task SubmitEmployeeFormAsync()
        {
            await locators["addButton"].WaitForAsync();
            await locators["addButton"].ClickAsync();
            await page.WaitForTimeoutAsync(3000);

        }

        public async Task LogoutAsync()
        {
            await locators["logoutButton"].ClickAsync();

            var alertLocator = page.Locator("//button[normalize-space()='Yes']");
            await alertLocator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            await alertLocator.ClickAsync();
        }

        public async Task<bool> IsSavedSuccessfullyTextDisplayed()
        {
            return await reuseableClass.IsElementVisibleAsync(page, locators["savedsuccessfully"]);
        }

        public async Task searchEmployee()
        {
            Console.WriteLine("search employee method: " + EmployeeId);
            await locators["searchEmployee"].WaitForAsync();
            await locators["searchEmployee"].ClickAsync();
            Console.WriteLine("After click method: " + EmployeeId);
            await locators["searchEmployee"].FillAsync(EmployeeId);
            Console.WriteLine("search employee click method: " + EmployeeId);


        }

        public async Task<bool> IsEmployeeDisplayed()
        {
            var employeeLocator = page.Locator($"text='{EmployeeId}'");

            // Wait until the element is visible (with timeout)
            try
            {
                await employeeLocator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
                return true;
            }
            catch (TimeoutException)
            {
                return false;
            }
        }


        public async Task<string> GetEmailID()
        {
            await reuseableClass.WaitUntilElementIsVisibleAsync(locators["emailText"]);
            await locators["emailText"].ScrollIntoViewIfNeededAsync();
            var emailId = await locators["emailText"].TextContentAsync();
            Console.WriteLine("Getting email ID: " + (emailId ?? "null"));
            await page.WaitForTimeoutAsync(5000);
            return emailId ?? string.Empty;
        }

        public async Task<string> StoreAuthToken()
        {
            // Wait until the emailText element is visible
            await reuseableClass.WaitUntilElementIsVisibleAsync(locators["emailText"]);

            // Get the text content of the emailText element
            var storedEmail = await locators["emailText"].TextContentAsync();

            // Null or empty check
            if (string.IsNullOrEmpty(storedEmail))
            {
                Console.WriteLine("Email not found or is empty.");
                throw new Exception("Email text is empty.");
            }

            // Log and store in global test data
            Console.WriteLine("In store auth method: " + storedEmail);
            GlobalData.TestData["email"] = storedEmail;

            return storedEmail;
        }


    }
}