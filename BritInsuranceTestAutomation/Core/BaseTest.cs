using NUnit.Framework;
using OpenQA.Selenium;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using BritInsuranceTestAutomation.Pages;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Chrome;
using AventStack.ExtentReports.Reporter.Model;
using BritInsuranceTestAutomation.Utils;

namespace BritInsuranceTestAutomation.Core
{
    [TestFixture]
    public class BaseTest
    {
        protected IWebDriver Driver;
        private static ExtentReports _extent;
        private static ExtentSparkReporter _htmlReporter;
        protected ExtentTest _test;
        private readonly string _reportFolderPath;
        private readonly string _screenshotFolderPath;

        public BaseTest()
        {
            // Define report and screenshot folder paths
            _reportFolderPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Reports");
            _screenshotFolderPath = Path.Combine(_reportFolderPath, "Screenshots");
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            
            // Initialize Extent Report with timestamp
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string reportFilePath = Path.Combine(_reportFolderPath, $"TestReport_{timestamp}.html");

            _htmlReporter = new ExtentSparkReporter(reportFilePath);
            _extent = new ExtentReports();

            // Configure HTML Reporter
            ConfigureHtmlReporter();

            _extent.AttachReporter(_htmlReporter);
        }

        
        private void ConfigureHtmlReporter()
        {
            _htmlReporter.Config.DocumentTitle = "Test Automation Report";
            _htmlReporter.Config.ReportName = "Test Execution Report";
            _htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Standard;
        }

  
        [SetUp]
        public void Setup()
        {
            _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
            _test.AssignCategory(TestContext.CurrentContext.Test.ClassName);

            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();
            Driver.Navigate().GoToUrl(TestContext.Parameters["url"]);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var stackTrace = TestContext.CurrentContext.Result.StackTrace;
                var errorMessage = TestContext.CurrentContext.Result.Message;

                switch (status)
                {
                    case TestStatus.Failed:
                        _test.Fail($"Test Failed: {errorMessage}")
                            .Fail(stackTrace);
                        TakeScreenshot($"Failed_Test_{TestContext.CurrentContext.Test.Name}");
                        break;

                    case TestStatus.Skipped:
                        _test.Skip("Test Skipped");
                        break;

                    default:
                        _test.Pass("Test Passed");
                        break;
                }
            }
            catch (Exception ex)
            {
                _test.Fail($"Test failed with exception: {ex.Message}");
                Console.WriteLine($"Error in TearDown: {ex.Message}");
            }
            finally
            {
                if (Driver != null)
                {
                    Driver.Quit();
                    Driver.Dispose();
                }
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            try
            {
                _extent.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OneTimeTearDown: {ex.Message}");
            }
        }

        protected void TakeScreenshot(string screenshotName)
        {
            try
            {
                var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var filename = Path.Combine(_screenshotFolderPath, $"{screenshotName}_{timestamp}.png");

                screenshot.SaveAsFile(filename);
                _test.AddScreenCaptureFromPath(filename);
            }
            catch (Exception ex)
            {
                _test.Fail($"Failed to capture screenshot: {ex.Message}");
                Console.WriteLine($"Error taking screenshot: {ex.Message}");
            }
        }

        protected void LogTestStep(string stepDescription, Status status = Status.Pass)
        {
            _test.Log(status, stepDescription);
        }
    }
}
