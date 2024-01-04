using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PWNunit
{
    [TestFixture]
  internal class EAApp : PageTest
    {
        [SetUp]
        public async Task SetUp()
        {
            Console.WriteLine("Browser Opened");
            await Page.GotoAsync("http://eaapp.somee.com/",
                new PageGotoOptions
                { Timeout=3000,WaitUntil=WaitUntilState.DOMContentLoaded});
            Console.WriteLine("Page loaded");
        }
       
        [Test]
        public async Task LoginTest()
        {
            

            //await Page.GetByText("Login").ClickAsync();
            // var lnkLogin = Page.Locator(selector: "text=Login");
            //await lnkLogin.ClickAsync();
            await Page.ClickAsync(selector:"text=Login",new PageClickOptions

            { Timeout=1000});
            await Console.Out.WriteLineAsync("Link clicke");


            //Console.WriteLine("Clicked on login button");
            await Expect(Page).ToHaveURLAsync("http://eaapp.somee.com/Account/Login");

            /*await Page.GetByLabel("UserName").FillAsync("admin");
            Console.WriteLine("Enter user name");

            await Page.GetByLabel("Password").FillAsync("password");
            Console.WriteLine("Enter password");*/

            await Page.FillAsync(selector: "#UserName", "admin");
            await Page.FillAsync(selector: "#Password", "password");
            await Console.Out.WriteAsync("Typed");


            var btnLogin = Page.Locator(selector: "input", new PageLocatorOptions { HasTextString = "Log in" });
            await btnLogin.ClickAsync();
            Console.WriteLine("Clicked");

            // await Expect(Page).ToHaveTitleAsync("Home - Execute Automation Employee App");
            await Expect(
                Page.Locator(selector: "text='Hello admin!'"))
                // (Page.Locator(selector: "text='Log off'"))
                 .ToBeVisibleAsync();

        } 
        
    }
}