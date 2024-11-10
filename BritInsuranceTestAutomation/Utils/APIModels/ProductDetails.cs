using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BritInsuranceTestAutomation.Utils.APIModels
{
    public class ProductDetails
    {
        public string name { get; set; }
        public Product product { get; set; }
    }

    public class Product
    {
        public int year { get; set; }
        public double price { get; set; }
        public string CPUModel { get; set; }
        public string hardDiskSize { get; set; }
    }

    public class ProductResponse
    {
        public string id { get; set; }
        public string name { get; set; }
        public DateTime createdAt { get; set; }
        public Product data { get; set; }
    }


    public class UpdateProduct
    {
        public string name { get; set; }
    }

    public class ResponseOfUpdatedProduct
    {

        public string id { get; set; }
        public string name { get; set; }
        public Product data { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
