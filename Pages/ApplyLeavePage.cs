using Microsoft.Playwright;
using System.Data.Common;
using System.Threading.Tasks;
using Utilities;

namespace Pages
{
    public class LeavePage
    {
        private readonly IPage page;

        // Locators
        private ILocator LeaveManagementButton => page.Locator("xpath=//*[text()='Leave Management']").First;
        private ILocator popupmeaage => page.Locator("xpath=//*[@class='modal-content']//button[text()='Ok']");
        private ILocator ApplyLeaveButton => page.Locator("xpath=//button[text()='Apply Leave']");
        private ILocator FromDateInput => page.Locator("#fromDate");
        private ILocator ToDateInput => page.Locator("#toDate");
        private ILocator SubjectInput => page.Locator("xpath=//input[@name='subject']");
        private ILocator ReasonTextarea => page.Locator("xpath=//textarea[@name='reason']");
        private ILocator LeaveDropdown => page.Locator("#leave");
        private ILocator SubmitButton => page.Locator("xpath=//button[text()='Submit']");
        private ILocator ApplyLeaveText => page.Locator("//*[@class='modal-heading']");
        private ILocator NumberOfDaysText => page.Locator("//*[contains(@class,'leave-form')]//p");
        private ILocator StartDate => page.Locator("//*[@class='ag-body ag-layout-normal']//*[@col-id='startDate']");
        private ILocator EndDate => page.Locator("//*[@class='ag-body ag-layout-normal']//*[@col-id='endDate']");
        private ILocator ActualNumberOfDaysOnUI => page.Locator("//*[@class='ag-body ag-layout-normal']//*[@col-id='days']");
        private ILocator LeaveManagementHeader => page.Locator("//*[contains(@class,'header')]//*[text()='Leave Management']");
        private ILocator YourProfile => page.Locator("//*[text()='Your Profile']");
        private ILocator OtherButton => page.Locator("//*[text()='Others']");
        private ILocator SelectLead => page.Locator("//*[normalize-space(@class)='select-input']");
        private ILocator SelectLeadinApplyLeave => page.Locator("//select[@name='lead']");

        private readonly int numberOFDays = 1;

        public LeavePage(IPage page)
        {
            this.page = page;
        }
        ReuseableClass reuseableClass = new ReuseableClass();
        public async Task NavigateToLeaveApplicationAsync()
        {
            await LeaveManagementButton.ClickAsync();

        }

        public async Task ClickYourProfile()
        {
            Console.WriteLine("Inside your profile method");
            await reuseableClass.IsElementVisibleAsync(page, YourProfile);
            await YourProfile.ClickAsync();
        }

        public async Task ClickOthersButton()
        {
            Console.WriteLine("Inside other button method");
            await reuseableClass.IsElementVisibleAsync(page, OtherButton);
            await OtherButton.ClickAsync();
        }

        public async Task SelectLeadinOthersTab()
        {
            await SelectLead.ClickAsync();
            await SelectLead.WaitForAsync();
            var options = await SelectLead.Locator("option").AllAsync();
            await SelectLead.SelectOptionAsync(new SelectOptionValue { Value = "shajahant389@gmail.com" });
        }

        public async Task ClickApplyLeaveButton()
        {
            await ApplyLeaveButton.ClickAsync();
            await popupmeaage.ClickAsync();
        }

        public async Task<bool> IsLeaveManagementHeaderDisplayed()
        {
            return await reuseableClass.IsElementVisibleAsync(page, LeaveManagementHeader);
        }

        public async Task ApplyLeaveAsync()
        {
            Console.WriteLine("Random date : " + numberOFDays);
            var currentDateString = GetFromDate();
            await SelectLeadinApplyLeave.ClickAsync();
            await SelectLeadinApplyLeave.WaitForAsync();
            // var options = await SelectLeadinApplyLeave.Locator("option").AllAsync();
            await SelectLeadinApplyLeave.SelectOptionAsync(new SelectOptionValue { Value = "shajahant389@gmail.com" });
            await FromDateInput.FillAsync(currentDateString);
            Console.WriteLine("current date from date: " + currentDateString);
            var toDateString = GetToDate();
            await ToDateInput.WaitForAsync();
            await ToDateInput.FillAsync(toDateString);
            await SubjectInput.WaitForAsync();
            await SubjectInput.FillAsync(FakeData.Subject);
            await ReasonTextarea.WaitForAsync();
            await ReasonTextarea.FillAsync(FakeData.Reason);
        }

        public async Task SubmitLeaveAsyn()
        {
            Console.WriteLine("Submit method start");
            await LeaveDropdown.ClickAsync();
            await SubmitButton.WaitForAsync();
            await SubmitButton.ClickAsync();
            Console.WriteLine("Submit method end");
        }
        public string GetFromDate()
        {
            var currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            return currentDate;
        }

