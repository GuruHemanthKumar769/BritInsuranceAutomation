using BritInsuranceTestAutomation.Utils;
using BritInsuranceTestAutomation.Utils.APIModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BritInsuranceTestAutomation.Tests.API
{
    public class ProudctCreation
    {
        public static async Task<string> ProductDetailsAsync(List<string> productValues, int year, double price)
        {
            var RestSharpController = new RestSharpController();
            Product productList = new Product()
            {
                year = year,
                price = price,
                CPUModel = productValues[0],
                hardDiskSize = productValues[1]
            };

            var allProductDetails = new ProductDetails
            {
                name = productValues[2],
                product = productList
            };

            string jsonBody = JsonConvert.SerializeObject(allProductDetails);
            string postResponse = await RestSharpController.PostAsync(TestContext.Parameters["apiURL"] + "/objects", jsonBody);
            return postResponse;
        }

        public static async Task<string> EditProductDetailsAsync(string id,string updatedName)
        {
            var RestSharpController = new RestSharpController();

            var updateName = new UpdateProduct { name = updatedName };
            string jsonBody = JsonConvert.SerializeObject(updateName);   
            string postResponse = await RestSharpController.PatchAsync(TestContext.Parameters["apiURL"] + "/objects/" + id, jsonBody);
            return postResponse;
        }



    }
}
