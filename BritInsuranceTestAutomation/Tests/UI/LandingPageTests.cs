using BritInsuranceTestAutomation.Core;
using BritInsuranceTestAutomation.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V128.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BritInsuranceTestAutomation.Utils;

namespace BritInsuranceTestAutomation.Tests.UI
{
    [TestFixture]
    public class LandingPageTests : BaseTest
    {
        [Test]
        public void ValidateSearchResults()
        {
            LandingPage landingPage = new LandingPage(Driver);
            SearchResultsPage searchResultsPage = new SearchResultsPage(Driver);

            List<string> expectedSearchResults = new List<string> { "Financials", "Interim results for the six months ended 30 June 2022", "Results for the year ended 31 December 2023", "Interim Report 2023", "Kirstin Simon", "Gavin Wilkinson", "Simon Lee", "John King" };


            // Verify landing page is displayed
            Assert.That(landingPage.IsLandingPageLoaded(), Is.True);

            // Search text
            landingPage.ClickOnSearchButton();
            landingPage.EnterTextInSearchBox(UIHelper.GetTestData("searchtext"));


            //Validate the results in the search results page
            string searchedText = searchResultsPage.GetSearchResultHeader();
            Assert.That(searchedText, Is.EqualTo(UIHelper.GetTestData("searchtext")));


            List<string> searchResultLinks = searchResultsPage.GetResultLinks();
            Assert.That(searchResultLinks.Count, Is.EqualTo(expectedSearchResults.Count));
            Assert.That(searchResultLinks, Is.EqualTo(expectedSearchResults));
        }
    }
}
