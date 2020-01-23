using RestSharp;

namespace BakeryUnitTests.SupportClasses
{
    public class RequestHelper
    {
        private RestClient _client = new RestClient("http://192.168.1.102:5000");
        private RestRequest _getProductsRequest = new RestRequest("products", Method.GET);
        private RestRequest _postProductRequest = new RestRequest("products", Method.POST);
        private RestRequest _getSpecificProductRequest = new RestRequest("products/{id}", Method.GET);
        private RestRequest _getNewestProductRequest = new RestRequest("products/newest_id", Method.GET);

        public RestClient Client
        {
            get { return _client; }
            set { _client = value; }
        }

        public RestRequest GetProductsRequest
        {
            get { return _getProductsRequest; }
            set { _getProductsRequest = value; }
        }

        public RestRequest GetSpecificProductRequest
        {
            get { return _getSpecificProductRequest; }
            set { _getSpecificProductRequest = value; }
        }

        public RestRequest GetNewestProductRequest
        {
            get { return _getNewestProductRequest; }
            set { _getNewestProductRequest = value; }
        }

        public RestRequest PostProductRequest
        {
            get { return _postProductRequest; }
            set { _postProductRequest = value; }
        }

        public void SetUpJsonHeaders()
        {
            GetProductsRequest.AddHeader("Content-Type", "application/json");
            PostProductRequest.AddHeader("Content-Type", "application/json");
            GetSpecificProductRequest.AddHeader("Content-Type", "application/json");
            GetNewestProductRequest.AddHeader("Content-Type", "application/json");
        }

        private void AddJsonBodyToPostProductRequest(string name, string price)
        {
            object jsonRequestBody = FormatPostProductRequestBody(name, price);

            PostProductRequest.AddJsonBody(jsonRequestBody);
        }

        public string FormatPostProductRequestBody(string name, string price)
        {
            return $"{{\"name\":\"{name}\",\"price\":\"{price}\",\"id\":\"\"}}";
        }

        public string ExecutePostProductRequest(string name, string price)
        {
            AddJsonBodyToPostProductRequest(name, price);

            return Client.Execute(PostProductRequest).Content;
        }

        public string ExecuteGetSpecificProductRequest(string id)
        {
            GetSpecificProductRequest.AddUrlSegment("id", id);

            return Client.Execute(GetSpecificProductRequest).Content;
        }
    }
}