        public string ExpectedStartDate()
        {
            var currentDate = DateTime.Now.ToString("dd-MM-yyyy");
            return currentDate;
        }

        public async Task ClickSubmitButton()
        {
            Console.WriteLine("Inside click submit button");
            await Task.Delay(3000);
            await reuseableClass.IsElementVisibleAsync(page, SubmitButton);
            await SubmitButton.ClickAsync();
        }

        public string GetToDate()
        {
            var currentDay = DateTime.Now.ToString("dd");
            var DayOftheWeekString = DateTime.Now.DayOfWeek.ToString();
            Console.WriteLine("current Day: " + currentDay);
            Console.WriteLine("Day of the week: " + DayOftheWeekString);
            if (DayOftheWeekString == "Saturday")
            {
                if (currentDay == "30" || currentDay == "31")
                {
                    var nextMonthDate = DateTime.Now.AddMonths(1).AddDays(numberOFDays).ToString("yyyy-MM-dd");
                    Console.WriteLine("Next month plus number of days with day saturday: " + nextMonthDate);
                    return nextMonthDate;
                }
                var currentDayPlusTwo = DateTime.Now.AddDays(1).AddDays(1).ToString("yyyy-MM-dd");
                Console.WriteLine("current Day plus one day: " + currentDayPlusTwo);
                return currentDayPlusTwo;
            }
            if (DayOftheWeekString == "Sunday")
            {
                if (currentDay == "30" || currentDay == "31")
                {
                    var nextMonthDate = DateTime.Now.AddMonths(1).AddDays(numberOFDays).ToString("yyyy-MM-dd");
                    Console.WriteLine("Next month plus number of days with day saturday: " + nextMonthDate);
                    return nextMonthDate;
                }
                var currentDayPlusTwo = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                Console.WriteLine("current Day plus one day: " + currentDayPlusTwo);
                return currentDayPlusTwo;
            }
            var currentDayPlus = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            Console.WriteLine("current Day plus one day: " + currentDayPlus);
            return currentDayPlus;
        }

        public string ExpectedToDate()
        {
            var currentDay = DateTime.Now.ToString("dd");
            var DayOftheWeekString = DateTime.Now.DayOfWeek.ToString();
            Console.WriteLine("current Day: " + currentDay);
            Console.WriteLine("Day of the week: " + DayOftheWeekString);
            if (DayOftheWeekString == "Saturday")
            {
                if (currentDay == "30" || currentDay == "31")
                {
                    var nextMonthDate = DateTime.Now.AddMonths(1).AddDays(numberOFDays).ToString("dd-MM-yyyy");
                    Console.WriteLine("Next month plus number of days with day saturday: " + nextMonthDate);
                    return nextMonthDate;
                }
                var currentDayPlusTwo = DateTime.Now.AddDays(1).AddDays(1).ToString("dd-MM-yyyy");
                Console.WriteLine("current Day plus one day: " + currentDayPlusTwo);
                return currentDayPlusTwo;
            }
            if (DayOftheWeekString == "Sunday")
            {
                if (currentDay == "30" || currentDay == "31")
                {
                    var nextMonthDate = DateTime.Now.AddMonths(1).AddDays(numberOFDays).ToString("dd-MM-yyyy");
                    Console.WriteLine("Next month plus number of days with day saturday: " + nextMonthDate);
                    return nextMonthDate;
                }
                var currentDayPlusTwo = DateTime.Now.AddDays(1).ToString("dd-MM-yyyy");
                Console.WriteLine("current Day plus one day: " + currentDayPlusTwo);
                return currentDayPlusTwo;
            }
            var currentDayPlus = DateTime.Now.AddDays(1).ToString("dd-MM-yyyy");
            Console.WriteLine("current Day plus one day: " + currentDayPlus);
            return currentDayPlus;
        }

        public async Task<bool> IsApplyTextDisplayed()
        {
            return await reuseableClass.IsElementVisibleAsync(page, ApplyLeaveText);
        }

        public async Task<string> ActualNumberOfDays()
        {
            var fullText = await reuseableClass.GetInnerText(page, NumberOfDaysText);
            Console.WriteLine("Actual Number of days form UI before triming: " + fullText);
            var parts = fullText.Split(':');
            if (parts.Length > 1)
            {
                return parts[1].Trim();
            }
            Console.WriteLine("Actual Number of days form UI after triming: " + fullText);
            return fullText;
        }

        public Task<string> ExpectedNumberOfDays()
        {
            var expected = numberOFDays.ToString();
            return Task.FromResult(expected);
        }

        public async Task<string> GetActualStartDate()
        {
            return await reuseableClass.GetInnerText(page, StartDate);
        }

        public async Task<string> GetActualEndtDate()
        {
            return await reuseableClass.GetInnerText(page, EndDate);
        }

        public async Task<string> GetActualNumberOfDays()
        {
            return await reuseableClass.GetInnerText(page, ActualNumberOfDaysOnUI);
        }
    }
}
