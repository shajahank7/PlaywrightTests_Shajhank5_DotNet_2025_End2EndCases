using Bogus;

namespace Utilities
{
    public static class FakeData
    {
        private static readonly Faker faker = new();
        private static readonly Random random = new();
        public static string FirstName => faker.Name.FirstName();
        public static string LastName => faker.Name.LastName();
        public static string Email => FirstName+DateTime.Now.ToString("yyyyMMdd")+"@gmail.com";
        public static string Phone => "9" + faker.Random.ReplaceNumbers("#########");
        public static string Department => faker.Commerce.Department();
        public static string Gender => faker.PickRandom(new[] { "Male", "Female", "Other" });
        public static string BloodGroup => faker.PickRandom(new[] { "A+", "A-", "B+", "B-", "O+", "O-", "AB+", "AB-" });
        public static string Location => faker.Address.City();

        // New additions
        public static string EmployeeId => faker.Random.String2(2, "ABCDEFGHIJKLMNOPQRSTUVWXYZ") + faker.Random.Number(1000, 9999);
        public static string Salary => faker.Random.Number(10000, 99999).ToString();
        public static int Date => faker.Random.Number(1, 30);
        public static string Subject => faker.PickRandom(new[] { "Medical appointment", "Family emergency", "Personal work", "Out of station travel", "Sick leave", "Attending a wedding", "Urgent personal reason" });
        public static string RandomId => $"OW{random.Next(1000, 9999)}";
        public static string DOB => faker.Date.Past(30, DateTime.Today.AddYears(-18)).ToString("yyyy-MM-dd");
        public static string DOJ => faker.Date.Past(10, DateTime.Today).ToString("yyyy-MM-dd");
        public static string RandomDesignation => faker.Name.JobTitle();
        public static string RandomDepartment => new[]
        {
            "Development", "Quality Assurance", "DevOps", "IT Support", "Security",
            "Cloud", "Networking", "Database", "User Interface", "Product Management"
        }[random.Next(10)];

        public static string Reason
        {
            get
            {
                switch (Subject)
                {
                    case "Medical appointment":
                        return "I have a scheduled medical appointment and need to be excused for the day.";
                    case "Family emergency":
                        return "There is an urgent family emergency that requires my immediate attention.";
                    case "Personal work":
                        return "I need to attend to some important personal matters that cannot be postponed.";
                    case "Out of station travel":
                        return "I am traveling out of town for personal reasons and will be unavailable.";
                    case "Sick leave":
                        return "I am feeling unwell and need to take rest as advised by my doctor.";
                    case "Attending a wedding":
                        return "I am attending a close relative’s wedding ceremony.";
                    case "Urgent personal reason":
                        return "I have an urgent personal situation that needs to be addressed immediately.";
                    default:
                        return "Personal reason.";
                }
            }
        }

    }

     

}
