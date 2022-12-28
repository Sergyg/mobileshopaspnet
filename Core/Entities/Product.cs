namespace Core.Entities
{
    public class Product : BaseEntity
    {
        // public Product(string name, string description, decimal price, string pictureUrl, ProductType productType, int productTypeId, ProductBrand productBrand, int productBrandId)
        // {
        //     Name = name;
        //     Description = description;
        //     Price = price;
        //     PictureUrl = pictureUrl;
        //     ProductType = productType;
        //     ProductTypeId = productTypeId;
        //     ProductBrand = productBrand;
        //     ProductBrandId = productBrandId;
        // }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public int ProductBrandId { get; set; }
    }
}