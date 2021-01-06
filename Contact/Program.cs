using System;
using System.Collections.ObjectModel;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
namespace Contact
{
    class Program
    {
        static void Main(string[] args)
        {
             string Name= "Srikanth";
             IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost/Cloud/Patient/Login.php";

            driver.Manage().Window.Maximize();


            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            try
            {

                string s = driver.Title;
                Console.WriteLine("Title is " + s);

               String Att_id= driver.FindElement(By.Id("patient_username")).GetAttribute("name");
                Console.WriteLine("Attribut is " + Att_id);

                ReadOnlyCollection<IWebElement> eles= driver.FindElements(By.TagName("a"));
               int elems= eles.Count;
                Console.WriteLine("Elements  is " + elems);

                String ses = eles[1].Text;
                Console.WriteLine("Text of First Tag  is " +ses);

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Id("patient_username")));
               // driver.FindElement(By.Id("patient_username")).SendKeys("Ganesh");
                js.ExecuteScript("document.getElementById('patient_username').value='Ganesh';");

                IWebElement pwd = driver.FindElement(By.XPath("//input[@id='patient_password']"));
                js.ExecuteScript("arguments[0].value='Test123';", pwd);
                 
                driver.FindElement(By.XPath("//button[@type='submit']")).Click();

                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//a[contains(@title,'Search and book an appointment')]")));
                driver.FindElement(By.XPath("//a[contains(@title,'Search and book an appointment')]")).Click();

                //wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//table[@id='example']")));
                //string Names= driver.FindElement(By.XPath("//table[@id='example']/tbody/tr/td[2]")).Text;

                Thread.Sleep(3000);

                int cnt = driver.FindElements(By.XPath("//table[@id='example']/tbody/tr/td[2]")).Count;
                Console.WriteLine(cnt);


                int cln = driver.FindElements(By.XPath("//table[@id='example']/thead/tr/th")).Count;
                Console.WriteLine(cln);

                for (int i=1;i<=cnt;i++)
                {
                    for (int j = 1; j <= cln; j++)
                    {
                        string Names = driver.FindElement(By.XPath("//table[@id='example']/tbody/tr[" + i + "]/td["+j+"]")).Text;
                        if (Names.Contains(Name))
                        {
                            driver.FindElement(By.XPath("//table[@id='example']/tbody/tr[" + i + "]/td[2]")).Click();
                            break;
                        }
                    }
                }


                js.ExecuteScript("window.scrollBy(0,250)","");
             

                driver.FindElement(By.XPath("//button[contains(text(),'Next')]")).Click();

                Thread.Sleep(3000);
                Screenshot pass = ((ITakesScreenshot)driver).GetScreenshot();
                pass.SaveAsFile("C:\\Users\\pc\\source\\repos\\Contact\\Selenium_Training_PASS" + DateTime.Now.ToString("yyyy-MM-dd_HHmmss") + ".png");
               
            }
            catch (Exception)
            {

                Screenshot Fail = ((ITakesScreenshot)driver).GetScreenshot();
                Fail.SaveAsFile(@"C:\\Users\\pc\\source\\repos\\Contact\\Selenium_Training-PASS" + DateTime.Now.ToString("yyyy-MM-dd_HHmmss") + ".png");
                driver.Close();
            }

            driver.Close();
        }
    }
}
