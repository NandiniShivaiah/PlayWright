using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PWPOM.PWTests.Pages;
using PWPOM.Test_Data_Classes;
using PWPOM.Utilities;

namespace PWPOM.PWTests.Tests
{
    [TestFixture]
    public class LoginPageTest : PageTest
    {
        private string currdir;

        [SetUp]
        public async Task SetUp()
        {
            Console.WriteLine("Browser Opened");
            await Page.GotoAsync("http://eaapp.somee.com/",
                new PageGotoOptions
                { Timeout = 5000, WaitUntil = WaitUntilState.DOMContentLoaded });
            Console.WriteLine("Page loaded");
        }

        [Test]
        [TestCase("admin", "password")]
        [TestCase("admin", "abc")]
        public async Task LoginTest(string username, string password)
        {
            // LoginPage loginPage = new (Page);
            NewLoginPage loginPage = new(Page);
            string? excelFilePath = currdir + "/Test Data/EAData.xlsx";
            string? sheetName = "Login Data";

            List<EAText> excelDataList = DataRead.ReadLoginCredData(excelFilePath, sheetName);

            foreach (var excelData in excelDataList)
            {
                string? username = excelData.UserName;
                string? password = excelData.Password;

                await loginPage.ClickLoginLink();
                await loginPage.Login(username, password);

                await Page.ScreenshotAsync(new()
                {
                    Path = currdir + "/Screenshots/screenshot.png",
                    FullPage = true,
                });

                Assert.IsTrue(await loginPage.ChkWelMess());
            }
        }
    }
}