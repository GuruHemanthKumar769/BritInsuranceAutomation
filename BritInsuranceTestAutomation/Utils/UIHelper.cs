using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using Newtonsoft.Json.Linq;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System.Diagnostics;

namespace BritInsuranceTestAutomation.Utils
{
    public class UIHelper
    {
        IWebDriver Driver;
        public UIHelper(IWebDriver driver)
        {
            Driver = driver;
        }
        public IWebElement WaitForElementToBeDisplayed(int timeInSeconds, IWebDriver driver, By element)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeInSeconds));
            return wait.Until(ExpectedConditions.ElementExists(element));
        }

        public IWebElement WaitForElementToBeClickable(int timeInSeconds, IWebDriver driver, By element)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeInSeconds));
            return wait.Until(ExpectedConditions.ElementToBeClickable(element));
        }


        public static string GetTestData(string parameter)
        {

            string configStream = File.ReadAllText(@"../../../TestData/test.json");
            var config = JObject.Parse(configStream.ToString());
            string configValue = config.GetValue(parameter).ToString();
            return configValue;

        }

        public void ScrollToElement(IWebElement element, IWebDriver driver, int timeInSeconds = 5)
        {
            int timeInMilliSeconds = timeInSeconds * 1000;
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }


        /// <summary>
        /// Enhanced page load wait with multiple conditions
        /// </summary>
        public void WaitForPageLoad(IWebDriver driver, int timeInSeconds)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeInSeconds));

            try
            {
                // Wait for Javascript to load
                wait.Until(driver =>
                {
                    try
                    {
                        string state = ((IJavaScriptExecutor)driver)
                            .ExecuteScript("return document.readyState")
                            .ToString();
                        return state.Equals("complete", StringComparison.OrdinalIgnoreCase);
                    }
                    catch (InvalidOperationException)
                    {
                        return false;
                    }
                    catch (NoSuchWindowException)
                    {
                        return false;
                    }
                });

                // Wait for jQuery to load
                wait.Until(driver =>
                {
                    try
                    {
                        var jQueryLoaded = ((IJavaScriptExecutor)driver)
                            .ExecuteScript("return typeof jQuery != 'undefined'")
                            .ToString()
                            .Equals("true", StringComparison.OrdinalIgnoreCase);

                        if (!jQueryLoaded) return true;

                        var jQueryReady = ((IJavaScriptExecutor)driver)
                            .ExecuteScript("return jQuery.active")
                            .ToString()
                            .Equals("0", StringComparison.OrdinalIgnoreCase);

                        return jQueryReady;
                    }
                    catch (InvalidOperationException)
                    {
                        return false;
                    }
                    catch (NoSuchWindowException)
                    {
                        return false;
                    }
                });

                // Wait for Angular if present
                wait.Until(driver =>
                {
                    try
                    {
                        var angularLoaded = ((IJavaScriptExecutor)driver)
                            .ExecuteScript("return typeof angular != 'undefined'")
                            .ToString()
                            .Equals("true", StringComparison.OrdinalIgnoreCase);

                        if (!angularLoaded) return true;

                        var angularReady = ((IJavaScriptExecutor)driver)
                            .ExecuteScript("return angular.element(document).injector().get('$http').pendingRequests.length === 0")
                            .ToString()
                            .Equals("true", StringComparison.OrdinalIgnoreCase);

                        return angularReady;
                    }
                    catch (InvalidOperationException)
                    {
                        return false;
                    }
                    catch (NoSuchWindowException)
                    {
                        return false;
                    }
                });

                // Wait for AJAX calls to complete
                wait.Until(driver =>
                {
                    try
                    {
                        var xhrComplete = ((IJavaScriptExecutor)driver)
                            .ExecuteScript(@"
                            var pending = false;
                            if (window.XMLHttpRequest) {
                                var xmlHttp = new XMLHttpRequest();
                                if (xmlHttp.readyState != null && xmlHttp.readyState != 0 && xmlHttp.readyState != 4) {
                                    pending = true;
                                }
                            }
                            return !pending;
                        ")
                            .ToString()
                            .Equals("true", StringComparison.OrdinalIgnoreCase);

                        return xhrComplete;
                    }
                    catch (InvalidOperationException)
                    {
                        return false;
                    }
                    catch (NoSuchWindowException)
                    {
                        return false;
                    }
                });

                // Additional check for any animations
                wait.Until(driver =>
                {
                    try
                    {
                        var animationsComplete = ((IJavaScriptExecutor)driver)
                            .ExecuteScript(@"
                            var pending = false;
                            if (window.jQuery) {
                                pending = jQuery(':animated').length > 0;
                            }
                            return !pending;
                        ")
                            .ToString()
                            .Equals("true", StringComparison.OrdinalIgnoreCase);

                        return animationsComplete;
                    }
                    catch (InvalidOperationException)
                    {
                        return false;
                    }
                    catch (NoSuchWindowException)
                    {
                        return false;
                    }
                });
            }
            catch (WebDriverTimeoutException ex)
            {
                throw new Exception($"Page did not load completely within {timeInSeconds} seconds. Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while waiting for page load: {ex.Message}");
            }
        }

        public bool IsElementDisplayed(IWebDriver driver, By locator, int timeoutSeconds = 5) =>
         new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds))
        .Until(d => d.FindElement(locator).Displayed);


        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
