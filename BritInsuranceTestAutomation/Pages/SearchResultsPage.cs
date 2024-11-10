using BritInsuranceTestAutomation.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BritInsuranceTestAutomation.Pages
{
    public class SearchResultsPage : UIHelper
    {
        private IWebDriver Driver;

        public SearchResultsPage(IWebDriver driver) : base(driver)
        {
            Driver = driver;
        }

        #region locators
        private readonly By _searchResults = By.XPath("//div/h1[contains(text(), 'Search')]/span");
        private readonly By _resultLinks = By.XPath("//a[contains(@class,'s-results')]");
        #endregion

        #region webelements
        IWebElement SearchResult => Driver.FindElement(_searchResults);
        IList<IWebElement> ResultLinks => Driver.FindElements(_resultLinks);
        #endregion

        #region
        public string GetSearchResultHeader()
        {
            WaitForElementToBeDisplayed(50, Driver, _searchResults);
            return SearchResult.Text;
        }

        public List<string> GetResultLinks()
        {
            List<string> resultLinks = new List<string>();
            WaitForElementToBeDisplayed(60, Driver, _resultLinks);
            foreach (var results in ResultLinks)
            {
                resultLinks.Add(results.Text);
            }
            return resultLinks;
        }
        #endregion



    }
}

