
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Free_key_vnhax
{
    internal class Program
    {

        [STAThread]
        static void Main(string[] args)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.facebook.com/vtpn.cnc/",
                UseShellExecute = true
            });
            if (!System.IO.File.Exists("ex.crx"))
            {
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    client.DownloadFileAsync(new Uri("https://cdn.discordapp.com/attachments/815703837675094056/1042487050487742555/FastForward_chromium_2159_dev.crx"), "ex.crx");
                }
            }
            ChromeOptions options = new ChromeOptions();
            options.AddExtension(@"ex.crx");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--window-position=-32000,-32000");
            //options.AddArgument("--headless");
            System.Environment.SetEnvironmentVariable("webdriver.chrome.driver", @"chromedriver.exe");
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            ChromeDriver chromeDriver = new ChromeDriver(chromeDriverService, options);
            chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles[0]);
            chromeDriver.Url = "https://vnhax.com/client-side/pubg/active?action=get";
            chromeDriver.Navigate();
            IJavaScriptExecutor js = chromeDriver as IJavaScriptExecutor;
            var dataFromJS = (string)js.ExecuteScript("var content = document.getElementsByClassName('container')[1].children[0].innerText;return content;");
            string _key = dataFromJS.Replace("Mã kích hoạt của bạn là:", "").Replace("Kích hoạt thành công !", "").Trim();
            MessageBox.Show("Key đã lấy thành công, đã copy key vào bộ nhớ tạm!");
            chromeDriver.Quit();
            Clipboard.SetText(_key);
        }
    }
}
