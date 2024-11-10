# BritInsuranceAutomation


Created framework which supports both UI and API tests . Devolped two tests one is for API and other for UI validation as the requirement document

How to run the tests
1) Test >> Configure Run settings >> Select solution wide runsettings file >> from config folder select the test.runsettings
2) Test >> TestExplorer >> Click on run button

Framework Explanation 
1) Configs : This has all the  runsettings files . Run settings file contains all the details specific to environment . For each enviroment we can have one runsettings file
2) Core : Contains base classes and core framework functionality.
3) pages: Implements Page Object Model pattern for UI elements and actions for the respective pages
4) Reports : Contains test execution reports and logging configuration
5) TestData: Created a json file which reads the key and value will be the output. so that we dont hardcode the test data.
6) Tests: Contains UI and API tests for and all the assertions are performed in the test classes 
7) Utils: Contains helper classes, extensions, and API models. [API models are the objects created for the individual APIs] [Helper class contains the all the customised methods for the UI selenium methods and RestSharpController  :  contains all the CURD operation resuable methods, which is single point of contact for all the tests which will be created for the APIs]
