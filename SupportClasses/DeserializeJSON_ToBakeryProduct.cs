using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BakeryUnitTests
{
    public static class DeserializeJSON_ToBakeryProduct
    {
        public static BakeryProduct GetAddedBakeryProductFromGetSpecificOrNewestIdRequest(string jsonResponse)
        {
            dynamic deserializedResponse = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

            string id = deserializedResponse.id;
            string name = deserializedResponse.name;
            string price = deserializedResponse.price;

            return new BakeryProduct(name, price, id);
        }

        public static BakeryProduct GetAddedBakeryProductFromPostRequest(string jsonResponse)
        {
            dynamic deserializedResponse = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

            string id = deserializedResponse.id;
            string name = string.Empty;
            string price = string.Empty;

            return new BakeryProduct(name, price, id);
        }

        public static List<BakeryProduct> GetAddedBakeriesProductFromGetProductsRequest(string jsonResponse)
        {
            JArray jArray = JArray.Parse(jsonResponse);
            List<BakeryProduct> bakeryProducts = jArray.ToObject<List<BakeryProduct>>();

            return bakeryProducts;
        }
    }
}
