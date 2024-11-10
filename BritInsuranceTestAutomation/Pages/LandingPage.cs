using BritInsuranceTestAutomation.Utils;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BritInsuranceTestAutomation.Pages
{
    public class LandingPage : UIHelper
    {
        private readonly IWebDriver Driver;
        public LandingPage(IWebDriver driver) : base(driver)
        {
            Driver = driver;
        }

        #region locators
        private readonly By _searchButton = By.XPath("//button[@aria-label='Search button']");
        private readonly By _searchTextbox = By.XPath("//input[contains(@placeholder,'Search for people')]");
        private readonly By _logo = By.XPath("//a[@aria-label='Logo']");

        #endregion

        #region webelements
        IWebElement SearchButton => Driver.FindElement(_searchButton);
        IWebElement SearchTextbox => Driver.FindElement(_searchTextbox);
        IWebElement Logo => Driver.FindElement(_logo);
        #endregion


        public void ClickOnSearchButton()
        {
            WaitForPageLoad(Driver, 30);
            WaitForElementToBeDisplayed(60, Driver, _searchButton);
            WaitForElementToBeClickable(60, Driver, _searchButton);
            ScrollToElement(SearchTextbox, Driver, 10);
            SearchButton.Click();
        }

        public void EnterTextInSearchBox(string value)
        {
            WaitForElementToBeDisplayed(60, Driver, _searchTextbox);
            WaitForElementToBeClickable(60, Driver, _searchTextbox);
            SearchTextbox.Click();
            SearchTextbox.Clear();
            SearchTextbox.SendKeys(value);
            SearchTextbox.SendKeys(Keys.Enter);
        }

        public bool IsLandingPageLoaded()
        {

            return IsElementDisplayed(Driver, _logo, 50);


        }

    }

}
