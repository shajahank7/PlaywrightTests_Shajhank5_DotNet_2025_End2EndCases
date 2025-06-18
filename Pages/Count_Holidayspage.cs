using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class HolidayPage
{
    private readonly IPage page;

    private ILocator HolidayItems => page.Locator("ul.events-list li");

    public HolidayPage(IPage page)
    {
        this.page = page;
    }

    public async Task WaitForHolidaysToLoadAsync()
    {
        await HolidayItems.First.WaitForAsync(new() { Timeout = 10000 });
    }

    public async Task<int> GetHolidayCountAsync()
    {
        return await HolidayItems.CountAsync();
    }

    public async Task<List<string>> GetHolidayTextsAsync()
    {
        var texts = await HolidayItems.AllTextContentsAsync();
        return texts.ToList(); // Requires: using System.Linq;
    }

    public async Task PrintHolidaysToConsoleAsync()
    {
        int count = await GetHolidayCountAsync();
        Console.WriteLine($"✅ Total holidays found: {count}");

        if (count > 0)
        {
            var texts = await GetHolidayTextsAsync();
            Console.WriteLine("🎉 Holidays:");
            foreach (var holiday in texts)
            {
                Console.WriteLine($"- {holiday}");
            }
        }
        else
        {
            Console.WriteLine("⚠️ No holidays found.");
            Console.WriteLine(await page.ContentAsync()); // For debugging
        }
    }
}
