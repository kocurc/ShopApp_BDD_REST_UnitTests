namespace BakeryUnitTests
{
    public class BakeryProduct
    {
        private string _name = "";
        private string _price = "";
        private string _id = "";

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Price
        {
            get => _price;
            set => _price = value;
        }

        public string Id
        {
            get => _id;
            set => _id = value;
        }

        public BakeryProduct(string name, string price, string id)
        {
            Name = name;
            Price = price;
            Id = id;
        }
    }
}
