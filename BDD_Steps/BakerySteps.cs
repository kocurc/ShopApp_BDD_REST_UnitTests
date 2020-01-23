using System.Collections.Generic;
using BakeryUnitTests.SupportClasses;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BakeryUnitTests.BDD_Steps
{
    [Binding]
    [Scope(Feature = "BakeryOwner")]
    public class BakerySteps
    {
        private RequestHelper requestHelper = new RequestHelper();

        [Given(@"Bakery goods are prepared for viewing")]
        public void GivenBakeryGoodsArePreparedForViewing()
        {
            requestHelper.SetUpJsonHeaders();
        }

        // PASSED

        [Given(@"Owner has baked new product '(.*)' that costs '(.*)'")]
        public void GivenOwnerHasBakedNewProductThatCosts(string name, string price)
        {
            string jsonResponse = requestHelper.ExecutePostProductRequest(name, price);
            BakeryProduct bakeryProduct = DeserializeJSON_ToBakeryProduct.GetAddedBakeryProductFromPostRequest(jsonResponse);
            
            ScenarioContext.Current.Set(bakeryProduct.Id, "id");
        }

        [Then(@"Customer can buy new product")]
        public void ThenCustomerCanBuyNewProduct()
        {
            string jsonResponse = requestHelper.ExecuteGetSpecificProductRequest(ScenarioContext.Current["id"].ToString());
            BakeryProduct bakeryProduct = DeserializeJSON_ToBakeryProduct.GetAddedBakeryProductFromGetSpecificOrNewestIdRequest(jsonResponse);

            Assert.AreEqual(ScenarioContext.Current["id"].ToString(), bakeryProduct.Id);
        }

        // PASSED

        [Given(@"Owner has baked product that already exists")]
        public void GivenOwnerHasBakedProductThatAlreadyExists()
        {
            string jsonResponse = requestHelper.ExecutePostProductRequest("Bread", "3.45");
            BakeryProduct bakeryProduct = DeserializeJSON_ToBakeryProduct.GetAddedBakeryProductFromPostRequest(jsonResponse);

            ScenarioContext.Current.Set(bakeryProduct.Id, "id");
        }

        // PASSED

        [Given(@"Owner has baked product '(.*)' without price")]
        public void GivenOwnerHasBakedProductWithoutPrice(string name)
        {
            string jsonResponse = requestHelper.ExecutePostProductRequest(name, null);
            BakeryProduct bakeryProduct = DeserializeJSON_ToBakeryProduct.GetAddedBakeryProductFromPostRequest(jsonResponse);

            ScenarioContext.Current.Set(bakeryProduct.Id, "id");
        }

        [Then(@"Customer cannot buy new product without price")]
        public void ThenCustomerCannotBuyNewProductWithoutPrice()
        {
            string jsonResponse = requestHelper.ExecuteGetSpecificProductRequest(ScenarioContext.Current["id"].ToString());
            BakeryProduct bakeryProduct = DeserializeJSON_ToBakeryProduct.GetAddedBakeryProductFromGetSpecificOrNewestIdRequest(jsonResponse);

            Assert.AreEqual(ScenarioContext.Current["id"].ToString(), bakeryProduct.Id);
            Assert.AreEqual(string.Empty, bakeryProduct.Price);
        }

        // PASSED

        [Given(@"Owner has baked new product with price '(.*)' and without name")]
        public void GivenOwnerHasBakedNewProductWithPriceWithoutName(string price)
        {
            string jsonResponse = requestHelper.ExecutePostProductRequest(string.Empty, price);
            BakeryProduct bakeryProduct = DeserializeJSON_ToBakeryProduct.GetAddedBakeryProductFromPostRequest(jsonResponse);

            ScenarioContext.Current.Set(bakeryProduct.Id, "id");
        }

        [Then(@"Customer cannot buy new product without name")]
        public void ThenCustomerCannotBuyNewProductWithoutName()
        {
            string jsonResponse = requestHelper.ExecuteGetSpecificProductRequest(ScenarioContext.Current["id"].ToString());
            BakeryProduct bakeryProduct = DeserializeJSON_ToBakeryProduct.GetAddedBakeryProductFromGetSpecificOrNewestIdRequest(jsonResponse);

            Assert.AreEqual(ScenarioContext.Current["id"].ToString(), bakeryProduct.Id);
            Assert.AreEqual(string.Empty, bakeryProduct.Name);
        }

        // PASSED

        [Given(@"Owner has baked new product without name and price")]
        public void GivenOwnerHasBakedNewProductWithoutNameAndPrice()
        {
            string jsonResponse = requestHelper.ExecutePostProductRequest(string.Empty, null);
            BakeryProduct bakeryProduct = DeserializeJSON_ToBakeryProduct.GetAddedBakeryProductFromPostRequest(jsonResponse);

            ScenarioContext.Current.Set(bakeryProduct.Id, "id");
        }

        [Then(@"Customer cannot buy new product without name and price")]
        public void ThenCustomerCannotBuyNewProductWithoutNameAndPrice()
        {
            string jsonResponse = requestHelper.ExecuteGetSpecificProductRequest(ScenarioContext.Current["id"].ToString());
            BakeryProduct bakeryProduct = DeserializeJSON_ToBakeryProduct.GetAddedBakeryProductFromGetSpecificOrNewestIdRequest(jsonResponse);

            Assert.AreEqual(ScenarioContext.Current["id"].ToString(), bakeryProduct.Id);
            Assert.AreEqual(string.Empty, bakeryProduct.Name);
            Assert.AreEqual(string.Empty, bakeryProduct.Price);
        }

        // PASSED

        [When(@"Customer asks for all bakery goods")]
        public void WhenCustomerAsksForAllBakeryGoods()
        {
            string jsonResponse = requestHelper.Client.Execute(requestHelper.GetProductsRequest).Content;
            List<BakeryProduct> bakeryProducts = DeserializeJSON_ToBakeryProduct.GetAddedBakeriesProductFromGetProductsRequest(jsonResponse);

            ScenarioContext.Current.Set(bakeryProducts, "bakeryProducts");
        }

        [Then(@"All bakery goods are visible for customer")]
        public void ThenAllBakeryGoodsAreVisibleForCustomer()
        {
            List<BakeryProduct> bakeryProducts = ScenarioContext.Current.Get<List<BakeryProduct>>("bakeryProducts");

            Assert.That(bakeryProducts.Count, Is.GreaterThan(1));
        }

        // NOT PASSED - Bug to investigate
        // newest_id returns product with "None" (previously added incorrect product) instead of newest product

        [When(@"Customer asks for most fresh bakery good")]
        public void WhenCustomerAsksForMostFreshBakeryGood()
        {
            string jsonResponse = requestHelper.Client.Execute(requestHelper.GetNewestProductRequest).Content;
            BakeryProduct bakeryProduct = DeserializeJSON_ToBakeryProduct.GetAddedBakeryProductFromGetSpecificOrNewestIdRequest(jsonResponse);

            ScenarioContext.Current.Set(bakeryProduct.Id, "id");
        }

        [Then(@"Bakery good with name '(.*)' and price '(.*)' is visible is visible")]
        public void ThenBakeryGoodWithNameAndPriceIsVisibleIsVisible(string name, string price)
        {
            string jsonResponse = requestHelper.ExecuteGetSpecificProductRequest(ScenarioContext.Current["id"].ToString());
            BakeryProduct bakeryProduct = DeserializeJSON_ToBakeryProduct.GetAddedBakeryProductFromGetSpecificOrNewestIdRequest(jsonResponse);

            Assert.AreEqual(ScenarioContext.Current["id"].ToString(), bakeryProduct.Id);
            Assert.AreEqual(name, bakeryProduct.Name);
            Assert.AreEqual(price, bakeryProduct.Price);
        }
    }
}
