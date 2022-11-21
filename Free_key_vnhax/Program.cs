
using KeyAuth;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Free_key_vnhax
{
    internal class Program
    {
        static api KeyAuthApp = new api(
name: "Get_key_vnhax",
ownerid: "JGyKOLSBtY",
secret: "5b685f6f985ee6d0e5f0059d405034de543827136d706e3d1d10df433b6d1657",
version: "1.0"
);

        static void autoUpdate()
        {
            if (KeyAuthApp.response.message == "invalidver")
            {
                if (!string.IsNullOrEmpty(KeyAuthApp.app_data.downloadLink))
                {
                    Console.WriteLine("\n Auto update avaliable!");
                    int choice = 2;
                    switch (choice)
                    {
                        case 1:
                            Process.Start(KeyAuthApp.app_data.downloadLink);
                            Environment.Exit(0);
                            break;
                        case 2:
                            Console.WriteLine(" Downloading file directly..");
                            Console.WriteLine(" New file will be opened shortly..");

                            WebClient webClient = new WebClient();
                            string destFile = Application.ExecutablePath;

                            string rand = random_string();

                            destFile = destFile.Replace(".exe", $"-{rand}.exe");
                            webClient.DownloadFile(KeyAuthApp.app_data.downloadLink, destFile);

                            Process.Start(destFile);
                            Process.Start(new ProcessStartInfo()
                            {
                                Arguments = "/C choice /C Y /N /D Y /T 3 & Del \"" + Application.ExecutablePath + "\"",
                                WindowStyle = ProcessWindowStyle.Hidden,
                                CreateNoWindow = true,
                                FileName = "cmd.exe"
                            });
                            Environment.Exit(0);

                            break;
                        default:
                            Console.WriteLine(" Invalid selection, terminating program..");
                            Thread.Sleep(1500);
                            Environment.Exit(0);
                            break;
                    }
                }
                Console.WriteLine("\n Status: Version of this program does not match the one online. Furthermore, the download link online isn't set. You will need to manually obtain the download link from the developer.");
                Thread.Sleep(2500);
                Environment.Exit(0);
            }
        }
        static string random_string()
        {
            string str = null;

            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                str += Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))).ToString();
            }
            return str;
        }
        [STAThread]
        static void Main(string[] args)
        {
            KeyAuthApp.init();
            autoUpdate();
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.facebook.com/vtpn.cnc/",
                UseShellExecute = true
            });
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.bothax.com/",
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
            var dataFromJS = (string)js.ExecuteScript("var content = document.getElementsByClassName('container')[1].children[0].innerText;var txt = content.substring(content.indexOf(\":\")+2);return txt;");
            string _key = dataFromJS.Trim();
            Console.WriteLine(_key);
            if (_key.Length != 0)
            {
                Console.Beep(400, 400);
                MessageBox.Show("Key đã lấy thành công, đã copy key vào bộ nhớ tạm!");
            }
            chromeDriver.Quit();
            Process.Start(new ProcessStartInfo
            {
                FileName = "taskkill",
                Arguments = $"/im chromedriver /f /t",
                CreateNoWindow = true,
                UseShellExecute = false
            }).WaitForExit();
            Clipboard.SetText(_key);
        }
    }
}
