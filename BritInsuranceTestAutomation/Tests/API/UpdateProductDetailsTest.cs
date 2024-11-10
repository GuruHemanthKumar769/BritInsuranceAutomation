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
    public class UpdateProductDetailsTest
    {

        [Test]
        public async Task ValidateProductionUpdation()
        {
            string productName = UIHelper.GenerateRandomString(10);
            List<string> values = new List<string>() { "Intel Core i7", "1 TB", "Dell" };
            int year = 2022;
            double price = 1849.99;
            string response = await ProudctCreation.ProductDetailsAsync(values, year, price);
            var deserializedResponse = JsonConvert.DeserializeObject<ProductResponse>(response);

            Assert.IsNotNull(deserializedResponse);
            Assert.That(deserializedResponse.id, Is.Not.Null);

            string responseOfEditProductDetailsAsync = await ProudctCreation.EditProductDetailsAsync(deserializedResponse.id, productName);


            var deserializedResponseOfUpdatedProduct = JsonConvert.DeserializeObject<ResponseOfUpdatedProduct>(responseOfEditProductDetailsAsync);


            // Validate whether product is updated
            Assert.That(deserializedResponseOfUpdatedProduct.name, Is.EqualTo(productName));

        }

    }
}
